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
    public partial class RolDuzenle : System.Web.UI.Page
    {
        metodlar klas = new metodlar();
        string RolID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["personelID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            RolID = Request.QueryString["RolID"];
            if (Page.IsPostBack == false)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select *  from Rol where RolID = @RolID";
                cmd.Parameters.AddWithValue("@RolID", RolID);
                DataRow drRol = klas.GetDataRow(cmd);
                TxtboxRolAdiDuzenle.Text = drRol["Adi"].ToString();

            }
        }

        protected void btnRolDuzenle_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = klas.baglan();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = baglanti;
            cmd.CommandText = "update Rol set Adi = @Adi where RolID = @RolID";
            cmd.Parameters.AddWithValue("@RolID", RolID);
            cmd.Parameters.Add("@Adi", TxtboxRolAdiDuzenle.Text);
            cmd.ExecuteNonQuery();
            Response.Redirect("RolEkle.aspx");
        }
    }
}