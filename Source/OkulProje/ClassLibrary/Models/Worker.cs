using ClassLibrary.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ClassLibrary.Models
{
    public class Worker
    {
        public int ID { get; set; }
        public User UserID { get; set; }
        public string WorkerMission { get; set; }
        public Boolean IsDeleted { get; set; }

        public int save()
        {
            if (this.ID == 0)
            {
                this.ID = DAL.insertSql("insert into Worker(UserID,WorkerMission) values (@UserID,@WorkerMission)", new List<MySqlParameter>() {

                        new MySqlParameter("@UserID",this.UserID.ID),
                        new MySqlParameter("@WorkerMission",this.WorkerMission)
                         });
            }
            else
            {
                DAL.insertSql("update Worker set UserID = @UserID,WorkerMission = @WorkerMission where ID = @ID",
                     new List<MySqlParameter>()
                     {
                        new MySqlParameter("@UserID",this.UserID.ID),
                        new MySqlParameter("@WorkerMission",this.WorkerMission)
                     });
             }
            return this.ID;
        }

        public void Delete()
        {
            DAL.insertSql("update Worker set IsDeleted=1 Where ID=@ID", new MySqlParameter("@ID", this.ID));
        }

        public List<Worker> getWorkers(string filter)
        {

            List<Worker> result = new List<Worker>();

            DataTable data = DAL.readData("select * from Worker where IsDeleted=0 and UserID Like @filter", new MySqlParameter("@filter", '%' + filter + '%'));

            foreach (DataRow dr in data.Rows)
            {

                result.Add(
                    new Worker()
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        UserID = new User() { ID = Convert.ToInt32(dr["UserID"].ToString()) },
                        WorkerMission = dr["WorkerMission"].ToString()
                    }
               );
            }
            return result;
        }

        public void getWorker()
        {
            DataTable data = DAL.readData("select * from Worker where ID=@ID", new MySqlParameter("@ID", this.ID));
            this.UserID.ID = Convert.ToInt32(data.Rows[0]["UserID"].ToString());
            this.WorkerMission = data.Rows[0]["WorkerMission"].ToString();
            this.IsDeleted = Convert.ToBoolean(data.Rows[0]["IsDeleted"].ToString());
        }
    }
}
