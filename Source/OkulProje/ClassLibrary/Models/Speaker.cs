using ClassLibrary.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibrary.Models
{
    public class Speaker
    {
        public int ID { get; set; }
        public string SpeakerName { get; set; }
        public string SpeakerWorksFor { get; set; }
        public string SpeakerSpeakAbout { get; set; }
        public string SpeakerCV { get; set; }
        public string SpeakerPhoto { get; set; }
        public Boolean IsDeleted { get; set; }

        public int save()
        {
            if (this.ID == 0)
            {
                this.ID = DAL.insertSql("insert into Speakers(SpeakersName,SpeakerPhoto,SpeakerCV,SpeakerWorksFor,SpeakerSpeakAbout) values (@SpeakerName,@SpeakerPhoto,@SpeakerCV,@SpeakerWorksFor,@SpeakerSpeakAbout)", new List<MySqlParameter>()
                    {
                        new MySqlParameter("@SpeakerName",this.SpeakerName),
                        new MySqlParameter("@SpeakerPhoto",this.SpeakerPhoto),
                        new MySqlParameter("@SpeakerCV",this.SpeakerCV),
                        new MySqlParameter("@SpeakerWorksFor",this.SpeakerWorksFor),
                        new MySqlParameter("@SpeakerSpeakAbout",this.SpeakerSpeakAbout)
                    });
            }
            else
            {
                DAL.insertSql("update Speakers set SpeakersName = @SpeakerName, SpeakerPhoto=@SpeakerPhoto, SpeakerCV=@SpeakerCV, SpeakerWorksFor=@SpeakerWorksFor, SpeakerSpeakAbout=@SpeakerSpeakAbout where ID = @ID",
                    new List<MySqlParameter>()
                    {
                       new MySqlParameter("@SpeakerName",this.SpeakerName),
                        new MySqlParameter("@SpeakerPhoto",this.SpeakerPhoto),
                        new MySqlParameter("@SpeakerCV",this.SpeakerCV),
                        new MySqlParameter("@SpeakerWorksFor",this.SpeakerWorksFor),
                        new MySqlParameter("@SpeakerSpeakAbout",this.SpeakerSpeakAbout),
                        new MySqlParameter("@ID",this.ID)
                    }
                    );
            }
            return this.ID;
        }

        public void Delete()
        {
            DAL.insertSql("update Speakers set IsDeleted=1 Where ID=@ID", new MySqlParameter("@ID", this.ID));
        }

        public List<Speaker> getSpeakers(string filter)
        {

            List<Speaker> result = new List<Speaker>();

            DataTable data = DAL.readData("select * from Speakers where IsDeleted=0 and SpeakersName Like @filter", new MySqlParameter("@filter", '%' + filter + '%'));



            foreach (DataRow dr in data.Rows)
            {

                result.Add(
                    new Speaker
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        SpeakerName = dr["SpeakersName"].ToString(),
                        SpeakerPhoto = dr["SpeakerPhoto"].ToString(),
                        SpeakerCV = dr["SpeakerCV"].ToString(),
                        SpeakerWorksFor = dr["SpeakerWorksFor"].ToString(),
                        SpeakerSpeakAbout = dr["SpeakerSpeakAbout"].ToString()
                    }
               );

            }
            return result;
        }

        public void getSpeaker()
        {
            DataTable data = DAL.readData("select * from Speakers where ID=@ID", new MySqlParameter("@ID", this.ID));
            this.SpeakerName = data.Rows[0]["SpeakersName"].ToString();
            this.SpeakerPhoto = data.Rows[0]["SpeakerPhoto"].ToString();
            this.SpeakerCV = data.Rows[0]["SpeakerCV"].ToString();
            this.SpeakerWorksFor = data.Rows[0]["SpeakerWorksFor"].ToString();
            this.SpeakerSpeakAbout = data.Rows[0]["SpeakerSpeakAbout"].ToString();
            this.IsDeleted = Convert.ToBoolean(data.Rows[0]["IsDeleted"].ToString());
        }
    }
}
