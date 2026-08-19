using Bhusamadhan.DataAccessLayer.LandDisputeDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.LandDispute.Entry
{
    public partial class ApplicationPrint : System.Web.UI.Page
    {
        private readonly ApplicationPreviewDAL _applicationPreviewDAL = new ApplicationPreviewDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long applicationId = GetApplicationIdFromQueryString();

                if (applicationId <= 0)
                {
                    Response.Redirect("~/LandDispute/ApplicationPreview.aspx");
                    return;
                }

                BindApplicationPrint(applicationId);
            }
        }

        private long GetApplicationIdFromQueryString()
        {
            long applicationId;

            if (!long.TryParse(Request.QueryString["a_id"], out applicationId))
                return 0;

            return applicationId;
        }

        private void BindApplicationPrint(long applicationId)
        {
            DataSet ds = _applicationPreviewDAL.GetApplicationPreview(applicationId);

            if (ds == null || ds.Tables.Count == 0)
                return;

            BindApplication(ds.Tables[0]);   //ok

           
            BindVadi(ds.Tables[1]);

           
            BindPratiVadi(ds.Tables[2]);

            // -- PratiVadi other information  //ok
            BindPratiVadiOtherDetails(ds.Tables[3]);

            
            BindKhataKhesra(ds.Tables[4]);

            BindVadiEvidence(ds.Tables[5]);

          
            BindPratiVadiEvidence(ds.Tables[6]);

      
            BindPoliceRevenueDetails(ds.Tables[7]);

            BindLandDisputeEvents(ds.Tables[8]);

  
            BindCourtDetails(ds.Tables[9]);

            BindActionDetails(ds.Tables[10]);
        }

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

     
        private void BindPratiVadiOtherDetails(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];


            lblprativadi_ka_suchit.Text = dr["PrativadiKoSuchit"].ToString();
            lblprativadi_ka_Karan.Text = dr["PrativadiKoSuchitKaran"].ToString();
            lblprativadi_ka_madham.Text = dr["SuchnaKaMadhyam"].ToString();
            lblprativadi_ka_SuchnaTamil.Text = dr["SuchnaTaamila"].ToString();
            lblprativadi_ka_Upashtith.Text = dr["PrativadiUpasthit"].ToString();
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

        private void BindPoliceRevenueDetails(DataTable dt)
        {

            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];

            lblPoliceAdhikariVivarni.Text = dr["PoliceAdhikariVivran"].ToString();
            lblHalkaKarmchariVivarni.Text = dr["HalkaKarmchariVivran"].ToString();
            lblVivaditBhukandKiMapiKaReasonHai.Text = dr["VivaditBhukhandMapiAvashyakta"].ToString();
            lblMapiValue.Text = dr["VivaditBhukhandMapi"].ToString();
            lblVivaditBhumiKaMapiNahiHoneKaKaran.Text = dr["VivaditBhukhandMapiReason"].ToString();
            lblMapiKeNirdharnKiThithiValue.Text = dr["MapiKeLieNirdharitTithi"].ToString();

        }

        private void BindLandDisputeEvents(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];
            lblPrathamikHai.Text = dt.Rows[0]["bhumi_vivad_Vivran_Available_Inhindi"].ToString();

            rptBhumiVivAdIncident.DataSource = dt;
            rptBhumiVivAdIncident.DataBind();

        }

      
        private void BindCourtDetails(DataTable dt)
        {

            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];
            lblPrakiriyadhinVadAvailable.Text = dt.Rows[0]["dispute_in_court_available"].ToString();

            rptNyayalayVivran.DataSource = dt;
            rptNyayalayVivran.DataBind();
        }

     
        public void BindActionDetails(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];

            lblVivaadKiSanvedanasheelata.Text = dt.Rows[0]["SensitivityType"].ToString();
            lblBaithakKiTithi.Text = dt.Rows[0]["Meeting_date"].ToString();
            lblkyaVaadeeUpasthitHai.Text = dt.Rows[0]["Is_Vadi_Present"].ToString();
            lblKyaPrativaadeeUpasthitHai.Text = dt.Rows[0]["Is_PratiVadi_Present"].ToString();
            lblBaithakKaNishkarsh.Text = dt.Rows[0]["BaithakKaNishkarsh"].ToString();

            lblAsveekrtiKaKaaran.Text = dt.Rows[0]["reason_for_rejection"].ToString();
            lblvadikaVadSankhyaVarsh.Text = dt.Rows[0]["vaadi_ki_vaad_sankhya_varsh"].ToString();

            lblBaithakMeinLiyaGayaNirnay.Text = dt.Rows[0]["conclusion_of_the_meeting"].ToString();
            lblAnchalaadhikaareeKaMantavy.Text = dt.Rows[0]["anchala_dhikari_mantavy"].ToString();
            lblThaanaadhyakshKaMantavy.Text = dt.Rows[0]["thana_prabhari_mantavy"].ToString();

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
            lblDistrict.Text = dr["District"].ToString();
            lblSubdivision.Text = dr["Subdivision"].ToString();
            lblBlock.Text = dr["Block"].ToString();
            lblPolice_Station.Text = dr["Police_Station"].ToString();

            lblAreaType.Text = dr["AreaType"].ToString();

            lblPanchayatName.Text = dr["Panchayat"].ToString();
            lblVILLNAME.Text = dr["Village"].ToString();
            lblWARDNAME.Text = dr["Ward"].ToString();

            lblvadi_Vivad_Ka_AadyatanKaran.Text = dr["BhumiVivadType"].ToString();

            lblvadi_rajashv_sankhaya.Text = dr["RajasvThanaSankhya"].ToString();

            lblVadi_BhumiKaPrakar.Text = dr["Bhumitype_Ka_Prakar"].ToString();

            lblvadi_sarkari_bhumi_ka_prakar.Text = dr["Sarkari_Bhumitype"].ToString();

            lblvadi_Sarkari_bhumi_ka_Prakar_ager_anya.Text = dr["SarkariBhumiType_Anya"].ToString();

            lblBhumiKa_VivadPrakar.Text = dr["BhumiVivadType"].ToString();

            lblvadi_Bhumivivad_Prakar_Anaya.Text = dr["BhumiVivadType_Anya"].ToString();


            lblVadiKabhumiVivaran.Text = dr["VadiVivarani"].ToString();

            lblPrativadiKabhumiVivaran.Text = dr["PrativadiVivarani"].ToString();

       

        }


    }
}