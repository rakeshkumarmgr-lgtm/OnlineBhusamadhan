using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Bhusamadhan.DB;

namespace Bhusamadhan.Public
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        string username = "";
        DBHelper objDBHelper = new DBHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["UserLogIn"];

         
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    int roleid = Convert.ToInt32(dt.Rows[0]["RoleID"].ToString());
                    username = dt.Rows[0]["UserID"].ToString();
                    txtUserID.Text = username;

                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }
            }

            else
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
        }

        protected void btnChangePwd_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text.Trim();

            string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);


            string confirmPassword = txtConfirmPassowrd.Text.Trim();

            string hashedConfirmPassword = BCrypt.Net.BCrypt.HashPassword(confirmPassword);

            string currentPassword = txtOldPassword.Text.Trim();


            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                lblErrorMsg.Text = "Password fields cannot be empty";
                return;
            }

            if (newPassword != confirmPassword)
            {
                lblErrorMsg.Text = "Confirm password does not match new password";
                return;
            }

            List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
            listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Uid", txtUserID.Text.Trim()));
            DataTable dtResult = objDBHelper.GetResults("select Password_BCrypt from UserLogin where UserID=@Uid ", listSQLP, false);

            if (dtResult.Rows.Count > 0)
            {
                string storedHash = dtResult.Rows[0]["Password_BCrypt"].ToString();

                bool b = false;
                if (!BCrypt.Net.BCrypt.Verify(currentPassword, storedHash))
                {
                    lblErrorMsg.Text = "Old password is incorrect";
                    return;
                }

                try
                {
                    List<SqlParameter> listSQLP1 = new List<SqlParameter>();
                    listSQLP1.Add(new SqlParameter("@Uid", txtUserID.Text.Trim()));
                    listSQLP1.Add(new SqlParameter("@NewPassword", hashedNewPassword));


                    b = objDBHelper.SetData("BS_SP_UpdatePassword", listSQLP1, true);

                    lblErrorMsg.Text = "Password has been changed successfully";
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message",
                        "alert('Oops!! following error occurred : " + ex.Message.ToString() + "');", true);
                }
            }
            else
            {
                lblErrorMsg.Text = "User not found";
            }
        }
    }
}