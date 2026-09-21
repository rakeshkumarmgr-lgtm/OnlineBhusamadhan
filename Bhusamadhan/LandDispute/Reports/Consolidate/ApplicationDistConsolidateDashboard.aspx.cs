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
        int subDivisionCode;
        int distCode;
        int blockCode;
        int thanaCode;

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

                subDivisionCode = dt.Rows[0]["Sub_DivCode"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["Sub_DivCode"]);

                distCode = dt.Rows[0]["District_Code"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["District_Code"]);

                blockCode = dt.Rows[0]["Block_Code"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["Block_Code"]);

                thanaCode = dt.Rows[0]["Thana_Code"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["Thana_Code"]);

                if (!IsPostBack)
                {
                    
                   
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

                        //string districtCode = Convert.ToString(dt.Rows[0]["District_Code"]);

                        getCircleWiseRpt(distCode);
                    }

                    else if (userrole.Equals("SDPGRO", StringComparison.OrdinalIgnoreCase) || userrole.Equals("SDOOPT", StringComparison.OrdinalIgnoreCase) ||  userrole.Equals("DSPOPT", StringComparison.OrdinalIgnoreCase))
                    {
                        ViewState["stepBack"] = "3";

                        //string districtCode = Convert.ToString(dt.Rows[0]["District_Code"]);

                        getCircleWiseRpt(distCode);
                    }
                    //circle wise login role
                    else if (userrole.Equals("COOPT", StringComparison.OrdinalIgnoreCase))
                    {
                        ViewState["stepBack"] = "2";

                        //string blockCode = Convert.ToString(dt.Rows[0]["Block_Code"]);

                        getThanaWiseRpt(blockCode);
                    }

                    //thana wise login role
                    else if (userrole.Equals("SHOOPT", StringComparison.OrdinalIgnoreCase))
                    {
                        ViewState["stepBack"] = "1";

                        //string thanaCode = Convert.ToString(dt.Rows[0]["Thana_Code"]);

                        getPanchayatWiseRpt(thanaCode);
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

                pnlDist.Visible = true;
                grd_District.Visible = true;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                Panel_Panchayats.Visible = false;
                grdPanchayats.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

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

        protected void getCircleWiseRpt(int Distcode)
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
                    toDate = Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd");
                }

                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 2));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@DistrictCode", Distcode));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Subdivision", subDivisionCode));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@FromDate", fromDate));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@ToDate", toDate));

                DataTable dt = objDBHelper.GetResults("Sp_GetApplicationConsolidateDashboard", listSQLP, true);
                if (dt != null && dt.Rows.Count > 0)
                {
                    grdCircle.Columns[1].FooterText = "Total :";
                    grdCircle.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                    grdCircle.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                    grdCircle.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Unfinalize")).Sum().ToString();

                    grdCircle.DataSource = dt;
                    grdCircle.DataBind();

                }

                else
                {
                    grdCircle.DataSource = null;
                    grdCircle.DataBind();
                }

                pnlDist.Visible = false;
                grd_District.Visible = false;

                pnlCircle.Visible = true;
                grdCircle.Visible = true;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                Panel_Panchayats.Visible = false;
                grdPanchayats.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;


            }
            catch (Exception ex)
            {
                grdCircle.DataSource = null;
                grdCircle.DataBind();
           
                lblMsg.Text = ex.Message;
                lblMsg.Visible = true;
            }
        }

        protected void getThanaWiseRpt(int blockCode)
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
                    toDate = Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd");
                }

                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 3));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@BlockCode", blockCode));
              
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@FromDate", fromDate));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@ToDate", toDate));

                DataTable dt = objDBHelper.GetResults("Sp_GetApplicationConsolidateDashboard", listSQLP, true);
                if (dt != null && dt.Rows.Count > 0)
                {
                    btn_Export.Visible = true;

                    grdThana.Columns[1].FooterText = "Total :";
                    grdThana.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                    grdThana.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                    grdThana.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Unfinalize")).Sum().ToString();

                    grdThana.DataSource = dt;
                    grdThana.DataBind();

                }

                else
                {
                    grdThana.DataSource = null;
                    grdThana.DataBind();
                }

                pnlDist.Visible = false;
                grd_District.Visible = false;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = true;
                grdThana.Visible = true;

                Panel_Panchayats.Visible = false;
                grdPanchayats.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            catch (Exception ex)
            {
                grdThana.DataSource = null;
                grdThana.DataBind();

                lblMsg.Text = ex.Message;
                lblMsg.Visible = true;
            }
        }

        protected void getPanchayatWiseRpt(int thanaCode)
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
                    toDate = Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd");
                }

                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 4));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@ThanCode", thanaCode));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@FromDate", fromDate));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@ToDate", toDate));

                DataTable dt = objDBHelper.GetResults("Sp_GetApplicationConsolidateDashboard", listSQLP, true);
                lblPrintDateforPanchayat.Text = DateTime.Now.ToString();
                if (dt != null && dt.Rows.Count > 0)
                {
                    btn_Export.Visible = true;

                    grdPanchayats.Columns[1].FooterText = "Total :";


                    grdPanchayats.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                    grdPanchayats.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                    grdPanchayats.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Unfinalize")).Sum().ToString();

                    grdPanchayats.DataSource = dt;
                    grdPanchayats.DataBind();

                }

                else
                {
                    grdPanchayats.DataSource = null;
                    grdPanchayats.DataBind();
                }

                pnlDist.Visible = false;
                grd_District.Visible = false;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                Panel_Panchayats.Visible = true;
                grdPanchayats.Visible = true;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            catch (Exception ex)
            {
                grdPanchayats.DataSource = null;
                grdPanchayats.DataBind();

                lblMsg.Text = ex.Message;
                lblMsg.Visible = true;
            }
        }
        //----------------------------------------------------------
        protected void grd_District_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName != "DistrictClick")
                    return;

                GridViewRow gvr =(GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                Label total = (Label)gvr.FindControl("lblTotal");

                if (total == null || Convert.ToInt32(total.Text) == 0)
                    return;

                string[] values = e.CommandArgument.ToString().Trim().Split(',');

                if (values.Length < 2)
                    return;

                int districtCode = Convert.ToInt32(values[0]);
                string districtName = values[1];

                ViewState["DistrictName"] = districtName;

                string stepBack = Convert.ToString(ViewState["stepBack"]);

                if (stepBack == "5" || stepBack == "4")
                {
                    lbltext.Text = "District Name :- " + districtName;
                }
                else
                {
                    lbltext.Text = "";
                }

                lbltext.Visible = true;
                btnback.Visible = true;

                getCircleWiseRpt(districtCode);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.Visible = true;
            }
        }

        protected void grdCircle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName != "BlockClick")
                    return;

                GridViewRow gvr =(GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                Label total = (Label)gvr.FindControl("lblTotal");

                if (total == null || Convert.ToInt32(total.Text) == 0)
                    return;

                string[] values = e.CommandArgument.ToString().Trim().Split(',');

                if (values.Length < 2)
                    return;

                int blockCode = Convert.ToInt32(values[0]);
                string blockName = values[1];

                ViewState["Block_Name"] = blockName;
                ViewState["BlockCode"] = blockCode;

                string stepBack = Convert.ToString(ViewState["stepBack"]);

                if (stepBack == "5" || stepBack == "4")
                {
                    lbltext.Text = " District Name :- " + Convert.ToString(ViewState["DistrictName"]) + " ,Circle/Block :-" + blockName;
                }
                else if (stepBack == "3")
                {
                    lbltext.Text = "Circle/Block :-" + blockName;
                }
                else
                {
                    lbltext.Text = "";
                }

                lbltext.Visible = true;
                btnback.Visible = true;

                getThanaWiseRpt(blockCode);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.Visible = true;
            }
        }

        protected void grdThana_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName != "PoliceStationClick")
                    return;

                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                Label total = (Label)gvr.FindControl("lblTotal");

                if (total == null || Convert.ToInt32(total.Text) == 0)
                    return;

                string[] values = e.CommandArgument.ToString().Trim().Split(',');

                if (values.Length < 2)
                    return;

                //string thanaCode = values[0];
                int thanaCode = Convert.ToInt32(values[0]);
                string thanaName = values[1];

                ViewState["Thana_Name"] = thanaName;
                ViewState["ThanaCode"] = thanaCode;

                string stepBack = Convert.ToString(ViewState["stepBack"]);

                if (stepBack == "5" || stepBack == "4")
                {
                    lbltext.Text = " District Name :- " + Convert.ToString(ViewState["DistrictName"]) + " ,Circle/Block :-" + Convert.ToString(ViewState["Block_Name"]) +",Thana :- " + thanaName;
                }
                else if (stepBack == "3")
                {
                    lbltext.Text = " Circle/Block :-" + Convert.ToString(ViewState["Block_Name"]) +" ,Thana :- " + thanaName;
                }
                else if (stepBack == "2")
                {
                    lbltext.Text = "Thana :- " + thanaName;
                }
                else
                {
                    lbltext.Text = "";
                }

                lbltext.Visible = true;
                btnback.Visible = true;

                getPanchayatWiseRpt(thanaCode);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.Visible = true;
            }
        }

        protected void grdPanchayats_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName != "PanchayatClick")
                    return;

                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                Label total = (Label)gvr.FindControl("lblTotal");

                if (total == null || Convert.ToInt32(total.Text) == 0)
                    return;

                string[] values = e.CommandArgument.ToString().Trim().Split(',');

                if (values.Length < 2)
                    return;

                int panchayatCode = Convert.ToInt32(values[0]);
                string panchayatName = values[1];

                // Use ViewState ThanaCode if available,
                // otherwise use Session Thana_Code.
                int thanaCode;

                if (ViewState["ThanaCode"] == null || string.IsNullOrWhiteSpace(Convert.ToString(ViewState["ThanaCode"])))
                {
                    thanaCode = Convert.ToInt32(Session["Thana_Code"]);
                }
                else
                {
                    thanaCode = Convert.ToInt32(ViewState["ThanaCode"]);
                }

                ViewState["PanchayatName"] = panchayatName;

                string stepBack = Convert.ToString(ViewState["stepBack"]);

                if (stepBack == "5" || stepBack == "4")
                {
                    lbltext.Text =" District Name :- " + Convert.ToString(ViewState["DistrictName"]) +
                        " ,Circle/Block :-" + Convert.ToString(ViewState["Block_Name"]) +
                        " ,Thana :- " + Convert.ToString(ViewState["Thana_Name"]) +
                        ",Panchayat :- " + panchayatName;
                }
                else if (stepBack == "3")
                {
                    lbltext.Text = "Circle/Block :-" + Convert.ToString(ViewState["Block_Name"]) + "Thana :- " + Convert.ToString(ViewState["Thana_Name"]) +",Panchayat :- " + panchayatName;
                }
                else if (stepBack == "2")
                {
                    lbltext.Text = "Thana :- " + Convert.ToString(ViewState["Thana_Name"]) + ",Panchayat :- " + panchayatName;
                }
                else if (stepBack == "1")
                {
                    lbltext.Text = "Panchayat :- " + panchayatName;
                }
                else
                {
                    lbltext.Text = "";
                }

                lbltext.Visible = true;
                btnback.Visible = true;

                getPanchayatWiseData(thanaCode, panchayatCode);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.Visible = true;
            }
        }

        private void getPanchayatWiseData(int Thanacode, int Panchayatcode)
        {
            try
            {
                
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 5));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@ThanCode", Thanacode));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Type", "Total"));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@PanchayatCode", Panchayatcode));

                DataTable dt = objDBHelper.GetResults("Sp_GetApplicationConsolidateDashboard", listSQLP, true);

                if (dt != null && dt.Rows.Count > 0)
                {
                    btn_Export.Visible = true;
                    Session["mySearchAppData03"] = dt;
                    GridView1.PageIndex = 0;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();


                }

                else
                {
                    Session["mySearchAppData03"] = null;
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
                pnlDist.Visible = false;
                grd_District.Visible = false;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                Panel_Panchayats.Visible = false;
                grdPanchayats.Visible = false;

                Pnlsearch.Visible = true;
                GridView1.Visible = true;


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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dt = Session["mySearchAppData03"] as DataTable;

                if (dt == null)
                    return;

                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.Visible = true;
            }
        }

        //------------Application No search
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton image1 = (ImageButton)e.Row.FindControl("Image1");
                image1.Attributes.Add("onclick", "return fnLinkbutton1('" + image1.ClientID + "')");


                ImageButton image2 = (ImageButton)e.Row.FindControl("Image2");
                image2.Attributes.Add("onclick", "return fnLinkbutton1('" + image2.ClientID + "')");


                ImageButton image3 = (ImageButton)e.Row.FindControl("Image3");
                image3.Attributes.Add("onclick", "return fnLinkbutton1('" + image3.ClientID + "')");


                ImageButton image4 = (ImageButton)e.Row.FindControl("Image4");
                image4.Attributes.Add("onclick", "return fnLinkbutton1('" + image4.ClientID + "')");


                ImageButton image5 = (ImageButton)e.Row.FindControl("Image5");
                image5.Attributes.Add("onclick", "return fnLinkbutton1('" + image5.ClientID + "')");


                ImageButton image6 = (ImageButton)e.Row.FindControl("Image6");
                image6.Attributes.Add("onclick", "return fnLinkbutton1('" + image6.ClientID + "')");
            }
        }

        protected void GetTotal(int code, string gridtype, string final)
        {
            try
            {
                Session["mySearchAppData03"] = null;

                DataTable dt = new DataTable();

                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add( new System.Data.SqlClient.SqlParameter("@QueryType", 5) );

                listSQLP.Add( new System.Data.SqlClient.SqlParameter("@Type", "Total") );

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@final", final) );

                if (gridtype == "District")
                {
                    ViewState["lnkClick"] = "lnkDistrictClick";

                    listSQLP.Add(new System.Data.SqlClient.SqlParameter("@DistrictCode", code));

                    dt = objDBHelper.GetResults("Sp_GetApplicationConsolidateDashboard",listSQLP,true);
                }
                else if (gridtype == "Block")
                {
                    ViewState["lnkClick"] = "lnkBlockClick";

                    listSQLP.Add( new System.Data.SqlClient.SqlParameter("@BlockCode", code));

                    dt = objDBHelper.GetResults( "Sp_GetApplicationConsolidateDashboard",listSQLP,true );
                }
                else if (gridtype == "Thana")
                {
                    ViewState["lnkClick"] = "lnkThanaClick";

                    listSQLP.Add( new System.Data.SqlClient.SqlParameter("@ThanCode", code));

                    dt = objDBHelper.GetResults("Sp_GetApplicationConsolidateDashboard", listSQLP, true );
                }
                else if (gridtype == "Panchayat")
                {
                    ViewState["lnkClick"] = "lnkPanchayatClick";

                    int selectedThanaCode;

                    if (ViewState["ThanaCode"] == null || string.IsNullOrWhiteSpace(Convert.ToString(ViewState["ThanaCode"])))
                    {
                        selectedThanaCode = thanaCode;
                    }
                    else
                    {
                        selectedThanaCode = Convert.ToInt32(ViewState["ThanaCode"]);
                    }

                    listSQLP.Add( new System.Data.SqlClient.SqlParameter( "@ThanCode", selectedThanaCode) );

                    listSQLP.Add( new System.Data.SqlClient.SqlParameter( "@PanchayatCode",code));

                    dt = objDBHelper.GetResults( "Sp_GetApplicationConsolidateDashboard", listSQLP, true );
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    btn_Export.Visible = true;

                    Session["mySearchAppData03"] = dt;

                    GridView1.PageIndex = 0;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    btn_Export.Visible = false;

                    Session["mySearchAppData03"] = null;

                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }

                pnlDist.Visible = false;
                grd_District.Visible = false;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                Panel_Panchayats.Visible = false;
                grdPanchayats.Visible = false;

                Pnlsearch.Visible = true;
                GridView1.Visible = true;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.Visible = true;
            }
        }

        //-------------------------------------------------

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            //LinkButton linkbtn = sender as LinkButton;
            //string discode = linkbtn.CommandArgument;
            //GetTotal(discode, "District", "0");
            //btnback.Visible = true;

            LinkButton linkbtn = sender as LinkButton;

            if (linkbtn == null)
                return;

            int districtCode = Convert.ToInt32(linkbtn.CommandArgument);

            GetTotal(districtCode, "District", "0");

            btnback.Visible = true;
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

            try
            {
                LinkButton linkbtn = sender as LinkButton;

                if (linkbtn == null)
                    return;

                string encryptedRegId = QueryStringHelper.Encrypt(linkbtn.CommandArgument);

                string url = "~/LandDispute/Reports/Consolidate/Information.aspx?RegId=" + encryptedRegId;

                ScriptManager.RegisterStartupScript( Page, Page.GetType(), "newWindow","window.open('" + url + "', '_blank');", true );
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.Visible = true;
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           
            ViewState["stepBack"] = "";
            ViewState["DistrictName"] = "";
            ViewState["Block_Name"] = "";
            ViewState["Block_Code"] = "";
            ViewState["PanchayatName"] = "";
            ViewState["ThanaCode"] = "";

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

                getCircleWiseRpt(distCode);
            }

            else if (userrole.Equals("SDPGRO", StringComparison.OrdinalIgnoreCase) || userrole.Equals("SDOOPT", StringComparison.OrdinalIgnoreCase) || userrole.Equals("DSPOPT", StringComparison.OrdinalIgnoreCase))
            {
                ViewState["stepBack"] = "3";

                getCircleWiseRpt(distCode);
            }
           
            else if (userrole.Equals("COOPT", StringComparison.OrdinalIgnoreCase))
            {
                ViewState["stepBack"] = "2";


                getThanaWiseRpt(blockCode);
            }

         
            else if (userrole.Equals("SHOOPT", StringComparison.OrdinalIgnoreCase))
            {
                ViewState["stepBack"] = "1";

                getPanchayatWiseRpt(thanaCode);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void btn_Export_Click(object sender, EventArgs e)
        {

        }
    }
}