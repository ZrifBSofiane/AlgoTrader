using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Service.Classes;
using Service.Interfaces;

namespace Web.Realtime
{
    public class PlatformHub : Hub
    {
        private readonly IAccountService _accountService = new AccountService();


        #region De/Re/Connected
        public override Task OnConnected()
        {
            var userId = Context.User.Identity.GetUserId();
            if (!string.IsNullOrWhiteSpace(userId))
            {
                _accountService.UpdateSignalRId(new Guid(userId), Context.ConnectionId);
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
            ConnectionMappingSingleton.Add(ticker, Context.ConnectionId);
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
            var clients = ConnectionMappingSingleton.GetConnections(ticker);
            if(clients.Count() > 0)
            {
                Clients.Clients(clients.ToList()).onReceivedQuote(ticker, bid, ask);
            }
            
            
        }
    }
}