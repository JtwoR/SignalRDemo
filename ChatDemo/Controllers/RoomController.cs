using ChatDemo.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatDemo.Controllers
{
    [Route("api/[controller]")]
    public class RoomController : Controller
    {
        private IDbHelper _db;

        public RoomController(IDbHelper db) {
            _db = db;
        }

        [HttpGet("rooms")]
        public async Task<object> Rooms()
        {
            return (await _db.Query<Room>("select * from room")).ToList();
        }
    }

    public class Room
    {
        public long id { get; set; }
        public string name { get; set; }
        public DateTime create_time { get; set; }
    }
}
