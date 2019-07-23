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
    public class ContextHub : Hub
    {

        private readonly IAccountService _accountService = new AccountService();

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
            }
            return base.OnDisconnected(stopCalled);
        }


        public void SendNewConnectionToAll(string user)
        {
            Clients.All.receiveNewConnectionToAll(user);
        }


        public void Hello()
        {
            Clients.All.hello();
        }
    }
}