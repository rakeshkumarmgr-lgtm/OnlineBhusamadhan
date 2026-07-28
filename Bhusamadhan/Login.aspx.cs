using Bhusamadhan.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan
{
    public partial class Login : System.Web.UI.Page
    {
        DBHelper objDBHelper = new DBHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetNoStore();
            //Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            //Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            if (!IsPostBack)
            {

                if (!IsPostBack)
                {
                    LoadCaptcha();

                    if (Session["FailedAttempt"] == null)
                        Session["FailedAttempt"] = 0;
                }
            }

        }

        private void LoadCaptcha()
        {
            imgCaptcha.ImageUrl = "~/Public/CreateCaptcha.aspx?x=" + Guid.NewGuid().ToString();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblErrorMsg.Text = "";

            if (!Page.IsValid)
                return;

            //-------Browser Session Failed Attempt---------------------------------------------
           
            int sessionAttempt = Convert.ToInt32(Session["FailedAttempt"]);

            if (sessionAttempt >= 5)
            {
                lblErrorMsg.Text = "Too many invalid attempts. Please close browser and try again.";
                return;
            }

            //------Captcha Validation----------------------------------------------
          
            if (Session["CaptchaCode"] == null)
            {
                lblErrorMsg.Text = "Captcha expired. Please try again.";

                LoadCaptcha();
                return;
            }

            if (!txtCaptha.Text.Trim().Equals(Session["CaptchaCode"].ToString(),StringComparison.OrdinalIgnoreCase))
            {
                Session["FailedAttempt"] = sessionAttempt + 1;

                lblErrorMsg.Text = "Invalid Captcha.";

                txtCaptha.Text = "";

                Session.Remove("CaptchaCode");

                LoadCaptcha();

                return;
            }

            //-------Get User---------------------------------------------
           
            string userid = txtUserid.Text.Trim();

            string password = txtPassword.Text.Trim();

            DataTable dtUser = GetUserByUsername(userid);

            if (dtUser == null || dtUser.Rows.Count == 0)
            {
                Session["FailedAttempt"] = sessionAttempt + 1;

                lblErrorMsg.Text = "Invalid Username or Password.";

                ClearLogin();

                return;
            }

            //------db Lock Check----------------------------------------------
            
            int dbAttempt = Convert.ToInt32(dtUser.Rows[0]["Attempt_Count"]);

            if (dbAttempt >= 5)
            {
                lblErrorMsg.Text = "Your account has been locked. Please contact Administrator.";

                return;
            }

            //-------Verify Password---------------------------------------------
           
            string dbPassword = dtUser.Rows[0]["Password_BCrypt"].ToString();

            bool passwordMatched = BCrypt.Net.BCrypt.Verify(password, dbPassword);

            if (!passwordMatched)
            {
                dbAttempt = UpdateFailedAttemptCount(userid, dbAttempt);

                Session["FailedAttempt"] = sessionAttempt + 1;

                if (dbAttempt >= 5)
                {
                    lblErrorMsg.Text = "Your account has been locked. Please contact Administrator.";
                }
                else
                {
                    lblErrorMsg.Text = "Invalid Username or Password. Remaining attempts : "+ (5 - dbAttempt);
                }

                ClearLogin();

                return;
            }

            //----Login Success------------------------------------------------
            

            Session["FailedAttempt"] = 0;

            Session.Remove("CaptchaCode");

            ResetFailedAttempt(userid);

            Session["UserLogIn"] = dtUser;

            HttpCookie cookie = new HttpCookie("Name");

            cookie.Values["Name"] = userid;

            cookie.Expires = DateTime.Now.AddMinutes(30);

            Response.Cookies.Add(cookie);

            // Optional Login History
            //LogUserLogin(userid);

            Response.Redirect("~/Default.aspx", false);

            Context.ApplicationInstance.CompleteRequest();
        }

        private void ClearLogin()
        {
            txtPassword.Text = "";

            txtCaptha.Text = "";

            Session.Remove("CaptchaCode");

            LoadCaptcha();

            txtPassword.Focus();
        }


      
        public DataTable GetUserByUsername(string username)
        {
            List<SqlParameter> listSQLP = new List<SqlParameter>();

            listSQLP.Add(new SqlParameter("@UserId", username));

            string sql = @"SELECT t.* FROM BS_VW_CheckCredential t  WHERE t.UserId=@UserId";

            return objDBHelper.GetResults(sql, listSQLP, false);
        }

        private void ResetFailedAttempt(string userid)
        {
            List<SqlParameter> listSQLP = new List<SqlParameter>();

            listSQLP.Add(new SqlParameter("@UserId", userid));

            objDBHelper.SetData( @"UPDATE UserLogin SET Attempt_Count=0,  Last_Login_Attempt=GETDATE() WHERE UserId=@UserId", listSQLP, false);
        }

        private int UpdateFailedAttemptCount(string userid, int dbAttempt)
        {
            dbAttempt++;

            List<SqlParameter> listSQLP = new List<SqlParameter>();

            listSQLP.Add(new SqlParameter("@UserId", userid));

            listSQLP.Add(new SqlParameter("@Attempt", dbAttempt));

            objDBHelper.SetData( @"UPDATE UserLogin SET Attempt_Count=@Attempt, Last_Login_Attempt=GETDATE() WHERE UserId=@UserId", listSQLP, false);

            return dbAttempt;
        }

        protected void btnRefreshCaptcha_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove("CaptchaCode");
            txtCaptha.Text = "";
            LoadCaptcha();
        }
    }
}