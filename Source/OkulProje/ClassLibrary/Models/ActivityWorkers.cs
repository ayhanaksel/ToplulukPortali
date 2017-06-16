using ClassLibrary.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ClassLibrary.Models
{
    class ActivityWorkers
    {
        public int ID { get; set; }
        public Activity Activity { get; set; }
        public Worker Worker { get; set; }
        public Boolean IsDeleted { get; set; }

        public int save()                                           //Yeni görevli eklemek için kullanılan kodlar.
        {
            if (this.ID == 0)
            {
                this.ID = DAL.insertSql("insert into ActivityWorkers(Activity,Worker) values (@ActivityID,@WorkerID)", new List<MySqlParameter>()
                    {
                        new MySqlParameter("@ActivityID",this.Activity.ID),
                        new MySqlParameter("@WorkerID",this.Worker.ID)
                    });
            }
            else
            {                                                               //Var olan görevliyi güncelleme kodları.
                DAL.insertSql("update ActivityWorkers set ActivityID = @ActivityID, WorkerID = @WorkerID where ID = @ID",
                    new List<MySqlParameter>()
                    {
                        new MySqlParameter("@ActivityID",this.Activity.ID),
                        new MySqlParameter("@WorkerID",this.Worker.ID)
                    }
                    );
            }
            return this.ID;
        }

        public void Delete()                                        //Var olan görevliyi silmemizi sağlayan kodlar.
        {
            DAL.insertSql("update ActivityWorkers set IsDeleted=1 Where ID=@ID", new MySqlParameter("@ID", this.ID));
        }

        public List<ActivityWorkers> getActivityWorkers(string filter)
        {

            List<ActivityWorkers> result = new List<ActivityWorkers>();                     //Görevlileri listelememizi sağlayan kodlar.

            DataTable data = DAL.readData("select * from ActivityWorkers where IsDeleted=0 and ActivityID Like @filter", new MySqlParameter("@filter", '%' + filter + '%'));



            foreach (DataRow dr in data.Rows)
            {

                result.Add(
                    new ActivityWorkers
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        Activity = new Activity() { ID = Convert.ToInt32(dr["ActivityID"].ToString()) },
                        Worker = new Worker() { ID = Convert.ToInt32(dr["WorkerID"].ToString()) },
                    }
               );

            }
            return result;
        }

        public void getActivityWorker()                                                                     //Tek bir görevliyi listelememizi sağlayan kodlar.
        {
            DataTable data = DAL.readData("select * from Activities where ID=@ID", new MySqlParameter("@ID", this.ID));
            this.Activity.ID = Convert.ToInt32(data.Rows[0]["ActivityID"].ToString());
            this.Worker.ID = Convert.ToInt32(data.Rows[0]["WorkerID"].ToString());
            this.IsDeleted = Convert.ToBoolean(data.Rows[0]["IsDeleted"].ToString());
        }
    }
}
