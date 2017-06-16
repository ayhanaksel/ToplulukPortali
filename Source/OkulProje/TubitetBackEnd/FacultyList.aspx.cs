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
    public partial class FacultyList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(Request.Cookies["kullanici"] == null)
            {                                                                        //Kullanıcı login oldumu onun kontrolunu yapar(güvenlik amaclı)
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

            Faculty f = new Faculty()
            {                                                           //Yeni fakulte ekleme kodları.
                ID = ID,
                FacultyName = txtFacultyName.Text
            };

            int control = f.save();

            if(control > 0)
            {
                X.Msg.Alert("Uyarı", "Fakülte kartı kayıt edilmiştir. Yeni bir kayıt daha yapabilirsiniz.").Show();
                listeleFonksiyonu();
                ResetForm();
            }
            else
            {
                X.Msg.Alert("Uyarı", "Veri tabanına kayıt etme hatası").Show();
            }

        }

        protected void btnClose_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            wndNew.Close();                                                     //Küçük ekleme ekranını kapatır.
        }

        private void ResetForm()
        {
            hdnID.Reset();
            txtFacultyName.Reset();
            txtFacultyName.Focus();
        }

        protected void btnNewFaculty_DirectClick(object sender, DirectEventArgs e)
        {
            ResetForm();
            wndNew.Show();
        }

        protected void btnList_DirectClick(object sender, DirectEventArgs e)
        {
            listeleFonksiyonu();
        }

        private void listeleFonksiyonu()
        {
            List<Faculty> faculties = new Faculty().getFaculties(txtFilter.Text);
            Store store = grdList.GetStore();
            store.DataSource = faculties;
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
            Faculty f = new Faculty() { ID = id };
            f.getFaculty();

            hdnID.SetValue(f.ID);
            txtFacultyName.Text = f.FacultyName;
            wndNew.Show();

        }

        private void Delete(int id)
        {

            Faculty f = new Faculty() {ID = id};
            f.Delete();
            btnList_DirectClick(null, null);

        }

     
    }
}