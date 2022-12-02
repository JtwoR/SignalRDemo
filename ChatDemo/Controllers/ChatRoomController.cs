using ChatDemo.DAL;
using ChatDemo.SignalR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatDemo.Controllers
{
    [Route("api/[controller]")]
    public class ChatRoomController : Controller
    {
        private IDbHelper _db;

        public ChatRoomController(IDbHelper db)
        {
            _db = db;
        }

        //[HttpGet("message")]
        //public async Task<object> Message(long? id=0)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(" select * from room_message ");
        //    if (id > 0) sb.Append($@" where id<{id}");
        //    sb.Append(" order by id desc limit 10 ");

        //    return (await _db.Query<ChatRoom>("select * from room_message order by id desc limit 10")).ToList();
        //}


        [HttpGet("message")]
        public async Task<object> Message()
        {
            var data= (await _db.Query<ChatLog>("select * from chatlog where room_id=@room_id order by id desc limit 10", new ChatLog { room_id = 1 }))?.Select(s=>new Message { 
                guid=s.user_id,
                message=s.content
            }).ToList();

            return new ResponseData<List<Message>>(data??new List<Message>());
        }

    }

    //public class ChatRoom { 
    //    public long id { get; set; }
    //    public long uid { get; set; }
    //    public string content { get; set; }
    //    public DateTime create_time { get; set; }
    //}

    public class ChatLog { 
        public int id { get; set; }
        public int room_id { get; set; }
        public string user_id { get; set; }
        public string content { get; set; }
    }
}
