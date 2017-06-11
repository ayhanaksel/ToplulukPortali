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
    public partial class InterestList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnNewInterest_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            ResetForm();
            wndNew.Show();
        }

        protected void btnList_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {

            List<Interest> interests = new Interest().getInterests(txtFilter.Text);
            Store store = grdList.GetStore();
            store.DataSource = interests;
            store.DataBind();

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

            Interest f = new Interest()
            {
                ID = ID,
                InterestName = txtInterestName.Text
            };

            int control = f.save();

            if (control > 0)
            {
                X.Msg.Alert("Uyarı", "İlgi alanı başarıyla kayıt edilmiştir. Yeni bir kayıt daha yapabilirsiniz.").Show();
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
            txtInterestName.Reset();
            txtInterestName.Focus();
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
            Interest i = new Interest() { ID = id };
            i.getInterest();

            hdnID.SetValue(i.ID);
            txtInterestName.Text = i.InterestName;
            wndNew.Show();

        }

        private void Delete(int id)
        {

            Interest i = new Interest() { ID = id };
            i.Delete();
            btnList_DirectClick(null, null);

        }

    }
}