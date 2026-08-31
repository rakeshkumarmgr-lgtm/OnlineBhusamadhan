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
    public class ApplicationStepMgmtDAL
    {
        string conStr = DBConHelper.GetConnectionString();

        public DataTable SearchApplication(string searchValue)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql;

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;

                    long applicationId;

                    // Search by a_id
                    if (long.TryParse(searchValue, out applicationId))
                    {
                        sql = @" SELECT * FROM BS_VW_GetApplicationStepManagement WHERE a_id = @a_id";

                        cmd.CommandText = sql;

                        cmd.Parameters.Add( "@a_id", SqlDbType.BigInt).Value = applicationId;
                    }
                    else
                    {
                       
                        sql = @" SELECT * FROM BS_VW_GetApplicationStepManagement WHERE ApplicationNo = @ApplicationNo";

                        cmd.CommandText = sql;

                        cmd.Parameters.Add( "@ApplicationNo",  SqlDbType.NVarChar, 50).Value = searchValue;
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }


        public bool UpdateCurrentStep( long applicationId, int newStep, string userId,int oldStep)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd =  new SqlCommand(" UPDATE BS_Matter_Registration SET CurrentStep = @CurrentStep WHERE a_id = @a_id;", con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add( "@a_id", SqlDbType.BigInt) .Value = applicationId;

                    cmd.Parameters.Add( "@CurrentStep", SqlDbType.Int) .Value = newStep;

                    //cmd.Parameters.Add( "@UpdatedBy", SqlDbType.NVarChar, 100).Value = userId;

                    //cmd.Parameters.Add( "@OldStep", SqlDbType.Int) .Value = oldStep;

                    con.Open();

                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0;
                }
            }
        }
    }
}