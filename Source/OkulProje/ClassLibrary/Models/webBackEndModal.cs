using ClassLibrary.Infrastructure;
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
                //new MySqlParameter("@key",this.key),
                new MySqlParameter("@value",this.value)
            });

            return this.id;
        }

        public void getValue()
        {
            DataTable data = DAL.readData("select @key from WebBackEnd where ID = 1", new MySqlParameter("@key", this.key));
            this.value = data.Rows[0][this.key].ToString();
        }

    }
}
