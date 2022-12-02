using ChatDemo.Controllers;
using ChatDemo.DAL;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatDemo.SignalR
{
    public class ChatHub : Hub
    {
        private IDbHelper _db;

        public ChatHub(IDbHelper db)
        {
            _db = db;


        }

        public override async Task OnConnectedAsync() 
        {
            var test = Context.ConnectionId;
           
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(Message message)
        {
            int check=await _db.Add<ChatLog>("insert into chatlog (room_id,user_id,content) values(@room_id,@user_id,@content)", new ChatLog { room_id=1,user_id=message.guid,content=message.message});

            if (check > 0)
            {
                await Clients.All.SendAsync("ReceiveMessage", message);
            }
            else { 
                //todo 通知发送失败
            }
        }

        public Task SendMessageToCaller(string user, string message)
        {
            return Clients.Caller.SendAsync("ReceiveMessage", user, message);
        }

        public Task SendMessageToGroup(string user, string message)
        {
            return Clients.Group("SignalR Users").SendAsync("ReceiveMessage", user, message);
        }

        //private async void init() {
        //   var users= await _db.Query<RoomUser>("select roomid,guid from room_user a inner join user b on a.uid=b.uid");
        //    foreach (RoomUser u in users) {
        //       await Groups.AddToGroupAsync(u.guid,u.roomid.ToString());
        //    }
        //}
    }

    public class Message { 
        public string guid { get; set; }
        public string message { get; set; }
        public string avatar { get { return $@"/image/{guid}.jpg"; } }
    }


    public class RoomUser
    {
        public int roomid { get; set; }
        public string guid { get; set; }
    }
}
