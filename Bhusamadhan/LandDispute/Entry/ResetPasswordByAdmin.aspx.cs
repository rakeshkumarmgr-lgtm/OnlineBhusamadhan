using Bhusamadhan.DataAccessLayer.LandDisputeDAL;
using Bhusamadhan.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.LandDispute.Entry
{
    public partial class ResetPasswordByAdmin : System.Web.UI.Page
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
                ddlRole.DataValueField = "Role";
                ddlRole.DataBind();

                ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "0"));
            }
            catch (Exception ex)
            {
                ddlRole.Items.Clear();

                ddlRole.Items.Insert(0, new ListItem("-- Unable to load roles --", "0"));

                lblMsg.Text = "Roles could not be loaded due to a technical problem.";

            }
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvUsers.PageIndex = 0; 
            BindUsersGrid();
        }

        private void BindUsersGrid()
        {
            string selectedRole = ddlRole.SelectedValue;
            string searchUserId = txtSearchUserId.Text.Trim();
          
            if (!string.IsNullOrEmpty(selectedRole) || !string.IsNullOrEmpty(searchUserId))
            {
                DataTable dt = dal.GetUsersByRole(selectedRole, searchUserId);
                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
            else
            {
                gvUsers.DataSource = null;
                gvUsers.DataBind();
            }
        }




       
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string userId = btn.CommandArgument;
            TextBox txtNewPassword = (TextBox)row.FindControl("txtNewPassword");
            string newPassword = txtNewPassword.Text.Trim();
            if (string.IsNullOrEmpty(newPassword))
            {
                lblMsg.Text = "Please enter a new password for User ID: " + userId;
                lblMsg.ForeColor = Color.Red;
                return;
            }
            try
            {
               
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                int rowsAffected = dal.UpdatePassword(userId, hashedPassword);
                if (rowsAffected > 0)
                {
                    lblMsg.Text = "Password successfully updated for User ID: " + userId;
                    lblMsg.ForeColor = Color.Green;

               
                    BindUsersGrid();
                }
                else
                {
                    lblMsg.Text = "Failed to update password. User ID not found.";
                    lblMsg.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
                lblMsg.ForeColor = Color.Red;
            }
        }


        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            BindUsersGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvUsers.PageIndex = 0;
            BindUsersGrid();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchUserId.Text = string.Empty;
            ddlRole.SelectedIndex = 0;
            lblMsg.Text = string.Empty;
            gvUsers.PageIndex = 0;
            BindUsersGrid();
        }
    }
}