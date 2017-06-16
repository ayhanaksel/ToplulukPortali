using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClassLibrary.Models;
using Newtonsoft.Json;

namespace TubitetWebAPI.Controllers
{
    public class UserTitleController : ApiController
    {

        [HttpPost]
        public int SaveNewUserTitle(UserTitle userTitle)
        {
            return userTitle.save();                                             //Yeni kullanıcı ünvanı ekleyen api.
        }

        public string getAllUserTitle()
        {
            return JsonConvert.SerializeObject(new UserTitle().getUserTitles(""));                          //Tüm ünvanları getiren api.
        }

        [HttpPost]
        public string getUserTitle(UserTitle u)
        {

            UserTitle usertitle = new UserTitle()                               //Tek ünvan bilgilerini kodlar.
            {
                ID = u.ID
            };
            usertitle.getUserTitle();


            return JsonConvert.SerializeObject(usertitle);

        }


    }
}
