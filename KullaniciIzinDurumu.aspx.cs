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
    public partial class KullaniciIzinListesi : System.Web.UI.Page
    {
        metodlar klas = new metodlar();
        string personelID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["personelID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            personelID =  Session["personelID"].ToString();
            SqlCommand cmd = new SqlCommand("SELECT dbo.Personel.ad,dbo.Personel.soyad, dbo.Bolum.bolumAdi,dbo.Izin.baslamaTarihi, dbo.Izin.bitisTarihi, dbo.Izin.izinTuru, dbo.Izin.aciklama, dbo.Izin.durum, dbo.Izin.onaylayan, dbo.Izin.onayTarihi,   dbo.Izin.islemTarihi FROM dbo.Bolum INNER JOIN  dbo.Personel ON dbo.Bolum.bolumID = dbo.Personel.bolumID INNER JOIN dbo.Izin ON dbo.Personel.personelID = dbo.Izin.personelID where dbo.Personel.personelID=@personelID");
 
            cmd.Parameters.AddWithValue("@personelID", personelID);
           
                DataTable dt = klas.GetDataTable(cmd);
                dtIzinlerDurumListesi.DataSource = dt;
                dtIzinlerDurumListesi.DataBind();

               
         
            
        }
     
    }
}