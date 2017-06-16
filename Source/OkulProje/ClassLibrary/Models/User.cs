using ClassLibrary.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace ClassLibrary.Models
{
    public class User
    {

        public int ID { get; set; }
        public Faculty Faculty { get; set; }
        public UserTitle UserTitle { get; set; }
        public string UserName { get; set; }
        public int IsVoted { get; set; }
        public string UserSurname { get; set; }
        public string Email { get; set; }
        public string UserNo { get; set; }
        public string UserAdress { get; set; }
        public string Password { get; set; }
        public int UserState { get; set; }
        public string ActivationCode { get; set; }
        public string UserPhoto { get; set; }
        public string UserIdentifyCopy { get; set; }
        public Boolean IsDeleted { get; set; }
        public List<UserInterest> UserInterests { get; set; }

        public User()
        {
            UserInterests = new List<UserInterest>();
            Faculty = new Faculty();
            UserTitle = new UserTitle();
        }
        
        //email gönderildiğinde email gönderildiği halde alınan hata

        public int save()
        {
                
            if(this.ID == 0)
            {

                DataTable data = DAL.readData("select * from User where Email=@Email", new MySqlParameter("@Email", this.Email)); //Böyle bir mail varmı kontrol
                if(data.Rows.Count == 0)
                {
                        Random rd = new Random();
                        string ActivationCode = rd.Next(10000, 99999).ToString();


                        this.ID = DAL.insertSql("insert into User(FacultyID,UserTitleID,IsVoted,Username,UserSurname,Email,UserNo,UserAdress,Password,UserState,ActivationCode,UserPhoto,UserIdentifyCopy) values(@FacultyID,@UserTitleID,@IsVoted,@UserName,@UserSurname,@Email,@UserNo,@UserAdress,@Password,@UserState,@ActivationCode,@UserPhoto,@UserIdentifyCopy)", new List<MySqlParameter>() {

                        new MySqlParameter("@FacultyID",this.Faculty.ID),
                        new MySqlParameter("@UserTitleID",this.UserTitle.ID),
                        new MySqlParameter("@UserName",this.UserName),
                        new MySqlParameter("@UserSurname",this.UserSurname),
                        new MySqlParameter("@Email",this.Email),                                    //Bu kod yeni kullanıcı eklemek için kullanıyor.
                        new MySqlParameter("@UserNo",this.UserNo),
                        new MySqlParameter("@UserAdress",this.UserAdress),
                        new MySqlParameter("@Password",this.Password),
                        new MySqlParameter("@UserState",this.UserState),
                        new MySqlParameter("@ActivationCode",ActivationCode),
                        new MySqlParameter("@UserPhoto",this.UserPhoto),
                        new MySqlParameter("@IsVoted",this.IsVoted),
                        new MySqlParameter("@UserIdentifyCopy",this.UserIdentifyCopy)
                         });

                        string kontrol = sendMail(ActivationCode);
                        if (kontrol == "0")
                        {
                            this.ID = -1;

                        }
                }
                else
                {
                    this.ID = -1; //Email kontrol et uyarısı
                }

                

            }
            else
            {
                DAL.insertSql("update User set FacultyID = @FacultyName,UserTitleID = @UserTitleID,UserName = @UserName,IsVoted = @IsVoted,UserSurname = @UserSurname,Email = @Email, UserNo = @UserNo,UserAdress = @UserAdress, Password = @Password,UserState = @UserState, ActivationCode = @ActivationCode , UserPhoto = @UserPhoto , UserIdentifyCopy = @UserIdentifyCopy where ID = @ID",
                    new List<MySqlParameter>()
                    {
                        new MySqlParameter("@FacultyID",this.Faculty.ID),
                        new MySqlParameter("@UserTitleID",this.UserTitle.ID),
                        new MySqlParameter("@UserName",this.UserName),
                        new MySqlParameter("@UserSurname",this.UserSurname),
                        new MySqlParameter("@Email",this.Email),                         //Kullanıcıyı güncellemek için kullanılır.
                        new MySqlParameter("@UserNo",this.UserNo),
                        new MySqlParameter("@UserAdress",this.UserAdress),
                        new MySqlParameter("@Password",this.Password),
                        new MySqlParameter("@UserState",this.UserState),
                        new MySqlParameter("@ActivationCode",this.ActivationCode),
                        new MySqlParameter("@UserPhoto",this.UserPhoto),
                        new MySqlParameter("@IsVoted",this.IsVoted),
                        new MySqlParameter("@UserIdentifyCopy",this.UserIdentifyCopy)
                    });

            }

            this.ClearAllInterests();

             foreach(UserInterest u in this.UserInterests)
             {
                if (u.User == null)
                {                                                                   //Bir kullanıcnın birden fazla ilgi alanı seçmesini sağlıyor.
                    u.User = new User();
                }

                 u.User.ID = this.ID;
                 u.Save();
             }


            return this.ID;

        }

        private void ClearAllInterests()
        {
            DAL.insertSql("Update UserInterest set IsDeleted = 1 Where UserId = @UserID", new MySqlParameter("@UserID", this.ID));      //İlgi alanlarını sıfırlıyor.
        }

        public int Delete()
        {
            DAL.insertSql("update User set IsDeleted=1 Where ID=@ID", new MySqlParameter("@ID", this.ID));          //Kullanıcı silmek için kullanılır.
            ClearAllInterests();
            return this.ID;
        }

        public User LoginControl()               //Böyle bir kullanıcı varmı yok mu bunu kontolu yapılır.Eğer kullanıcı varsa yönetici mi normal üye mi bunu kontrolu yapılır.
        {

            DataTable data = DAL.readData("select * from User where IsDeleted = 0 and Email=@Email and Password=@Password",new List<MySqlParameter> {
                new MySqlParameter("@Email",this.Email),
                new MySqlParameter("@Password",this.Password)
            });

            if (data.Rows.Count == 1)
            {

                this.UserName = data.Rows[0]["UserName"].ToString();
                this.UserSurname = data.Rows[0]["UserSurname"].ToString();
                this.ID = Convert.ToInt32(data.Rows[0]["ID"]);

                this.Faculty.ID = Convert.ToInt32(data.Rows[0]["FacultyID"]);
                this.Faculty = new Faculty()
                {
                    ID = this.Faculty.ID
                };
                this.Faculty.getFaculty();

                this.UserTitle.ID = Convert.ToInt32(data.Rows[0]["UserTitleID"]);
                this.UserTitle = new UserTitle()
                {
                    ID = this.UserTitle.ID
                };
                this.UserTitle.getUserTitle();

                this.Email = data.Rows[0]["Email"].ToString();
                this.UserNo = data.Rows[0]["UserNo"].ToString();
                this.UserAdress = data.Rows[0]["UserAdress"].ToString();
                this.Password = data.Rows[0]["Password"].ToString();
                this.UserState =Convert.ToInt32(data.Rows[0]["UserState"]);
                this.UserPhoto = data.Rows[0]["UserPhoto"].ToString();
                this.IsVoted = Convert.ToInt32(data.Rows[0]["IsVoted"].ToString());
                this.UserIdentifyCopy = data.Rows[0]["UserIdentifyCopy"].ToString();

                return this;

            }
            else
                return null;
        }

        public string sendMail(string ActivationCode)
        {      
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("ufukunal.00@gmail.com");
            //
            ePosta.To.Add(this.Email);      
            //
            ePosta.Subject = "KONU";                                            //Yeni kayıt olunduğunda mail gönderen kodlar.
            //
            ePosta.Body = "Activation Code = "+ ActivationCode;
            //
            SmtpClient smtp = new SmtpClient();
            //
            smtp.Credentials = new System.Net.NetworkCredential("ufukunal.00@gmail.com", "51697152152++");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            object userState = ePosta;
            bool kontrol = true;
            try
            {
                smtp.SendAsync(ePosta, (object)ePosta);
            }
            catch (SmtpException ex)
            {
                kontrol = false;
                  
            }

            if (kontrol == true){
                return "1";
                
            }
            else
            {
                return "0";
            }
   

        }

        public int ChangedState() //userstate == 1 ve activation doğru ise pasif **** userstate == 1 ve activation yanlış ise beklemede **** userstate == 2 ise aktif
        {

            if (this.UserState == 1)
            {
                DataTable data = DAL.readData("select * from User where ID=@ID", new MySqlParameter("@ID", this.ID));

                if (this.ActivationCode == data.Rows[0]["ActivationCode"].ToString())
                {
                   this.UserState = 1;                                              //Üyenin durumunu deiştiren kodlar.(beklemede-pasif-aktif)
                }
                else
                {
                    this.UserState = 0;
                }

            }
            else
            {
                this.UserState = 2;
            }

            DAL.insertSql("update User set UserState = @UserState where ID=@ID", new List<MySqlParameter>()
            {
                new MySqlParameter("@UserState",this.UserState),
                new MySqlParameter("ID",this.ID)
            });


            return this.UserState;
        }

    }
}
 