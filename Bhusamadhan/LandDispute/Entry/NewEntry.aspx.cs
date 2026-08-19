using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.LandDispute.Entry
{
    public partial class NewEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Remove previously selected/resumed application
                Session.Remove("ApplicationId");

                // Start a completely new application
                Response.Redirect("~/LandDispute/Entry/EntryPage.aspx", false);

                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}