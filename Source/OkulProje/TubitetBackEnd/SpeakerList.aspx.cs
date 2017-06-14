using System;
using ClassLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;

namespace TubitetBackEnd
{
    public partial class SpeakerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["kullanici"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Interest interest = new Interest();
                store.DataSource = interest.getInterests("");
                store.DataBind();
            }
        }
        protected void btnPhotoSave_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            if (this.attachPhoto.HasFile)
            {
                string photoPath = Server.MapPath("~/SpeakerPhoto/" + this.attachPhoto.PostedFile.FileName);
                this.attachPhoto.PostedFile.SaveAs(photoPath);
                X.Msg.Alert("Başarılı", "Resim Kaydedildi").Show();
                Image1.ImageUrl = "SpeakerPhoto/"+ this.attachPhoto.PostedFile.FileName;    
            }
            else
            {
                X.Msg.Alert("Uyarı", "Resim Kaydedilemedi").Show();
            }
        }

        protected void btnSave_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {

            if (this.attachCV.HasFile)
            {
                string cvPath = Server.MapPath("~/SpeakersCV/" + this.attachCV.PostedFile.FileName);
                this.attachCV.PostedFile.SaveAs(cvPath);
                X.Msg.Alert("Başarılı", "CV Kaydedildi.").Show();
            }
            else
            {
                X.Msg.Alert("Uyarı", "CV Kaydedilemedi.").Show();
            }

            int ID = 0;
            try
            {
                ID = Convert.ToInt32(hdnID.Value);
            }
            catch (Exception)
            {

            }

            Speaker f = new Speaker()
            {
                ID = ID,
                SpeakerName = txtSpeakerName.Text,
                SpeakerPhoto = this.attachPhoto.PostedFile.FileName,
                SpeakerCV = this.attachCV.PostedFile.FileName,
                SpeakerWorksFor = txtSpeakerWorksFor.Text,
                SpeakerSpeakAbout = cmbxInterest.SelectedItem.Text
            };

            int control = f.save();

            if (control > 0)
            {
                X.Msg.Alert("Uyarı", "Konuşmacı kayıt edilmiştir. Yeni bir kayıt daha yapabilirsiniz.").Show();
                ResetForm();
            }
            else
            {
                X.Msg.Alert("Uyarı", "Veri tabanına kayıt etme hatası").Show();
            }

        }

        protected void btnClose_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            ResetForm();
            wndNew.Close();

        }

        private void ResetForm()
        {
            hdnID.Reset();
            txtSpeakerWorksFor.Reset();
            txtSpeakerName.Reset();
            cmbxInterest.Text = "";
            Image1.ImageUrl = "";
            attachPhoto.Reset();
            attachCV.Reset();
            
        }

        protected void btnNewSpeaker_DirectClick(object sender, DirectEventArgs e)
        {
            ResetForm();
            wndNew.Show();
        }

        protected void btnList_DirectClick(object sender, DirectEventArgs e)
        {
            List<Speaker> speakers = new Speaker().getSpeakers(txtFilter.Text);
            Store store = grdList.GetStore();
            store.DataSource = speakers;
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
            Speaker f = new Speaker() { ID = id };
            f.getSpeaker();
            hdnID.SetValue(f.ID);
            txtSpeakerName.Text = f.SpeakerName;
            cmbxInterest.Text = f.SpeakerSpeakAbout;
            Image1.ImageUrl = "SpeakerPhoto/"+f.SpeakerPhoto;
            txtSpeakerWorksFor.Text = f.SpeakerWorksFor;
            wndNew.Show();

        }

        private void Delete(int id)
        {

            Speaker f = new Speaker() { ID = id };
            f.Delete();
            btnList_DirectClick(null, null);

        }

    }
}