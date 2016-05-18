using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace PTS2
{
    public partial class PozisyonEkle : System.Web.UI.Page
    {

        
        metodlar klas = new metodlar();
        string bolumID = "";
        string islem = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["personelID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            
           bolumID = Request.QueryString["bolumID"];
            islem = Request.QueryString["islem"];
            try
            {
                if (islem == "sil")
                {
                    klas.cmd("delete from Bolum where bolumID=" + bolumID);
                }
            }
            catch (Exception)
            {
                AlertCustom.ShowCustom(this.Page, "Veritabanında Bu Bölümde Olan Kullanıcı var.!");
            }
           

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Bolum";
            DataTable dtBolumler = klas.GetDataTable(cmd);
            rpBolumler.DataSource = dtBolumler;
            rpBolumler.DataBind();
        }

        protected void btnBolumEKle_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = klas.baglan();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection=baglanti;
            cmd.CommandText = "insert into Bolum (bolumAdi) values (@bolumAdi)";
            cmd.Parameters.Add("bolumAdi", TxtboxBolumAdi.Text);
            cmd.ExecuteNonQuery();
            Response.Redirect("BolumEkle.aspx");
            
        }

       
    }
}