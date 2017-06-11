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
            return userTitle.save();
        }

        public string getAllUserTitle()
        {
            return JsonConvert.SerializeObject(new UserTitle().getUserTitles(""));
        }

        [HttpPost]
        public string getUserTitle(UserTitle u) //  SOOOOOORRRRR
        {

            UserTitle usertitle = new UserTitle()
            {
                ID = u.ID
            };
            usertitle.getUserTitle();


            return JsonConvert.SerializeObject(usertitle);

        }


    }
}
