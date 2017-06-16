using ClassLibrary.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibrary.Models
{
    public class Interest
    {

        public int ID { get; set; }
        public string InterestName { get; set; }
        public Boolean IsDeleted { get; set; }

        public int save()
        {
            if (this.ID == 0)                                                        //ilgi alanları ekleme ve güncelleme kodları.
            {                             
                this.ID = DAL.insertSql("insert into Interest(InterestName) values (@InterestName)", new MySqlParameter("@InterestName", this.InterestName));
            }
            else
            {
                this.ID = DAL.insertSql("update Interest set InterestName = @InterestName where ID=@ID",
                    new List<MySqlParameter>()
                    {
                         new MySqlParameter("@ID",this.ID),
                         new MySqlParameter("@InterestName",this.InterestName)
                    });
                   
            }
            return this.ID;
        }

        public void Delete()                                                    //ilgi alanları silme kodları.
        {
            DAL.insertSql("update Interest set IsDeleted = 1 where ID=@ID", new MySqlParameter("@ID", this.ID));
        }

        public List<Interest> getInterests(string filter)
        {                                                                          //İlgi alanları listeleme kodları.

            List<Interest> Interests = new List<Interest>();

            DataTable data = DAL.readData("select * from Interest where IsDeleted = 0 and InterestName Like @filter", new MySqlParameter("@filter", '%' + filter + '%'));
            
            foreach(DataRow row in data.Rows)
            {

                Interests.Add(
                    new Interest()
                    {
                        ID = Convert.ToInt32(row["ID"].ToString()),
                        InterestName = row["InterestName"].ToString()
                    }
                );

            }
            return Interests;

        }

        public void getInterest()
        {                                                                           //Tek bir ilgi alanı bilgilerini getirme kodları

            DataTable data = DAL.readData("select * from Interest where ID=@ID", new MySqlParameter("@ID", this.ID));
            this.InterestName = data.Rows[0]["InterestName"].ToString();
            this.IsDeleted = Convert.ToBoolean(data.Rows[0]["IsDeleted"]);


        }

    }
}
