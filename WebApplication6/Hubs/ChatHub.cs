using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using _chat.Models;
using WebApplication6;

namespace _chat.hub
{
    public class ChatHub : Hub
    {
        userssDataContext db = new userssDataContext();
        
        Table table = new Table();
        /* public void sendMessage(string msg)
         {
             Clients.All.addMessage(msg);
         }*/
        static List<user> Users = new List<user>();

        // Отправка сообщений
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

        // Подключение нового пользователя
        public void Connect(string userName,string password)
        {
            if (userName != "" && password != "")
            {
                var id = Context.ConnectionId;
                table.Login = userName;
                table.Password = password;
                db.Table.InsertOnSubmit(table);
                db.SubmitChanges();
                if (!Users.Any(x => x.ConnectionId == id))
                {
                    Users.Add(new user { ConnectionId = id, Name = userName, Password = password });

                    // Посылаем сообщение текущему пользователю
                    Clients.Caller.onConnected(id, userName, Users);

                    // Посылаем сообщение всем пользователям, кроме текущего
                    Clients.AllExcept(id).onNewUserConnected(id, userName);
                }
            }
         
        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}