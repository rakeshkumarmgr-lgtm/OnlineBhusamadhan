using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bhusamadhan.DataAccessLayer.LandDisputeDAL
{
    public class ApplicationPreviewDAL
    {
        private readonly string conStr = ConfigurationManager.ConnectionStrings["conns"].ConnectionString;

        public DataSet GetApplicationPreview(long applicationId)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("BS_SP_GetApplicationPreview", con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;

                da.Fill(ds);
            }

            return ds;
        }
    }
}