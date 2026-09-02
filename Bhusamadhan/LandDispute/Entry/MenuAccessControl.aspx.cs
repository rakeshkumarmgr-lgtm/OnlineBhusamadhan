using Bhusamadhan.DataAccessLayer.LandDisputeDAL;
using Bhusamadhan.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.LandDispute.Entry
{
    public partial class MenuAccessControl : System.Web.UI.Page
    {
        string userid = "";
        string userrole = "";
        int roleid;
        DBHelper objDBHelper = new DBHelper();
        private readonly MenuPermissionDAL dal = new MenuPermissionDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = Session["UserLogIn"] as DataTable;

                if (dt == null || dt.Rows.Count != 1)
                {
                    Session.Clear();
                    Session.Abandon();

                    Response.Redirect("~/Login.aspx");
                    return;
                }

                roleid = Convert.ToInt32(dt.Rows[0]["RoleID"]);
                userrole = Convert.ToString(dt.Rows[0]["Userrole"]);
                userid = Convert.ToString(dt.Rows[0]["UserID"]);

                if (userid != "NIC_ADMIN" || !string.Equals(userrole, "NICADMIN", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                if (!IsPostBack)
                {
                    BindRoles();
                    BindParentMenus();

                    ddlChildMenu.Items.Clear();

                    ddlChildMenu.Items.Insert(0, new ListItem("-- Select Parent Menu First --", "0"));

                    gvMenuPermission.DataSource = null;
                    gvMenuPermission.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                Session.Clear();
                Session.Abandon();

                Response.Redirect("~/Login.aspx");
            }
        }

        private void BindRoles()
        {
            try
            {
                DataTable dt = dal.GetRoles();

                ddlRole.DataSource = dt;
                ddlRole.DataTextField = "RoleDisplay";
                ddlRole.DataValueField = "ID";
                ddlRole.DataBind();

                ddlRole.Items.Insert( 0, new ListItem("-- Select Role --", "0"));
            }
            catch (Exception ex)
            {
                ddlRole.Items.Clear();

                ddlRole.Items.Insert( 0, new ListItem("-- Unable to load roles --", "0"));

                lblMsg.Text = "Roles could not be loaded due to a technical problem.";
              
            }
        }

        private void BindParentMenus()
        {
            try
            {
                DataTable dt = dal.GetParentMenus();

                ddlParentMenu.DataSource = dt;
                ddlParentMenu.DataTextField = "MenuName";
                ddlParentMenu.DataValueField = "ParentMenuID";

                ddlParentMenu.DataBind();

                ddlParentMenu.Items.Insert(0, new ListItem("-- Select Parent Menu --", "0"));
            }

            catch (Exception ex)
            {
                ddlParentMenu.Items.Clear();

                ddlParentMenu.Items.Insert(0, new ListItem("-- Unable to load roles --", "0"));

                lblMsg.Text = "Roles could not be loaded due to a technical problem.";

            }
        }

        protected void ddlParentMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChildMenu.Items.Clear();

            int parentMenuId;

            if (!int.TryParse(ddlParentMenu.SelectedValue, out parentMenuId) || parentMenuId <= 0)
            {
                ddlChildMenu.Items.Insert(0, new ListItem("-- Select Parent Menu First --", "0"));

                return;
            }


            DataTable dt = dal.GetChildMenus(parentMenuId);


            ddlChildMenu.Items.Add(new ListItem("-- Select Child Menu --", "0"));


            foreach (DataRow row in dt.Rows)
            {
                string childMenuId = row["ChildMenuID"].ToString();

                string menuName = row["MenuName"].ToString();

                string url = row["NavigateUrl"].ToString();

                string fileName = "";

                if (!string.IsNullOrWhiteSpace(url))
                {
                    try
                    {
                        fileName =  System.IO.Path.GetFileName(url);
                    }
                    catch
                    {
                        fileName = url;
                    }
                }

                string displayText;

                if (!string.IsNullOrEmpty(fileName))
                {
                    displayText = "[" + childMenuId + "] " + menuName + " | " + fileName;
                }
                else
                {
                    displayText = "[" + childMenuId + "] " + menuName;
                }

                ddlChildMenu.Items.Add( new ListItem(  displayText, childMenuId));
            }
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            int roleId;

            if (!int.TryParse(ddlRole.SelectedValue, out roleId) || roleId <= 0)
            {
                gvMenuPermission.DataSource = null;
                gvMenuPermission.DataBind();

                return;
            }

            BindMenuPermissions(roleId);
        }

        private void BindMenuPermissions(int roleId)
        {
            DataTable dt = dal.GetMenuPermissions(roleId);

            gvMenuPermission.DataSource = dt;
            gvMenuPermission.DataBind();
        }

        

        //-----------------Add Menu--------------------------------------------
        protected void btnGrantAccess_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

          
                int roleId;
            int parentMenuId;

            int? childMenuId = null;

            if (!int.TryParse(ddlRole.SelectedValue, out roleId) || roleId <= 0)
            {
                lblMsg.Text = "Please select a role.";

                return;
            }



            if (!int.TryParse(ddlParentMenu.SelectedValue, out parentMenuId) || parentMenuId <= 0)
            {
                lblMsg.Text = "Please select a parent menu.";

                return;
            }


            int selectedChildMenuId;

            if (!int.TryParse(ddlChildMenu.SelectedValue, out selectedChildMenuId))
            {
                lblMsg.Text = "Please select a child menu/page.";

                return;
            }


            if (selectedChildMenuId > 0)
            {
                childMenuId = selectedChildMenuId;
            }


            try
            {

                bool exists = dal.PermissionExists(roleId, parentMenuId, childMenuId);


                if (exists)
                {
                    lblMsg.Text = "This menu/page is already assigned to the selected role. Please use the Grant/Revoke button in the Existing Menu Permissions section.";

                    return;
                }


                bool result = dal.InsertPermission(roleId, parentMenuId, childMenuId);


                if (result)
                {
                    lblMsg.Text = "Menu access granted successfully.";


                    BindMenuPermissions(roleId);


                    // Reset selection
                    ddlParentMenu.SelectedIndex = 0;

                    ddlChildMenu.Items.Clear();

                    ddlChildMenu.Items.Insert(0, new ListItem("-- Select Child Menu First --", "0"));
                }
                else
                {
                    lblMsg.Text = "Menu permission could not be added.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;

            }
        }

        //--------------Menu update------------------------

        protected void gvMenuPermission_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "TogglePermission")
            {
                return;
            }


            int roleId;

            if (!int.TryParse( ddlRole.SelectedValue,  out roleId) || roleId <= 0)
            {
                lblMsg.Text ="Please select a role.";
                return;
            }


            int rowIndex;

            if (!int.TryParse( e.CommandArgument.ToString(), out rowIndex))
            {
                return;
            }


            if (rowIndex < 0 || rowIndex >= gvMenuPermission.Rows.Count)
            {
                return;
            }


            GridViewRow row = gvMenuPermission.Rows[rowIndex];


            int parentMenuId = Convert.ToInt32( gvMenuPermission .DataKeys[rowIndex].Values["ParentMenuID"]);

            object childValue = gvMenuPermission .DataKeys[rowIndex].Values["ChildMenuID"];


            int? childMenuId = null;


            if (childValue != null && childValue != DBNull.Value && !string.IsNullOrEmpty( childValue.ToString()))
            {
                childMenuId = Convert.ToInt32(childValue);
            }

            Label lblStatus = row.FindControl( "lblAccessStatus") as Label;


            if (lblStatus == null)
            {
                return;
            }


            string currentStatus =lblStatus.Text;

            bool newAccess = currentStatus != "Granted";


            try
            {


                bool result = dal.UpdatePermission( roleId, parentMenuId, childMenuId, newAccess);


                if (result)
                {
                    if (newAccess)
                    {
                        lblMsg.Text ="Menu access granted successfully.";
                    }
                    else
                    {
                        lblMsg.Text ="Menu access revoked successfully.";
                    }


                    BindMenuPermissions(roleId);
                }
                else
                {
                    lblMsg.Text ="Permission could not be updated.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;

            }
        }

        protected string GetStatusCss(string status)
        {
            switch (status)
            {
                case "Granted":
                    return "badge badge-success p-2";

                case "Revoked":
                    return "badge badge-danger p-2";

                default:
                    return "badge badge-secondary p-2";
            }
        }

        protected string GetButtonCss(string status)
        {
            switch (status)
            {
                case "Granted":
                    return "btn btn-sm btn-danger";

                case "Revoked":
                    return "btn btn-sm btn-success";

                default:
                    return "btn btn-sm btn-primary";
            }
        }

        protected string GetButtonText(string status)
        {
            switch (status)
            {
                case "Granted":
                    return "Revoke";

                case "Revoked":
                    return "Grant";

                default:
                    return "Grant";
            }
        }

        protected string GetButtonIcon(string status)
        {
            switch (status)
            {
                case "Granted":
                    return "fas fa-ban mr-1";

                default:
                    return "fas fa-check mr-1";
            }
        }
        protected string GetConfirmMessage( string status)
        {
            if (status == "Granted")
            {
                return "return confirm('Are you sure you want to revoke access to this menu/page?');";
            }

            return "return confirm('Are you sure you want to grant access to this menu/page?');";
        }
    }
}