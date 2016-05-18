using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace PTS2
{
    public partial class SifremiUnuttum : System.Web.UI.Page
    {
        metodlar klas = new metodlar();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        
        protected void btnSifreGonder_Click(object sender, EventArgs e)
        {


            string sifre = RastgeleUret();
            string sifreMD5 = MD5Olustur(sifre);
            SqlConnection baglanti = klas.baglan();
 
            // textboxdaki yazılı emailin veritabanında olup olmadığını kontrol eder
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Personel where email=@email";
            cmd.Connection = baglanti;
            cmd.Parameters.AddWithValue("@email", txtboxSifreGonder.Text.Trim() + "@" + ddlEmail.SelectedItem);
            SqlDataReader dr = cmd.ExecuteReader();

 
            if (dr.Read())
            {
                // textboxdaki emaili olan personelin sifresini güncelle
                SqlConnection baglanti2 = klas.baglan();
                SqlCommand cmd1 = new SqlCommand("update Personel set  sifre=@sifre  where email = @email");
                cmd1.Connection = baglanti2;
                cmd1.Parameters.AddWithValue("@sifre", sifreMD5);
                cmd1.Parameters.AddWithValue("@email", (txtboxSifreGonder.Text.Trim() + "@" + ddlEmail.SelectedItem));
                cmd1.ExecuteNonQuery();

                SendMail("Personel Kayıt Sistemi Giriş Şifreniz", "Mail içeriği ve Şifre = " + sifre, (txtboxSifreGonder.Text.Trim() + "@" + ddlEmail.SelectedItem));
                AlertCustom.ShowCustom(this.Page, "Yeni Şifreniz Mail Adresinize Gönderilmiştir");

            }
            else
                AlertCustom.ShowCustom(this.Page, "Böyle Bir  Mail Adresi Bulunmamaktadır.!");

            temizle(Pnl);
        }
        public void temizle(Panel PNL)
        {
            foreach (Control ctrl in Pnl.Controls)
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
                if (i == 1 || i == 4)
                {
                    ret += randsayi(rnd);
                }
                if (i == 2)
                {
                    ret += randbh(rnd);
                }
                if (i == 3)
                {
                    ret += randnok(rnd);
                }


            }
 
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
    }
}