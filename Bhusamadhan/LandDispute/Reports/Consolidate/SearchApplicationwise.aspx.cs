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

namespace Bhusamadhan.LandDispute.Reports.Consolidate
{
    public partial class SearchApplicationwise : System.Web.UI.Page
    {
        
        string userid = "";
        string userrole = "";
        int roleid;
        
        DBHelper objDBHelper = new DBHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["UserLogIn"] as DataTable;

            if (dt != null && dt.Rows.Count == 1)
            {
                roleid = Convert.ToInt32(dt.Rows[0]["RoleID"]);
                userrole = dt.Rows[0]["Userrole"].ToString();
                userid = dt.Rows[0]["UserID"].ToString();

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
                BindFilter(dt.Rows[0]);
            }
        }

        private void BindFilter(DataRow user)
        {
          
            bindRange();

            if (!string.IsNullOrEmpty(user["RangeCode"]?.ToString()))
            {
                ddlRange.SelectedValue = user["RangeCode"].ToString();
                ddlRange.Enabled = false;
            }

          
            bindCommissionary();

            if (!string.IsNullOrEmpty(user["Commsionary_Code"]?.ToString()))
            {
                ddlCommissionary.SelectedValue = user["Commsionary_Code"].ToString();
                ddlCommissionary.Enabled = false;
            }

         
            if (userrole == "DIG")
            {
                divddlRange.Visible = true;
                divLabRange.Visible = true;

                divddlCommissionary.Visible = false;
                divLabCommissionary.Visible = false;
            }
            else
            {
                divddlRange.Visible = false;
                divLabRange.Visible = false;

                divddlCommissionary.Visible = true;
                divLabCommissionary.Visible = true;
            }

         
            bindDistrict();

            if (!string.IsNullOrEmpty(user["District_Code"]?.ToString()))
            {
                ddlDistrict.SelectedValue = user["District_Code"].ToString();
                ddlDistrict.Enabled = false;
            }

          
            bindSubDivision();

            if (!string.IsNullOrEmpty(user["Sub_DivCode"]?.ToString()))
            {
                ddlSubDivision.SelectedValue = user["Sub_DivCode"].ToString();
                ddlSubDivision.Enabled = false;
            }

            bindBlock();

            if (!string.IsNullOrEmpty(user["Block_Code"]?.ToString()))
            {
                ddlBlock.SelectedValue = user["Block_Code"].ToString();
                ddlBlock.Enabled = false;
            }

            bindthana();

            if (!string.IsNullOrEmpty(user["Thana_Code"]?.ToString()))
            {
                ddlThana.SelectedValue = user["Thana_Code"].ToString();
                ddlThana.Enabled = false;
            }
        }

