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
    public partial class Finalize : System.Web.UI.Page
    {
        string thanacode = "";
        string userid = "";
        string userrole = "";
        int roleid;
        int thanaCode;

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
                BindFinalizedApplications();
            }
        }

        private void BindFinalizedApplications()
        {
            try
            {
                string searchText = txtSearch.Text.Trim();

                long matterStatus = 0;

                if (!string.IsNullOrEmpty(ddlaction.SelectedValue))
                {
                    long.TryParse(  ddlaction.SelectedValue,  out matterStatus);
                }

                DataTable dt = _matterDAL.GetFinalizedApplications(  userid, searchText,  matterStatus);

                gvFinalized.DataSource = dt;
                gvFinalized.DataBind();

                lblTotal.Text = "Total : " + dt.Rows.Count;

                if (dt.Rows.Count == 0)
                {
                    lblMsg.Text = "कोई Finalized Application उपलब्ध नहीं है।";
                }
                else
                {
                    lblMsg.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Finalized Application list load करने में समस्या हुई।";

                // Log ex
            }
        }

        protected void gvFinalized_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFinalized.PageIndex = e.NewPageIndex;

            BindFinalizedApplications();
        }

        protected void ddlaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvFinalized.PageIndex = 0;

            BindFinalizedApplications();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            gvFinalized.PageIndex = 0;

            BindFinalizedApplications();
        }
    }
}