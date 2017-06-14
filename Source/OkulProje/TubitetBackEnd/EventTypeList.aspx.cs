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
    public partial class EventTypeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["kullanici"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnSave_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {

            int ID = 0;
            try
            {
                ID = Convert.ToInt32(hdnID.Value);
            }
            catch (Exception)
            {

            }

            ActivityType f = new ActivityType()
            {
                ID = ID,
                TypeName = txtEventTypeName.Text
            };

            int control = f.save();

            if (control > 0)
            {
                X.Msg.Alert("Uyarı", "Etkinlik türü başarıyla kayıt edilmiştir. Yeni bir kayıt daha yapabilirsiniz.").Show();
                ResetForm();
            }
            else
            {
                X.Msg.Alert("Uyarı", "Veri tabanına kayıt etme hatası").Show();
            }

        }

        protected void btnClose_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            wndNew.Close();
        }

        private void ResetForm()
        {
            hdnID.Reset();
            txtEventTypeName.Reset();
            txtEventTypeName.Focus();
        }

        protected void btnNewEventType_DirectClick(object sender, DirectEventArgs e)
        {
            ResetForm();
            wndNew.Show();
        }

        protected void btnList_DirectClick(object sender, DirectEventArgs e)
        {
            List<ActivityType> activities = new ActivityType().getActivities(txtFilter.Text);
            Store store = grdList.GetStore();
            store.DataSource = activities;
            store.DataBind();
        }

        [DirectMethod]
        public void ColumnEvents(object sender, Ext.Net.DirectEventArgs e)
        {
            int ID = Convert.ToInt32(e.ExtraParams["ID"]);
            string CommandName = e.ExtraParams["CommandName"];

            switch (CommandName)
            {
                case "cmdUpdate":
                    Update(ID);
                    break;
                case "cmdDelete":
                    Delete(ID);
                    break;
            }
        }

        private void Update(int id)
        {
            ActivityType f = new ActivityType() { ID = id };
            f.getActivity();

            hdnID.SetValue(f.ID);
            txtEventTypeName.Text = f.TypeName;
            wndNew.Show();

        }

        private void Delete(int id)
        {

            ActivityType f = new ActivityType() { ID = id };
            f.Delete();
            btnList_DirectClick(null, null);

        }
    }
}