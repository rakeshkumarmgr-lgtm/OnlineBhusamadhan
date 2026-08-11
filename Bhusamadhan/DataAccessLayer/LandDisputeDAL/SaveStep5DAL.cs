using System;
using System.Data;
using System.Data.SqlClient;

namespace Bhusamadhan.DataAccessLayer.LandDisputeDAL
{
    public class SaveStep5DAL
    {
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
    }
}