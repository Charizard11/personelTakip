using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace PTS2
{
    public partial class YonlendirmeSayfası : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["personelID"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            HtmlMeta meta = new HtmlMeta();
            meta.HttpEquiv = "Refresh";
            string rol = Session["RolID"].ToString();
            
             if (Session["isAdmin"].ToString() == "True")
            {
                meta.Content = "1;url=admin.aspx";
            }
            else
                meta.Content = "1;url=IzinListesi.aspx";
 
            this.Page.Controls.Add(meta);
        }
    }
}