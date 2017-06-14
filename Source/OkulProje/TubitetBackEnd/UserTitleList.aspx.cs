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
    public partial class UserTitleList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["kullanici"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnNewUserTitle_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            ResetForm();
            wndNew.Show();
        }

        protected void btnFind_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            List<UserTitle> UserTitles = new UserTitle().getUserTitles(txtFilter.Text);
            Store store = grdList.GetStore();
            store.DataSource = UserTitles;
            store.DataBind();
        }

        protected void btnSave_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(hdnID.Value);
            }
            catch (Exception) { }

            UserTitle u = new UserTitle()
            {
                ID = id,
                UserTitleName = txtNewUserTitle.Text
            };

            int control = u.save();
            if (control > 0)
            {
                X.Msg.Alert("Uyarı", "Ünvan kayıt edilmiştir. Yeni bir kayıt daha yapabilirsiniz.").Show();
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
            txtNewUserTitle.Clear();
            txtNewUserTitle.Focus();
        }

        [DirectMethod]
        public void ColumnEvents(object sender, Ext.Net.DirectEventArgs e)
        {
            int ID = Convert.ToInt32(e.ExtraParams["ID"].ToString());
            string CommandName = e.ExtraParams["CommandName"].ToString();

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

        private void Update(int ID)
        {
            UserTitle u = new UserTitle(){ ID = ID };
            u.getUserTitle();

            hdnID.SetValue(u.ID);
            txtNewUserTitle.Text = u.UserTitleName;
            wndNew.Show();
        }

        private void Delete(int ID)
        {
            UserTitle u = new UserTitle() {ID = ID};
            u.Delete();
            btnFind_DirectClick(null, null);
        }

    }
}