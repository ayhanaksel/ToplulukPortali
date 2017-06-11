﻿using ClassLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace TubitetWebAPI.Controllers
{
    public class FacultyController : ApiController
    {
        
        [HttpPost]
         public int SaveNewFaculty(Faculty faculty)
        {
            var model = new Faculty();
            if(faculty != null) { 
            model = faculty;
                model.save();
                return 0;
            }
            return 0;
        }

        public string getAllFaculties()
        {
            return JsonConvert.SerializeObject(new Faculty().getFaculties(""));
        }

        [HttpPost]
        public string getFaculty(Faculty f) //  SOOOOOORRRRR
        {

            Faculty faculty = new Faculty()
            {
                ID = f.ID
            };
            faculty.getFaculty();
 

            return JsonConvert.SerializeObject(faculty);

        }

    }
}
