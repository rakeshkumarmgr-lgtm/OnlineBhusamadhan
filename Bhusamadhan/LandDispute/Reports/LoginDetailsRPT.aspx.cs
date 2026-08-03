using Bhusamadhan.DB;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.LandDispute.Reports
{
    public partial class LoginDetailsRPT : System.Web.UI.Page
    {
        string thanacode = "";
        string userid = "";
        string userrole = "";
        int roleid ;
        int commCode;
        int subDivision;
        int distCode;
        int blockCode;
        int thanaCode;

        DBHelper objDBHelper = new DBHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["UserLogIn"] as DataTable;

            if (dt != null && dt.Rows.Count == 1)
            {
                roleid = Convert.ToInt32(dt.Rows[0]["RoleID"]);
                userrole = dt.Rows[0]["Userrole"].ToString();
                userid = dt.Rows[0]["UserID"].ToString();

                #region
                if (dt.Rows[0]["Commsionary_Code"] != DBNull.Value)
                    commCode = Convert.ToInt32(dt.Rows[0]["Commsionary_Code"]);
                else
                    commCode = 0;
                //------------------------------------------
                if (dt.Rows[0]["District_Code"] != DBNull.Value)
                    distCode = Convert.ToInt32(dt.Rows[0]["District_Code"]);
                else
                    distCode = 0;
                //----------------------------------------------
                if (dt.Rows[0]["Sub_DivCode"] != DBNull.Value)
                    subDivision = Convert.ToInt32(dt.Rows[0]["Sub_DivCode"]);
                else
                    subDivision = 0;
                //-----------------------------------------------
                if (dt.Rows[0]["Block_Code"] != DBNull.Value)
                    blockCode = Convert.ToInt32(dt.Rows[0]["Block_Code"]);
                else
                    blockCode = 0;
                //----------------------------------------------
                if (dt.Rows[0]["Thana_Code"] != DBNull.Value)
                    thanaCode = Convert.ToInt32(dt.Rows[0]["Thana_Code"]);
                else
                    thanaCode = 0;
                #endregion
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
                bindCommissionary();

                if (commCode > 0 && ddlCommissionary.Items.FindByValue(commCode.ToString()) != null)
                {
                    ddlCommissionary.SelectedValue = commCode.ToString();
                    ddlCommissionary.Enabled = false;
                }

                bindDistrict();

                if (distCode > 0 && ddlDistrict.Items.FindByValue(distCode.ToString()) != null)
                {
                    ddlDistrict.SelectedValue = distCode.ToString();
                    ddlDistrict.Enabled = false;
                }

                bindSubDivision();

                if (subDivision > 0 && ddlSubDivision.Items.FindByValue(subDivision.ToString()) != null)
                {
                    ddlSubDivision.SelectedValue = subDivision.ToString();
                    ddlSubDivision.Enabled = false;
                }

                bindBlock();

                if (blockCode > 0 && ddlBlock.Items.FindByValue(blockCode.ToString()) != null)
                {
                    ddlBlock.SelectedValue = blockCode.ToString();
                    ddlBlock.Enabled = false;
                }

                bindPoliceStation();

                if (thanaCode > 0 && ddlPoliceStation.Items.FindByValue(thanaCode.ToString()) != null)
                {
                    ddlPoliceStation.SelectedValue = thanaCode.ToString();
                    ddlPoliceStation.Enabled = false;
                }

                bindRole();

                if (roleid > 0 && ddlRole.Items.FindByValue(roleid.ToString()) != null)
                {
                    ddlRole.SelectedValue = roleid.ToString();
                    ddlRole.Enabled = false;
                }
            }
        }

        private int GetSelectedValue(DropDownList ddl)
        {
            int value;
            return int.TryParse(ddl.SelectedValue, out value) ? value : 0;
        }


        private void bindCommissionary()
        {
            ddlCommissionary.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 1));
              

                DataTable dt = objDBHelper.GetResults("SP_commissionary", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlCommissionary.DataSource = dt;
                    ddlCommissionary.DataTextField = "DIVISIONAME";
                    ddlCommissionary.DataValueField = "DIVISIONCODE";
                    ddlCommissionary.DataBind();
                    ddlCommissionary.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlCommissionary.DataSource = null;

                    ddlCommissionary.DataBind();

                    ddlCommissionary.Items.Insert(0, new ListItem("All", "0"));
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = "<pre>" + ex.ToString() + "</pre>";
            }

        }

     
        private void bindDistrict()
        {
            ddlDistrict.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 3));
                listSQLP.Add(new SqlParameter("@CommissionaryCode",  GetSelectedValue(ddlCommissionary)));
                DataTable dt = objDBHelper.GetResults("SP_commissionary", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlDistrict.DataSource = dt;
                    ddlDistrict.DataTextField = "DISTRICTNAME";
                    ddlDistrict.DataValueField = "DISTRICTCODE";
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, new ListItem("All", "0"));

                    //            lblMsg.Text =
                    //"Items = " + ddlDistrict.Items.Count +
                    //"<br/>Text = " + ddlDistrict.Items[0].Text +
                    //"<br/>Value = " + ddlDistrict.Items[0].Value;
                }
                else
                {
                    ddlDistrict.DataSource = null;

                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, new ListItem("All", "0"));
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = "<pre>" + ex.ToString() + "</pre>";
            }

        }

     
        private void bindSubDivision()
        {
            ddlSubDivision.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 4));
                listSQLP.Add(new SqlParameter("@District", GetSelectedValue(ddlDistrict)));
                DataTable dt = objDBHelper.GetResults("SP_commissionary", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlSubDivision.DataSource = dt;
                    ddlSubDivision.DataTextField = "Sd_Name_En";
                    ddlSubDivision.DataValueField = "Sd_Code2";
                    ddlSubDivision.DataBind();
                    ddlSubDivision.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlSubDivision.DataSource = null;

                    ddlSubDivision.DataBind();
                    ddlSubDivision.Items.Insert(0, new ListItem("All", "0"));
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = "<pre>" + ex.ToString() + "</pre>";
            }

        }

        private void bindBlock()
        {
            ddlBlock.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType",5));
                listSQLP.Add(new SqlParameter("@SubDivision", GetSelectedValue(ddlSubDivision)));
                DataTable dt = objDBHelper.GetResults("SP_commissionary", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlBlock.DataSource = dt;
                    ddlBlock.DataTextField = "BlockName";
                    ddlBlock.DataValueField = "BlockCode";
                    ddlBlock.DataBind();
                    ddlBlock.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlBlock.DataSource = null;

                    ddlBlock.DataBind();

                    ddlBlock.Items.Insert(0, new ListItem("All", "0"));
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = "<pre>" + ex.ToString() + "</pre>";
            }

        }

     
        private void bindPoliceStation()
        {
            ddlPoliceStation.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType",9));
                listSQLP.Add(new SqlParameter("@SubDivision", GetSelectedValue(ddlSubDivision)));
                listSQLP.Add(new SqlParameter("@District", GetSelectedValue(ddlDistrict)));
                listSQLP.Add(new SqlParameter("@BlockCode", GetSelectedValue(ddlBlock)));

                DataTable dt = objDBHelper.GetResults("SP_commissionary", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlPoliceStation.DataSource = dt;
                    ddlPoliceStation.DataTextField = "Police_Station";
                    ddlPoliceStation.DataValueField = "PS_Code";
                    ddlPoliceStation.DataBind();
                    ddlPoliceStation.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlPoliceStation.DataSource = null;

                    ddlPoliceStation.DataBind();
                    ddlPoliceStation.Items.Insert(0, new ListItem("All", "0"));
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = "<pre>" + ex.ToString() + "</pre>";
            }

        }

      
        private void bindRole()
        {
            ddlRole.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", Convert.ToInt32("9")));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString())));
                
                DataTable dt = objDBHelper.GetResults("Select Id,RoleDesc From mst_Role  WHERE ID NOT IN(13,1,2) order by ID", listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlRole.DataSource = dt;
                    ddlRole.DataTextField = "RoleDesc";
                    ddlRole.DataValueField = "ID";
                    ddlRole.DataBind();
                    ddlRole.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlRole.DataSource = null;

                    ddlRole.DataBind();
                    ddlRole.Items.Insert(0, new ListItem("All", "0"));
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = "<pre>" + ex.ToString() + "</pre>";
            }

        }

        protected void ddlCommissionary_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindDistrict();
            bindSubDivision();
            bindBlock();
            bindPoliceStation();

        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindSubDivision();
            bindBlock();
            bindPoliceStation();

        }

        protected void ddlSubDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindBlock();
            bindPoliceStation();

        }

        protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bindBlock();
            bindPoliceStation();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            bindGridData();
        }

        private void bindGridData()
        {
            lblMsg.Text = string.Empty;

            try
            {
                rptLoginDetails.DataSource = null;
                rptLoginDetails.DataBind();

                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 1));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Comm_Code", GetSelectedValue(ddlCommissionary)));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", GetSelectedValue(ddlDistrict)));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Sub_DivCode", GetSelectedValue(ddlSubDivision)));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Block_Code", GetSelectedValue(ddlBlock)));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Thana_code", GetSelectedValue(ddlPoliceStation)));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Role", GetSelectedValue(ddlRole)));

                //lblMsg.Text =$"QueryType=1<br/>" +$"Comm={GetSelectedValue(ddlCommissionary)}<br/>" +$"District={GetSelectedValue(ddlDistrict)}<br/>" +$"SubDivision={GetSelectedValue(ddlSubDivision)}<br/>" +$"Block={GetSelectedValue(ddlBlock)}<br/>" +$"Thana={GetSelectedValue(ddlPoliceStation)}<br/>" + $"Role={GetSelectedValue(ddlRole)}";
                DataTable dtGetResult = objDBHelper.GetResults("BS_SP_GetUserLoginDetails", listSQLP, true);
                if (dtGetResult != null && dtGetResult.Rows.Count > 0)
                {
                    Pnldata.Visible = true;
                    rptLoginDetails.DataSource = dtGetResult;
                    rptLoginDetails.DataBind();
                    lblMsg.Text = "";
                    //----------------------------------Header Info-----------------------------------------------------------

                    RepeaterItem headerItem = rptLoginDetails.Controls[0] as RepeaterItem;

                    if (headerItem != null)
                    {
                        System.Web.UI.WebControls.Label lblHeaderInfo = (System.Web.UI.WebControls.Label)headerItem.FindControl("lblHeaderInfo");

                        if (lblHeaderInfo != null)
                        {
                            lblHeaderInfo.Text ="Division : " + ddlCommissionary.SelectedItem.Text +"  ||  District : " + ddlDistrict.SelectedItem.Text +  "  ||  Block : " + ddlBlock.SelectedItem.Text + "  ||  Role : " + ddlRole.SelectedItem.Text;
                        }
                    }
                }
                else
                {
                    Pnldata.Visible = false;

                    rptLoginDetails.DataSource = null;
                    rptLoginDetails.DataBind();

                    lblMsg.Text = "<span class='text-danger fw-bold'>No record found.</span>";

                }


            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        protected void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = GetLoginDetailsData();

                if (dt != null && dt.Rows.Count > 0)
                {
                    ExportToExcel(dt);
                }
                else
                {
                    lblMsg.Text = "No record found for export.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private DataTable GetLoginDetailsData()
        {
            List<System.Data.SqlClient.SqlParameter> listSQLP =  new List<System.Data.SqlClient.SqlParameter>();

            listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 1));
            listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Comm_Code", GetSelectedValue(ddlCommissionary)));
            listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", GetSelectedValue(ddlDistrict)));
            listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Sub_DivCode", GetSelectedValue(ddlSubDivision)));
            listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Block_Code", GetSelectedValue(ddlBlock)));
            listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Thana_code", GetSelectedValue(ddlPoliceStation)));
            listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Role", GetSelectedValue(ddlRole)));

            DataTable dt =   objDBHelper.GetResults("BS_SP_GetUserLoginDetails", listSQLP, true);

            return dt;
        }

        private void ExportToExcel(DataTable dt)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("User Login Details");

                // Report Heading
                ws.Cell(1, 1).Value = "User Login Details Report";

                ws.Range(1, 1, 1, 9).Merge();
                ws.Range(1, 1, 1, 9).Style.Font.Bold = true;
                ws.Range(1, 1, 1, 9).Style.Alignment.Horizontal =  XLAlignmentHorizontalValues.Center;


                // Selected Filter Information
                ws.Cell(2, 1).Value =  "Division : " + ddlCommissionary.SelectedItem.Text + " || District : " + ddlDistrict.SelectedItem.Text + " || Block : " + ddlBlock.SelectedItem.Text;


                ws.Range(2, 1, 2, 9).Merge();


                // Start Data from Row 4
                ws.Cell(4, 1).InsertTable(dt);


                ws.Columns().AdjustToContents();


                Response.Clear();
                Response.Buffer = true;

                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                Response.AddHeader( "content-disposition", "attachment;filename=UserLoginDetails.xlsx");


                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);

                    ms.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}