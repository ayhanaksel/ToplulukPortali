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
    public class InterestController : ApiController
    {

        [HttpPost]
        public int saveNewInterest(Interest interest)
        {
            return interest.save();                             //Yeni ilgi alanı ekleyen api.
        }

        public string getAllInterest()
        {                                                           //Tüm ilgi alanlarını getiren api.

            return JsonConvert.SerializeObject(new Interest().getInterests(""));

        }

        [HttpPost]
        public string getInterest(Interest i)                                       //Tek bir ilgi alanı getiren api.
        {
            Interest interest = new Interest()
            {
                ID = i.ID
            };
            interest.getInterest();

            return JsonConvert.SerializeObject(new Interest().getInterests(""));
        }

    }
}
