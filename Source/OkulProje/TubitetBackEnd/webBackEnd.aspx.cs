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
                key = "AboutMe"
            };
            web.getValue();
            txtText.Text = web.value;
            hdnSection.SetValue("AboutMe");
        }

        protected void btnVision_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            webSection.Title = "Vision";
            webSection.Show();
            webBackEndModal web = new webBackEndModal()
            {
                key = "Vision"
            };
            web.getValue();
            txtText.Text = web.value;
            hdnSection.SetValue("Vision");
        }

        protected void btnMission_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            webSection.Title = "Mission";
            webSection.Show();
            webBackEndModal web = new webBackEndModal()
            {
                key = "Mission"
            };
            web.getValue();
            txtText.Text = web.value;
            hdnSection.SetValue("Mission");
        }

        protected void btnContact_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            webSection.Title = "Contact";
            webSection.Show();
            webBackEndModal web = new webBackEndModal()
            {
                key = "Contact"
            };
            web.getValue();
            txtText.Text = web.value;
            hdnSection.SetValue("COntact");
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