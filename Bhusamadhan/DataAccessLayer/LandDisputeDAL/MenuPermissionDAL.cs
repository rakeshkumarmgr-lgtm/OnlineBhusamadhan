using Bhusamadhan.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bhusamadhan.DataAccessLayer.LandDisputeDAL
{
    public class MenuPermissionDAL
    {
        string conStr = DBConHelper.GetConnectionString();

        public DataTable GetRoles()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con =new SqlConnection(conStr))
                {
                    //string sql = @"SELECT ID, Role,  RoleDesc, Role + ' - ' + ISNULL(RoleDesc, '') AS RoleDisplay FROM mst_Role where role!='NICADMIN' ORDER BY Role";
                    string sql = @"SELECT ID, Role,  RoleDesc, Role + ' - ' + ISNULL(RoleDesc, '') AS RoleDisplay FROM mst_Role  ORDER BY Role";


                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                return dt;
            }
          
            catch (Exception ex)
            {
              
                throw;
            }
        }

    
        //--- Get Menu Permission for Selected Role
      
        public DataTable GetMenuPermissions(int roleId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    string sql = @"SELECT
                CASE WHEN UP.ChildMenuID IS NULL THEN 'Parent' ELSE 'Child' END AS MenuType,

                UP.ParentMenuID, UP.ChildMenuID, P.MenuName AS ParentMenuName,

                CASE WHEN UP.ChildMenuID IS NULL THEN P.MenuName ELSE C.MenuName END AS ChildMenuName,

                CASE WHEN UP.ChildMenuID IS NULL THEN P.NavigateUrl ELSE C.NavigateUrl END AS NavigateUrl,

                CASE WHEN UP.HasAccess = 1 THEN 'Granted' ELSE 'Revoked' END AS AccessStatus,UP.SL_No

            FROM BS_UserMenuPermission UP

            INNER JOIN BS_TopMenuMst P ON P.ParentMenuID = UP.ParentMenuID

            LEFT JOIN BS_ChildMenuMst C ON C.ChildMenuID = UP.ChildMenuID AND C.ParentMenuID = UP.ParentMenuID

            WHERE UP.RoleID = @RoleID

            ORDER BY UP.ParentMenuID, CASE WHEN UP.ChildMenuID IS NULL THEN 0 ELSE 1 END, UP.ChildMenuID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleId;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                return dt;
            }

            catch (Exception ex)
            {

                throw;
            }
        }
        public bool InsertPermission( int roleId, int parentMenuId, int? childMenuId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    string sql = @" INSERT INTO BS_UserMenuPermission(RoleID, ParentMenuID, ChildMenuID, HasAccess)
                                 VALUES ( @RoleID,@ParentMenuID,@ChildMenuID, 1)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleId;

                        cmd.Parameters.Add("@ParentMenuID", SqlDbType.Int).Value = parentMenuId;

                        cmd.Parameters.Add("@ChildMenuID", SqlDbType.Int).Value = childMenuId.HasValue ? (object)childMenuId.Value : DBNull.Value;

                        con.Open();

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool UpdatePermission( int roleId, int parentMenuId, int? childMenuId, bool hasAccess)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(conStr))
                {
                    string sql = @" UPDATE BS_UserMenuPermission SET HasAccess = @HasAccess WHERE RoleID = @RoleID AND ParentMenuID = @ParentMenuID
                                  AND
                                  (
                                      ChildMenuID = @ChildMenuID OR (ChildMenuID IS NULL AND @ChildMenuID IS NULL)
                                  )";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleId;

                        cmd.Parameters.Add("@ParentMenuID", SqlDbType.Int).Value = parentMenuId;

                        cmd.Parameters.Add("@ChildMenuID", SqlDbType.Int).Value = childMenuId.HasValue ? (object)childMenuId.Value : DBNull.Value;

                        cmd.Parameters.Add("@HasAccess", SqlDbType.Bit).Value = hasAccess;

                        con.Open();

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }

            catch (Exception ex)
            {

                throw;
            }
        }

        public bool PermissionExists( int roleId, int parentMenuId, int? childMenuId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = @" SELECT COUNT(1)  FROM BS_UserMenuPermission WHERE RoleID = @RoleID AND ParentMenuID = @ParentMenuID
                                  AND
                                  (
                                      ChildMenuID = @ChildMenuID OR (ChildMenuID IS NULL AND @ChildMenuID IS NULL)
                                  )";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleId;

                    cmd.Parameters.Add("@ParentMenuID", SqlDbType.Int) .Value = parentMenuId;

                    cmd.Parameters.Add("@ChildMenuID", SqlDbType.Int).Value = childMenuId.HasValue ? (object)childMenuId.Value: DBNull.Value;

                    con.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            }

            catch (Exception ex)
            {

                throw;
            }
        }

        public DataTable GetParentMenus()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    string sql = @" SELECT ParentMenuID,MenuName FROM BS_TopMenuMst WHERE IsActive = 1 ORDER BY DisplayOrder, MenuName";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                return dt;
            }

            catch (Exception ex)
            {

                throw;
            }
        }

        public DataTable GetChildMenus(int parentMenuId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    string sql = @" SELECT ChildMenuID, MenuName, NavigateUrl, DisplayOrder FROM BS_ChildMenuMst WHERE ParentMenuID = @ParentMenuID AND IsActive = 1  ORDER BY  DisplayOrder, ChildMenuID";

                    using (SqlCommand cmd =
                        new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@ParentMenuID", SqlDbType.Int).Value = parentMenuId;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool UpdateUserProfile( string userId, string userrole, string name, string email, string mobile)
        {
            try
            {
                string query = @"  UPDATE UserLogin SET  Name = @Name, Email = @Email, Mobile = @Mobile,Attempt_Count=0 WHERE UserID = @UserID AND Userrole = @Userrole";


                using (SqlConnection con = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd =  new SqlCommand(query, con))
                    {
                        //cmd.Parameters.Add( "@username",  SqlDbType.NVarChar, 100).Value = username;

                        cmd.Parameters.Add( "@Name", SqlDbType.NVarChar, 150).Value = name;

                        cmd.Parameters.Add( "@Email", SqlDbType.NVarChar, 150).Value = string.IsNullOrWhiteSpace(email)  ? (object)DBNull.Value : email;

                        cmd.Parameters.Add("@Mobile", SqlDbType.VarChar, 15).Value = mobile;

                        cmd.Parameters.Add(  "@UserID", SqlDbType.VarChar, 50).Value =  userId;

                        cmd.Parameters.Add( "@Userrole", SqlDbType.VarChar, 50).Value = userrole;


                        con.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}