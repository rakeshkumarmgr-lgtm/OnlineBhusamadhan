using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bhusamadhan.DB
{
    
    public class MenuHelper
    {
        DBHelper objDBHelper = new DBHelper();
        public DataTable GetMenuByRole(int roleId)
        {
            List<SqlParameter> listSQLP = new List<SqlParameter>();

            listSQLP.Add(new SqlParameter("@RoleID", roleId));

            string sql = @" SELECT T.ParentMenuID, T.MenuName  AS ParentMenuName,  T.NavigateUrl  AS ParentNavigateUrl,T.IconClass  AS ParentIcon,
                        T.DisplayOrder, C.ChildMenuID, C.MenuName  AS ChildMenuName, C.NavigateUrl, C.IconClass AS ChildIcon, C.DisplayOrder AS ChildDisplayOrder

                        FROM BS_UserMenuPermission P INNER JOIN BS_TopMenuMst T ON P.ParentMenuID = T.ParentMenuID

                        LEFT JOIN BS_ChildMenuMst C ON T.ParentMenuID = C.ParentMenuID AND C.IsActive = 1

                        WHERE P.RoleID = @RoleID AND T.IsActive = 1 ORDER BY T.DisplayOrder, C.DisplayOrder;";

            return objDBHelper.GetResults(sql, listSQLP, false);
        }
    }
}