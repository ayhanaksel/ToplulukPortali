using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TubitetBackEnd
{
    public partial class EventList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["kullanici"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnNewEvent_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {

        }
    }
}