        private void bindRange()
        {
            ddlRange.Items.Clear();
        
            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 0));
                DataTable dtresult = objDBHelper.GetResults("SearchApplicationWise", listSQLP, true);
                if (dtresult.Rows.Count > 0)
                {
                    ddlRange.DataSource = dtresult;
                    ddlRange.DataTextField = "RangeName";
                    ddlRange.DataValueField = "Rangeid";
                    ddlRange.DataBind();
                    ddlRange.Items.Insert(0, new ListItem("All", "0"));

                   
                }
                else
                {
                    ddlRange.DataSource = null;

                    ddlRange.DataBind();

                  
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
            }

        }

        private void bindCommissionary()
        {
            ddlCommissionary.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 1));
                DataTable dtresult = objDBHelper.GetResults("SearchApplicationWise", listSQLP, true);
                if (dtresult.Rows.Count > 0)
                {
                    ddlCommissionary.DataSource = dtresult;
                    ddlCommissionary.DataTextField = "DIVISIONAME";
                    ddlCommissionary.DataValueField = "DIVISIONCODE";
                    ddlCommissionary.DataBind();
                    //ddlCommissionary.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlCommissionary.DataSource = null;

                    ddlCommissionary.DataBind();


                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
            }
        }

        private void bindDistrict()
        {
            ddlDistrict.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 2));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@DivisionCode", Convert.ToInt32(ddlCommissionary.SelectedValue.ToString())));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Rangeid", Convert.ToInt32(ddlRange.SelectedValue.ToString())));

                string query = @"select distinct cd.DISTRICTCODE,cd.DISTRICTNAME from mst_Commissionary_Districts cd         
                                 where cd.DIVISIONCODE=@DivisionCode or RangeCode=@Rangeid  order by cd.DISTRICTNAME";

                DataTable dtresult = objDBHelper.GetResults(query, listSQLP, false);
                if (dtresult.Rows.Count > 0)
                {
                    ddlDistrict.DataSource = dtresult;
                    ddlDistrict.DataTextField = "DISTRICTNAME";
                    ddlDistrict.DataValueField = "DISTRICTCODE";
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlDistrict.DataSource = null;

                    ddlDistrict.DataBind();


                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
            }
        }

        private void bindSubDivision()
        {
            ddlSubDivision.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 3));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@DISTRICTCODE", Convert.ToInt32(ddlDistrict.SelectedValue.ToString())));
                DataTable dtresult = objDBHelper.GetResults("SearchApplicationWise", listSQLP, true);
                if (dtresult.Rows.Count > 0)
                {
                    ddlSubDivision.DataSource = dtresult;
                    ddlSubDivision.DataTextField = "Sd_Name_En";
                    ddlSubDivision.DataValueField = "Sd_Code2";
                    ddlSubDivision.DataBind();
                    ddlSubDivision.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlSubDivision.DataSource = null;
                    ddlSubDivision.DataTextField = "Sd_Name_En";
                    ddlSubDivision.DataValueField = "Sd_Code2";
                    ddlSubDivision.DataBind();
                    ddlSubDivision.Items.Insert(0, new ListItem("All", "0"));


                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
            }
        }

        private void bindBlock()
        {
            ddlBlock.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 4));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@subDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString())));
                listSQLP.Add(new SqlParameter("@subDivision", string.IsNullOrWhiteSpace(ddlSubDivision.SelectedValue) ? "0" : ddlSubDivision.SelectedValue));

                DataTable dtresult = objDBHelper.GetResults("SearchApplicationWise", listSQLP, true);
                if (dtresult.Rows.Count > 0)
                {
                    ddlBlock.DataSource = dtresult;
                    ddlBlock.DataTextField = "BlockName";
                    ddlBlock.DataValueField = "BlockCode";
                    ddlBlock.DataBind();
                    ddlBlock.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlBlock.DataSource = null;
                    ddlBlock.DataTextField = "BlockName";
                    ddlBlock.DataValueField = "BlockCode";
                    ddlBlock.DataBind();
                    ddlBlock.Items.Insert(0, new ListItem("All", "0"));

                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
            }
        }

        private void bindthana()
        {
            ddlThana.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 5));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@DISTRICTCODE", Convert.ToInt32(ddlDistrict.SelectedValue.ToString())));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@subDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString())));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@BlockCode", Convert.ToInt32(ddlBlock.SelectedValue.ToString())));

                listSQLP.Add(new SqlParameter( "@DISTRICTCODE", string.IsNullOrWhiteSpace(ddlDistrict.SelectedValue) ? "0" : ddlDistrict.SelectedValue));

                listSQLP.Add(new SqlParameter( "@subDivision", string.IsNullOrWhiteSpace(ddlSubDivision.SelectedValue) ? "0" : ddlSubDivision.SelectedValue));

                listSQLP.Add(new SqlParameter("@BlockCode", string.IsNullOrWhiteSpace(ddlBlock.SelectedValue)? "0" : ddlBlock.SelectedValue));

                DataTable dtresult = objDBHelper.GetResults("SearchApplicationWise", listSQLP, true);
                if (dtresult.Rows.Count > 0)
                {
                    ddlThana.DataSource = dtresult;
                    ddlThana.DataTextField = "Police_Station";
                    ddlThana.DataValueField = "PS_Code";
                    ddlThana.DataBind();
                    ddlThana.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlThana.DataSource = null;
                    ddlThana.DataTextField = "";
                    ddlThana.DataValueField = "";
                    ddlThana.DataBind();
                    ddlThana.Items.Insert(0, new ListItem("All", "0"));

                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
            }
        }

        protected void ddlCommissionary_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindDistrict();
            bindSubDivision();
            bindBlock();
            bindthana();
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindSubDivision();
            bindBlock();
            bindthana();
        }

        protected void ddlSubDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindBlock();
            bindthana();
        }

        protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindthana();
        }

       

        protected void ddlSearchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            divlblsearchType.Visible = true;
            divtxtsearchType.Visible = true;
            if (ddlSearchby.SelectedValue == "1")
            {
                lblsearchType.Text = "Application No";
                txtsearch.Text = "";
            }
            else if (ddlSearchby.SelectedValue == "2")
            {
                lblsearchType.Text = "Vadi Name";
                txtsearch.Text = "";
            }
            else if (ddlSearchby.SelectedValue == "3")
            {
                lblsearchType.Text = "Prativadi Name";
                txtsearch.Text = "";
            }
            else if (ddlSearchby.SelectedValue == "4")
            {
                lblsearchType.Text = "Mobile Number";
            }
            else if (ddlSearchby.SelectedValue == "5")
            {
                lblsearchType.Text = "Mobile Number";
                txtsearch.Text = "";
            }
            else
            {
                divlblsearchType.Visible = false;
                divtxtsearchType.Visible = false;
                txtsearch.Text = "";
            }
        }

        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            bindGrid();
        }


        private void bindGrid()
        {
            try
            {
                int pageIndex = Convert.ToInt32(ViewState["PageIndex"] ?? "1");
                int pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);

                string searchValue = txtsearch.Text.Trim();
                string searchBy = ddlSearchby.SelectedValue;

                string applicationNo = string.Empty;
                string vadiName = string.Empty;
                string pratiVadiName = string.Empty;
                string vadiMobileNo = string.Empty;
                string pratiVadiMobileNo = string.Empty;

                switch (searchBy)
                {
                    case "1":
                        applicationNo = searchValue;
                        break;

                    case "2":
                        vadiName = searchValue;
                        break;

                    case "3":
                        pratiVadiName = searchValue;
                        break;

                    case "4":
                        vadiMobileNo = searchValue;
                        break;

                    case "5":
                        pratiVadiMobileNo = searchValue;
                        break;
                }

                DateTime? fromDate = null;
                DateTime? toDate = null;

                if (!string.IsNullOrWhiteSpace(txtfrmdate.Text))
                {
                    fromDate = DateTime.ParseExact(txtfrmdate.Text.Trim(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }

                if (!string.IsNullOrWhiteSpace(txtTodate.Text))
                {
                    toDate = DateTime.ParseExact(txtTodate.Text.Trim(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }

                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 6));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@DIVISIONCODE", Convert.ToInt32(ddlCommissionary.SelectedValue.ToString())));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Rangeid", Convert.ToInt32(ddlRange.SelectedValue.ToString())));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@DISTRICTCODE", Convert.ToInt32(ddlDistrict.SelectedValue.ToString())));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@subDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString())));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@BlockCode", Convert.ToInt32(ddlBlock.SelectedValue.ToString())));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@ThanaCode", Convert.ToInt32(ddlThana.SelectedValue.ToString())));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@fromdate", (object)fromDate ?? DBNull.Value));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@ToDate", (object)toDate ?? DBNull.Value));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@ApplicationNo", applicationNo));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@vadi_Name", vadiName));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@pratiVadi_Name", pratiVadiName));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Vadi_MobileNo", vadiMobileNo));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@pratiVadi_MobileNo", pratiVadiMobileNo));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@PageIndex", pageIndex));

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@PageSize", pageSize));

                SqlParameter recordCount = new SqlParameter("@RecordCount", SqlDbType.Int);

                recordCount.Direction = ParameterDirection.Output;

                listSQLP.Add(recordCount);

                DataTable dt = objDBHelper.GetResults("SearchApplicationWise_new", listSQLP, true);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                int totalRecords = 0;

                if (recordCount.Value != null && recordCount.Value != DBNull.Value)
                {
                    totalRecords = Convert.ToInt32(recordCount.Value);
                }

                PopulatePager(totalRecords, pageIndex, pageSize.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            ViewState["PageIndex"] = pageIndex;
            bindGrid();
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton linkbtn = sender as LinkButton;

                if (linkbtn == null || string.IsNullOrWhiteSpace(linkbtn.CommandArgument))
                {
                    lblMsg.Text = "Invalid application ID.";
                    return;
                }

                string encryptedRegId = QueryStringHelper.Encrypt(linkbtn.CommandArgument);

                string strFilePath = "Information.aspx?RegId=" + encryptedRegId;

                ScriptManager.RegisterStartupScript( Page, Page.GetType(), "newWindow", "window.open('" + strFilePath + "', '_blank');",true);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

            setBackColorOfLinkButton();
        }

        protected void setBackColorOfLinkButton()
        {
            foreach (RepeaterItem item in rptPager.Items)
            {
                LinkButton lnkButton = (LinkButton)item.FindControl("lnkPage");

                lnkButton.BackColor = lnkButton.Enabled ? System.Drawing.Color.FromName("#afa8a8ed") : System.Drawing.Color.FromName("#1eb089");
            }
        }

        private void PopulatePager(int recordCount, int currentPage, string pagesize)
        {
            double dblPageCount = (double)((decimal)recordCount / decimal.Parse(pagesize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                int showMax = 10;
                int startPage;
                int endPage;
                if (((pageCount - currentPage) < 10) || ((pageCount - currentPage) == 0))
                {
                    if (pageCount <= 9)
                    {
                        startPage = 1;
                        pages.Add(new ListItem("First", "1", currentPage > 1));
                        int i = 0;
                        for (i = startPage; i <= pageCount; i++)
                        {
                            pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                        }
                        pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
                    }
                    else
                    {
                        startPage = pageCount - 10 + 1;
                        pages.Add(new ListItem("First", "1", currentPage > 1));
                        int i = 0;
                        for (i = startPage; i <= pageCount; i++)
                        {
                            pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                        }
                        pages.Add(new ListItem("Last", pageCount.ToString(), (i - 1) != currentPage));
                    }
                }
                else if (pageCount - currentPage >= 10)
                {
                    startPage = currentPage;
                    endPage = currentPage + showMax - 1;
                    pages.Add(new ListItem("First", "1", currentPage > 1));

                    for (int i = startPage; i <= endPage; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                    pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
                }
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
            setBackColorOfLinkButton();
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                ExportExcel(GridView1);


            }
            catch (Exception ex)
            {

                lblMsg.Text = ex.Message;
            }
        }

        protected void ExportExcel(GridView Gv)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;

                string fileName = DateTime.Now.ToString("ddMMyyHHmmss") + "_ApplicationConsolidateRpt.xls";

                Response.AddHeader("content-disposition", "attachment;filename=" + fileName);

                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "";

                using (StringWriter sw = new StringWriter())
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                   
                    if (Gv.HeaderRow != null)
                    {
                        foreach (TableCell cell in Gv.HeaderRow.Cells)
                        {
                            cell.Style["border-style"] = "solid";
                            cell.Style["border-color"] = "black";
                            cell.Style["background-color"] = "DarkSeaGreen";
                            cell.Style["font-weight"] = "bold";
                            cell.Style["color"] = "black";
                        }
                    }

                    if (Gv.FooterRow != null)
                    {
                        foreach (TableCell cell in Gv.FooterRow.Cells)
                        {
                            cell.Style["border-style"] = "solid";
                            cell.Style["border-color"] = "black";
                            cell.Style["background-color"] = "DarkSeaGreen";
                            cell.Style["font-weight"] = "bold";
                            cell.Style["color"] = "black";
                        }
                    }

                 
                    foreach (GridViewRow row in Gv.Rows)
                    {
                        row.BackColor = System.Drawing.Color.White;

                        foreach (TableCell cell in row.Cells)
                        {
                            cell.Style["border-style"] = "solid";
                            cell.Style["border-color"] = "black";
                            cell.ForeColor = System.Drawing.Color.Black;
                        }
                    }
                 
                    Response.Write( @"<style>TD { mso-number-format:\@; }</style>");
                  
                    string headerText = Server.HtmlEncode(lbltext.Text);

                    Response.Write( "<h3 style='text-align:center;'>" + headerText +"</h3>");

                    Gv.RenderControl(htw);

                    Response.Write(sw.ToString());
                }

                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Required for exporting GridView to Excel
        }

        
    }

}
