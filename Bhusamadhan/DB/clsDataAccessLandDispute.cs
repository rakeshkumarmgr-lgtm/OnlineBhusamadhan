using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace Bhusamadhan.DB
{
    public class clsDataAccessLandDispute
    {
        SqlConnection con = new SqlConnection();
        SqlTransaction Trans;

        public clsDataAccessLandDispute()
        {
            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conns"].ConnectionString;
        }

        public DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                //con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                SqlDataAdapter adap1 = new SqlDataAdapter();
                cmd.Connection = con;
                adap1.SelectCommand = cmd;
                adap1.Fill(dt);
                adap1.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                //ExceptionLogging.SendErrorToText(ex);
                //HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");
                con.Close();
                return dt;
            }

            finally
            {
                con.Close();
            }

        }

        public DataSet GetDataset(string query, List<SqlParameter> param)
        {

            DataSet ds = new DataSet();
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                if (param != null)
                {
                    foreach (SqlParameter prm in param)
                    {
                        cmd.Parameters.Add(prm);
                    }
                }
                SqlDataAdapter adap1 = new SqlDataAdapter();
                cmd.Connection = con;
                adap1.SelectCommand = cmd;
                adap1.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {

                return ds;
            }

            finally
            {
                con.Close();
            }

        }


        public string GetFinanceYear(string date)
        {
            DateTime dt = Convert.ToDateTime(DateTime.Now);
            int y = Convert.ToInt32(dt.Year);
            string m = Convert.ToString(dt.Month);
            string d = Convert.ToString(dt.Day);
            int check = Convert.ToInt32(m);
            if (check > 3)
            {
                date = y + "-" + Convert.ToString(y + 1);
            }
            else
            {
                date = Convert.ToString(y - 1) + "-" + y;
            }
            return date;
        }
        public string GetFinanceYearShort()
        {
            string date = "";
            DateTime dt = Convert.ToDateTime(DateTime.Now);
            int y = Convert.ToInt32(dt.Year);
            string m = Convert.ToString(dt.Month);
            string d = Convert.ToString(dt.Day);
            int check = Convert.ToInt32(m);
            if (check > 3)
            {
                date = y + "-" + Convert.ToString(y + 1);
            }
            else
            {
                date = Convert.ToString(y - 1) + "-" + y;
            }
            string rtn1 = date.Substring(2, 2);
            string rtn2 = date.Substring(7, 2);
            return date = rtn1 + rtn2;
        }
        public DataTable GetDataTable(string query, SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                foreach (SqlParameter prm in param)
                {
                    cmd.Parameters.Add(prm);
                }
                SqlDataAdapter adap1 = new SqlDataAdapter();
                cmd.Connection = con;
                adap1.SelectCommand = cmd;
                adap1.Fill(dt);
                adap1.Dispose();
                return dt;

            }
            catch (Exception ex)
            {
                //ExceptionLogging.SendErrorToText(ex);
                //HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");
                con.Close();
                return dt;
            }

            finally
            {
                con.Close();
            }

        }

        //public DataTable GetDataTableWithProc(string ProcName, SqlParameter[] param)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = ProcName;
        //        foreach (SqlParameter prm in param)
        //        {
        //            cmd.Parameters.Add(prm);
        //        }
        //        SqlDataAdapter adap1 = new SqlDataAdapter();
        //        cmd.Connection = con;
        //        cmd.CommandTimeout = 0;
        //        adap1.SelectCommand = cmd;
        //        adap1.Fill(dt);
        //        adap1.Dispose();
        //        return dt;

        //    }
        //    catch (Exception ex)
        //    {
        //        // ExceptionLogging.SendErrorToText(ex);
        //        // HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");
        //        return dt;
        //    }



        //}

        public DataTable GetDataTableWithProc(string procName, SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlCommand cmd = new SqlCommand(procName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    if (parameters != null)
                    {
                        foreach (SqlParameter prm in parameters)
                        {
                            cmd.Parameters.Add(prm);
                        }
                    }

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ApplicationException( "Database error occurred while executing stored procedure : " + procName, ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException( "Unexpected error occurred while executing stored procedure : " + procName, ex);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return dt;
        }


        public int ExecuteSql(string Query, SqlParameter[] param)
        {
            int r = 0;
            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = Query;
                foreach (SqlParameter prm in param)
                {
                    cmd.Parameters.Add(prm);
                }
                cmd.Connection = con;
                r = cmd.ExecuteNonQuery();
                return r;
            }
            catch (Exception ex)
            {
                // ExceptionLogging.SendErrorToText(ex);
                // HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");
                con.Close();
                return r;

            }

            finally
            {
                con.Close();
            }
        }

        public int ExecuteSql(string Query, SqlParameter[] param, Label lblMsg)
        {

            try
            {


                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = con;
                SqlCmd.Transaction = Trans;

                SqlCmd.CommandType = CommandType.Text;
                SqlCmd.CommandText = Query;
                SqlCmd.Parameters.Clear();
                foreach (SqlParameter prm in param)
                {
                    SqlCmd.Parameters.Add(prm);
                }
                SqlCmd.Connection = con;
                return SqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
               
                con.Close();
                return 0;
            }
        }

        public string ExecuteScalar(string strSql)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                //cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSql;
                cmd.Connection = con;
                cmd.Connection.Open();
                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

                //ExceptionLogging.SendErrorToText(ex);
                //HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");
                con.Close();
                return "";
            }
            finally
            {

                con.Close();
            }
        }


        public byte[] ExecuteScalar(string strSql, SqlParameter[] param)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                con.Open();



                cmd.CommandText = strSql;
                foreach (SqlParameter prm in param)
                {
                    cmd.Parameters.Add(prm);
                }
                cmd.Connection = con;
                //cmd.Connection.Open();
                return (byte[])cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //ExceptionLogging.SendErrorToText(ex);
                // HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");
                con.Close();
                return null;
            }
            finally
            {
                //cmd.Connection.Close();
                con.Close();
            }
        }

        public int ExecuteSql(string Query)
        {


            try
            {
                int x = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand();

                string strCommand = Query;
                cmd.CommandText = strCommand;
                cmd.Connection = con;
                x = cmd.ExecuteNonQuery();
                return x;
            }
            catch (Exception ex)
            {
                return 0;

            }

            finally
            {
                con.Close();
            }

        }

        public void OpenConnection()
        {
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
        }
        public void CloseConnection()
        {
            con.Close();
        }

        public void BeginTransaction()
        {
            SqlCommand cmd = new SqlCommand();
            Trans = con.BeginTransaction(IsolationLevel.Serializable);
            cmd.Transaction = Trans;

        }
        public void BeginTransaction(IsolationLevel level)
        {
            SqlCommand cmd = new SqlCommand();
            Trans = con.BeginTransaction(level);
            cmd.Transaction = Trans;

        }

        public void Commit()
        {
            Trans.Commit();

        }
        public void Rollback()
        {
            Trans.Rollback();

        }

        public int ExecuteSqlTrans(string SqlStr)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {

                //cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlStr;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // ExceptionLogging.SendErrorToText(ex);
                // HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");
                con.Close();
                return 0;
            }
            finally
            {
                //cmd.Connection.Close();
                con.Close();
            }
        }

        public string getPath()
        {
            string Eurl = "http://fts.bih.nic.in";
            return Eurl;
        }

        public string GetIpValue()
        {
            string ipAdd = "Not Available";
            ipAdd = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAdd))
            {
                ipAdd = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipAdd;
        }
    }
}