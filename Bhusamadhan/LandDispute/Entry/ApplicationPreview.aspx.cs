using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bhusamadhan.DataAccessLayer.LandDisputeDAL;

namespace Bhusamadhan.LandDispute.Entry
{
    public partial class ApplicationPreview : System.Web.UI.Page
    {
        string userid = "";
        private readonly FinalSubmitDAL _finalSubmitDAL = new FinalSubmitDAL();
        private readonly ApplicationPreviewDAL _applicationPreviewDAL = new ApplicationPreviewDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["UserLogIn"] as DataTable;

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    //int roleid = Convert.ToInt32(dt.Rows[0]["RoleID"].ToString());
                    userid = dt.Rows[0]["UserID"].ToString();
                }
            }

            //if (!IsPostBack)
            //{
            //    if (Request.QueryString["a_id"] == null)
            //    {
            //        Response.Redirect("~/LandDispute/Entry/EntryPage.aspx");
            //        return;
            //    }

            //    long applicationId = Convert.ToInt64(Request.QueryString["a_id"]);

            //    BindApplicationPreview(applicationId);
            //}

            if (!IsPostBack)
            {
                long applicationId = GetApplicationIdFromQueryString();

                if (applicationId <= 0)
                {
                    Response.Redirect("~/LandDispute/Entry/EntryPage.aspx");
                    return;
                }

                BindApplicationPreview(applicationId);
            }
        }

        private long GetApplicationIdFromQueryString()
        {
            long applicationId;

            if (!long.TryParse(Request.QueryString["a_id"], out applicationId))
                return 0;

            return applicationId;
        }

        private void BindApplicationPreview(long applicationId)
        {
            DataSet ds =_applicationPreviewDAL.GetApplicationPreview(applicationId);

            if (ds == null || ds.Tables.Count == 0)
                return;

            // 0 - Application / main land dispute information
            BindApplication(ds.Tables[0]);   //ok

            // 1 - Vadi    //ok
            BindVadi(ds.Tables[1]);

            // 2 - PratiVadi   //ok
            BindPratiVadi(ds.Tables[2]);

            // 3 - PratiVadi other information  //ok
            BindPratiVadiOtherDetails(ds.Tables[3]);

            // 4 - Khata-Khesra  //ok
            BindKhataKhesra(ds.Tables[4]);

            // 5 - Vadi evidence //ok
            BindVadiEvidence(ds.Tables[5]);

            // 6 - PratiVadi evidence //ok
            BindPratiVadiEvidence(ds.Tables[6]);

            // 7 - Police / Revenue / Halka details //ok
            BindPoliceRevenueDetails(ds.Tables[7]);

            // 8 - Land dispute events
            BindLandDisputeEvents(ds.Tables[8]);

            // 9 - Court details
            BindCourtDetails(ds.Tables[9]);

            // 10 - Action details
           BindActionDetails(ds.Tables[10]);
        }

        //----------Vadi Records---------
        private void BindVadi(DataTable dt)
        {
            rptVadi.DataSource = dt;
            rptVadi.DataBind();
        }
        private void BindPratiVadi(DataTable dt)
        {
            rptPratiwadi.DataSource = dt;
            rptPratiwadi.DataBind();
        }

        // 3 - PratiVadi other information
        private void BindPratiVadiOtherDetails(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];

    
            lblprativadi_ka_suchit.Text= ": " + dr["PrativadiKoSuchit"].ToString();
            lblprativadi_ka_Karan.Text = ": " + dr["PrativadiKoSuchitKaran"].ToString();
            lblprativadi_ka_madham.Text = ": " + dr["SuchnaKaMadhyam"].ToString();
            lblprativadi_ka_SuchnaTamil.Text = ": " + dr["SuchnaTaamila"].ToString();
            lblprativadi_ka_Upashtith.Text = ": " + dr["PrativadiUpasthit"].ToString();
        }

        // 4 - Khata-Khesra
        private void BindKhataKhesra(DataTable dt)
        {
            rptBhumiKhataKhesra.DataSource = dt;
            rptBhumiKhataKhesra.DataBind();
        }

        // 5 - Vadi evidence
        private void BindVadiEvidence(DataTable dt)
        {
            rptVadiEvidence.DataSource = dt;
            rptVadiEvidence.DataBind();
        }

        // 6 - PratiVadi evidence
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

            lblPoliceAdhikariVivarni.Text= ": " + dr["PoliceAdhikariVivran"].ToString();
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
            lblPrathamikHai.Text =  dt.Rows[0]["bhumi_vivad_Vivran_Available_Inhindi"].ToString();


            rptBhumiVivAdIncident.DataSource = dt;
            rptBhumiVivAdIncident.DataBind();

        }

        // 9 -
        private void BindCourtDetails(DataTable dt)
        {

            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];
            lblPrakiriyadhinVadAvailable.Text= ": "+ dt.Rows[0]["dispute_in_court_available"].ToString();

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

            SetPdfButton( lnkCircleOfficer_letterOfIntent, dr["CircleOfficer_letterOfIntent"] );

            SetPdfButton( lnkPoliceOfficer_letterOfIntent, dr["PoliceOfficer_letterOfIntent"] );
        }

        private void BindApplication(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];

            // Application information
            lblApplicationNo.Text = ": " + dr["ApplicationNo"].ToString();
         
            lblAppDate.Text = ": " + dr["AavedanKiTithi"].ToString();

            lbla_id.Value = ": " + dr["a_id"].ToString();

            // भूमि विवाद का विवरण
            lblDistrict.Text = ": " + dr["District"].ToString();
            lblSubdivision.Text = ": " + dr["Subdivision"].ToString();
            lblBlock.Text = ": " + dr["Block"].ToString();
            lblPolice_Station.Text = ": " + dr["Police_Station"].ToString();

            lblAreaType.Text = ": " + dr["AreaType"].ToString();

            lblPanchayatName.Text = ": " + dr["Panchayat"].ToString();
            lblVILLNAME.Text = ": " + dr["Village"].ToString();
            lblWARDNAME.Text = ": " + dr["Ward"].ToString();

            lblvadi_Vivad_Ka_AadyatanKaran.Text = ": " + dr["BhumiVivadType"].ToString();

            lblvadi_rajashv_sankhaya.Text = ": " + dr["RajasvThanaSankhya"].ToString();

            lblVadi_BhumiKaPrakar.Text = ": " + dr["Bhumitype_Ka_Prakar"].ToString();

            lblvadi_sarkari_bhumi_ka_prakar.Text = ": " + dr["Sarkari_Bhumitype"].ToString();

            lblvadi_Sarkari_bhumi_ka_Prakar_ager_anya.Text = ": " + dr["SarkariBhumiType_Anya"].ToString();

            lblBhumiKa_VivadPrakar.Text = ": " + dr["BhumiVivadType"].ToString();

            lblvadi_Bhumivivad_Prakar_Anaya.Text = ": " + dr["BhumiVivadType_Anya"].ToString();

     
            lblVadiKabhumiVivaran.Text = ": " + dr["VadiVivarani"].ToString();

            lblPrativadiKabhumiVivaran.Text = ": " + dr["PrativadiVivarani"].ToString();

            SetPdfButton( lnkAppDoc, dr["Vadi_sakshya_File"]);

            SetPdfButton( lnkPrativadiDoc, dr["Prativadi_sakshya_File"] );


           
        }

  
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LandDispute/Entry/EntryPage.aspx?a_id=" + Request.QueryString["a_id"]);
        }

        protected void btnFinalSubmit_Click(object sender, EventArgs e)
        {
            long applicationId = Convert.ToInt64(Request.QueryString["a_id"]);

            //string applicationNo = GenerateApplicationNo(applicationId);
            string applicationNo = _finalSubmitDAL.GenerateApplicationNo( applicationId, userid);

            if (string.IsNullOrEmpty(applicationNo))
            {
                lblApplicationNo.Text ="Application number could not be generated.";

                return;
            }

            //lblApplicationNo.Text = "Application Number : " + applicationNo;
            lblApplicationNo.Text =  applicationNo;

            btnFinalSubmit.Enabled = false;

            btnEdit.Enabled = false;
           
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

        //-------------------Vadi Evidence Document View
        protected void rptVadiEvidence_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "View")
                return;

            string filePath = Convert.ToString(e.CommandArgument);

            string url = GetDocumentUrl(filePath);

            if (string.IsNullOrWhiteSpace(url))
                return;

            string script = "window.open('" + HttpUtility.JavaScriptStringEncode(url) + "', '_blank');";

            ScriptManager.RegisterStartupScript( this,  GetType(), "ViewVadiPdf_" + Guid.NewGuid().ToString("N"), script, true );
        }

        protected void rptPratiwadiEvidence_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "View")
                return;

            string filePath = Convert.ToString(e.CommandArgument);

            string url = GetDocumentUrl(filePath);

            if (string.IsNullOrWhiteSpace(url))  return;

            string script = "window.open('" +  HttpUtility.JavaScriptStringEncode(url) +  "', '_blank');";

            ScriptManager.RegisterStartupScript(  this, GetType(), "ViewPratiwadiPdf_" + Guid.NewGuid().ToString("N"), script, true );
        }

        //private string GenerateApplicationNo(long applicationId)
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conns"].ConnectionString))
        //    {
        //        con.Open();

        //        SqlCommand cmd = new SqlCommand("BS_SP_FinalSubmit", con);

        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;
        //        cmd.Parameters.Add("@CUUser", SqlDbType.NVarChar, 50).Value = userid;

        //        return cmd.ExecuteScalar().ToString();
        //    }
        //}
    }
}