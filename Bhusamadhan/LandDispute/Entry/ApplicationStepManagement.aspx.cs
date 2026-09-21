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
    public partial class ApplicationStepManagement : System.Web.UI.Page
    {
        string userid = "";
        string userrole = "";
        int roleid;
        DBHelper objDBHelper = new DBHelper();
        ApplicationStepMgmtDAL _dal= new ApplicationStepMgmtDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["UserLogIn"] as DataTable;

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    roleid = Convert.ToInt32(dt.Rows[0]["RoleID"]);
                    userrole = dt.Rows[0]["Userrole"].ToString();
                    userid = dt.Rows[0]["UserID"].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CheckUserPermission();
            }
        }

        private void CheckUserPermission()
        {
          

            int roleId;

            if (!int.TryParse(roleid.ToString(), out roleId))
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (roleid != 17)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           
            try
            {
                lblMessage.Text = "";
                pnlApplication.Visible = false;

                string searchValue = txtSearch.Text.Trim();

                if (string.IsNullOrWhiteSpace(searchValue))
                {
                    ShowMessage( "कृपया Application ID या Application No दर्ज करें।", "danger");

                    return;
                }


                DataTable dt = _dal.SearchApplication(searchValue);

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowMessage("Application नहीं मिला।","warning");

                    return;
                }

                if (dt.Rows.Count > 1)
                {
                    ShowMessage( "एक से अधिक Application मिले। कृपया सही Application No दर्ज करें।","warning");

                    return;
                }


                DataRow row = dt.Rows[0];

                BindApplication(row);

                pnlApplication.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }

        private void BindApplication(DataRow row)
        {
            lblAId.Text = Convert.ToString(row["a_id"]);

            lblApplicationNo.Text = Convert.ToString(row["ApplicationNo"]);

            lblUser.Text = Convert.ToString(row["cuuser"]);

            lblAavedanKiTithi.Text = Convert.ToString(row["AavedanKiTithi"]);

            lblCurrentStep.Text = Convert.ToString(row["CurrentStep"]);

            lblDistrict.Text = Convert.ToString(row["District"]);

            lblBlock.Text =  Convert.ToString(row["Block"]);

            lblPoliceStation.Text = Convert.ToString(row["Police_Station"]);

            lblAreaType.Text = Convert.ToString(row["AreaType"]);

            lblVillage.Text = Convert.ToString(row["Village"]);

            lblRajasvThana.Text = Convert.ToString(row["RajasvThanaSankhya"]);

            lblBhumitype.Text = Convert.ToString(row["Bhumitype_Ka_Prakar"]);

            lblBhumiVivadType.Text = Convert.ToString(row["BhumiVivadType"]);


            // Select existing CurrentStep
            rblSteps.ClearSelection();

            string currentStep = Convert.ToString(row["CurrentStep"]);

            ListItem selected = rblSteps.Items.FindByValue(currentStep);

            if (selected != null)
            {
                selected.Selected = true;
            }
        }

        protected void btnUpdateStep_Click(object sender, EventArgs e)
        {
            try
            {
                if (!pnlApplication.Visible)
                {
                    ShowMessage( "पहले Application खोजें।",  "warning");

                    return;
                }


                if (string.IsNullOrWhiteSpace(lblAId.Text))
                {
                    ShowMessage( "Application ID उपलब्ध नहीं है।", "danger");

                    return;
                }


                int applicationId;

                if (!int.TryParse(  lblAId.Text, out applicationId))
                {
                    ShowMessage( "Invalid Application ID.", "danger");

                    return;
                }


                if (rblSteps.SelectedIndex < 0)
                {
                    ShowMessage( "कृपया नया Step चुनें।","warning");

                    return;
                }


                int newStep;

                if (!int.TryParse(  rblSteps.SelectedValue, out newStep))
                {
                    ShowMessage( "Invalid Step.", "danger");

                    return;
                }


                if (newStep < 1 || newStep > 7)
                {
                    ShowMessage( "Step केवल 1 से 7 के बीच होना चाहिए।","danger");

                    return;
                }


                int currentStep;

                if (!int.TryParse( lblCurrentStep.Text, out currentStep))
                {
                    ShowMessage( "Current Step invalid है।", "danger");

                    return;
                }


                if (currentStep == newStep)
                {
                    ShowMessage("Current Step पहले से ही Step " + newStep + " है।","info");

                    return;
                }



                if (string.IsNullOrWhiteSpace(userid))
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }


                bool result = _dal.UpdateCurrentStep(applicationId, newStep, userid, currentStep);


                if (result)
                {
                    lblCurrentStep.Text = newStep.ToString();

                    ShowMessage(  "Application का CurrentStep सफलतापूर्वक Step " + newStep + " कर दिया गया है।", "success");
                }
                else
                {
                    ShowMessage( "CurrentStep update नहीं हो सका।", "danger");
                }
            }
            catch (Exception ex)
            {
       
                lblMessage.Text = ex.ToString();


            }
        }


        private void ShowMessage( string message, string type)
        {
            lblMessage.Text = "<div class='alert alert-" +  type +  "'>" + Server.HtmlEncode(message) + "</div>";
        }
    }
}
    