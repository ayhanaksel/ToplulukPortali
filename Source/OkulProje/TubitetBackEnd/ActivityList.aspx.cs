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
    public partial class ActivityList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ActivityType activity = new ActivityType();
            store.DataSource = activity.getActivities("");
            store.DataBind();

            Saloon salon = new Saloon();
            store1.DataSource = salon.getSaloons("");
            store1.DataBind();
        }
        protected void btnPhotoSave_DirectClick(object sender, Ext.Net.DirectEventArgs e)
        {
            if (this.attachPhoto.HasFile)
            {
                string photoPath = Server.MapPath("~/SpeakerPhoto/" + this.attachPhoto.PostedFile.FileName);
                this.attachPhoto.PostedFile.SaveAs(photoPath);
                X.Msg.Alert("Başarılı", "Resim Kaydedildi").Show();
                Image1.ImageUrl = "SpeakerPhoto/" + this.attachPhoto.PostedFile.FileName;
            }
            else
            {
                X.Msg.Alert("Uyarı", "Resim Kaydedilemedi").Show();
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
            int control=0;
            //Update işleminde id alındı ve resim yolu eklendi
            if (ID > 0)//Eğer id 0 dan büyükse update işlemi yapılsın
            {
                if (attachPhoto.FileName != "")  UpdateResimYolu = attachPhoto.FileName; 
                Activity f = new Activity()
                {
                    ID = ID,
                    ActivityName = txtActivityName.Text,
                    ActivityType = cmbxActivityType.Text,
                    ActivityDate = dtfActivityDate.SelectedDate,
                    Saloon = cmbxSaloon.Text,
                    GuessLimit = Convert.ToInt32(txtGuessLimit.Text),
                    ActivityPhoto = UpdateResimYolu
                };
                control = f.save();
            }
            else//0 dan küçük ise insert işlemi yapılsın
            {

                Activity f = new Activity()
                {
                    ID = ID,
                    ActivityName = txtActivityName.Text,
                    ActivityType = cmbxActivityType.SelectedItem.Text,
                    ActivityDate = dtfActivityDate.SelectedDate,
                    Saloon = cmbxSaloon.SelectedItem.Text,
                    GuessLimit = Convert.ToInt32(txtGuessLimit.Text),
                    ActivityPhoto = this.attachPhoto.PostedFile.FileName
                };
                control = f.save();

            }

     

            if (control > 0)
            {
                X.Msg.Alert("Uyarı", "Konuşmacı kayıt edilmiştir. Yeni bir kayıt daha yapabilirsiniz.").Show();
                ListelemeFonksiyonu();
                ResetForm();
            }
            else
            {
                X.Msg.Alert("Uyarı", "Veri tabanına kayıt etme hatası").Show();
            }

        }
        private void ResetForm()
        {
            hdnID.Reset();
            dtfActivityDate.Reset();
            txtActivityName.Reset();
            txtGuessLimit.Reset();
            cmbxActivityType.Text = "";
            Image1.ImageUrl = "";
            attachPhoto.Reset();
            cmbxSaloon.Text = "";
        }
        protected void btnClose_DirectClick(object sender, Ext.Net.DirectEventArgs e) //
        {
            ResetForm();
            wndNew.Close();

        }
        protected void btnNewActivity_DirectClick(object sender, DirectEventArgs e) // Yeni Aktivite eklemek için kullanılan buton
        {
            ResetForm();
            wndNew.Show();
        }
        protected void btnList_DirectClick(object sender, DirectEventArgs e)
        {
            ListelemeFonksiyonu();
        }

        private void ListelemeFonksiyonu() //listeleme fonksiyonu yazıldı
        {
            List<Activity> activities = new Activity().getActivities(txtFilter.Text);
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
        // static yolu resim tanımladık
        private static String  UpdateResimYolu=String.Empty;
        private void Update(int id)
        {
            Activity f = new Activity() { ID = id };
            f.getActivity();
            hdnID.SetValue(f.ID);
            txtActivityName.Text = f.ActivityName;
            cmbxActivityType.Text = f.ActivityType;
            cmbxSaloon.Text = f.Saloon;
            Image1.ImageUrl = "SpeakerPhoto/" + f.ActivityPhoto;
            //Burda tanımlanan resim yolu atandı
            UpdateResimYolu = f.ActivityPhoto;
            txtGuessLimit.Text = f.GuessLimit.ToString();
            dtfActivityDate.Text = f.ActivityDate.ToString();
            wndNew.Show();

        }

        private void Delete(int id)
        {

            Activity f = new Activity() { ID = id };
            f.Delete();
            btnList_DirectClick(null, null);

        }
    }
}