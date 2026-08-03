using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.LandDispute.Entry
{
    public partial class ApplicationPreview : System.Web.UI.Page
    {
        string userid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["UserLogIn"] as DataTable;

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    //int roleid = Convert.ToInt32(dt.Rows[0]["RoleID"].ToString());
                    userid = dt.Rows[0]["UserID"].ToString();
                }
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["a_id"] == null)
                {
                    Response.Redirect("~/LandDispute/Entry/EntryPage.aspx");
                    return;
                }

                long applicationId = Convert.ToInt64(Request.QueryString["a_id"]);

                BindApplication(applicationId);
            }
        }

        //Bind Preview
        private void BindApplication(long applicationId)
        {
            // Read all seven step tables

            // Display in Labels/GridView etc.
        }

        //This will reopen the wizard.
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LandDispute/Entry/EntryPage.aspx?a_id=" + Request.QueryString["a_id"]);
        }

        protected void btnFinalSubmit_Click(object sender, EventArgs e)
        {
            long applicationId = Convert.ToInt64(Request.QueryString["a_id"]);

            string applicationNo = GenerateApplicationNo(applicationId);

            lblApplicationNo.Text = "Application Number : " + applicationNo;

            btnFinalSubmit.Enabled = false;

            btnEdit.Enabled = false;
           
        }

        private string GenerateApplicationNo(long applicationId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conns"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("BS_SP_FinalSubmit", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;
                cmd.Parameters.AddWithValue("@CUUser", SqlDbType.VarChar).Value = userid;

                return cmd.ExecuteScalar().ToString();
            }
        }
    }
}