using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Bhusamadhan.DataAccessLayer.LandDisputeDAL
{
    public class AddAnotherMeetingDAL
    {
        private readonly string conStr = ConfigurationManager.ConnectionStrings["conns"].ConnectionString;


        public int GetPreviousMeetingCount(long applicationId)
        {
            const string sql = @" SELECT COUNT(1) FROM BS_ActionDetailsEntry  WHERE a_id = @a_id;";

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;

                con.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }


        public bool SaveAnotherMeeting( long applicationId, DateTime? meetingDate, string isVadiPresent, string isPratiVadiPresent, string conclusionOfMeeting, string anchalaDhikariMantavy, string thanaPrabhariMantavy, string jointReportFile, long? matterStatus, string matterStatusBy, DateTime? matterStatusDate,  DateTime? dateOfDisposal, string reasonForRejection, DateTime? mapiKiTithi, DateTime? agaliSunavaeeKiTithi, string circleOfficerLetter,  string policeOfficerLetter, long? bhumiSavedansheelta,   string mapikaPrativadan, DateTime? mapiKiNirdharitThith,string userid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                using (SqlCommand cmd = new SqlCommand("BS_SP_Insert_Another_Metting", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@a_id", SqlDbType.BigInt).Value = applicationId;

                    cmd.Parameters.Add("@Meeting_date", SqlDbType.DateTime).Value = ToDbValue(meetingDate);

                    cmd.Parameters.Add("@Is_Vadi_Present", SqlDbType.Char, 1)
                        .Value = ToDbValue(isVadiPresent);

                    cmd.Parameters.Add("@Is_PratiVadi_Present", SqlDbType.Char, 1).Value = ToDbValue(isPratiVadiPresent);

                    cmd.Parameters.Add("@conclusion_of_the_meeting", SqlDbType.NVarChar, 500).Value = ToDbValue(conclusionOfMeeting);

                    cmd.Parameters.Add("@anchala_dhikari_mantavy", SqlDbType.NVarChar, 500).Value = ToDbValue(anchalaDhikariMantavy);

                    cmd.Parameters.Add("@thana_prabhari_mantavy", SqlDbType.NVarChar, 500).Value = ToDbValue(thanaPrabhariMantavy);

                    cmd.Parameters.Add("@Joint_report_SHO_Circle_Officer_file", SqlDbType.NVarChar, 500).Value = ToDbValue(jointReportFile);

                    cmd.Parameters.Add("@Matter_Status", SqlDbType.BigInt).Value = ToDbValue(matterStatus);

                    cmd.Parameters.Add("@Matter_Status_by", SqlDbType.NVarChar, 30).Value = ToDbValue(matterStatusBy);

                    cmd.Parameters.Add("@Matter_Status_date", SqlDbType.DateTime).Value = ToDbValue(matterStatusDate);

                    cmd.Parameters.Add("@date_of_disposal", SqlDbType.DateTime).Value = ToDbValue(dateOfDisposal);

                    cmd.Parameters.Add("@reason_for_rejection", SqlDbType.NVarChar, 500).Value = ToDbValue(reasonForRejection);

                    cmd.Parameters.Add("@mapi_ki_tithi", SqlDbType.DateTime).Value = ToDbValue(mapiKiTithi);

                    cmd.Parameters.Add("@agali_sunavaee_ki_tithi", SqlDbType.DateTime).Value = ToDbValue(agaliSunavaeeKiTithi);

                    cmd.Parameters.Add("@CircleOfficer_letterOfIntent", SqlDbType.NVarChar, 500).Value = ToDbValue(circleOfficerLetter);

                    cmd.Parameters.Add("@PoliceOfficer_letterOfIntent", SqlDbType.NVarChar, 500).Value = ToDbValue(policeOfficerLetter);

                    cmd.Parameters.Add("@Bhumi_savedansheelta", SqlDbType.BigInt).Value = ToDbValue(bhumiSavedansheelta);

                    //cmd.Parameters.Add("@vaadi_ki_vaad_sankhya_varsh", SqlDbType.VarChar, 50).Value = ToDbValue(vaadiKiVaadSankhyaVarsh);

                    cmd.Parameters.Add("@MapikaPrativadan", SqlDbType.NVarChar, -1).Value = ToDbValue(mapikaPrativadan);

                    cmd.Parameters.Add("@MapiKiNirdharitThith", SqlDbType.DateTime).Value = ToDbValue(mapiKiNirdharitThith);
                    cmd.Parameters.Add("@CUUser", SqlDbType.NVarChar).Value = userid;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error in BS_Insert_Another_Metting: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AddAnotherMeetingDAL.SaveAnotherMeeting: " + ex.Message, ex);
            }
        }


        private object ToDbValue(object value)
        {
            if (value == null)
                return DBNull.Value;

            if (value is string text && string.IsNullOrWhiteSpace(text))
                return DBNull.Value;

            return value is string ? ((string)value).Trim() : value;
        }
    }
}