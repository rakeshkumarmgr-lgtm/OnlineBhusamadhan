using Bhusamadhan.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Bhusamadhan.DataAccessLayer.DashboardDAL
{
    public class DashboardDataDAL
    {
        private readonly DBHelper _dbHelper;

        public DashboardDataDAL()
        {
            _dbHelper = new DBHelper();
        }

        public DataTable GetDashboardData( string queryType,  int rangeCode, int divisionCode,  int districtCode, int subDivisionCode, int blockCode,  int thanaCode)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@QuertType", SqlDbType.VarChar, 20)
                {
                    Value = queryType
                },

                new SqlParameter("@RangeCode", SqlDbType.Int)
                {
                    Value = rangeCode
                },

                new SqlParameter("@DivisionCode", SqlDbType.Int)
                {
                    Value = divisionCode
                },

                new SqlParameter("@DISTRICTCODE", SqlDbType.Int)
                {
                    Value = districtCode
                },

                new SqlParameter("@SubDivisionCode", SqlDbType.Int)
                {
                    Value = subDivisionCode
                },

                new SqlParameter("@BlockCode", SqlDbType.Int)
                {
                    Value = blockCode
                },

                new SqlParameter("@ThanaCode", SqlDbType.Int)
                {
                    Value = thanaCode
                }
            };

            return _dbHelper.GetResults("BS_sp_GetDasboardData", parameters, true);
        }
    }
}