using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;

namespace PTS2
{
    public partial class PersonelGüncelle : System.Web.UI.Page
    {
        metodlar klas = new metodlar();
        string personelid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            personelid = Request.QueryString["personelID"];
            lblEmailKontrol.Text = "";

            if (Session["personelID"] == null)
                Response.Redirect("Login.aspx");


            if (Session["personelID"].ToString() == personelid)
            {
                pnlSifreGorunurluk.Visible = true;
            }
            // en başta sessionla gelen kişinin isadmin kısmının true yada false olup olmadığını kontrol edicen

           
            //if (Session["isAdmin"].ToString() == "True")
            //{
            //    pnlRol.Visible = true;
            //}

            //if (Session["RolId"].ToString() == "5" )
            //{
            //    btnGorunurluk2.Visible = true;
            //    pnlRol.Visible = true;
            //    btnSifreGonder.Visible = true;
            //}
            if (Session["isAdmin"].ToString() == "True")
            {
                btnGorunurluk2.Visible = true;
                pnlRol.Visible = true;
                btnSifreGonder.Visible = true;
            }
            else
            {
                btnGorunurluk1.Visible = true;
                pnlSifreGorunurluk.Visible = true;
            }


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT dbo.Rol.RolID, dbo.Bolum.bolumID, dbo.Personel.ad, dbo.Personel.soyad,dbo.Personel.isAdmin, dbo.Personel.email, dbo.Personel.tel, dbo.Personel.dogumTarihi, dbo.Personel.sifre FROM dbo.Bolum INNER JOIN dbo.Personel ON dbo.Bolum.bolumID = dbo.Personel.bolumID INNER JOIN dbo.Rol ON dbo.Personel.RolID = dbo.Rol.RolID where dbo.Personel.personelID=@personelId";
            // literal için
            cmd.Parameters.AddWithValue("@personelId", personelid);
            DataRow drPersonel = klas.GetDataRow(cmd);

            //if (Convert.ToBoolean(drPersonel["isAdmin"]) == true)
                if (drPersonel["isAdmin"].ToString() == "True")
            {
                lbAdminlik.Text = "<span class=\"glyphicon glyphicon-remove-circle\"></span>Onayı Kaldır";
            }
            else
                lbAdminlik.Text = "<span class=\"glyphicon glyphicon-ok-circle\"></span>Onayla";

            if (Page.IsPostBack == false)
            {
                
                Rol();
                ddlRolGuncelle.SelectedValue = drPersonel["RolID"].ToString();
                TxtboxAdGuncelle.Text = drPersonel["ad"].ToString();
                TxtboxsoyadGuncelle.Text = drPersonel["soyad"].ToString();
                TxtboxepostaGuncelle.Text = drPersonel["email"].ToString().Substring(0, drPersonel["email"].ToString().IndexOf("@"));

                string Metin = drPersonel["email"].ToString();
                char[] aranan = {'@'};
                int i = Metin.IndexOfAny(aranan);
                string tur = Metin.Substring(i + 1);
               
                if (tur  == "hotmail.com")
               {
                   ddlEmailGuncelleProviders.SelectedValue = (1).ToString();
               }
               else if (tur == "gmail.com")
               {
                   ddlEmailGuncelleProviders.SelectedValue = (3).ToString();
               }
               else if (tur == "medoc.com.tr")
               {
                   ddlEmailGuncelleProviders.SelectedValue = (2).ToString();
               }
                else if (tur == "yahoo.com")
                {
                    ddlEmailGuncelleProviders.SelectedValue = (4).ToString();
                }

               
                TxtboxtelGuncelle.Text = drPersonel["tel"].ToString();
                TxtboxdogumtarihiGuncelle.Text = drPersonel["dogumTarihi"].ToString();
                Pozisyon();
                ddlPozisyonGuncelle.SelectedValue = drPersonel["bolumID"].ToString();

               
            }
        }
       
