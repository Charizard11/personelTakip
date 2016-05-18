using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PTS2
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["personelID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            
            
        }

        protected void btnekle_Click(object sender, EventArgs e)
        {

        }

        protected void lbSecureExit_Click(object sender, EventArgs e)
        {
            Session.Remove("PersonelID");
            Response.Redirect("Login.aspx");
        }

        
    }
}