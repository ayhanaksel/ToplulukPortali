using ClassLibrary.Models;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TubitetBackEnd
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {

            User user = new User()
            {
                Email = txtEmail.Text,
                Password = txtPass.Text.Trim()
            };
            user.LoginControl();
            if(user.ID != 0)
            {
                
                if(user.UserTitle.ID != 1)
                {
                    wndYetkisiz.Show();                                             //Kullanıcı varmı yok mu kontrolu yapan ve Gerekli mesajları gösteren kodlar.
                }
                else
                {

                    HttpCookie userCookie = new HttpCookie("kullanici");

                    userCookie["ad"] = user.UserName;
                    userCookie["soyad"] = user.UserSurname;
                    userCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(userCookie);

                    X.Msg.Alert("Uyarı", userCookie["ad"] + userCookie["soyad"]).Show();

                    //Server.Transfer("Index.aspx");
                    Response.Redirect("Index.aspx");
                }
            }
            else
            {
                //X.Msg.Alert("Status", "Kullanıcı Adı veya Şifre hatalı", new JFunction { Fn = "showResult" }).Show();
                //Response.Redirect(Request.RawUrl);
                wndHata.Show();
            }


        }

        protected void btnClose_DirectClick(object sender, DirectEventArgs e)
        {
            wndHata.Close();
        }

        protected void Button1_DirectClick(object sender, DirectEventArgs e)
        {
            wndYetkisiz.Close();
        }
    }
}