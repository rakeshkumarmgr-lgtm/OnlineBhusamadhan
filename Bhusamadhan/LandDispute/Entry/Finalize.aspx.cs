
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bhusamadhan.DB;
using System.Data;
using System.Data.SqlClient;

namespace Bhusamadhan.LandDispute.Entry
{
   
    public partial class Finalize : System.Web.UI.Page
    {
        string thanacode = "";
        string userid = "";
        string userrole = "";
        int roleid;
        int thanaCode;
        DBHelper objDBHelper = new DBHelper();
        //clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
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
                GV_GetThanaDeatilsPageWise(1);
            }
        }

        private void GV_GetThanaDeatilsPageWise(int pageIndex)
        {
            try
            {
                List<SqlParameter> listSQLP = new List<SqlParameter>();

                listSQLP.Add(new SqlParameter("@PageIndex", pageIndex));
                listSQLP.Add(new SqlParameter("@PageSize", Convert.ToInt32(ddlPageSize.SelectedValue)));

                SqlParameter outRecordCount = new SqlParameter("@RecordCount", SqlDbType.Int);
                outRecordCount.Direction = ParameterDirection.Output;
                listSQLP.Add(outRecordCount);

                listSQLP.Add(new SqlParameter("@Thana_code", thanaCode));
                listSQLP.Add(new SqlParameter("@search", txtSearch.Text.Trim()));
                listSQLP.Add(new SqlParameter("@Matter_Status", Convert.ToInt32(ddlaction.SelectedValue)));

                DataTable dt = objDBHelper.GetResults("usp_GetSaveDataFinalize", listSQLP, true);

                grdMatterRegistration.DataSource = dt;
                grdMatterRegistration.DataBind();

                int recordCount = 0;

                if (outRecordCount.Value != DBNull.Value)
                    recordCount = Convert.ToInt32(outRecordCount.Value);

                PopulatePager(recordCount, pageIndex);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        //private void GV_GetThanaDeatilsPageWise(int pageIndex)
        //{
        //    DataTable dt = new DataTable();
        //    SqlParameter[] p = new SqlParameter[6];
        //    try
        //    {
        //        p[0] = new SqlParameter("@PageIndex", pageIndex);
        //        p[1] = new SqlParameter("@PageSize", int.Parse(ddlPageSize.SelectedValue));


        //        p[2] = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
        //        p[2].Direction = System.Data.ParameterDirection.Output;

        //        p[3] = new SqlParameter("@Thana_code ", thanaCode);
        //        p[4] = new SqlParameter("@search", txtSearch.Text.ToString());
        //        p[5] = new SqlParameter("@Matter_Status", int.Parse(ddlaction.SelectedValue));

        //        dt = clsData.GetDataTableWithProc("usp_GetSaveDataFinalize", p);
        //        int totalRecord = 0;
        //        if (p[2].Value != null)
        //        {
        //            int.TryParse(p[2].Value.ToString(), out totalRecord);
        //        }
        //        grdMatterRegistration.DataSource = dt;
        //        grdMatterRegistration.DataBind();
        //        int recordCount = Convert.ToInt32(p[2].Value);
        //        this.PopulatePager(recordCount, pageIndex);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        private void PopulatePager(int recordCount, int currentPage)
        {
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(ddlPageSize.SelectedValue));

            int pageCount = (int)Math.Ceiling(dblPageCount);

            List<ListItem> pages = new List<ListItem>();

            if (pageCount > 0)
            {
                pages.Add(new ListItem("First", "1", currentPage > 1));

                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }

                pages.Add(new ListItem("Last",  pageCount.ToString(),currentPage < pageCount));
            }

            rptPager.DataSource = pages;
            rptPager.DataBind();
        }

        protected void ddlaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            GV_GetThanaDeatilsPageWise(1);
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GV_GetThanaDeatilsPageWise(1);
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GV_GetThanaDeatilsPageWise(1);
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            try
            {
             
                LinkButton linkbtn = sender as LinkButton;
                string UrlRedirect = linkbtn.CommandArgument;// enc.Encrypt(linkbtn.CommandArgument);
                Response.Redirect("~/LandDispute/Entry/EntryPage.aspx?RegId=" + UrlRedirect);

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }

        }

        protected void Page_Changed(object sender, CommandEventArgs e)
        {
            int pageIndex = Convert.ToInt32(e.CommandArgument);

            GV_GetThanaDeatilsPageWise(pageIndex);
        }

    }
}