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
    public partial class VarsayilanRol : System.Web.UI.Page
    {
        metodlar klas = new metodlar();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["personelID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            SqlCommand cmd = new SqlCommand("select * from Rol where isDefault = 1");
            DataRow drrol = klas.GetDataRow(cmd);
            string i = drrol["RolID"].ToString();
            if (Page.IsPostBack == false)
            {
                RolVarsayilan();
                ddlVarsayilanRolGuncelle.SelectedValue = drrol["RolID"].ToString();

                UstRol();
                //ddlUstRolGuncelle.SelectedValue = drrol["UstRolId"].ToString();
                ddlUstRolGuncelle.Items.Insert(0, new ListItem("Seçiniz", ""));
            }
        }
        void RolVarsayilan()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Rol";
            DataTable rol = klas.GetDataTable(cmd);
            ddlVarsayilanRolGuncelle.DataTextField = "Adi";
            ddlVarsayilanRolGuncelle.DataValueField = "RolID";
            ddlVarsayilanRolGuncelle.DataSource = rol;
            ddlVarsayilanRolGuncelle.DataBind();

        }
        void UstRol()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Rol";
            DataTable rol = klas.GetDataTable(cmd);
            ddlUstRolGuncelle.DataTextField = "Adi";
            ddlUstRolGuncelle.DataValueField = "RolID";
            ddlUstRolGuncelle.DataSource = rol;
            ddlUstRolGuncelle.DataBind();

        }
        protected void btnVarsayilanRolGuncelle_Click(object sender, EventArgs e)
        {
            SqlConnection bgl = klas.baglan();
            SqlCommand cmd2 = new SqlCommand("update Rol set isDefault = 0");
            cmd2.Connection = bgl;
            cmd2.ExecuteNonQuery();
            SqlConnection baglanti = klas.baglan();
            SqlCommand cmd1 = new SqlCommand("update Rol set isDefault=1,UstRolId=@UstRolId where RolID=@RolID");
            cmd1.Connection = baglanti;
            cmd1.Parameters.Add("@RolID",ddlVarsayilanRolGuncelle.SelectedValue);
            cmd1.Parameters.Add("@UstRolId",ddlUstRolGuncelle.SelectedValue);
            cmd1.ExecuteNonQuery();
            AlertCustom.ShowCustom(this.Page, "Güncelleme İşlemi Başarılı..");
           
        }
       
    }
}