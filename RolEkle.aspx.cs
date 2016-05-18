using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace PTS2
{
    public partial class RolEkle : System.Web.UI.Page
    {
        metodlar klas = new metodlar();
        string RolID = "";
        string islem = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["personelID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            RolID = Request.QueryString["RolID"];
            islem = Request.QueryString["islem"];
            try
            {
                if (islem == "sil")
                {
                    klas.cmd("delete from Rol where RolID=" + RolID);
                     
                }
            }
            catch (Exception)
            {

                AlertCustom.ShowCustom(this.Page, "Veritabanında Bu Rolde Kullanıcı var.!");
            }
           

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Rol";
            DataTable dtRoller = klas.GetDataTable(cmd);
            rpRoller.DataSource = dtRoller;
            rpRoller.DataBind();

            SqlCommand cmm = new SqlCommand("select * from Rol where isDefault = 1");
            DataRow drrol = klas.GetDataRow(cmm);
            string i = drrol["RolID"].ToString();

            if (Page.IsPostBack == false)
            {

                //RolVarsayilan();
                LiteralRolGuncelle.Text = "<a href=\"VarsayilanRol.aspx\"><span class=\"glyphicon glyphicon-edit\"></span> Güncelle</a>";
                //ddlRolVarsayilan.SelectedValue = drrol["RolID"].ToString();

                Rol();
                ddlRol.Items.Insert(0, new ListItem("Seçiniz", ""));
                ddlRol.Items.Insert(1, new ListItem("Üst Rol Yok", "0"));

                
               
            }

        }
        void Rol()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Rol";
            DataTable rol = klas.GetDataTable(cmd);
            ddlRol.DataTextField = "Adi";
            ddlRol.DataValueField = "RolID";
            ddlRol.DataSource = rol;
            ddlRol.DataBind();

        }
       
        protected void btnRolEkle_Click(object sender, EventArgs e)
        {

            

            SqlConnection baglanti = klas.baglan();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = baglanti;
            cmd.CommandText = "insert into Rol (Adi,UstRolId) values (@Adi,@UstRolId)";
            cmd.Parameters.Add("@Adi",txtboxRolEkle.Text);
            cmd.Parameters.Add("@UstRolId", ddlRol.SelectedValue);
            cmd.ExecuteNonQuery();
           
           

            Response.Redirect("RolEkle.aspx");

        }
    }
}