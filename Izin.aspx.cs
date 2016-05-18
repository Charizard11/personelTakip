using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using PTS2.Common;


namespace PTS2
{
    public partial class Izin : System.Web.UI.Page
    {
        string personelID = "";
        metodlar klas = new metodlar();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["personelID"] == null)
                Response.Redirect("Login.aspx");

            if (Session["AdSoyad"] != null)
                lblPersonelAdi.Text = Session["AdSoyad"].ToString();

 
            else
            {
                btnCıkış.Visible = true;
                LiteralGuncelleme.Visible = true;
                btnGeri.Visible = false;
                
            }
            if (Session["isAdmin"].ToString() != "True")
            {

                LiteralGuncelleme.Visible = true;

            }

            if (!IsPostBack) // *** dropdownlist li enum kullanımı ***
            {


                
                LiteralGuncelleme.Text = "<a href=\"PersonelGuncelle.aspx?personelID=" + Session["personelID"] + "\"><span class=\"glyphicon glyphicon-edit\"></span>Bilgilerini Güncelle</a>";
                Array values = Enum.GetValues(typeof(Enumlar.IzinTipi));
                Array names = Enum.GetNames(typeof(Enumlar.IzinTipi));

                for (int i = 0; i < names.Length; i++)
                {
                    ddlIzinTuru.Items.Add(new ListItem(names.GetValue(i).ToString(), Convert.ToInt32(values.GetValue(i)).ToString()));
                }
                ddlIzinTuru.Items.Insert(0, new ListItem("Seçiniz", ""));
            }
        }

        protected void btnEkle_Click(object sender, EventArgs e)
        {

            

            DateTime dt4 = Convert.ToDateTime(txtBaslangicTarihi.Text);
            DateTime dt5 = Convert.ToDateTime(txtBitisTarihi.Text);

 
           
          
            if (DateTime.Compare(dt4, dt5) == 0)
            {

                AlertCustom.ShowCustom(this.Page, "İki Tarih Eşit Olamaz..!!");
            }

            else if (DateTime.Compare(dt4, dt5) > 0)
            {
                AlertCustom.ShowCustom(this.Page, "Başlangıç Tarihi, Bitiş Tarihinden Sonra Olamaz!!");

            }
            else
            {


                SqlConnection con = klas.baglan();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;
                cmd.CommandText = "insert into Izin (baslamaTarihi,bitisTarihi,personelID,durum,islemTarihi,aciklama,izinTuru) values (@baslamaTarihi,@bitisTarihi,@personelID,@durum,@islemTarihi,@aciklama,@izinTuru)";
                cmd.Parameters.Add("@baslamaTarihi", dt4);
                cmd.Parameters.Add("@aciklama", txtboxAciklama.Text);
                //<%# PTS2.Web.Utilities.EnumExtensionMethods.GetDescription((PTS2.Common.Enumlar.IzinTipi)Convert.ToInt32( Eval("izinTuru"))) %>
                cmd.Parameters.Add("@izinTuru", ddlIzinTuru.SelectedValue);
                //cmd.Parameters.Add("@bitisTarihi", txtBitisTarihi.Text);
                cmd.Parameters.Add("@bitisTarihi", dt5);
                cmd.Parameters.Add("@islemTarihi", DateTime.Now);
                cmd.Parameters.Add("@durum", (short)Enumlar.IzinDurum.ISLEME_ALINDI);
                //cmd.Parameters.Add("@gun",gunn);
                if (Session["personelID"] != null)
                {
                    cmd.Parameters.Add("@personelID", System.Data.SqlDbType.SmallInt).Value = Convert.ToInt32(Session["personelID"].ToString());
                }
                cmd.ExecuteNonQuery();

                AlertCustom.ShowCustom(this.Page, "İzin İşlemi başarılı..");
                txtBaslangicTarihi.Text = "";
                txtBitisTarihi.Text = "";


            }
            temizle(pnlIzin);

           
        }

        protected void btnCıkış_Click(object sender, EventArgs e)
        {
            Session.Remove("personelID");
            Response.Redirect("Login.aspx");
        }

        public void temizle(Panel PNL)
        {
            foreach (Control ctrl in pnlIzin.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).Text = "";
                }
                if (ctrl is DropDownList)
                {
                    ((DropDownList)ctrl).Text = "";
                }
                

            }
        }





    }
}