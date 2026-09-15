using Bhusamadhan.DB;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;
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
using System.Xml;

namespace Bhusamadhan.LandDispute.Reports.Consolidate
{
    public partial class ApplicationDistConsolidateDashboard : System.Web.UI.Page
    {
        string userid = "";
        string userrole = "";
        int roleid;
        int divisionCode ;
        int rangeCode ;
        DBHelper objDBHelper = new DBHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = Session["UserLogIn"] as DataTable;

                if (dt == null || dt.Rows.Count != 1)
                {
                    Session.Clear();
                    Session.Abandon();

                    Response.Redirect("~/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

             
                roleid = Convert.ToInt32(dt.Rows[0]["RoleID"]);
                userrole = Convert.ToString(dt.Rows[0]["Userrole"]).Trim();
                userid = Convert.ToString(dt.Rows[0]["UserID"]).Trim();
                divisionCode = dt.Rows[0]["Commsionary_Code"] == DBNull.Value? 0: Convert.ToInt32(dt.Rows[0]["Commsionary_Code"]);

                rangeCode = dt.Rows[0]["RangeCode"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["RangeCode"]);

                if (!IsPostBack)
                {
                    
                    // Initialize navigation ViewState
                    ViewState["lnkClick"] = string.Empty;
                    ViewState["stepBack"] = string.Empty;
                    ViewState["DistrictName"] = string.Empty;
                    ViewState["Block_Name"] = string.Empty;
                    ViewState["Block_Code"] = string.Empty;
                    ViewState["PanchayatName"] = string.Empty;
                    ViewState["ThanaCode"] = string.Empty;

                    if (userrole.Equals("HQ", StringComparison.OrdinalIgnoreCase) || userrole.Equals("ADMHOME", StringComparison.OrdinalIgnoreCase) || userrole.Equals("ADMLR", StringComparison.OrdinalIgnoreCase) || userrole.Equals("ADGLAW", StringComparison.OrdinalIgnoreCase))
                    {
                        ViewState["stepBack"] = "5";

                        bindDivision();
                    }

                  
                    else if (userrole.Equals("COM", StringComparison.OrdinalIgnoreCase))
                    {
                        ViewState["stepBack"] = "4";

                        bindDivision();
                    }

                 
                    else if (userrole.Equals("DIG", StringComparison.OrdinalIgnoreCase))
                    {
                        ViewState["stepBack"] = "4";

                        bindDivision();
                    }

                    else if (userrole.Equals("DPGRO", StringComparison.OrdinalIgnoreCase) || userrole.Equals("DMOPT", StringComparison.OrdinalIgnoreCase) || userrole.Equals("SSPOPT", StringComparison.OrdinalIgnoreCase) || userrole.Equals("ADM", StringComparison.OrdinalIgnoreCase))
                    {
                        ViewState["stepBack"] = "3";

                        string districtCode = Convert.ToString(dt.Rows[0]["District_Code"]);

                        //getCircleWiseRpt(districtCode);
                    }

                    else if (userrole.Equals("SDPGRO", StringComparison.OrdinalIgnoreCase) || userrole.Equals("SDOOPT", StringComparison.OrdinalIgnoreCase) ||  userrole.Equals("DSPOPT", StringComparison.OrdinalIgnoreCase))
                    {
                        ViewState["stepBack"] = "3";

                        string districtCode = Convert.ToString(dt.Rows[0]["District_Code"]);

                        //getCircleWiseRpt(districtCode);
                    }
                    //circle wise login role
                    else if (userrole.Equals("COOPT", StringComparison.OrdinalIgnoreCase))
                    {
                        ViewState["stepBack"] = "2";

                        string blockCode = Convert.ToString(dt.Rows[0]["Block_Code"]);

                        //getThanaWiseRpt(blockCode);
                    }

                    //thana wise login role
                    else if (userrole.Equals("SHOOPT", StringComparison.OrdinalIgnoreCase))
                    {
                        ViewState["stepBack"] = "1";

                        string thanaCode = Convert.ToString(dt.Rows[0]["Thana_Code"]);

                        //getPanchayatWiseRpt(thanaCode);
                    }

                    else
                    {
                        Session.Clear();
                        Session.Abandon();

                        Response.Redirect("~/Login.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.Visible = true;

            }
        }

        public void bindDivision()
        {
            try
            {
                object fromDate = DBNull.Value;
                object toDate = DBNull.Value;

                if (!string.IsNullOrWhiteSpace(txtFromdate.Text))
                {
                    fromDate = Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd");
                }

                if (!string.IsNullOrWhiteSpace(txTodate.Text))
                {
                    toDate = Convert.ToDateTime(txTodate.Text) .ToString("yyyy-MM-dd");
                }

                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 1));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Divisioncode",  divisionCode));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Rangeid", rangeCode));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@FromDate", fromDate));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@ToDate", toDate));

