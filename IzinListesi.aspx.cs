using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using PTS2.Common;
using System.Web.UI.HtmlControls;

namespace PTS2
{
    public partial class IzinListesi : System.Web.UI.Page
    {
        metodlar klas = new metodlar();

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["personelID"] == null)
            {
                Response.Redirect("Login.aspx");
            }

       

            if (Session["isAdmin"].ToString() == "True")
            {
                 
                btnVisibilty.Visible = true;
                
            }
           
            if (!IsPostBack)
                PersonelGetir(null);



        }
        private void PersonelGetir(List<string> durumListe)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT dbo.Izin.izinID, dbo.Bolum.bolumAdi, dbo.Personel.ad, dbo.Personel.personelID, dbo.Personel.soyad, dbo.Izin.baslamaTarihi,dbo.Izin.aciklama,dbo.Izin.izinTuru, dbo.Izin.bitisTarihi, dbo.Izin.durum, dbo.Izin.islemTarihi, dbo.Izin.onaylayan, dbo.Izin.onayTarihi FROM  dbo.Bolum INNER JOIN dbo.Personel ON dbo.Bolum.bolumID = dbo.Personel.bolumID INNER JOIN dbo.Izin ON dbo.Personel.personelID = dbo.Izin.personelID where dbo.Personel.Aktif=1 and dbo.Personel.RolID IN (";

            //RolId sine göre onaylama yetksi oldugu bölümdeki kişileri getirir..
            string query = string.Empty;

            if (Session["RolId"] != null)
            {

                query = "SELECT dbo.Rol.RolID from dbo.Rol WHERE dbo.Rol.UstRolId >" + Session["UstRolId"].ToString();
                
            }
            else
                Response.Redirect("/Login.aspx");

            SqlCommand cmdRol = new SqlCommand();
            cmdRol.CommandText = query;
            SqlConnection con = klas.baglan();
            cmdRol.Connection = con;
            SqlDataReader dr = cmdRol.ExecuteReader();
            string rolidleri = string.Empty;
            int count = 0;
            while (dr.Read())
            {
                if (dr["RolId"] != DBNull.Value)
                    rolidleri += dr["RolId"] + ",";
                count++;
            }
           
            dr.Close();

            if (count == 0)
            {
                dtIzinlerListesi.DataSource = null;
                dtIzinlerListesi.DataBind();
                return;
            }
            else if (count > 0)
            {
                rolidleri = rolidleri.Substring(0, rolidleri.Length - 1);
            }

            cmd.CommandText += rolidleri + ")";

            if (durumListe != null)
            {
                if (durumListe.Count > 0)
                {
                    cmd.CommandText += " and durum IN (";
                    for (int i = 0; i < durumListe.Count; i++)
                    {
                        cmd.CommandText += durumListe[i] + ",";
                    }
                    cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 1);
                    cmd.CommandText += ")";
                }
            }

            DataTable dtkullanıcı = klas.GetDataTable(cmd);
            dtIzinlerListesi.DataSource = dtkullanıcı;
            dtIzinlerListesi.DataBind();
        }

        protected void btnCikis_Click(object sender, EventArgs e)
        {
            Session.Remove("personelID");
            Response.Redirect("Login.aspx");
        }

        protected void cbReddedilenler_CheckedChanged(object sender, EventArgs e)
        {

            DurumlariAl();
        }
        // Checbodaki seçililere göre getirme olayı
        private void DurumlariAl()
        {
            List<string> durumlar = new List<string>();
            if (cbReddedilenler.Checked)
            {
                durumlar.Add(((int)Enumlar.IzinDurum.REDDEDILDI).ToString());
            }
            if (cbOnaylananlar.Checked)
            {
                durumlar.Add(((int)Enumlar.IzinDurum.ONAYLANDI).ToString());
            }
            if (cbIslemdeOlanlar.Checked)
            {
                durumlar.Add(((int)Enumlar.IzinDurum.ISLEME_ALINDI).ToString());
            }
            PersonelGetir(durumlar);
        }

        protected void cbOnaylananlar_CheckedChanged(object sender, EventArgs e)
        {
            DurumlariAl();
        }

        protected void cbIslemdeOlanlar_CheckedChanged(object sender, EventArgs e)
        {
            DurumlariAl();
        }

        protected void dtIzinlerListesi_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lbOnayla = e.Item.FindControl("lbOnayla") as LinkButton;
                HtmlGenericControl spanOnayla = e.Item.FindControl("spanOnayla") as HtmlGenericControl;
                LinkButton lbReddet = e.Item.FindControl("lbReddet") as LinkButton;
                HtmlGenericControl spanReddet = e.Item.FindControl("spanReddet") as HtmlGenericControl;
                LinkButton lbKaldir = e.Item.FindControl("lbKaldir") as LinkButton;
                HtmlGenericControl spanKaldir = e.Item.FindControl("spanKaldir") as HtmlGenericControl;

                
                int izinID = 0;
                Int16 durum = 0;
                if (DataBinder.Eval(e.Item.DataItem, "izinID") != DBNull.Value)
                    izinID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "izinID"));
                if (DataBinder.Eval(e.Item.DataItem, "durum") != DBNull.Value)
                    durum = Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "durum"));

                //if (Session["RolId"] != null)
                //{
                //    if (Session["RolId"].ToString() == "5")
                //        //e.Item.FindControl("tdOnayla").Attributes.Add("style", "display:none");
                //        //e.Item.FindControl("tdOnayla").Visible = false;
                    

                //}
                if (spanOnayla != null || spanReddet != null)
                {
                    if (durum == 2)
                    {
                        lbOnayla.Visible = false;
                        lbReddet.Visible = false;
                        lbKaldir.Visible = true;
                      
                        lbKaldir.Text = "<span class=\"glyphicon glyphicon-remove\"></span>Onayı Kaldır";


                    }
                    else if (durum == 3)
                    {
                        lbOnayla.Visible = false;
                        lbReddet.Visible = false;
                        lbKaldir.Visible = true;
                      
                        lbKaldir.Text = "<span class=\"glyphicon glyphicon-remove\"></span>Red Kaldır";
                    }
                    else
                    {
                         
                            lbOnayla.Visible = true;
                            lbReddet.Visible = true;
                            lbKaldir.Visible = false;
                        
                    }


                }
            }

        }

        protected void dtIzinlerListesi_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }


        protected void lbOnayla_Click(object sender, EventArgs e)
        {
            //if (Session["RolId"].ToString() == "5" )
            //{
            //     AlertCustom.ShowCustom(this.Page, "Yetkili Değilsiniz.!");
            //}
            if (Session["isAdmin"].ToString() == "True")
            {
                AlertCustom.ShowCustom(this.Page, "Yetkili Değilsiniz.!");
            }
            else
            { 
            LinkButton lbOnayla = (LinkButton)(sender);
            int izinID = Convert.ToInt32(lbOnayla.CommandArgument);
            SqlConnection baglanti = klas.baglan();
            SqlCommand cmd3 = new SqlCommand("update Izin set durum=2,onaylayan=@onaylayan,onayTarihi=@onayTarihi  where  izinID=@izinID");
            //SqlCommand cmd3 = new SqlCommand("update Izin set durum=2 where izinID=@izinID");
            cmd3.Connection = baglanti;
            cmd3.Parameters.AddWithValue("@izinID", izinID);
            cmd3.Parameters.AddWithValue("@onaylayan", Session["AdSoyad"].ToString());
            cmd3.Parameters.AddWithValue("@onayTarihi",DateTime.Now);
            cmd3.ExecuteNonQuery();
            lbOnayla.Text = "<span class=\"glyphicon glyphicon-remove\"></span>Onaylandı";
            DurumlariAl();
            PersonelGetir(null);
            }

        }

        protected void lbReddet_Click(object sender, EventArgs e)
        {

            //if (Session["RolId"].ToString() == "5")
            //{
            //    AlertCustom.ShowCustom(this.Page, "Yetkili Değilsiniz.!");
            //}
            if (Session["isAdmin"].ToString() == "True")
            {
                AlertCustom.ShowCustom(this.Page, "Yetkili Değilsiniz.!");
            }
            else
            {
                LinkButton lbOnayla = (LinkButton)(sender);
                int izinID = Convert.ToInt32(lbOnayla.CommandArgument);
                SqlConnection baglanti = klas.baglan();
                SqlCommand cmd3 = new SqlCommand("update Izin set durum=3,onaylayan=@onaylayan,onayTarihi=@onayTarihi  where izinID=@izinID");
                cmd3.Connection = baglanti;
                cmd3.Parameters.AddWithValue("@izinID", izinID);
                cmd3.Parameters.AddWithValue("@onaylayan", Session["AdSoyad"].ToString());
                cmd3.Parameters.AddWithValue("@onayTarihi", DateTime.Now);
                cmd3.ExecuteNonQuery();
                lbOnayla.Text = "<span class=\"glyphicon glyphicon-ok-circle\"></span>Reddedildi";
                DurumlariAl();
            }
        }


        protected void lbKaldir_Click(object sender, EventArgs e)
        {
          
            if (Session["isAdmin"].ToString() == "True")
            {
                AlertCustom.ShowCustom(this.Page, "Yetkili Değilsiniz.!");
            }
            else
            {
                LinkButton lbKaldır = (LinkButton)(sender);
                int izinID = Convert.ToInt32(lbKaldır.CommandArgument);
                SqlConnection baglanti = klas.baglan();
                SqlCommand cmd3 = new SqlCommand("update Izin set durum=1,onaylayan=@onaylayan,onayTarihi=@onayTarihi where izinID=@izinID");
                cmd3.Connection = baglanti;
                cmd3.Parameters.AddWithValue("@izinID", izinID);
                cmd3.Parameters.AddWithValue("@onaylayan", "");
                cmd3.Parameters.AddWithValue("@onayTarihi", DBNull.Value);
                cmd3.ExecuteNonQuery();
                DurumlariAl();
            }
        }

       


    }
}