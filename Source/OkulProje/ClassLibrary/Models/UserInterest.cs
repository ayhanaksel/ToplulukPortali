using ClassLibrary.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Models
{
    public class UserInterest
    {

        public int ID { get; set; }
        public User User{ get; set; }
        public Interest Interest{ get; set; }
        public Boolean IsDeleted{ get; set; }

        public int Save()
        {

            this.ID = DAL.insertSql("insert into UserInterest(UserID,InterestID) values(@UserID,@InterestID)", new List<MySqlParameter>() {

                new MySqlParameter("@UserID",this.User.ID),
                new MySqlParameter("@InterestID",this.Interest.ID)

            });

            return this.ID;
        }


    }
}
