using Bhusamadhan.DataAccessLayer.LandDisputeDAL;
using Bhusamadhan.DB;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.LandDispute.Entry
{
   
    public partial class AddMettingApplication : System.Web.UI.Page
    {
        string userid = "";
        string userrole = "";
        int roleid;
        DBHelper objDBHelper = new DBHelper();
        private readonly ApplicationPreviewDAL _applicationPreviewDAL = new ApplicationPreviewDAL();
        private readonly AddAnotherMeetingDAL _meetingDAL = new AddAnotherMeetingDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["UserLogIn"] as DataTable;

            if (dt != null && dt.Rows.Count == 1)
            {
                int roleid = Convert.ToInt32(dt.Rows[0]["RoleID"].ToString());
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
                long applicationId = GetApplicationIdFromQueryString();

                if (applicationId <= 0)
                {
                    Response.Redirect("~/LandDispute/Entry/SearchAppForMetting.aspx");
                    return;
                }
                bind_BhumiSanvedanshilta();
                // Now use applicationId
                //FillApplication(applicationId);
                BindApplicationPreview(applicationId);

            }
        }

        private long GetApplicationIdFromQueryString()
        {
            string encryptedId = Request.QueryString["RegId"];

            if (string.IsNullOrWhiteSpace(encryptedId))
                return 0;

            string decryptedId =  QueryStringHelper.Decrypt(encryptedId);

            if (string.IsNullOrWhiteSpace(decryptedId))
                return 0;

            long applicationId;

            if (!long.TryParse(decryptedId, out applicationId))
                return 0;

            return applicationId;
        }


        private void BindApplicationPreview(long applicationId)
        {
            DataSet ds = _applicationPreviewDAL.GetApplicationPreview(applicationId);

            if (ds == null || ds.Tables.Count == 0)
                return;

            //------- Application / main land dispute information
            BindApplication(ds.Tables[0]);   //ok

            //-------- Vadi    //ok
            BindVadi(ds.Tables[1]);

            //--------- PratiVadi   //ok
            BindPratiVadi(ds.Tables[2]);

            //---------- PratiVadi other information  //ok
            BindPratiVadiOtherDetails(ds.Tables[3]);

            //---------- Khata-Khesra  //ok
            BindKhataKhesra(ds.Tables[4]);

            //------------ Vadi evidence //ok
            BindVadiEvidence(ds.Tables[5]);

            //--------- PratiVadi evidence //ok
            BindPratiVadiEvidence(ds.Tables[6]);

            //------- Police / Revenue / Halka details //ok
            BindPoliceRevenueDetails(ds.Tables[7]);

            //----- Land dispute events
            BindLandDisputeEvents(ds.Tables[8]);

            //------Court details
            BindCourtDetails(ds.Tables[9]);

            //--------- Action details
            BindActionDetails(ds.Tables[10]);

            //------- Remarks detail
            BindRemarksDetails(ds.Tables[11]);
        }

        private string GetDocumentUrl(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return string.Empty;

            string baseUrl = ConfigurationManager.AppSettings["DocumentServer"];

            if (string.IsNullOrWhiteSpace(baseUrl))
                return string.Empty;

            baseUrl = baseUrl.TrimEnd('/');

            // ~/LandDoc/Upload/abc.pdf
            filePath = filePath.Trim().Replace("~", "");

            if (!filePath.StartsWith("/"))
                filePath = "/" + filePath;

            return baseUrl + filePath;
        }

        private void SetPdfButton(ImageButton button, object fileValue)
        {
            string filePath = Convert.ToString(fileValue);

            if (string.IsNullOrWhiteSpace(filePath))
            {
                button.Visible = false;
                return;
            }

            string url = GetDocumentUrl(filePath);

            if (string.IsNullOrWhiteSpace(url))
            {
                button.Visible = false;
                return;
            }

            button.Visible = true;

            button.OnClientClick = "window.open('" + HttpUtility.JavaScriptStringEncode(url) + "', '_blank'); return false;";
        }

        private void BindApplication(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];

            // Application information
            lblApplicationNo.Text = dr["ApplicationNo"].ToString();

            lblAppDate.Text = dr["AavedanKiTithi"].ToString();

            // भूमि विवाद का विवरण
            lblDistrict.Text = ": " + dr["District"].ToString();
            lblSubdivision.Text = ": " + dr["Subdivision"].ToString();
            lblBlock.Text = ": " + dr["Block"].ToString();
            lblPolice_Station.Text = ": " + dr["Police_Station"].ToString();

            lblAreaType.Text = ": " + dr["AreaType"].ToString();

            lblPanchayatName.Text = ": " + dr["Panchayat"].ToString();
            lblVILLNAME.Text = ": " + dr["Village"].ToString();
            lblWARDNAME.Text = ": " + dr["Ward"].ToString();

            lblBhumiKa_VivadPrakar.Text = ": " + dr["BhumiVivadType"].ToString();

            lblvadi_rajashv_sankhaya.Text = ": " + dr["RajasvThanaSankhya"].ToString();

            lblVadi_BhumiKaPrakar.Text = ": " + dr["Bhumitype_Ka_Prakar"].ToString();

            lblvadi_sarkari_bhumi_ka_prakar.Text = ": " + dr["Sarkari_Bhumitype"].ToString();

            lblvadi_Sarkari_bhumi_ka_Prakar_ager_anya.Text = ": " + dr["SarkariBhumiType_Anya"].ToString();

            lblBhumiKa_VivadPrakar.Text = ": " + dr["BhumiVivadType"].ToString();

            lblvadi_Bhumivivad_Prakar_Anaya.Text = ": " + dr["BhumiVivadType_Anya"].ToString();

            lblvadi_Vivad_Ki_Adyatan_Sthithi.Text = ": " + dr["BhumiVivadKaAdyatanSthiti"].ToString();
            lblVadiKabhumiVivaran.Text = ": " + dr["VadiVivarani"].ToString();

            lblPrativadiKabhumiVivaran.Text = ": " + dr["PrativadiVivarani"].ToString();

            SetPdfButton(lnkAppDoc, dr["Vadi_sakshya_File"]);

            SetPdfButton(lnkPrativadiDoc, dr["Prativadi_sakshya_File"]);



        }

        private void BindVadi(DataTable dt)
        {
            rptWadi.DataSource = dt;
            rptWadi.DataBind();
        }
        private void BindPratiVadi(DataTable dt)
        {
            rptPratiWadi.DataSource = dt;
            rptPratiWadi.DataBind();
        }

        private void BindPratiVadiOtherDetails(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];


            lblprativadi_ka_suchit.Text = ": " + dr["PrativadiKoSuchit"].ToString();
            lblprativadi_ka_Karan.Text = ": " + dr["PrativadiKoSuchitKaran"].ToString();
            lblprativadi_ka_madhayam.Text = ": " + dr["SuchnaKaMadhyam"].ToString();
            lblprativadi_ka_SuchnaTamil.Text = ": " + dr["SuchnaTaamila"].ToString();
            lblprativadi_ka_Upashtith.Text = ": " + dr["PrativadiUpasthit"].ToString();
        }

        private void BindKhataKhesra(DataTable dt)
        {
            rptBhumiKhataKhesra.DataSource = dt;
            rptBhumiKhataKhesra.DataBind();
        }

        private void BindVadiEvidence(DataTable dt)
        {
            rptVadiEvidence.DataSource = dt;
            rptVadiEvidence.DataBind();
        }

 
        private void BindPratiVadiEvidence(DataTable dt)
        {
            rptPratiwadiEvidence.DataSource = dt;
            rptPratiwadiEvidence.DataBind();
        }

        // 7- POLICE / REVENUE / MAPI
        private void BindPoliceRevenueDetails(DataTable dt)
        {

            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];

            lblPoliceAdhikariVivarni.Text = ": " + dr["PoliceAdhikariVivran"].ToString();
            lblHalkaKarmchariVivarni.Text = ": " + dr["HalkaKarmchariVivran"].ToString();
            lblVivaditBhukandKiMapiKaReasonHai.Text = ": " + dr["VivaditBhukhandMapiAvashyakta"].ToString();
            lblMapiValue.Text = ": " + dr["VivaditBhukhandMapi"].ToString();
            lblVivaditBhumiKaMapiNahiHoneKaKaran.Text = ": " + dr["VivaditBhukhandMapiReason"].ToString();
            lblMapiKeNirdharnKiThithiValue.Text = ": " + dr["MapiKeLieNirdharitTithi"].ToString();

            SetPdfButton(lnkpulis_padadhikari_Patr_file, dr["PoliceReportFile"]);
            SetPdfButton(lnkfile_halkakarmchari_praptr, dr["HalkaReportFile"]);
            SetPdfButton(lnkfile_bhukand_prativedan, dr["MapiReportFile"]);
        }

        // 8 - Land dispute events
        private void BindLandDisputeEvents(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];
            lblPrathamikHai.Text = ": "+ dt.Rows[0]["bhumi_vivad_Vivran_Available_Inhindi"].ToString();


            rptBhumiVivAdIncident.DataSource = dt;
            rptBhumiVivAdIncident.DataBind();

        }

        // 9 -court detail
        private void BindCourtDetails(DataTable dt)
        {

            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];
            lblPrakiriyadhinVadAvailable.Text = ": "+ dt.Rows[0]["dispute_in_court_available"].ToString();

            rptNyayalayVivran.DataSource = dt;
            rptNyayalayVivran.DataBind();
        }

        // 10 - Action details
        public void BindActionDetails(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];

            lblVivaadKiSanvedanasheelata.Text = ": " + dt.Rows[0]["SensitivityType"].ToString();
            lblBaithakKiTithi.Text = ": " + dt.Rows[0]["Meeting_date"].ToString();
            lblkyaVaadeeUpasthitHai.Text = ": " + dt.Rows[0]["Is_Vadi_Present"].ToString();
            lblKyaPrativaadeeUpasthitHai.Text = ": " + dt.Rows[0]["Is_PratiVadi_Present"].ToString();
            lblBaithakKaNishkarsh.Text = ": " + dt.Rows[0]["BaithakKaNishkarsh"].ToString();

            lblAsveekrtiKaKaaran.Text = ": " + dt.Rows[0]["reason_for_rejection"].ToString();
            lblvadikaVadSankhyaVarsh.Text = ": " + dt.Rows[0]["vaadi_ki_vaad_sankhya_varsh"].ToString();

            lblBaithakMeinLiyaGayaNirnay.Text = ": " + dt.Rows[0]["conclusion_of_the_meeting"].ToString();
            lblAnchalaadhikaareeKaMantavy.Text = ": " + dt.Rows[0]["anchala_dhikari_mantavy"].ToString();
            lblThaanaadhyakshKaMantavy.Text = ": " + dt.Rows[0]["thana_prabhari_mantavy"].ToString();

            // -------Matter Status dependent information----------------------------------


            int matterStatus = 0;

            if (dr["Matter_Status"] != DBNull.Value) int.TryParse(dr["Matter_Status"].ToString(), out matterStatus);

            divtithi.Visible = false;
            divvadikavarsh.Visible = false;
            divAsveekrtiKaKaaranLabel.Visible = false;


            switch (matterStatus)
            {
                case 1:

                    divtithi.Visible = true;

                    lbltithi.Text = "प्रारंभिक निष्पादन की तिथि";
                    lbltithivalue.Text = ":" + dr["MatterStatusDate"].ToString();

                    break;


                case 2:

                    divtithi.Visible = true;

                    lbltithi.Text = "मापी की तिथि";
                    lbltithivalue.Text = ":" + dr["MapiKiTithi"].ToString();

                    break;


                case 3:

                    divtithi.Visible = true;

                    lbltithi.Text = "अगली सुनवाई की तिथि";
                    lbltithivalue.Text = ":" + dr["AgaliSunavaeeKiTithi"].ToString();

                    break;


                case 4:

                    divAsveekrtiKaKaaranLabel.Visible = true;

                    break;


                case 5:

                    divtithi.Visible = true;

                    lbltithi.Text = "अंतिम निष्पादन की तिथि";
                    lbltithivalue.Text = ":" + dr["DateOfDisposal"].ToString();

                    break;


                case 6:

                    divvadikavarsh.Visible = true;

                    lblvadikaVadSankhyaVarsh.Text = ":" + dr["vaadi_ki_vaad_sankhya_varsh"].ToString();

                    break;

            }

            SetPdfButton(lnkJointDoc__letterOfIntent, dr["Joint_report_SHO_Circle_Officer_file"]);

            SetPdfButton(lnkCircleOfficer_letterOfIntent, dr["CircleOfficer_letterOfIntent"]);

            SetPdfButton(lnkPoliceOfficer_letterOfIntent, dr["PoliceOfficer_letterOfIntent"]);
        }

        public void BindRemarksDetails(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            rptRemarks.DataSource = dt;
            rptRemarks.DataBind();
        }


        protected void rptVadiEvidence_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "View")
                return;

            string filePath = Convert.ToString(e.CommandArgument);

            string url = GetDocumentUrl(filePath);

            if (string.IsNullOrWhiteSpace(url))
                return;

            string script = "window.open('" + HttpUtility.JavaScriptStringEncode(url) + "', '_blank');";

            ScriptManager.RegisterStartupScript(this, GetType(), "ViewVadiPdf_" + Guid.NewGuid().ToString("N"), script, true);
        }

        protected void rptPratiwadiEvidence_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "View")
                return;

            string filePath = Convert.ToString(e.CommandArgument);

            string url = GetDocumentUrl(filePath);

            if (string.IsNullOrWhiteSpace(url)) return;

            string script = "window.open('" + HttpUtility.JavaScriptStringEncode(url) + "', '_blank');";

            ScriptManager.RegisterStartupScript(this, GetType(), "ViewPratiwadiPdf_" + Guid.NewGuid().ToString("N"), script, true);
        }

        protected void rptRemarks_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "View")
                return;

            string filePath = Convert.ToString(e.CommandArgument);

            string url = GetDocumentUrl(filePath);

            if (string.IsNullOrWhiteSpace(url)) return;

            string script = "window.open('" + HttpUtility.JavaScriptStringEncode(url) + "', '_blank');";

            ScriptManager.RegisterStartupScript(this, GetType(), "ViewRemarksPdf_" + Guid.NewGuid().ToString("N"), script, true);
        }

        //---------------------Entry part----------------------------------------------

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

            string baseUrl =  ConfigurationManager.AppSettings["DocumentServer"];

            if (string.IsNullOrWhiteSpace(baseUrl))
                return string.Empty;

            baseUrl = baseUrl.TrimEnd('/');

            filePath = filePath.Trim().Replace("~", "");

            if (!filePath.StartsWith("/"))
                filePath = "/" + filePath;

            return baseUrl + filePath;
        }

        private void bind_BhumiSanvedanshilta()
        {
            ddlbhumivivadki_sanvedanshilta.Items.Clear();
          

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                DataTable dtResult = objDBHelper.GetResults("SELECT id,SensitivityType FROM mst_SensitivityType;", listSQLP, false);
                if (dtResult.Rows.Count > 0)
                {
                    ddlbhumivivadki_sanvedanshilta.DataSource = dtResult;
                    ddlbhumivivadki_sanvedanshilta.DataTextField = "SensitivityType";
                    ddlbhumivivadki_sanvedanshilta.DataValueField = "id";
                    ddlbhumivivadki_sanvedanshilta.DataBind();
                    ddlbhumivivadki_sanvedanshilta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));


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

        protected void ddlbhumivivadki_sanvedanshilta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "0")
            {
                onestar.Visible = false;
                twostar.Visible = false;
                threestar.Visible = false;
                fourstar.Visible = false;
            }
            else if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "1")
            {
                onestar.Visible = true;
                twostar.Visible = false;
                threestar.Visible = false;
                fourstar.Visible = false;
            }
            else if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "2")
            {
                onestar.Visible = false;
                twostar.Visible = true;
                threestar.Visible = false;
                fourstar.Visible = false;
            }
            else if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "3")
            {
                onestar.Visible = false;
                twostar.Visible = false;
                threestar.Visible = true;
                fourstar.Visible = false;
            }
            else if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "4")
            {
                onestar.Visible = false;
                twostar.Visible = false;
                threestar.Visible = false;
                fourstar.Visible = true;
            }
        }

        protected void ddlaction_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            divlabNextDate.Visible = false;
            divNextDate.Visible = false;
            divCancelReason.Visible = false;
            divvadkavars.Visible = false;
            txtAgalaDate.Text = "";
            txtCancelReason.Text = "";
            //txtAgalaDate,txtCancelReason, txtAgalaDate,labNextDate

            if (ddlaction.SelectedIndex == 1)
            {
               
                divlabNextDate.Visible = true;
                divNextDate.Visible = true;
                labNextDate.Text = "प्रारंभिक निष्पादन की तिथि";
            }
            else if (ddlaction.SelectedIndex == 2)
            {
              
                divlabNextDate.Visible = true;
                labNextDate.Text = "अस्वीकृति का कारण";
                divCancelReason.Visible = true;

            }
            else if (ddlaction.SelectedIndex == 3)
            {
              
                divlabNextDate.Visible = true;
                divNextDate.Visible = true;
                labNextDate.Text = "मापी की तिथि";
            }
            else if (ddlaction.SelectedIndex == 4)
            {
              
                divlabNextDate.Visible = true;
                divNextDate.Visible = true;
                labNextDate.Text = "अगली सुनवाई की तिथि";
            }
            else if (ddlaction.SelectedIndex == 5)
            {
             
                divlabNextDate.Visible = true;
                divNextDate.Visible = true;
                labNextDate.Text = "अंतिम निष्पादन की तिथि";
            }
            else if (ddlaction.SelectedIndex == 6)
            {
               
                divvadkavars.Visible = true;
                divlabNextDate.Visible = true;
                labNextDate.Text = "वादी की वाद संख्या / वर्ष";
            }

            if ((lastAction.Value == "2" && ddlaction.SelectedIndex == 1) || (lastAction.Value == "2" && ddlaction.SelectedIndex == 5))
            {
                lastActionMapi.Visible = true;
            }
        }

        public bool Validation()
        {
            bool flag = true;
            if ((lastAction.Value == "2" && ddlaction.SelectedIndex == 1) || (lastAction.Value == "2" && ddlaction.SelectedIndex == 5))
            {
                if (!lastActionMapiKaPrativadan.HasFile)
                {
                    flag = false;
                }
                if (txtMapikiNirdharitThiti.Text == "")
                {
                    flag = false;
                }
            }
            return flag;
        }

        private bool SaveAnotherMeeting()
        {
            try
            {
                long applicationId = GetApplicationIdFromQueryString();

                if (applicationId <= 0)
                {
                    lblMsg.Text = "Invalid application.";
                    return false;
                }

                if (!Validation())
                    return false;

                int previousMeetingCount = _meetingDAL.GetPreviousMeetingCount(applicationId);

                int nextMeetingNumber = previousMeetingCount + 1;

                string landDocPath = string.Empty;
                string circleOfficerPath = string.Empty;
                string policeOfficerPath = string.Empty;
                string mapiPrativadanPath = string.Empty;

                if (LandDoc.HasFile)
                {
                    if (FileUploadValidator.IsPdf(LandDoc.PostedFile, 1024, 1024) != "OK")
                    {
                        lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                        return false;
                    }

                    landDocPath = "~/LandDoc/Upload/ID" + applicationId + "/LandDocuments" + nextMeetingNumber + ".pdf";

                    string savedPath = InsSaveFile("LandDocuments" + nextMeetingNumber, LandDoc, applicationId.ToString());

                    if (savedPath == "0" || savedPath != landDocPath)
                    {
                        lblMsg.Text = "Technical Error while uploading Land document.";
                        return false;
                    }
                }

                if (CircleOfficer_letterOfIntent.HasFile)
                {
                    if (FileUploadValidator.IsPdf(CircleOfficer_letterOfIntent.PostedFile, 1024, 1024) != "OK")
                    {
                        lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                        return false;
                    }

                    circleOfficerPath = "~/LandDoc/Upload/ID" + applicationId + "/CirclePulisPadadhikariPatr" + nextMeetingNumber + ".pdf";

                    string savedPath = InsSaveFile("CirclePulisPadadhikariPatr" + nextMeetingNumber, CircleOfficer_letterOfIntent, applicationId.ToString());

                    if (savedPath == "0" || savedPath != circleOfficerPath)
                    {
                        lblMsg.Text = "Technical Error while uploading Circle Officer document.";
                        return false;
                    }
                }



                if (PoliceOfficer_letterOfIntent.HasFile)
                {
                    if (FileUploadValidator.IsPdf(PoliceOfficer_letterOfIntent.PostedFile, 1024, 1024) != "OK")
                    {
                        lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                        return false;
                    }

                    policeOfficerPath = "~/LandDoc/Upload/ID" + applicationId + "/PulisPadadhikariPatr" + nextMeetingNumber + ".pdf";

                    string savedPath = InsSaveFile("PulisPadadhikariPatr" + nextMeetingNumber, PoliceOfficer_letterOfIntent, applicationId.ToString());

                    if (savedPath == "0" || savedPath != policeOfficerPath)
                    {
                        lblMsg.Text = "Technical Error while uploading Police Officer document.";
                        return false;
                    }
                }


                if (lastActionMapiKaPrativadan.HasFile)
                {
                    if (FileUploadValidator.IsPdf(lastActionMapiKaPrativadan.PostedFile, 1024, 1024) != "OK")
                    {
                        lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                        return false;
                    }

                    mapiPrativadanPath = "~/LandDoc/Upload/ID" + applicationId + "/MapiKaPrativadan" + nextMeetingNumber + ".pdf";

                    string savedPath = InsSaveFile("MapiKaPrativadan" + nextMeetingNumber, lastActionMapiKaPrativadan, applicationId.ToString());

                    if (savedPath == "0" || savedPath != mapiPrativadanPath)
                    {
                        lblMsg.Text = "Technical Error while uploading Mapi Prativadan document.";
                        return false;
                    }
                }


                DateTime meetingDate;

                if (!DateTime.TryParseExact(txtbaithakDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out meetingDate))
                {
                    lblMsg.Text = "कृपया बैठक की सही तिथि अंकित करें।";
                    return false;
                }


                DateTime? nextDate = GetDateOrDefault(txtAgalaDate.Text);
                DateTime? mapiDate = GetDateOrDefault(txtMapikiNirdharitThiti.Text);

                bool result = _meetingDAL.SaveAnotherMeeting(applicationId, meetingDate, ddlIsVadiAvailable.SelectedValue.Trim(), ddl_IsprativadiAvailable.SelectedValue.Trim(), txtfalafal.Text.Trim(), txtabhiyukt_anchaladhikari.Text.Trim(), txtabhiyukt_thaanprabhaaree.Text.Trim(), landDocPath, string.IsNullOrWhiteSpace(ddlaction.SelectedValue) ? (long?)null : Convert.ToInt64(ddlaction.SelectedValue),

                 userid, DateTime.Now, nextDate, txtCancelReason.Text.Trim(), mapiDate, nextDate, circleOfficerPath, policeOfficerPath,

                 string.IsNullOrWhiteSpace(ddlbhumivivadki_sanvedanshilta.SelectedValue) ? (long?)null : Convert.ToInt64(ddlbhumivivadki_sanvedanshilta.SelectedValue),

                  mapiPrativadanPath, mapiDate,userid);


                if (!result)
                {
                    lblMsg.Text = "बैठक विवरण सहेजने में तकनीकी समस्या हुई।";
                    return false;
                }


                lblMsg.Text = "नई बैठक के अनुसार अंचलाधिकारी एवं थाना अध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कार्रवाई की विवरणी सफलतापूर्वक सहेजी गई।";

                ResetMeetingControls();

                RefreshApplicationData(applicationId);

                Session["REFRESH_METTING_SEARCH"] = 1;

                return true;
            }
            catch (Exception ex)
            {
                
                lblMsg.Text ="Error: " + ex.Message + "<br/>Inner Error: " + (ex.InnerException != null ? ex.InnerException.Message : "No inner exception.");

                return false;
            }
        }

        private DateTime? GetDateOrDefault(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            DateTime date;

            if (DateTime.TryParseExact( value.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
            }

            return null;
        }

        private void ResetMeetingControls()
        {
            ddlbhumivivadki_sanvedanshilta.SelectedIndex = 0;

            txtbaithakDate.Text = string.Empty;

            ddlIsVadiAvailable.SelectedIndex = 0;

            ddl_IsprativadiAvailable.SelectedIndex = 0;

            ddlaction.SelectedIndex = 0;

            txtfalafal.Text = string.Empty;

            txtabhiyukt_anchaladhikari.Text = string.Empty;

            txtabhiyukt_thaanprabhaaree.Text = string.Empty;

            txtCancelReason.Text = string.Empty;

            labNextDate.Text = string.Empty;

            txtAgalaDate.Text = string.Empty;

            txtMapikiNirdharitThiti.Text = string.Empty;

            lastActionMapiKaPrativadan.Attributes.Clear();

            ddlaction_SelectedIndexChanged( ddlaction, EventArgs.Empty);

            ddlbhumivivadki_sanvedanshilta_SelectedIndexChanged( ddlbhumivivadki_sanvedanshilta,  EventArgs.Empty);
        }

        private void RefreshApplicationData(long applicationId)
        {
            if (applicationId <= 0)
                return;

            BindApplicationPreview(applicationId);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            SaveAnotherMeeting();

        }

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
            }


            else
            {
                return false;
            }


        }
    }
}