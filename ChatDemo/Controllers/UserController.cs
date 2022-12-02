using ChatDemo.DAL;
using JR.Tool;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatDemo.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private IDbHelper _db;

        public UserController(IDbHelper db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("avatar")]
        public async Task<ResponseData<string>> Avatar(string guid)
        {
            return new ResponseData<string>(AvatorHelper.GetAvatar(guid));
        }


        [HttpGet("register")]
        public async Task<ResponseData<bool>> Register(string guid)
        {

            bool check = false;
            if (!(await _db.Query<User>("select id from user where id=@id", new User { id = guid })).Any())
            {
                check = await _db.Add<User>("insert into user (id,name,avatar) values(@id,@name,@avatar)", new User { id = guid, name = string.Empty, avatar = $@"{guid}.jpg" }) > 0 ? true : false;
            }
            else {
                check = true;
            }
            return new ResponseData<bool>(check);
        }
        
    }

    public class User
    { 
        public string id { get; set;}
        public string name { get; set; }
        public string avatar { get; set; }
    }
    //public class User
    //{
    //    public long id { get; set; }
    //    public long guid { get; set; }
    //    public DateTime create_time { get; set; }
    //    public DateTime login_time { get; set; }
    //}

    public class ResponseData<T>
    {
        public string msg { get; set; }
        public int code { get; set; }
        public T data { get; set; }

        public ResponseData(T d, string m = null, int c = 200)
        {
            data = d;
            msg = m ?? "成功";
            code = c;
        }
    }
}
