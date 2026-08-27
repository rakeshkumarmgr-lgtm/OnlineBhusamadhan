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
    public partial class AddHoliday : System.Web.UI.Page
    {
        string userid = "";

        DBHelper objDBHelper = new DBHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["UserLogIn"] as DataTable;

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    userid = dt.Rows[0]["UserID"].ToString();
                }
            }

            else
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            if (!Page.IsValid)
            {
                return;
            }
            bool b = false;
            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@HolidayDate",Convert.ToDateTime(txtHolidayDate.Text.Trim())));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Remark", txtRemark.Text.Trim()));

                b = objDBHelper.SetData("insert into tbl_holiday (holiday_date,Remark) values (@HolidayDate,@Remark)", listSQLP, false);

                if (!b)
                {
                    lblMsg.Text = "Unable to add record";
                }
                else
                {
                    lblMsg.Text = "Holiday added successfully";
                    txtHolidayDate.Text = "";
                    txtRemark.Text = "";

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }
    }
}