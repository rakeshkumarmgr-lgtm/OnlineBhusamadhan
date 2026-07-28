using Bhusamadhan.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.LandDispute.Entry
{
    public partial class EntryPage : System.Web.UI.Page
    {
        string thanacode = "";
        string userid = "";
        DBHelper objDBHelper = new DBHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["UserLogIn"] as DataTable;

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    //int roleid = Convert.ToInt32(dt.Rows[0]["RoleID"].ToString());
                    userid = dt.Rows[0]["UserID"].ToString();
                    ddlDistrict.SelectedValue = dt.Rows[0]["District_Code"].ToString();
                    ddlDistrict.Enabled = false;
                    ddlSubdivision.SelectedValue = dt.Rows[0]["Sub_DivCode"].ToString();
                    ddlSubdivision.Enabled = false;
                    ddlBlock.SelectedValue= dt.Rows[0]["Block_Code"].ToString();
                    ddlBlock.Enabled = false;
                    ddlPolice.SelectedValue = dt.Rows[0]["Thana_Code"].ToString();
                    if (ddlPolice.SelectedValue.Trim() != "0")
                    {
                        ddlPolice.Enabled = false;
                    }
                    thanacode= dt.Rows[0]["Thana_Code"].ToString();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

            else
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                ApplicationId = GetDraftApplicationId();

                if (ApplicationId > 0)
                {
                    CurrentStep = GetCurrentStep(ApplicationId);
                }
                else
                {
                    CurrentStep = 1;
                }

                ShowStep(CurrentStep);

                //------Master Bind-----------------
                LoadMasterData();
                //----------------------------------
                //ShowStep(CurrentStep);

                ViewState["vadiDetails"] = vadiDetails();
            }
        }


        private void LoadMasterData()
        {
            //---------Step 1 wadi/pratiwadi--------
            AdharYearsBind();
            BindDist_Wadi_Pratiwadi();
            BindSubDivision_wadi();
            BindSubDivision();
            BindBlock_Wadi();
            BindBlock();
            BindPolice_wadi();
            BindPolice();
            BindPanchyat_Wadi();
            BindPanchyat();
            BindVillage_Wadi();
            BindVillage();
            bindward_Wadi();
            bindward();
            BindVadi_Prativadi_Anya_Type();
            BindVadi_Sanstha_Anya_Type();
            bindDepartment();
            bind_bhumivivad_ki_adyatan_sthiti();// भूमि विवाद की अद्यतन स्थिति  
            bindbumitype();
            bindSarkariBumitype();
            bind_bhumivivad_Type();

            //---------Step 2 pratiwadi--------
            BindSubDivision_Pratiwadi();
            BindBlock_Pratiwadi();
            BindPolice_Prtiwadi();
            BindVillage_Pratiwadi();
            BindPanchyat_Prtiwadi();
            bindward_Pratiwadi();

            //------Step 3--------------

            bindLandUnit();
            bind_khatiyan_Type();// खतियन में किस्म जमीन की विवरणी :

            //------Step 4--------------
            bindLandEvidence();

            //-------Step 6----------------

            BindNyayalaya();
            BindNyayalayaType();
            BindNyayalayaType_dist();
            BindNyayalayaType_SubDivision();
            BindNyayalayaType_Vibhag();
        }


        public long ApplicationId
        {
            get
            {
                if (Session["ApplicationId"] == null)
                    return 0;

                return Convert.ToInt64(Session["ApplicationId"]);
            }
            set
            {
                Session["ApplicationId"] = value;
            }
        }

        private long GetDraftApplicationId()
        {
            long applicationId = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conns"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@" SELECT TOP (1) a_id FROM BS_Matter_Registration WHERE UserID=@UserID  AND  IsFinalSubmit=0 ORDER BY a_id DESC", con);

                cmd.Parameters.AddWithValue("@UserID", userid);

                con.Open();

                object obj = cmd.ExecuteScalar();

                if (obj != null)
                {
                    applicationId = Convert.ToInt64(obj);
                }
            }

            return applicationId;
        }

        private int GetCurrentStep(long applicationId)
        {
            int step = 1;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conns"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@" SELECT ISNULL(CurrentStep,1) FROM BS_Matter_Registration WHERE a_id=@a_id", con);

                cmd.Parameters.AddWithValue("@a_id", applicationId);

                con.Open();

                object obj = cmd.ExecuteScalar();

                if (obj != null && obj != DBNull.Value)
                {
                    step = Convert.ToInt32(obj);
                }
            }

            return step;
        }

        public int CurrentStep
        {
            get
            {
                if (ViewState["CurrentStep"] == null)
                    ViewState["CurrentStep"] = 1;

                return Convert.ToInt32(ViewState["CurrentStep"]);
            }

            set
            {
                ViewState["CurrentStep"] = value;
            }
        }

        private void ShowStep(int step)
        {
            pnlStep1.Visible = false;
            pnlStep2.Visible = false;
            pnlStep3.Visible = false;
            pnlStep4.Visible = false;
            pnlStep5.Visible = false;
            pnlStep6.Visible = false;
            pnlStep7.Visible = false;

            switch (step)
            {
                case 1:
                    pnlStep1.Visible = true;
                    break;

                case 2:
                    pnlStep2.Visible = true;
                    break;

                case 3:
                    pnlStep3.Visible = true;
                    break;

                case 4:
                    pnlStep4.Visible = true;
                    break;

                case 5:
                    pnlStep5.Visible = true;
                    break;

                case 6:
                    pnlStep6.Visible = true;
                    break;

                case 7:
                    pnlStep7.Visible = true;
                    break;
            }

            SetWizard(step);

            btnPrevious.Visible = (step > 1);

            //btnNext.Text = (step == 7) ? "Finish" : "Save & Next";
            if (step == 7)
                btnNext.Text = "Finish";
            else
                btnNext.Text = "Save & Next";
        }

        private void SetWizard(int currentStep)
        {
            System.Web.UI.HtmlControls.HtmlAnchor[] steps =
            {
                    hstep1, hstep2, hstep3, hstep4, hstep5, hstep6, hstep7
            };

            for (int i = 0; i < steps.Length; i++)
            {
                if (i < currentStep - 1)
                {
                    steps[i].Attributes["class"] = "step completed";
                }
                else if (i == currentStep - 1)
                {
                    steps[i].Attributes["class"] = "step current";
                }
                else
                {
                    steps[i].Attributes["class"] = "step disabled";
                }
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CurrentStep > 1)
            {
                CurrentStep--;

                ShowStep(CurrentStep);
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            bool saved = false;

            switch (CurrentStep)
            {
                case 1:

                    if (ApplicationId == 0)
                    {
                        ApplicationId = SaveMatterRegistration();

                        if (ApplicationId == 0)
                            return;
                    }

                    saved = SaveStep1(ApplicationId);

                    if (!saved)
                        return;

                    CurrentStep = 2;

                    UpdateCurrentStep(ApplicationId, CurrentStep);

                    ShowStep(CurrentStep);

                    break;

                case 2:

                    saved = SaveStep2(ApplicationId);

                    if (!saved)
                        return;

                    CurrentStep = 3;

                    UpdateCurrentStep(ApplicationId, CurrentStep);

                    ShowStep(CurrentStep);

                    break;

                case 3:

                    saved = SaveStep3(ApplicationId);

                    if (!saved)
                        return;

                    CurrentStep = 4;

                    UpdateCurrentStep(ApplicationId, CurrentStep);

                    ShowStep(CurrentStep);

                    break;

                case 4:

                    saved = SaveStep4(ApplicationId);

                    if (!saved)
                        return;

                    CurrentStep = 5;

                    UpdateCurrentStep(ApplicationId, CurrentStep);

                    ShowStep(CurrentStep);

                    break;

                case 5:

                    saved = SaveStep5(ApplicationId);

                    if (!saved)
                        return;

                    CurrentStep = 6;

                    UpdateCurrentStep(ApplicationId, CurrentStep);

                    ShowStep(CurrentStep);

                    break;

                case 6:

                    saved = SaveStep6(ApplicationId);

                    if (!saved)
                        return;

                    CurrentStep = 7;

                    UpdateCurrentStep(ApplicationId, CurrentStep);

                    ShowStep(CurrentStep);

                    break;

                case 7:

                    saved = SaveStep7(ApplicationId);

                    if (!saved)
                        return;

                    Response.Redirect("~/LandDispute/Entry/ApplicationPreview.aspx?a_id=" + ApplicationId);

                    break;
            }
        }

        private void UpdateCurrentStep(long applicationId, int step)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conns"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;

                cmd.CommandText = @"UPDATE BS_Matter_Registration SET CurrentStep=@CurrentStep WHERE a_id=@a_id";

                cmd.Parameters.AddWithValue("@CurrentStep", step);

                cmd.Parameters.AddWithValue("@a_id", applicationId);

                cmd.ExecuteNonQuery();
            }
        }

        private long SaveMatterRegistration()
        {
            long aid = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conns"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(@"INSERT INTO BS_Matter_Registration( Created_date,UserID, CurrentStep,IsFinalSubmit) OUTPUT INSERTED.a_id VALUES(GETDATE(), @UserID, 1,0)", con);

                cmd.Parameters.AddWithValue("@UserID", userid);   // Logged-in user id

                aid = Convert.ToInt64(cmd.ExecuteScalar());
            }

            return aid;
        }

        private bool SaveStep1(long applicationId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conns"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;

                cmd.CommandText = @"IF EXISTS(SELECT 1 FROM BS_VadiDetailEntry WHERE a_id=@a_id)
                         UPDATE BS_VadiDetailEntry SET vadi_Name=@vadi_Name WHERE a_id=@a_id

                         ELSE 

                        INSERT INTO BS_VadiDetailEntry (  a_id, vadi_Name ) VALUES ( @a_id, @vadi_Name )";

                cmd.Parameters.AddWithValue("@a_id", applicationId);

                cmd.Parameters.AddWithValue("@vadi_Name", txtNamePerAadhaar.Text.Trim());

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private bool SaveStep2(long applicationId)
        {
            return true;
        }

        private bool SaveStep3(long applicationId)
        {
            return true;
        }

        private bool SaveStep4(long applicationId)
        {
            return true;
        }

        private bool SaveStep5(long applicationId)
        {
            return true;
        }

        private bool SaveStep6(long applicationId)
        {
            return true;
        }

        private bool SaveStep7(long applicationId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conns"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;

                cmd.CommandText =  @"IF EXISTS(SELECT 1 FROM BS_ActionDetailsEntry WHERE a_id=@a_id)

            UPDATE BS_ActionDetailsEntry  SET Meeting_Date=@Meeting_Date,  Is_Vadi_Present=@Is_Vadi_Present, UserID=@UserID WHERE a_id=@a_id

               ELSE

             INSERT INTO BS_ActionDetailsEntry ( a_id,  Meeting_Date, Is_Vadi_Present,  UserID ) VALUES ( @a_id,  @Meeting_Date, @Is_Vadi_Present,  @UserID )";

                cmd.Parameters.AddWithValue("@a_id", applicationId);

                //cmd.Parameters.AddWithValue("@Meeting_Date", Convert.ToDateTime( txtMeetingDate.Text));

                //cmd.Parameters.AddWithValue("@Is_Vadi_Present", ddlPresent.SelectedValue);

                cmd.Parameters.AddWithValue("@UserID", userid);

                cmd.ExecuteNonQuery();

                return true;
            }
        }

        //-------------Step 1/2----------------------------

        protected void AdharYearsBind()
        {
            //string[] retVal = new string[122]; ;
            //int index = 0;
            //for (int i = 2021; i >= 1900; i--)
            //{
            //    retVal[index] = i.ToString();
            //    index = index + 1;
            //}

            //ddlYear.DataSource = retVal;
            //ddlYear.DataBind();
            //ddlYear.Items.Insert(0, new ListItem("--Select--", "0"));

            //return;

            ddlYear.DataSource = Enumerable.Range(1900, 122).Reverse().Select(x => x.ToString());

            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindDist_Wadi_Pratiwadi()
        {
            ddlUserDist.Items.Clear();
            ddlDistrict.Items.Clear();
            ddlPDistrict.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                DataTable dtDistrict = objDBHelper.GetResults("SELECT distinct DISTRICTNAME,DISTRICTCODE from mst_Commissionary_Districts ORDER BY DISTRICTNAME;", listSQLP, false);
                if (dtDistrict.Rows.Count > 0)
                {
                    ddlUserDist.DataSource = dtDistrict;
                    ddlUserDist.DataTextField = "DISTRICTNAME";
                    ddlUserDist.DataValueField = "DISTRICTCODE";
                    ddlUserDist.DataBind();
                    ddlUserDist.Items.Insert(0, new ListItem("--Select--", "0"));

                    ddlDistrict.DataSource = dtDistrict;
                    ddlDistrict.DataTextField = "DISTRICTNAME";
                    ddlDistrict.DataValueField = "DISTRICTCODE";
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));

                    ddlPDistrict.DataSource = dtDistrict;
                    ddlPDistrict.DataTextField = "DISTRICTNAME";
                    ddlPDistrict.DataValueField = "DISTRICTCODE";
                    ddlPDistrict.DataBind();
                    ddlPDistrict.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlUserDist.DataSource = null;

                    ddlUserDist.DataBind();

                    ddlDistrict.DataSource = null;

                    ddlDistrict.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindSubDivision_wadi()
        {
            ddlUserSubdivision.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", ddlUserDist.SelectedValue.ToString()));
                DataTable dt = objDBHelper.GetResults("select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where sd.DistCode=@District_Code order by sd.Sd_Name_En", listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlUserSubdivision.DataSource = dt;
                    ddlUserSubdivision.DataTextField = "SubDivisionName";
                    ddlUserSubdivision.DataValueField = "SubDivisionCode";
                    ddlUserSubdivision.DataBind();
                    ddlUserSubdivision.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlUserSubdivision.DataSource = null;

                    ddlUserSubdivision.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }
        private void BindSubDivision()
        {
            ddlSubdivision.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@thana_code", thanacode));
                string sql = @"select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where Sd_Code2 in (select SubDivCode from Blocks where BlockCode in(select code from MstThanaMapping where thana_code=@thana_code)) and sd.DistCode=@District_Code";
                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlSubdivision.DataSource = dt;
                    ddlSubdivision.DataTextField = "SubDivisionName";
                    ddlSubdivision.DataValueField = "SubDivisionCode";
                    ddlSubdivision.DataBind();
                    ddlSubdivision.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlSubdivision.DataSource = null;

                    ddlSubdivision.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }


        private void BindBlock_Wadi()
        {
            ddlUserBlock.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
               
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", ddlUserDist.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Subdivision_Code", ddlUserSubdivision.SelectedValue.ToString()));

                DataTable dt = objDBHelper.GetResults("select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) order by BlockName", listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlUserBlock.DataSource = dt;
                    ddlUserBlock.DataTextField = "BlockName";
                    ddlUserBlock.DataValueField = "BlockCode";
                    ddlUserBlock.DataBind();
                    ddlUserBlock.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlUserBlock.DataSource = null;

                    ddlUserBlock.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }


        private void BindBlock()
        {
            ddlBlock.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Subdivision_Code", ddlSubdivision.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@thana_code", thanacode));

                string sql = @"select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) and BlockCode in (select code from MstThanaMapping where thana_code=@thana_code)  order by BlockName";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
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
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindPolice_wadi()
        {
            ddlUserThana.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", ddlUserDist.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Subdivision_Code", ddlUserSubdivision.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Circle_Code", ddlUserBlock.SelectedValue.ToString()));

                string sql = @"select DISTINCT  t.Police_Station,t.PS_Code from mst_thana t
	                        left join MstThanaMapping m on m.Thana_Code=t.PS_Code 
	                        left join Blocks b on b.BlockCode=m.Code and m.Type='Block'
	                        where District_code=@District_Code and  b.SubDivCode is not null and m.code=@Circle_Code and b.SubDivCode=@Subdivision_Code
                            ORDER BY Police_Station";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlUserThana.DataSource = dt;
                    ddlUserThana.DataTextField = "Police_Station";
                    ddlUserThana.DataValueField = "PS_Code";
                    ddlUserThana.DataBind();
                    ddlUserThana.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlUserThana.DataSource = null;

                    ddlUserThana.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindPolice()
        {
            ddlPolice.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Subdivision_Code", ddlSubdivision.SelectedValue.ToString()));
                

                string sql = @"select DISTINCT t.Police_Station,t.PS_Code from mst_Thana t  where t.Subdivision_Code=@Subdivision_Code and t.District_code=@District_Code";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlPolice.DataSource = dt;
                    ddlPolice.DataTextField = "Police_Station";
                    ddlPolice.DataValueField = "PS_Code";
                    ddlPolice.DataBind();
                    ddlPolice.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlPolice.DataSource = null;

                    ddlPolice.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindPanchyat_Wadi()
        {
            ddlUserPanchyat.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@BlockCode", ddlUserBlock.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));

                DataTable dt = objDBHelper.GetResults("select DISTINCT PanchayatCode,PanchayatNameHnd,PanchayatName from mst_Panchayats t inner join Blocks p on t.BlockCode = p.BlockCode where p.BlockCode=@BlockCode and (@AreaType='' or t.AreaType=@AreaType) order by PanchayatName", listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlUserPanchyat.DataSource = dt;
                    ddlUserPanchyat.DataTextField = "PanchayatName";
                    ddlUserPanchyat.DataValueField = "PanchayatCode";
                    ddlUserPanchyat.DataBind();
                    ddlUserPanchyat.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlUserPanchyat.DataSource = null;

                    ddlUserPanchyat.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindPanchyat()
        {
            ddlPanchyat.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@BlockCode", ddlBlock.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlareatype.SelectedValue.ToString()));

                string sql = @" select DISTINCT PanchayatCode,PanchayatNameHnd,PanchayatName from mst_Panchayats t inner join Blocks p on t.BlockCode = p.BlockCode 
                                where p.DistCode=@District_Code and p.BlockCode=@BlockCode and (@AreaType='' or t.AreaType=@AreaType) order by PanchayatNameHnd";
                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlPanchyat.DataSource = dt;
                    ddlPanchyat.DataTextField = "PanchayatName";
                    ddlPanchyat.DataValueField = "PanchayatCode";
                    ddlPanchyat.DataBind();
                    ddlPanchyat.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlPanchyat.DataSource = null;

                    ddlPanchyat.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindVillage_Wadi()
        {
            ddlUserVillage.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@PanchayatCode", ddlUserPanchyat.SelectedValue.ToString()));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));

                string sql = @"select DISTINCT v.VILLCODE, v.VILLNAME  from mst_Panchayats p 
                            inner join PanchayatVillage pv on p.PanchayatCode=pv.PanchayatCode
                            inner join mst_VillageMaster v on v.VILLCODE=pv.VillageCode
                            where p.PanchayatCode=@PanchayatCode order by v.VILLNAME	";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlUserVillage.DataSource = dt;
                    ddlUserVillage.DataTextField = "VILLNAME";
                    ddlUserVillage.DataValueField = "VILLCODE";
                    ddlUserVillage.DataBind();
                    ddlUserVillage.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlUserVillage.DataSource = null;

                    ddlUserVillage.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindVillage()
        {
            ddlVillage.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@PanchayatCode", ddlPanchyat.SelectedValue.ToString()));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));

                string sql = @"select DISTINCT v.VILLCODE, v.VILLNAME  from mst_Panchayats p 
                            inner join PanchayatVillage pv on p.PanchayatCode=pv.PanchayatCode
                            inner join mst_VillageMaster v on v.VILLCODE=pv.VillageCode
                            where p.PanchayatCode=@PanchayatCode order by 	 v.VILLNAME";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlVillage.DataSource = dt;
                    ddlVillage.DataTextField = "VILLNAME";
                    ddlVillage.DataValueField = "VILLCODE";
                    ddlVillage.DataBind();
                    ddlVillage.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlVillage.DataSource = null;

                    ddlVillage.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void bindward_Wadi()
        {
            ddlUserWard.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Panchayat", ddlUserPanchyat.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));

                string sql = @"select DISTINCT t.WARDNAME,WARDCODE,t.AreaType from mst_Wards t left join mst_Panchayats p on t.PANCHAYATCODE = p.PanchayatCode where p.PANCHAYATCODE=@Panchayat and p.AreaType=@AreaType order by WARDNAME";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlUserWard.DataSource = dt;
                    ddlUserWard.DataTextField = "WARDNAME";
                    ddlUserWard.DataValueField = "WARDCODE";
                    ddlUserWard.DataBind();
                    ddlUserWard.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlUserWard.DataSource = null;

                    ddlUserWard.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void bindward()
        {
            ddlWard.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Panchayat", ddlPanchyat.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlareatype.SelectedValue.ToString()));

                string sql = @"  select DISTINCT t.WARDNAME,WARDCODE,t.AreaType from mst_Wards t left join mst_Panchayats p on t.PANCHAYATCODE = p.PanchayatCode where p.PANCHAYATCODE=@Panchayat and p.AreaType=@AreaType order by WARDNAME";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlWard.DataSource = dt;
                    ddlWard.DataTextField = "WARDNAME";
                    ddlWard.DataValueField = "WARDCODE";
                    ddlWard.DataBind();
                    ddlWard.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlWard.DataSource = null;

                    ddlWard.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindVadi_Prativadi_Anya_Type()
        {
            ddlWsanstha_naam.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Panchayat", ddlUserPanchyat.SelectedValue.ToString()));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));

                string sql = @"select id, name from Vadi_Prativadi_Anya_Type order by id asc";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlWsanstha_naam.DataSource = dt;
                    ddlWsanstha_naam.DataTextField = "name";
                    ddlWsanstha_naam.DataValueField = "id";
                    ddlWsanstha_naam.DataBind();
                    ddlWsanstha_naam.Items.Insert(0, new ListItem("--Select--", "0"));

                    ddlPsanstha_naam.DataSource = dt;
                    ddlPsanstha_naam.DataTextField = "name";
                    ddlPsanstha_naam.DataValueField = "id";
                    ddlPsanstha_naam.DataBind();
                    ddlPsanstha_naam.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlWsanstha_naam.DataSource = null;

                    ddlWsanstha_naam.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindVadi_Sanstha_Anya_Type()
        {
           
            ddlWsanshaanya_naam.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Panchayat", ddlUserPanchyat.SelectedValue.ToString()));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));

                string sql = @"SELECT id, name FROM mst_sanstha_ka_sambandh_type order by id asc";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlWsanshaanya_naam.DataSource = dt;
                    ddlWsanshaanya_naam.DataTextField = "name";
                    ddlWsanshaanya_naam.DataValueField = "id";
                    ddlWsanshaanya_naam.DataBind();
                    ddlWsanshaanya_naam.Items.Insert(0, new ListItem("--Select--", "0"));

                    ddlPsanshaanya_naam.DataSource = dt;
                    ddlPsanshaanya_naam.DataTextField = "name";
                    ddlPsanshaanya_naam.DataValueField = "id";
                    ddlPsanshaanya_naam.DataBind();
                    ddlPsanshaanya_naam.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlWsanshaanya_naam.DataSource = null;

                    ddlWsanshaanya_naam.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }


        private void bindDepartment()
        {

            ddlWvibhaag_naam.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Panchayat", ddlUserPanchyat.SelectedValue.ToString()));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));

              
                DataTable dt = objDBHelper.GetResults("SP_BindDepartment", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlWvibhaag_naam.DataSource = dt;
                    ddlWvibhaag_naam.DataTextField = "name";
                    ddlWvibhaag_naam.DataValueField = "id";
                    ddlWvibhaag_naam.DataBind();
                    ddlWvibhaag_naam.Items.Insert(0, new ListItem("--Select--", "0"));

                    ddlPvibhaag_naam.DataSource = dt;
                    ddlPvibhaag_naam.DataTextField = "name";
                    ddlPvibhaag_naam.DataValueField = "id";
                    ddlPvibhaag_naam.DataBind();
                    ddlPvibhaag_naam.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlWvibhaag_naam.DataSource = null;

                    ddlWvibhaag_naam.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_bhumivivad_ki_adyatan_sthiti()
        {

            ddl_vivad_adyatan_sthiti.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Panchayat", ddlUserPanchyat.SelectedValue.ToString()));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));


                DataTable dt = objDBHelper.GetResults("SP_GetBhumi_Vivad_adyatan_sthiti", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddl_vivad_adyatan_sthiti.DataSource = dt;
                    ddl_vivad_adyatan_sthiti.DataTextField = "status_name";
                    ddl_vivad_adyatan_sthiti.DataValueField = "id";
                    ddl_vivad_adyatan_sthiti.DataBind();
                    ddl_vivad_adyatan_sthiti.Items.Insert(0, new ListItem("--Select--", "0"));

                 
                }
                else
                {
                    ddl_vivad_adyatan_sthiti.DataSource = null;

                    ddl_vivad_adyatan_sthiti.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        private void bindbumitype()//   भूमि का प्रकार
        {

            ddlbhumitype.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Panchayat", ddlUserPanchyat.SelectedValue.ToString()));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));


                DataTable dt = objDBHelper.GetResults("SP_BindBhumitype", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlbhumitype.DataSource = dt;
                    ddlbhumitype.DataTextField = "bhumitype";
                    ddlbhumitype.DataValueField = "id";
                    ddlbhumitype.DataBind();
                    ddlbhumitype.Items.Insert(0, new ListItem("--Select--", "0"));


                }
                else
                {
                    ddlbhumitype.DataSource = null;

                    ddlbhumitype.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_bhumivivad_Type()// भूमि विवाद का प्रकार 
        {

            ddlbhumivivadtype.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                DataTable dt = objDBHelper.GetResults("SP_GetBhumi_VivadType", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlbhumivivadtype.DataSource = dt;
                    ddlbhumivivadtype.DataTextField = "vivadtype";
                    ddlbhumivivadtype.DataValueField = "id";
                    ddlbhumivivadtype.DataBind();
                    ddlbhumivivadtype.Items.Insert(0, new ListItem("--Select--", "0"));


                }
                else
                {
                    ddlbhumivivadtype.DataSource = null;

                    ddlbhumivivadtype.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        private void bindSarkariBumitype()//  सरकारी भूमि का प्रकार
        {

            ddlsarkaribhumitype.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                DataTable dt = objDBHelper.GetResults("SP_GetSarkariBhumi_type", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlsarkaribhumitype.DataSource = dt;
                    ddlsarkaribhumitype.DataTextField = "bhumitype";
                    ddlsarkaribhumitype.DataValueField = "id";
                    ddlsarkaribhumitype.DataBind();
                    ddlsarkaribhumitype.Items.Insert(0, new ListItem("--Select--", "0"));


                }
                else
                {
                    ddlsarkaribhumitype.DataSource = null;

                    ddlsarkaribhumitype.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        //--------------Pratawadi Section------------------------------------------------
     
        private void BindSubDivision_Pratiwadi()
        {
            ddlPSubdivision.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", ddlPDistrict.SelectedValue.ToString()));
                DataTable dt = objDBHelper.GetResults("select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where sd.DistCode=@District_Code order by sd.Sd_Name_En", listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlPSubdivision.DataSource = dt;
                    ddlPSubdivision.DataTextField = "SubDivisionName";
                    ddlPSubdivision.DataValueField = "SubDivisionCode";
                    ddlPSubdivision.DataBind();
                    ddlPSubdivision.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlPSubdivision.DataSource = null;

                    ddlPSubdivision.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindBlock_Pratiwadi()
        {
            ddlPBlock.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", ddlPDistrict.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Subdivision_Code", ddlPSubdivision.SelectedValue.ToString()));

                DataTable dt = objDBHelper.GetResults("select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) order by BlockName", listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlPBlock.DataSource = dt;
                    ddlPBlock.DataTextField = "BlockName";
                    ddlPBlock.DataValueField = "BlockCode";
                    ddlPBlock.DataBind();
                    ddlPBlock.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlPBlock.DataSource = null;

                    ddlPBlock.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindPolice_Prtiwadi()
        {
            ddlPThana.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", ddlPDistrict.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Subdivision_Code", ddlPSubdivision.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Circle_Code", ddlPBlock.SelectedValue.ToString()));

                string sql = @"select DISTINCT  t.Police_Station,t.PS_Code from mst_thana t
	                        left join MstThanaMapping m on m.Thana_Code=t.PS_Code 
	                        left join Blocks b on b.BlockCode=m.Code and m.Type='Block'
	                        where District_code=@District_Code and  b.SubDivCode is not null and m.code=@Circle_Code and b.SubDivCode=@Subdivision_Code
                            ORDER BY Police_Station";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlPThana.DataSource = dt;
                    ddlPThana.DataTextField = "Police_Station";
                    ddlPThana.DataValueField = "PS_Code";
                    ddlPThana.DataBind();
                    ddlPThana.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlPThana.DataSource = null;

                    ddlPThana.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindVillage_Pratiwadi()
        {
            ddlPVillage.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@PanchayatCode", ddlPPanchyat.SelectedValue.ToString()));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));

                string sql = @"select DISTINCT v.VILLCODE, v.VILLNAME  from mst_Panchayats p 
                            inner join PanchayatVillage pv on p.PanchayatCode=pv.PanchayatCode
                            inner join mst_VillageMaster v on v.VILLCODE=pv.VillageCode
                            where p.PanchayatCode=@PanchayatCode order by v.VILLNAME";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlPVillage.DataSource = dt;
                    ddlPVillage.DataTextField = "VILLNAME";
                    ddlPVillage.DataValueField = "VILLCODE";
                    ddlPVillage.DataBind();
                    ddlPVillage.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlPVillage.DataSource = null;

                    ddlPVillage.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindPanchyat_Prtiwadi()
        {
            ddlPPanchyat.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@BlockCode", ddlPBlock.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlPAreatype.SelectedValue.ToString()));

                DataTable dt = objDBHelper.GetResults("select DISTINCT PanchayatCode,PanchayatNameHnd,PanchayatName from mst_Panchayats t inner join Blocks p on t.BlockCode = p.BlockCode where p.BlockCode=@BlockCode and (@AreaType='' or t.AreaType=@AreaType) order by PanchayatName", listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlPPanchyat.DataSource = dt;
                    ddlPPanchyat.DataTextField = "PanchayatName";
                    ddlPPanchyat.DataValueField = "PanchayatCode";
                    ddlPPanchyat.DataBind();
                    ddlPPanchyat.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlPPanchyat.DataSource = null;

                    ddlPPanchyat.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void bindward_Pratiwadi()
        {
            ddlPWard.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Panchayat", ddlPPanchyat.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlPAreatype.SelectedValue.ToString()));

                string sql = @"select DISTINCT t.WARDNAME,WARDCODE,t.AreaType from mst_Wards t left join mst_Panchayats p on t.PANCHAYATCODE = p.PanchayatCode where p.PANCHAYATCODE=@Panchayat and p.AreaType=@AreaType order by WARDNAME";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlPWard.DataSource = dt;
                    ddlPWard.DataTextField = "WARDNAME";
                    ddlPWard.DataValueField = "WARDCODE";
                    ddlPWard.DataBind();
                    ddlPWard.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlPWard.DataSource = null;

                    ddlPWard.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        //-------------Step3---------------------------------------
        private void BindLandUnit(DropDownList ddl, int type)
        {
            try
            {
                List<SqlParameter> listSQLP = new List<SqlParameter>();

                listSQLP.Add(new SqlParameter("@filter", type));

                DataTable dt = objDBHelper.GetResults("SP_GetLandUnit", listSQLP, true);

                ddl.DataSource = dt;
                ddl.DataTextField = "name_hin";
                ddl.DataValueField = "id";
                ddl.DataBind();

                ddl.Items.Insert(0, new ListItem("--चुने--", "0"));
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


        private void bindLandUnit()
        {
            BindLandUnit(ddlrakabasankhya, 1);
            BindLandUnit(ddlrakabasankhya1, 2);
            BindLandUnit(ddlrakabasankhya2, 3);
        }


        private void bind_khatiyan_Type()
        {
            ddlkhatiyan_me_jaminvivran.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                DataTable dt = objDBHelper.GetResults("SP_GetKhatiyan_Type", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlkhatiyan_me_jaminvivran.DataSource = dt;
                    ddlkhatiyan_me_jaminvivran.DataTextField = "Landdesciption";
                    ddlkhatiyan_me_jaminvivran.DataValueField = "id";
                    ddlkhatiyan_me_jaminvivran.DataBind();
                    ddlkhatiyan_me_jaminvivran.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlkhatiyan_me_jaminvivran.DataSource = null;

                    ddlkhatiyan_me_jaminvivran.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        //-------------Step4-------------------
        private void bindLandEvidence()
        {
            ddlVadiEvidenceType.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                DataTable dt = objDBHelper.GetResults("SP_BindLandEvidence", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlVadiEvidenceType.DataSource = dt;
                    ddlVadiEvidenceType.DataTextField = "name";
                    ddlVadiEvidenceType.DataValueField = "id";
                    ddlVadiEvidenceType.DataBind();
                    ddlVadiEvidenceType.Items.Insert(0, new ListItem("--Select--", "0"));

                    ddlPrativadiEvidenceType.DataSource = dt;
                    ddlPrativadiEvidenceType.DataTextField = "name";
                    ddlPrativadiEvidenceType.DataValueField = "id";
                    ddlPrativadiEvidenceType.DataBind();
                    ddlPrativadiEvidenceType.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlVadiEvidenceType.DataSource = null;

                    ddlVadiEvidenceType.DataBind();

                    ddlPrativadiEvidenceType.DataSource = null;
                    ddlPrativadiEvidenceType.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        //-------------------------Step4------------------------------------------

        //-------------step 6--------------------------------------

        private void Bindbsndhara()
        {
            ddlbsn_dhara_hai.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();


                string sql = @"select ID, BNS_Sec from Bns_dhara";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlbsn_dhara_hai.DataSource = dt;
                    ddlbsn_dhara_hai.DataTextField = "BNS_Sec";
                    ddlbsn_dhara_hai.DataValueField = "ID";
                    ddlbsn_dhara_hai.DataBind();
                    ddlbsn_dhara_hai.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlbsn_dhara_hai.DataSource = null;

                    ddlbsn_dhara_hai.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }
        private void BindNyayalaya()
        {
            ddlnyayalaya.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();


                string sql = @"select id, name from mst_court order by id asc";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlnyayalaya.DataSource = dt;
                    ddlnyayalaya.DataTextField = "name";
                    ddlnyayalaya.DataValueField = "id";
                    ddlnyayalaya.DataBind();
                    ddlnyayalaya.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlnyayalaya.DataSource = null;

                    ddlnyayalaya.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }
        private void BindNyayalayaType()
        {
            ddlnyayalaya_type.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@court_id", ddlnyayalaya.SelectedValue.ToString()));

                string sql = @"select t.id, t.name from mst_CourtType t inner join mst_court c on t.court_id = c.id
                           where t.court_id=@court_id and isnull(t.IsActive,'N')='Y' order by t.id, t.name asc";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlnyayalaya_type.DataSource = dt;
                    ddlnyayalaya_type.DataTextField = "name";
                    ddlnyayalaya_type.DataValueField = "id";
                    ddlnyayalaya_type.DataBind();
                    ddlnyayalaya_type.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlnyayalaya_type.DataSource = null;

                    ddlnyayalaya_type.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }
        private void BindNyayalayaType_Vibhag()//----need to check query from previous method
        {
            ddlVibhag_nyayalay_type.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();


                string sql = @" select DISTINCT sd.name ,sd.id from mst_Deptl_Public_Grievance_Redressal_Court_Type sd order by sd.id";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlVibhag_nyayalay_type.DataSource = dt;
                    ddlVibhag_nyayalay_type.DataTextField = "name";
                    ddlVibhag_nyayalay_type.DataValueField = "id";
                    ddlVibhag_nyayalay_type.DataBind();
                    ddlVibhag_nyayalay_type.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlVibhag_nyayalay_type.DataSource = null;

                    ddlVibhag_nyayalay_type.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindNyayalayaType_dist()//-------------------District bind method is somewhere available.Need to check
        {
            ddlDist_nyayalaya_type.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();


                string sql = @"SELECT distinct DISTRICTNAME,DISTRICTCODE from mst_Commissionary_Districts ORDER BY DISTRICTNAME ";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlDist_nyayalaya_type.DataSource = dt;
                    ddlDist_nyayalaya_type.DataTextField = "DISTRICTNAME";
                    ddlDist_nyayalaya_type.DataValueField = "DISTRICTCODE";
                    ddlDist_nyayalaya_type.DataBind();
                    ddlDist_nyayalaya_type.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlDist_nyayalaya_type.DataSource = null;

                    ddlDist_nyayalaya_type.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }


        private void BindNyayalayaType_SubDivision()//------------subdivision bind method is somewhere available.Need to check
        {
            ddlSubdivision_nyayalaya_type.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();


                string sql = @"select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd order by sd.Sd_Name_En";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlSubdivision_nyayalaya_type.DataSource = dt;
                    ddlSubdivision_nyayalaya_type.DataTextField = "SubDivisionName";
                    ddlSubdivision_nyayalaya_type.DataValueField = "SubDivisionCode";
                    ddlSubdivision_nyayalaya_type.DataBind();
                    ddlSubdivision_nyayalaya_type.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlSubdivision_nyayalaya_type.DataSource = null;

                    ddlSubdivision_nyayalaya_type.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        //----------------Step 4------------------------------------------------
        protected void ddlIsVadiEvi_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtVadiEvidenceType.Text = "";
            if (ddlIsVadiEvi.SelectedValue == "Y")
            {
                divVadiEvidenceType.Visible = true;
                divvadi_dastavej.Visible = true;

            }
            else if (ddlIsPvadiEvi.SelectedValue == "N" || ddlIsPvadiEvi.SelectedValue == "0")
            {
                divVadiEvidenceType.Visible = false;
                divvadi_dastavej.Visible = false;
                divtxtVadiEvidenceType.Visible = false;

            }
        }

        //-------------Step5---------------------------------------------
        protected void ddlbhukhand_mapi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //divbhukhand_mapi.Attributes.Add("class", "col-md-9");
            divbhukhand_Copy.Visible = false;
            ddlbhukhand_Copy.SelectedIndex = 0;
            ddlbhukhand_Copy_SelectedIndexChanged(sender, e);   //-----------mistake

            if (ddlbhukhand_mapi.SelectedIndex == 1)
            {
                //divbhukhand_mapi.Attributes.Add("class", "col-md-6");
                divbhukhand_Copy.Visible = true;
                ddlbhukhand_Copy.SelectedIndex = 0;
            }
        }

        protected void ddlbhukhand_Copy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMapiKeNirdharit_tithi.Text = string.Empty;
            txtbhukhand_reason.Text = string.Empty;

            switch (ddlbhukhand_Copy.SelectedValue)
            {
                case "Y":   // मापी हुई है

                    divBhukhandReport.Visible = true;
                    divBhukhandReason.Visible = false;
                    divMapiKeNirdharit_tithi.Visible = false;

                    break;

                case "N":   // मापी नहीं हुई है

                    divBhukhandReport.Visible = false;
                    divBhukhandReason.Visible = true;
                    divMapiKeNirdharit_tithi.Visible = true;

                    break;

                default:

                    divBhukhandReport.Visible = false;
                    divBhukhandReason.Visible = false;
                    divMapiKeNirdharit_tithi.Visible = false;

                    break;
            }
        }

        //---------------------------------------------------------



        protected void ddlIsPvadiEvi_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPrativadiEvidenceType.Text = "";
            if (ddlIsPvadiEvi.SelectedValue == "Y")
            {
                divPrativadiEvidence.Visible = true;
                divPrativadi_dastavej_new.Visible = true;
            }
            else if (ddlIsPvadiEvi.SelectedValue == "N" || ddlIsPvadiEvi.SelectedValue == "0")
            {
                divPrativadiEvidence.Visible = false;
                divPrativadi_dastavej_new.Visible = false;
                divtxtPrativadiEvidenceType.Visible = false;
            }
        }

        //------------------------Step6-------------------------------------------

        //-------------steps 6---------------------------

        protected void dd_IsBhumiVivad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dd_IsBhumiVivad.SelectedValue == "Y")
            {
                // btnbhumivivad.Visible = true;
                btnBhumiVivadVivran1.Visible = true;
                btnBhumiVivadVivran2.Visible = true;
                btnBhumiVivadVivran3.Visible = true;
                btnBhumiVivadVivran4.Visible = true;
                btnBhumiVivadVivran5.Visible = true;
                //btnBhumiVivadVivran6.Visible = true;
                //ViewState["DetailsOfIncidentDT"] = DetailsOfIncidentDT();
                //this.BindGrid();
            }
            else
            {
                btnBhumiVivadVivran1.Visible = false;
                btnBhumiVivadVivran2.Visible = false;
                btnBhumiVivadVivran3.Visible = false;
                btnBhumiVivadVivran4.Visible = false;
                btnBhumiVivadVivran5.Visible = false;
                //btnBhumiVivadVivran6.Visible = false;
                //btnbhumivivad.Visible = false;
                //ViewState["DetailsOfIncidentDT"] = DetailsOfIncidentDT();
                //this.BindGrid();
            }
        }

        protected void ddlPrathmiki_huyee_hai_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFIR_sankhya.Text = "";
            txtPrathmik_vivran.Text = "";
            if (ddlPrathmiki_huyee_hai.SelectedIndex == 1)
            {
                divPrathmiki_sankhiyan.Visible = true;
                divPrathmiki_vivaran.Visible = true;
            }
            else
            {
                divPrathmiki_sankhiyan.Visible = false;
                divPrathmiki_vivaran.Visible = false;
            }
        }

        protected void ddlAprathmiki_huyee_hai_SelectedIndexChanged(object sender, EventArgs e)
        {

            chk107.Checked = false;
            chk109.Checked = false;
            chk110.Checked = false;
            chk113.Checked = false;
            chk116.Checked = false;
            chk133.Checked = false;
            chk144.Checked = false;
            chk145.Checked = false;
            chk147.Checked = false;
            txtAFIR_sankhya.Text = "";
            txtAprathmik_vivran.Text = "";

            if (ddlAprathmiki_huyee_hai.SelectedIndex == 1)
            {
                Bindbsndhara();
                divdharabsn.Visible = true;
                //divDhara.Visible = true;
                divAPrathmiki_sankhiyan.Visible = true;
                divAPrathmiki_vivaran.Visible = true;
                rdoOld.Checked = false;
                rdoNew.Checked = false;
                txtbnm.Text = "";
                txtdhara.Text = "";
                ddldhara1.ClearSelection();
                ddlbsn_dhara_hai.ClearSelection();

                //divbsn.Visible = true;
                //divdhara1.Visible = true;

                //checkboxolddhara
                chk107.Checked = false;
                chk109.Checked = false;
                chk110.Checked = false;
                chk113.Checked = false;
                chk116.Checked = false;
                chk133.Checked = false;
                chk144.Checked = false;
                chk145.Checked = false;
                chk147.Checked = false;
                //endcheckboxolddhara
            }
            else
            {
                divAPrathmiki_sankhiyan.Visible = false;
                divAPrathmiki_vivaran.Visible = false;
                divdharabsn.Visible = false;
                divDhara.Visible = false;
                divbsn.Visible = false;
                divdhara1.Visible = false;
                rdoOld.Checked = false;
                rdoNew.Checked = false;
                txtbnm.Text = "";
                txtdhara.Text = "";
                ddldhara1.ClearSelection();
                ddlbsn_dhara_hai.ClearSelection();
                //checkboxolddhara
                chk107.Checked = false;
                chk109.Checked = false;
                chk110.Checked = false;
                chk113.Checked = false;
                chk116.Checked = false;
                chk133.Checked = false;
                chk144.Checked = false;
                chk145.Checked = false;
                chk147.Checked = false;


            }
        }


        protected void ddlbsn_dhara_hai_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 🔥 STEP 0: SAVE selected IPC values (HiddenField priority)
            List<string> selectedIPC = new List<string>();

            if (!string.IsNullOrEmpty(hdnSelectedIPC.Value))
            {
                selectedIPC = hdnSelectedIPC.Value.Split(',').ToList();
            }
            else
            {
                foreach (ListItem item in ddldhara1.Items)
                {
                    if (item.Selected)
                    {
                        selectedIPC.Add(item.Value);
                    }
                }
            }

            ViewState["SelectedIPC"] = selectedIPC;

            // 🔹 STEP 1: Get selected BNS items
            var selectedItems = ddlbsn_dhara_hai.Items
                                .Cast<ListItem>()
                                .Where(i => i.Selected)
                                .ToList();

            bool contains41 = selectedItems.Any(i => i.Value == "41");
            int selectedCount = selectedItems.Count;

            // 🔹 STEP 2: UI Visibility Logic
            if (contains41 && selectedCount == 1)
            {
                div_tbnm.Visible = true;
                div_tdhara.Visible = true;
                divdhara1.Visible = false;
            }
            else if (contains41 && selectedCount > 1)
            {
                div_tbnm.Visible = true;
                div_tdhara.Visible = true;
                divdhara1.Visible = true;
            }
            else
            {
                div_tbnm.Visible = false;
                div_tdhara.Visible = false;
                divdhara1.Visible = true;
            }

            // 🔥 STEP 3: Bind IPC based on BNS
            Binddhara();
        }

        void Binddhara()
        {
            try
            {
                // 🔥 GUARD (MOST IMPORTANT FIX)
                string ctrl = Request["__EVENTTARGET"];

                if (ctrl != ddlbsn_dhara_hai.UniqueID)
                {
                    return;
                }

                // 🔹 Step 0: ViewState se lo
                List<string> previouslySelected = new List<string>();

                if (ViewState["SelectedIPC"] != null)
                {
                    previouslySelected = (List<string>)ViewState["SelectedIPC"];
                }

                // 🔹 Step 1: BNS selected IDs
                List<string> selectedIds = new List<string>();

                foreach (ListItem item in ddlbsn_dhara_hai.Items)
                {
                    if (item.Selected)
                    {
                        selectedIds.Add(item.Value);
                    }
                }

                if (selectedIds.Count == 0)
                {
                    ddldhara1.Items.Clear();
                    return;
                }

                string ids = string.Join(",", selectedIds);



                // DataTable dt = clsData.GetDataTable(sql);

                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                string sql = @"SELECT ID, IPC_Sec  FROM Bns_dhara   WHERE ID IN (" + ids + ")";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddldhara1.DataSource = dt;
                    ddldhara1.DataTextField = "IPC_Sec";
                    ddldhara1.DataValueField = "IPC_Sec";
                    ddldhara1.DataBind();
                }
                else
                {
                    ddlbsn_dhara_hai.DataSource = null;

                    ddlbsn_dhara_hai.DataBind();
                }


                // 🔥 restore selection
                foreach (ListItem item in ddldhara1.Items)
                {
                    if (previouslySelected.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }

                // 🔥 NEW: updated selection ko dobara ViewState me save karo
                List<string> updatedSelected = new List<string>();

                foreach (ListItem item in ddldhara1.Items)
                {
                    if (item.Selected)
                    {
                        updatedSelected.Add(item.Value);
                    }
                }

                ViewState["SelectedIPC"] = updatedSelected;

                dt.Dispose();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSanhaStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSanahaSankhiyan.Text = "";
            if (ddlSanhaStatus.SelectedIndex == 1)
            {
                divSanahaSankhiyan1.Visible = true;
                divSanahaSankhiyan2.Visible = true;

            }
            else
            {
                divSanahaSankhiyan1.Visible = false;
                divSanahaSankhiyan2.Visible = false;

            }
        }

        protected void ddl_Isbhumi_Viviad_available_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Isbhumi_Viviad_available.SelectedValue == "Y")
            {
                btnnyayalay1.Visible = true;
                //btnnyayalay2.Visible = true;
                btnnyayalay3.Visible = true;
                btnnyayalay4.Visible = true;
                btnnyayalay5.Visible = true;
                btnnyayalay6.Visible = true;
                //btnnyayalay7.Visible = true;
                // btnnayaylaysave.Visible = true;
                //ViewState["CourtDisputeDetailsDT"] = CourtDisputeDetailsDT();
                //this.BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();
            }
            else
            {
                btnnyayalay1.Visible = false;
                btnnyayalay3.Visible = false;
                btnnyayalay4.Visible = false;
                btnnyayalay5.Visible = false;
                btnnyayalay6.Visible = false;
                //btnnyayalay7.Visible = false;
                //btnnayaylaysave.Visible = false;
                divDist_nyayalaya_type.Visible = false;
                divSubdivision_nyayalaya_type.Visible = false;
                //ViewState["CourtDisputeDetailsDT"] = CourtDisputeDetailsDT();
                //this.BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();
            }
        }

        protected void ddlnyayalaya_SelectedIndexChanged(object sender, EventArgs e)
        {
            div_rajasw_vevhar_nyalay.Visible = false;
            divSubdivision_nyayalaya_type.Visible = false;
            divDist_nyayalaya_type.Visible = false;
            //divVibhag_nyayalay_type.Visible = false;
            if (ddlnyayalaya.SelectedIndex == 1)
            {
                BindNyayalayaType();
                div_rajasw_vevhar_nyalay.Visible = true;
                labNyayalaya_type.Text = "राजस्व न्यायालय का प्रकार";
            }

            else if (ddlnyayalaya.SelectedIndex == 2)
            {
                BindNyayalayaType();
                div_rajasw_vevhar_nyalay.Visible = true;
                labNyayalaya_type.Text = "व्यवहार न्यायालय का प्रकार";
            }
            else if (ddlnyayalaya.SelectedIndex == 3)
            {
                BindNyayalayaType_dist();
                BindNyayalayaType_SubDivision();
                divDist_nyayalaya_type.Visible = true;
                divSubdivision_nyayalaya_type.Visible = true;
            }
            else if (ddlnyayalaya.SelectedIndex == 4)
            {
                //BindNyayalayaType();
                //div_rajasw_vevhar_nyalay.Visible = true;
                //labNyayalaya_type.Text = "लोक शिकायत निवारण न्यायालय का प्रकार";
            }
            else
            {
                BindNyayalayaType();
                ddlnyayalaya_type.SelectedIndex = 0;
            }
        }



        protected void ddlnyayalaya_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            divDist_nyayalaya_type.Visible = false;
            divSubdivision_nyayalaya_type.Visible = false;
            //divVibhag_nyayalay_type.Visible = false;
            if (ddlnyayalaya_type.SelectedIndex == 1)
            {
                BindNyayalayaType_Vibhag();
                //divVibhag_nyayalay_type.Visible = true;
            }
            else if (ddlnyayalaya_type.SelectedIndex == 2)
            {
                BindNyayalayaType_SubDivision();
                divSubdivision_nyayalaya_type.Visible = true;
            }
            else if (ddlnyayalaya_type.SelectedIndex == 3)
            {
                BindNyayalayaType_dist();
                divDist_nyayalaya_type.Visible = true;
            }
        }


        protected void ddlDist_nyayalaya_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindNyayalayaType_SubDivision();
        }

        protected void DharaChanged(object sender, EventArgs e)
        {
            if (rdoOld.Checked)
            {
                divDhara.Visible = true;
                divbsn.Visible = false;
                divdhara1.Visible = false;
                div_tbnm.Visible = false;
                div_tdhara.Visible = false;
            }
            else if (rdoNew.Checked)
            {

                divDhara.Visible = false;
                divbsn.Visible = true;
                divdhara1.Visible = true;
            }
        }







        //------------------------------------------------------
        protected void ddlUserDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDistrict();
            ddlUserAreatype.SelectedIndex = 0;
        }

        private void RefreshDistrict()
        {
            BindSubDivision_wadi();
            BindBlock_Wadi();
            BindPolice_wadi();
            BindVillage_Wadi();
            BindPanchyat_Wadi();
            bindward_Wadi();
        }
        //-------------------------------------------------------------------
        protected void ddlUserSubdivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSubdivision();
            ddlUserAreatype.SelectedIndex = 0;
        }

        private void RefreshSubdivision()
        {
            BindBlock_Wadi();
            BindPolice_wadi();
            BindVillage_Wadi();
            BindPanchyat_Wadi();
            bindward_Wadi();
        }
        //----------------------------------------------------------------------
        protected void ddlUserBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshBlock();
            ddlUserAreatype_SelectedIndexChanged(sender, e);//---------need to correct
            ddlUserAreatype.SelectedIndex = 0;
        }

        private void RefreshBlock()
        {
            BindPolice_wadi();
            BindVillage_Wadi();
            BindPanchyat_Wadi();
            bindward_Wadi();
        }

        //---------------------------------------------------------


        //protected void ddlUserVillage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    divUserVillage.Attributes.Add("class", "col-md-12");
        //    //divUserVillage_Anya.Visible = false;
        //    if (ddlUserVillage.SelectedValue == "-1")
        //    {
        //        //divUserVillage.Attributes.Add("class", "col-md-5");
        //        //divUserVillage_Anya.Visible = true;
        //    }

        //    //ddlUserWard_SelectedIndexChanged(sender, e);
        //}

        //protected void ddlUserWard_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    divUserWard.Attributes.Add("class", "col-md-3");
        //    //divUserWard_Anya.Visible = false;
        //    if (ddlUserWard.SelectedValue == "-1")
        //    {
        //        // divUserWard.Attributes.Add("class", "col-md-5");
        //        //divUserWard_Anya.Visible = true;
        //    }
        //}
        //-----------------------------------------------------------------------------------------
        protected void ddlUserPanchyat_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPanchayat();
            //divUserPanchyat.Attributes.Add("class", "col-md-3");
            // divUserPanchyat_Anya.Visible = false;
            //if (ddlUserPanchyat.SelectedValue == "-1")
            //{
            //    //divUserPanchyat.Attributes.Add("class", "col-md-5");
            //    // divUserPanchyat_Anya.Visible = true;
            //}

            //ddlUserVillage_SelectedIndexChanged(sender, e);
            //ddlUserWard_SelectedIndexChanged(sender, e);
        }

        private void RefreshPanchayat()
        {
            BindVillage_Wadi();
            bindward_Wadi();
        }
        //----------------------------------------------------------------------------

        protected void ddlUserAreatype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserAreatype.SelectedIndex == 2)
            {
                labUVillage.Text = "नगर निकाय";
                divUserMohalla.Visible = true;
                divUserVillageCol.Visible = false;
                UWard.Visible = true;
            }
            else
            {
                labUVillage.Text = "ग्राम पंचायत";
                divUserMohalla.Visible = false;
                divUserVillageCol.Visible = true;
                UWard.Visible = false;
            }
            
            BindPanchyat_Wadi();
            RefreshPanchayat();

            //ddlUserVillage_SelectedIndexChanged(sender, e);
            //ddlUserWard_SelectedIndexChanged(sender, e);
            //ddlUserPanchyat_SelectedIndexChanged(sender, e);
        }


        protected void ddl_is_vadi_from_an_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_is_vadi_from_an_dept.SelectedValue == "Y")
            {
                divWVibhag_details.Visible = true;
                divWvibhaag_padanaam.Visible = true;
                ddl_is_vadi_from_an_org.SelectedValue = "N";
                ddl_is_vadi_from_an_org.Enabled = false;
                divWSanstha_details.Visible = false;
            }
            else if (ddl_is_vadi_from_an_dept.SelectedValue == "N")
            {
                divWVibhag_details.Visible = false;
                divWvibhaag_padanaam.Visible = false;
                ddl_is_vadi_from_an_org.SelectedValue = "0";
                ddl_is_vadi_from_an_org.Enabled = true;
                //divWSanstha_details.Visible = false;
            }
            else if (ddl_is_vadi_from_an_dept.SelectedValue == "0")
            {
                divWVibhag_details.Visible = false;
                divWvibhaag_padanaam.Visible = false;
                ddl_is_vadi_from_an_org.SelectedValue = "0";
                ddl_is_vadi_from_an_org.Enabled = true;
                divWSanstha_details.Visible = false;
            }
        }

        protected void ddl_is_vadi_from_an_org_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlWsanstha_naam.SelectedIndex = 0;
            txtWsanstha_padanaam.Text = "";
            divWSanstha_details.Visible = false;
            txtWsanstha_naam.Text = "";
            if (ddl_is_vadi_from_an_org.SelectedIndex == 1)
            {
                divWSanstha_details.Visible = true;
            }
        }

        protected void rptWadi_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                DataTable dt = ViewState["vadiDetails"] as DataTable;

                if (dt == null)
                    return;

                int index = Convert.ToInt32(e.CommandArgument);

                if (index >= 0 && index < dt.Rows.Count)
                {
                    dt.Rows.RemoveAt(index);

                    ViewState["vadiDetails"] = dt;

                    BindWadiRepeater();
                }
            }
        }

        protected void ddlareatype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlareatype.SelectedIndex == 2)
            {
                divVillage.Visible = false;
            }
            else
            {

                divVillage.Visible = true;
            }


            BindVillage();
            BindPanchyat();
            bindward();


        }

        protected void ddlPanchyat_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillage();
            bindward();

        }

        protected void ddlbhumitype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlbhumitype.SelectedIndex == 2)
            {
                divSarkaribhumitype.Visible = true;

                divSarkaribhumitype.Visible = true;
                ddlsarkaribhumitype.Enabled = true;

                //ddlsarkaribhumitype.SelectedIndex = 0;
            }
            else
            {
                divSarkaribhumitype.Visible = false;

                divSarkaribhumitype.Visible = false;
                ddlsarkaribhumitype.Enabled = false;
                // ddlsarkaribhumitype.Visible = false;
                //ddlsarkaribhumitype.SelectedIndex = 0;

            }

            RefreshSarkariBhumiType();
        }

        private void RefreshSarkariBhumiType()
        {
            ddlsarkaribhumitype_SelectedIndexChanged(ddlsarkaribhumitype, EventArgs.Empty);
        }

        protected void ddlsarkaribhumitype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsarkaribhumitype.SelectedValue == "6")
            {

                divsarkaribhumitype_Anya.Visible = true;
                divsarkaribhumitype_Anya.Visible = true;
                txtsarkaribhumitype_Anya.Visible = true;
                txtsarkaribhumitype_Anya.Enabled = true;
                txtsarkaribhumitype_Anya.Text = "";
            }
            else
            {

                divsarkaribhumitype_Anya.Visible = false;
                divsarkaribhumitype_Anya.Visible = false;
                txtsarkaribhumitype_Anya.Visible = false;
                txtsarkaribhumitype_Anya.Enabled = false;
                txtsarkaribhumitype_Anya.Text = "";

            }
        }

        protected void ddlbhumivivadtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlbhumivivadtype.SelectedValue == "20")
            {

                divBhumivivad_Anya.Visible = true;
                divBhumivivad_Anya.Visible = true;

                txtbhumivivad_Anya.Enabled = true;
                txtbhumivivad_Anya.Text = "";

            }
            else
            {

                divBhumivivad_Anya.Visible = false;
                divBhumivivad_Anya.Visible = false;

                txtbhumivivad_Anya.Enabled = false;
                txtbhumivivad_Anya.Text = "";

            }
        }

        //---------------Step2-pratiwadi-section---------------------------

        protected void ddlPDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubDivision_Pratiwadi();
            BindBlock_Pratiwadi();
            BindPolice_Prtiwadi();
            BindVillage_Pratiwadi();
            BindPanchyat_Prtiwadi();
            bindward_Pratiwadi();
            ddlPAreatype.SelectedIndex = 0;
        }


        protected void ddlPSubdivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBlock_Pratiwadi();
            BindPolice_Prtiwadi();
            BindVillage_Pratiwadi();
            BindPanchyat_Prtiwadi();
            bindward_Pratiwadi();
            ddlPAreatype.SelectedIndex = 0;
        }

        protected void ddlPBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPolice_Prtiwadi();
            BindVillage_Pratiwadi();
            BindPanchyat_Prtiwadi();
            bindward_Pratiwadi();

            ddlPAreatype.SelectedIndex = 0;

            LoadAreaTypeControls();
        }

        protected void ddlPAreatype_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAreaTypeControls();
        }

        private void LoadAreaTypeControls()
        {
            if (ddlPAreatype.SelectedValue == "U")
            {
                divPMohalla.Visible = true;
                divPVillageCol.Visible = false;
            }
            else
            {
                divPMohalla.Visible = false;
                divPVillageCol.Visible = true;
            }

            BindVillage_Pratiwadi();
            BindPanchyat_Prtiwadi();
            bindward_Pratiwadi();

            LoadVillageControls();
            LoadWardControls();
            LoadPanchayatControls();
        }

        protected void ddlPPanchyat_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillage_Pratiwadi();
            bindward_Pratiwadi();

            LoadPanchayatControls();
            LoadVillageControls();
            LoadWardControls();
        }

        protected void ddlPVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVillageControls();

            LoadWardControls();
        }

        protected void ddlPWard_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadWardControls();
        }


        private void LoadPanchayatControls()
        {
            divPPanchyat_Anya.Visible = (ddlPPanchyat.SelectedValue == "-1");
        }

        private void LoadVillageControls()
        {
            divPVillage_Anya.Visible = (ddlPVillage.SelectedValue == "-1");
        }

        private void LoadWardControls()
        {
            divPWard_Anya.Visible = false;
            if (ddlPWard.SelectedValue == "-1")
            {
                divPWard_Anya.Visible = true;
            }
        }

        protected void ddl_is_pratiVadi_from_an_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_is_pratiVadi_from_an_dept.SelectedValue == "Y")
            {
                divPVibhag_details.Visible = true;
                divPVibhag_details2.Visible = true;
                ddlPvibhaag_naam.SelectedValue = "0";
                txtPvibhaag_padanaam.Text = "";
                ddl_is_pratiVadi_from_an_org.SelectedValue = "N";
                ddl_is_pratiVadi_from_an_org.Enabled = false;
                divPSanstha_details.Visible = false;
            }
            else if (ddl_is_pratiVadi_from_an_dept.SelectedValue == "N")
            {
                divPVibhag_details.Visible = false;
                divPVibhag_details2.Visible = false;
                ddl_is_pratiVadi_from_an_org.SelectedValue = "0";
                ddl_is_pratiVadi_from_an_org.Enabled = true;
            }
            else if (ddl_is_pratiVadi_from_an_dept.SelectedValue == "0")
            {
                divPVibhag_details.Visible = false;
                divPVibhag_details2.Visible = false;
                ddl_is_pratiVadi_from_an_org.SelectedValue = "0";
                ddl_is_pratiVadi_from_an_org.Enabled = true;
                divPSanstha_details.Visible = false;
            }
        }

        protected void ddl_is_pratiVadi_from_an_org_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPsanstha_naam.SelectedIndex = 0;
            txtPsanstha_padanaam.Text = "";
            divPSanstha_details.Visible = false;


            if (ddl_is_pratiVadi_from_an_org.SelectedIndex == 1)
            {
                divPSanstha_details.Visible = true;

            }


        }

        protected void ddlwadi_pratiwadi_sunwai_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlKiskeduwara_bhejagaya.SelectedIndex = 0;
            txtsunwaiHetuNoticKaKaran.Text = "";
            divSuchana_ka_tamila.Visible = false;
            divSuchana_ka_upasthiti.Visible = false;
            ddlSuchana_ka_tamila.SelectedIndex = 0;
            ddlSuchana_ka_upasthiti.SelectedIndex = 0;

            if (ddlwadi_pratiwadi_sunwai.SelectedIndex == 1)
            {
                ddlKiskeduwara_bhejagaya.Visible = true;
                txtsunwaiHetuNoticKaKaran.Visible = false;
                labNotice.Text = "माध्यम";

                divSuchana_ka_tamila.Visible = true;
            }
            else if (ddlwadi_pratiwadi_sunwai.SelectedIndex == 2)
            {
                ddlKiskeduwara_bhejagaya.Visible = false;
                txtsunwaiHetuNoticKaKaran.Visible = true;
                labNotice.Text = "कारण स्पष्ट करें";

            }
            else
            {
                ddlKiskeduwara_bhejagaya.Visible = false;
                txtsunwaiHetuNoticKaKaran.Visible = false;
                labNotice.Text = "";


            }
        }

        protected void ddlSuchana_ka_tamila_SelectedIndexChanged(object sender, EventArgs e)
        {
            divSuchana_ka_upasthiti.Visible = true;
            ddlSuchana_ka_upasthiti.SelectedIndex = 0;
            if (ddlSuchana_ka_tamila.SelectedIndex == 1)
            {
                divSuchana_ka_upasthiti.Visible = true;
            }
        }


        //-----------add and remove Record in view step for first button click.It does not go to database

        //--------------validatinig step-1---------------------------------
        public bool ValidateVadiDetail()
        {
            if (string.IsNullOrWhiteSpace(txtNamePerAadhaar.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया वादी का नाम अंकित करें...!');", true);
                txtNamePerAadhaar.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFName.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया पिता/ पति का नाम अंकित करें...!');", true);
                txtFName.Focus();
                return false;
            }

            if (ddlgender.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया लिंग चुनें...!');", true);
                ddlgender.Focus();
                return false;
            }
            if (ddlUserDist.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया जिला चुनें...!');", true);
                ddlUserDist.Focus();
                return false;
            }

            if (ddlUserSubdivision.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया अनुमंडल चुनें...!');", true);
                ddlUserSubdivision.Focus();
                return false;
            }

            if (ddlUserBlock.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया अंचल चुनें...!');", true);
                ddlUserBlock.Focus();
                return false;
            }

            if (ddlUserThana.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया थाना चुनें...!');", true);
                ddlUserThana.Focus();
                return false;
            }

            if (ddlUserAreatype.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया क्षेत्र का प्रकार चुनें...!');", true);
                ddlUserAreatype.Focus();
                return false;
            }

            if (ddlUserPanchyat.SelectedIndex == 0)
            {

                if (labUVillage.Text == "ग्राम पंचायत")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया ग्राम पंचायत चुनें...!');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया नगर निकाय चुनें...!');", true);
                }
                ddlUserPanchyat.Focus();
                return false;
            }
            if (ddlUserAreatype.SelectedIndex == 1 && ddlUserVillage.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया राजस्व ग्राम चुनें...!');", true);
                ddlUserVillage.Focus();
                return false;
            }
            if (ddlUserAreatype.SelectedIndex == 2 && ddlUserWard.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया वार्ड चुनें...!');", true);
                ddlUserWard.Focus();
                return false;
            }
            //if (string.IsNullOrWhiteSpace(txtUserMohalla.Text))
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया मोहल्ला संख्या अंकित करें...!');", true);
            //    txtUserMohalla.Focus();
            //    return false;
            //}

            if (string.IsNullOrWhiteSpace(txtvadimobile.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया मोबाइल संख्या अंकित करें...!');", true);
                txtvadimobile.Focus();
                return false;
            }

            if (txtvadimobile.Text.Length != 10)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please Enter valid mobile no...!');", true);
                txtvadimobile.Focus();
                return false;
            }
            if (ddl_is_vadi_from_an_dept.SelectedIndex == 0 || ddl_is_vadi_from_an_dept.SelectedIndex == 1)
            {
                if (ddl_is_vadi_from_an_dept.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('क्या वादी किसी विभाग का प्रतिनिधि है कृपया चुनें...!');", true);
                    ddl_is_vadi_from_an_dept.Focus();
                    return false;
                }
                if (ddlWvibhaag_naam.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया विभाग का नाम चुनें...!');", true);
                    ddlWvibhaag_naam.Focus();
                    return false;
                }
            }
            if (ddl_is_vadi_from_an_org.SelectedIndex == 0 || ddl_is_vadi_from_an_org.SelectedIndex == 1)
            {
                if (ddl_is_vadi_from_an_org.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('क्या वादी किसी संस्था का प्रतिनिधि है कृपया चुनें...!');", true);
                    ddl_is_vadi_from_an_org.Focus();
                    return false;
                }
                if (ddlWsanstha_naam.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया संस्था का प्रकार चुनें...!');", true);
                    ddlWsanstha_naam.Focus();
                    return false;
                }
                if (ddlWsanshaanya_naam.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया संस्था का सम्बन्ध चुनें...!');", true);
                    ddlWsanshaanya_naam.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtWsanstha_naam.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया संस्था का नाम अंकित करें...!');", true);
                    txtWsanstha_naam.Focus();
                    return false;
                }
            }
            return true;
        }

        protected void btnAddVadiDetail_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            if (!Page.IsValid)
            {
                return;
            }

            if (!ValidateVadiDetail())
            {
                return;
            }

            try
            {
                DataTable dt = ViewState["vadiDetails"] as DataTable;

                if (dt == null)
                {
                    lblMsg.Text = "ViewState[vadiDetails] is NULL";
                    return;
                }

                DataRow dr = dt.NewRow();

                #region Basic Information

                dr["vadi_Name"] = txtNamePerAadhaar.Text.Trim();

                dr["Vadi_Father_Husband_Name"] = txtFName.Text.Trim();

                dr["NameAsPerAadhaar"] = txtNamePerAadhaar.Text.Trim();

                dr["AadharNo"] = "";

                dr["YearOfBirthAsPerAadhaar"] = ddlYear.SelectedValue == "0" ? (object)DBNull.Value : Convert.ToInt32(ddlYear.SelectedValue);

                dr["SexAsPerAadhaar"] = ddlgender.SelectedValue;

                dr["Vadi_MobileNo"] = txtvadimobile.Text.Trim();

                dr["IsVerifyAadhaa"] = "N";

                #endregion

                #region Department

                dr["is_vadi_from_an_dept"] = ddl_is_vadi_from_an_dept.SelectedValue;

                dr["vadi_dept_id"] = ddlWvibhaag_naam.SelectedValue == "0" ? "" : ddlWvibhaag_naam.SelectedValue;

                dr["vadi_dept_name"] = ddlWvibhaag_naam.SelectedItem?.Text ?? "";

                dr["vadi_dept_pad_name"] = txtWvibhaag_padanaam.Text.Trim();

                #endregion

                #region Organization

                dr["is_vadi_from_an_org"] = ddl_is_vadi_from_an_org.SelectedValue;

                dr["vadi_org_type"] = ddlWsanstha_naam.SelectedValue == "0" ? (object)DBNull.Value: Convert.ToInt32(ddlWsanstha_naam.SelectedValue);

                dr["vadi_org_name"] = txtWsanstha_naam.Text.Trim();

                dr["vadi_org_pad_name"] = txtWsanstha_padanaam.Text.Trim();

                dr["sanstha_sambandh_type"] = ddlWsanshaanya_naam.SelectedValue == "0" ? (object)DBNull.Value : Convert.ToInt32(ddlWsanshaanya_naam.SelectedValue);

                #endregion

                #region Address Codes

                dr["Vadi_District_Code"] = ddlUserDist.SelectedValue;
                dr["Vadi_Sub_DivCode"] = ddlUserSubdivision.SelectedValue;
                dr["Vadi_Block_Code"] = ddlUserBlock.SelectedValue;
                dr["Vadi_Thana_code"] = ddlUserThana.SelectedValue;
                dr["Vadi_AreaType"] = ddlUserAreatype.SelectedValue;
                dr["Vadi_Panchayat_Code"] = ddlUserPanchyat.SelectedValue;
                dr["Vadi_Village_Code"] = ddlUserVillage.SelectedValue;
                dr["Vadi_WardNo"] = ddlUserWard.SelectedValue;

                dr["Vadi_Panchayat_Anya"] = txtUserPanchyat_Anya.Text.Trim();
                dr["Vadi_Village_Anya"] = txtUserVillage_Anya.Text.Trim();
                dr["Vadi_WardNo_Anya"] = txtUserWard_Anya.Text.Trim();
                dr["Mohalla"] = txtUserMohalla.Text.Trim();

                #endregion

                #region Display Columns (For Repeater Only)

                dr["DistrictName"] = ddlUserDist.SelectedItem?.Text ?? "";
                dr["SubdivisionName"] = ddlUserSubdivision.SelectedItem?.Text ?? "";
                dr["BlockName"] = ddlUserBlock.SelectedItem?.Text ?? "";
                dr["ThanaName"] = ddlUserThana.SelectedItem?.Text ?? "";
                dr["AreaTypeName"] = ddlUserAreatype.SelectedItem?.Text ?? "";
                dr["PanchayatName"] = ddlUserPanchyat.SelectedItem?.Text ?? "";
                dr["VillageName"] = ddlUserVillage.SelectedItem?.Text ?? "";
                dr["WardName"] = ddlUserWard.SelectedItem?.Text ?? "";

                dr["OrgTypeName"] =  ddl_is_vadi_from_an_dept.SelectedValue == "Y" ? ddlWvibhaag_naam.SelectedItem?.Text ?? "" : ddl_is_vadi_from_an_org.SelectedValue == "Y"  ? ddlWsanstha_naam.SelectedItem?.Text ?? "" : "";

                dr["AssociationName"] = ddlWsanshaanya_naam.SelectedItem?.Text ?? "";

                #endregion

                dt.Rows.Add(dr);

                ViewState["vadiDetails"] = dt;

                BindWadiRepeater();

                hfwadiprint.Value = "Printstep1";

                //pnlupdate1.Update();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString().Replace(Environment.NewLine, "<br/>");
            }
        }


        private void BindWadiRepeater()
        {
            rptWadi.DataSource = ViewState["vadiDetails"] as DataTable;
            rptWadi.DataBind();
        }

        private DataTable vadiDetails()
        {
            DataTable dt = new DataTable();

            #region Database Columns (Values to Save)

            dt.Columns.Add("vadi_Name", typeof(string));

            dt.Columns.Add("is_vadi_from_an_org", typeof(string));
            dt.Columns.Add("vadi_org_type", typeof(int));              // Organization Type Id
            dt.Columns.Add("vadi_org_name", typeof(string));
            dt.Columns.Add("vadi_org_pad_name", typeof(string));

            dt.Columns.Add("is_vadi_from_an_dept", typeof(string));
            dt.Columns.Add("vadi_dept_id", typeof(string));            // Department Id
            dt.Columns.Add("vadi_dept_name", typeof(string));          // Department Name
            dt.Columns.Add("vadi_dept_pad_name", typeof(string));

            dt.Columns.Add("Vadi_Father_Husband_Name", typeof(string));
            dt.Columns.Add("NameAsPerAadhaar", typeof(string));
            dt.Columns.Add("AadharNo", typeof(string));

            dt.Columns.Add("YearOfBirthAsPerAadhaar", typeof(int));
            dt.Columns.Add("SexAsPerAadhaar", typeof(string));

            dt.Columns.Add("Vadi_District_Code", typeof(string));
            dt.Columns.Add("Vadi_Sub_DivCode", typeof(string));
            dt.Columns.Add("Vadi_Block_Code", typeof(string));
            dt.Columns.Add("Vadi_Thana_code", typeof(string));
            dt.Columns.Add("Vadi_AreaType", typeof(string));
            dt.Columns.Add("Vadi_Panchayat_Code", typeof(string));
            dt.Columns.Add("Vadi_Village_Code", typeof(string));
            dt.Columns.Add("Vadi_WardNo", typeof(string));

            dt.Columns.Add("Vadi_MobileNo", typeof(string));
            dt.Columns.Add("IsVerifyAadhaa", typeof(string));

            dt.Columns.Add("Vadi_Panchayat_Anya", typeof(string));
            dt.Columns.Add("Vadi_Village_Anya", typeof(string));
            dt.Columns.Add("Vadi_WardNo_Anya", typeof(string));
            dt.Columns.Add("Mohalla", typeof(string));

            dt.Columns.Add("sanstha_sambandh_type", typeof(int));      // Relation Id

            #endregion


            #region Display Columns (Used Only in Repeater)

            dt.Columns.Add("DistrictName", typeof(string));
            dt.Columns.Add("SubdivisionName", typeof(string));
            dt.Columns.Add("BlockName", typeof(string));
            dt.Columns.Add("ThanaName", typeof(string));
            dt.Columns.Add("AreaTypeName", typeof(string));
            dt.Columns.Add("PanchayatName", typeof(string));
            dt.Columns.Add("VillageName", typeof(string));
            dt.Columns.Add("WardName", typeof(string));

            dt.Columns.Add("OrgTypeName", typeof(string));             // संस्था का प्रकार
            dt.Columns.Add("AssociationName", typeof(string));         // संस्था का सम्बन्ध

            #endregion

            return dt;
        }

       

    }
}

