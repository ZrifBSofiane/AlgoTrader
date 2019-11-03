using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BusMapping;
using DataHistoricalRepository.DataAccess;
using DataHistoricalRepository.Models;
using Management.Interfaces;
using Management.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Service.Classes;
using Service.DTO;
using Service.Interfaces;

namespace Web.Realtime
{
    public class PlatformHub : Hub
    {
        private readonly IAccountService _accountService = new AccountService();
        private readonly static TransactionMapping<string> _transactions = new TransactionMapping<string>();
        private readonly static PriceRealTime<string> _priceMapping = new PriceRealTime<string>();
        private readonly IPnl _pnlManagement = new Pnl();
        private static Dictionary<string, string> signalRidServer = new Dictionary<string, string>();
        private readonly static HistoricalDataRepository historical = new HistoricalDataRepository();
        private readonly ITransactionService _transactionService = new TransactionService();


        private delegate void NewQuoteDetailedEvent(string symbol, double bid, double ask, double timestamp, double open, double high, double low, double close, double volume);
        private event NewQuoteDetailedEvent OnNewQuoteDetailed;

        public PlatformHub()
        {
            //OnNewQuoteDetailed += Test();
        }

        //private NewQuoteDetailedEvent Test()
        //{
        //    throw new NotImplementedException();
        //}



        #region De/Re/Connected
        public override Task OnConnected()
        {
            var userId = Context.User.Identity.GetUserId();
            if (!string.IsNullOrWhiteSpace(userId))
            {
                _accountService.UpdateSignalRId(new Guid(userId), Context.ConnectionId);
                var products = _transactionService.GetLiveDeal(userId);
                products.ForEach(p =>
                {
                    ConnectionMappingSingleton.Add(p.Product.Name, Context.ConnectionId, userId);
                    _transactions.Add(p.Product.Name, p);
                });
            }
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            var userId = Context.User.Identity.GetUserId();
            if (!string.IsNullOrWhiteSpace(userId))
            {
                _accountService.UpdateSignalRId(new Guid(userId), Context.ConnectionId);
            }
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userId = Context.User.Identity.GetUserId();
            if (!string.IsNullOrWhiteSpace(userId))
            {
                _accountService.UpdateSignalRId(new Guid(userId), null);
                ConnectionMappingSingleton.RemoveFromAll(Context.ConnectionId);
            }
            return base.OnDisconnected(stopCalled);
        }
        #endregion

        #region Un/Subscribe
        public bool onSubscribe(string ticker)
        {
            ConnectionMappingSingleton.Add(ticker, Context.ConnectionId, Context.User.Identity.GetUserId());
            return true;
        }

        public bool onUnSubscribe(string ticker)
        {
            ConnectionMappingSingleton.Remove(ticker, Context.ConnectionId);
            return true;
        }
        #endregion


        // Only the server connected to broker can use this function
        public void SendQuote(string ticker, double bid, double ask)
        {
            var test = _transactions.GetConnections("EURUSD");
            var clients = ConnectionMappingSingleton.GetConnections(ticker);
            if (clients.Count() > 0)
            {
                Clients.Clients(clients.Select(t => t.SignalRId).ToList()).onReceivedQuote(ticker, bid, ask);
            }
        }

        public void SendDetailedQuoteAsync(string symbol, double bid, double ask, DateTime dateTime, double open, double high, double low, double close, double volume)
        {
            

            var listTransaction = _transactions.GetConnections(symbol).ToList();
            _pnlManagement.UpdatePnl(ref listTransaction, bid, ask);
             SendPnLUpdate(listTransaction);

            // Updqte database
            var lastPrice = _priceMapping.Get(symbol);
            var timeStamp = ConvertToUnixTimestamp(dateTime);
            if (lastPrice != null)
            {
                if (lastPrice.Time != dateTime)
                {
                    Debug.WriteLine($"Add database Close {lastPrice.Close} Time {lastPrice.Time}");
 /*                   historical.AddDataAsync(new HistoricalData()
                    {
                        Name = symbol,
                        Open = lastPrice.Open,
                        Close = lastPrice.Close,
                        High = lastPrice.High,
                        Low = lastPrice.Low,
                        Date = lastPrice.Time,
                        Time = (long)timeStamp,
                    });*/
                }
            }
            _priceMapping.AddOrUpdate(symbol, new ProductRealTime()
            {
                Ask = ask,
                Bid = bid,
                Name = symbol,
                Time = dateTime,
                Open = (decimal)open,
                High = (decimal)high,
                Close = (decimal)close,
                Low = (decimal)low,
            });


            var clients = ConnectionMappingSingleton.GetConnections(symbol);
            if (clients.Count() > 0)
            {
                Clients.Clients(clients.Select(t => t.SignalRId).ToList()).onReceivedDetailedQuote(symbol, bid, ask, timeStamp, open, high, low, close, volume);
            }
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public void SendPnLUpdate(List<TransactionDTO> transactions)
        {
            transactions.ForEach(t =>
            {
                var clients = ConnectionMappingSingleton.GetConnections(t.Product.Name);
                var signalRId = clients.Where(f => f.UserId == t.User.Id.ToString()).Select(f => f.SignalRId).ToList();
                //_accountService.GetSignalRIdByUser(t.User.Id);
                Debug.WriteLine("SignalRId :" + signalRId);
                Debug.WriteLine("Pnl :" + t.PnL);
                if (signalRId.Count() > 0)
                {
                    Debug.WriteLine("PnL send to :" + signalRId);
                    Clients.Clients(signalRId).onUpdatePnL(t);
                }
            });
        }

        public bool RegisterServer(string name)
        {
            if(!signalRidServer.ContainsKey(name))
            {
                signalRidServer.Add(name, Context.ConnectionId);
            }
            return true;
        }

        public bool UnRegisterServer(string name)
        {
            if (signalRidServer.ContainsKey(name))
            {
                signalRidServer.Remove(name);
            }
            return true;
        }


        public void GetHistoricalDataFromServer(string signalrId, string product)
        {
            if (signalRidServer.Count == 0)
                return;
            if (!signalRidServer.ContainsKey("server1"))
                return;
            else
                Clients.Client(signalRidServer["server1"]).getHistoricalData(signalrId, product);
        }

    }
}