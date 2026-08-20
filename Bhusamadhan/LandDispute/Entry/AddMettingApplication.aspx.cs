using Bhusamadhan.DataAccessLayer.LandDisputeDAL;
using Bhusamadhan.DB;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

            // 0 - Application / main land dispute information
            BindApplication(ds.Tables[0]);   //ok

            // 1 - Vadi    //ok
            //BindVadi(ds.Tables[1]);

            // 2 - PratiVadi   //ok
            //BindPratiVadi(ds.Tables[2]);

            // 3 - PratiVadi other information  //ok
            //BindPratiVadiOtherDetails(ds.Tables[3]);

            // 4 - Khata-Khesra  //ok
            //BindKhataKhesra(ds.Tables[4]);

            // 5 - Vadi evidence //ok
            //BindVadiEvidence(ds.Tables[5]);

            // 6 - PratiVadi evidence //ok
            //BindPratiVadiEvidence(ds.Tables[6]);

            // 7 - Police / Revenue / Halka details //ok
            //BindPoliceRevenueDetails(ds.Tables[7]);

            // 8 - Land dispute events
            //BindLandDisputeEvents(ds.Tables[8]);

            // 9 - Court details
            //BindCourtDetails(ds.Tables[9]);

            // 10 - Action details
            //BindActionDetails(ds.Tables[10]);
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
            lblDistrict.Text = ": "+ dr["District"].ToString();
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

            lblvadi_Vivad_Ki_Adyatan_Sthithi.Text= ": " + dr["BhumiVivadKaAdyatanSthiti"].ToString();
            lblVadiKabhumiVivaran.Text = ": " + dr["VadiVivarani"].ToString();

            lblPrativadiKabhumiVivaran.Text = ": " + dr["PrativadiVivarani"].ToString();

            SetPdfButton(lnkAppDoc, dr["Vadi_sakshya_File"]);

            SetPdfButton(lnkPrativadiDoc, dr["Prativadi_sakshya_File"]);



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