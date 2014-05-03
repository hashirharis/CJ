using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UserProfileControl : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    int userid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        fillComboValues();

        if (!IsPostBack)
            if (Session["UserID"] != null)
            {
                userid = int.Parse(Session["UserID"].ToString());
                CheckExistence(userid);
            }
        
        if (Session["NewIndustry"] != null)
            if (Session["NewIndustry"].ToString() == "Yes")
            { txtIndustry.Visible = true; ddlIndustry.Visible = false; }

        if (Session["NewOrg"] != null)
            if (Session["NewOrg"].ToString() == "Yes")
            { txtOrganization.Visible = true; ddlOrg.Visible = false; }

        if (Session["GrpExists"] != null)
            if (Session["GrpExists"].ToString() == "No")
            { lblUserGrp_label.Visible = false; ddlGrpUser.Visible = false; }
        
    }
    private bool CheckAge()
    {
        try
        {
            int age = int.Parse(txtAge.Text.Trim());
            if (age <= 12) { lblMessage.Text = "age should be greater than 12"; return false; }

            return true;
        }
        catch (Exception ex) { lblMessage.Text = "Enter valid age"; return false; }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Session["SubCtrl"] = "UserTrainingControl.ascx";
        //Response.Redirect("FJAHome.aspx");

        int age = 0;
        if (ValidateMandatory() == false)
        {
            lblMessage.Text = "Enter the Required Fields";
            return;
        }
        else
        {
            if (CheckAge() == false) { //lblMessage.Text = "Enter valid age"; 
                return; }

            try
            {
                int industryid = 0, orgid = 0;
                if (Session["UserID"] != null)
                    userid = int.Parse(Session["UserID"].ToString());
                string qualification = ""; string organizationname = ""; string industryname = "";
                if (ddlQualification.SelectedValue == "00" || ddlQualification.SelectedValue == "0")
                {
                    qualification = txtEduQual.Text;
                    if (qualification != "")
                        dataclass.AddQualification(0, txtEduQual.Text, 1, userid);
                }
                else qualification = ddlQualification.SelectedItem.Text;

                if (ddlOrg.SelectedValue == "00" || ddlOrg.SelectedValue == "0")
                {
                    organizationname = txtOrganization.Text;
                    if (organizationname != "")
                    {
                        dataclass.AddOrganization(0, organizationname, 1, userid, "",1,"","");
                        var getorgId = from orgdet in dataclass.Organizations
                                       where orgdet.Name == organizationname
                                       select orgdet;
                        if (getorgId.Count() > 0)
                            if (getorgId.First().OrganizationID != null)
                                orgid = int.Parse(getorgId.First().OrganizationID.ToString());
                    }
                }
                else
                    if (ddlOrg.SelectedIndex > 0)
                        orgid = int.Parse(ddlOrg.SelectedValue);

                if (ddlIndustry.SelectedValue == "00" || ddlIndustry.SelectedValue == "0")
                {
                    industryname = txtIndustry.Text;
                    if (industryname != "")
                    {
                        dataclass.AddIndustry(0, industryname, 1, userid);

                        var getindId = from inddet in dataclass.Industries
                                       where inddet.Name == industryname
                                       select inddet;
                        if (getindId.Count() > 0)
                            if (getindId.First().IndustryID != null)
                                industryid = int.Parse(getindId.First().IndustryID.ToString());

                    }
                }
                else
                    if (ddlIndustry.SelectedIndex > 0)
                        industryid = int.Parse(ddlIndustry.SelectedValue);


                //if (ddlGrpUser.SelectedValue == "00" || ddlGrpUser.SelectedValue == "0")
                //{
                //    usergroup  = txtUserGroup.Text;
                //    dataclass.AddGroupUser(0, usergroup, 1, userid);
                //}
                //else qualification = ddlGrpUser.SelectedItem.Text;

                if (txtAge.Text != null)
                    age = int.Parse(txtAge.Text);
                dataclass.AddUserProfile(userid, txtFsName.Text, txtMidName.Text, txtLstName.Text,
                    ddlGender.SelectedValue, age, orgid, industryid, int.Parse(ddlJobCatgy.SelectedValue), int.Parse(ddlGrpUser.SelectedValue),
                    txtJob.Text, int.Parse(ddlTotExpYears.SelectedValue), int.Parse(ddlTotExpMonths.SelectedValue), int.Parse(ddlCurExpYears.SelectedValue), int.Parse(ddlCurExpMonths.SelectedValue), qualification, txtProffQual.Text, 1, userid, txtEmailId.Text, txtPhoneNumber.Text);

                dataclass.AddUserFirstLoginDate(userid);

                ClearControls();
                lblMessage.Text = "Profile Details are Saved Successfully";

                string usercode = Session["UserCode"].ToString();
                string curcontrol = "TestIntroductionControl.ascx";// "UserTrainingControl.ascx";// "UserTrainingIntroduction.ascx";//"UserTrainingControl.ascx";

                if (Session["evalResult"] == null)
                {
                    int Evalstatid = 0;
                    if (Session["EvalStatId"] != null)
                        Evalstatid = int.Parse(Session["EvalStatId"].ToString());
                    dataclass.ProcedureEvaluationStatus(Evalstatid, curcontrol, 0, 0, usercode, userid);

                    if (Evalstatid == 0)
                    {
                         var EvaluationDetails = from EvalDet in dataclass.EvaluationStatus
                                                    where EvalDet.UserId == userid
                                                    select EvalDet;
                         if (EvaluationDetails.Count() > 0)
                         {
                             if (EvaluationDetails.First().EvalStatusId != null)
                                 Session["EvalStatId"] = EvaluationDetails.First().EvalStatusId.ToString();
                         }
                    }
                }
                ////
                Session["NewIndustry"] = null; Session["NewOrg"] = null; Session["GrpExists"] = null;
                ////

                Session["SubCtrl"] = curcontrol;
                Response.Redirect("FJAHome.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                return;
            }
        }
    }


    private void CheckExistence(int userid)
    {
        var ProfileDetails1 = from ProfileDetails in dataclass.UserProfiles
                              where ProfileDetails.UserId == userid
                              select ProfileDetails;
        if (ProfileDetails1.Count() > 0)
        //if (ProfileDetails1.First().FirstName != null)
        {
            if (ProfileDetails1.First().FirstName != null)
                txtFsName.Text = ProfileDetails1.First().FirstName.ToString();
            if (ProfileDetails1.First().LastName != null)
                txtLstName.Text = ProfileDetails1.First().LastName.ToString();
            if (ProfileDetails1.First().MiddleName != null)
                txtMidName.Text = ProfileDetails1.First().MiddleName.ToString();
            if (ProfileDetails1.First().Designation != null)
                txtJob.Text = ProfileDetails1.First().Designation.ToString();
            if (ProfileDetails1.First().OrganizationID != null && ProfileDetails1.First().OrganizationID !=0)
            {

                for (int i = 0; i < ddlOrg.Items.Count; i++)
                {
                    if (ddlOrg.Items[i].Value == ProfileDetails1.First().OrganizationID.ToString())
                    {
                        Session["Orgindex"] = i.ToString();
                        ddlOrg.SelectedIndex = i;
                        fillprofiles();
                        break;
                    }
                }
            }
            else { txtOrganization.Visible = true; ddlOrg.Visible = false; Session["NewOrg"] = "Yes"; }

            if (ProfileDetails1.First().GrpUserID != null && ProfileDetails1.First().GrpUserID !=0)
            {
                for (int i = 0; i < ddlGrpUser.Items.Count; i++)
                {
                    if (ddlGrpUser.Items[i].Value == ProfileDetails1.First().GrpUserID.ToString())
                    {
                        Session["GrpIndex"] = i.ToString();
                        ddlGrpUser.SelectedIndex = i; break;
                    }
                }
                //lblUserGrp_label.Visible = false; ddlGrpUser.Visible = false;
            }
            else { lblUserGrp_label.Visible = false; ddlGrpUser.Visible = false; Session["GrpExists"] = "No"; }

            //ddlGrpUser.SelectedValue = ProfileDetails1.First().GrpUserID.ToString();
            if (ProfileDetails1.First().JobCategoryID != null)
                ddlJobCatgy.SelectedValue = ProfileDetails1.First().JobCategoryID.ToString();
            if (ProfileDetails1.First().CompanyID != null && ProfileDetails1.First().CompanyID != 0)
            {
                for(int i=0;i<ddlIndustry.Items.Count;i++)
                {
                    if (ddlIndustry.Items[i].Value == ProfileDetails1.First().CompanyID.ToString())
                    {
                        ddlIndustry.SelectedIndex = i;
                        Session["industryindex"] = i.ToString();
                        break;
                    }
                }
                //ddlIndustry.SelectedValue = ProfileDetails1.First().CompanyID.ToString();
            }
            //else { txtIndustry.Visible = true; ddlIndustry.Visible = false; Session["NewIndustry"] = "Yes"; }
            if (ProfileDetails1.First().PresJobExpYrs != null)
                ddlCurExpYears.SelectedValue = ProfileDetails1.First().PresJobExpYrs.ToString();
            if (ProfileDetails1.First().PresJobExpMonths != null)
                ddlCurExpMonths.SelectedValue = ProfileDetails1.First().PresJobExpMonths.ToString();
            if (ProfileDetails1.First().ProfCertifications != null)
                txtProffQual.Text = ProfileDetails1.First().ProfCertifications.ToString();
            if (ProfileDetails1.First().TotWorkExpYrs != null)
                ddlTotExpYears.SelectedValue = ProfileDetails1.First().TotWorkExpYrs.ToString();
            if (ProfileDetails1.First().TotWorkExpMonths != null)
                ddlTotExpMonths.SelectedValue = ProfileDetails1.First().TotWorkExpMonths.ToString();
            if (ProfileDetails1.First().EducQualifications != null)
            {
                //ddlQualification.SelectedValue = ProfileDetails1.First().EducQualifications.ToString();
                for (int i = 0; i < ddlQualification.Items.Count; i++)
                {
                    if (ddlQualification.Items[i].Text == ProfileDetails1.First().EducQualifications.ToString())
                    {
                        Session["qualIndex"] = i.ToString();
                        ddlQualification.SelectedIndex = i; break;
                    }
                }
            }
            if (ProfileDetails1.First().Age != null)
                txtAge.Text = ProfileDetails1.First().Age.ToString();
            if (ProfileDetails1.First().Gender != null)
                ddlGender.SelectedValue = ProfileDetails1.First().Gender.ToString();
            if (ProfileDetails1.First().EmailId != null)
                txtEmailId.Text = ProfileDetails1.First().EmailId.ToString();

            if (ProfileDetails1.First().PhoneNum != null)
                txtPhoneNumber.Text = ProfileDetails1.First().PhoneNum.ToString();

        }      

    }
    private Boolean ValidateMandatory()
    {
        Boolean result = true;
        //txtJob.Text == "" ||
        if (txtFsName.Text == "" || txtLstName.Text.Trim()=="" || txtAge.Text.Trim()=="" ||  ddlJobCatgy.SelectedIndex <= 0 || (txtEduQual.Text == "" && (ddlQualification.SelectedValue=="0" || ddlQualification.SelectedValue=="00")))
            result = false;
        return result;
    }
    private void ClearControls()
    {
        txtFsName.Text = "";
        txtLstName.Text = "";
        txtMidName.Text = "";

        txtJob.Text = "";
        ddlOrg.SelectedIndex = 0;
        ddlJobCatgy.SelectedIndex = 0;
        ddlGrpUser.SelectedIndex = 0;
        ddlIndustry.SelectedIndex = 0;
        //txtPresJobExp.Text = "";
        txtProffQual.Text = "";
        //txtTotalExp.Text = "";
        txtEduQual.Text = "";
        txtAge.Text = "0";

        ddlQualification.SelectedIndex = 0;
        ddlTotExpMonths.SelectedIndex = 0;
        ddlTotExpYears.SelectedIndex = 0;
        ddlCurExpMonths.SelectedIndex = 0;
        ddlCurExpYears.SelectedIndex = 0;


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }
    //protected void ddlGrpUser_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if(ddlGrpUser.SelectedIndex > 0)
    //        if (Session["Grpindex"] != null)
    //    if (Session["GrpIndex"].ToString())
    //        if(Session["GrpIndex"].ToString() ! =ddlGrpUser.SelectedIndex)



    //}

    protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtOrganization.Visible = false; 
        if (ddlOrg.SelectedIndex > 0)
        {
            
            if (Session["Orgindex"] != null)
            {
                if (Session["OrgIndex"].ToString() != ddlOrg.SelectedIndex.ToString())
                {
                    
                    // Session["OrgValue"] = ddlOrg.SelectedValue;
                    fillprofiles();

                }
            }
            else
            {
                Session["OrgIndex"] = ddlOrg.SelectedIndex;
                fillprofiles();
            }
            if (ddlOrg.SelectedIndex == ddlOrg.Items.Count - 1)
            {               
                txtOrganization.Visible = true; 
            }

        }
        else Session["OrgIndex"] = null;
    }

    private void fillComboValues()
    {
        // int orgvalue = 0;
        ListItem listnew;
        listnew = new ListItem("--select--", "0");
        ddlOrg.Items.Clear();
        ddlOrg.Items.Add(listnew);
        int orgIndex = 0;

        if (Session["OrgIndex"] != null)
        {
            orgIndex = int.Parse(Session["OrgIndex"].ToString());
        }

        ddlOrg.DataSource = LinqDataSource1;
        ddlOrg.DataTextField = "Name";
        ddlOrg.DataValueField = "OrganizationID";       
        ddlOrg.DataBind();
         listnew = new ListItem("-- other --", "00");
        ddlOrg.Items.Add(listnew);
        ddlOrg.SelectedIndex = orgIndex;
        if (ddlOrg.SelectedValue == "00")
            txtOrganization.Visible = true;

        //listnew = new ListItem("--select--", "0");
        //ddlGrpUser.Items.Clear();
        //ddlGrpUser.Items.Add(listnew);

        if (orgIndex > 0)
        {
            fillprofiles();
        }

        listnew = new ListItem("--select--", "0");
        ddlQualification.Items.Clear();
        ddlQualification.Items.Add(listnew);
        ddlQualification.DataSource = LinqQualifications;
        ddlQualification.DataTextField = "Qualification1";
        ddlQualification.DataValueField = "Qualificationid";
        ddlQualification.DataBind();
        listnew = new ListItem("--other--", "00");
        ddlQualification.Items.Add(listnew);
        if (Session["qualIndex"] != null)
        {
            ddlQualification.SelectedIndex = int.Parse(Session["qualIndex"].ToString());
            if (ddlQualification.SelectedValue == "00")
                txtEduQual.Visible = true;
        }
        int industryindex=0;
        if(Session["industryindex"] != null)
            industryindex=int.Parse(Session["industryindex"].ToString());
        listnew = new ListItem("--select--", "0");
        ddlIndustry.Items.Clear();
        ddlIndustry.Items.Add(listnew);
        ddlIndustry.DataSource = LinqDataSource2;
        ddlIndustry.DataTextField = "Name";
        ddlIndustry.DataValueField = "IndustryID";
        ddlIndustry.DataBind();
        listnew = new ListItem("--other--", "00");
        ddlIndustry.Items.Add(listnew);
        ddlIndustry.SelectedIndex = industryindex;
        if (ddlIndustry.SelectedValue == "00")
            txtIndustry.Visible = true;
    }
    private void fillprofiles()
    {
        //if (orgIndex > 0)
        //orgIndex = int.Parse(Session["OrgIndex"].ToString());
        //
        int grpIndex = 0;
        if (Session["grpindex"] != null)
            grpIndex = int.Parse(Session["grpindex"].ToString());
        ListItem listnew = new ListItem("--select--", "0");
        ddlGrpUser.Items.Clear();
        ddlGrpUser.Items.Add(listnew);
        if (ddlOrg.SelectedValue != "00" && ddlOrg.SelectedIndex > 0)
        {
            GrpUserLinqDataSource.Where = "OrganizationID = " + ddlOrg.SelectedValue;
            ddlGrpUser.DataSource = GrpUserLinqDataSource;
            ddlGrpUser.DataTextField = "GroupName";
            ddlGrpUser.DataValueField = "GroupUserID";
            ddlGrpUser.DataBind();
        }
        //listnew = new ListItem("-- other --", "00");
        //ddlGrpUser.Items.Add(listnew);
        if (grpIndex > 0)
            ddlGrpUser.SelectedIndex = grpIndex;
        //if (ddlGrpUser.SelectedValue == "00")
        //    txtUserGroup.Visible = true;
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session["SubCtrl"] = null;
        //Session["MasterCtrl"] = "~/MasterPage.master";
        Response.Redirect("FJAHome.aspx");
    }
    protected void ddlQualification_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtEduQual.Visible = false;
        Session["qualIndex"] = ddlQualification.SelectedIndex.ToString();
        if (ddlQualification.SelectedValue == "00")
            txtEduQual.Visible = true;
    }
    protected void ddlGrpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrg.SelectedIndex > 0)
        {
            Session["grpindex"] = ddlGrpUser.SelectedIndex.ToString();
            
            //if (ddlGrpUser.SelectedIndex == ddlGrpUser.Items.Count - 1)
            //{
            //    txtUserGroup.Visible = true;
            //}
        }
        else Session["grpindex"] = null;
    }
    protected void ddlIndustry_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtIndustry.Visible = false;
        if (ddlOrg.SelectedIndex > 0)
        {
            Session["industryindex"] = ddlIndustry.SelectedIndex.ToString();

            if (ddlIndustry.SelectedIndex == ddlIndustry.Items.Count - 1)
            {
                txtIndustry.Visible = true;
            }
        }
        else Session["industryindex"] = null;
    }
}