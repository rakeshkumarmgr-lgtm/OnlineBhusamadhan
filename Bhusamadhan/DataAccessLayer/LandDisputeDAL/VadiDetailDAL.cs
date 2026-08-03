using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bhusamadhan.DataAccessLayer.LandDisputeDAL
{
    public class VadiDetailDAL
    {
        public void SaveVadiDetails( long applicationId,  DataTable dtVadi, SqlConnection con, SqlTransaction trans)
        {
            if (dtVadi == null || dtVadi.Rows.Count == 0)
                return;

            using (SqlCommand cmd = new SqlCommand("BS_SP_SaveVadiDetails", con, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;

                SqlParameter tvp = cmd.Parameters.Add("@VadiDetails", SqlDbType.Structured);

                tvp.TypeName = "dbo.BS_VadiDetailType";
                tvp.Value = dtVadi;

                cmd.ExecuteNonQuery();
            }
        }
    }
}