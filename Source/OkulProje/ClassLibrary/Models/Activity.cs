using ClassLibrary.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ClassLibrary.Models
{
    public class Activity
    {
        public int ID { get; set; }
        public string ActivityName { get; set; }
        public string ActivityType { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Saloon { get; set; }
        public int GuessLimit { get; set; }
        public string ActivityPhoto { get; set; }
        public Boolean IsDeleted { get; set; }

        public int save()
        {
            if (this.ID == 0)
            {
                this.ID = DAL.insertSql("insert into Activities(ActivityName,ActivityType,ActivityDate,Saloon,GuessLimit,ActivityPhoto) values (@ActivityName,@ActivityType,@ActivityDate,@Saloon,@GuessLimit,@ActivityPhoto)", new List<MySqlParameter>()
                {                                                                                           //Yeni etkinlik eklemek için kullanılır.
                    new MySqlParameter("@ActivityName",this.ActivityName),
                    new MySqlParameter("@ActivityType",this.ActivityType),
                    new MySqlParameter("@ActivityDate",this.ActivityDate),
                    new MySqlParameter("@Saloon",this.Saloon),
                    new MySqlParameter("@GuessLimit",this.GuessLimit),
                    new MySqlParameter("@ActivityPhoto",this.ActivityPhoto)
                });
            }
            else
            {                                                                                               //Var olan etkinliği güncellemek için kullanılır.
                DAL.insertSql("update Activities set ActivityName = @ActivityName,ActivityType = @ActivityType,ActivityDate = @ActivityDate,Saloon = @Saloon,GuessLimit = @GuessLimit,ActivityPhoto = @ActivityPhoto where ID = @ID",
                     new List<MySqlParameter>()
                     {
                        new MySqlParameter("@ActivityName",this.ActivityName),
                        new MySqlParameter("@ActivityType",this.ActivityType),
                        new MySqlParameter("@ActivityDate",this.ActivityDate),
                        new MySqlParameter("@Saloon",this.Saloon),
                        new MySqlParameter("@GuessLimit",this.GuessLimit),
                        new MySqlParameter("@ActivityPhoto",this.ActivityPhoto),
                        new MySqlParameter("@ID",this.ID)
                     });
            }
            return this.ID;
        }

        public void Delete()
        {                                                                                   //Var olan etkinliği silmek için kullanılır.
            DAL.insertSql("update Activities set IsDeleted=1 Where ID=@ID", new MySqlParameter("@ID", this.ID));
        }

        public List<Activity> getActivities(string filter)
        {
                                                                                                    //Tüm etkinlikleri listelememizi sağlar.
            List<Activity> result = new List<Activity>();

            DataTable data = DAL.readData("select * from Activities where IsDeleted=0 and ActivityName Like @filter", new MySqlParameter("@filter", '%' + filter + '%'));

            foreach (DataRow dr in data.Rows)
            {

                result.Add(
                    new Activity()
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        ActivityName = dr["ActivityName"].ToString(),
                        ActivityType = dr["ActivityType"].ToString(),
                        ActivityDate = Convert.ToDateTime(dr["ActivityDate"].ToString()),
                        Saloon = dr["Saloon"].ToString(),
                        GuessLimit = Convert.ToInt32(dr["GuessLimit"].ToString()),
                        ActivityPhoto = dr["ActivityPhoto"].ToString(),
                    }
               );
            }
            return result;
        }

        public void getActivity()
        {                                                                               //Tek bir etkinliği getirmemizi sağlar.
            DataTable data = DAL.readData("select * from Activities where ID=@ID", new MySqlParameter("@ID", this.ID));
            this.ActivityName = data.Rows[0]["ActivityName"].ToString();
            this.ActivityType = data.Rows[0]["ActivityType"].ToString();
            this.ActivityDate = Convert.ToDateTime(data.Rows[0]["ActivityDate"].ToString());
            this.Saloon = data.Rows[0]["Saloon"].ToString();
            this.GuessLimit = Convert.ToInt32(data.Rows[0]["GuessLimit"].ToString());
            this.ActivityPhoto = data.Rows[0]["ActivityPhoto"].ToString();
            this.IsDeleted = Convert.ToBoolean(data.Rows[0]["IsDeleted"].ToString());
        }
    }
}
