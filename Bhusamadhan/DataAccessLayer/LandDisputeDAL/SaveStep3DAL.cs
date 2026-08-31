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
    
    public class SaveStep3DAL
    {
        string conStr = DBConHelper.GetConnectionString();
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
                using (SqlCommand cmd = new SqlCommand("select  id, a_id,khataNo,khesraNo, RakbaNo1,Rakba_unit1,RakbaNo2,Rakba_unit2,RakbaNo3,Rakba_unit3,LandTypesInKhatian,LandDetailsInKhatian,North_chauhaddee,South_chauhaddee,East_chauhaddee, West_chauhaddee, Rakba,Landdesciption from BS_VW_GetKhataKhesra_Step3 WHERE a_id = @a_id", con))
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

        //----------------------CO-------------------------------------------------

        //public long SaveStep3_CO(long applicationId,long VivaditDistCode, long VivaditBlockCode, long VivaditMoujaCode, DataTable dtKhataKhesraForDb, string userid, SqlConnection con, SqlTransaction trans)
        public long SaveStep3_CO(long applicationId, DataTable dtKhataKhesraForDb, string userid, SqlConnection con, SqlTransaction trans)

        {
            using (SqlCommand cmd = new SqlCommand("BS_SP_SaveStep3_CO", con, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@a_id", applicationId);
                //cmd.Parameters.AddWithValue("@VivaditDistCode", VivaditDistCode);
                //cmd.Parameters.AddWithValue("@VivaditBlockCode", VivaditBlockCode);
                //cmd.Parameters.AddWithValue("@VivaditMoujaCode", VivaditMoujaCode);

                SqlParameter tvpParameter = cmd.Parameters.AddWithValue("@LandDetailsEntryTable", dtKhataKhesraForDb);

                tvpParameter.SqlDbType = SqlDbType.Structured;
                tvpParameter.TypeName = "dbo.LandDetailsEntryType_CO";

                cmd.Parameters.Add("@CUUser", SqlDbType.NVarChar).Value = userid;

                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

        public DataTable GetKhataKhesraDetails_CO(long applicationId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
               
                using (SqlCommand cmd = new SqlCommand("select  id, a_id,khataNo,khesraNo, RakbaNo1,Rakba_unit1,RakbaNo2,Rakba_unit2,RakbaNo3,Rakba_unit3,LandTypesInKhatian,LandDetailsInKhatian,North_chauhaddee,South_chauhaddee,East_chauhaddee, West_chauhaddee, Vivadith_District_Code, Vivadith_Block_Code,Vivadith_Mauza,Rakba,Landdesciption,Vivadith_District_Name,Vivadith_Block_Name, Vivadith_Mauza_Name from BS_VW_GetKhataKhesra_Step3 WHERE a_id = @a_id", con))
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