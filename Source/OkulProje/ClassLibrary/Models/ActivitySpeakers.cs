using ClassLibrary.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibrary.Models
{
    public class ActivitySpeakers
    {
        public int ID { get; set; }
        public Activity Activity { get; set; }
        public Speaker Speaker { get; set; }
        public Boolean IsDeleted { get; set; }

        public int save()                                          //Yeni konuşmacı eklememizi sağlayan kodlar.
        {
            if (this.ID == 0)
            {
                this.ID = DAL.insertSql("insert into ActivitySpeakers(Activity,Speaker) values (@ActivityID,@SpeakerID)", new List<MySqlParameter>()
                    {
                        new MySqlParameter("@ActivityID",this.Activity.ID),
                        new MySqlParameter("@SpeakerID",this.Speaker.ID)
                    });
            }
            else
            {                                                           //Var olan konuşmacıyı güncellememizi sağlayan kodlar.
                DAL.insertSql("update ActivitySpeakers set ActivityID = @ActivityID, SpeakerID = @SpeakerID where ID = @ID",
                    new List<MySqlParameter>()
                    {
                        new MySqlParameter("@ActivityID",this.Activity.ID),
                        new MySqlParameter("@SpeakerID",this.Speaker.ID)
                    }
                    );
            }
            return this.ID;
        }

        public void Delete()
        {                                                                   //Var olan konuşmacıyı silmemizi sağlar.
            DAL.insertSql("update ActivitySpeakers set IsDeleted=1 Where ID=@ID", new MySqlParameter("@ID", this.ID));
        }

        public List<ActivitySpeakers> getActivitySpeakers(string filter)
        {
                                                                            // Konuşmacıları listelememizi sağlayan kodlar.
            List<ActivitySpeakers> result = new List<ActivitySpeakers>();

            DataTable data = DAL.readData("select * from ActivitySpeakers where IsDeleted=0 and ActivityID Like @filter", new MySqlParameter("@filter", '%' + filter + '%'));



            foreach (DataRow dr in data.Rows)
            {

                result.Add(
                    new ActivitySpeakers
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        Activity = new Activity() { ID = Convert.ToInt32(dr["ActivityID"].ToString()) },
                        Speaker = new Speaker() { ID = Convert.ToInt32(dr["SpeakerID"].ToString()) },
                    }
               );

            }
            return result;
        }

        public void getActivitySpeaker()
        {                                                                                        //Tek bir kullanıcıyı listelememiz için kullanılan kodlar.
            DataTable data = DAL.readData("select * from Activities where ID=@ID", new MySqlParameter("@ID", this.ID));
            this.Activity.ID = Convert.ToInt32(data.Rows[0]["ActivityID"].ToString());
            this.Speaker.ID = Convert.ToInt32(data.Rows[0]["SpeakerID"].ToString());
            this.IsDeleted = Convert.ToBoolean(data.Rows[0]["IsDeleted"].ToString());
        }
    }
}
