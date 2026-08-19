using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UserControl ucMenu = (UserControl)LoadControl("~/UC/Menu.ascx");

            phMenu.Controls.Clear();

            phMenu.Controls.Add(ucMenu);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["UserLogIn"] as DataTable;

            if (dt == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            else
            {
                lblUserName.Text = " <i class='fas fa-map-marker-alt mr-2'></i> User Name : " + dt.Rows[0]["UserName"].ToString();
                lblUserID.Text = "<i class='fas fa-user-tag mr-2'></i> User ID : " + dt.Rows[0]["UserID"].ToString();
                lblUseridSidebar.Text = dt.Rows[0]["UserID"].ToString();
                lblName.Text = dt.Rows[0]["Name"].ToString();
                lblDist.Text = "<i class='fas fa-map-marker-alt mr-2'></i> District : " + dt.Rows[0]["DISTRICTNAME"].ToString();
                lblBlock.Text = "<i class='fas fa-map-marker-alt mr-2'></i> Block : " + dt.Rows[0]["BlockName"].ToString();
            }



            lblDate.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy"));

            //phMenu.Controls.Clear();

            //UserControl ucMenu = (UserControl)LoadControl("~/UC/Menu.ascx");

            //phMenu.Controls.Add(ucMenu);

        }

      

    }
}