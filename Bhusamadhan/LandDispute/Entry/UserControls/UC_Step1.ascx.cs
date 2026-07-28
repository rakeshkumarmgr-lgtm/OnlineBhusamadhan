
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bhusamadhan.DB;

namespace Bhusamadhan.LandDispute.Entry.UserControls
{
    public partial class UC_Step1 : System.Web.UI.UserControl
    {
        DBHelper objDBHelper = new DBHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //------Master Bind-----------------
                LoadMasterData();
            

                ViewState["vadiDetails"] = vadiDetails();
            }
        }

        private void LoadMasterData()
        {
            AdharYearsBind();
            BindDist_Wadi_Pratiwadi();
            BindSubDivision_wadi();
            BindBlock_Wadi();
            BindPolice_wadi();
            BindPanchyat_Wadi();
            BindVillage_Wadi();
            bindward_Wadi();
            BindVadi_Prativadi_Anya_Type();
            BindVadi_Sanstha_Anya_Type();
            bindDepartment();
        }

        protected void AdharYearsBind()
        {
            //string[] retVal = new string[122]; ;
            //int index = 0;
            //for (int i = 2021; i >= 1900; i--)
            //{
            //    retVal[index] = i.ToString();
            //    index = index + 1;
            //}

            //ddlYear.DataSource = retVal;
            //ddlYear.DataBind();
            //ddlYear.Items.Insert(0, new ListItem("--Select--", "0"));

            //return;

            ddlYear.DataSource = Enumerable.Range(1900, 122).Reverse().Select(x => x.ToString());

            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindDist_Wadi_Pratiwadi()
        {
            ddlUserDist.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                DataTable dtDistrict = objDBHelper.GetResults("SELECT distinct DISTRICTNAME,DISTRICTCODE from mst_Commissionary_Districts ORDER BY DISTRICTNAME;", listSQLP, false);
                if (dtDistrict.Rows.Count > 0)
                {
                    ddlUserDist.DataSource = dtDistrict;
                    ddlUserDist.DataTextField = "DISTRICTNAME";
                    ddlUserDist.DataValueField = "DISTRICTCODE";
                    ddlUserDist.DataBind();
                    ddlUserDist.Items.Insert(0, new ListItem("--Select--", "0"));

                    //ddlPDistrict.DataSource = dt;
                    //ddlPDistrict.DataTextField = "DISTRICTNAME";
                    //ddlPDistrict.DataValueField = "DISTRICTCODE";
                    //ddlPDistrict.DataBind();
                    //ddlPDistrict.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlUserDist.DataSource = null;

                    ddlUserDist.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindSubDivision_wadi()
        {
            ddlUserSubdivision.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", ddlUserDist.SelectedValue.ToString()));
                DataTable dt = objDBHelper.GetResults("select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where sd.DistCode=@District_Code order by sd.Sd_Name_En", listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlUserSubdivision.DataSource = dt;
                    ddlUserSubdivision.DataTextField = "SubDivisionName";
                    ddlUserSubdivision.DataValueField = "SubDivisionCode";
                    ddlUserSubdivision.DataBind();
                    ddlUserSubdivision.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlUserSubdivision.DataSource = null;

                    ddlUserSubdivision.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindBlock_Wadi()
        {
            ddlUserBlock.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", ddlUserDist.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Subdivision_Code", ddlUserSubdivision.SelectedValue.ToString()));

                DataTable dt = objDBHelper.GetResults("select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) order by BlockName", listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlUserBlock.DataSource = dt;
                    ddlUserBlock.DataTextField = "BlockName";
                    ddlUserBlock.DataValueField = "BlockCode";
                    ddlUserBlock.DataBind();
                    ddlUserBlock.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlUserBlock.DataSource = null;

                    ddlUserBlock.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindPolice_wadi()
        {
            ddlUserThana.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@District_Code", ddlUserDist.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Subdivision_Code", ddlUserSubdivision.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Circle_Code", ddlUserBlock.SelectedValue.ToString()));

                string sql = @"select DISTINCT  t.Police_Station,t.PS_Code from mst_thana t
	                        left join MstThanaMapping m on m.Thana_Code=t.PS_Code 
	                        left join Blocks b on b.BlockCode=m.Code and m.Type='Block'
	                        where District_code=@District_Code and  b.SubDivCode is not null and m.code=@Circle_Code and b.SubDivCode=@Subdivision_Code
                            ORDER BY Police_Station";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlUserThana.DataSource = dt;
                    ddlUserThana.DataTextField = "Police_Station";
                    ddlUserThana.DataValueField = "PS_Code";
                    ddlUserThana.DataBind();
                    ddlUserThana.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlUserBlock.DataSource = null;

                    ddlUserBlock.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindPanchyat_Wadi()
        {
            ddlUserPanchyat.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@BlockCode", ddlUserBlock.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));

                DataTable dt = objDBHelper.GetResults("select DISTINCT PanchayatCode,PanchayatNameHnd,PanchayatName from mst_Panchayats t inner join Blocks p on t.BlockCode = p.BlockCode where p.BlockCode=@BlockCode and (@AreaType='' or t.AreaType=@AreaType) order by PanchayatName", listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlUserPanchyat.DataSource = dt;
                    ddlUserPanchyat.DataTextField = "PanchayatName";
                    ddlUserPanchyat.DataValueField = "PanchayatCode";
                    ddlUserPanchyat.DataBind();
                    ddlUserPanchyat.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlUserPanchyat.DataSource = null;

                    ddlUserPanchyat.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindVillage_Wadi()
        {
            ddlUserVillage.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@PanchayatCode", ddlUserPanchyat.SelectedValue.ToString()));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));

                string sql = @"select DISTINCT v.VILLCODE, v.VILLNAME  from mst_Panchayats p 
                            inner join PanchayatVillage pv on p.PanchayatCode=pv.PanchayatCode
                            inner join mst_VillageMaster v on v.VILLCODE=pv.VillageCode
                            where p.PanchayatCode=@PanchayatCode order by v.VILLNAME	";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlUserVillage.DataSource = dt;
                    ddlUserVillage.DataTextField = "VILLNAME";
                    ddlUserVillage.DataValueField = "VILLCODE";
                    ddlUserVillage.DataBind();
                    ddlUserVillage.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlUserVillage.DataSource = null;

                    ddlUserVillage.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void bindward_Wadi()
        {
            ddlUserWard.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Panchayat", ddlUserPanchyat.SelectedValue.ToString()));
                listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));

                string sql = @"select DISTINCT t.WARDNAME,WARDCODE,t.AreaType from mst_Wards t left join mst_Panchayats p on t.PANCHAYATCODE = p.PanchayatCode where p.PANCHAYATCODE=@Panchayat and p.AreaType=@AreaType order by WARDNAME";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlUserWard.DataSource = dt;
                    ddlUserWard.DataTextField = "WARDNAME";
                    ddlUserWard.DataValueField = "WARDCODE";
                    ddlUserWard.DataBind();
                    ddlUserWard.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlUserWard.DataSource = null;

                    ddlUserWard.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindVadi_Prativadi_Anya_Type()
        {
            ddlWsanstha_naam.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Panchayat", ddlUserPanchyat.SelectedValue.ToString()));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));

                string sql = @"select id, name from Vadi_Prativadi_Anya_Type order by id asc";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlWsanstha_naam.DataSource = dt;
                    ddlWsanstha_naam.DataTextField = "name";
                    ddlWsanstha_naam.DataValueField = "id";
                    ddlWsanstha_naam.DataBind();
                    ddlWsanstha_naam.Items.Insert(0, new ListItem("--Select--", "0"));

                    //ddlPsanstha_naam.DataSource = dt;
                    //ddlPsanstha_naam.DataTextField = "name";
                    //ddlPsanstha_naam.DataValueField = "id";
                    //ddlPsanstha_naam.DataBind();
                    //ddlPsanstha_naam.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlWsanstha_naam.DataSource = null;

                    ddlWsanstha_naam.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        private void BindVadi_Sanstha_Anya_Type()
        {

            ddlWsanshaanya_naam.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Panchayat", ddlUserPanchyat.SelectedValue.ToString()));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));

                string sql = @"SELECT id, name FROM mst_sanstha_ka_sambandh_type order by id asc";

                DataTable dt = objDBHelper.GetResults(sql, listSQLP, false);
                if (dt.Rows.Count > 0)
                {
                    ddlWsanshaanya_naam.DataSource = dt;
                    ddlWsanshaanya_naam.DataTextField = "name";
                    ddlWsanshaanya_naam.DataValueField = "id";
                    ddlWsanshaanya_naam.DataBind();
                    ddlWsanshaanya_naam.Items.Insert(0, new ListItem("--Select--", "0"));

                    //ddlPsanshaanya_naam.DataSource = dt;
                    //ddlPsanshaanya_naam.DataTextField = "name";
                    //ddlPsanshaanya_naam.DataValueField = "id";
                    //ddlPsanshaanya_naam.DataBind();
                    //ddlPsanshaanya_naam.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlWsanshaanya_naam.DataSource = null;

                    ddlWsanshaanya_naam.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }


        private void bindDepartment()
        {

            ddlWvibhaag_naam.Items.Clear();

            try
            {
                List<System.Data.SqlClient.SqlParameter> listSQLP = new List<System.Data.SqlClient.SqlParameter>();

                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@Panchayat", ddlUserPanchyat.SelectedValue.ToString()));
                //listSQLP.Add(new System.Data.SqlClient.SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString()));


                DataTable dt = objDBHelper.GetResults("SP_BindDepartment", listSQLP, true);
                if (dt.Rows.Count > 0)
                {
                    ddlWvibhaag_naam.DataSource = dt;
                    ddlWvibhaag_naam.DataTextField = "name";
                    ddlWvibhaag_naam.DataValueField = "id";
                    ddlWvibhaag_naam.DataBind();
                    ddlWvibhaag_naam.Items.Insert(0, new ListItem("--Select--", "0"));

                    //ddlPvibhaag_naam.DataSource = dt;
                    //ddlPvibhaag_naam.DataTextField = "name";
                    //ddlPvibhaag_naam.DataValueField = "id";
                    //ddlPvibhaag_naam.DataBind();
                    //ddlPvibhaag_naam.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlWvibhaag_naam.DataSource = null;

                    ddlWvibhaag_naam.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlUserDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDistrict();
            ddlUserAreatype.SelectedIndex = 0;
        }

        private void RefreshDistrict()
        {
            BindSubDivision_wadi();
            BindBlock_Wadi();
            BindPolice_wadi();
            BindVillage_Wadi();
            BindPanchyat_Wadi();
            bindward_Wadi();
        }
        //-------------------------------------------------------------------
        protected void ddlUserSubdivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSubdivision();
            ddlUserAreatype.SelectedIndex = 0;
        }

        private void RefreshSubdivision()
        {
            BindBlock_Wadi();
            BindPolice_wadi();
            BindVillage_Wadi();
            BindPanchyat_Wadi();
            bindward_Wadi();
        }

        //-------------------------------------------------------------------

        protected void ddlUserBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshBlock();
            ddlUserAreatype_SelectedIndexChanged(sender, e);
            ddlUserAreatype.SelectedIndex = 0;
        }

        private void RefreshBlock()
        {
            BindPolice_wadi();
            BindVillage_Wadi();
            BindPanchyat_Wadi();
            bindward_Wadi();
        }
        //-------------------------------------------------------------------
        protected void ddlUserPanchyat_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPanchayat();
        }

        private void RefreshPanchayat()
        {
            BindVillage_Wadi();
            bindward_Wadi();
        }
        //-------------------------------------------------------------------
        protected void ddlUserAreatype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserAreatype.SelectedIndex == 2)
            {
                labUVillage.Text = "नगर निकाय";
                divUserMohalla.Visible = true;
                divUserVillageCol.Visible = false;
                UWard.Visible = true;
            }
            else
            {
                labUVillage.Text = "ग्राम पंचायत";
                divUserMohalla.Visible = false;
                divUserVillageCol.Visible = true;
                UWard.Visible = false;
            }

            BindPanchyat_Wadi();
            RefreshPanchayat();

            //ddlUserVillage_SelectedIndexChanged(sender, e);
            //ddlUserWard_SelectedIndexChanged(sender, e);
            //ddlUserPanchyat_SelectedIndexChanged(sender, e);
        }
        //-------------------------------------------------------------------
        protected void ddl_is_vadi_from_an_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_is_vadi_from_an_dept.SelectedValue == "Y")
            {
                divWVibhag_details.Visible = true;
                divWvibhaag_padanaam.Visible = true;
                ddl_is_vadi_from_an_org.SelectedValue = "N";
                ddl_is_vadi_from_an_org.Enabled = false;
                divWSanstha_details.Visible = false;
            }
            else if (ddl_is_vadi_from_an_dept.SelectedValue == "N")
            {
                divWVibhag_details.Visible = false;
                divWvibhaag_padanaam.Visible = false;
                ddl_is_vadi_from_an_org.SelectedValue = "0";
                ddl_is_vadi_from_an_org.Enabled = true;
                //divWSanstha_details.Visible = false;
            }
            else if (ddl_is_vadi_from_an_dept.SelectedValue == "0")
            {
                divWVibhag_details.Visible = false;
                divWvibhaag_padanaam.Visible = false;
                ddl_is_vadi_from_an_org.SelectedValue = "0";
                ddl_is_vadi_from_an_org.Enabled = true;
                divWSanstha_details.Visible = false;
            }
        }
        //-------------------------------------------------------------------
        protected void ddl_is_vadi_from_an_org_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlWsanstha_naam.SelectedIndex = 0;
            txtWsanstha_padanaam.Text = "";
            divWSanstha_details.Visible = false;
            txtWsanstha_naam.Text = "";
            if (ddl_is_vadi_from_an_org.SelectedIndex == 1)
            {
                divWSanstha_details.Visible = true;
            }
        }


        //-----------add and remove Record in view step for first button click.It does not go to database

        protected void btnAddVadiDetail_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            try
            {
                DataTable dt = ViewState["vadiDetails"] as DataTable;

                if (dt == null)
                {
                    lblMsg.Text = "ViewState[vadiDetails] is NULL";
                    return;
                }

                DataRow dr = dt.NewRow();

                #region Basic Information

                dr["vadi_Name"] = txtNamePerAadhaar.Text.Trim();

                dr["Vadi_Father_Husband_Name"] = txtFName.Text.Trim();

                dr["NameAsPerAadhaar"] = txtNamePerAadhaar.Text.Trim();

                dr["AadharNo"] = "";

                dr["YearOfBirthAsPerAadhaar"] = ddlYear.SelectedValue == "0" ? (object)DBNull.Value : Convert.ToInt32(ddlYear.SelectedValue);

                dr["SexAsPerAadhaar"] = ddlgender.SelectedValue;

                dr["Vadi_MobileNo"] = txtvadimobile.Text.Trim();

                dr["IsVerifyAadhaa"] = "N";

                #endregion

                #region Department

                dr["is_vadi_from_an_dept"] = ddl_is_vadi_from_an_dept.SelectedValue;

                dr["vadi_dept_id"] = ddlWvibhaag_naam.SelectedValue == "0" ? "" : ddlWvibhaag_naam.SelectedValue;

                dr["vadi_dept_name"] = ddlWvibhaag_naam.SelectedItem?.Text ?? "";

                dr["vadi_dept_pad_name"] = txtWvibhaag_padanaam.Text.Trim();

                #endregion

                #region Organization

                dr["is_vadi_from_an_org"] = ddl_is_vadi_from_an_org.SelectedValue;

                dr["vadi_org_type"] = ddlWsanstha_naam.SelectedValue == "0" ? (object)DBNull.Value : Convert.ToInt32(ddlWsanstha_naam.SelectedValue);

                dr["vadi_org_name"] = txtWsanstha_naam.Text.Trim();

                dr["vadi_org_pad_name"] = txtWsanstha_padanaam.Text.Trim();

                dr["sanstha_sambandh_type"] = ddlWsanshaanya_naam.SelectedValue == "0" ? (object)DBNull.Value : Convert.ToInt32(ddlWsanshaanya_naam.SelectedValue);

                #endregion

                #region Address Codes

                dr["Vadi_District_Code"] = ddlUserDist.SelectedValue;
                dr["Vadi_Sub_DivCode"] = ddlUserSubdivision.SelectedValue;
                dr["Vadi_Block_Code"] = ddlUserBlock.SelectedValue;
                dr["Vadi_Thana_code"] = ddlUserThana.SelectedValue;
                dr["Vadi_AreaType"] = ddlUserAreatype.SelectedValue;
                dr["Vadi_Panchayat_Code"] = ddlUserPanchyat.SelectedValue;
                dr["Vadi_Village_Code"] = ddlUserVillage.SelectedValue;
                dr["Vadi_WardNo"] = ddlUserWard.SelectedValue;

                dr["Vadi_Panchayat_Anya"] = txtUserPanchyat_Anya.Text.Trim();
                dr["Vadi_Village_Anya"] = txtUserVillage_Anya.Text.Trim();
                dr["Vadi_WardNo_Anya"] = txtUserWard_Anya.Text.Trim();
                dr["Mohalla"] = txtUserMohalla.Text.Trim();

                #endregion

                #region Display Columns (For Repeater Only)

                dr["DistrictName"] = ddlUserDist.SelectedItem?.Text ?? "";
                dr["SubdivisionName"] = ddlUserSubdivision.SelectedItem?.Text ?? "";
                dr["BlockName"] = ddlUserBlock.SelectedItem?.Text ?? "";
                dr["ThanaName"] = ddlUserThana.SelectedItem?.Text ?? "";
                dr["AreaTypeName"] = ddlUserAreatype.SelectedItem?.Text ?? "";
                dr["PanchayatName"] = ddlUserPanchyat.SelectedItem?.Text ?? "";
                dr["VillageName"] = ddlUserVillage.SelectedItem?.Text ?? "";
                dr["WardName"] = ddlUserWard.SelectedItem?.Text ?? "";

                dr["OrgTypeName"] = ddl_is_vadi_from_an_dept.SelectedValue == "Y" ? ddlWvibhaag_naam.SelectedItem?.Text ?? "" : ddl_is_vadi_from_an_org.SelectedValue == "Y" ? ddlWsanstha_naam.SelectedItem?.Text ?? "" : "";

                dr["AssociationName"] = ddlWsanshaanya_naam.SelectedItem?.Text ?? "";

                #endregion

                dt.Rows.Add(dr);

                ViewState["vadiDetails"] = dt;

                BindWadiRepeater();

                hfwadiprint.Value = "Printstep1";

                //pnlupdate1.Update();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString().Replace(Environment.NewLine, "<br/>");
            }
        }

        private void BindWadiRepeater()
        {
            rptWadi.DataSource = ViewState["vadiDetails"] as DataTable;
            rptWadi.DataBind();
        }

        private DataTable vadiDetails()
        {
            DataTable dt = new DataTable();

            #region Database Columns (Values to Save)

            dt.Columns.Add("vadi_Name", typeof(string));

            dt.Columns.Add("is_vadi_from_an_org", typeof(string));
            dt.Columns.Add("vadi_org_type", typeof(int));              // Organization Type Id
            dt.Columns.Add("vadi_org_name", typeof(string));
            dt.Columns.Add("vadi_org_pad_name", typeof(string));

            dt.Columns.Add("is_vadi_from_an_dept", typeof(string));
            dt.Columns.Add("vadi_dept_id", typeof(string));            // Department Id
            dt.Columns.Add("vadi_dept_name", typeof(string));          // Department Name
            dt.Columns.Add("vadi_dept_pad_name", typeof(string));

            dt.Columns.Add("Vadi_Father_Husband_Name", typeof(string));
            dt.Columns.Add("NameAsPerAadhaar", typeof(string));
            dt.Columns.Add("AadharNo", typeof(string));

            dt.Columns.Add("YearOfBirthAsPerAadhaar", typeof(int));
            dt.Columns.Add("SexAsPerAadhaar", typeof(string));

            dt.Columns.Add("Vadi_District_Code", typeof(string));
            dt.Columns.Add("Vadi_Sub_DivCode", typeof(string));
            dt.Columns.Add("Vadi_Block_Code", typeof(string));
            dt.Columns.Add("Vadi_Thana_code", typeof(string));
            dt.Columns.Add("Vadi_AreaType", typeof(string));
            dt.Columns.Add("Vadi_Panchayat_Code", typeof(string));
            dt.Columns.Add("Vadi_Village_Code", typeof(string));
            dt.Columns.Add("Vadi_WardNo", typeof(string));

            dt.Columns.Add("Vadi_MobileNo", typeof(string));
            dt.Columns.Add("IsVerifyAadhaa", typeof(string));

            dt.Columns.Add("Vadi_Panchayat_Anya", typeof(string));
            dt.Columns.Add("Vadi_Village_Anya", typeof(string));
            dt.Columns.Add("Vadi_WardNo_Anya", typeof(string));
            dt.Columns.Add("Mohalla", typeof(string));

            dt.Columns.Add("sanstha_sambandh_type", typeof(int));      // Relation Id

            #endregion


            #region Display Columns (Used Only in Repeater)

            dt.Columns.Add("DistrictName", typeof(string));
            dt.Columns.Add("SubdivisionName", typeof(string));
            dt.Columns.Add("BlockName", typeof(string));
            dt.Columns.Add("ThanaName", typeof(string));
            dt.Columns.Add("AreaTypeName", typeof(string));
            dt.Columns.Add("PanchayatName", typeof(string));
            dt.Columns.Add("VillageName", typeof(string));
            dt.Columns.Add("WardName", typeof(string));

            dt.Columns.Add("OrgTypeName", typeof(string));             // संस्था का प्रकार
            dt.Columns.Add("AssociationName", typeof(string));         // संस्था का सम्बन्ध

            #endregion

            return dt;
        }

        protected void rptWadi_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                DataTable dt = ViewState["vadiDetails"] as DataTable;

                if (dt == null)
                    return;

                int index = Convert.ToInt32(e.CommandArgument);

                if (index >= 0 && index < dt.Rows.Count)
                {
                    dt.Rows.RemoveAt(index);

                    ViewState["vadiDetails"] = dt;

                    BindWadiRepeater();
                }
            }
        }
    }
}