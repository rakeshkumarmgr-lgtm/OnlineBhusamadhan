using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Bhusamadhan.DB
{
    public class DBHelper
    {

        public DBHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static SqlConnection GetConnectionString()
        {
            return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conns"].ToString());
        }

        public DataTable GetResults(string ProcedureORQuery, List<SqlParameter> Parameters, bool isByProcedure)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = GetConnectionString();
            DataTable dtReturn = new DataTable();
            if (isByProcedure)
                cmd.CommandType = CommandType.StoredProcedure;
            else
                cmd.CommandType = CommandType.Text;

            cmd.Connection = con;
            da.SelectCommand = cmd;
            cmd.CommandText = ProcedureORQuery;
            try
            {
                foreach (SqlParameter p in Parameters)
                {
                    cmd.Parameters.Add(p);
                }
                con.Open();
                da.Fill(dtReturn);
                return dtReturn;
            }
            catch (SqlException sqle)
            {
                if (sqle.ErrorCode < 50000)
                    throw new Exception(sqle.Message);
                else
                    throw new Exception("there is some error,  please try agin");
            }
            catch (Exception ex)

            { throw new Exception(ex.Message); }

            finally
            {
                con.Close();
                da.Dispose();
                cmd.Dispose();
                con.Dispose();

            }
        }

        public bool SetData(string ProcedureORQuery, List<SqlParameter> Parameters, bool isByProcedure)
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = GetConnectionString();

            if (isByProcedure)
                cmd.CommandType = CommandType.StoredProcedure;
            else
                cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

            cmd.CommandText = ProcedureORQuery;
            try
            {
                foreach (SqlParameter p in Parameters)
                {
                    cmd.Parameters.Add(p);
                }
                con.Open();
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (SqlException sqle)
            {
                if (sqle.ErrorCode < 50000)
                    throw new Exception(sqle.Message);
                else
                    throw new Exception("there is some error,  please try agin");
            }
            catch (Exception ex)

            { throw new Exception(ex.Message); }

            finally
            {
                con.Close();

                cmd.Dispose();
                con.Dispose();

            }
        }

        public object GetScalerResults(string ProcedureORQuery, List<SqlParameter> Parameters, bool isByProcedure)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = GetConnectionString();

            if (isByProcedure)
                cmd.CommandType = CommandType.StoredProcedure;
            else
                cmd.CommandType = CommandType.Text;

            cmd.Connection = con;
            cmd.CommandText = ProcedureORQuery;
            try
            {
                foreach (SqlParameter p in Parameters)
                {
                    cmd.Parameters.Add(p);
                }
                con.Open();
                var value = cmd.ExecuteScalar();
                return value;
            }
            catch (SqlException sqle)
            {
                if (sqle.ErrorCode < 50000)
                    throw new Exception(sqle.Message);
                else
                    throw new Exception("there is some error,  please try agin");
            }
            catch (Exception ex)

            { throw new Exception(ex.Message); }

            finally
            {
                con.Close();
                cmd.Dispose();
                con.Dispose();

            }
        }
    }


}