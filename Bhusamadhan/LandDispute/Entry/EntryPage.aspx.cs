using Bhusamadhan.DataAccessLayer.LandDisputeDAL;
using Bhusamadhan.DB;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
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
        string commCode = "";
        string userid = "";

        DBHelper objDBHelper = new DBHelper();
        string connectionString = DBConHelper.GetConnectionString();
        private readonly MatterRegistrationDAL _matterDAL = new MatterRegistrationDAL();

        //private readonly VadiDetailDAL _vadiDAL = new VadiDetailDAL();

        private readonly SaveStep2DAL _step2DAL = new SaveStep2DAL();

        private readonly SaveStep3DAL _step3DAL = new SaveStep3DAL();

        private readonly SaveStep4DAL _step4DAL = new SaveStep4DAL();

        private readonly SaveStep5DAL _step5DAL = new SaveStep5DAL();

        private readonly SaveStep6DAL _step6DAL = new SaveStep6DAL();

        private readonly SaveStep7DAL _step7DAL = new SaveStep7DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["UserLogIn"] as DataTable;

            if (dt == null || dt.Rows.Count != 1)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            DataRow row = dt.Rows[0];

            // Get values from Session
            string distVal = row["District_Code"].ToString();
            string subdivVal = row["Sub_DivCode"].ToString();
            string blockVal = row["Block_Code"].ToString();
            string thanaCode = row["Thana_Code"].ToString();

            commCode = row["Commsionary_Code"].ToString();
            userid = row["UserID"].ToString();

        
            //-------------- BindBlock() uses thanacode
            thanacode = thanaCode;


            if (!IsPostBack)
            {
                
                //ApplicationId = GetDraftApplicationId();


                LoadMasterData();


                if (ddlDistrict.Items.FindByValue(distVal) != null)
                {
                    ddlDistrict.SelectedValue = distVal;
                }

                ddlDistrict.Enabled = false;


                BindSubDivision();
                if (ddlSubdivision.Items.FindByValue(subdivVal) != null)
                {
                    ddlSubdivision.SelectedValue = subdivVal;
                }

                ddlSubdivision.Enabled = false;


                BindBlock();


                if (ddlBlock.Items.FindByValue(blockVal) != null)
                {
                    ddlBlock.SelectedValue = blockVal;
                }

                ddlBlock.Enabled = false;

                BindPolice();
                if (ddlPolice.Items.FindByValue(thanaCode) != null)
                {
                    ddlPolice.SelectedValue = thanaCode;
                }

                if (ddlPolice.SelectedValue.Trim() != "0")
                {
                    ddlPolice.Enabled = false;
                }



                //------------- Existing Application / Wizard logic
                long selectedApplicationId = 0;

                if (Request.QueryString["a_id"] != null)
                {
                    long.TryParse( Request.QueryString["a_id"], out selectedApplicationId );
                }

                if (selectedApplicationId > 0)
                {
                    // Resume selected unfinalized application
                    ApplicationId = selectedApplicationId;

                    CurrentStep = GetCurrentStep(ApplicationId);

                    LoadVadiDetails(ApplicationId);

                    DisplayApplicationInfo();
                }
                else
                {
                   
                    ApplicationId = 0;

                    CurrentStep = 1;

                    ViewState["vadiDetails"] = CreateVadiTable();
                }

                ShowStep(CurrentStep);

                //if (ApplicationId > 0)
                //{
                //    CurrentStep = GetCurrentStep(ApplicationId);

                //    LoadVadiDetails(ApplicationId);

                //    DisplayApplicationInfo();
                //}
                //else
                //{
                //    CurrentStep = 1;

                //    ViewState["vadiDetails"] = CreateVadiTable();
                //}

                //ShowStep(CurrentStep);
            }
        }


        private void LoadMasterData()
        {
            //---------Step 1 wadi/pratiwadi--------
            AdharYearsBind();
            BindDist_Wadi_Pratiwadi();
            BindSubDivision_wadi();
            //BindSubDivision();
            BindBlock_Wadi();
            BindPolice_wadi();
            //BindPolice();

            //BindBlock();
           
            
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

            //-----------Step7---------------------

            bind_BhumiSanvedanshilta();
        }

        //---------------------Basic Steps-----------------------------
        private long ApplicationId
        {
            get
            {
                return Session["ApplicationId"] == null ? 0 : Convert.ToInt64(Session["ApplicationId"]);
            }
            set
            {
                Session["ApplicationId"] = value;
            }
        }

        private long GetDraftApplicationId()
        {
            long applicationId = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@" SELECT TOP (1) a_id FROM BS_Matter_Registration WHERE CUUser=@UserID  AND Final = 0 ORDER BY a_id DESC", con);

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

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@" SELECT ISNULL(CurrentStep,1) FROM BS_Matter_Registration WHERE a_id=@a_id and CUUser=@UserID AND ISNULL(Final, 0) = 0", con);

                cmd.Parameters.AddWithValue("@a_id", applicationId);
                cmd.Parameters.AddWithValue("@UserID", userid);

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

                    if (ApplicationId > 0)
                    {
                        FillStep1(ApplicationId);
                    }
                    break;

                case 2:
                    pnlStep2.Visible = true;
                    if (ApplicationId > 0)
                    {
                        FillStep2(ApplicationId);
                    }
                    break;

                case 3:
                    pnlStep3.Visible = true;
                    if (ApplicationId > 0)
                    {
                        FillStep3(ApplicationId);
                    }
                    break;

                case 4:
                    pnlStep4.Visible = true;
                    if (ApplicationId > 0)
                    {
                        FillStep4(ApplicationId);
                    }
                    break;

                case 5:
                    pnlStep5.Visible = true;
                    if (ApplicationId > 0)
                    {
                        FillStep5(ApplicationId);
                     }
                    break;

                case 6:
                    pnlStep6.Visible = true;
                    if (ApplicationId > 0)
                    {
                        FillStep6(ApplicationId);
                    }
                    break;

                case 7:
                    pnlStep7.Visible = true;
                    if (ApplicationId > 0)
                    {
                        FillStep7(ApplicationId);
                    }
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

        //---------------Butto previous & next ------------------------
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
            bool result = false;

            switch (CurrentStep)
            {
                case 1:
                    Page.Validate("2");

                    if (!Page.IsValid)
                    {
                        return;
                    }


                    result = SaveStep1();
                    break;

                case 2:
                    Page.Validate("3");

                    if (!Page.IsValid)
                        return;

                    result = SaveStep2();
                    break;

                case 3:
                    //Page.Validate("4");

                    //if (!Page.IsValid)
                    //    return;

                    result = SaveStep3();
                    break;

                case 4:
                    //Page.Validate("5");

                    //if (!Page.IsValid)
                    //    return;

                    result = SaveStep4();
                    break;

                case 5:
                    //Page.Validate("6");

                    //if (!Page.IsValid)
                    //    return;

                    result = SaveStep5();
                    break;

                case 6:
                    //Page.Validate("7");

                    //if (!Page.IsValid)
                    //    return;

                    result = SaveStep6();
                    break;

                case 7:
                    Page.Validate("8");

                    if (!Page.IsValid)
                        return;

                    result = SaveStep7();
                    if (result)
                    {
                        Response.Redirect("~/LandDispute/Entry/ApplicationPreview.aspx?a_id=" + ApplicationId);
                    }

                    return;
            }

            if (result)
            {
                CurrentStep = GetCurrentStep(ApplicationId);

                ShowStep(CurrentStep);
            }
        }

        //=========================Global Methods==============================================

        private void DisplayApplicationInfo()
        {
            if (ApplicationId > 0)
            {
                divDraftInfo.Visible = true;
                lblApplicationId.Text = ApplicationId.ToString();
            }
            else
            {
                divDraftInfo.Visible = false;
            }
        }

        private string GetUserIP()
        {
            string iMainpaddress = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(iMainpaddress))
            {
                return iMainpaddress.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }

        private string InsSaveFile(string fileName, FileUpload fuFile, string a_id)
        {
            string uploadDirectory = string.Empty;
            string pdfpath = FileSaveServer.getBase64(fuFile);
            string extension = string.Empty;
            extension = Path.GetExtension(fuFile.FileName).ToLower();
            string fn = "ID" + a_id;
            uploadDirectory = "~/LandDoc/Upload/" + fn + "/";
            string resi = FileSaveServer.InsertPDFNew(uploadDirectory, pdfpath, fileName, extension);
            // Utility.showMessage(Page, resi);
            return resi;
            //return uploadDirectory + fileName + extension;
        }

        private string InsSaveFile(string fileName, FileUpload fuFile, string a_id, string path)
        {
            string uploadDirectory = string.Empty;
            string pdfpath = FileSaveServer.getBase64(fuFile);
            string extension = string.Empty;
            extension = Path.GetExtension(fuFile.FileName).ToLower();
            uploadDirectory = path;
            string resi = FileSaveServer.InsertPDFNew(uploadDirectory, pdfpath, fileName, extension);
            //if (resi == "0")
            //{
            //    return resi;
            //}
            //else
            //{
            //    return uploadDirectory + fileName + extension;
            //}
            return resi;
        }

        private string GetDocumentServerUrl(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return string.Empty;

            string baseUrl =
                ConfigurationManager.AppSettings["DocumentServer"];

            if (string.IsNullOrWhiteSpace(baseUrl))
                return string.Empty;

            baseUrl = baseUrl.TrimEnd('/');



            filePath = filePath.Trim().Replace("~", "");

            if (!filePath.StartsWith("/"))
                filePath = "/" + filePath;

            return baseUrl + filePath;
        }


        //=======================================================================================


        //---------------------------Step 1 form entry ---------------------------------------------------


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
        private DataTable CreateVadiTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("vadi_Name");
            dt.Columns.Add("is_vadi_from_an_org");
            dt.Columns.Add("vadi_org_type");
            dt.Columns.Add("vadi_org_name");
            dt.Columns.Add("vadi_org_pad_name");
            dt.Columns.Add("is_vadi_from_an_dept");
            dt.Columns.Add("vadi_dept_name");
            dt.Columns.Add("vadi_dept_pad_name");
            dt.Columns.Add("Vadi_Father_Husband_Name");
            dt.Columns.Add("NameAsPerAadhaar");
            dt.Columns.Add("AadharNo");
            dt.Columns.Add("YearOfBirthAsPerAadhaar");
            dt.Columns.Add("SexAsPerAadhaar");
            dt.Columns.Add("Vadi_District_Code");
            dt.Columns.Add("Vadi_Sub_DivCode");
            dt.Columns.Add("Vadi_Block_Code");
            dt.Columns.Add("Vadi_Thana_code");
            dt.Columns.Add("Vadi_AreaType");
            dt.Columns.Add("Vadi_Panchayat_Code");
            dt.Columns.Add("Vadi_Village_Code");
            dt.Columns.Add("Vadi_WardNo");
            dt.Columns.Add("Vadi_MobileNo");
            dt.Columns.Add("IsVerifyAadhaa");
           
            dt.Columns.Add("Vadi_Panchayat_Anya");
            dt.Columns.Add("Vadi_Village_Anya");
            dt.Columns.Add("Vadi_WardNo_Anya");
            dt.Columns.Add("mohalla");
            dt.Columns.Add("sanstha_sambandh_type");

            //--------------------------------------------------
            // Display columns
            //--------------------------------------------------

            dt.Columns.Add("DistrictName");
            dt.Columns.Add("SubDivisionName");
            dt.Columns.Add("BlockName");
            //dt.Columns.Add("ThanaName");
            dt.Columns.Add("AreaTypeName");
            dt.Columns.Add("PanchayatName");
            dt.Columns.Add("VillageName");
            dt.Columns.Add("WardName");


            return dt;
        }
        
        protected void btnAddVadiDetail_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            if (!Page.IsValid)
            {
                return;
            }

            //if (!ValidateVadiDetail())
            //{
            //    return;
            //}

            try
            {
                DataTable dt = ViewState["vadiDetails"] as DataTable;
            
                //if (dt == null)
                //{
                //    lblMsg.Text = "ViewState[vadiDetails] is NULL";
                //    return;
                //}


                DataRow dr = dt.NewRow();


                dr["vadi_Name"] = txtNamePerAadhaar.Text.Trim();

                dr["is_vadi_from_an_org"] = ddl_is_vadi_from_an_org.SelectedValue.ToString();

                dr["vadi_org_type"] = ddlWsanstha_naam.SelectedValue.ToString();

                dr["vadi_org_name"] = txtWsanstha_naam.Text.Trim();

                dr["vadi_org_pad_name"] = txtWsanstha_padanaam.Text.Trim();

                dr["is_vadi_from_an_dept"] = ddl_is_vadi_from_an_dept.SelectedValue.ToString();

                dr["vadi_dept_name"] = ddlWvibhaag_naam.SelectedValue.ToString();

                dr["vadi_dept_pad_name"] = txtWvibhaag_padanaam.Text.Trim();

                dr["Vadi_Father_Husband_Name"] = txtFName.Text.Trim();

                dr["NameAsPerAadhaar"] = txtNamePerAadhaar.Text.Trim();

                dr["AadharNo"] = "";

                dr["YearOfBirthAsPerAadhaar"] = ddlYear.SelectedValue.ToString();

                dr["SexAsPerAadhaar"] = ddlgender.SelectedValue.ToUpper();

                dr["Vadi_District_Code"] = ddlUserDist.SelectedValue.ToString();
              

                dr["Vadi_Sub_DivCode"] = ddlUserSubdivision.SelectedValue.ToString();
               

                dr["Vadi_Block_Code"] = ddlUserBlock.SelectedValue.ToString();
              
                dr["Vadi_Thana_code"] = ddlUserThana.SelectedValue.ToString();
                
                dr["Vadi_AreaType"] = ddlUserAreatype.SelectedValue.ToString();
                
                dr["Vadi_Panchayat_Code"] = ddlUserPanchyat.SelectedValue.ToString();
              
                dr["Vadi_Village_Code"] = ddlUserVillage.SelectedValue.ToString();
               
                dr["Vadi_WardNo"] = ddlUserWard.SelectedValue.ToString();
               
                dr["Vadi_MobileNo"] = txtvadimobile.Text.Trim();

                dr["IsVerifyAadhaa"] = 'N';

                dr["Vadi_Panchayat_Anya"] = txtUserPanchyat_Anya.Text.Trim();

                dr["Vadi_Village_Anya"] = txtUserVillage_Anya.Text.Trim();

                dr["Vadi_WardNo_Anya"] = txtUserWard_Anya.Text.Trim();

                dr["mohalla"] = txtUserMohalla.Text.Trim();

                dr["sanstha_sambandh_type"] = ddlWsanshaanya_naam.SelectedValue.ToString();

                //------------------------Display Column----------------------

                dr["DistrictName"] = ddlUserDist.SelectedItem.Text;

                dr["SubDivisionName"] = ddlUserSubdivision.SelectedItem.Text;

                dr["BlockName"] = ddlUserBlock.SelectedItem.Text;

                //dr["ThanaName"] = ddlUserThana.SelectedItem.Text;

                dr["AreaTypeName"] = ddlUserAreatype.SelectedValue == "R" ? "ग्रामीण" : "शहरी";

                dr["PanchayatName"] = ddlUserPanchyat.SelectedItem.Text;

                dr["VillageName"] = ddlUserVillage.SelectedItem.Text;

                dr["WardName"] = ddlUserWard.SelectedItem.Text;

                dt.Rows.Add(dr);

                ViewState["vadiDetails"] = dt;

                BindWadiRepeater();

                ClearVadiFields();

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
            }
        }

        public void ClearVadiFields()
        {
            txtNamePerAadhaar.Text = "";
            ddl_is_vadi_from_an_org.SelectedIndex = 0;
            ddlWsanstha_naam.SelectedIndex = 0;

            txtWsanstha_naam.Text = "";

            txtWsanstha_padanaam.Text = "";
            ddl_is_vadi_from_an_dept.SelectedIndex = 0;
            ddlWvibhaag_naam.SelectedIndex = 0;

            txtWvibhaag_padanaam.Text = "";

            txtFName.Text = "";

            txtNamePerAadhaar.Text = "";

            ddlYear.SelectedIndex = 0;
            ddlgender.SelectedIndex = 0;
            ddlUserDist.SelectedIndex = 0;
            ddlUserSubdivision.SelectedIndex = 0;
            ddlUserBlock.SelectedIndex = 0;
            ddlUserThana.SelectedIndex = 0;
            ddlUserAreatype.SelectedIndex = 0;

            ddlUserPanchyat.SelectedIndex = 0;
            ddlUserVillage.SelectedIndex = 0;

            ddlUserWard.SelectedIndex = 0;

            txtvadimobile.Text = "";

            txtUserPanchyat_Anya.Text = "";

            txtUserVillage_Anya.Text = "";

            txtUserWard_Anya.Text = "";

            txtUserMohalla.Text = "";

            ddlWsanshaanya_naam.SelectedIndex = 0;
        }

        private void LoadVadiDetails(long applicationId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(@"select * from BS_VW_GetVadi_Step1 WHERE a_id=@a_id", con);

                da.SelectCommand.Parameters.AddWithValue("@a_id", applicationId);

                da.Fill(dt);
            }

            ViewState["vadiDetails"] = dt;

            rptWadi.DataSource = dt;
            rptWadi.DataBind();
        }

        private DataTable GetVadiDetails()
        {
            if (ViewState["vadiDetails"] == null)
            {
                ViewState["vadiDetails"] = CreateVadiTable();
            }

            return (DataTable)ViewState["vadiDetails"];
        }

        private void BindWadiRepeater()
        {
            rptWadi.DataSource = ViewState["vadiDetails"] as DataTable;
            rptWadi.DataBind();


        }

        protected void rptWadi_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "Remove")
                return;

            DataTable dt = GetVadiDetails();

            int index = Convert.ToInt32(e.CommandArgument);

            dt.Rows.RemoveAt(index);

            ViewState["vadiDetails"] = dt;

            BindWadiRepeater();
        }

        private bool SaveStep1()
        {
            //if (!ValidateStep1())
            //    return false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlTransaction trans = con.BeginTransaction();

                try
                {

                    string guid = "";

                    if (ApplicationId == 0)
                    {
                        guid = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        guid = _matterDAL.GetApplicationGuid(ApplicationId, con, trans);

                        if (string.IsNullOrWhiteSpace(guid))
                            guid = Guid.NewGuid().ToString();
                    }

                    string Vadi_sakshya_FilePath = string.Empty;
                    string Prativadi_sakshya_FilePath = string.Empty;

                    string uploadFolder = $"~/LandDoc/Upload/ID{guid}";


                    if (ApplicationId > 0)
                    {
                        DataRow dr = _matterDAL.GetUploadedFiles(ApplicationId, con, trans);

                        if (dr != null)
                        {
                            Vadi_sakshya_FilePath = dr["Vadi_sakshya_File"].ToString();
                            Prativadi_sakshya_FilePath = dr["Prativadi_sakshya_File"].ToString();
                        }
                    }

                    //-------------------- Validate --------------------//

                    if (AppDoc.HasFile)
                    {
                        if (FileUploadValidator.IsPdf(AppDoc.PostedFile, 1024, 1024) != "OK")
                        {
                            lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                            return false;
                        }

                        Vadi_sakshya_FilePath = $"{uploadFolder}/Vadi_sakshya_File.pdf";

                        string savedPath = InsSaveFile("Vadi_sakshya_File", AppDoc, guid);

                        if (savedPath == "0" || savedPath != Vadi_sakshya_FilePath)
                        {
                            lblMsg.Text = "Technical Error while uploading Vadi document.";
                            return false;
                        }
                    }

                    if (PrativadiDoc.HasFile)
                    {
                        if (FileUploadValidator.IsPdf(PrativadiDoc.PostedFile, 1024, 1024) != "OK")
                        {
                            lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                            return false;
                        }

                        Prativadi_sakshya_FilePath = $"{uploadFolder}/Prativadi_sakshya_File.pdf";

                        string savedPath = InsSaveFile("Prativadi_sakshya_File", PrativadiDoc, guid);

                        if (savedPath == "0" || savedPath != Prativadi_sakshya_FilePath)
                        {
                            lblMsg.Text = "Technical Error while uploading Prativadi document.";
                            return false;
                        }
                    }



                    //long applicationId = _matterDAL.SaveStep1(ApplicationId, Convert.ToInt32(commCode), txtrajaswa_sankhya.Text.Trim(), Convert.ToInt32(ddlbhumitype.SelectedValue), Convert.ToInt32(ddlsarkaribhumitype.SelectedValue), txtsarkaribhumitype_Anya.Text.Trim(), Convert.ToInt32(ddlbhumivivadtype.SelectedValue), txtbhumivivad_Anya.Text.Trim(), Convert.ToInt32(ddl_vivad_adyatan_sthiti.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlSubdivision.SelectedValue), Convert.ToInt32(ddlBlock.SelectedValue), Convert.ToInt32(ddlPolice.SelectedValue), Convert.ToInt32(ddlPanchyat.SelectedValue), txtPanchyat_Anya.Text.Trim(), ddlareatype.SelectedValue, Convert.ToInt32(ddlVillage.SelectedValue), txtVillage_Anya.Text.Trim(), Convert.ToInt32(ddlWard.SelectedValue), txtWard_Anya.Text.Trim(), Vadi_sakshya_FilePath, Prativadi_sakshya_FilePath, Convert.ToDateTime(txtAwadenKiTithi.Text.ToString()), txtVadiVivarani.Text.Trim(), txtPrativadiVivarani.Text.Trim(), guid, userid, GetUserIP().ToString(), con, trans);

                    long applicationId = _matterDAL.SaveStep1( ApplicationId, Convert.ToInt32(commCode),txtrajaswa_sankhya.Text.Trim(),  Convert.ToInt32(ddlbhumitype.SelectedValue),  Convert.ToInt32(ddlsarkaribhumitype.SelectedValue), txtsarkaribhumitype_Anya.Text.Trim(),  Convert.ToInt32(ddlbhumivivadtype.SelectedValue), txtbhumivivad_Anya.Text.Trim(),  Convert.ToInt32(ddl_vivad_adyatan_sthiti.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlSubdivision.SelectedValue), Convert.ToInt32(ddlBlock.SelectedValue), Convert.ToInt32(ddlPolice.SelectedValue),  Convert.ToInt32(ddlPanchyat.SelectedValue),  txtPanchyat_Anya.Text.Trim(),  ddlareatype.SelectedValue,  Convert.ToInt32(ddlVillage.SelectedValue), txtVillage_Anya.Text.Trim(), Convert.ToInt32(ddlWard.SelectedValue), txtWard_Anya.Text.Trim(), Vadi_sakshya_FilePath, Prativadi_sakshya_FilePath, Convert.ToDateTime(txtAwadenKiTithi.Text), txtVadiVivarani.Text.Trim(), txtPrativadiVivarani.Text.Trim(), guid, userid, GetUserIP().ToString(), con, trans);


                    // Store in Session

                    ApplicationId = applicationId;


                    // Save Vadi

                    DataTable dtVadi = GetVadiDetails();
                    //DataTable dtVadi = GetVadiDetailsForSave();

                    if (dtVadi == null || dtVadi.Rows.Count == 0)
                    {
                        trans.Rollback();

                        lblMsg.Text = "कृपया पहले 'Save' बटन दबाकर कम से कम एक वादी जोड़ें।";

                        return false;
                    }

                    //-------- Create a filtered copy of GetVadiDetails()--------------------
                    DataTable dtForDb = dtVadi.DefaultView.ToTable(false,
                                    "vadi_Name",
                                    "is_vadi_from_an_org",
                                    "vadi_org_type",
                                    "vadi_org_name",
                                    "vadi_org_pad_name",
                                    "is_vadi_from_an_dept",
                                    "vadi_dept_name",
                                    "vadi_dept_pad_name",
                                    "Vadi_Father_Husband_Name",
                                    "NameAsPerAadhaar",
                                    "AadharNo",
                                    "YearOfBirthAsPerAadhaar",
                                    "SexAsPerAadhaar",
                                    "Vadi_District_Code",
                                    "Vadi_Sub_DivCode",
                                    "Vadi_Block_Code",
                                    "Vadi_Thana_code",
                                    "Vadi_AreaType",
                                    "Vadi_Panchayat_Code",
                                    "Vadi_Village_Code",
                                    "Vadi_WardNo",
                                    "Vadi_MobileNo",
                                    "IsVerifyAadhaa",
                                    "Vadi_Panchayat_Anya",
                                    "Vadi_Village_Anya",
                                    "Vadi_WardNo_Anya",
                                    "mohalla",
                                    "sanstha_sambandh_type"
                   );


                    //_vadiDAL.SaveVadiDetails( applicationId, dtVadi,userid,  con, trans);
                    _matterDAL.SaveVadiDetails(applicationId, dtForDb, userid, con, trans);

                    // Update Current Step                 

                    _matterDAL.UpdateCurrentStep(applicationId, 2, con, trans);

                    DisplayApplicationInfo();
                    trans.Commit();

                    lblMsg.Text = "Step-1 saved successfully.";

                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();

                    lblMsg.Text = ex.Message;

                    return false;
                }
            }
        }

        private void FillStep1(long applicationId)
        {
            DataTable dt = _matterDAL.GetStep1(applicationId);

            if (dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];

            BindDist_Wadi_Pratiwadi();

            string districtCode = dr["District_Code"].ToString();

            if (ddlDistrict.Items.FindByValue(districtCode) != null)
            {
                ddlDistrict.SelectedValue = districtCode;
            }


            BindBlock();

            string blockCode = dr["Block_Code"].ToString();

            if (ddlBlock.Items.FindByValue(blockCode) != null)
            {
                ddlBlock.SelectedValue = blockCode;
            }

            string areaType = dr["AreaType"].ToString();

            if (ddlareatype.Items.FindByValue(areaType) != null)
            {
                ddlareatype.SelectedValue = areaType;
            }

            if (areaType == "R")
            {
                divPanchyat.Visible = true;
                divVillage.Visible = true;
                divWard.Visible = true;

                divPanchyat_Anya.Visible = false;
                divVillage_Anya.Visible = false;
                divWard_Anya.Visible = false;

              
                BindPanchyat();

                string panchayatCode = dr["Panchayat_Code"].ToString();

                if (ddlPanchyat.Items.FindByValue(panchayatCode) != null)
                {
                    ddlPanchyat.SelectedValue = panchayatCode;
                }

                BindVillage();

                string village = dr["Village"].ToString();

                if (ddlVillage.Items.FindByValue(village) != null)
                {
                    ddlVillage.SelectedValue = village;
                }



                bindward();

                string ward = dr["WardNo"].ToString();

                if (ddlWard.Items.FindByValue(ward) != null)
                {
                    ddlWard.SelectedValue = ward;
                }



                txtPanchyat_Anya.Text = dr["Panchayat_Anya"].ToString();
                txtVillage_Anya.Text = dr["Village_Anya"].ToString();
                txtWard_Anya.Text = dr["WardNo_Anya"].ToString();
            }
            else if (areaType == "U")
            {
               

                divPanchyat.Visible = false;
                divVillage.Visible = false;
                divWard.Visible = true;

                divPanchyat_Anya.Visible = false;
                divVillage_Anya.Visible = false;
                divWard_Anya.Visible = false;

             
                bindward();

                string ward = dr["WardNo"].ToString();

                if (ddlWard.Items.FindByValue(ward) != null)
                {
                    ddlWard.SelectedValue = ward;
                }

                txtWard_Anya.Text = dr["WardNo_Anya"].ToString();
            }


            //---------------------------------------------------------------
            string vivadAdyatanStithi =  dr["bhumi_vivad_ka_adyatan_sthiti"].ToString();

            if (ddl_vivad_adyatan_sthiti.Items.FindByValue(vivadAdyatanStithi) != null)
            {
                ddl_vivad_adyatan_sthiti.SelectedValue = vivadAdyatanStithi;
            }


            txtrajaswa_sankhya.Text = dr["rajasv_thaana_sankhya"].ToString();

           string bhumiType = dr["Bhumitype"].ToString();

            if (ddlbhumitype.Items.FindByValue(bhumiType) != null)
            {
                ddlbhumitype.SelectedValue = bhumiType;
            }

            if (ddlbhumitype.SelectedIndex == 2)
            {
                divSarkaribhumitype.Visible = true;
              
                ddlsarkaribhumitype.Enabled = true;
          
                ddlsarkaribhumitype.SelectedIndex = 0;
                ddlsarkaribhumitype.SelectedValue = dr["SarkariBhumiType"].ToString();
                ddlsarkaribhumitype_SelectedIndexChanged(this, EventArgs.Empty);
                if (ddlsarkaribhumitype.SelectedValue == "6")
                {
                    txtsarkaribhumitype_Anya.Text = dr["SarkariBhumiType_Anya"].ToString();
                }
            }
            else
            {
                divSarkaribhumitype.Visible = false;
             
                divSarkaribhumitype.Visible = false;
                ddlsarkaribhumitype.Enabled = false;
            
                ddlsarkaribhumitype.SelectedValue = dr["SarkariBhumiType"].ToString();

            }

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

            ddlbhumivivadtype.SelectedValue = dr["BhumiVivadType"].ToString();

            if (ddlbhumivivadtype.SelectedValue == "20")
            {

                divBhumivivad_Anya.Visible = true;
                divBhumivivad_Anya.Visible = true;
            
                txtbhumivivad_Anya.Enabled = true;
                txtbhumivivad_Anya.Text = "";
                txtbhumivivad_Anya.Text = dr["BhumiVivadType_Anya"].ToString();

            }
            else
            {
         
                divBhumivivad_Anya.Visible = false;
                divBhumivivad_Anya.Visible = false;
         
                txtbhumivivad_Anya.Enabled = false;
                txtbhumivivad_Anya.Text = "";

            }


            string bhumiVivadType = dr["BhumiVivadType"].ToString();

            if (ddlbhumivivadtype.SelectedValue == "20")
            {

                divBhumivivad_Anya.Visible = true;
                divBhumivivad_Anya.Visible = true;
            
                txtbhumivivad_Anya.Enabled = true;
                txtbhumivivad_Anya.Text = "";
                txtbhumivivad_Anya.Text = dr["BhumiVivadType_Anya"].ToString();

            }
            else
            {
              
                divBhumivivad_Anya.Visible = false;
                divBhumivivad_Anya.Visible = false;
       
                txtbhumivivad_Anya.Enabled = false;
                txtbhumivivad_Anya.Text = "";

            }

            txtbhumivivad_Anya.Text =  dr["BhumiVivadType_Anya"].ToString();

            if (bhumiVivadType == "OTHER")
            {
                divBhumivivad_Anya.Visible = true;
            }
            else
            {
                divBhumivivad_Anya.Visible = false;
            }

            txtAwadenKiTithi.Text = dr["AavedanKiTithi"].ToString();

            txtVadiVivarani.Text = dr["VadiVivarani"].ToString();
            txtPrativadiVivarani.Text = dr["PrativadiVivarani"].ToString();

            //----------------------------------------------------------------------


            string baseUrl = ConfigurationManager.AppSettings["DocumentServer"];

         
            baseUrl = baseUrl.TrimEnd('/');

        
            if (!string.IsNullOrWhiteSpace(dr["Vadi_sakshya_File"].ToString()))
            {
                string filePath = dr["Vadi_sakshya_File"].ToString();

                // Convert "~/LandDoc/Upload/..." to "/LandDoc/Upload/..."
                filePath = filePath.Replace("~", "");

                lnkAppDoc.HRef = baseUrl + filePath;
                lnkAppDoc.Target = "_blank";
                lnkAppDoc.Visible = true;
            }
            else
            {
                lnkAppDoc.Visible = false;
            }

            if (!string.IsNullOrWhiteSpace(dr["Prativadi_sakshya_File"].ToString()))
            {
                string filePath = dr["Prativadi_sakshya_File"].ToString();

                filePath = filePath.Replace("~", "");

                lnkPrativadiDoc.HRef = baseUrl + filePath;
                lnkPrativadiDoc.Target = "_blank";
                lnkPrativadiDoc.Visible = true;
            }
            else
            {
                lnkPrativadiDoc.Visible = false;
            }

            //------------------------------------

            LoadVadiDetails(applicationId);
        }

        //----------------Step1 complete-------------------------------------------------

        //---------------------------Step2------------------------------------------------------

        private DataTable CreatePratiVadiTable()
        {
            DataTable dt;

            if (ViewState["PratiVadiDetails"] == null)
            {
                dt = new DataTable();

                // Database fields
                dt.Columns.Add("pratiVadi_Name", typeof(string));
                dt.Columns.Add("is_pratiVadi_from_an_dept", typeof(string));
                dt.Columns.Add("pratiVadi_dept_name", typeof(long));
                dt.Columns.Add("pratiVadi_dept_pad_name", typeof(string));

                dt.Columns.Add("is_pratiVadi_from_an_org", typeof(string));
                dt.Columns.Add("pratiVadi_org_type", typeof(long));
                dt.Columns.Add("pratiVadi_org_name", typeof(string));
                dt.Columns.Add("pratiVadi_org_pad_name", typeof(string));

                dt.Columns.Add("pratiVadi_Father_Husband_Name", typeof(string));

                dt.Columns.Add("pratiVadi_District_Code", typeof(long));
                dt.Columns.Add("pratiVadi_Sub_DivCode", typeof(long));
                dt.Columns.Add("pratiVadi_Block_Code", typeof(long));
                dt.Columns.Add("pratiVadi_Thana_code", typeof(long));

                dt.Columns.Add("pratiVadi_AreaType", typeof(string));

                dt.Columns.Add("pratiVadi_Panchayat_Code", typeof(long));
                dt.Columns.Add("pratiVadi_Panchayat_Anya", typeof(string));

                dt.Columns.Add("pratiVadi_Village_Code", typeof(long));
                dt.Columns.Add("pratiVadi_Village_Anya", typeof(string));

                dt.Columns.Add("pratiVadi_WardNo", typeof(long));
                //---------------------------------
                dt.Columns.Add("pratiVadi_WardNo_Anya", typeof(string));
                //----------------------------------------
                dt.Columns.Add("pratiVadi_MobileNo", typeof(string));
                dt.Columns.Add("mohalla", typeof(string));

                dt.Columns.Add("sanstha_sambandh_type", typeof(int));

             
                // Display fields
                dt.Columns.Add("DistrictName", typeof(string));
                dt.Columns.Add("SubDivisionName", typeof(string));
                dt.Columns.Add("BlockName", typeof(string));
                dt.Columns.Add("AreaTypeName", typeof(string));
                dt.Columns.Add("PanchayatName", typeof(string));
                dt.Columns.Add("VillageName", typeof(string));
                dt.Columns.Add("WardName", typeof(string));

                ViewState["PratiVadiDetails"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["PratiVadiDetails"];
            }

            return dt;
        }
        protected void btnAddPratiVadiDetail_Click(object sender, EventArgs e)
        {
            Page.Validate("PratiVadi");
            if (!Page.IsValid)
                return;

            DataTable dt = CreatePratiVadiTable();

            DataRow dr = dt.NewRow();

            dr["pratiVadi_Name"] = txtPName.Text.Trim();

            dr["pratiVadi_Father_Husband_Name"] = txtPFName.Text.Trim();

            dr["pratiVadi_MobileNo"] = txtprativadi_Mobile.Text.Trim();


            dr["is_pratiVadi_from_an_dept"] = ddl_is_pratiVadi_from_an_dept.SelectedValue.ToString(); ;

            dr["pratiVadi_dept_name"] = ddlPvibhaag_naam.SelectedValue.ToString();

            dr["pratiVadi_dept_pad_name"] = txtPvibhaag_padanaam.Text.Trim();


            dr["is_pratiVadi_from_an_org"] = ddl_is_pratiVadi_from_an_org.SelectedValue.ToString();

            dr["pratiVadi_org_type"] = ddlPsanstha_naam.SelectedValue.ToString();

            dr["pratiVadi_org_name"] = txtPsanstha_naam.Text.Trim();

            dr["pratiVadi_org_pad_name"] = txtPsanstha_padanaam.Text.Trim();


            dr["pratiVadi_District_Code"] = ddlPDistrict.SelectedValue.ToString();

            dr["pratiVadi_Sub_DivCode"] = ddlPSubdivision.SelectedValue.ToString();

            dr["pratiVadi_Block_Code"] = ddlPBlock.SelectedValue.ToString();

            dr["pratiVadi_Thana_code"] = ddlPThana.SelectedValue.ToString();


            dr["pratiVadi_AreaType"] = ddlPAreatype.SelectedValue.ToString(); ;


            dr["pratiVadi_Panchayat_Code"] = ddlPPanchyat.SelectedValue.ToString();

            dr["pratiVadi_Panchayat_Anya"] = txtPPanchyat_Anya.Text.Trim();



            dr["pratiVadi_Village_Code"] = ddlPVillage.SelectedValue.ToString();

            dr["pratiVadi_Village_Anya"] = txtPVillage_Anya.Text.Trim();


            dr["pratiVadi_WardNo"] = ddlPWard.SelectedValue.ToString();
            //-------------------------
            dr["pratiVadi_WardNo_Anya"] = txtPWard_Anya.Text.Trim();
            //---------------------------------

            dr["mohalla"] = txtPMohalla.Text.Trim();

            dr["sanstha_sambandh_type"] = ddlPsanshaanya_naam.SelectedValue.ToString();

           
            //-------------- Display Names------------------------------------
           

            dr["DistrictName"] = ddlPDistrict.SelectedItem.Text;

            dr["SubDivisionName"] = ddlPSubdivision.SelectedItem.Text;

            dr["BlockName"] = ddlPBlock.SelectedItem.Text;

            dr["AreaTypeName"] = ddlPAreatype.SelectedItem.Text;

            dr["PanchayatName"] = ddlPPanchyat.SelectedItem.Text;

            dr["VillageName"] = ddlPVillage.SelectedItem.Text;

            dr["WardName"] = ddlPWard.SelectedItem.Text;

            dt.Rows.Add(dr);

            ViewState["PratiVadiDetails"] = dt;

            BindPratiVadiRepeater();

            ClearPratiVadiControls();
        }

        private void BindPratiVadiRepeater()
        {
            DataTable dt = CreatePratiVadiTable();

            Pratiwadi_repeater.DataSource = dt;
            Pratiwadi_repeater.DataBind();
        }

        private void ClearPratiVadiControls()
        {
            txtPName.Text = "";
            txtPFName.Text = "";
            txtprativadi_Mobile.Text = "";

            ddl_is_pratiVadi_from_an_dept.SelectedIndex = 0;
            ddlPvibhaag_naam.SelectedIndex = 0;
            txtPvibhaag_padanaam.Text = "";

            ddl_is_pratiVadi_from_an_org.SelectedIndex = 0;
            ddlPsanstha_naam.SelectedIndex = 0;
            txtPsanstha_naam.Text = "";
            txtPsanstha_padanaam.Text = "";

            ddlPDistrict.SelectedIndex = 0;
            ddlPSubdivision.SelectedIndex = 0;
            ddlPBlock.SelectedIndex = 0;
            ddlPThana.SelectedIndex = 0;

            ddlPAreatype.SelectedIndex = 0;

            ddlPPanchyat.SelectedIndex = 0;
            ddlPVillage.SelectedIndex = 0;
            ddlPWard.SelectedIndex = 0;

            txtPPanchyat_Anya.Text = "";
            txtPVillage_Anya.Text = "";
            txtPWard_Anya.Text = "";

            txtPMohalla.Text = "";

            ddlPsanshaanya_naam.SelectedIndex = 0;
        }

        protected void Pratiwadi_repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                DataTable dt = CreatePratiVadiTable();

                if (index >= 0 && index < dt.Rows.Count)
                {
                    dt.Rows.RemoveAt(index);
                    dt.AcceptChanges();

                    ViewState["PratiVadiDetails"] = dt;

                    BindPratiVadiRepeater();
                }
            }
        }

        private DataTable GetPratiVadiDetails()
        {
            if (ViewState["PratiVadiDetails"] == null)
            {
                ViewState["PratiVadiDetails"] = CreatePratiVadiTable();
            }

            return (DataTable)ViewState["PratiVadiDetails"];
        }

        private bool SaveStep2()
        {
            if (ApplicationId == 0)
            {
                lblMsg.Text = "Application not found.";
                return false;
            }


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlTransaction trans = con.BeginTransaction();


                DataTable dtPratiVadi = GetPratiVadiDetails();

                if (dtPratiVadi == null || dtPratiVadi.Rows.Count == 0)
                {
                    trans.Rollback();

                    lblMsg.Text = "कृपया पहले 'Save' बटन दबाकर कम से कम एक प्रतिवादी जोड़ें।";

                    return false;
                }

                //-------- Create a filtered copy of GetPratiVadiDetails()--------------------
                DataTable dtPratiVadiForDb = dtPratiVadi.DefaultView.ToTable(false,
                                       "pratiVadi_Name",
                                       "is_pratiVadi_from_an_dept",
                                       "pratiVadi_dept_name",
                                       "pratiVadi_dept_pad_name",

                                       "is_pratiVadi_from_an_org",
                                       "pratiVadi_org_type",
                                       "pratiVadi_org_name",
                                       "pratiVadi_org_pad_name",

                                       "pratiVadi_Father_Husband_Name",

                                       "pratiVadi_District_Code",
                                       "pratiVadi_Sub_DivCode",
                                       "pratiVadi_Block_Code",
                                       "pratiVadi_Thana_code",

                                       "pratiVadi_AreaType",

                                       "pratiVadi_Panchayat_Code",
                                       "pratiVadi_Village_Code",
                                       "pratiVadi_WardNo",
                                       "pratiVadi_MobileNo",

                                       "pratiVadi_Panchayat_Anya",                                       
                                       "pratiVadi_Village_Anya",
                                       "pratiVadi_WardNo_Anya",
                                       "mohalla",
                                       "sanstha_sambandh_type"
                                       );



                try
                {
                   //SaveStep2(long applicationId, string prativadiKoSuchitKiyaGayaHai, string givenInfoType, string givenInfoDesc, string prativadiKoSuchanaKaTaamilaPraaptHai, string prativadiUpasthitHuaHai, DataTable dtPratiVadiForDb, string userid, SqlConnection con, SqlTransaction trans)
                    long savedApplicationId = _step2DAL.SaveStep2 (ApplicationId, ddlwadi_pratiwadi_sunwai.SelectedValue, ddlKiskeduwara_bhejagaya.SelectedValue, txtsunwaiHetuNoticKaKaran.Text.Trim(), ddlSuchana_ka_tamila.SelectedValue, ddlSuchana_ka_upasthiti.SelectedValue, dtPratiVadiForDb, userid, con, trans);

                   
                    trans.Commit();

               
                    ApplicationId = savedApplicationId;
                    DisplayApplicationInfo();
                    lblMsg.Text = "Step-2 saved successfully.";


                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();

                    lblMsg.Text = ex.Message;

                    return false;
                }
            }
        }

        private void FillStep2(long applicationId)
        {
           
            DataTable dtDb =  _step2DAL.GetPratiVadiDetails(applicationId);

            ViewState["PratiVadiDetails"] = dtDb;

            Pratiwadi_repeater.DataSource = dtDb;
            Pratiwadi_repeater.DataBind();

            DataTable dtDbAnya = _step2DAL.GetPratiVadiAnyaVivranStep2(applicationId);

            if (dtDbAnya.Rows.Count == 0)
                return;

            DataRow dr = dtDbAnya.Rows[0];

          

            ddlwadi_pratiwadi_sunwai.SelectedValue = dr["prativadi_ko_suchit_kiya_gaya_hai"].ToString();
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
                div_sunwaiHetuNoticKaKaran.Visible = false;
                divSuchana_ka_upasthiti.Visible = true;
                divSuchana_ka_tamila.Visible = true;
            }
            else if (ddlwadi_pratiwadi_sunwai.SelectedIndex == 2)
            {
                ddlKiskeduwara_bhejagaya.Visible = false;
                txtsunwaiHetuNoticKaKaran.Visible = true;
                labNotice.Text = "कारण स्पष्ट करें";
                div_sunwaiHetuNoticKaKaran.Visible = true;
            }
            else
            {
                ddlKiskeduwara_bhejagaya.Visible = false;
                txtsunwaiHetuNoticKaKaran.Visible = false;
                labNotice.Text = "";
                div_sunwaiHetuNoticKaKaran.Visible = false;

            }

            if (ddlKiskeduwara_bhejagaya.Visible == true)
            {
                ddlKiskeduwara_bhejagaya.SelectedValue = dr["given_info_type"].ToString();

            }
            if (txtsunwaiHetuNoticKaKaran.Visible == true)
            {
                txtsunwaiHetuNoticKaKaran.Text = dr["given_info_desc"].ToString();

            }


            if (divSuchana_ka_tamila.Visible == true)
            {
                ddlSuchana_ka_tamila.SelectedValue = dr["prativadi_ko_suchana_ka_taamila_praapt_hai"].ToString();
            }
            if (ddlSuchana_ka_upasthiti.Visible == true)
            {
                ddlSuchana_ka_upasthiti.SelectedValue = dr["prativadi_upasthit_hua_hai"].ToString();

            }


        }

        //---------------------------Step2 complete------------------------------------------------------

        //---------------------------Step3 ------------------------------------------------------

        private DataTable CreateKhataKhesraVivarniTable()
        {
            DataTable dt;

            if (ViewState["KhataKhesraDetails"] == null)
            {
                dt = new DataTable();

                // Database fields
                dt.Columns.Add("khataNo", typeof(string));
                dt.Columns.Add("khesraNo", typeof(string));

                dt.Columns.Add("RakbaNo1", typeof(string));
                dt.Columns.Add("Rakba_unit1", typeof(int));
                dt.Columns.Add("RakbaNo2", typeof(string));
                dt.Columns.Add("Rakba_unit2", typeof(int));
                dt.Columns.Add("RakbaNo3", typeof(string));
                dt.Columns.Add("Rakba_unit3", typeof(int));

                dt.Columns.Add("LandTypesInKhatian", typeof(int));
                dt.Columns.Add("LandDetailsInKhatian", typeof(string));
                dt.Columns.Add("North_chauhaddee", typeof(string));
                dt.Columns.Add("South_chauhaddee", typeof(string));
                dt.Columns.Add("East_chauhaddee", typeof(string));
                dt.Columns.Add("West_chauhaddee", typeof(string));

                dt.Columns.Add("LandTypesInKhatianDesc", typeof(string));
                dt.Columns.Add("Rakba", typeof(string));

              
                ViewState["KhataKhesraDetails"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["KhataKhesraDetails"];
            }

            return dt;
        }

        protected void btnsaveBhumiKaVivaran_Click(object sender, EventArgs e)
        {

            Page.Validate("4");
            if (!Page.IsValid)
                return;

            DataTable dt = CreateKhataKhesraVivarniTable();

            DataRow dr = dt.NewRow();

            dr["khataNo"] = txtkhatasankhya.Text.Trim();

            dr["khesraNo"] = txtkhesarasankhya.Text.Trim();

            dr["RakbaNo1"] = txtrakabasankhya1.Text.Trim();

            dr["Rakba_unit1"] = ddlrakabaunit1.SelectedValue.ToString(); ;

            dr["RakbaNo2"] = txtrakabasankhya2.Text.Trim();

            dr["Rakba_unit2"] = ddlrakabaunit2.SelectedValue.ToString();

            dr["RakbaNo3"] = txtrakabasankhya3.Text.Trim();

            dr["Rakba_unit3"] = ddlrakabaunit3.SelectedValue.ToString();


            dr["LandTypesInKhatian"] = ddlkhatiyan_me_jaminvivran.SelectedValue.ToString();

            dr["LandDetailsInKhatian"] = txtkhatiyan_me_jaminvivran_text.Text.Trim();


            dr["North_chauhaddee"] = txtuttari_chohaddi.Text.Trim();

            dr["South_chauhaddee"] = txtdakshini_chohaddi.Text.Trim();

            dr["East_chauhaddee"] = txtpurvi_chohaddi.Text.Trim();

            dr["West_chauhaddee"] = txtpashchimi_chohaddi.Text.Trim();

            //------------display-------------------------------

            dr["Landdesciption"] = ddlkhatiyan_me_jaminvivran.SelectedItem.Text;
            dr["Rakba"] = txtrakabasankhya1.Text.Trim() + " " + ddlrakabaunit1.SelectedItem.ToString() + "," + txtrakabasankhya2.Text.Trim() + " " + ddlrakabaunit2.SelectedItem.ToString() + ", " + txtrakabasankhya3.Text.Trim() + "," + ddlrakabaunit3.SelectedItem.ToString();

            dt.Rows.Add(dr);

            ViewState["KhataKhesraDetails"] = dt;

            BindKhataKhesraRepeater();

            ClearKharaKhesraControls();

        }

        private void BindKhataKhesraRepeater()
        {
            DataTable dt = CreateKhataKhesraVivarniTable();

            rptKhataKhesraVivarni.DataSource = dt;
            rptKhataKhesraVivarni.DataBind();
        }

        private void ClearKharaKhesraControls()
        {
            txtkhatasankhya.Text = "";

            txtkhesarasankhya.Text = "";

            txtrakabasankhya1.Text = "";

            txtrakabasankhya1.Text = "";

            ddlrakabaunit1.SelectedIndex = 0;

            txtrakabasankhya2.Text = "";

            ddlrakabaunit2.SelectedIndex = 0;

            txtrakabasankhya3.Text = "";

            ddlrakabaunit3.SelectedIndex = 0;

            ddlkhatiyan_me_jaminvivran.SelectedIndex = 0;

            txtkhatiyan_me_jaminvivran_text.Text = "";

            txtuttari_chohaddi.Text = "";

            txtdakshini_chohaddi.Text = "";

            txtpurvi_chohaddi.Text = "";

            txtpashchimi_chohaddi.Text = "";
        }

        protected void rptKhataKhesraVivarni_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                DataTable dt = CreateKhataKhesraVivarniTable();

                if (index >= 0 && index < dt.Rows.Count)
                {
                    dt.Rows.RemoveAt(index);
                    dt.AcceptChanges();

                    ViewState["KhataKhesraDetails"] = dt;

                    BindKhataKhesraRepeater();
                }
            }
        }

        private DataTable GetKhataKhesraDetails()
        {
            if (ViewState["KhataKhesraDetails"] == null)
            {
                ViewState["KhataKhesraDetails"] = CreateKhataKhesraVivarniTable();
            }

            return (DataTable)ViewState["KhataKhesraDetails"];
        }
        private bool SaveStep3()
        {
            if (ApplicationId == 0)
            {
                lblMsg.Text = "Application not found.";
                return false;
            }


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlTransaction trans = con.BeginTransaction();


                DataTable dtKhataKhesra = GetKhataKhesraDetails();

                if (dtKhataKhesra == null || dtKhataKhesra.Rows.Count == 0)
                {
                    trans.Rollback();

                    lblMsg.Text = "कृपया पहले 'Save' बटन दबाकर खाता खेसरा का विवरण अंकित करें।";

                    return false;
                }

                //-------- Create a filtered copy of  CreateKhataKhesraVivarniTable()--------------------
                DataTable dtKhataKhesraForDb = dtKhataKhesra.DefaultView.ToTable(false,
                                                       "khataNo",
                                                        "khesraNo",
                                                        "RakbaNo1",
                                                        "Rakba_unit1",
                                                        "RakbaNo2",
                                                        "Rakba_unit2",
                                                        "RakbaNo3",
                                                        "Rakba_unit3",
                                                        "LandTypesInKhatian",
                                                        "LandDetailsInKhatian",
                                                        "North_chauhaddee",
                                                        "South_chauhaddee",
                                                        "East_chauhaddee",
                                                        "West_chauhaddee"

                                                        );


                try
                {
                    long savedApplicationId = _step3DAL.SaveStep3(ApplicationId, dtKhataKhesraForDb, userid, con, trans);

                    trans.Commit();

                    ApplicationId = savedApplicationId;

                    DisplayApplicationInfo();
                    lblMsg.Text = "Step-3 saved successfully.";

                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();

                    lblMsg.Text = ex.Message;

                    return false;
                }
            }
        }

        private void FillStep3(long applicationId)
        {

            DataTable dtDb = _step3DAL.GetKhataKhesraDetails(applicationId);

            ViewState["KhataKhesraDetails"] = dtDb;

            rptKhataKhesraVivarni.DataSource = dtDb;
            rptKhataKhesraVivarni.DataBind();


        }

        //------------------------------------------step4--------------------------------------------------------

        //---------------------------Vadi Evidence--------------------------------------

        private DataTable CreateVadiEvidenceDetailTable()
        {
            DataTable dt;

            if (ViewState["VadiEvidenceDetail"] == null)
            {
                dt = new DataTable();

                dt.Columns.Add("evidence_id", typeof(string));
                dt.Columns.Add("evidence_name", typeof(string));
                dt.Columns.Add("evidence_any_name", typeof(string));
                dt.Columns.Add("FullfileName", typeof(string));


                ViewState["VadiEvidenceDetail"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["VadiEvidenceDetail"];
            }

            return dt;
        }

        protected void rptVadiEvidence_ItemCommand(  object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int index;

                if (!int.TryParse(Convert.ToString(e.CommandArgument), out index))
                    return;

                DataTable dt = CreateVadiEvidenceDetailTable();

                if (index >= 0 && index < dt.Rows.Count)
                {
                    dt.Rows.RemoveAt(index);
                    dt.AcceptChanges();

                    ViewState["VadiEvidenceDetail"] = dt;

                    BindVadiEvidenceRepeater();
                }

                return;
            }

            if (e.CommandName == "View")
            {
             
                string filePath = Convert.ToString(e.CommandArgument);

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    lblMsg.Text = "दस्तावेज़ उपलब्ध नहीं है।";
                    return;
                }

             
                string baseUrl =  ConfigurationManager.AppSettings["DocumentServer"];

                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    lblMsg.Text = "Document Server उपलब्ध नहीं है।";
                    return;
                }

                baseUrl = baseUrl.TrimEnd('/');

                filePath = filePath.Trim().Replace("~", "");

                if (!filePath.StartsWith("/"))
                    filePath = "/" + filePath;

                string documentUrl = baseUrl + filePath;

               
                string script = "window.open('" +  HttpUtility.JavaScriptStringEncode(documentUrl) + "', '_blank');";

                ScriptManager.RegisterStartupScript( this, GetType(), "ViewVadiPdf_" + Guid.NewGuid().ToString("N"), script, true );

                return;
            }
        }

        private DataTable GetVadiEvidenceDetails()
        {
            if (ViewState["VadiEvidenceDetail"] == null)
            {
                ViewState["VadiEvidenceDetail"] = CreateVadiEvidenceDetailTable();
            }

            return (DataTable)ViewState["VadiEvidenceDetail"];
        }

        protected void btnAddVadiEvidenceDetail_Click(object sender, EventArgs e)
        {


            string ddlIsVadiEvi1 = ddlIsVadiEvi.SelectedValue.Trim();

            if (ddlIsVadiEvi1 == "0")
            {
                lblMsg.Text = "कृपया वादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ? चुनें...!";

                ddlIsVadiEvi.Focus();
                return;
            }

            if (ddlIsVadiEvi1 == "Y")
            {
                if (ddlVadiEvidenceType.SelectedIndex == 0)
                {
                    lblMsg.Text = "कृपया साक्ष्य का प्रकार चुनें...!";

                    ddlVadiEvidenceType.Focus();
                    return;
                }

                if (ddlVadiEvidenceType.SelectedValue == "9" &&
                    string.IsNullOrWhiteSpace(txtVadiEvidenceType.Text))
                {
                    lblMsg.Text = "कृपया अन्य साक्ष्य का प्रकार अंकित करें...!";

                    txtVadiEvidenceType.Focus();
                    return;
                }

                if (!file_vadi_dastavej_new.HasFile)
                {
                    lblMsg.Text = "कृपया दस्तावेज़ चुनें...!";

                    file_vadi_dastavej_new.Focus();
                    return;
                }
            }


            if (file_vadi_dastavej_new.HasFile)
            {
                string fileExtension =
                    Path.GetExtension(file_vadi_dastavej_new.FileName).ToLower();

                if (fileExtension != ".pdf")
                {
                    lblMsg.Text = "केवल PDF फ़ाइल अपलोड करें।";

                    file_vadi_dastavej_new.Focus();
                    return;
                }

                if (file_vadi_dastavej_new.PostedFile.ContentLength >
                    (3 * 1024 * 1024))
                {
                    lblMsg.Text = "फ़ाइल का आकार अधिकतम 3 MB होना चाहिए।";

                    file_vadi_dastavej_new.Focus();
                    return;
                }
            }


            long a_id = ApplicationId;

            if (a_id <= 0)
            {
                lblMsg.Text = "आवेदन की पहचान उपलब्ध नहीं है। कृपया पुनः प्रयास करें।";

                return;
            }



            DataTable dt = CreateVadiEvidenceDetailTable();

            int rowNo = dt.Rows.Count + 1;

            string fileName = "VadiEvidence" + rowNo;

            string uploadDirectory = "~/LandDoc/Upload/VadiEvidence" + a_id + "/";

            string vadiEvidenceFile = string.Empty;


            //------------------- Upload PDF through ImageWebService


            if (file_vadi_dastavej_new.HasFile)
            {
                string fileUploadResult = InsSaveFile(fileName, file_vadi_dastavej_new, a_id.ToString(), uploadDirectory);

                string expectedFilePath = uploadDirectory + fileName + ".pdf";

                if (fileUploadResult == "0" || string.IsNullOrWhiteSpace(fileUploadResult))
                {
                    lblMsg.Text = "दस्तावेज़ अपलोड करने में तकनीकी समस्या हुई।";

                    return;
                }

                if (fileUploadResult != expectedFilePath)
                {
                    lblMsg.Text = "दस्तावेज़ अपलोड करने में तकनीकी समस्या हुई।";

                    return;
                }

                vadiEvidenceFile = expectedFilePath;
            }

            string evidenceId = ddlVadiEvidenceType.SelectedValue.Trim();

            string evidenceName = evidenceId != "9" ? ddlVadiEvidenceType.SelectedItem.Text.Trim() : txtVadiEvidenceType.Text.Trim();

            string evidenceAnyName = evidenceId == "9" ? txtVadiEvidenceType.Text.Trim() : string.Empty;

            dt.Rows.Add(evidenceId, evidenceName, evidenceAnyName, vadiEvidenceFile);

            ViewState["VadiEvidenceDetail"] = dt;

            BindVadiEvidenceRepeater();

            txtVadiEvidenceType.Text = string.Empty;

            ddlVadiEvidenceType.SelectedIndex = 0;

            divtxtVadiEvidenceType.Visible = false;

            lblMsg.Text = "रिकॉर्ड सफलतापूर्वक जोड़ा गया।";
        }

        private void BindVadiEvidenceRepeater()
        {
            DataTable dt = CreateVadiEvidenceDetailTable();

            rptVadiEvidence.DataSource = dt;
            rptVadiEvidence.DataBind();
        }


        //---------------------------Prativadi Evidence--------------------------------------
        private DataTable CreatePrativadiEvidenceDetailTable()
        {
            DataTable dt;

            if (ViewState["PratiVadiEvidenceDetail"] == null)
            {
                dt = new DataTable();

                dt.Columns.Add("evidence_id", typeof(string));
                dt.Columns.Add("evidence_name", typeof(string));
                dt.Columns.Add("evidence_any_name", typeof(string));
                dt.Columns.Add("FullfileName", typeof(string));


                ViewState["PratiVadiEvidenceDetail"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["PratiVadiEvidenceDetail"];
            }

            return dt;
        }

        protected void btnAddPrativadiEvidenceDetail_Click(object sender, EventArgs e)
        {

            string ddlIsPvadiEvi1 = ddlIsPvadiEvi.SelectedValue.Trim();

            if (ddlIsPvadiEvi1 == "0")
            {
                lblMsg.Text = "कृपया प्रतिवादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ? चुनें...!";

                ddlIsPvadiEvi.Focus();
                return;
            }

            if (ddlIsPvadiEvi1 == "Y")
            {
                if (ddlPrativadiEvidenceType.SelectedIndex == 0)
                {
                    lblMsg.Text = "कृपया साक्ष्य का प्रकार चुनें...!";

                    ddlPrativadiEvidenceType.Focus();
                    return;
                }

                if (ddlPrativadiEvidenceType.SelectedValue == "9" && string.IsNullOrWhiteSpace(txtPrativadiEvidenceType.Text))
                {
                    lblMsg.Text = "कृपया अन्य साक्ष्य का प्रकार अंकित करें...!";

                    txtPrativadiEvidenceType.Focus();
                    return;
                }


                if (!file_Prativadi_dastavej_new.HasFile)
                {
                    lblMsg.Text = "कृपया दस्तावेज़ चुनें...!";

                    file_Prativadi_dastavej_new.Focus();
                    return;
                }
            }


            if (file_Prativadi_dastavej_new.HasFile)
            {
                string fileExtension = Path.GetExtension(file_Prativadi_dastavej_new.FileName).ToLower();

                if (fileExtension != ".pdf")
                {
                    lblMsg.Text = "केवल PDF फ़ाइल अपलोड करें।";

                    file_Prativadi_dastavej_new.Focus();
                    return;
                }

                if (file_Prativadi_dastavej_new.PostedFile.ContentLength > (3 * 1024 * 1024))
                {
                    lblMsg.Text = "फ़ाइल का आकार अधिकतम 3 MB होना चाहिए।";

                    file_Prativadi_dastavej_new.Focus();
                    return;
                }
            }


            long a_id = ApplicationId;

            if (a_id <= 0)
            {
                lblMsg.Text = "आवेदन की पहचान उपलब्ध नहीं है। कृपया पुनः प्रयास करें.";

                return;
            }


            DataTable dt = CreatePrativadiEvidenceDetailTable();

            int rowNo = dt.Rows.Count + 1;

            string fileName = "PrativadiEvidence" + rowNo;

            string uploadDirectory = "~/LandDoc/Upload/PrativadiEvidence" + a_id + "/";

            string prativadiEvidenceFile = string.Empty;


            //---------------------Upload PDF through ImageWebService


            if (file_Prativadi_dastavej_new.HasFile)
            {
                string fileUploadResult = InsSaveFile(fileName, file_Prativadi_dastavej_new, a_id.ToString(), uploadDirectory);

                string expectedFilePath = uploadDirectory + fileName + ".pdf";

                if (fileUploadResult == "0" || string.IsNullOrWhiteSpace(fileUploadResult))
                {
                    lblMsg.Text = "दस्तावेज़ अपलोड करने में तकनीकी समस्या हुई।";

                    return;
                }

                if (fileUploadResult != expectedFilePath)
                {
                    lblMsg.Text = "दस्तावेज़ अपलोड करने में तकनीकी समस्या हुई।";

                    return;
                }

                prativadiEvidenceFile = expectedFilePath;
            }


            string evidenceId = ddlPrativadiEvidenceType.SelectedValue.Trim();

            string evidenceName = evidenceId != "9" ? ddlPrativadiEvidenceType.SelectedItem.Text.Trim() : txtPrativadiEvidenceType.Text.Trim();

            string evidenceAnyName = evidenceId == "9" ? txtPrativadiEvidenceType.Text.Trim() : string.Empty;


            dt.Rows.Add(evidenceId, evidenceName, evidenceAnyName, prativadiEvidenceFile);

            ViewState["PrativadiEvidenceDetail"] = dt;

            BindPrativadiEvidenceRepeater();

            txtPrativadiEvidenceType.Text = string.Empty;

            ddlPrativadiEvidenceType.SelectedIndex = 0;

            divtxtPrativadiEvidenceType.Visible = false;

            lblMsg.Text = "रिकॉर्ड सफलतापूर्वक जोड़ा गया।";
        }

        private void BindPrativadiEvidenceRepeater()
        {
            DataTable dt = CreatePrativadiEvidenceDetailTable();

            rptPrativadiEvidence.DataSource = dt;
            rptPrativadiEvidence.DataBind();
        }

        protected void rptPrativadiEvidence_ItemCommand( object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int index;

                if (!int.TryParse(Convert.ToString(e.CommandArgument), out index))
                    return;

                DataTable dt = CreatePrativadiEvidenceDetailTable();

                if (index >= 0 && index < dt.Rows.Count)
                {
                    dt.Rows.RemoveAt(index);
                    dt.AcceptChanges();

                    ViewState["PratiVadiEvidenceDetail"] = dt;

                    BindPrativadiEvidenceRepeater();
                }

                return;
            }

            if (e.CommandName == "View")
            {
               
                string filePath = Convert.ToString(e.CommandArgument);

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    lblMsg.Text = "दस्तावेज़ उपलब्ध नहीं है।";
                    return;
                }

               
                string baseUrl =ConfigurationManager.AppSettings["DocumentServer"];

                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    lblMsg.Text = "Document Server उपलब्ध नहीं है।";
                    return;
                }

                baseUrl = baseUrl.TrimEnd('/');

                filePath = filePath.Trim().Replace("~", "");

                if (!filePath.StartsWith("/"))
                    filePath = "/" + filePath;

                string documentUrl = baseUrl + filePath;

                string script = "window.open('" + HttpUtility.JavaScriptStringEncode(documentUrl) + "', '_blank');";

                ScriptManager.RegisterStartupScript(  this, GetType(), "ViewPrativadiPdf_" + Guid.NewGuid().ToString("N"), script, true  );

                return;
            }
        }

        private DataTable GetPratiVadiEvidenceDetails()
        {
            if (ViewState["PratiVadiEvidenceDetail"] == null)
            {
                ViewState["PratiVadiEvidenceDetail"] = CreatePrativadiEvidenceDetailTable();
            }

            return (DataTable)ViewState["PratiVadiEvidenceDetail"];
        }
        private bool SaveStep4()
        {
            if (ApplicationId == 0)
            {
                lblMsg.Text = "Application not found.";
                return false;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlTransaction trans = null;

                try
                {
                    con.Open();

                    trans = con.BeginTransaction();

                   
               
                    DataTable dtVadiEvidence = GetVadiEvidenceDetails();

                    if (dtVadiEvidence == null || dtVadiEvidence.Rows.Count == 0)
                    {
                        trans.Rollback();

                        lblMsg.Text = "कृपया पहले 'Save' बटन दबाकर वादी साक्ष्य का विवरण अंकित करें।";

                        return false;
                    }

                    // ------------------------------------------------
                    // Create filtered Vadi DataTable for database
                    // ------------------------------------------------
                    DataTable dtVadiEvidenceForDb = dtVadiEvidence.DefaultView.ToTable(
                            false,
                            "evidence_id",
                            "evidence_any_name",
                            "FullfileName"
                        );

                    dtVadiEvidenceForDb.Columns["evidence_any_name"] .ColumnName = "evidence_anya";

                    dtVadiEvidenceForDb.Columns["FullfileName"] .ColumnName = "Vadi_sakshya_File";

                    DataTable dtPrativadiEvidence = GetPratiVadiEvidenceDetails();

                    if (dtPrativadiEvidence == null || dtPrativadiEvidence.Rows.Count == 0)
                    {
                        trans.Rollback();

                        lblMsg.Text = "कृपया पहले 'Save' बटन दबाकर प्रतिवादी साक्ष्य का विवरण अंकित करें।";

                        return false;
                    }

                    // ------------------------------------------------
                    // Create filtered Prativadi DataTable for database
                    // ------------------------------------------------
                    DataTable dtPrativadiEvidenceForDb = dtPrativadiEvidence.DefaultView.ToTable(
                            false,
                            "evidence_id",
                            "evidence_any_name",
                            "FullfileName"
                        );

                    dtPrativadiEvidenceForDb.Columns["evidence_any_name"] .ColumnName = "evidence_anya";

                    dtPrativadiEvidenceForDb.Columns["FullfileName"].ColumnName = "Prativadi_sakshya_File";

                    long applicationId = _step4DAL.SaveStep4(  ApplicationId, dtVadiEvidenceForDb, dtPrativadiEvidenceForDb, userid, con, trans);

                 
                    trans.Commit();

                 
                    ApplicationId = applicationId;

                    DisplayApplicationInfo();
                    lblMsg.Text = "Step-4 saved successfully.";

                    return true;
                }
                catch (Exception ex)
                {
                    if (trans != null)
                        trans.Rollback();

                    lblMsg.Text = ex.Message;

                    return false;
                }
            }
        }

        private void FillStep4(long applicationId)
        {
           

            DataTable dtVadiDb = _step4DAL.GetVadiEvidenceDetails(applicationId);

            DataTable dtVadi = CreateVadiEvidenceDetailTable();

            // Remove existing ViewState rows
            dtVadi.Clear();

            foreach (DataRow row in dtVadiDb.Rows)
            {
                DataRow newRow = dtVadi.NewRow();

                newRow["evidence_id"] = row["evidence_id"];
                newRow["evidence_any_name"] = row["evidence_any_name"];
                newRow["FullfileName"] = row["FullfileName"];

                // evidence_name is a UI column.
                // It is not currently returned by the database query.
                newRow["evidence_name"] = "";

                dtVadi.Rows.Add(newRow);
            }

            ViewState["VadiEvidenceDetail"] = dtVadi;

            rptVadiEvidence.DataSource = dtVadi;
            rptVadiEvidence.DataBind();


            DataTable dtPrativadiDb = _step4DAL.GetPrativadiEvidenceDetails(applicationId);

            DataTable dtPrativadi = CreatePrativadiEvidenceDetailTable();

            dtPrativadi.Clear();

            foreach (DataRow row in dtPrativadiDb.Rows)
            {
                DataRow newRow = dtPrativadi.NewRow();

                newRow["evidence_id"] = row["evidence_id"];
                newRow["evidence_any_name"] = row["evidence_any_name"];
                newRow["FullfileName"] = row["FullfileName"];

                // UI column
                newRow["evidence_name"] = "";

                dtPrativadi.Rows.Add(newRow);
            }

            ViewState["PratiVadiEvidenceDetail"] = dtPrativadi;

            rptPrativadiEvidence.DataSource = dtPrativadi;
            rptPrativadiEvidence.DataBind();
        }

        //-----------------------------step4 complete here-------------------------------------------


        //-----------------------------------------Step5---------------------------------------------------

        bool validateFile(FileUpload fuFile, string FileType)
        {
            if (fuFile.HasFile)
            {
                int contentLength = fuFile.PostedFile.ContentLength;
                string extension = Path.GetExtension(fuFile.PostedFile.FileName);
                long maxFileSize = 5000000;

                string mimeType = fuFile.PostedFile.ContentType;
                string allowedMimeType = "application/pdf";

                if (mimeType == allowedMimeType)
                {
                    switch (FileType)
                    {
                        case "zip":
                            switch (extension.ToLower())
                            {
                                case ".zip":
                                    break;
                                default:
                                    lblMsg.Text = "This file type is not allowed.";
                                    // ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
                                    return false;
                            }

                            if (contentLength > (1 * 1024 * 1024))
                            {
                                lblMsg.Text = "File size must be less than or equal to 3 MB";
                                return false;
                            }
                            break;
                        case "doc":

                            switch (extension.ToLower())
                            {
                                //case ".jMD":
                                //case ".jpeg":
                                case ".pdf":

                                    break;
                                default:
                                    lblMsg.Text = "This file type is not allowed.";
                                    // ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
                                    return false;
                            }
                            if (contentLength > (3 * 1024 * 1024))
                            {
                                lblMsg.Text = "File size must be less than or equal to 3 MB";
                                return false;
                            }
                            break;
                        case "Image":

                            switch (extension.ToLower())
                            {

                                case ".png":
                                case ".PNG":
                                case ".jpg":
                                case ".JPG":
                                case ".jpeg":
                                case ".JPEG":


                                    break;
                                default:
                                    lblMsg.Text = "This file type is not allowed.";
                                    // ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
                                    return false;
                            }
                            if (contentLength > (0.4 * 1024 * 1024))
                            {
                                lblMsg.Text = "File size must be less than or equal to 400KB";
                                return false;
                            }
                            break;



                        default:
                            lblMsg.Text = "Unknown File Type !!";
                            return false;
                    }
                }
                else
                {

                    lblMsg.Text = "Invalid file type. Only PDF files are allowed.";
                    return false;
                }
            }
            return true;
        }
        private bool ValidateStep5()
        {
            if (ddlbhukhand_mapi.SelectedIndex == 0)
            {
                lblMsg.Text = "कृपया विवादित भू-खंड की मापी चुनें...";
                ddlbhukhand_mapi.Focus();
                return false;
            }

            if (ddlbhukhand_mapi.SelectedIndex == 1 &&  ddlbhukhand_Copy.SelectedIndex == 0)
            {
                lblMsg.Text = "कृपया विवादित भू-खंड की मापी चुनें...";
                ddlbhukhand_Copy.Focus();
                return false;
            }

            if (ddlbhukhand_mapi.SelectedIndex == 1 &&  ddlbhukhand_Copy.SelectedIndex == 2 &&  string.IsNullOrWhiteSpace(txtMapiKeNirdharit_tithi.Text))
            {
                lblMsg.Text = "कृपया मापी के लिए निर्धारित तिथि अंकित करें...";
                txtMapiKeNirdharit_tithi.Focus();
                return false;
            }

            return true;
        }

        private bool SaveStep5()
        {
            if (ApplicationId == 0)
            {
                lblMsg.Text = "Application not found.";
                return false;
            }

            if (!ValidateStep5())
                return false;

            long applicationId = ApplicationId;

            string pulisPadadhikarPatrFile = string.Empty;
            string halkaKarmchariPatrFile = string.Empty;
            string vivaaditBhukhandMapiFile = string.Empty;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlTransaction trans = null;

                try
                {
                    con.Open();
                    trans = con.BeginTransaction();

     
                    if (pulis_padadhikari_Patr_file.HasFile)
                    {
                        if (!validateFile(pulis_padadhikari_Patr_file, "doc"))
                            return false;

                        string result = FileUploadValidator.IsPdf( pulis_padadhikari_Patr_file.PostedFile, 1024, 1024);

                        if (result != "OK")
                        {
                            lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                            return false;
                        }

                        string path = "~/LandDoc/Upload/PulisPadadhikariPatr" + applicationId + "/";

                        string uploadedPath = InsSaveFile( "PulisPadadhikariPatr",  pulis_padadhikari_Patr_file,  applicationId.ToString(), path);

                        string expectedPath =  "~/LandDoc/Upload/PulisPadadhikariPatr" + applicationId + "/PulisPadadhikariPatr.pdf";

                        if (uploadedPath == "0" || uploadedPath != expectedPath)
                        {
                            lblMsg.Text = "Technical Error";
                            return false;
                        }

                        pulisPadadhikarPatrFile = "~/LandDoc/Upload/PulisPadadhikariPatr#a_id#/PulisPadadhikariPatr.pdf";
                    }

                   
                    //------------------Upload Halka Karmchari PDF---------------
                   
                    if (file_halkakarmchari_praptr.HasFile)
                    {
                        if (!validateFile(file_halkakarmchari_praptr, "doc"))
                            return false;

                        string result = FileUploadValidator.IsPdf( file_halkakarmchari_praptr.PostedFile, 1024, 1024);

                        if (result != "OK")
                        {
                            lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                            return false;
                        }

                        string path =  "~/LandDoc/Upload/FileHalkakarmchariPraptr" + applicationId + "/";

                        string uploadedPath = InsSaveFile( "FileHalkakarmchariPraptr", file_halkakarmchari_praptr, applicationId.ToString(), path);

                        string expectedPath = "~/LandDoc/Upload/FileHalkakarmchariPraptr" + applicationId + "/FileHalkakarmchariPraptr.pdf";

                        if (uploadedPath == "0" || uploadedPath != expectedPath)
                        {
                            lblMsg.Text = "Technical Error";
                            return false;
                        }

                        halkaKarmchariPatrFile = "~/LandDoc/Upload/FileHalkakarmchariPraptr#a_id#/FileHalkakarmchariPraptr.pdf";
                    }

                  
                    // -------------Upload Bhukhand Map / Report PDF
                    

                    if (ddlbhukhand_Copy.SelectedValue == "Y" && file_bhukand_prativedan.HasFile)
                    {
                        if (!validateFile(file_bhukand_prativedan, "doc"))
                            return false;

                        string result = FileUploadValidator.IsPdf( file_bhukand_prativedan.PostedFile, 1024, 1024);

                        if (result != "OK")
                        {
                            lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                            return false;
                        }

                        string path = "~/LandDoc/Upload/BhukhandPrativedanPatra" + applicationId + "/";

                        string uploadedPath = InsSaveFile( "BhukhandPrativedanPatra", file_bhukand_prativedan, applicationId.ToString(), path);

                        string expectedPath = "~/LandDoc/Upload/BhukhandPrativedanPatra" + applicationId  + "/BhukhandPrativedanPatra.pdf";

                        if (uploadedPath == "0" || uploadedPath != expectedPath)
                        {
                            lblMsg.Text = "Technical Error";
                            return false;
                        }

                        vivaaditBhukhandMapiFile = "~/LandDoc/Upload/BhukhandPrativedanPatra#a_id#/BhukhandPrativedanPatra.pdf";
                    }

                    bool saved = _step5DAL.SaveStep5(  applicationId,  txtpulis_padadhikari_vivarani.Text.Trim(), pulisPadadhikarPatrFile, txthalkakarmchari_prativedan.Text.Trim(), halkaKarmchariPatrFile,ddlbhukhand_mapi.SelectedValue.Trim(), ddlbhukhand_Copy.SelectedValue.Trim(), string.IsNullOrWhiteSpace(txtMapiKeNirdharit_tithi.Text) ? "01-01-1900" : txtMapiKeNirdharit_tithi.Text.Trim(),vivaaditBhukhandMapiFile,txtbhukhand_reason.Text.Trim(), con, trans);

                    if (!saved)
                    {
                        trans.Rollback();
                        lblMsg.Text = "Step 5 data could not be saved.";
                        
                        return false;
                    }

                 
                    trans.Commit();


                    DisplayApplicationInfo();
                    lblMsg.Text = "Step-5 saved successfully.";
                    ClearStep5();
                    return true;
                    

                }
                catch (Exception ex)
                {
                    if (trans != null)
                    {
                        try
                        {
                            trans.Rollback();
                        }
                        catch
                        {
                            lblMsg.Text = ex.Message;
                        }
                    }

                    lblMsg.Text = ex.Message;
                    return false;
                }
            }
        }

        private void ClearStep5()
        {
            txtpulis_padadhikari_vivarani.Text = string.Empty;

            txthalkakarmchari_prativedan.Text = string.Empty;

            ddlbhukhand_mapi.SelectedIndex = 0;
            ddlbhukhand_Copy.SelectedIndex = 0;

            txtMapiKeNirdharit_tithi.Text = string.Empty;
            txtbhukhand_reason.Text = string.Empty;
        }

        private void FillStep5(long applicationId)
        {
            DataTable dtDb = _step5DAL.GetStep5Details(applicationId);


            if (dtDb.Rows.Count == 0)
                return;

            DataRow dr = dtDb.Rows[0];

            txtpulis_padadhikari_vivarani.Text = dr["pulis_padadhikari_vivarani"].ToString();
            txthalkakarmchari_prativedan.Text = dr["HalkaKarmchari_vivran"].ToString();
            ddlbhukhand_mapi.SelectedValue = dr["vivadit_bhukhand_Mapi_ki_avashyakta_hai"].ToString();

            if (ddlbhukhand_mapi.SelectedIndex == 1)
            {

                divbhukhand_Copy.Visible = true;
                //ddlbhukhand_Copy.SelectedIndex = 0;
                ddlbhukhand_Copy.SelectedValue = dr["vivadit_bhukhand_Mapi"].ToString();
            }

            if (ddlbhukhand_Copy.SelectedValue == "Y")
            {
               
                file_bhukand_prativedan.Visible = true;
               
                txtbhukhand_reason.Visible = false;
                divMapiKeNirdharit_tithi.Visible = false;
            }
            else if (ddlbhukhand_Copy.SelectedValue == "N")
            {
                
                file_bhukand_prativedan.Visible = false;
              
                txtbhukhand_reason.Visible = true;
                divMapiKeNirdharit_tithi.Visible = true;
            }
            else
            {
               
                file_bhukand_prativedan.Visible = false;
            
                txtbhukhand_reason.Visible = false;
                divMapiKeNirdharit_tithi.Visible = false;
            }


            txtMapiKeNirdharit_tithi.Text = dr["maapee_ke_lie_nirdhaarit_tithi"].ToString();
            txtbhukhand_reason.Text = dr["vivaadit_bhukhand_Mapi_Reason"].ToString();

            //if (dr["pulis_padadhikar_Patr_file"].ToString() != "")
            //{
            //    lnkpulis_padadhikari_Patr_file.Visible = true;
            //    lnkpulis_padadhikari_Patr_file.Attributes.Add("path",dr["pulis_padadhikar_Patr_file"].ToString());
            //}
            //else
            //{
            //    lnkpulis_padadhikari_Patr_file.Visible = false;
            //}
            //if (dr["HalkaKarmchari_Patr_file"].ToString() != "")
            //{
            //    lnkfile_halkakarmchari_praptr.Visible = true;
            //    lnkfile_halkakarmchari_praptr.Attributes.Add("path", dr["HalkaKarmchari_Patr_file"].ToString());
            //}
            //else
            //{
            //    lnkfile_halkakarmchari_praptr.Visible = false;
            //}
            //if (dr["vivaadit_bhukhand_Mapi_File"].ToString() != "")
            //{
            //    lnkfile_bhukand_prativedan.Visible = true;
            //    lnkfile_bhukand_prativedan.Attributes.Add("path", dr["vivaadit_bhukhand_Mapi_File"].ToString());
            //}
            //else
            //{
            //    lnkfile_bhukand_prativedan.Visible = false;
            //}


            string policeFile =  Convert.ToString(dr["pulis_padadhikar_Patr_file"]);

            if (!string.IsNullOrWhiteSpace(policeFile))
            {
                string url = GetDocumentServerUrl(policeFile);

                if (!string.IsNullOrWhiteSpace(url))
                {
                    lnkpulis_padadhikari_Patr_file.HRef = url;
                    lnkpulis_padadhikari_Patr_file.Target = "_blank";
                    lnkpulis_padadhikari_Patr_file.Visible = true;
                }
            }
            else
            {
                lnkpulis_padadhikari_Patr_file.Visible = false;
            }


            string halkaFile = Convert.ToString(dr["HalkaKarmchari_Patr_file"]);

            if (!string.IsNullOrWhiteSpace(halkaFile))
            {
                string url = GetDocumentServerUrl(halkaFile);

                if (!string.IsNullOrWhiteSpace(url))
                {
                    lnkfile_halkakarmchari_praptr.HRef = url;
                    lnkfile_halkakarmchari_praptr.Target = "_blank";
                    lnkfile_halkakarmchari_praptr.Visible = true;
                }
            }
            else
            {
                lnkfile_halkakarmchari_praptr.Visible = false;
            }


            string bhukhandFile = Convert.ToString(dr["vivaadit_bhukhand_Mapi_File"]);

            if (!string.IsNullOrWhiteSpace(bhukhandFile))
            {
                string url = GetDocumentServerUrl(bhukhandFile);

                if (!string.IsNullOrWhiteSpace(url))
                {
                    lnkfile_bhukand_prativedan.HRef = url;
                    lnkfile_bhukand_prativedan.Target = "_blank";
                    lnkfile_bhukand_prativedan.Visible = true;
                }
            }
            else
            {
                lnkfile_bhukand_prativedan.Visible = false;
            }
        }


        //-----------------------------------------Step5 complete---------------------------------------------------


        //---------------------------------------Step6 --------------------------------

        private DataTable DetailsOfIncidentDT()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Ghatna_Vardat_date", typeof(string));
            dt.Columns.Add("Ghatna_Short_vivran", typeof(string));
            dt.Columns.Add("is_FIR_registered", typeof(string));
            dt.Columns.Add("praathamiki_sankhya", typeof(string));
            dt.Columns.Add("praathamiki_ka_vivaran", typeof(string));
            dt.Columns.Add("is_complaint_filed", typeof(string));
            dt.Columns.Add("dhaara", typeof(string));
            dt.Columns.Add("apraathamiki_sankhya", typeof(string));
            dt.Columns.Add("apraathamiki_ka_vivaran", typeof(string));
            dt.Columns.Add("Abhiyukt", typeof(string));
            dt.Columns.Add("is_Sanha_recorded", typeof(string));
            dt.Columns.Add("sanha_sankhya", typeof(string));
            dt.Columns.Add("bnm", typeof(string));
            dt.Columns.Add("newdhara", typeof(string));
            dt.Columns.Add("bnm1", typeof(string));
            dt.Columns.Add("newdhara1", typeof(string));

            return dt;
        }

        private DataTable CourtDisputeDetailsDT()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("courtID", typeof(string));
            dt.Columns.Add("courtTypeID", typeof(string));
            dt.Columns.Add("District_Code", typeof(string));
            dt.Columns.Add("Sub_DivCode", typeof(string));
            dt.Columns.Add("Vibhag_code", typeof(string));
            dt.Columns.Add("vaadi_ki_vaad_sankhya_varsh", typeof(string));
            dt.Columns.Add("vadi_name", typeof(string));
            dt.Columns.Add("prativadi_name", typeof(string));
            dt.Columns.Add("vaad_ki_addhatan_sthiti_vivaran", typeof(string));
            dt.Columns.Add("court", typeof(string));
            dt.Columns.Add("courtType", typeof(string));
            dt.Columns.Add("Dst", typeof(string));
            dt.Columns.Add("SubDiv", typeof(string));
            dt.Columns.Add("Vibhag", typeof(string));

            return dt;
        }
        protected void btnbhumivivad_Click(object sender, EventArgs e)
        {

            string dhaara = GetSelectedOldDhara();


            if (ddlAprathmiki_huyee_hai.SelectedValue == "Y")
            {

                if (!rdoOld.Checked && !rdoNew.Checked)
                {
                    ShowMessage("Please select Old Dhara or New Dhara");
                    return;
                }


                //--------------Old Dhara validation

                if (rdoOld.Checked)
                {
                    bool anyOldDharaSelected = chk107.Checked || chk109.Checked || chk110.Checked || chk113.Checked || chk116.Checked || chk133.Checked || chk144.Checked || chk145.Checked || chk147.Checked;

                    if (!anyOldDharaSelected)
                    {
                        ShowMessage("Please select at least one Old Dhara");
                        return;
                    }
                }


                //--------- New Dhara validation

                if (rdoNew.Checked)
                {
                    bool anyBNMSelected = ddlbsn_dhara_hai.Items.Cast<ListItem>().Any(x => x.Selected);

                    if (!anyBNMSelected)
                    {
                        ShowMessage("Please select at least one BNM");
                        return;
                    }

                    bool anyNewDharaSelected = ddldhara1.Items.Cast<ListItem>().Any(x => x.Selected);

                    if (!anyNewDharaSelected)
                    {
                        ShowMessage("Please select at least one New Dhara");
                        return;
                    }


                    bool contains41 = ddlbsn_dhara_hai.Items.Cast<ListItem>().Any(x => x.Selected && x.Value == "41");

                    if (contains41)
                    {
                        if (string.IsNullOrWhiteSpace(txtbnm.Text) || string.IsNullOrWhiteSpace(txtdhara.Text))
                        {
                            ShowMessage("Please fill Other BNM and Other Dhara details");
                            return;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(txtAFIR_sankhya.Text))
                {
                    ShowMessage("कृपया अप्राथमिकी संख्या अंकित करें...!");
                    return;
                }
            }

            string bnm = "";
            string newdhara = "";
            string bnm1 = "";
            string newdhara1 = "";

            if (rdoNew.Checked)
            {
                bnm = GetSelectedValues(ddlbsn_dhara_hai);
                newdhara = GetSelectedValues(ddldhara1);

                bool contains41 = ddlbsn_dhara_hai.Items.Cast<ListItem>().Any(x => x.Selected && x.Value == "41");

                if (contains41)
                {
                    bnm1 = txtbnm.Text.Trim();
                    newdhara1 = txtdhara.Text.Trim();
                }
            }


            DataTable dt = ViewState["DetailsOfIncidentDT"] as DataTable;

            if (dt == null)
            {
                ShowMessage("Incident details table is not initialized.");
                return;
            }
            //-------- Add incident details
            DateTime ghatnaDate;

            if (DateTime.TryParseExact(txtghatanaDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ghatnaDate))
            {
                dt.Rows.Add(
                //txtghatanaDate.Text.Trim(),
                ghatnaDate,
                txtghatanavivran.Text.Trim(),
                ddlPrathmiki_huyee_hai.SelectedValue.Trim(),
                txtFIR_sankhya.Text.Trim(),
                txtPrathmik_vivran.Text.Trim(),
                ddlAprathmiki_huyee_hai.SelectedValue.Trim(),
                dhaara,
                txtAFIR_sankhya.Text.Trim(),
                txtAprathmik_vivran.Text.Trim(),
                txtabhiyukt_vaad.Text.Trim(),
                ddlSanhaStatus.SelectedValue.Trim(),
                txtSanahaSankhiyan.Text.Trim(),
                bnm,
                newdhara,
                bnm1,
                newdhara1
            );
            }
            else
            {
                // Handle invalid date input gracefully
                lblMsg.Text = "Please enter date in dd-MM-yyyy format.";
            }

            ViewState["DetailsOfIncidentDT"] = dt;

            //-------------Bind GridView

            BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();


            ClearBhumiVivadControls();
        }

        private string GetSelectedOldDhara()
        {
            List<string> selectedDhara = new List<string>();

            if (chk107.Checked)
                selectedDhara.Add(chk107.Text);

            if (chk109.Checked)
                selectedDhara.Add(chk109.Text);

            if (chk110.Checked)
                selectedDhara.Add(chk110.Text);

            if (chk113.Checked)
                selectedDhara.Add(chk113.Text);

            if (chk116.Checked)
                selectedDhara.Add(chk116.Text);

            if (chk133.Checked)
                selectedDhara.Add(chk133.Text);

            if (chk144.Checked)
                selectedDhara.Add(chk144.Text);

            if (chk145.Checked)
                selectedDhara.Add(chk145.Text);

            if (chk147.Checked)
                selectedDhara.Add(chk147.Text);

            return string.Join(", ", selectedDhara);
        }

        private string GetSelectedValues(ListBox listBox)
        {
            return string.Join(", ", listBox.Items.Cast<ListItem>().Where(x => x.Selected).Select(x => x.Text));
        }

        private void ShowMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{message.Replace("'", "\\'")}');", true);
        }

        private void ClearBhumiVivadControls()
        {
            txtghatanaDate.Text = "";
            txtghatanavivran.Text = "";

            ddlPrathmiki_huyee_hai.SelectedIndex = 0;
            txtFIR_sankhya.Text = "";
            txtPrathmik_vivran.Text = "";

            ddlAprathmiki_huyee_hai.SelectedIndex = 0;
            txtAFIR_sankhya.Text = "";
            txtAprathmik_vivran.Text = "";

            txtabhiyukt_vaad.Text = "";

            rdoOld.Checked = false;
            rdoNew.Checked = false;

            txtbnm.Text = "";
            txtdhara.Text = "";

            ddldhara1.ClearSelection();
            ddlbsn_dhara_hai.ClearSelection();

            div_tbnm.Visible = false;
            div_tdhara.Visible = false;
            divdhara1.Visible = false;
            divbsn.Visible = false;
            divdharabsn.Visible = false;

            divAPrathmiki_sankhiyan.Visible = false;
            divAPrathmiki_vivaran.Visible = false;

            chk107.Checked = false;
            chk109.Checked = false;
            chk110.Checked = false;
            chk113.Checked = false;
            chk116.Checked = false;
            chk133.Checked = false;
            chk144.Checked = false;
            chk145.Checked = false;
            chk147.Checked = false;

            divDhara.Visible = false;
        }

        protected void BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin()
        {
            DataTable dtIncident = ViewState["DetailsOfIncidentDT"] as DataTable;

            if (dtIncident != null)
            {
                grdbhumivivad.DataSource = dtIncident;
                grdbhumivivad.DataBind();
            }
            else
            {
                grdbhumivivad.DataSource = null;
                grdbhumivivad.DataBind();
            }


            DataTable dtCourt = ViewState["CourtDisputeDetailsDT"] as DataTable;

            if (dtCourt != null)
            {
                grdnyayalay_vivran.DataSource = dtCourt;
                grdnyayalay_vivran.DataBind();
            }
            else
            {
                grdnyayalay_vivran.DataSource = null;
                grdnyayalay_vivran.DataBind();
            }
        }

        protected void btnnayaylaysave_Click(object sender, EventArgs e)
        {
            DataTable dt = ViewState["CourtDisputeDetailsDT"] as DataTable;

            if (dt == null)
            {
                dt = CourtDisputeDetailsDT();
            }

            dt.Rows.Add(
                ddlnyayalaya.SelectedValue.Trim(),
                ddlnyayalaya_type.SelectedValue.Trim(),
                ddlDist_nyayalaya_type.SelectedValue.Trim(),
                ddlSubdivision_nyayalaya_type.SelectedValue.Trim(),
                ddlVibhag_nyayalay_type.SelectedValue.Trim(),

                txtdayarvaadsankhya_nayalay.Text.Trim(),
                txtvaadiname_nayaylay.Text.Trim(),
                txtprativadi_nayaylay.Text.Trim(),
                txtwadKiAddhatan_Sthiti_nayayaly.Text.Trim(),

                ddlnyayalaya.SelectedItem != null ? ddlnyayalaya.SelectedItem.Text : "",

                ddlnyayalaya_type.SelectedItem != null ? ddlnyayalaya_type.SelectedItem.Text : "",

                ddlDist_nyayalaya_type.SelectedValue != "0" &&
                ddlDist_nyayalaya_type.SelectedItem != null ? ddlDist_nyayalaya_type.SelectedItem.Text : "",

                ddlSubdivision_nyayalaya_type.SelectedValue != "0" &&
                ddlSubdivision_nyayalaya_type.SelectedItem != null ? ddlSubdivision_nyayalaya_type.SelectedItem.Text : "",

                ddlVibhag_nyayalay_type.SelectedValue != "0" &&
                ddlVibhag_nyayalay_type.SelectedItem != null ? ddlVibhag_nyayalay_type.SelectedItem.Text : ""
            );

            ViewState["CourtDisputeDetailsDT"] = dt;

            BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();

            ClearCourtDisputeControls();
        }

        private void ClearCourtDisputeControls()
        {
            ddlnyayalaya.SelectedIndex = 0;
            ddlnyayalaya_type.SelectedIndex = 0;

            if (ddlDist_nyayalaya_type.Items.Count > 0)
                ddlDist_nyayalaya_type.SelectedIndex = 0;

            if (ddlSubdivision_nyayalaya_type.Items.Count > 0)
                ddlSubdivision_nyayalaya_type.SelectedIndex = 0;

            if (ddlVibhag_nyayalay_type.Items.Count > 0)
                ddlVibhag_nyayalay_type.SelectedIndex = 0;

            txtdayarvaadsankhya_nayalay.Text = "";
            txtvaadiname_nayaylay.Text = "";
            txtdayaryear_nayayaly.Text = "";
            txtprativadi_nayaylay.Text = "";
            txtwadKiAddhatan_Sthiti_nayayaly.Text = "";
        }

        public bool valifBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin()
        {
            lblMsg.Text = string.Empty;


            if (dd_IsBhumiVivad.SelectedIndex == 0)
            {
                lblMsg.Text = "क्या भूमि विवाद सें संबंधित प्राथमिकी/अप्राथमिकी दर्ज है ? हां/नहीं चुनें...";

                dd_IsBhumiVivad.Focus();
                return false;
            }


            if (dd_IsBhumiVivad.SelectedIndex == 1 && grdbhumivivad.Rows.Count == 0)
            {
                lblMsg.Text = "कृपया विवाद सें संबंधित घटना/ वारदात का विवरण जोड़ें...";

                btnBhumiVivadVivran6.Focus();
                return false;
            }


            if (ddl_Isbhumi_Viviad_available.SelectedIndex == 0)
            {
                lblMsg.Text = "क्या न्यायालय में प्रक्रियाधीन वाद का विवरण उपलब्ध है ? हां/नहीं चुनें...";

                ddl_Isbhumi_Viviad_available.Focus();
                return false;
            }

            if (ddl_Isbhumi_Viviad_available.SelectedIndex == 1 && grdnyayalay_vivran.Rows.Count == 0)
            {
                lblMsg.Text = "कृपया न्यायालय में प्रक्रियाधीन वाद का विवरण जोड़ें...";

                btnnayaylaysave.Focus();
                return false;
            }

            return true;
        }

        private bool SaveStep6()
        {
            if (ApplicationId == 0)
            {
                lblMsg.Text = "Application not found.";
                return false;
            }

            if (!valifBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin())
            {
                return false;
            }

            DataTable landDisputeDetails = ViewState["DetailsOfIncidentDT"] as DataTable;

            DataTable courtDisputeDetails = ViewState["CourtDisputeDetailsDT"] as DataTable;

            if (landDisputeDetails == null)
            {
                landDisputeDetails = DetailsOfIncidentDT();
            }

            if (courtDisputeDetails == null)
            {
                courtDisputeDetails = CourtDisputeDetailsDT();
            }

            //---- Create copy 
            DataTable courtDetailsForSave = courtDisputeDetails.Copy();

            courtDetailsForSave.Columns.Remove("court");
            courtDetailsForSave.Columns.Remove("courtType");
            courtDetailsForSave.Columns.Remove("Dst");
            courtDetailsForSave.Columns.Remove("SubDiv");
            courtDetailsForSave.Columns.Remove("Vibhag");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlTransaction trans = null;

                try
                {
                    con.Open();

                    trans = con.BeginTransaction();

                    bool result = _step6DAL.SaveStep6(ApplicationId, dd_IsBhumiVivad.SelectedValue.Trim(), ddl_Isbhumi_Viviad_available.SelectedValue.Trim(), landDisputeDetails, courtDetailsForSave, userid, con, trans);

                    if (!result)
                    {
                        trans.Rollback();

                        lblMsg.Text = "Step-6 data could not be saved.";
                        return false;
                    }

                    trans.Commit();

                    DisplayApplicationInfo();
                    lblMsg.Text = "Step-6 saved successfully.";

                    ClearStep6();

                    return true;


                }
                catch (Exception ex)
                {
                    if (trans != null)
                    {
                        try
                        {
                            trans.Rollback();
                        }
                        catch
                        {
                            lblMsg.Text = ex.Message;
                        }
                    }

                    lblMsg.Text = ex.Message;
                    return false;
                }
            }
        }

        public void ClearStep6()
        {
            DataTable incidentDetails = ViewState["DetailsOfIncidentDT"] as DataTable;

            if (incidentDetails != null)
            {
                incidentDetails.Clear();

                ViewState["DetailsOfIncidentDT"] = incidentDetails;

                grdbhumivivad.DataSource = incidentDetails;
                grdbhumivivad.DataBind();
            }

            DataTable courtDetails = ViewState["CourtDisputeDetailsDT"] as DataTable;

            if (courtDetails != null)
            {
                courtDetails.Clear();

                ViewState["CourtDisputeDetailsDT"] = courtDetails;

                grdnyayalay_vivran.DataSource = courtDetails;
                grdnyayalay_vivran.DataBind();
            }

            ddlYear.SelectedIndex = 0;
            dd_IsBhumiVivad.SelectedIndex = 0;
            ddl_Isbhumi_Viviad_available.SelectedIndex = 0;
        }

        private void FillStep6(long applicationId)
        {
            if (applicationId == 0)
                return;

            try
            {

                DataTable dtMatter = _step6DAL.GetStep6MatterDetails(applicationId);

                if (dtMatter.Rows.Count > 0)
                {
                    DataRow dr = dtMatter.Rows[0];

                    string bhumiVivad = dr["bhumi_vivad_Vivran_Available"].ToString();

                    string courtDispute = dr["dispute_in_court_available"].ToString();

                    if (dd_IsBhumiVivad.Items.FindByValue(bhumiVivad) != null)
                        dd_IsBhumiVivad.SelectedValue = bhumiVivad;

                    if (ddl_Isbhumi_Viviad_available.Items.FindByValue(courtDispute) != null)
                        ddl_Isbhumi_Viviad_available.SelectedValue = courtDispute;
                }

                DataTable dtIncident = _step6DAL.GetIncidentDetails(applicationId);

                ViewState["DetailsOfIncidentDT"] = dtIncident;

                grdbhumivivad.DataSource = dtIncident;
                grdbhumivivad.DataBind();

                DataTable dtCourt = _step6DAL.GetCourtDisputeDetails(applicationId);

                ViewState["CourtDisputeDetailsDT"] = dtCourt;

                grdnyayalay_vivran.DataSource = dtCourt;
                grdnyayalay_vivran.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void grdbhumivivad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                DataTable dt = ViewState["DetailsOfIncidentDT"] as DataTable;

                if (dt != null && rowIndex >= 0 && rowIndex < dt.Rows.Count)
                {
                    // Remove the row from DataTable
                    dt.Rows.RemoveAt(rowIndex);

                    // Save back to ViewState
                    ViewState["DetailsOfIncidentDT"] = dt;

                    // Rebind GridView
                    grdbhumivivad.DataSource = dt;
                    grdbhumivivad.DataBind();
                }
            }
        }

        protected void grdnyayalay_vivran_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                DataTable dt = ViewState["CourtDisputeDetailsDT"] as DataTable;

                if (dt != null && rowIndex >= 0 && rowIndex < dt.Rows.Count)
                {
                    // Remove the row from DataTable
                    dt.Rows.RemoveAt(rowIndex);

                    // Save back to ViewState
                    ViewState["CourtDisputeDetailsDT"] = dt;

                    // Rebind GridView
                    grdnyayalay_vivran.DataSource = dt;
                    grdnyayalay_vivran.DataBind();
                }
            }
        }

   
        //-----------------------------------------Step6 complete--------------------------------------------------


        //-----------------Step7---------------------

        private DataTable CreateActionDetailsTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Meeting_date", typeof(DateTime));
            dt.Columns.Add("Is_Vadi_Present", typeof(string));
            dt.Columns.Add("Is_PratiVadi_Present", typeof(string));
            dt.Columns.Add("conclusion_of_the_meeting", typeof(string));
            dt.Columns.Add("anchala_dhikari_mantavy", typeof(string));
            dt.Columns.Add("thana_prabhari_mantavy", typeof(string));
            dt.Columns.Add("Joint_report_SHO_Circle_Officer_file", typeof(string));
            dt.Columns.Add("Matter_Status", typeof(long));
            dt.Columns.Add("Matter_Status_by", typeof(string));
            dt.Columns.Add("Matter_Status_date", typeof(DateTime));
            dt.Columns.Add("date_of_disposal", typeof(DateTime));
            dt.Columns.Add("reason_for_rejection", typeof(string));
            dt.Columns.Add("mapi_ki_tithi", typeof(DateTime));
            dt.Columns.Add("agali_sunavaee_ki_tithi", typeof(DateTime));
            dt.Columns.Add("CircleOfficer_letterOfIntent", typeof(string));
            dt.Columns.Add("PoliceOfficer_letterOfIntent", typeof(string));
            dt.Columns.Add("Bhumi_savedansheelta", typeof(long));
            dt.Columns.Add("vaadi_ki_vaad_sankhya_varsh", typeof(string));

            return dt;
        }

        private object GetDateValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return DBNull.Value;

            DateTime date;

            if (DateTime.TryParseExact(value.Trim(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
            {
                return date;
            }

            return DBNull.Value;
        }

        private bool ValidateStep7()
        {
            bool isValid = true;

            if (ddlbhumivivadki_sanvedanshilta.SelectedIndex == 0)
            {
                lblMsg.Text = "कृपया भूमि की संवेदनशीलता चुनें...!";
                ddlbhumivivadki_sanvedanshilta.Focus();
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtbaithakDate.Text))
            {
                lblMsg.Text = "कृपया बैठक की तिथि अंकित करें...!";
                txtbaithakDate.Focus();
                isValid = false;
            }

            if (ddlIsVadiAvailable.SelectedIndex == 0)
            {
                lblMsg.Text = "क्या वादी उपस्थित है ? हां/नहीं चुनें...!";
                ddlIsVadiAvailable.Focus();
                isValid = false;
            }

            if (ddl_IsprativadiAvailable.SelectedIndex == 0)
            {
                lblMsg.Text = "क्या प्रतिवादी उपस्थित है ? हां/नहीं चुनें...!";
                ddl_IsprativadiAvailable.Focus();
                isValid = false;
            }

            if (ddlaction.SelectedIndex == 0)
            {
                lblMsg.Text = "कृपया बैठक का निष्कर्ष चुनें...!";
                ddlaction.Focus();
                isValid = false;
            }


            if (ddlaction.SelectedValue == "2")
            {
                if (string.IsNullOrWhiteSpace(txtAgalaDate.Text))
                {
                    lblMsg.Text = "कृपया मापी की तिथि अंकित करें...!";
                    txtAgalaDate.Focus();
                    isValid = false;
                }
            }


            if (ddlaction.SelectedValue == "4")
            {
                if (string.IsNullOrWhiteSpace(txtCancelReason.Text))
                {
                    lblMsg.Text = "कृपया अस्वीकृति का कारण अंकित करें...!";
                    txtCancelReason.Focus();
                    isValid = false;
                }
            }


            if (ddlaction.SelectedValue == "5")
            {
                if (string.IsNullOrWhiteSpace(txtAgalaDate.Text))
                {
                    lblMsg.Text = "कृपया अंतिम निष्पादन की तिथि अंकित करें...!";
                    txtAgalaDate.Focus();
                    isValid = false;
                }
            }


            if (ddlaction.SelectedValue == "6")
            {
                if (string.IsNullOrWhiteSpace(txtvadkavars.Text))
                {
                    lblMsg.Text = "कृपया वादी की वाद संख्या / वर्ष अंकित करें...!";
                    txtvadkavars.Focus();
                    isValid = false;
                }
            }

            return isValid;
        }

        private bool SaveStep7Files(out string landDoc, out string circleOfficerLetter, out string policeOfficerLetter)
        {
            // Preserve previously saved document paths
            landDoc = hdLandDoc.Value;
            circleOfficerLetter = hdCircleOfficer_letterofintent.Value;
            policeOfficerLetter = hdPoliceOfficer_letterOfIntent.Value;

            // Get next document number only when a file is actually uploaded
            int fileNo = 0;

            if (LandDoc.HasFile || CircleOfficer_letterOfIntent.HasFile || PoliceOfficer_letterOfIntent.HasFile)
            {
                fileNo = GetStep7FileCount() + 1;
            }

            // --------------------------------------------------
            // Land Document - OPTIONAL
            // --------------------------------------------------

            if (LandDoc.HasFile)
            {
                if (!validateFile(LandDoc, "doc"))
                    return false;

                string result = FileUploadValidator.IsPdf(LandDoc.PostedFile, 1024, 1024);

                if (result != "OK")
                {
                    lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                    return false;
                }

                landDoc = "~/LandDoc/Upload/LandDocuments" + ApplicationId + "/LandDocuments" + fileNo + ".pdf";

                string path = "~/LandDoc/Upload/LandDocuments" + ApplicationId + "/";

                string savedPath = InsSaveFile("LandDocuments" + fileNo, LandDoc, ApplicationId.ToString(), path);

                if (savedPath == "0" || savedPath != landDoc)
                {
                    landDoc = "";
                    lblMsg.Text = "LandDocuments not upload";
                    return false;
                }
            }

            // --------------------------------------------------
            // Circle Officer Letter - OPTIONAL
            // --------------------------------------------------

            if (CircleOfficer_letterOfIntent.HasFile)
            {
                if (!validateFile(CircleOfficer_letterOfIntent, "doc"))
                    return false;

                string result = FileUploadValidator.IsPdf(CircleOfficer_letterOfIntent.PostedFile, 1024, 1024);

                if (result != "OK")
                {
                    lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                    return false;
                }

                circleOfficerLetter = "~/LandDoc/Upload/CirclePulisPadadhikariPatr" + ApplicationId + "/CirclePulisPadadhikariPatr" + fileNo + ".pdf";

                string path = "~/LandDoc/Upload/CirclePulisPadadhikariPatr" + ApplicationId + "/";

                string savedPath = InsSaveFile("CirclePulisPadadhikariPatr" + fileNo, CircleOfficer_letterOfIntent, ApplicationId.ToString(), path);

                if (savedPath == "0" || savedPath != circleOfficerLetter)
                {
                    circleOfficerLetter = "";
                    lblMsg.Text = "Technical Error";
                    return false;
                }
            }

            // --------------------------------------------------
            // Police Officer Letter - OPTIONAL
            // --------------------------------------------------

            if (PoliceOfficer_letterOfIntent.HasFile)
            {
                if (!validateFile(PoliceOfficer_letterOfIntent, "doc"))
                    return false;

                string result = FileUploadValidator.IsPdf(PoliceOfficer_letterOfIntent.PostedFile, 1024, 1024);

                if (result != "OK")
                {
                    lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                    return false;
                }

                policeOfficerLetter = "~/LandDoc/Upload/PulisPadadhikariPatr" + ApplicationId + "/PulisPadadhikariPatr" + fileNo + ".pdf";

                string path = "~/LandDoc/Upload/PulisPadadhikariPatr" + ApplicationId + "/";

                string savedPath = InsSaveFile("PulisPadadhikariPatr" + fileNo, PoliceOfficer_letterOfIntent, ApplicationId.ToString(), path);

                if (savedPath == "0" || savedPath != policeOfficerLetter)
                {
                    policeOfficerLetter = "";
                    lblMsg.Text = "Technical Error";
                    return false;
                }
            }


            return true;
        }

        private int GetStep7FileCount()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM BS_ActionDetailsEntry WHERE a_id = @a_id", con))
            {
                cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = ApplicationId;

                con.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private bool SaveStep7()
        {
            if (ApplicationId == 0)
            {
                lblMsg.Text = "Application not found.";
                return false;
            }

            if (!ValidateStep7())
                return false;



            string landDoc;
            string circleOfficerLetter;
            string policeOfficerLetter;


            // -----------------File upload


            if (!SaveStep7Files(out landDoc, out circleOfficerLetter, out policeOfficerLetter))
            {
                return false;
            }

            DataTable dtAction = CreateActionDetailsTable();


            //------------ Step-7 TVP data


            dtAction.Rows.Add(
                GetDateValue(txtbaithakDate.Text),
                ddlIsVadiAvailable.SelectedValue.Trim(),
                ddl_IsprativadiAvailable.SelectedValue.Trim(),
                txtfalafal.Text.Trim(),
                txtabhiyukt_anchaladhikari.Text.Trim(),
                txtabhiyukt_thaanprabhaaree.Text.Trim(),
                landDoc,
                Convert.ToInt64(ddlaction.SelectedValue),
                 userid,
                DateTime.Now,
                GetDateValue(txtAgalaDate.Text),
                txtCancelReason.Text.Trim(),
                GetDateValue(txtAgalaDate.Text),
                GetDateValue(txtAgalaDate.Text),
                circleOfficerLetter,
                policeOfficerLetter,
                Convert.ToInt64(ddlbhumivivadki_sanvedanshilta.SelectedValue),
                txtvadkavars.Text.Trim()
            );

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlTransaction trans = null;

                try
                {
                    con.Open();
                    trans = con.BeginTransaction();

                    _step7DAL.SaveStep7(ApplicationId, userid, dtAction, con, trans);

                    trans.Commit();
                    DisplayApplicationInfo();
                    lblMsg.Text = "Step-7 saved successfully.";

                    return true;
                }
                catch (Exception ex)
                {
                    if (trans != null)
                    {
                        try
                        {
                            trans.Rollback();
                        }
                        catch
                        {
                        }
                    }

                    lblMsg.Text = ex.Message;
                    return false;
                }
            }
        }

        private void FillStep7(long applicationId)
        {
            DataTable dt = _step7DAL.GetStep7(applicationId);

            if (dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];

            string baseUrl = ConfigurationManager.AppSettings["DocumentServer"];

            if (!string.IsNullOrWhiteSpace(baseUrl))
                baseUrl = baseUrl.TrimEnd('/');


            if (dr["Bhumi_savedansheelta"] != DBNull.Value)
            {
                ddlbhumivivadki_sanvedanshilta.SelectedValue = dr["Bhumi_savedansheelta"].ToString();
            }



            if (dr["Meeting_date"] != DBNull.Value)
            {
                txtbaithakDate.Text = Convert.ToDateTime(dr["Meeting_date"]).ToString("dd-MM-yyyy");
            }


            if (dr["Is_Vadi_Present"] != DBNull.Value)
            {
                ddlIsVadiAvailable.SelectedValue = dr["Is_Vadi_Present"].ToString();
            }


            if (dr["Is_PratiVadi_Present"] != DBNull.Value)
            {
                ddl_IsprativadiAvailable.SelectedValue = dr["Is_PratiVadi_Present"].ToString();
            }


            if (dr["Matter_Status"] != DBNull.Value)
            {
                ddlaction.SelectedValue = dr["Matter_Status"].ToString();
            }


            txtfalafal.Text = dr["conclusion_of_the_meeting"] == DBNull.Value ? "" : dr["conclusion_of_the_meeting"].ToString();


            txtabhiyukt_anchaladhikari.Text = dr["anchala_dhikari_mantavy"] == DBNull.Value ? "" : dr["anchala_dhikari_mantavy"].ToString();


            txtabhiyukt_thaanprabhaaree.Text = dr["thana_prabhari_mantavy"] == DBNull.Value ? "" : dr["thana_prabhari_mantavy"].ToString();


            txtvadkavars.Text = dr["vaadi_ki_vaad_sankhya_varsh"] == DBNull.Value ? "" : dr["vaadi_ki_vaad_sankhya_varsh"].ToString();

            txtCancelReason.Text = dr["reason_for_rejection"] == DBNull.Value ? "" : dr["reason_for_rejection"].ToString();

            if (dr["mapi_ki_tithi"] != DBNull.Value)
            {
                txtAgalaDate.Text = Convert.ToDateTime(dr["mapi_ki_tithi"]).ToString("dd-MM-yyyy");
            }
            else if (dr["agali_sunavaee_ki_tithi"] != DBNull.Value)
            {
                txtAgalaDate.Text = Convert.ToDateTime(dr["agali_sunavaee_ki_tithi"]).ToString("dd-MM-yyyy");
            }
            else if (dr["date_of_disposal"] != DBNull.Value)
            {
                txtAgalaDate.Text = Convert.ToDateTime(dr["date_of_disposal"]).ToString("dd-MM-yyyy");
            }

            string landDocument = dr["Joint_report_SHO_Circle_Officer_file"] == DBNull.Value ? "" : dr["Joint_report_SHO_Circle_Officer_file"].ToString();

            if (!string.IsNullOrWhiteSpace(landDocument))
            {
                hdLandDoc.Value = landDocument;

                string filePath = landDocument.Replace("~", "");

                lnkLandDoc.HRef = baseUrl + filePath;
                lnkLandDoc.Target = "_blank";
                lnkLandDoc.Visible = true;
            }
            else
            {
                hdLandDoc.Value = "";
                lnkLandDoc.Visible = false;
            }



            string circleOfficerDocument = dr["CircleOfficer_letterOfIntent"] == DBNull.Value ? "" : dr["CircleOfficer_letterOfIntent"].ToString();

            if (!string.IsNullOrWhiteSpace(circleOfficerDocument))
            {

                hdCircleOfficer_letterofintent.Value = circleOfficerDocument;

                string filePath = circleOfficerDocument.Replace("~", "");

                lnkCircleOfficer_letterOfIntent.HRef = baseUrl + filePath;

                lnkCircleOfficer_letterOfIntent.Target = "_blank";
                lnkCircleOfficer_letterOfIntent.Visible = true;
            }
            else
            {
                hdCircleOfficer_letterofintent.Value = "";
                lnkCircleOfficer_letterOfIntent.Visible = false;
            }



            string policeOfficerDocument = dr["PoliceOfficer_letterOfIntent"] == DBNull.Value ? "" : dr["PoliceOfficer_letterOfIntent"].ToString();

            if (!string.IsNullOrWhiteSpace(policeOfficerDocument))
            {

                hdPoliceOfficer_letterOfIntent.Value = policeOfficerDocument;

                string filePath = policeOfficerDocument.Replace("~", "");

                lnkPoliceOfficer_letterOfIntent.HRef = baseUrl + filePath;

                lnkPoliceOfficer_letterOfIntent.Target = "_blank";
                lnkPoliceOfficer_letterOfIntent.Visible = true;
            }
            else
            {
                hdPoliceOfficer_letterOfIntent.Value = "";
                lnkPoliceOfficer_letterOfIntent.Visible = false;
            }
        }


        //===============================Master Table Bind=================================================================

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


                DataTable dt = objDBHelper.GetResults("select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) and BlockCode in (select code from MstThanaMapping where thana_code=@thana_code)  order by BlockName;", listSQLP, false);
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
            BindLandUnit(ddlrakabaunit1, 1);
            BindLandUnit(ddlrakabaunit2, 2);
            BindLandUnit(ddlrakabaunit3, 3);
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

        //-------------------------Step6------------------------------------------
      
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

    
        protected void ddlIsVadiEvi_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtVadiEvidenceType.Text = "";

            if (ddlIsVadiEvi.SelectedValue == "Y")
            {
                divVadiEvidenceType.Visible = true;
                divvadi_dastavej.Visible = true;
            }
            else
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
            /*ddlbhukhand_Copy_SelectedIndexChanged(sender, e); */  //-----------mistake
            HandleBhukhandCopySelection();
            if (ddlbhukhand_mapi.SelectedIndex == 1)
            {
                //divbhukhand_mapi.Attributes.Add("class", "col-md-6");
                divbhukhand_Copy.Visible = true;
                ddlbhukhand_Copy.SelectedIndex = 0;
            }
        }

        protected void ddlbhukhand_Copy_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            HandleBhukhandCopySelection();
        }

        private void HandleBhukhandCopySelection()
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

        //-------------steps 6---------------------------

        protected void dd_IsBhumiVivad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dd_IsBhumiVivad.SelectedValue == "Y")
            {
                btnbhumivivad.Visible = true;
                btnBhumiVivadVivran1.Visible = true;
                btnBhumiVivadVivran2.Visible = true;
                btnBhumiVivadVivran3.Visible = true;
                btnBhumiVivadVivran4.Visible = true;
                btnBhumiVivadVivran5.Visible = true;
                btnBhumiVivadVivran6.Visible = true;
                //ViewState["DetailsOfIncidentDT"] = DetailsOfIncidentDT();
                if (ViewState["DetailsOfIncidentDT"] == null)
                {
                    ViewState["DetailsOfIncidentDT"] = DetailsOfIncidentDT();
                }
                //this.BindGrid();
            }
            else
            {
                btnBhumiVivadVivran1.Visible = false;
                btnBhumiVivadVivran2.Visible = false;
                btnBhumiVivadVivran3.Visible = false;
                btnBhumiVivadVivran4.Visible = false;
                btnBhumiVivadVivran5.Visible = false;
                btnBhumiVivadVivran6.Visible = false;
                btnbhumivivad.Visible = false;
                //ViewState["DetailsOfIncidentDT"] = DetailsOfIncidentDT();
                if (ViewState["DetailsOfIncidentDT"] == null)
                {
                    ViewState["DetailsOfIncidentDT"] = DetailsOfIncidentDT();
                }
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
                btnnyayalay7.Visible = true;
                btnnayaylaysave.Visible = true;
                //ViewState["CourtDisputeDetailsDT"] = CourtDisputeDetailsDT();
                if (ViewState["CourtDisputeDetailsDT"] == null)
                {
                    ViewState["CourtDisputeDetailsDT"] = CourtDisputeDetailsDT();
                }
                //this.BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();
            }
            else
            {
                btnnyayalay1.Visible = false;
                btnnyayalay3.Visible = false;
                btnnyayalay4.Visible = false;
                btnnyayalay5.Visible = false;
                btnnyayalay6.Visible = false;
                btnnyayalay7.Visible = false;
                btnnayaylaysave.Visible = false;
                divDist_nyayalaya_type.Visible = false;
                divSubdivision_nyayalaya_type.Visible = false;
                //ViewState["CourtDisputeDetailsDT"] = CourtDisputeDetailsDT();
                if (ViewState["CourtDisputeDetailsDT"] == null)
                {
                    ViewState["CourtDisputeDetailsDT"] = CourtDisputeDetailsDT();
                }
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

        //-----------------------------------------------------------------------------------------
        protected void ddlUserPanchyat_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPanchayat();
          
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

        //---------------Selected Event Change Event---------------------------

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



        //-----------------------Step7----------------------------------------

        private void bind_BhumiSanvedanshilta()// भूमि विवाद कि सवेदनशीलता
        {
            ddlbhumivivadki_sanvedanshilta.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString()));

                DataTable dt = objDBHelper.GetResults("SP_SensitivityType", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlbhumivivadki_sanvedanshilta.DataSource = dt;
                    ddlbhumivivadki_sanvedanshilta.DataTextField = "SensitivityType";
                    ddlbhumivivadki_sanvedanshilta.DataValueField = "id";
                    ddlbhumivivadki_sanvedanshilta.DataBind();
                    ddlbhumivivadki_sanvedanshilta.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlbhumivivadki_sanvedanshilta.DataSource = null;

                    ddlbhumivivadki_sanvedanshilta.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        protected void ddlaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            divNextDate.Visible = false;
            divCancelReason.Visible = false;
            divvadkavars.Visible = false;
            txtAgalaDate.Text = "";
            txtCancelReason.Text = "";
            txtvadkavars.Text = "";

            if (ddlaction.SelectedIndex == 1)
            {

                divNextDate.Visible = true;
                labNextDate.Text = "प्रारंभिक निष्पादन की तिथि";
            }
            else if (ddlaction.SelectedIndex == 2)
            {

                divCancelReason.Visible = true;

            }
            else if (ddlaction.SelectedIndex == 3)
            {

                divNextDate.Visible = true;
                labNextDate.Text = "मापी की तिथि";
            }
            else if (ddlaction.SelectedIndex == 4)
            {
                divNextDate.Visible = true;
                labNextDate.Text = "अगली सुनवाई की तिथि";
                divCancelReason.Visible = true;
            }
            else if (ddlaction.SelectedIndex == 5)
            {

                divNextDate.Visible = true;
                labNextDate.Text = "अंतिम निष्पादन की तिथि";
            }
            else if (ddlaction.SelectedIndex == 6)
            {

                divvadkavars.Visible = true;

                labNextDate.Text = "वादी की वाद संख्या / वर्ष";
            }
        }

        protected void ddlVadiEvidenceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            divtxtVadiEvidenceType.Visible = false;
            if (ddlVadiEvidenceType.SelectedValue == "9")
            {
                divtxtVadiEvidenceType.Visible = true;
            }
        }

        protected void ddlPrativadiEvidenceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            divtxtPrativadiEvidenceType.Visible = false;
            if (ddlPrativadiEvidenceType.SelectedValue == "9")
            {
                divtxtPrativadiEvidenceType.Visible = true;
            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}

