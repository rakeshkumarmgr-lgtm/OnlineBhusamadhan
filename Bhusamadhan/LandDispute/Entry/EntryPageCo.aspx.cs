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
    public partial class EntryPageCo : System.Web.UI.Page
    {
        string thanacode = "";
        string commCode = "";
        string userid = "";

        DBHelper objDBHelper = new DBHelper();
        string connectionString = DBConHelper.GetConnectionString();
        private readonly MatterRegistrationDAL _matterDAL = new MatterRegistrationDAL();
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
            //string thanaCode = row["Thana_Code"].ToString();

            commCode = row["Commsionary_Code"].ToString();
            userid = row["UserID"].ToString();



            if (!IsPostBack)
            {

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

                BindPolice(distVal, subdivVal, blockVal);

                //------------- Existing Application / Wizard logic
                long selectedApplicationId = 0;

                if (Request.QueryString["a_id"] != null)
                {
                    long.TryParse(Request.QueryString["a_id"], out selectedApplicationId);
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

           
        }


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
                        //FillStep2(ApplicationId);
                    }
                    break;

                case 3:
                    pnlStep3.Visible = true;
                    if (ApplicationId > 0)
                    {
                        //FillStep3(ApplicationId);
                    }
                    break;

                case 4:
                    //pnlStep4.Visible = true;
                    //if (ApplicationId > 0)
                    //{
                    //    FillStep4(ApplicationId);
                    //}
                    break;

                case 5:
                    //pnlStep5.Visible = true;
                    //if (ApplicationId > 0)
                    //{
                    //    FillStep5(ApplicationId);
                    //}
                    break;

                case 6:
                    //pnlStep6.Visible = true;
                    //if (ApplicationId > 0)
                    //{
                    //    FillStep6(ApplicationId);
                    //}
                    break;

                case 7:
                    //pnlStep7.Visible = true;
                    //if (ApplicationId > 0)
                    //{
                    //    FillStep7(ApplicationId);
                    //}
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

   
        // =====================================================
        // Global Methods
        // =====================================================

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


        // =====================================================
        // Steps Binding
        // =====================================================
        //-------------- Step1---------------------
     
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

            try
            {
                DataTable dt = ViewState["vadiDetails"] as DataTable;

           
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

                dr["VillageName"] = ddlUserVillage.SelectedItem.Text;//------Throwing error on selecting Urban

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

                    long applicationId = _matterDAL.SaveStep1(ApplicationId, Convert.ToInt32(commCode), txtrajaswa_sankhya.Text.Trim(), Convert.ToInt32(ddlbhumitype.SelectedValue), Convert.ToInt32(ddlsarkaribhumitype.SelectedValue), txtsarkaribhumitype_Anya.Text.Trim(), Convert.ToInt32(ddlbhumivivadtype.SelectedValue), txtbhumivivad_Anya.Text.Trim(), Convert.ToInt32(ddl_vivad_adyatan_sthiti.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlSubdivision.SelectedValue), Convert.ToInt32(ddlBlock.SelectedValue), Convert.ToInt32(ddlPolice.SelectedValue), Convert.ToInt32(ddlPanchyat.SelectedValue), txtPanchyat_Anya.Text.Trim(), ddlareatype.SelectedValue, Convert.ToInt32(ddlVillage.SelectedValue), txtVillage_Anya.Text.Trim(), Convert.ToInt32(ddlWard.SelectedValue), txtWard_Anya.Text.Trim(), Vadi_sakshya_FilePath, Prativadi_sakshya_FilePath, Convert.ToDateTime(txtAwadenKiTithi.Text), txtVadiVivarani.Text.Trim(), txtPrativadiVivarani.Text.Trim(), guid, userid, GetUserIP().ToString(), con, trans);


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
            string vivadAdyatanStithi = dr["bhumi_vivad_ka_adyatan_sthiti"].ToString();

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

            txtbhumivivad_Anya.Text = dr["BhumiVivadType_Anya"].ToString();

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


        // =====================================================
        // Master Data Binding
        // =====================================================

        protected void AdharYearsBind()
        {
           
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
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@thana_code", thanacode));
                //string sql = @"select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where Sd_Code2 in (select SubDivCode from Blocks where BlockCode in(select code from MstThanaMapping where thana_code=@thana_code)) and sd.DistCode=@District_Code";
                string sql = @"select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where sd.DistCode=@District_Code order by sd.Sd_Name_En";
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
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@thana_code", thanacode));


                //DataTable dt = objDBHelper.GetResults("select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) and BlockCode in (select code from MstThanaMapping where thana_code=@thana_code)  order by BlockName;", listSQLP, false);
                DataTable dt = objDBHelper.GetResults("select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) order by BlockName;", listSQLP, false);
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

        private void BindPolice(string distVal, string subdivVal, string blockVal)
        {
            ddlPolice.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", distVal));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Subdivision_Code", subdivVal));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Circle_Code", blockVal));


                //string sql = @"select DISTINCT t.Police_Station,t.PS_Code from mst_Thana t  where t.Subdivision_Code=@Subdivision_Code and t.District_code=@District_Code and t.Circle_Code=@Circle_Code";
                string sql = @"select DISTINCT  t.Police_Station,t.PS_Code from mst_thana t
                    left join MstThanaMapping m on m.Thana_Code=t.PS_Code 
                    left join Blocks b on b.BlockCode=m.Code and m.Type='Block'
                    where District_code=@District_Code and  b.SubDivCode is not null and m.code=@Circle_Code and b.SubDivCode=@Subdivision_Code
                    ORDER BY Police_Station";

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
            ddlPsanstha_naam.Items.Clear();

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
            ddlPsanshaanya_naam.Items.Clear();
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
            ddlPvibhaag_naam.Items.Clear();
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

                    ddlPvibhaag_naam.DataSource = null;

                    ddlPvibhaag_naam.DataBind();
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

        private void bindbumitype()
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


        // =====================================================
        // Event Binding
        // =====================================================


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

        protected void ddlUserPanchyat_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPanchayat();
        }

        private void RefreshPanchayat()
        {
            BindVillage_Wadi();
            bindward_Wadi();
        }

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
            ddlsarkaribhumitype_SelectedIndexChanged(ddlsarkaribhumitype, EventArgs.Empty);//-----------need to correct
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
        






        // =====================================================
        // Button Events
        // =====================================================

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
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

                    //result = SaveStep2();
                    break;

                case 3:
                    //Page.Validate("4");

                    //if (!Page.IsValid)
                    //    return;

                    //result = SaveStep3();
                    break;

                case 4:
                    //Page.Validate("5");

                    //if (!Page.IsValid)
                    //    return;

                    //result = SaveStep4();
                    break;

                case 5:
                    //Page.Validate("6");

                    //if (!Page.IsValid)
                    //    return;

                    //result = SaveStep5();
                    break;

                case 6:
                    //Page.Validate("7");

                    //if (!Page.IsValid)
                    //    return;

                    //result = SaveStep6();
                    break;

                case 7:
                    Page.Validate("8");

                    if (!Page.IsValid)
                        return;

                    //result = SaveStep7();
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

        
    }
}