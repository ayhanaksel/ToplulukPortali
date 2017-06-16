using ClassLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace TubitetWebAPI.Controllers
{
    public class UserController : ApiController
    {

        [HttpPost]
        public int saveNewUser(User user)
        {
                                                                             //Yeni kullanıcı ekleyen api.
            int eklenenuser = user.save();

            return eklenenuser;

        }

        //Kullanıcı varsa geriye json formatta nesne donuyor. yoksa nesne boş olarak dönüyor.
        [HttpPost]
        public string LoginControl(User user)
        {
            User u = new User();
            u = user.LoginControl();                                            //Login kontrolu yapan api.
            return JsonConvert.SerializeObject(u);
        }


        [HttpPost]
        public int ChangedState(User user) //user = activationcode + userıd + istene userstate
        {
            return user.ChangedState();                                         //Kullanıcının durumunu değiştiren api.
        }

        [HttpPost]
        public int Delete(User user)
        {    
            return user.Delete();                                               //Kullanıcı silen api.
        }

    }
}
