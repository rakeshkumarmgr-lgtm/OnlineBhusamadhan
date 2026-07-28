using Bhusamadhan.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.Public
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        int Uid;
        string UserID = "";
        DBHelper objDBHelper = new DBHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillCapctha();
            }
        }

        private void FillCapctha()
        {
            try
            {

                Random random = new Random();

                string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                StringBuilder captcha = new StringBuilder();

                for (int i = 0; i < 5; i++)

                    captcha.Append(combination[random.Next(combination.Length)]);

                Session["captcha"] = captcha.ToString();

                lblCaptchaImage.Text = captcha.ToString();

                //imgCaptcha.ImageUrl = "~/Public/ForgotPassword.aspx?New=1";
                //return Session["captcha"] as String;

            }

            catch
            {

                throw;

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lblCaptchaImage.Text == txtCaptchaImage.Text)
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@UserId", txtUserID.Text.Trim()));
                DataTable dtResults = objDBHelper.GetResults("select UserID,Mobile from UserLogin where UserID=@UserId", listSQLP, false);
                if (dtResults.Rows.Count > 0)
                {

                    UserID = dtResults.Rows[0]["UserID"].ToString();
                    string ContactNo = dtResults.Rows[0]["Mobile"].ToString();


                    if (UserID == txtUserID.Text.Trim() && ContactNo == txtContactNo.Text.Trim())
                    {
                        hdUserID.Value = dtResults.Rows[0]["UserID"].ToString();

                        pnlResetPwd.Visible = true;
                        pnlForgotPassword.Visible = false;
                        //ForgotPass(Uid, UserID);
                    }
                    else
                    {
                        lblErrMsg.Text = "User id / Contact No do not match";
                       
                        hdUserID.Value = "";
                    }



                }
                else
                {
                    lblErrMsg.Text = "User not exists";
                    hdUserID.Value = "";
                }

            }
            else
            {
                //lblErrMsg.Text = "You have Entered InValid Captcha Characters please Enter again";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please enter correct captcha !');", true);
                txtCaptchaImage.Text = "";
                FillCapctha();
            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx", false);
        }

        protected void btnChangePwd_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPwd.Text.Trim();

            string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

            string confirmPassword = txtConfirmPwd.Text.Trim();

            string hashedConfirmPassword = BCrypt.Net.BCrypt.HashPassword(confirmPassword);
            if (Page.IsValid)
            {
                try
                {

                    if (txtNewPwd.Text == "" || txtNewPwd.Text != txtConfirmPwd.Text)
                    {
                        throw new Exception("Confirm password not matched with new password");
                    }
                    else
                    {
                        List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                       
                        listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Uid", hdUserID.Value));
                        listSQLP.Add(new System.Data.SqlClient.SqlParameter("@NewPassword", hashedNewPassword));

                        objDBHelper.SetData("BS_SP_UpdatePassword", listSQLP, true);
                        lblChangePwdMsg.Text = "Password has been changed successfully";

                    }

                }
                catch (Exception ex)
                {
                    lblChangePwdMsg.Text = "Oops!! following error occured : " + ex.Message.ToString();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                }
            }
        }
    }
}