using Bhusamadhan.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.LandDispute.Entry
{
    public partial class MenuManagement : System.Web.UI.Page
    {
        string userid = "";
        string userrole = "";
        int roleid;
        DBHelper objDBHelper = new DBHelper();
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

                if (roleid != 17 || !string.Equals(userrole, "NICADMIN", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                if (!IsPostBack)
                {
                   
                    LoadParentDropdown();
                    LoadChildMenus();
                  
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


        private void LoadParentDropdown()
        {
            ddlChildParent.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                string sql = @" SELECT ParentMenuID, MenuName  FROM BS_TopMenuMst WHERE IsActive = 1  ORDER BY DisplayOrder, MenuName";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlChildParent.DataSource = dt;
                    ddlChildParent.DataTextField = "MenuName";
                    ddlChildParent.DataValueField = "ParentMenuID";
                    ddlChildParent.DataBind();
                    ddlChildParent.Items.Insert(0, new ListItem("-- Select Parent Menu --", "0"));

                }
                else
                {
                    ddlChildParent.DataSource = null;
                    ddlChildParent.DataBind();

                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        private void LoadChildMenus()
        {
            gvChildMenus.DataSource = null;

            gvChildMenus.DataBind();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                string sql = @" SELECT C.ChildMenuID,  C.ParentMenuID, P.MenuName AS ParentMenuName, C.MenuName,
                               C.NavigateUrl, C.IconClass, C.DisplayOrder, C.IsActive FROM BS_ChildMenuMst C
                               INNER JOIN BS_TopMenuMst P ON C.ParentMenuID = P.ParentMenuID
                               ORDER BY P.DisplayOrder, C.DisplayOrder,  C.ChildMenuID";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);

                gvChildMenus.DataSource = dt;

                gvChildMenus.DataBind();

                // Populate Parent Menu dropdown
                foreach (GridViewRow row in gvChildMenus.Rows)
                {
                    DropDownList ddlParent = row.FindControl("ddlGridParentMenu") as DropDownList;

                    if (ddlParent == null)
                        continue;


                    LoadGridParentDropdown(ddlParent);


                    // Get ParentMenuID from DataKeys
                    int childMenuID =  Convert.ToInt32( gvChildMenus.DataKeys[row.RowIndex].Value);


                    DataRow[] selectedRows = dt.Select( "ChildMenuID = " + childMenuID);


                    if (selectedRows.Length > 0)
                    {
                        string parentMenuID = selectedRows[0]["ParentMenuID"].ToString();

                        ListItem item = ddlParent.Items.FindByValue(  parentMenuID);

                        if (item != null)
                        {
                            ddlParent.ClearSelection();
                            item.Selected = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        private void LoadGridParentDropdown( DropDownList ddlParent)
        {
            ddlParent.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                string sql = @" SELECT ParentMenuID, MenuName FROM BS_TopMenuMst WHERE IsActive = 1 ORDER BY DisplayOrder, MenuName";

                DataTable dt = objDBHelper.GetResults( sql, listSQLP, false);


                ddlParent.DataSource = dt;

                ddlParent.DataTextField = "MenuName";

                ddlParent.DataValueField = "ParentMenuID";

                ddlParent.DataBind();

                ddlParent.Items.Insert( 0, new ListItem( "-- Select Parent Menu --", "0"));
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


        protected void btnAddChild_Click(object sender, EventArgs e)
        {
            if (roleid != 17)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }

            try
            {
                int parentMenuID =
                    Convert.ToInt32(ddlChildParent.SelectedValue);

                if (parentMenuID <= 0)
                {
                    lblMsg.Text = "Please select Parent Menu.";
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtChildMenuName.Text))
                {
                    lblMsg.Text = "Please enter Page Name.";
                    return;
                }

                int displayOrder;

                if (!int.TryParse(
                    txtChildDisplayOrder.Text.Trim(),
                    out displayOrder))
                {
                    lblMsg.Text = "Please enter valid Display Order.";
                    return;
                }


                // Generate ChildMenuID
                List<System.Data.SqlClient.SqlParameter> listSQLP1 =
                    new List<System.Data.SqlClient.SqlParameter>();

                string getIdSql = @" SELECT ISNULL(MAX(ChildMenuID), 0) + 1 FROM BS_ChildMenuMst";


                DataTable dtID =  objDBHelper.GetResults( getIdSql,  listSQLP1,false);


                int childMenuID = Convert.ToInt32(dtID.Rows[0][0]);


                // Insert child menu
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add( new System.Data.SqlClient.SqlParameter( "@ChildMenuID", childMenuID));

                listSQLP.Add( new System.Data.SqlClient.SqlParameter("@ParentMenuID", parentMenuID));

                listSQLP.Add( new System.Data.SqlClient.SqlParameter("@MenuName", txtChildMenuName.Text.Trim()));

                listSQLP.Add( new System.Data.SqlClient.SqlParameter("@NavigateUrl", txtChildNavigateUrl.Text.Trim()));

                listSQLP.Add( new System.Data.SqlClient.SqlParameter( "@IconClass", txtChildIconClass.Text.Trim()));

                listSQLP.Add( new System.Data.SqlClient.SqlParameter("@DisplayOrder", displayOrder));

                listSQLP.Add( new System.Data.SqlClient.SqlParameter( "@IsActive", chkChildActive.Checked));


                string sql = @"
            INSERT INTO BS_ChildMenuMst
            (
                ChildMenuID,
                ParentMenuID,
                MenuName,
                NavigateUrl,
                IconClass,
                DisplayOrder,
                IsActive
            )
            VALUES
            (
                @ChildMenuID,
                @ParentMenuID,
                @MenuName,
                @NavigateUrl,
                @IconClass,
                @DisplayOrder,
                @IsActive
            )";


                bool result = objDBHelper.SetData( sql, listSQLP,false);


                if (!result)
                {
                    lblMsg.Text = "Page could not be added.";
                    return;
                }


                lblMsg.Text = "Page added successfully.";

                LoadChildMenus();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnSaveChildMenus_Click(object sender, EventArgs e)
        {
            //if (roleid != 17)
            //{
            //    Response.Redirect("~/Default.aspx");
            //    return;
            //}
            bool b = false;
            try
            {
                foreach (GridViewRow row in gvChildMenus.Rows)
                {
                    if (row.RowType != DataControlRowType.DataRow)
                        continue;


                    int childMenuID =  Convert.ToInt32( gvChildMenus.DataKeys[row.RowIndex].Value);


                    DropDownList ddlParent =  row.FindControl( "ddlGridParentMenu") as DropDownList;


                    TextBox txtMenuName =  row.FindControl( "txtGridMenuName") as TextBox;


                    TextBox txtNavigateUrl =  row.FindControl(  "txtGridNavigateUrl")  as TextBox;


                    TextBox txtIconClass =  row.FindControl( "txtGridIconClass") as TextBox;


                    TextBox txtDisplayOrder = row.FindControl( "txtGridDisplayOrder")  as TextBox;


                    CheckBox chkActive = row.FindControl( "chkGridActive") as CheckBox;


                    if (ddlParent == null || txtMenuName == null ||  txtNavigateUrl == null ||  txtIconClass == null || txtDisplayOrder == null || chkActive == null)
                    {
                        continue;
                    }


                    int parentMenuID;

                    if (!int.TryParse( ddlParent.SelectedValue,  out parentMenuID) ||  parentMenuID <= 0)
                    {
                        lblMsg.Text = "Invalid Parent Menu for ChildMenuID: " + childMenuID;

                        return;
                    }


                    int displayOrder;

                    if (!int.TryParse( txtDisplayOrder.Text.Trim(),  out displayOrder))
                    {
                        lblMsg.Text = "Invalid Display Order for ChildMenuID: " + childMenuID;

                        return;
                    }


                    List<System.Data.SqlClient.SqlParameter> listSQLP =  new List<System.Data.SqlClient.SqlParameter>();


                    listSQLP.Add(  new System.Data.SqlClient.SqlParameter( "@ChildMenuID",  childMenuID));


                    listSQLP.Add( new System.Data.SqlClient.SqlParameter( "@ParentMenuID",  parentMenuID));


                    listSQLP.Add(new System.Data.SqlClient.SqlParameter( "@MenuName", txtMenuName.Text.Trim()));


                    listSQLP.Add( new System.Data.SqlClient.SqlParameter( "@NavigateUrl", txtNavigateUrl.Text.Trim()));


                    listSQLP.Add( new System.Data.SqlClient.SqlParameter( "@IconClass", txtIconClass.Text.Trim()));


                    listSQLP.Add(  new System.Data.SqlClient.SqlParameter( "@DisplayOrder",  displayOrder));


                    listSQLP.Add( new System.Data.SqlClient.SqlParameter( "@IsActive", chkActive.Checked));


                    string sql = @"  UPDATE BS_ChildMenuMst
                                        SET
                                            ParentMenuID = @ParentMenuID,
                                            MenuName = @MenuName,
                                            NavigateUrl = @NavigateUrl,
                                            IconClass = @IconClass,
                                            DisplayOrder = @DisplayOrder,
                                            IsActive = @IsActive
                                        WHERE ChildMenuID = @ChildMenuID";


                    b = objDBHelper.SetData(sql, listSQLP, false);
                }

                LoadChildMenus();

                lblMsg.Text = "All child menu changes saved successfully.";

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        //private void LoadRoles()
        //{
        //    ddlRole.Items.Clear();

        //    try
        //    {
        //        List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

        //        string sql = @" SELECT ID, Role, RoleDesc,Role + ' - ' + ISNULL(RoleDesc, '') AS RoleDisplay FROM mst_Role ORDER BY Role";

        //        DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlRole.DataSource = dt;
        //            ddlRole.DataTextField = "RoleDisplay";
        //            ddlRole.DataValueField = "ID";
        //            ddlRole.DataBind();
        //            ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "0"));

        //        }
        //        else
        //        {
        //            ddlRole.DataSource = null;
        //            ddlRole.DataBind();

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = ex.Message.ToString();
        //    }
        //}

      
    }
}