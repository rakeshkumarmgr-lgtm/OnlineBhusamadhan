using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.LandDispute.Entry
{
    public partial class ApplicationPreview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            string applicationNo = "";

            using (SqlConnection con =  new SqlConnection(ConfigurationManager.ConnectionStrings["conns"].ConnectionString))
            {
                con.Open();

                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    //----------------------------------------------------
                    // Get Next Running Number
                    //----------------------------------------------------

                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = con;

                    cmd.Transaction = tran;

                    cmd.CommandText =  @"SELECT ISNULL(MAX(CAST(RIGHT(ApplicationNo,5) AS INT)),0)+1  FROM BS_Matter_Registration  WHERE YEAR(Created_Date)=YEAR(GETDATE())  AND ApplicationNo IS NOT NULL";

                    int nextNo =
                        Convert.ToInt32(cmd.ExecuteScalar());

                    //----------------------------------------------------
                    // Generate Number
                    //----------------------------------------------------

                    applicationNo = "LD"+ DateTime.Now.ToString("yy") + nextNo.ToString("D5");

                    //----------------------------------------------------
                    // Update Master
                    //----------------------------------------------------

                    cmd.Parameters.Clear();

                    cmd.CommandText =
                    @"UPDATE BS_Matter_Registration  SET  ApplicationNo=@ApplicationNo,  CurrentStep=7,  IsFinalSubmit=1  WHERE a_id=@a_id";

                    cmd.Parameters.AddWithValue("@ApplicationNo", applicationNo);

                    cmd.Parameters.AddWithValue("@a_id", applicationId);

                    cmd.ExecuteNonQuery();

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }

            return applicationNo;
        }
    }
}