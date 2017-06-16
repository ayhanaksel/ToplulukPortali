using ClassLibrary.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibrary.Models
{
     public class Faculty
    {
        public int ID { get; set; }
        public string FacultyName { get; set; }
        public Boolean IsDeleted { get; set; }

        public int save()
        {
            if(this.ID == 0)
            {                                                                   //Yeni fakulte ekleme kodları.
                this.ID = DAL.insertSql("insert into Faculty(FacultyName) values (@FacultyName)", new MySqlParameter("@FacultyName", this.FacultyName));
            }
            else
            {
                DAL.insertSql("update Faculty set FacultyName = @FacultyName where ID = @ID",
                    new List<MySqlParameter>()
                    {
                        new MySqlParameter("@FacultyName",this.FacultyName),
                        new MySqlParameter("@ID",this.ID)
                    }
                    );
            }
            return this.ID;
        }

        public void Delete()
        {                                                                   //Fakulte silme kodları.
            DAL.insertSql("update Faculty set IsDeleted=1 Where ID=@ID", new MySqlParameter("@ID", this.ID));
        }

        public List<Faculty> getFaculties(string filter)
        {

            List<Faculty> result = new List<Faculty>();

            DataTable data = DAL.readData("select * from Faculty where IsDeleted=0 and FacultyName Like @filter", new MySqlParameter("@filter", '%' + filter + '%'));

                                                                               //Fakulteleri listeleme kodları.

            foreach (DataRow dr in data.Rows)
            {
               
                result.Add(
                    new Faculty
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        FacultyName = dr["FacultyName"].ToString()
                    }
               );

            }
            return result;
        }

        public void getFaculty()
        {                                                                                   //Bir fakulte bilgilerini getirme kodları.
            DataTable data = DAL.readData("select * from Faculty where ID=@ID", new MySqlParameter("@ID", this.ID));
            this.FacultyName = data.Rows[0]["FacultyName"].ToString();
            this.IsDeleted = Convert.ToBoolean(data.Rows[0]["IsDeleted"].ToString());
        }

    }
}