                DataTable dt = objDBHelper.GetResults("Sp_GetApplicationConsolidateDashboard", listSQLP, true);
                if (dt != null && dt.Rows.Count > 0)
                {
                    btn_Export.Visible = true;
                    lblPrintDateforDistrict.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    grd_District.Columns[1].FooterText = "Total :";
                    grd_District.Columns[2].FooterText = dt.AsEnumerable().Sum(x => x.Field<int>("Total")).ToString();
                    grd_District.Columns[3].FooterText = dt.AsEnumerable().Sum(x => x.Field<int>("Finalize")).ToString();
                    grd_District.Columns[4].FooterText = dt.AsEnumerable().Sum(x => x.Field<int>("Unfinalize")).ToString();
                    grd_District.DataSource = dt; grd_District.DataBind();


                }

                else
                {
                    grd_District.DataSource = null;
                    grd_District.DataBind();
                }
            }
            catch (Exception ex)
            {
                grd_District.DataSource = null; 
                grd_District.DataBind(); 
                btn_Export.Visible = false;
                lblMsg.Text = ex.Message;
                lblMsg.Visible = true;
            }

        }


        protected void grd_District_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdCircle_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdThana_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdPanchayats_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        //------------Application No search
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        //-------------------------------------------------

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            //LinkButton linkbtn = sender as LinkButton;
            //string discode = linkbtn.CommandArgument;
            //GetTotal(discode, "District", "0");
            //btnback.Visible = true;
        }

        protected void lnkTotalFinalize_Click(object sender, EventArgs e)
        {
            //LinkButton linkbtn = sender as LinkButton;
            //string discode = linkbtn.CommandArgument;
            //GetTotal(discode, "District", "1");
            //btnback.Visible = true;
        }

        protected void lnkTotalUnfinalize_Click(object sender, EventArgs e)
        {
            //LinkButton linkbtn = sender as LinkButton;
            //string discode = linkbtn.CommandArgument;
            //GetTotal(discode, "District", "2");
            //btnback.Visible = true;
        }

        //-----------------block-------------------

        protected void lnkBlockTotal_Click(object sender, EventArgs e)
        {
            //LinkButton linkbtn = sender as LinkButton;
            //string block = linkbtn.CommandArgument;
            //GetTotal(block, "Block", "0");
            //btnback.Visible = true;
        }

        protected void lnkBlockTotalFinalize_Click(object sender, EventArgs e)
        {
            //LinkButton linkbtn = sender as LinkButton;
            //string block = linkbtn.CommandArgument;
            //GetTotal(block, "Block", "1");
            //btnback.Visible = true;
        }

        protected void lnkBlockTotalUnfinalize_Click(object sender, EventArgs e)
        {
            //LinkButton linkbtn = sender as LinkButton;
            //string block = linkbtn.CommandArgument;
            //GetTotal(block, "Block", "2");
            //btnback.Visible = true;
        }

        //-------------Thana

        protected void lnkThanaTotal_Click(object sender, EventArgs e)
        {
            //LinkButton linkbtn = sender as LinkButton;
            //string thana = linkbtn.CommandArgument;
            //GetTotal(thana, "Thana", "0");
            //btnback.Visible = true;
        }

        protected void lnkThanaTotalFinalize_Click(object sender, EventArgs e)
        {
            //LinkButton linkbtn = sender as LinkButton;
            //string thana = linkbtn.CommandArgument;
            //GetTotal(thana, "Thana", "1");
            //btnback.Visible = true;
        }

        protected void lnkThanaTotalUnfinalize_Click(object sender, EventArgs e)
        {
            //LinkButton linkbtn = sender as LinkButton;
            //string thana = linkbtn.CommandArgument;
            //GetTotal(thana, "Thana", "2");
            //btnback.Visible = true;
        }

        //-------------Panchayat

        protected void lnkPanchayaTotal_Click(object sender, EventArgs e)
        {
            //LinkButton linkbtn = sender as LinkButton;
            //string thana = linkbtn.CommandArgument;
            //GetTotal(thana, "Panchayat", "0");
            //btnback.Visible = true;
        }

        protected void lnkPanchayaTotalFinalize_Click(object sender, EventArgs e)
        {
            //LinkButton linkbtn = sender as LinkButton;
            //string thana = linkbtn.CommandArgument;
            //GetTotal(thana, "Panchayat", "1");
            //btnback.Visible = true;
        }

        protected void lnkPanchayaTotalUnfinalize_Click(object sender, EventArgs e)
        {
            //LinkButton linkbtn = sender as LinkButton;
            //string thana = linkbtn.CommandArgument;
            //GetTotal(thana, "Panchayat", "2");
            //btnback.Visible = true;
        }

        public bool CheckNull(object myValue)
        {
            if (myValue == null)
            {
                return false;
            }

            if (myValue is DBNull)
            {
                return false;
            }
            return true;
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Encryptor enc = new Encryptor(Encryptor.PrivateKey);
            //    LinkButton linkbtn = sender as LinkButton;
            //    string UrlRedirect = enc.Encrypt(linkbtn.CommandArgument);
            //    string strFilePath = "Information.aspx?RegId=" + UrlRedirect;
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('" + strFilePath + "','_blank')", true);
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message.ToString());
            //}

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           
            ViewState["stepBack"] = "";
            ViewState["DistrictName"] = "";
            ViewState["Block_Name"] = "";
            ViewState["Block_Code"] = "";
            ViewState["PanchayatName"] = "";
            ViewState["ThanaCode"] = "";
           
            if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR") //
            {
                ViewState["stepBack"] = "5";
                bindDivision();
            }
          
            else if (Session["Role"].ToString() == "COM")
            {
                ViewState["stepBack"] = "4";
                bindDivision();
            }
           
            else if (Session["Role"].ToString() == "DPGRO" || Session["Role"].ToString() == "DMOPT" || Session["Role"].ToString() == "SSPOPT" || Session["Role"].ToString() == "ADM")
            {
                ViewState["stepBack"] = "3";
                //getCircleWiseRpt(Convert.ToString(Session["District_Code"]));
            }
          
            else if (Session["Role"].ToString() == "SDPGRO" || Session["Role"].ToString() == "SDOOPT" || Session["Role"].ToString() == "DSPOPT")
            {
                ViewState["stepBack"] = "3";
                //getCircleWiseRpt(Convert.ToString(Session["District_Code"]));
            }
            
            else if (Session["Role"].ToString() == "COOPT")
            {
                ViewState["stepBack"] = "2";
                //getThanaWiseRpt(Convert.ToString(Session["Block_Code"]));
            }
           
            else if (Session["Role"].ToString() == "SHOOPT")
            {
                ViewState["stepBack"] = "1";
                //getPanchayatWiseRpt(Convert.ToString(Session["Thana_Code"]));
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btn_Export_Click(object sender, EventArgs e)
        {

        }
    }
}