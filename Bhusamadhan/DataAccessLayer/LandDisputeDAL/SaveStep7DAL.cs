using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Bhusamadhan.DataAccessLayer.LandDisputeDAL
{
    public class SaveStep7DAL
    {
        private readonly string conStr = ConfigurationManager.ConnectionStrings["conns"].ConnectionString;
        public void SaveStep7( long applicationId, string userid,  DataTable dtAction, SqlConnection con, SqlTransaction trans)
        {
            using (SqlCommand cmd = new SqlCommand("BS_SP_SaveStep7", con, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;
                cmd.Parameters.Add("@CUUser", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(userid) ? (object)DBNull.Value : userid;

                SqlParameter tvp = cmd.Parameters.Add( "@ActionDetailsEntryTable", SqlDbType.Structured);

                tvp.TypeName = "dbo.BS_ActionDetailsEntryType";
                tvp.Value = dtAction;

                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetStep7(long applicationId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT top 1 * FROM BS_ActionDetailsEntry WHERE a_id = @a_id ORDER BY id DESC", con))
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