using Bhusamadhan.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bhusamadhan.DataAccessLayer.LandDisputeDAL
{
    public class MatterRegistrationDAL
    {
        private readonly string conStr = ConfigurationManager.ConnectionStrings["conns"].ConnectionString;

        public long SaveStep1( long applicationId, int commCode, string rajasvThaanaSankhya, int bhumiType, int sarkariBhumiType, string sarkariBhumiTypeAnya, int bhumiVivadType, string bhumiVivadTypeAnya, int bhumiVivadKaAdyatanSthiti,int districtCode,int subDivCode, int blockCode, int thanaCode, int panchayatCode,string panchayatAnya,string areaType, int village, string villageAnya,int wardNo, string wardNoAnya,string vadiSakshyaFile, string prativadiSakshyaFile, DateTime? aavedanKiTithi, string vadiVivarani, string prativadiVivarani, string guid, string userId, string ipAddress, SqlConnection con, SqlTransaction trans)
        {
            using (SqlCommand cmd = new SqlCommand("BS_sp_SaveStep1", con, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@a_id", applicationId);

                cmd.Parameters.AddWithValue("@Comm_Code", commCode);
                cmd.Parameters.AddWithValue("@rajasv_thaana_sankhya", string.IsNullOrWhiteSpace(rajasvThaanaSankhya) ? (object)DBNull.Value : rajasvThaanaSankhya);

                cmd.Parameters.AddWithValue("@Bhumitype", bhumiType);
                cmd.Parameters.AddWithValue("@SarkariBhumiType", sarkariBhumiType);
                cmd.Parameters.AddWithValue("@SarkariBhumiType_Anya",  string.IsNullOrWhiteSpace(sarkariBhumiTypeAnya) ? (object)DBNull.Value : sarkariBhumiTypeAnya);

                cmd.Parameters.AddWithValue("@BhumiVivadType", bhumiVivadType);
                cmd.Parameters.AddWithValue("@BhumiVivadType_Anya",  string.IsNullOrWhiteSpace(bhumiVivadTypeAnya) ? (object)DBNull.Value : bhumiVivadTypeAnya);

                cmd.Parameters.AddWithValue("@bhumi_vivad_ka_adyatan_sthiti", bhumiVivadKaAdyatanSthiti);

                cmd.Parameters.AddWithValue("@District_Code", districtCode);
                cmd.Parameters.AddWithValue("@Sub_DivCode", subDivCode);
                cmd.Parameters.AddWithValue("@Block_Code", blockCode);
                cmd.Parameters.AddWithValue("@Thana_code", thanaCode);

                cmd.Parameters.AddWithValue("@Panchayat_Code", panchayatCode);
                cmd.Parameters.AddWithValue("@Panchayat_Anya", string.IsNullOrWhiteSpace(panchayatAnya) ? (object)DBNull.Value : panchayatAnya);

                cmd.Parameters.AddWithValue("@AreaType",  string.IsNullOrWhiteSpace(areaType) ? (object)DBNull.Value : areaType);

                cmd.Parameters.AddWithValue("@Village", village);

                cmd.Parameters.AddWithValue("@Village_Anya", string.IsNullOrWhiteSpace(villageAnya) ? (object)DBNull.Value : villageAnya);

                cmd.Parameters.AddWithValue("@WardNo", wardNo);

                cmd.Parameters.AddWithValue("@WardNo_Anya", string.IsNullOrWhiteSpace(wardNoAnya) ? (object)DBNull.Value : wardNoAnya);

                cmd.Parameters.AddWithValue("@Vadi_sakshya_File", string.IsNullOrWhiteSpace(vadiSakshyaFile) ? (object)DBNull.Value : vadiSakshyaFile);

                cmd.Parameters.AddWithValue("@Prativadi_sakshya_File", string.IsNullOrWhiteSpace(prativadiSakshyaFile) ? (object)DBNull.Value : prativadiSakshyaFile);

                cmd.Parameters.AddWithValue("@AavedanKiTithi", aavedanKiTithi.HasValue ? (object)aavedanKiTithi.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@VadiVivarani", string.IsNullOrWhiteSpace(vadiVivarani) ? (object)DBNull.Value : vadiVivarani);

                cmd.Parameters.AddWithValue("@PrativadiVivarani", string.IsNullOrWhiteSpace(prativadiVivarani) ? (object)DBNull.Value : prativadiVivarani);

                cmd.Parameters.AddWithValue("@Guid",  string.IsNullOrWhiteSpace(guid) ? (object)DBNull.Value : guid);

                cmd.Parameters.AddWithValue("@CUUser", userId);

                cmd.Parameters.AddWithValue("@CUIPAddress",  string.IsNullOrWhiteSpace(ipAddress) ? (object)DBNull.Value : ipAddress);

                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

        public void SaveVadiDetails(long applicationId, DataTable dtForDb, string userid, SqlConnection con, SqlTransaction trans)
        {
            if (dtForDb == null || dtForDb.Rows.Count == 0)
                return;

            using (SqlCommand cmd = new SqlCommand("BS_SP_SaveVadiDetails", con, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;

                SqlParameter tvp = cmd.Parameters.Add("@VadiDetails", SqlDbType.Structured);

                tvp.TypeName = "dbo.BS_VadiDetailType";
                tvp.Value = dtForDb;
                cmd.Parameters.Add("@CUUser", SqlDbType.NVarChar).Value = userid;
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateCurrentStep(long applicationId, int currentStep, SqlConnection con, SqlTransaction trans)
        {
            using (SqlCommand cmd = new SqlCommand( @"UPDATE BS_Matter_Registration SET CurrentStep=@CurrentStep WHERE a_id=@a_id", con, trans))
            {
                cmd.Parameters.AddWithValue("@CurrentStep", currentStep);
                cmd.Parameters.AddWithValue("@a_id", applicationId);

                cmd.ExecuteNonQuery();
            }
        }

        public string GetApplicationGuid(long applicationId,SqlConnection con, SqlTransaction trans)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Guid FROM BS_Matter_Registration WHERE a_id=@a_id", con,  trans))
            {
                cmd.Parameters.AddWithValue("@a_id", applicationId);

                object obj = cmd.ExecuteScalar();

                return obj == null ? "" : obj.ToString();
            }
        }

        public DataRow GetUploadedFiles(long applicationId, SqlConnection con,  SqlTransaction trans)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(@" SELECT Vadi_sakshya_File, Prativadi_sakshya_File, Guid FROM BS_Matter_Registration WHERE a_id=@a_id", con))
            {
                da.SelectCommand.Transaction = trans;
                da.SelectCommand.Parameters.AddWithValue("@a_id", applicationId);

                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count == 0)
                    return null;

                return dt.Rows[0];
            }
        }

        public DataTable GetStep1(long applicationId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {

                string query = @"SELECT District_Code,  Block_Code, Panchayat_Code,  AreaType, VadiVivarani, PrativadiVivarani, Vadi_sakshya_File, Prativadi_sakshya_File FROM BS_Matter_Registration WHERE a_id=@a_id";

                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@a_id", applicationId);
                    da.Fill(dt);
                }
            }

            return dt;
        }

       
    }
}