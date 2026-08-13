using Bhusamadhan.DataAccessLayer.DashboardDAL;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly DashboardDataDAL _dashboardDAL = new DashboardDataDAL();
        int roleid;
        string  userrole;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["UserLogIn"] as DataTable;

            if (dt != null && dt.Rows.Count == 1)
            {
                roleid = Convert.ToInt32(dt.Rows[0]["RoleID"]);
                userrole = dt.Rows[0]["Userrole"].ToString();
               
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
                LoadDashboard();
            }
        }

        private void LoadDashboard()
        {
            DataTable login = Session["UserLogIn"] as DataTable;

            if (login == null || login.Rows.Count == 0)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            DataRow user = login.Rows[0];

            string userRole = Convert.ToString(user["Userrole"]);

            SetDashboardVisibility(userRole);

            DataTable dt = _dashboardDAL.GetDashboardData(
                "HQ",
                GetInt(user["RangeCode"]),
                GetInt(user["Commsionary_Code"]),
                GetInt(user["District_Code"]),
                GetInt(user["Sub_DivCode"]),
                GetInt(user["Block_Code"]),
                GetInt(user["Thana_Code"])
            );

            if (dt != null && dt.Rows.Count > 0)
            {
                BindDashboard(dt.Rows[0]);
            }
            else
            {
                SetDashboardZero();
            }
        }

        private void SetDashboardVisibility(string userRole)
        {
            bool isSHOOPT = string.Equals( userRole, "SHOOPT",  StringComparison.OrdinalIgnoreCase);

            TotalApplication1.Visible = isSHOOPT;
            TotalApplication2.Visible = !isSHOOPT;

            Finalize1.Visible = isSHOOPT;
            Finalize2.Visible = !isSHOOPT;

            UnFinalize1.Visible = isSHOOPT;
            UnFinalize2.Visible = !isSHOOPT;
        }

        private int GetInt(object value)
        {
            int result;
            return int.TryParse(Convert.ToString(value), out result) ? result : 0;
        }

        private void BindDashboard(DataRow row)
        {
            string total = Convert.ToString(row["Total"]);
            string finalize = Convert.ToString(row["Finalize"]);
            string unfinalize = Convert.ToString(row["Unfinalize"]);

            lbltotalapplication1.Text = total;
            lbltotalapplication2.Text = total;

            lblFinalize1.Text = finalize;
            lblFinalize2.Text = finalize;

            lblUnFinalize1.Text = unfinalize;
            lblUnFinalize2.Text = unfinalize;

            lblatiSavedansheel.Text = Convert.ToString(row["atisanvedanasheel"]);
            lblsavedansheel.Text = Convert.ToString(row["sanvedanasheel"]);
            lblsamanya.Text = Convert.ToString(row["saamaany"]);

            lblnispadan.Text = Convert.ToString(row["Nirast"]);
            lblFinaldisposal.Text = Convert.ToString(row["FinalNirast"]);
            lblprakreeyadheen.Text = Convert.ToString(row["Prakriyadhin"]);
            lblmapikenirdharit.Text = Convert.ToString(row["Mapi_Nirdharit"]);
            lblashvikrit.Text = Convert.ToString(row["Ashwikrit"]);
            lblNaylayNilambit.Text = Convert.ToString(row["NayalayNilambit"]);
        }

        private void SetDashboardZero()
        {
            lbltotalapplication1.Text = "0";
            lbltotalapplication2.Text = "0";

            lblFinalize1.Text = "0";
            lblFinalize2.Text = "0";

            lblUnFinalize1.Text = "0";
            lblUnFinalize2.Text = "0";

            lblatiSavedansheel.Text = "0";
            lblsavedansheel.Text = "0";
            lblsamanya.Text = "0";

            lblnispadan.Text = "0";
            lblFinaldisposal.Text = "0";
            lblprakreeyadheen.Text = "0";
            lblmapikenirdharit.Text = "0";
            lblashvikrit.Text = "0";
            lblNaylayNilambit.Text = "0";
        }

    }


}