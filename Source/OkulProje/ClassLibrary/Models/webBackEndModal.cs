﻿using ClassLibrary.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibrary.Models
{
    public class webBackEndModal
    {

        public int id { get; set; }
        public string  key { get; set; }
        public string  value { get; set; }

        public int save()
        {
            DAL.insertSql("update WebBackEnd set " + this.key + " = @value where id = 1", new List<MySqlParameter>()
            {
                //new MySqlParameter("@key",this.key),                          //web yonetimin tek bir bölümün değiştirilmesi kodu.
                new MySqlParameter("@value",this.value)
            });

            return this.id;
        }

        // misyon için = Mission
        // vizyon için = Vision
        // iletişim bilgileri için = Contact
        // hakkımda için  = AboutMe 
        // parametre olarak gönderilecek

        public void getValue()
        {                                                                           //tek bir bölümün verilerini getirme kodu.
            DataTable data = DAL.readData("select * from WebBackEnd where ID = 1", new MySqlParameter("@key", this.key));
            this.value = data.Rows[0][this.key].ToString();
        }

    }
}
