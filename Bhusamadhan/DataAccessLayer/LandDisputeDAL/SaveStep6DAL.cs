using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Bhusamadhan.DataAccessLayer.LandDisputeDAL
{
    public class SaveStep6DAL
    {
        private readonly string conStr = ConfigurationManager.ConnectionStrings["conns"].ConnectionString;
        public bool SaveStep6( long applicationId,  string bhumiVivadAvailable,  string disputeInCourtAvailable,  DataTable landDisputeDetails, DataTable courtDisputeDetails, string cuUser, SqlConnection con,  SqlTransaction trans)
        {
            using (SqlCommand cmd = new SqlCommand("BS_SP_SaveStep6", con, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@a_id", SqlDbType.BigInt) .Value = applicationId;

                cmd.Parameters.Add("@bhumi_vivad_Vivran_Available", SqlDbType.VarChar, 10) .Value = bhumiVivadAvailable;

                cmd.Parameters.Add("@dispute_in_court_available", SqlDbType.VarChar, 10) .Value = disputeInCourtAvailable;

                cmd.Parameters.Add("@CUUser", SqlDbType.NVarChar, 50) .Value = cuUser;

                SqlParameter landParam = cmd.Parameters.Add( "@LandDisputeDetailsEntryTable", SqlDbType.Structured);

                landParam.TypeName = "dbo.BS_LandDisputeDetailsEntryType";

                landParam.Value = landDisputeDetails;

                SqlParameter courtParam = cmd.Parameters.Add( "@CourtDisputeDetailsEntryTable",   SqlDbType.Structured);

                courtParam.TypeName = "dbo.CourtDisputeDetailsEntryType";

                courtParam.Value = courtDisputeDetails;

                object result = cmd.ExecuteScalar();

                return result != null && result != DBNull.Value && Convert.ToInt64(result) == applicationId;
            }
        }

        public DataTable GetStep6MatterDetails(long applicationId)
        {
            using (SqlConnection con = new SqlConnection( conStr))
            {
                using (SqlCommand cmd = new SqlCommand( @"SELECT  bhumi_vivad_Vivran_Available,  dispute_in_court_available  FROM BS_Matter_Registration  WHERE a_id = @a_id", con))
                {
                    cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public DataTable GetIncidentDetails(long applicationId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT  Ghatna_Vardat_date, Ghatna_Short_vivran, is_FIR_registered, praathamiki_sankhya, praathamiki_ka_vivaran,
                is_complaint_filed, dhaara, apraathamiki_sankhya, apraathamiki_ka_vivaran, Abhiyukt, is_Sanha_recorded,
                sanha_sankhya, bns as bnm, dhaaranew as newdhara, bns_oth as bnm1, dhaara_oth as newdhara1 FROM BS_LandDisputeDetailsEntry WHERE a_id = @a_id", con))
                {
                    cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public DataTable GetCourtDisputeDetails(long applicationId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(@"select  courtID, courtTypeID, District_Code,   Sub_DivCode, Vibhag_code,  vaadi_ki_vaad_sankhya_varsh, vadi_name,  prativadi_name,  vaad_ki_addhatan_sthiti_vivaran, court,courtType, Dst, SubDiv, Vibhag from BS_VW_GetLandCourtDisputeDetails WHERE a_id = @a_id", con))
                {
                    cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}