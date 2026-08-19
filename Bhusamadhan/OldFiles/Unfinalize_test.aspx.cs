using Bhusamadhan.DataAccessLayer.LandDisputeDAL;
using Bhusamadhan.DB;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.LandDispute.Entry
{
    public partial class Unfinalize_test : System.Web.UI.Page
    {
        string thanacode = "";
        string userid = "";
        string userrole = "";
        int roleid;
        int thanaCode;
        DBHelper objDBHelper = new DBHelper();

        private readonly MatterRegistrationDAL _matterDAL = new MatterRegistrationDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["UserLogIn"] as DataTable;

            if (dt != null && dt.Rows.Count == 1)
            {
                roleid = Convert.ToInt32(dt.Rows[0]["RoleID"]);
                userrole = dt.Rows[0]["Userrole"].ToString();
                userid = dt.Rows[0]["UserID"].ToString();
                thanaCode = Convert.ToInt32(dt.Rows[0]["Thana_Code"]);
            }
            else
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindUnfinalizedApplications();
            }


        }

        private void BindUnfinalizedApplications()
        {
            try
            {
                string mobileNo = txtSearch.Text.Trim();

                DataTable dt = _matterDAL.GetUnfinalizedApplications( userid, mobileNo);

                gvUnfinalized.DataSource = dt;
                gvUnfinalized.DataBind();

                lblTotal.Text = "Total : " + dt.Rows.Count;

                if (dt.Rows.Count == 0)
                {
                    lblMsg.Text = string.IsNullOrWhiteSpace(mobileNo)  ? "कोई Unfinalized Application उपलब्ध नहीं है।"  : "इस मोबाइल नंबर से कोई Unfinalized Application नहीं मिला।";
                }
                else
                {
                    lblMsg.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Application list load करने में समस्या हुई।";

                // Log ex
            }
        }

        protected void gvUnfinalized_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUnfinalized.PageIndex = e.NewPageIndex;

            BindUnfinalizedApplications();
        }

        protected void gvUnfinalized_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "ContinueApplication")
                return;

            long applicationId;

            if (!long.TryParse(  e.CommandArgument.ToString(), out applicationId))
            {
                lblMsg.Text = "Invalid application.";
                return;
            }


            // Open selected unfinalized application
            Response.Redirect("~/LandDispute/Entry/EntryPage.aspx?a_id=" + applicationId);
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            gvUnfinalized.PageIndex = 0;

            BindUnfinalizedApplications();
        }
    }
}