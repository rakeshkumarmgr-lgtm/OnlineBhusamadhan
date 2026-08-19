using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Bhusamadhan.DataAccessLayer.LandDisputeDAL
{
    public class SaveStep5DAL
    {
        private readonly string conStr = ConfigurationManager.ConnectionStrings["conns"].ConnectionString;
        public bool SaveStep5( long applicationId, string pulisPadadhikariVivarani, string pulisPadadhikarPatrFile, string halkaKarmchariVivran, string halkaKarmchariPatrFile,  string vivaditBhukhandMapiKiAvashyaktaHai,  string vivaditBhukhandMapi, string maapeeKeLieNirdhaaritTithi,  string vivaaditBhukhandMapiFile, string vivaaditBhukhandMapiReason,  SqlConnection con, SqlTransaction trans)
        {
            using (SqlCommand cmd = new SqlCommand("BS_SP_SaveStep5", con, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;

                cmd.Parameters.Add("@pulis_padadhikari_vivarani", SqlDbType.NVarChar, -1) .Value = pulisPadadhikariVivarani ?? string.Empty;

                cmd.Parameters.Add("@pulis_padadhikar_Patr_file", SqlDbType.NVarChar, 500) .Value = pulisPadadhikarPatrFile ?? string.Empty;

                cmd.Parameters.Add("@HalkaKarmchari_vivran", SqlDbType.NVarChar, -1).Value = halkaKarmchariVivran ?? string.Empty;

                cmd.Parameters.Add("@HalkaKarmchari_Patr_file", SqlDbType.NVarChar, 500) .Value = halkaKarmchariPatrFile ?? string.Empty;

                cmd.Parameters.Add("@vivadit_bhukhand_Mapi_ki_avashyakta_hai", SqlDbType.NVarChar, 10) .Value = vivaditBhukhandMapiKiAvashyaktaHai ?? string.Empty;

                cmd.Parameters.Add("@vivadit_bhukhand_Mapi", SqlDbType.NVarChar, 10) .Value = vivaditBhukhandMapi ?? string.Empty;

                cmd.Parameters.Add("@maapee_ke_lie_nirdhaarit_tithi", SqlDbType.VarChar, 10) .Value = string.IsNullOrWhiteSpace(maapeeKeLieNirdhaaritTithi)  ? "01-01-1900" : maapeeKeLieNirdhaaritTithi;

                cmd.Parameters.Add("@vivaadit_bhukhand_Mapi_File", SqlDbType.NVarChar, 500) .Value = vivaaditBhukhandMapiFile ?? string.Empty;

                cmd.Parameters.Add("@vivaadit_bhukhand_Mapi_Reason", SqlDbType.NVarChar, -1) .Value = vivaaditBhukhandMapiReason ?? string.Empty;

                int rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());

                return rowsAffected > 0;
            }
        }

        public DataTable GetStep5Details(long applicationId)
        {
            DataTable dt = new DataTable();


            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(@" select pulis_padadhikari_vivarani,pulis_padadhikar_Patr_file,HalkaKarmchari_vivran ,HalkaKarmchari_Patr_file,vivadit_bhukhand_Mapi_ki_avashyakta_hai,vivadit_bhukhand_Mapi,convert (varchar(10),maapee_ke_lie_nirdhaarit_tithi,105) as maapee_ke_lie_nirdhaarit_tithi ,vivaadit_bhukhand_Mapi_File,vivaadit_bhukhand_Mapi_Reason from  BS_Matter_Registration where a_id=@a_id and isnull(vivadit_bhukhand_Mapi_ki_avashyakta_hai,'')<>''", con))
                {
                    cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }


    }
}