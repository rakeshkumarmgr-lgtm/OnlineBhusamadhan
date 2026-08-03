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

        public long SaveStep1( long applicationId, string districtCode, string blockCode, string villageCode, string userId, SqlConnection con,  SqlTransaction trans)
        {
            using (SqlCommand cmd = new SqlCommand("BS_SP_SaveStep1", con, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@a_id", applicationId);
                cmd.Parameters.AddWithValue("@District_Code", districtCode);
                cmd.Parameters.AddWithValue("@Block_Code", blockCode);
                cmd.Parameters.AddWithValue("@Village", villageCode);
                cmd.Parameters.AddWithValue("@CUUser", userId);

                return Convert.ToInt64(cmd.ExecuteScalar());
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
    }
}