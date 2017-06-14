using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Models;
using Ext.Net;


namespace TubitetBackEnd
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 

        }

        protected void btnCikis_DirectClick(object sender, DirectEventArgs e)
        {           
            if (Request.Cookies["kullanici"] != null)
            {
                HttpCookie musteriCookie = Request.Cookies["kullanici"];
                musteriCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(musteriCookie);
                Response.Redirect("Login.aspx");
            }
           
        }
    }
}