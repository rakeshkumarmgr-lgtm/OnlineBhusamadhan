using Bhusamadhan.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.LandDispute.Entry
{
    public partial class SearchAppForMetting : System.Web.UI.Page
    {
        string thanacode = "";
        string userid = "";
        string userrole = "";
        int roleid;
        int thanaCode;
        DBHelper objDBHelper = new DBHelper();

        clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["UserLogIn"] as DataTable;

            if (dt != null && dt.Rows.Count == 1)
            {
                int roleid = Convert.ToInt32(dt.Rows[0]["RoleID"].ToString());
                userid = dt.Rows[0]["UserID"].ToString();
                ddlCommissionary.SelectedValue = dt.Rows[0]["Commsionary_Code"].ToString();
                ddlCommissionary.Enabled = false;

                ddlDistrict.SelectedValue = dt.Rows[0]["District_Code"].ToString();
                ddlDistrict.Enabled = false;

                ddlSubDivision.SelectedValue = dt.Rows[0]["Sub_DivCode"].ToString();
                //ddlSubDivision.Enabled = false;

                ddlBlock.SelectedValue = dt.Rows[0]["Block_Code"].ToString();
                //ddlBlock.Enabled = false;

                ddlPoliceStation.SelectedValue = dt.Rows[0]["Thana_Code"].ToString();
                if (ddlPoliceStation.SelectedValue.Trim() != "0")
                {
                    ddlPoliceStation.Enabled = false;
                }
                thanacode = dt.Rows[0]["Thana_Code"].ToString();
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
                bindMasterData();
            }
        }

        private void bindMasterData()
        {
            bindCommissionary();
            bindDistrict();
            bindSubDivision();
            bindBlock();
            bindPoliceStation();
            bindPanchayat();
            bindVillage();
            bindWard();
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
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void bindDistrict()
        {
            ddlDistrict.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 3));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@CommissionaryCode", Convert.ToInt32(ddlCommissionary.SelectedValue.ToString())));

                DataTable dt = objDBHelper.GetResults("SP_commissionary", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlDistrict.DataSource = dt;
                    ddlDistrict.DataTextField = "DISTRICTNAME";
                    ddlDistrict.DataValueField = "DISTRICTCODE";
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, new ListItem("All", "0"));
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
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void bindSubDivision()
        {
            ddlSubDivision.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 4));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District", Convert.ToInt32(ddlDistrict.SelectedValue.ToString())));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@thana_code", Convert.ToInt32(thanacode)));

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
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void bindBlock()
        {
            ddlBlock.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 5));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString())));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@thana_code", Convert.ToInt32(thanacode)));

                DataTable dt = objDBHelper.GetResults("SP_commissionary", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlBlock.DataSource = dt;
                    ddlBlock.DataTextField = "BlockName";
                    ddlBlock.DataValueField = "BlockCode";
                    ddlBlock.DataBind();
                    ddlBlock.Items.Insert(0, new ListItem("--Select--", "0"));
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
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void bindPanchayat()
        {
            ddlPanchayat.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 6));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@BlockCode", Convert.ToInt32(ddlBlock.SelectedValue.ToString())));
              

                DataTable dt = objDBHelper.GetResults("SP_commissionary", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlPanchayat.DataSource = dt;
                    ddlPanchayat.DataTextField = "PanchayatName";
                    ddlPanchayat.DataValueField = "PanchayatCode";
                    ddlPanchayat.DataBind();
                    ddlPanchayat.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlPanchayat.DataSource = null;
                    ddlPanchayat.DataBind();
                    ddlPanchayat.Items.Insert(0, new ListItem("All", "0"));
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }


        private void bindVillage()
        {
            ddlVillage.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 7));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@BlockCode", Convert.ToInt32(ddlBlock.SelectedValue.ToString())));


                DataTable dt = objDBHelper.GetResults("SP_commissionary", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlVillage.DataSource = dt;
                    ddlVillage.DataTextField = "VILLNAME";
                    ddlVillage.DataValueField = "VILLCODE";
                    ddlVillage.DataBind();
                    ddlVillage.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlVillage.DataSource = null;
                    ddlVillage.DataBind();
                    ddlVillage.Items.Insert(0, new ListItem("All", "0"));
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }


        private void bindPoliceStation()
        {
            ddlPoliceStation.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 9));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString())));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District", Convert.ToInt32(ddlDistrict.SelectedValue.ToString())));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@BlockCode", Convert.ToInt32(ddlBlock.SelectedValue.ToString())));


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
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void bindWard()
        {
            ddlWard.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@QueryType", 8));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@PanchayatCode", Convert.ToInt32(ddlPanchayat.SelectedValue.ToString())));
              

                DataTable dt = objDBHelper.GetResults("SP_commissionary", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlWard.DataSource = dt;
                    ddlWard.DataTextField = "WardName";
                    ddlWard.DataValueField = "WardCode";
                    ddlWard.DataBind();
                    ddlWard.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlWard.DataSource = null;
                    ddlWard.DataBind();
                    ddlWard.Items.Insert(0, new ListItem("All", "0"));
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        protected void ddlCommissionary_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindDistrict();
            bindSubDivision();
            bindBlock();
            bindPoliceStation();
            bindPanchayat();
            bindVillage();
            bindWard();
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindSubDivision();
            bindBlock();
            bindPoliceStation();
            bindPanchayat();
            bindVillage();
            bindWard();
        }

        protected void ddlSubDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindBlock();
           
        }

        protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindPanchayat();
            bindVillage();
        }

        protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindWard();
        }


        //private void bindGridData(int pageIndex)
        //{
        //    try
        //    {
        //        List<SqlParameter> listSQLP = new List<SqlParameter>();

        //        listSQLP.Add(new SqlParameter("@PageIndex", pageIndex));
        //        listSQLP.Add(new SqlParameter("@PageSize", Convert.ToInt32(ddlPageSize.SelectedValue)));

        //        SqlParameter outRecordCount = new SqlParameter("@RecordCount", SqlDbType.Int);
        //        outRecordCount.Direction = ParameterDirection.Output;
        //        listSQLP.Add(outRecordCount);


        //        listSQLP.Add(new SqlParameter("@Comm_Code", Convert.ToInt32(ddlCommissionary.SelectedValue.Trim())));
        //        listSQLP.Add(new SqlParameter("@District_Code", Convert.ToInt32(ddlDistrict.SelectedValue.Trim())));
        //        listSQLP.Add(new SqlParameter("@Sub_DivCode", Convert.ToInt32(ddlSubDivision.SelectedValue.Trim())));
        //        listSQLP.Add(new SqlParameter("@Block_Code", Convert.ToInt32(ddlBlock.SelectedValue.Trim())));
        //        listSQLP.Add(new SqlParameter("@Thana_code", Convert.ToInt32(ddlPoliceStation.SelectedValue.Trim())));
        //        listSQLP.Add(new SqlParameter("@Panchayat_Code", Convert.ToInt32(ddlPanchayat.SelectedValue.Trim())));
        //        listSQLP.Add(new SqlParameter("@Village", Convert.ToInt32(ddlVillage.SelectedValue.Trim())));
        //        listSQLP.Add(new SqlParameter("@WardNo", Convert.ToInt32(ddlWard.SelectedValue.Trim())));

        //        DataTable dt = objDBHelper.GetResults("SP_SearchMatterRegistrationNew", listSQLP, true);

        //        GridView1.DataSource = dt;
        //        GridView1.DataBind();

        //        int recordCount = 0;

        //        if (outRecordCount.Value != DBNull.Value)
        //            recordCount = Convert.ToInt32(outRecordCount.Value);

        //        PopulatePager(recordCount, pageIndex);
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = ex.Message;
        //    }
        //}


        private void bindGridData(int pageIndex)
        {
            DataTable dt = new DataTable();
            SqlParameter[] p = new SqlParameter[12];
            try
            {

                p[0] = new SqlParameter("@QueryType", "2");
                p[1] = new SqlParameter("@Comm_Code", Convert.ToInt32(ddlCommissionary.SelectedValue.Trim()));
                p[2] = new SqlParameter("@District_Code", Convert.ToInt32(ddlDistrict.SelectedValue.Trim()));
                p[3] = new SqlParameter("@Sub_DivCode", Convert.ToInt32(ddlSubDivision.SelectedValue.Trim()));
                p[4] = new SqlParameter("@Block_Code", Convert.ToInt32(ddlBlock.SelectedValue.Trim()));
                p[5] = new SqlParameter("@Thana_code", Convert.ToInt32(ddlPoliceStation.SelectedValue.Trim()));
                p[6] = new SqlParameter("@Panchayat_Code", Convert.ToInt32(ddlPanchayat.SelectedValue.Trim()));
                p[7] = new SqlParameter("@Village", Convert.ToInt32(ddlVillage.SelectedValue.Trim()));
                p[8] = new SqlParameter("@WardNo", Convert.ToInt32(ddlWard.SelectedValue.Trim()));

                p[9] = new SqlParameter("@PageIndex", pageIndex);
                p[10] = new SqlParameter("@PageSize", int.Parse(ddlPageSize.SelectedValue));


                p[11] = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
                p[11].Direction = System.Data.ParameterDirection.Output;

                dt = clsData.GetDataTableWithProc("SP_SearchMatterRegistrationNew", p);
                int totalRecord = 0;
                if (p[11].Value != null)
                {
                    int.TryParse(p[11].Value.ToString(), out totalRecord);
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();


                int recordCount = Convert.ToInt32(p[11].Value);
                this.PopulatePager(recordCount, pageIndex);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        private void PopulatePager(int recordCount, int currentPage)
        {
            double dblPageCount = (double)((decimal)recordCount / decimal.Parse(ddlPageSize.SelectedValue));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                pages.Add(new ListItem("First", "1", currentPage > 1));
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindGridData(1);
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bindGridData(1);
        }

        protected void Page_Changed(object sender, CommandEventArgs e)
        {
            int pageIndex = Convert.ToInt32(e.CommandArgument);

            this.bindGridData(pageIndex);
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton linkbtn = sender as LinkButton;
                string UrlRedirect = linkbtn.CommandArgument;// enc.Encrypt(linkbtn.CommandArgument);
                //Response.Redirect("~/LandDispute/Entry/EntryPage.aspx?RegId=" + UrlRedirect);
                Response.Redirect("~/LandDispute/Entry/AddMettingApplication.aspx?RegId=" + UrlRedirect, false);

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }

        }

        //----------------what is the use of below function used?----------------------------------
        public bool CheckImage(object url)
        {
            if (url.ToString() != "")
            {
                string p = (url.ToString()).Replace("~", "");
                url = "http://localhost:8080" + p;
                try
                {
                    using (var webClient = new WebClient())
                    {
                        byte[] imageBytes = webClient.DownloadData(url.ToString());
                        string imreBase64Data = Convert.ToBase64String(imageBytes);
                        string imgDataURL = string.Format("data:Application/pdf;base64,{0}", imreBase64Data);

                    }
                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
                //return true;
            }


            else
            {
                return false;
            }


        }


        public bool CheckNull(object myValue)
        {
            if (myValue == null || myValue.ToString() == "")
            {
                return false;
            }

            if (myValue is DBNull)
            {
                return false;
            }

            return true;
        }

        //[System.Web.Services.WebMethod()]
        //public static string Getpdf(string url)
        //{
        //    //string urlpath = "";
        //    //try
        //    //{
        //    //    using (var webClient = new WebClient())
        //    //    {
        //    //        byte[] imageBytes = webClient.DownloadData(url);
        //    //        string imreBase64Data = Convert.ToBase64String(imageBytes);
        //    //        string imgDataURL = string.Format("data:Application/pdf;base64,{0}", imreBase64Data);
        //    //        urlpath = imgDataURL;
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    urlpath = ex.Message;
        //    //}
        //    Encryptor enc = new Encryptor(Encryptor.PrivateKey);
        //    string urlpath = "";
        //    string encPathgov = enc.EncodeTo64(url);
        //    encPathgov = Aes256CbcEncrypterApp.Encrypt(encPathgov, System.Web.HttpContext.Current.Session["aes256key"].ToString());
        //    urlpath = encPathgov;
        //    return urlpath;
        //}
    }
}