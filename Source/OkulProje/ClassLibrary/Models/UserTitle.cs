using ClassLibrary.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibrary.Models
{
    public class UserTitle
    {

        public int ID { get; set; }
        public string UserTitleName { get; set; }
        public Boolean IsDeleted { get; set; }

        public int save()
        {
            if (this.ID == 0)
            {                                                           //Kullanıcı ünvanı ekleme kodları.
                this.ID = DAL.insertSql("insert into UserTitle(Title) values (@UserTitleName)", new MySqlParameter("@UserTitleName", this.UserTitleName));
            }
            else
            {                                                           //Kullanıcı ünvanı güncelleme kodarı.
                this.ID = DAL.insertSql("update UserTitle set Title = @UserTitleName where ID=@ID",
                    new List<MySqlParameter>()
                    {
                         new MySqlParameter("@ID",this.ID),
                         new MySqlParameter("@UserTitleName",this.UserTitleName)
                    });

            }
            return this.ID;
        }

        public void Delete()
        {                                                                   //Kullanıcı ünvanı silme kodları.
            DAL.insertSql("update UserTitle set IsDeleted = 1 where ID=@ID", new MySqlParameter("@ID", this.ID));
        }



        public List<UserTitle> getUserTitles(string filter)
        {                                                                       //Kullanıcı ünvanları listeleme kodları.

            List<UserTitle> UserTitles= new List<UserTitle>();

            DataTable data = DAL.readData("select * from UserTitle where IsDeleted = 0 and Title Like @filter", new MySqlParameter("@filter", '%' + filter + '%'));

            foreach (DataRow row in data.Rows)
            {

                UserTitles.Add(
                    new UserTitle()
                    {
                        ID = Convert.ToInt32(row["ID"].ToString()),
                        UserTitleName = row["Title"].ToString()
                    }
                );

            }
            return UserTitles;

        }

        public void getUserTitle()
        {                                                                           //Tek bir ünvanın bilgilerinin getirilmesi.

            DataTable data = DAL.readData("select * from UserTitle where ID=@ID", new MySqlParameter("@ID", this.ID));
            this.UserTitleName = data.Rows[0]["Title"].ToString();
            this.IsDeleted = Convert.ToBoolean(data.Rows[0]["IsDeleted"]);


        }
    }
}
