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

        public long SaveStep1( long applicationId, long commCode, string rajasvThaanaSankhya, long bhumiType, long sarkariBhumiType,  string sarkariBhumiTypeAnya, long bhumiVivadType, string bhumiVivadTypeAnya, long bhumiVivadKaAdyatanSthiti, long districtCode, long subDivCode, long blockCode, long thanaCode, long panchayatCode, string panchayatAnya, string areaType, long village,  string villageAnya,long wardNo, string wardNoAnya, string vadiSakshyaFile, string prativadiSakshyaFile,  DateTime? aavedanKiTithi,  string vadiVivarani,  string prativadiVivarani, string guid,  string userId, string ipAddress, SqlConnection con, SqlTransaction trans)
        {
            using (SqlCommand cmd = new SqlCommand("BS_sp_SaveStep1", con, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;

                cmd.Parameters.Add("@Comm_Code", SqlDbType.BigInt).Value = commCode;

                cmd.Parameters.Add("@rajasv_thaana_sankhya", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(rajasvThaanaSankhya)  ? (object)DBNull.Value : rajasvThaanaSankhya;

                cmd.Parameters.Add("@Bhumitype", SqlDbType.BigInt).Value = bhumiType;

                cmd.Parameters.Add("@SarkariBhumiType", SqlDbType.BigInt).Value = sarkariBhumiType;

                cmd.Parameters.Add("@SarkariBhumiType_Anya", SqlDbType.NVarChar, 200).Value = string.IsNullOrWhiteSpace(sarkariBhumiTypeAnya) ? (object)DBNull.Value : sarkariBhumiTypeAnya;

                cmd.Parameters.Add("@BhumiVivadType", SqlDbType.BigInt).Value = bhumiVivadType;

                cmd.Parameters.Add("@BhumiVivadType_Anya", SqlDbType.NVarChar, 200).Value = string.IsNullOrWhiteSpace(bhumiVivadTypeAnya)  ? (object)DBNull.Value : bhumiVivadTypeAnya;

                cmd.Parameters.Add("@bhumi_vivad_ka_adyatan_sthiti", SqlDbType.BigInt).Value = bhumiVivadKaAdyatanSthiti;

                cmd.Parameters.Add("@District_Code", SqlDbType.BigInt).Value = districtCode;

                cmd.Parameters.Add("@Sub_DivCode", SqlDbType.BigInt).Value = subDivCode;

                cmd.Parameters.Add("@Block_Code", SqlDbType.BigInt).Value = blockCode;

                cmd.Parameters.Add("@Thana_code", SqlDbType.BigInt).Value =  thanaCode;

                cmd.Parameters.Add("@Panchayat_Code", SqlDbType.BigInt).Value = panchayatCode;

                cmd.Parameters.Add("@Panchayat_Anya", SqlDbType.NVarChar, 200).Value = string.IsNullOrWhiteSpace(panchayatAnya) ? (object)DBNull.Value : panchayatAnya;

                cmd.Parameters.Add("@AreaType", SqlDbType.Char, 1).Value = string.IsNullOrWhiteSpace(areaType) ? (object)DBNull.Value: areaType;

                cmd.Parameters.Add("@Village", SqlDbType.BigInt).Value = village;

                cmd.Parameters.Add("@Village_Anya", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(villageAnya)  ? (object)DBNull.Value : villageAnya;

                cmd.Parameters.Add("@WardNo", SqlDbType.BigInt).Value =  wardNo;

                cmd.Parameters.Add("@WardNo_Anya", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(wardNoAnya)  ? (object)DBNull.Value : wardNoAnya;

                cmd.Parameters.Add("@Vadi_sakshya_File", SqlDbType.NVarChar, 500).Value = string.IsNullOrWhiteSpace(vadiSakshyaFile)  ? (object)DBNull.Value : vadiSakshyaFile;

                cmd.Parameters.Add("@Prativadi_sakshya_File", SqlDbType.NVarChar, 500).Value = string.IsNullOrWhiteSpace(prativadiSakshyaFile)  ? (object)DBNull.Value : prativadiSakshyaFile;

                cmd.Parameters.Add("@AavedanKiTithi", SqlDbType.Date).Value = aavedanKiTithi.HasValue ? (object)aavedanKiTithi.Value : DBNull.Value;

                cmd.Parameters.Add("@VadiVivarani", SqlDbType.NVarChar, -1).Value = string.IsNullOrWhiteSpace(vadiVivarani) ? (object)DBNull.Value : vadiVivarani;

                cmd.Parameters.Add("@PrativadiVivarani", SqlDbType.NVarChar, -1).Value = string.IsNullOrWhiteSpace(prativadiVivarani) ? (object)DBNull.Value : prativadiVivarani;

                cmd.Parameters.Add("@Guid", SqlDbType.NVarChar, 500).Value = string.IsNullOrWhiteSpace(guid) ? (object)DBNull.Value : guid;

                cmd.Parameters.Add("@CUUser", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(userId) ? (object)DBNull.Value : userId;

                cmd.Parameters.Add("@CUIPAddress", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(ipAddress) ? (object)DBNull.Value : ipAddress;

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

                string query = @"select   Range_Code,Comm_Code,rajasv_thaana_sankhya ,Bhumitype ,SarkariBhumiType,SarkariBhumiType_Anya ,BhumiVivadType ,BhumiVivadType_Anya  ,bhumi_vivad_ka_adyatan_sthiti ,District_Code,Sub_DivCode,Block_Code,Thana_code ,Panchayat_Code ,Panchayat_Anya ,AreaType,Village ,Village_Anya  ,WardNo,WardNo_Anya ,rtrim(ltrim(Vadi_sakshya_File)) as Vadi_sakshya_File ,rtrim(ltrim(Prativadi_sakshya_File)) as Prativadi_sakshya_File  ,dispute_sensitivity ,convert(varchar(10),AavedanKiTithi ,105) as AavedanKiTithi,VadiVivarani,PrativadiVivarani from BS_Matter_Registration  WHERE a_id=@a_id";

                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@a_id", applicationId);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GetUnfinalizedApplications(string userId, string mobileNo)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                #region
                //    string query = @"SELECT  m.a_id, d.DISTRICTNAME, s.Sd_Name_En, b.BlockName,  th.Police_Station,

                //    CASE
                //        WHEN m.Panchayat_Code = '-1'
                //            THEN m.Panchayat_Anya
                //        ELSE p.PanchayatName
                //    END AS PanchayatName,

                //    CASE
                //        WHEN m.Village = '-1'
                //            THEN m.Village_Anya
                //        ELSE vill.VILLNAME
                //    END AS VILLNAME,

                //    (SELECT TOP 1 vadi_Name FROM BS_VadiDetailEntry  WHERE a_id = m.a_id ) + ' (' + CONVERT(VARCHAR(10), ISNULL(v.TotalVadi, 0)) + ')' AS vadi_Name,

                //    (SELECT TOP 1 Vadi_MobileNo FROM BS_VadiDetailEntry WHERE a_id = m.a_id ) AS Vadi_MobileNo,

                //    ISNULL((SELECT TOP 1 pratiVadi_Name FROM BS_PrtiVadiDetailEntry  WHERE a_id = m.a_id ), '' ) + ' (' + CONVERT(VARCHAR(10), ISNULL(pv.TotalPratiVadi, 0)) + ')'  AS pratiVadi_Name,

                //    CASE
                //        WHEN m.Bhumitype = 1
                //            THEN N'रैयती'
                //        ELSE N'सरकारी'
                //    END AS Bhumitype, bv.vivadtype,m.CurrentStep

                //FROM BS_Matter_Registration m

                //INNER JOIN mst_bhumiVivad_type bv ON bv.id = m.BhumiVivadType

                //INNER JOIN mst_thana th  ON th.PS_Code = m.Thana_code

                //LEFT JOIN mst_Panchayats p ON p.BlockCode = m.Block_Code AND p.AreaType = m.AreaType AND p.PanchayatCode = m.Panchayat_Code

                //LEFT JOIN mst_VillageMaster vill ON vill.VILLCODE = m.Village

                //LEFT JOIN mst_Commissionary_Districts d ON d.DISTRICTCODE = m.District_Code

                //INNER JOIN SubDivisions s ON s.Sd_Code2 = m.Sub_DivCode

                //INNER JOIN Blocks b  ON b.BlockCode = m.Block_Code

                //LEFT JOIN ( SELECT  a_id, COUNT(*) AS TotalVadi FROM BS_VadiDetailEntry GROUP BY a_id ) v ON v.a_id = m.a_id

                //LEFT JOIN ( SELECT a_id,COUNT(*) AS TotalPratiVadi FROM BS_PrtiVadiDetailEntry GROUP BY a_id ) pv  ON pv.a_id = m.a_id

                //WHERE m.CUUser = @CUUser AND ISNULL(m.Final, 0) = 0
                //AND ( @MobileNo = ''  OR EXISTS ( SELECT 1 FROM BS_VadiDetailEntry sv WHERE sv.a_id = m.a_id  AND sv.Vadi_MobileNo LIKE '%' + @MobileNo + '%') )

                //ORDER BY  m.a_id DESC;";
                #endregion

                using (SqlCommand cmd = new SqlCommand("BS_GetUnfinalizedApplication", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CUUser", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(userId) ? (object)DBNull.Value : userId;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.NVarChar, 20).Value = string.IsNullOrWhiteSpace(mobileNo) ? "" : mobileNo.Trim();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GetFinalizedApplications( string userId, string searchText,  long matterStatus)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(  "BS_GetFinalizedApplication", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CUUser", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(userId) ? (object)DBNull.Value: userId.Trim();

                    cmd.Parameters.Add("@SearchText", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(searchText)  ? "" : searchText.Trim();

                    cmd.Parameters.Add("@Matter_Status", SqlDbType.BigInt).Value = matterStatus;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }


        public DataTable GetFinalizedApplicationsForMeeting(string userId, string searchText, long matterStatus)
        {
            DataTable dt1 = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("BS_GetFinalizedApplicationForMeeting", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CUUser", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(userId) ? (object)DBNull.Value : userId.Trim();

                    cmd.Parameters.Add("@SearchText", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(searchText) ? "" : searchText.Trim();

                    cmd.Parameters.Add("@Matter_Status", SqlDbType.BigInt).Value = matterStatus;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt1);
                    }
                }
            }

            return dt1;
        }


    }
}