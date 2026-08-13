using Bhusamadhan.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.UC
{
    public partial class Menu : System.Web.UI.UserControl
    {
        MenuHelper objDB = new MenuHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BuildMenu();
        }

        private void BuildMenu()
        {
            if (Session["UserLogIn"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            DataTable dtUser = (DataTable)Session["UserLogIn"];

            int roleId = Convert.ToInt32(dtUser.Rows[0]["RoleID"]);

            DataTable dt = objDB.GetMenuByRole(roleId);

            StringBuilder sb = new StringBuilder();

            // Get Parent Menu
            DataView dv = new DataView(dt);
            dv.Sort = "DisplayOrder ASC";

            DataTable dtParent = dv.ToTable(true, "ParentMenuID", "ParentMenuName", "ParentIcon", "ParentNavigateUrl", "DisplayOrder");

            foreach (DataRow parent in dtParent.Rows)
            {
                int parentId = Convert.ToInt32(parent["ParentMenuID"]);

                DataRow[] childRows = dt.Select( "ParentMenuID=" + parentId, "ChildDisplayOrder ASC");

                bool hasChild = childRows.Any(r => r["ChildMenuID"] != DBNull.Value);

                //-----------------------------------------------------
                // Parent Menu having Child Menus
                //-----------------------------------------------------
                if (hasChild)
                {
                    sb.AppendLine("<li class='nav-item has-treeview'>");

                    sb.AppendLine("<a href='#' class='nav-link'>");

                    sb.AppendLine("<i class='nav-icon " + parent["ParentIcon"] + "'></i>");

                    sb.AppendLine("<p>");
                    sb.AppendLine(Server.HtmlEncode(parent["ParentMenuName"].ToString()));
                    sb.AppendLine("<i class='right fas fa-angle-left'></i>");
                    sb.AppendLine("</p>");

                    sb.AppendLine("</a>");

                    sb.AppendLine("<ul class='nav nav-treeview'>");

                    foreach (DataRow child in childRows)
                    {
                        if (child["ChildMenuID"] == DBNull.Value)
                            continue;

                        string childUrl = "#";

                        if (child["NavigateUrl"] != DBNull.Value)
                        {
                            string dbUrl = child["NavigateUrl"].ToString().Trim();

                            if (!string.IsNullOrEmpty(dbUrl) && dbUrl != "#")
                                childUrl = ResolveUrl(dbUrl);
                        }

                        sb.AppendLine("<li class='nav-item'>");

                        sb.AppendLine("<a href='" + childUrl + "' class='nav-link'>");

                        sb.AppendLine("<i class='nav-icon " + child["ChildIcon"] + "'></i>");

                        sb.AppendLine("<p>");
                        sb.AppendLine(Server.HtmlEncode(child["ChildMenuName"].ToString()));
                        sb.AppendLine("</p>");

                        sb.AppendLine("</a>");

                        sb.AppendLine("</li>");
                    }

                    sb.AppendLine("</ul>");

                    sb.AppendLine("</li>");
                }

                //-----------------------------------------------------
                // Parent Menu without Child
                //-----------------------------------------------------
                else
                {
                    string parentUrl = "#";

                    if (parent["ParentNavigateUrl"] != DBNull.Value)
                    {
                        string dbUrl = parent["ParentNavigateUrl"].ToString().Trim();

                        if (!string.IsNullOrEmpty(dbUrl) && dbUrl != "#")
                            parentUrl = ResolveUrl(dbUrl);
                    }

                    sb.AppendLine("<li class='nav-item'>");

                    sb.AppendLine("<a href='" + parentUrl + "' class='nav-link'>");

                    sb.AppendLine("<i class='nav-icon " + parent["ParentIcon"] + "'></i>");

                    sb.AppendLine("<p>");
                    sb.AppendLine(Server.HtmlEncode(parent["ParentMenuName"].ToString()));
                    sb.AppendLine("</p>");

                    sb.AppendLine("</a>");

                    sb.AppendLine("</li>");
                }
            }

            ltMenu.Text = sb.ToString();
        }

        private string GenerateMenu(DataTable dt, int? parent)
        {
            StringBuilder sb = new StringBuilder();

            DataRow[] rows;

            if (parent == null)
                rows = dt.Select("ParentMenuID IS NULL");
            else
                rows = dt.Select("ParentMenuID=" + parent);

            foreach (DataRow row in rows)
            {
                int id = Convert.ToInt32(row["ParentMenuID"]);

                DataRow[] child = dt.Select("ParentMenuID=" + id);

                if (child.Length > 0)
                {
                    sb.Append("<li class='nav-item has-treeview'>");

                    sb.Append("<a href='#' class='nav-link'>");

                    sb.Append("<i class='nav-icon " + row["IconClass"] + "'></i>");

                    sb.Append("<p>");

                    sb.Append(row["MenuName"]);

                    sb.Append("<i class='right fas fa-angle-left'></i>");

                    sb.Append("</p>");

                    sb.Append("</a>");

                    sb.Append("<ul class='nav nav-treeview'>");

                    sb.Append(GenerateMenu(dt, id));

                    sb.Append("</ul>");

                    sb.Append("</li>");
                }
                else
                {
                    sb.Append("<li class='nav-item'>");

                    sb.Append("<a href='" + row["NavigateUrl"] + "' class='nav-link'>");

                    sb.Append("<i class='nav-icon " + row["IconClass"] + "'></i>");

                    sb.Append("<p>");

                    sb.Append(row["MenuName"]);

                    sb.Append("</p>");

                    sb.Append("</a>");

                    sb.Append("</li>");
                }
            }

            return sb.ToString();
        }
    }
}