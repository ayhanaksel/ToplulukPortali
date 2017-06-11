using ClassLibrary.Models;
using Ext.Net;
using System;
using System.Collections.Generic;

namespace TubitetBackEnd
{
    public partial class SaloonList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

            Saloon s = new Saloon()
            {
                ID = ID,
                SaloonName = txtSaloonName.Text,
                SaloonQuata = Convert.ToInt32(txtSaloonQuata.Text),
                SaloonAddress = txtSaloonAddress.Text
            };

            int control = s.save();

            if (control > 0)
            {
                X.Msg.Alert("Uyarı", "Salon başarıyla kayıt edilmiştir. Yeni bir kayıt daha yapabilirsiniz.").Show();
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
            txtSaloonName.Reset();
            txtSaloonName.Focus();
            txtSaloonQuata.Reset();
            txtSaloonAddress.Reset();
        }

        protected void btnNewSaloon_DirectClick(object sender, DirectEventArgs e)
        {
            ResetForm();
            wndNew.Show();
        }

        protected void btnList_DirectClick(object sender, DirectEventArgs e)
        {
            List<Saloon> saloons = new Saloon().getSaloons(txtFilter.Text);
            Store store = grdList.GetStore();
            store.DataSource = saloons;
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
            Saloon s = new Saloon() { ID = id };
            s.getSaloon();

            hdnID.SetValue(s.ID);
            txtSaloonName.Text = s.SaloonName;
            txtSaloonQuata.SetValue(s.SaloonQuata);
            txtSaloonAddress.Text = s.SaloonAddress;
            wndNew.Show();

        }

        private void Delete(int id)
        {

            Saloon s = new Saloon() { ID = id };
            s.Delete();
            btnList_DirectClick(null, null);

        }
    }
}