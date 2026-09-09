using Bhusamadhan.DataAccessLayer.LandDisputeDAL;
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
    public partial class SearchAppForMettingCO : System.Web.UI.Page
    {
        string thanacode = "";
        string userid = "";
        string userrole = "";
        string commissionaryCode = "";
        string districtCode = "";
        string subDivisionCode = "";
        string blockCode = "";
        int roleid;
        int thanaCode;
        DBHelper objDBHelper = new DBHelper();
        private readonly MatterRegistrationDAL _matterDAL = new MatterRegistrationDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = Session["UserLogIn"] as DataTable;

                if (dt == null || dt.Rows.Count != 1)
                {
                    Session.Clear();
                    Session.Abandon();
                    Response.Redirect("~/Login.aspx");
                    return;
                }

                userid = Convert.ToString(dt.Rows[0]["UserID"]);

                thanaCode = 0;

                if (!IsPostBack)
                {
                   
                    bindCommissionary();

                    commissionaryCode = Convert.ToString(dt.Rows[0]["Commsionary_Code"]);

                    if (ddlCommissionary.Items.FindByValue(commissionaryCode) != null)
                    {
                        ddlCommissionary.SelectedValue = commissionaryCode;
                    }

                    ddlCommissionary.Enabled = false;

                    bindDistrict();

                    districtCode = Convert.ToString(dt.Rows[0]["District_Code"]);

                    if (ddlDistrict.Items.FindByValue(districtCode) != null)
                    {
                        ddlDistrict.SelectedValue = districtCode;
                    }

                    ddlDistrict.Enabled = false;


                    bindSubDivision();

                   subDivisionCode = Convert.ToString(dt.Rows[0]["Sub_DivCode"]);

                    if (ddlSubDivision.Items.FindByValue(subDivisionCode) != null)
                    {
                        ddlSubDivision.SelectedValue = subDivisionCode;
                    }


                    bindBlock();

                    blockCode = Convert.ToString(dt.Rows[0]["Block_Code"]);

                    if (ddlBlock.Items.FindByValue(blockCode) != null)
                    {
                        ddlBlock.SelectedValue = blockCode;
                    }


                    bindPoliceStation();

                    bindPanchayat();

                    bindVillage();

                    bindWard();

                    BindFinalizedApplicationsForMeeting();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        //private void bindMasterData()
        //{
        //    bindCommissionary();
        //    bindDistrict();
        //    bindSubDivision();
        //    bindBlock();
        //    bindPoliceStation();
        //    bindPanchayat();
        //    bindVillage();
        //    bindWard();
        //}

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
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@CommissionaryCode", commissionaryCode));

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
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District", districtCode));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@thana_code", 0));

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
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@SubDivision", subDivisionCode));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@thana_code", 0));

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
                int subDivision = 0;
                int district = 0;
                int blockCode = 0;

                int.TryParse( ddlSubDivision.SelectedValue, out subDivision);

                int.TryParse( ddlDistrict.SelectedValue, out district);

                int.TryParse( ddlBlock.SelectedValue,out blockCode);

                SqlParameter QueryType = new SqlParameter("@QueryType", 9);

                SqlParameter SubDivision = new SqlParameter("@SubDivision", subDivision);

                SqlParameter District = new SqlParameter("@District", district);

                SqlParameter BlockCode = new SqlParameter("@BlockCode", blockCode);

                DataTable dt = objDBHelper.GetResults( "SP_commissionary", new List<SqlParameter> { QueryType, SubDivision, District, BlockCode }, true);

                ddlPoliceStation.DataSource = dt;
                ddlPoliceStation.DataTextField = "Police_Station";
                ddlPoliceStation.DataValueField = "PS_Code";
                ddlPoliceStation.DataBind();

                ddlPoliceStation.Items.Insert(0, new ListItem("All", "0"));
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
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

        private void BindFinalizedApplicationsForMeeting()
        {
            try
            {

                string searchText = txtSearch.Text.Trim();

                long matterStatus = 0;

                DataTable dt = _matterDAL.GetFinalizedApplicationsForMeeting(userid, searchText, matterStatus);

                gvFinalizedForMeetingCO.DataSource = dt;
                gvFinalizedForMeetingCO.DataBind();

                lblTotal.Text = "Total : " + dt.Rows.Count;

                if (dt.Rows.Count == 0)
                {
                    lblMsg.Text = "कोई Finalized Application उपलब्ध नहीं है।";
                }
                else
                {
                    lblMsg.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                //lblMsg.Text = "Finalized Application list load करने में समस्या हुई।";

                // Log ex
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            BindFinalizedApplicationsForMeeting();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            gvFinalizedForMeetingCO.PageIndex = 0;

            BindFinalizedApplicationsForMeeting();
        }

        protected void gvFinalizedForMeetingCO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFinalizedForMeetingCO.PageIndex = e.NewPageIndex;

            BindFinalizedApplicationsForMeeting();
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton linkbtn = sender as LinkButton;

                if (linkbtn == null)
                    return;

                string applicationId = linkbtn.CommandArgument;

                if (string.IsNullOrWhiteSpace(applicationId))
                {
                    lblMsg.Text = "Application ID not found.";
                    return;
                }

                string encryptedId = QueryStringHelper.Encrypt(applicationId);

                string url = "~/LandDispute/Entry/AddMettingApplication.aspx?RegId=" + encryptedId;

                Response.Redirect(url, false);

                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}