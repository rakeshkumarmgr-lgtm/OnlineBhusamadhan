using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bhusamadhan.DataAccessLayer.LandDisputeDAL
{
    public class FinalSubmitDAL
    {
        private readonly string conStr = ConfigurationManager.ConnectionStrings["conns"].ConnectionString;
        public string GenerateApplicationNo(long applicationId, string userId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("BS_SP_FinalSubmit", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;

                    cmd.Parameters.Add("@CUUser", SqlDbType.NVarChar, 50).Value = userId;

                    con.Open();

                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        return string.Empty;

                    return result.ToString();
                }
            }
        }
    }
}