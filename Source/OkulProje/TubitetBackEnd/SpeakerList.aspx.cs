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
            Interest interest = new Interest();
            store.DataSource = interest.getInterests("");
            store.DataBind();

            if (Request.Cookies["kullanici"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void btnPhotoSave_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            if (this.attachPhoto.HasFile)               //Eğer Fotoğraf seçilmiş ise fotoğrafın yolunu server'a kaydeder.
            {
                string photoPath = Server.MapPath("~/SpeakerPhoto/" + this.attachPhoto.PostedFile.FileName);
                this.attachPhoto.PostedFile.SaveAs(photoPath);
                X.Msg.Alert("Başarılı", "Resim Kaydedildi").Show();
                Image1.ImageUrl = "SpeakerPhoto/"+ this.attachPhoto.PostedFile.FileName;    
            }
            else
            {                                                       //Eğer seçilmemişse kaydetme hatası mesajı kullanıcıya gösterilir.
                X.Msg.Alert("Uyarı", "Resim Kaydedilemedi").Show();
            }
        }

        protected void btnSave_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {

            if (this.attachCV.HasFile)                              //Eğer CV seçilmişse seçilen dosyanın yolunu server'a kaydeder.
            {
                string cvPath = Server.MapPath("~/SpeakersCV/" + this.attachCV.PostedFile.FileName);
                this.attachCV.PostedFile.SaveAs(cvPath);
                X.Msg.Alert("Başarılı", "CV Kaydedildi.").Show();
            }
            else
            {                                                       //Eğer CV seçilmemişse kaydetme hatası mesajı kullanıcıya gösterilir.
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
            int control = 0;
            //Update işleminde id alındı ve resim yolu eklendi
            if (ID > 0)//Eğer id 0 dan büyükse update işlemi yapılsın
            {
                if (attachPhoto.FileName != "") UpdateResimYolu = attachPhoto.FileName;
                if (attachCV.FileName != "") UpdateCVYolu = attachCV.FileName;
                Speaker f = new Speaker()
                {
                    ID = ID,
                    SpeakerName = txtSpeakerName.Text,
                    SpeakerPhoto = UpdateResimYolu,
                    SpeakerCV = UpdateCVYolu,
                    SpeakerWorksFor = txtSpeakerWorksFor.Text,
                    SpeakerSpeakAbout = cmbxInterest.Text
                };
                control = f.save();
            }
            else // değilse insert işlemi yapılsın
            {
                Speaker f = new Speaker()
                {
                    ID = ID,
                    SpeakerName = txtSpeakerName.Text,
                    SpeakerPhoto = this.attachPhoto.PostedFile.FileName,
                    SpeakerCV = this.attachCV.PostedFile.FileName,
                    SpeakerWorksFor = txtSpeakerWorksFor.Text,
                    SpeakerSpeakAbout = cmbxInterest.SelectedItem.Text
                };
                control = f.save();
            }

            if (control > 0)
            {
                X.Msg.Alert("Uyarı", "Konuşmacı kayıt edilmiştir. Yeni bir kayıt daha yapabilirsiniz.").Show();
                listelemeFonsiyonu();
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
            listelemeFonsiyonu();
        }

        private void listelemeFonsiyonu()
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
        private static String UpdateResimYolu = String.Empty;
        private static String UpdateCVYolu = String.Empty;
        private void Update(int id)
        {
            Speaker f = new Speaker() { ID = id };
            f.getSpeaker();
            hdnID.SetValue(f.ID);
            txtSpeakerName.Text = f.SpeakerName;
            cmbxInterest.Text = f.SpeakerSpeakAbout;
            Image1.ImageUrl = "SpeakerPhoto/"+f.SpeakerPhoto;
            UpdateResimYolu = f.SpeakerPhoto;
            attachCV.Text = f.SpeakerCV;
            UpdateCVYolu = f.SpeakerCV;
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