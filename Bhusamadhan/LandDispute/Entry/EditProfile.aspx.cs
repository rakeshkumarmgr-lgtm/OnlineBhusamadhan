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
    public partial class EditProfile : System.Web.UI.Page
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
                    FillUsers(ddlRole.SelectedValue);

                }
                //BindRoles();
                //FillUsers(ddlRole.SelectedValue);
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
            lblMsg.Visible = false;
            lblMsg.Text = "";

            gvUsers.EditIndex = -1;
            gvUsers.PageIndex = 0;
            txtSearchUser.Text = "";

            string userrole = ddlRole.SelectedValue;


            if (string.IsNullOrWhiteSpace(userrole) ||    userrole == "0")
            {
                gvUsers.DataSource = null;
                gvUsers.DataBind();

                return;
            }


            FillUsers(userrole);
        }

        private void FillUsers(string userrole, string searchText = "")
        {
            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Userrole ", userrole));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@SearchText ", searchText ?? ""));

                string query = @" SELECT  UserID,  username, Name, Email, Mobile,Attempt_Count FROM UserLogin WHERE Userrole = @Userrole
                                      AND
                                      (
                                            @SearchText = ''  OR UserID LIKE '%' + @SearchText + '%' OR Name LIKE '%' + @SearchText + '%' OR Email LIKE '%' + @SearchText + '%'  OR Mobile LIKE '%' + @SearchText + '%'
                                      )
                                    ORDER BY Name";

                DataTable dt = objDBHelper.GetResults(query, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    gvUsers.DataSource = dt;
                    gvUsers.DataBind();
                }
                else
                {
                    gvUsers.DataSource = null;
                    gvUsers.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = "<pre>" + ex.ToString() + "</pre>";
            }
        }

        protected void txtSearchUser_TextChanged(object sender, EventArgs e)
        {
            try
            {
                gvUsers.EditIndex = -1;
                gvUsers.PageIndex = 0;

                string userrole = ddlRole.SelectedValue;

                if (string.IsNullOrWhiteSpace(userrole) || userrole == "0")
                {
                    gvUsers.DataSource = null;
                    gvUsers.DataBind();

                    lblMsg.Text = "Please select a role first.";
                    lblMsg.CssClass = "alert alert-warning d-block message";
                    lblMsg.Visible = true;

                    return;
                }

                string searchText = txtSearchUser.Text.Trim();

                FillUsers(userrole, searchText);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();

                lblMsg.CssClass = "alert alert-danger d-block message";

                lblMsg.Visible = true;
            }
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchUser.Text = "";

                gvUsers.EditIndex = -1;
                gvUsers.PageIndex = 0;

                string userrole = ddlRole.SelectedValue;

                if (!string.IsNullOrWhiteSpace(userrole) && userrole != "0")
                {
                    FillUsers(userrole);
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();

                lblMsg.CssClass = "alert alert-danger d-block message";

                lblMsg.Visible = true;
            }
        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvUsers.EditIndex = -1;

                gvUsers.PageIndex = e.NewPageIndex;

                string userrole = ddlRole.SelectedValue;
                string searchText = txtSearchUser.Text.Trim();

                if (!string.IsNullOrWhiteSpace(userrole) && userrole != "0")
                {
                    FillUsers(userrole, searchText);
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();

                lblMsg.CssClass = "alert alert-danger d-block message";

                lblMsg.Visible = true;
            }
        }

        protected void gvUsers_RowEditing( object sender, GridViewEditEventArgs e)
        {
            try
            {
                string userrole = ddlRole.SelectedValue;
                string searchText = txtSearchUser.Text.Trim();

                if (string.IsNullOrWhiteSpace(userrole) || userrole == "0")
                {
                    return;
                }

                gvUsers.EditIndex = e.NewEditIndex;

                FillUsers(userrole, searchText);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
                  
                lblMsg.CssClass ="alert alert-danger d-block message";

                lblMsg.Visible = true;
            }
        }

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvUsers.EditIndex = -1;

                string userrole = ddlRole.SelectedValue;
                string searchText = txtSearchUser.Text.Trim();

                if (!string.IsNullOrWhiteSpace(userrole) && userrole != "0")
                {
                    FillUsers(userrole, searchText);
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                   
            }
        }

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblMsg.Visible = false;
            lblMsg.Text = "";

            try
            {
                string userrole = ddlRole.SelectedValue;

                if (string.IsNullOrWhiteSpace(userrole) || userrole == "0")
                {
                    lblMsg.Text = "Please select a valid role.";
                    lblMsg.CssClass = "alert alert-warning d-block message";
                    lblMsg.Visible = true;

                    return;
                }


                GridViewRow row = gvUsers.Rows[e.RowIndex];


                TextBox txtUserID = row.FindControl("txtGridUserID") as TextBox;

                TextBox txtUsername =row.FindControl("txtGridUsername") as TextBox;

                TextBox txtName =  row.FindControl("txtGridName") as TextBox;

                TextBox txtEmail = row.FindControl("txtGridEmail") as TextBox;

                TextBox txtMobile = row.FindControl("txtGridMobile") as TextBox;


                if (txtUserID == null ||  txtName == null || txtEmail == null || txtMobile == null)
                {
                    lblMsg.Text ="Unable to read selected user information.";

                    lblMsg.CssClass = "alert alert-danger d-block message";

                    lblMsg.Visible = true;

                    return;
                }


                string userId = txtUserID.Text.Trim();

                string username = txtUsername.Text.Trim();

                string name = txtName.Text.Trim();

                string email = txtEmail.Text.Trim();

                string mobile = txtMobile.Text.Trim();


                if (string.IsNullOrWhiteSpace(userId))
                {
                    lblMsg.Text = "Invalid User ID.";
                    lblMsg.CssClass ="alert alert-danger d-block message";
                    lblMsg.Visible = true;

                    return;
                }


                if (string.IsNullOrWhiteSpace(username))
                {
                    lblMsg.Text = "Username cannot be blank.";
                    lblMsg.CssClass ="alert alert-warning d-block message";
                    lblMsg.Visible = true;

                    return;
                }


                if (string.IsNullOrWhiteSpace(name))
                {
                    lblMsg.Text = "Name cannot be blank.";
                    lblMsg.CssClass ="alert alert-warning d-block message";
                    lblMsg.Visible = true;

                    return;
                }


                if (string.IsNullOrWhiteSpace(mobile))
                {
                    lblMsg.Text = "Mobile number cannot be blank.";
                    lblMsg.CssClass = "alert alert-warning d-block message";
                    lblMsg.Visible = true;

                    return;
                }

                bool result = dal.UpdateUserProfile(  userId, userrole,  name, email, mobile);


                if (result)
                {
                    gvUsers.EditIndex = -1;

                    string searchText = txtSearchUser.Text.Trim();

                    FillUsers(userrole, searchText);

                    lblMsg.Text ="User profile updated successfully.";

                    lblMsg.CssClass ="alert alert-success d-block message";

                    lblMsg.Visible = true;
                }
                else
                {
                    lblMsg.Text = "Profile was not updated. Please verify the selected user.";

                    lblMsg.CssClass ="alert alert-warning d-block message";

                    lblMsg.Visible = true;
                }
            }
           
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
                  
                lblMsg.CssClass ="alert alert-danger d-block message";

                lblMsg.Visible = true;

            
            }
        }

       

      

      
    }
}