        void Pozisyon()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Bolum";
            DataTable dtPozisyon = klas.GetDataTable(cmd);
            ddlPozisyonGuncelle.DataTextField = "bolumAdi";
            ddlPozisyonGuncelle.DataValueField = "bolumID";
            ddlPozisyonGuncelle.DataSource = dtPozisyon;
            ddlPozisyonGuncelle.DataBind();
        }
        void Rol()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Rol";
            DataTable dtRol = klas.GetDataTable(cmd);
            ddlRolGuncelle.DataTextField = "Adi";
            ddlRolGuncelle.DataValueField = "RolID";
            ddlRolGuncelle.DataSource = dtRol;
            ddlRolGuncelle.DataBind();
        }
        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Personel where personelID=@personelID";

            cmd.Parameters.Add("@personelID", personelid);

            DataRow drper = klas.GetDataRow(cmd);
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "select * from Personel where email='" + TxtboxepostaGuncelle.Text + "@" + ddlEmailGuncelleProviders.SelectedItem + "' and email !='" + drper["email"].ToString() + "'";
            DataRow drpersonel = klas.GetDataRow(cmd1);
            if (drpersonel == null)
            {
                //if (drper["sifre"].ToString() == MD5Olustur(TxtboxEskiSifreGuncelle.Text))
                if (tbYeniSifreGuncelle.Text.Trim() == tbTekrarYeniSifreGuncelle.Text.Trim())
                {
                    if (drper["sifre"].ToString() == MD5Olustur(TxtboxEskiSifreGuncelle.Text.Trim()) || Session["isAdmin"].ToString() == "True")
                    {
                        SqlConnection baglanti = klas.baglan();
                        SqlCommand cmd3 = new SqlCommand("update Personel set bolumID=@bolumID, RolID=@RolID ,ad=@ad, soyad=@soyad, email=@email, tel=@tel, dogumTarihi=@dogumTarihi " + ((Session["isAdmin"].ToString() == "True" && Session["personelID"].ToString() != personelid) || tbYeniSifreGuncelle.Text.Trim() == "" ? "" : ", sifre=@sifre ") + " where personelID=" + personelid, baglanti);
 
                        cmd3.Parameters.AddWithValue("@RolID", ddlRolGuncelle.SelectedValue);
                        cmd3.Parameters.AddWithValue("@bolumID", ddlPozisyonGuncelle.SelectedValue);
                        cmd3.Parameters.AddWithValue("@email", TxtboxepostaGuncelle.Text.Trim() + "@" + ddlEmailGuncelleProviders.SelectedItem);
                        cmd3.Parameters.AddWithValue("@ad", TxtboxAdGuncelle.Text.Trim());
                        //cmd3.Parameters.AddWithValue("@sifre", MD5Olustur(TxtboxYeniSifreGuncelle.Text));
                        if (tbTekrarYeniSifreGuncelle.Text != null && tbYeniSifreGuncelle.Text != null)
                        {
                           
                            if (Session["isAdmin"].ToString() != "True" || Session["personelID"].ToString() == personelid)
                            {
                                cmd3.Parameters.AddWithValue("@sifre", MD5Olustur(tbYeniSifreGuncelle.Text.Trim()));
                            }
                           
                        }


                        cmd3.Parameters.AddWithValue("@soyad", TxtboxsoyadGuncelle.Text.Trim());
                        cmd3.Parameters.AddWithValue("@tel", TxtboxtelGuncelle.Text);
                        cmd3.Parameters.AddWithValue("@dogumTarihi", Convert.ToDateTime(TxtboxdogumtarihiGuncelle.Text));

                        cmd3.ExecuteNonQuery();

                        AlertCustom.ShowCustom(this.Page, "Güncelleme İşlemi Başarılı..");
                    }
                    else
                    {
                        lblEmailKontrol.Text = "Eski Şifreniz Uyuşmuyor..!";
                    }
                }
                else
                {
                    lblEmailKontrol.Text = "Yeni Şifreleriniz Uyuşmuyor..!";
                }
            }
            else
            {
                lblEmailKontrol.Text = "Bu E-Posta Kullanılmamaktadır ..!";
            }

        }
       
        protected void btnSifreGonder_Click(object sender, EventArgs e)
        {
            string sifre = RastgeleUret();
            string sifreMD5 = MD5Olustur(sifre);
            SqlConnection baglantii = klas.baglan();
            SqlCommand cmd4 = new SqlCommand("update Personel set  sifre=@sifre  where personelID="+personelid);
            cmd4.Connection = baglantii;

            cmd4.Parameters.AddWithValue("@sifre", sifreMD5);
            cmd4.ExecuteNonQuery();
            SendMail("Personel Kayıt Sistemi Yeni Giriş Şifreniz", "Mail içeriği ve Yeni Şifreniz = " + sifre, TxtboxepostaGuncelle.Text + "@" + ddlEmailGuncelleProviders.SelectedItem);
            AlertCustom.ShowCustom(this.Page, "Yeni şifre E-Posta adresine gönderilmiştir.");
        }
        public static void SendMail(string konu, string strBody, string kime)
        {
            string mailAdres = ConfigurationManager.AppSettings["EMailAdres"].ToString();
            string mailSifre = ConfigurationManager.AppSettings["Password"].ToString();

            //string Notifications = ConfigurationManager.AppSettings["Notifications"].ToString();
            //if (Notifications == "ON")
            //{
            string ssl = ConfigurationManager.AppSettings["SSL"].ToString();

            System.Net.Mail.MailMessage MyMailMessage = new System.Net.Mail.MailMessage(mailAdres, kime, konu, strBody);
            MyMailMessage.IsBodyHtml = true;
            int MailPortNumber = Convert.ToInt32(ConfigurationManager.AppSettings["MailPortNumber"].ToString());//587
            string MailURL = ConfigurationManager.AppSettings["MailURL"].ToString();//
            System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential(mailAdres, mailSifre);
            System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient(MailURL, MailPortNumber);
            if (ssl == "Enabled")
                mailClient.EnableSsl = true;
            else if (ssl == "Disabled")
                mailClient.EnableSsl = false;

            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = mailAuthentication;
            mailClient.Send(MyMailMessage);

            //}
        }
        public string MD5Olustur(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        string RastgeleUret()
        {
            Random rnd = new System.Random(unchecked((int)DateTime.Now.Ticks));
            string ret = "";
            for (int i = 0; i < 7; i++)
            {
                if (i == 0 || i == 5 || i == 6)
                {
                    ret += randkh(rnd);
                }
                else if (i == 1 || i == 4)
                {
                    ret += randsayi(rnd);
                }
                else if (i == 2)
                {
                    ret += randbh(rnd);
                }
                else if (i == 3)
                {
                    ret += randnok(rnd);
                }


            }

            // ***************  şifre yi burdan email olarak gönder  ****************

            return ret;

            //return MD5Olustur(ret);
        }

        const string sayi = "0123456789";
        char randsayi(Random rnd)
        {
            return sayi[rnd.Next(sayi.Length)];
        }

        const string bh = "ABCDEFGHIJKLMNOPRSTUVYZ";
        char randbh(Random rnd)
        {
            return bh[rnd.Next(bh.Length)];
        }

        const string kh = "abcdefghijklmnoprstuvyz";
        char randkh(Random rnd)
        {
            return kh[rnd.Next(nok.Length)];
        }

        const string nok = "%&+@?!$#";
        char randnok(Random rnd)
        {
            return nok[rnd.Next(nok.Length)];
        }

        protected void lbAdminlik_Click(object sender, EventArgs e)
        {
            SqlConnection bgln = klas.baglan();
            SqlCommand cmdbgln = new SqlCommand("update Personel set  isAdmin=@isAdmin  where personelID=" + personelid);
            cmdbgln.Connection = bgln;

            if (lbAdminlik.Text == "<span class=\"glyphicon glyphicon-ok-circle\"></span>Onayla")
            {
                lbAdminlik.Text = "<span class=\"glyphicon glyphicon-remove-circle\"></span>Onayı Kaldır";
                cmdbgln.Parameters.AddWithValue("isAdmin",true);
              
            }
            else
            {
                lbAdminlik.Text = "<span class=\"glyphicon glyphicon-ok-circle\"></span>Onayla";
                cmdbgln.Parameters.AddWithValue("isAdmin", false);
            }
            cmdbgln.ExecuteNonQuery();
        }
    }
}