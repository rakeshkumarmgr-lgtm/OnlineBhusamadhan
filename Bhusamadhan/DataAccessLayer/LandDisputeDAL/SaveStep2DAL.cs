using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bhusamadhan.DataAccessLayer.LandDisputeDAL
{
    public class SaveStep2DAL
    {
        private readonly string conStr = ConfigurationManager.ConnectionStrings["conns"].ConnectionString;
        public long SaveStep2( long applicationId, string prativadiKoSuchitKiyaGayaHai, string givenInfoType, string givenInfoDesc, string prativadiKoSuchanaKaTaamilaPraaptHai, string prativadiUpasthitHuaHai, DataTable dtPratiVadiForDb, string userid, SqlConnection con, SqlTransaction trans)
        {
            using (SqlCommand cmd = new SqlCommand("BS_SP_SaveStep2", con, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@a_id", applicationId);

                cmd.Parameters.AddWithValue( "@prativadi_ko_suchit_kiya_gaya_hai",  prativadiKoSuchitKiyaGayaHai ?? "");

                cmd.Parameters.AddWithValue( "@given_info_type", givenInfoType ?? "");

                cmd.Parameters.AddWithValue( "@given_info_desc", givenInfoDesc ?? "");

                cmd.Parameters.AddWithValue( "@prativadi_ko_suchana_ka_taamila_praapt_hai", prativadiKoSuchanaKaTaamilaPraaptHai ?? "");

                cmd.Parameters.AddWithValue( "@prativadi_upasthit_hua_hai",  prativadiUpasthitHuaHai ?? "");


                SqlParameter tvpParameter = cmd.Parameters.AddWithValue( "@PrtiVadiDetailEntryTable", dtPratiVadiForDb);

                tvpParameter.SqlDbType = SqlDbType.Structured;
                tvpParameter.TypeName = "dbo.PrtiVadiDetailEntryType";

                cmd.Parameters.Add("@CUUser", SqlDbType.NVarChar).Value = userid;

                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

        public DataTable GetPratiVadiDetails(long applicationId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from BS_VW_GetPratiVadi_Step2 WHERE a_id = @a_id", con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@a_id", SqlDbType.BigInt) .Value = applicationId;

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