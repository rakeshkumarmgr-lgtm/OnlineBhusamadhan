using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bhusamadhan.DataAccessLayer.LandDisputeDAL
{
    public class SaveStep4DAL
    {
        private readonly string conStr = ConfigurationManager.ConnectionStrings["conns"].ConnectionString;
        public long SaveStep4(  long applicationId,  DataTable vadiEvidenceTable, DataTable prativadiEvidenceTable, string userid,  SqlConnection con, SqlTransaction trans)
        {
            using (SqlCommand cmd = new SqlCommand("BS_SP_SaveStep4", con, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;

             
                cmd.Parameters.AddWithValue("@a_id", applicationId);

              
                SqlParameter vadiParameter =  cmd.Parameters.AddWithValue( "@VadiEvidenceEntryTable", vadiEvidenceTable);

                vadiParameter.SqlDbType = SqlDbType.Structured;
                vadiParameter.TypeName = "dbo.VadiEvidenceEntryType";

              
                SqlParameter prativadiParameter = cmd.Parameters.AddWithValue( "@PrativadiEvidenceEntryTable", prativadiEvidenceTable);

                prativadiParameter.SqlDbType = SqlDbType.Structured;
                prativadiParameter.TypeName = "dbo.PrativadiEvidenceEntryType";

               
                cmd.Parameters.AddWithValue("@CUUser", userid);

               
                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

     
        public DataTable GetVadiEvidenceDetails(long applicationId)
        {
            DataTable dt = new DataTable();

          
            using (SqlConnection con =  new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT evidence_id,  evidence_anya AS evidence_any_name,  Vadi_sakshya_File AS FullfileName
                    FROM BS_Vadi_Evidence_Entry
                    WHERE a_id = @a_id
                    ORDER BY evidence_id", con))
                {
                    cmd.Parameters.Add("@a_id", SqlDbType.BigInt) .Value = applicationId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }


        public DataTable GetPrativadiEvidenceDetails(long applicationId)
        {
            DataTable dt = new DataTable();

          
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT evidence_id, evidence_anya AS evidence_any_name, Prativadi_sakshya_File AS FullfileName
                    FROM BS_Prativadi_Evidence_Entry
                    WHERE a_id = @a_id
                    ORDER BY evidence_id", con))
                {
                    cmd.Parameters.Add("@a_id", SqlDbType.BigInt) .Value = applicationId;

                    using (SqlDataAdapter da =  new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}