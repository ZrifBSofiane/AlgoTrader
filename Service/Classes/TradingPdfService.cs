using Reposiroty.Models;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service.Classes
{
    public static class TradingPdfService
    {
        private static readonly ITransactionService _transactionService = new TransactionService();
        private static readonly IUserService _userService = new UserService();
        private static readonly IAccountService _accountService = new AccountService();


        public static void BuildTradingPdfForAll()
        {
            var now = DateTime.Now;
            var users = _userService.Get();
            try
            {
                foreach (var user in users)
                {
                    var transactionsOfDay = _transactionService.GetAllByUser(user.Id);
                    var account = _accountService.Get(user.Email);
                    GenerateTradingPdf(transactionsOfDay, account);
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void BuildTradingPdf(string userId, ref byte[] result)
        {
            var now = DateTime.Now;
            try
            {
                var user = _userService.Get(userId, false);
                var transactionsOfDay = _transactionService.GetAllByUser(new Guid(userId));
                var account = _accountService.Get(user.Email);
                result = GenerateTradingPdf(transactionsOfDay, account);

            }
            catch (Exception e)
            {

            }
        }

        public static byte[] GenerateTradingPdf(List<TransactionDTO> transactions, AccountDTO user)
        {
            var i = 0;
            var html = "<!DOCTYPE html>" +
                "<html lang='en'>" +
                " <head>" +
                "<title>Restaurant Mayda</title>" +
                "<meta charset='utf-8'>" +
                "<meta name='viewport' content='width=device-width, initial-scale=1, shrink-to-fit=no'>" +
                "<link href='https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css' rel='stylesheet' integrity='sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO' crossorigin='anonymous'>" +
                "</head>" +
                "<body data-spy='scroll' data-target='#site-navbar' data-offset='200'>" +

                " <div class='col-md-12 text-center'>" +
                "<h1>FINANCIAL IT - PLATFORM</h1>" +
                "</div>                                                   " +
                "<div class='container-fluid'>                            " +
                "<div class='text-center'>                           " +
                "<div class'float-left'>                        " +
                $"<h6> N° account : {user.Id}</h6>            " +
                "</div>                                           " +
                "<div class='float-right'>                        " +
                $"<h6> Name :{user.User.Name}</h6>     " +
                "</div>                                           " +
                "</div>                                               " +
                "<br>                                                 " +
                "<div class='text-center'>                            " +
                "<div class='float-left'>                        " +
                $"<h6>Date : {DateTime.Now}</h6>           " +
                "</div>                                           " +
                " <div class='float-right'>                        " +
                "<h6>Daily Confirmation</h6>                 " +
                "</div>                                           " +
                "</div>                                               " +
                "<br>                                                 " +
                "<br>                                                 " +
                "<div class='row'>                                    " +
                "<div class='float-left'>                         " +
                " Closed Transactions :                        " +
                " </div>                                           " +
                "</div>                                               " +
                "<div class='row'>                                    " +
                " <table class='table'>                            " +
                " <thead>                                    " +
                " <tr>                                   " +
                " <th width='4%;' scope='col'>#</th> " +
                " <th scope='col'>Open Time</th>     " +
                " <th scope='col'>Close Time</th>          " +
                " <th scope='col'>Way</th>          " +
                " <th scope='col'>Product</th>         " +
                " <th scope='col'>Start price</th>           " +
                " <th scope='col'>End Price</th>           " +
                " <th scope='col'>Quantity</th>    " +
                " <th scope='col'>PnL</th>         " +
                " <th scope='col'>Commission</th>    " +
                " <th scope='col'>PnL</th>           " +
                " </tr>                                  " +
                "</thead>                                   " +
                " <tbody>                                      ";
            if (transactions.Count == 0)
            {
                html += "<tr><th colspan=11>You don't have any trade this day</th></tr>";
            }
            foreach (var tr in transactions)
            {
                html += " <tr>                                   " +
                $"<th>{tr.Id}</th>                         " +
                $"<td>{tr.StartDate}</td>       " +
                $"<td>{tr.EndDate}</td>                       " +
                $"<td>{tr.Way}</td>                    " +
                $"<td>{tr.Product.Name}</td>                    " +
                $"<td>{tr.StartPrice}</td>                         " +
                $"<td>{tr.EndPrice}</td>                         " +
                $"<td>{tr.Quantity}</td>       " +
                $"<td>{tr.PnL}</td>                    " +
                $"<td>--</td>                      " +
                $"<td>{tr.PnL}</td>                     " +
                "</tr>   ";
            }
            html += "</tbody> " +
            " </table>" +
            "</div> " +
            "<br>" +
            "<br>" +
            "<div class='row'>" +
            " <div class='float-left'>" +
            " <h6>A/C Summary :</h6>" +
            "</div>" +
            "<table class='table'>" +
            " <tbody>" +
            " <tr>" +
            " <td>Closed Trade P/L : </td>" +
            $" <td>{transactions.Where(t => t.Statuts == Enums.StatusDeal.Closed.ToString()).Sum(t => t.PnL)}</td>" +
            "  </tr>" +
            "  <tr>" +
            "  <td>Balance : </td>" +
            $" <td>{user.Amount}</td>" +
            " </tr>" +
            " <tr>" +
            "<td>Available margin : </td>" +
            " <td>1548</td>" +
            " </tr>  " +
            " </tbody>" +
            " </table>" +
            " </div>" +


            "<div class='row'>" +
            " Please kindly inform us within 24 hours if the information on this statement is not accurate, otherwise this statement will be considered accepted by you. <br>" +
            " Financial IT - Platform" +

            " </div>" +
            " </div>" +
            " <!-- END Modal -->" +
            "<script src='https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.js'></script>" +
            " <script src='https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js' integrity='sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy' crossorigin='anonymous'></script>" +
            " </body>" +
            "</html>";

            IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();

            Renderer.PrintOptions.DPI = 300;
            var byteTab = Renderer.RenderHtmlAsPdf(html).Stream.ToArray();

            byte[] bytes = byteTab;
            string contentType = "application/unknown";
            var reponse = new HttpResponseMessage(HttpStatusCode.OK);
            reponse.Content = new StreamContent(new MemoryStream(bytes));
            reponse.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            reponse.Content.Headers.ContentDisposition.FileName = $"Statement {user.User.Name}";
            reponse.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return bytes;

            //TODO : BELOW IS TO SAVE THE DOCUMENT INTO THE SERVER
            //return FileTransferService.UploadFile(new Guid(user.Id), byteTab, $"Trading_{DateTime.Now.ToShortDateString().Replace("/", "_")}.pdf");
        }
    }
}
