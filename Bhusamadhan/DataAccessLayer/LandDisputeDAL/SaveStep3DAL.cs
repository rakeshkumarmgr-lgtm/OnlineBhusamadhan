using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bhusamadhan.DataAccessLayer.LandDisputeDAL
{
    
    public class SaveStep3DAL
    {
        private readonly string conStr = ConfigurationManager.ConnectionStrings["conns"].ConnectionString;

        public long SaveStep3(long applicationId, DataTable dtKhataKhesraForDb, string userid, SqlConnection con, SqlTransaction trans)
        {
            using (SqlCommand cmd = new SqlCommand("BS_SP_SaveStep3", con, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@a_id", applicationId);

                SqlParameter tvpParameter = cmd.Parameters.AddWithValue("@LandDetailsEntryTable", dtKhataKhesraForDb);

                tvpParameter.SqlDbType = SqlDbType.Structured;
                tvpParameter.TypeName = "dbo.LandDetailsEntryType";

                cmd.Parameters.Add("@CUUser", SqlDbType.NVarChar).Value = userid;

                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

        public DataTable GetKhataKhesraDetails(long applicationId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from BS_VW_GetKhataKhesra_Step3 WHERE a_id = @a_id", con))
                {
                    cmd.CommandType = CommandType.Text;

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