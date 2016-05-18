using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace PTS2
{
    public partial class Default : System.Web.UI.Page
    {
        metodlar klas = new metodlar();

        protected void Page_Load(object sender, EventArgs e)
        {
           
            LiteralSifreUnuttum.Text = "<a href=\"SifremiUnuttum.aspx\"><span class=\"glyphicon glyphicon-info-sign\"></span> Şifremi Unuttum.!</a>";
           
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

        protected void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {


                SqlCommand cmd = new SqlCommand("select p.*,r.UstRolID from Personel p inner join Rol r on p.RolID=r.RolID where p.email=@email and p.sifre=@sifre");
                cmd.Parameters.AddWithValue("@email", TextBoxemail.Text.Trim() + "@" + ddlEmailProviders.SelectedItem);
                //cmd.Parameters.AddWithValue("@sifre", TextBoxsifre.Text.Trim());
                cmd.Parameters.AddWithValue("sifre", MD5Olustur(TextBoxsifre.Text.Trim()));
                DataRow drgiris = klas.GetDataRow(cmd);
                Session["personelID"] = drgiris["personelID"].ToString();
                Session["RolID"] = drgiris["RolID"].ToString();
                Session["UstRolId"] = drgiris["UstRolId"].ToString();
                Session["isAdmin"] = drgiris["isAdmin"].ToString();

              
            
                
                if (drgiris != null)
                {
                    if (Convert.ToBoolean(drgiris["Aktif"]) == true && Convert.ToBoolean(drgiris["Engel"]) == false)
                    {

                        Session["AdSoyad"] = drgiris["ad"].ToString() + " " + drgiris["soyad"].ToString();
                        string rol = Session["RolID"].ToString();



                        Response.Redirect("YonlendirmeSayfasi.aspx");


                    }

                    if (Convert.ToBoolean(drgiris["Engel"]) == true && Convert.ToBoolean(drgiris["Aktif"]) == true)
                    {
                        lblUyari.Text = "Giriş İçin Yetkili Değilsiniz..!";
                    }

                }
           
                
            }
            catch (Exception)
            {
                
             
                lblUyari.Text = "Şifre veya E-Posta hatalı..!";
            }
        }
 
    }
}