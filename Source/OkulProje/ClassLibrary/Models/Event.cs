using ClassLibrary.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibrary.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string ActivityName { get; set; }
        public EventType ActivityType { get; set; }
        public DateTime ActivityDate { get; set; }
        public Saloon SaloonID { get; set; }
        public int GuessLimit { get; set; }
        public Speaker SpeakersID { get; set; }
        public Worker WorkersID { get; set; }
        public string ActivityPhoto { get; set; }
        public Boolean IsDeleted { get; set; }

        public int save()
        {
            if (this.ID == 0)
            {
                this.ID = DAL.insertSql("insert into Activities(ActivityName,ActivityType,ActivityDate,SaloonID,GuessLimit,SpeakersID,WorkersID,ActivityPhoto) values (@ActivityName,@ActivityType,@ActivityDate,@SaloonID,@GuessLimit,@SpeakersID,@WorkersID,@ActivityPhoto)", new List<MySqlParameter>()
                {
                    new MySqlParameter("@ActivityName",this.ActivityName),
                    new MySqlParameter("@ActivityType",this.ActivityType.ID),
                    new MySqlParameter("@ActivityDate",this.ActivityDate),
                    new MySqlParameter("@SaloonID",this.SaloonID.ID),
                    new MySqlParameter("@GuessLimit",this.GuessLimit),
                    new MySqlParameter("@SpeakersID",this.SpeakersID.ID),
                    new MySqlParameter("@WorkersID",this.WorkersID.ID),
                    new MySqlParameter("@ActivityPhoto",this.ActivityPhoto)
                });
            }
            else
            {
                DAL.insertSql("update Activities set ActivityName = @ActivityName,ActivityType = @ActivityType,ActivityDate = @ActivityDate,SaloonID = @SaloonID,GuessLimit = @GuessLimit,SpeakersID = @SpeakersID,WorkersID = @WorkersID,ActivityPhoto = @ActivityPhoto where ID = @ID",
                     new List<MySqlParameter>()
                     {
                        new MySqlParameter("@ActivityName",this.ActivityName),
                        new MySqlParameter("@ActivityType",this.ActivityType.ID),
                        new MySqlParameter("@ActivityDate",this.ActivityDate),
                        new MySqlParameter("@SaloonID",this.SaloonID.ID),
                        new MySqlParameter("@GuessLimit",this.GuessLimit),
                        new MySqlParameter("@SpeakersID",this.SpeakersID.ID),
                        new MySqlParameter("@WorkersID",this.WorkersID.ID),
                        new MySqlParameter("@ActivityPhoto",this.ActivityPhoto)
                     });
            }
            return this.ID;
        }

        public void Delete()
        {
            DAL.insertSql("update Activities set IsDeleted=1 Where ID=@ID", new MySqlParameter("@ID", this.ID));
        }

        public List<Event> getActivities(string filter)
        {

            List<Event> result = new List<Event>();

            DataTable data = DAL.readData("select * from Activities where IsDeleted=0 and ActivityName Like @filter", new MySqlParameter("@filter", '%' + filter + '%'));

            foreach (DataRow dr in data.Rows)
            {

                result.Add(
                    new Event()
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        ActivityName = dr["ActivityName"].ToString(),
                        ActivityType = new EventType() { ID = Convert.ToInt32(dr["ActivityType"].ToString()) },
                        ActivityDate = Convert.ToDateTime(dr["ActivityDate"].ToString()),
                        SaloonID = new Saloon() { ID = Convert.ToInt32(dr["SaloonID"].ToString()) },
                        GuessLimit = Convert.ToInt32(dr["ActivityName"].ToString()),
                        SpeakersID = new Speaker() { ID = Convert.ToInt32(dr["SpeakersID"].ToString()) },
                        WorkersID = new Worker() { ID = Convert.ToInt32(dr["WorkersID"].ToString()) },
                        ActivityPhoto = dr["ActivityPhoto"].ToString(),
                    }
               );
            }
            return result;
        }

        public void getWorker()
        {
            DataTable data = DAL.readData("select * from Activities where ID=@ID", new MySqlParameter("@ID", this.ID));
            this.ActivityName = data.Rows[0]["ActivityName"].ToString();
            this.ActivityType.ID = Convert.ToInt32(data.Rows[0]["ActivityType"].ToString());
            this.ActivityDate = Convert.ToDateTime(data.Rows[0]["ActivityDate"].ToString());
            this.SaloonID.ID = Convert.ToInt32(data.Rows[0]["SaloonID"].ToString());
            this.GuessLimit = Convert.ToInt32(data.Rows[0]["GuessLimit"].ToString());
            this.SpeakersID.ID = Convert.ToInt32(data.Rows[0]["SpeakersID"].ToString());
            this.WorkersID.ID = Convert.ToInt32(data.Rows[0]["WorkersID"].ToString());
            this.ActivityPhoto = data.Rows[0]["ActivityPhoto"].ToString();
            this.IsDeleted = Convert.ToBoolean(data.Rows[0]["IsDeleted"].ToString());
        }
    }
}
