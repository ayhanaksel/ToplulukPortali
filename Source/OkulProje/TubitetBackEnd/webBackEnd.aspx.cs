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
    public partial class webBackEnd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAbout_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            webSection.Title = "About";
            webSection.Show();
            webBackEndModal web = new webBackEndModal()
            {
                key = "aboutMe"
            };
            web.getValue();
            txtText.Text = web.value;
            hdnSection.SetValue("aboutMe");
        }

        protected void btnVision_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            webSection.Title = "Vision";
            webSection.Show();
            webBackEndModal web = new webBackEndModal()
            {
                key = "vision"
            };
            web.getValue();
            txtText.Text = web.value;
            hdnSection.SetValue("vision");
        }

        protected void btnMission_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            webSection.Title = "Mission";
            webSection.Show();
            webBackEndModal web = new webBackEndModal()
            {
                key = "mission"
            };
            web.getValue();
            txtText.Text = web.value;
            hdnSection.SetValue("mission");
        }

        protected void btnContact_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            webSection.Title = "Contact";
            webSection.Show();
            webBackEndModal web = new webBackEndModal()
            {
                key = "contact"
            };
            web.getValue();
            txtText.Text = web.value;
            hdnSection.SetValue("contact");
        }

        protected void btnChange_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            webBackEndModal web = new webBackEndModal()
            {
                key = Convert.ToString(hdnSection.Value),
                value = txtText.Text
            };
            web.save();
            webSection.Close();
            X.Msg.Alert("Bilgilendirme", "Metin değiştirildi.").Show();
        }

        protected void btnClose_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            webSection.Close();
        }
    }
}