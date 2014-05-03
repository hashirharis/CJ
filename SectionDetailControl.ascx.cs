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

public partial class SectionDetailControl : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    bool specialadmin = false;
    int OrganizationID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "SpecialAdmin")
        {            
            specialadmin = true; OrganizationID = int.Parse(Session["AdminOrganizationID"].ToString());            
        }
        //TODO following method calling will change after assign section categories to existing sections
        //FillSectionlist(0);
        FillSectionCategory();
    }

    protected void ddlSectionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSectionName.SelectedIndex > 0)
        {
            // txtSectionName.Visible = true;
            if (ddlSectionName.SelectedValue != "0" && ddlSectionName.SelectedValue != "00")
            {
                ResetFirstLevelVar(); ResetSecondLevelVar();
                txtSectionName.Text = ddlSectionName.SelectedItem.Text;
                Session["sectionIndex"] = ddlSectionName.SelectedIndex;
                FillSubLevel1Sections();
            }
            else
            {
                pnlSublevel1.Enabled = false; pnlsublevel2.Enabled = false;
                txtSectionName2.Text = ""; ResetFirstLevelVar(); ResetSecondLevelVar();//txtSectionName.Text = "";
            }

        }
        else
        {
            Session["SecName"] = null; pnlSublevel1.Enabled = false; txtSectionName.Text = "";
            pnlsublevel2.Enabled = false; ResetFirstLevelVar(); ResetSecondLevelVar();
        }
    }
    private bool CheckNameDuplication(string secname)
    {
        bool nameexists = false;
        var details1 = from details in dataclasses.SectionDetails
                       where details.SectionName == secname && details.ParentId==int.Parse(ddlFirstLevelVarList.SelectedValue)
                       select details;
        if (details1.Count() > 0)
        { lblMessage.Text = "Name duplication,enter new values"; nameexists = true; }

        return nameexists;
    }
    private bool CheckFistLevalNameDuplication(string secname)
    {
        bool nameexists = false;
        var details1 = from details in dataclasses.SectionDetails
                       where details.SectionName == secname && details.ParentId == int.Parse(ddlSectionName.SelectedValue)
                       select details;
        if (details1.Count() > 0)
        { lblMessage.Text = "Name duplication,enter new values"; nameexists = true; }

        return nameexists;
    }
    private bool CheckSectionNameDuplication(string secname)
    {
        bool nameexists = false;
        var details1 = from details in dataclasses.SectionDetails
                       where details.SectionName == secname && details.ParentId == 0
                       select details;
        if (details1.Count() > 0)
        { lblMessage.Text = "Name duplication,enter new values"; nameexists = true; }

        return nameexists;
    }

    private void FillSectionCategory()
    {
        ListItem listname;
        int sectionCategoryId = 0;
        int sectionCategoryIndex = 0;
        if (Session["sectionCategoryIndex"] != null)
            sectionCategoryIndex = int.Parse(Session["sectionCategoryIndex"].ToString());
        listname = new ListItem("-- Select Section Category --", "0");
        ddlSectionCategory.Items.Clear();
        ddlSectionCategory.Items.Add(listname);
        ddlSectionCategory.DataSource = sectionCategoryDataSource;
        ddlSectionCategory.DataTextField = "SectionCategoryName";
        ddlSectionCategory.DataValueField = "SectionCategoryId";
        ddlSectionCategory.DataBind();
        if (sectionCategoryIndex > 0)
        {
            ddlSectionCategory.SelectedIndex = sectionCategoryIndex;
            sectionCategoryId = int.Parse(ddlSectionCategory.SelectedValue);

            FillSectionlist(sectionCategoryId);
        }
    }

    private void FillSectionlist(int sectionCategoryId)
    {
        try
        {
            ListItem listname;
            ListItem list1;
            list1 = new ListItem("-- Add new --", "00");
           
            listname = new ListItem("-- select --", "0");
            ddlSectionName.Items.Clear();
            ddlSectionName.Items.Add(listname);
            int sectionIndex = 0;

            if (Session["sectionIndex"] != null)
                sectionIndex = int.Parse(Session["sectionIndex"].ToString());

            if (specialadmin == true)
            {
                if (sectionCategoryId > 0)
                {
                    var details1 = from details in dataclasses.SectionDetails
                                   where details.ParentId == 0 && details.OrganizationId == OrganizationID && details.SectionCategoryId == sectionCategoryId
                                   orderby details.SectionName ascending
                                   select details;
                    if (details1.Count() > 0)
                    {
                        ddlSectionName.DataSource = details1;
                        ddlSectionName.DataTextField = "SectionName";
                        ddlSectionName.DataValueField = "SectionId";
                        ddlSectionName.DataBind();
                    }
                }
                else
                {
                    var details1 = from details in dataclasses.SectionDetails
                                   where details.ParentId == 0 && details.OrganizationId == OrganizationID
                                   orderby details.SectionName ascending
                                   select details;
                    if (details1.Count() > 0)
                    {
                        ddlSectionName.DataSource = details1;
                        ddlSectionName.DataTextField = "SectionName";
                        ddlSectionName.DataValueField = "SectionId";
                        ddlSectionName.DataBind();
                    }
                }

            }
            else
            {
                if (sectionCategoryId > 0)
                {
                    var details1 = from details in dataclasses.SectionDetails
                                   where details.ParentId == 0 && details.AdminAccess == 1 && details.SectionCategoryId == sectionCategoryId
                                   orderby details.SectionName ascending
                                   select details;
                    if (details1.Count() > 0)
                    {
                        ddlSectionName.DataSource = details1;
                        ddlSectionName.DataTextField = "SectionName";
                        ddlSectionName.DataValueField = "SectionId";
                        ddlSectionName.DataBind();
                    }

                }
                else
                {
                    var details1 = from details in dataclasses.SectionDetails
                                   where details.ParentId == 0 && details.AdminAccess == 1
                                   orderby details.SectionName ascending
                                   select details;
                    if (details1.Count() > 0)
                    {
                        ddlSectionName.DataSource = details1;
                        ddlSectionName.DataTextField = "SectionName";
                        ddlSectionName.DataValueField = "SectionId";
                        ddlSectionName.DataBind();
                    }
                }
            }
            ddlSectionName.Items.Add(list1);
            ddlSectionName.SelectedIndex = sectionIndex;
            if (sectionIndex > 0)
            {
                FillSubLevel1Sections();
            }
            else
            {
                list1 = new ListItem("-- Add new --", "00");
                listname = new ListItem("-- select --", "0");
                ddlFirstLevelVarList.Items.Clear();
                ddlFirstLevelVarList.Items.Add(listname);
                ddlFirstLevelVarList.Items.Add(list1);

                list1 = new ListItem("-- Add new --", "00");
                listname = new ListItem("-- select --", "0");
                ddlSecondLevelVarList.Items.Clear();
                ddlSecondLevelVarList.Items.Add(listname);
                ddlSecondLevelVarList.Items.Add(list1);
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    private void FillSubLevel1Sections()
    {
        ListItem listname;
        ListItem list1;
        list1 = new ListItem("-- Add new --", "00");
        listname = new ListItem("-- select --", "0");
        ddlFirstLevelVarList.Items.Clear();
        ddlFirstLevelVarList.Items.Add(listname);
        pnlSublevel1.Enabled = true;
        int parentindex = 0;
        if (Session["SubLevel1Index"] != null)
            parentindex = int.Parse(Session["SubLevel1Index"].ToString());

        var details1 = from details in dataclasses.SectionDetails
                       where details.ParentId == int.Parse(ddlSectionName.SelectedValue)
                       orderby details.SectionName ascending
                       select details;
        if (details1.Count() > 0)
        {
            ddlFirstLevelVarList.DataSource = details1;
            ddlFirstLevelVarList.DataTextField = "SectionName";
            ddlFirstLevelVarList.DataValueField = "SectionId";
            ddlFirstLevelVarList.DataBind();
        }
        ddlFirstLevelVarList.Items.Add(list1);
        ddlFirstLevelVarList.SelectedIndex = parentindex;
        if (parentindex > 0)
        {
            FillSubLevel2Sections();
        }
        else
        {
            list1 = new ListItem("-- Add new --", "00");
            listname = new ListItem("-- select --", "0");
            ddlSecondLevelVarList.Items.Clear();
            ddlSecondLevelVarList.Items.Add(listname);
            ddlSecondLevelVarList.Items.Add(list1);
        }
    }

    private void FillSubLevel2Sections()
    {
        
        ListItem listname;
        ListItem list1;
        list1 = new ListItem("-- Add new --", "00");
        listname = new ListItem("-- select --", "0");
        ddlSecondLevelVarList.Items.Clear();
        ddlSecondLevelVarList.Items.Add(listname);
        pnlsublevel2.Enabled = true;
        int subindex = 0;
        if (Session["SubLevel2Index"] != null)
            subindex = int.Parse(Session["SubLevel2Index"].ToString());

        var details1 = from details in dataclasses.SectionDetails
                       where details.ParentId == int.Parse(ddlFirstLevelVarList.SelectedValue)
                       orderby details.SectionName ascending
                       select details;
        if (details1.Count() > 0)
        {
            ddlSecondLevelVarList.DataSource = details1;
            ddlSecondLevelVarList.DataTextField = "SectionName";
            ddlSecondLevelVarList.DataValueField = "SectionId";
            ddlSecondLevelVarList.DataBind();
        }
            ddlSecondLevelVarList.Items.Add(list1);
            //if (subindex > 0)
            //{
                ddlSecondLevelVarList.SelectedIndex = subindex;
            //}else 
        
    }   

    protected void btnAddSections_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtSectionName.Text == "")
                lblMessage.Text = "Enter sectionName/briefName values";
            else if (ddlSectionCategory.SelectedIndex <= 0)
            {
                lblMessage.Text = "Please select sectioncategory ";
            }
            else
            {
                if (CheckSectionNameDuplication(txtSectionName.Text.Trim()) == false)
                {

                    int sectionCategoryId = int.Parse(ddlSectionCategory.SelectedValue);
                    int sectionid = 0;
                    int userid = 0;
                    int status = 0;
                    int parentid = 0;

                    if (ddlSectionName.SelectedIndex > 0 && ddlSectionName.SelectedValue != "00")
                        sectionid = int.Parse(ddlSectionName.SelectedValue);

                    userid = int.Parse(Session["userid"].ToString());
                    status = int.Parse(ddlStatus.SelectedValue);
                    int adminaccess = 1;
                    if (specialadmin == true) adminaccess = 0;
                    dataclasses.AddSectionDetail(sectionid, txtSectionName.Text.Trim(), parentid, status, userid, adminaccess, OrganizationID, sectionCategoryId);
                    if (sectionid > 0)
                        dataclasses.Procedure_UpdateSectionNameForAll(txtSectionName.Text, ddlSectionName.SelectedItem.Text);
                    lblMessage.Text = "value saved"; ResetSections();
                    FillSectionlist(sectionCategoryId);
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "error: " + ex.Message;
        }
    }

    private void ResetSections()
    {
        txtSectionName.Text = "";//ddlSectionName.SelectedIndex = 0;        
        pnlSublevel1.Enabled = false; pnlsublevel2.Enabled = false; Session["sectionIndex"] = null;
    }
    protected void btnResetSections_Click(object sender, EventArgs e)
    {
        txtSectionName.Text = "";
        ddlSectionName.SelectedIndex = 0;
        pnlSublevel1.Enabled = false; pnlsublevel2.Enabled = false; Session["sectionIndex"] = null;
    }
    protected void btnDeleteSection_Click(object sender, EventArgs e)
    {
        int secid=0;
        if (ddlSectionName.SelectedValue != "0" && ddlSectionName.SelectedValue != "00")
        {
            
            secid = int.Parse(ddlSectionName.SelectedValue);
            dataclasses.DeleteSectionDetail(secid);

            // code to delete all entries related to selected variable // bip 28-12-2009
            var firstlevelvariablelist = from firstlevelvardet in dataclasses.SectionDetails
                                         where firstlevelvardet.ParentId == secid
                                         select firstlevelvardet;
            foreach (var firstlevelvarids in firstlevelvariablelist)
            {
                int firstlevelid = firstlevelvarids.SectionId;
                dataclasses.DeleteSectionDetailWithParentId(firstlevelid);
               // dataclasses.DeleteSectionDetail(firstlevelid);
            }
            dataclasses.DeleteSectionDetailWithParentId(secid);
            dataclasses.Procedure_DeleteDataRelatedToVariable(secid, 0, 0, ddlSectionName.SelectedItem.Text, "", "");
            lblMessage.Text = "Deletion Succesfull";
        }
        else lblMessage.Text = "Please select a section for deletion";
    }
    protected void btnAddSubUnderSections_Click(object sender, EventArgs e)
    {
        pnlSublevel1.Enabled = true;
        ResetFirstLevelVar(); ResetSecondLevelVar();
        FillSubLevel1Sections();
    }
    protected void btnAddFirstLevelVar_Click(object sender, EventArgs e)
    {
        if (ddlSectionName.SelectedIndex <= 0) { lblMessageSubSec1.Text = "Please select a section from the list"; return; }
        if (txtParentName.Text == "")
            lblMessageSubSec1.Text = "Enter 1st Level Variable name";
        else
            if (CheckFistLevalNameDuplication(txtParentName.Text.Trim()) == false)
            {
                int sectionid = 0;
                int userid = 0;
                int status = 0;
                int parentid = 0;
                if (ddlSectionName.SelectedIndex > 0)
                    parentid = int.Parse(ddlSectionName.SelectedValue);
                if (ddlFirstLevelVarList.SelectedIndex > 0 && ddlFirstLevelVarList.SelectedValue != "00")
                    sectionid = int.Parse(ddlFirstLevelVarList.SelectedValue);

                userid = int.Parse(Session["userid"].ToString());
                status = int.Parse(ddlStatusSub1.SelectedValue);
                int adminaccess = 1;
                if (specialadmin == true) adminaccess = 0;
                dataclasses.AddSectionDetail(sectionid, txtParentName.Text, parentid, status, userid,adminaccess,OrganizationID,0);
                if (sectionid > 0)
                    dataclasses.Procedure_UpdateSectionNameSub1ForAll(ddlSectionName.SelectedItem.Text, txtParentName.Text, ddlFirstLevelVarList.SelectedItem.Text);
                
                lblMessageSubSec1.Text = "Saved Successfully ...";
                ResetFirstLevelVar();
                FillSubLevel1Sections();
            }
    }
    private void ResetFirstLevelVar()
    {
        pnlSublevel1.Enabled = true; Session["SubLevel1Index"] = null;txtParentName.Text = "";
        //ddlFirstLevelVarList.SelectedIndex = 0; 
        pnlsublevel2.Enabled = false; ddlSecondLevelVarList.SelectedIndex = 0; txtSectionName2.Text = "";
    }
    protected void btnResetFirstLevelVar_Click(object sender, EventArgs e)
    {
        ResetFirstLevelVar();
    }
    protected void btnDeleteFirstLevelVar_Click(object sender, EventArgs e)
    {
        int secid = 0;
        if (ddlFirstLevelVarList.SelectedValue != "0" && ddlFirstLevelVarList.SelectedValue != "00")
        {
            secid = int.Parse(ddlFirstLevelVarList.SelectedValue);
            dataclasses.DeleteSectionDetail(secid);
            // bip 28-12-2009
            dataclasses.DeleteSectionDetailWithParentId(secid);
            int variableid = int.Parse(ddlSectionName.SelectedValue);
            dataclasses.Procedure_DeleteDataRelatedToVariable(variableid, secid, 0, ddlSectionName.SelectedItem.Text, ddlFirstLevelVarList.SelectedItem.Text, "");
        }
        else lblMessageSubSec1.Text = "Please select a value for deletion";
    }
    protected void btnAddSubLevelUnderFirstLevel_Click(object sender, EventArgs e)
    {
        if (ddlFirstLevelVarList.SelectedIndex <= 0) { lblMessage.Text = "Please select a section from the list"; return; }
       
        pnlsublevel2.Enabled = true;
        ResetSecondLevelVar();
    }
    protected void btnSubmitSecondLevelVar_Click(object sender, EventArgs e)
    {
        if (ddlFirstLevelVarList.SelectedIndex <= 0) { lblMessageSubSec2.Text = "Please select a section from the list"; return; }
        if (txtSectionName2.Text == "")
            lblMessageSubSec2.Text = "Enter the values";
        else
            if (CheckNameDuplication(txtSectionName2.Text.Trim()) == false)
            {
                int sectionid = 0;
                int userid = 0;
                int status = 0;
                int parentid = 0;

                if (ddlFirstLevelVarList.SelectedIndex > 0)
                    parentid = int.Parse(ddlFirstLevelVarList.SelectedValue);
                if (ddlSecondLevelVarList.SelectedIndex > 0 && ddlSecondLevelVarList.SelectedValue!="00")
                    sectionid = int.Parse(ddlSecondLevelVarList.SelectedValue);

                userid = int.Parse(Session["userid"].ToString());
                status = int.Parse(ddlStatusSub1.SelectedValue);
                int adminaccess = 1;
                if (specialadmin == true) adminaccess = 0;
                dataclasses.AddSectionDetail(sectionid, txtSectionName2.Text, parentid, status, userid,adminaccess,OrganizationID,0);
                if (sectionid > 0)
                    dataclasses.Procedure_UpdateSectionNameSub2ForAll(ddlSectionName.SelectedItem.Text, ddlFirstLevelVarList.SelectedItem.Text, txtSectionName2.Text, ddlSecondLevelVarList.SelectedItem.Text);
                
                lblMessageSubSec2.Text = "value saved";
                ResetSecondLevelVar();
                FillSubLevel2Sections();
                
            }
    }

    private void ResetSecondLevelVar()
    {
        Session["SubLevel2Index"] = null; //ddlSecondLevelVarList.SelectedIndex = 0; 9447755333
        txtSectionName2.Text = "";
    }
    protected void btnRestSecondLevelVar_Click(object sender, EventArgs e)
    {
        ResetSecondLevelVar();
    }
    protected void btnDeleteSecondLevelVar_Click(object sender, EventArgs e)
    {
        int secid = 0;
        if (ddlSecondLevelVarList.SelectedValue != "0" && ddlSecondLevelVarList.SelectedValue != "00")
        {
           
            secid = int.Parse(ddlSecondLevelVarList.SelectedValue);
            dataclasses.DeleteSectionDetail(secid);
            int variableid = int.Parse(ddlSectionName.SelectedValue);
            int firstvariableid = int.Parse(ddlFirstLevelVarList.SelectedValue);
            dataclasses.Procedure_DeleteDataRelatedToVariable(variableid, firstvariableid, secid, ddlSectionName.SelectedItem.Text, ddlFirstLevelVarList.SelectedItem.Text, ddlSecondLevelVarList.SelectedItem.Text);
            lblMessageSubSec2.Text = "Deletion successfull";
        }
        else lblMessageSubSec2.Text = "Please select a value for deletion";

    }
    protected void ddlSecondLevelVarList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SubLevel2Index"] = null;
        if (ddlSecondLevelVarList.SelectedIndex > 0 && ddlSecondLevelVarList.SelectedValue != "00")
        {
            Session["SubLevel2Index"] = ddlSecondLevelVarList.SelectedIndex;
            txtSectionName2.Text = ddlSecondLevelVarList.SelectedItem.Text;
        }
    }
    protected void ddlFirstLevelVarList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Prntnme = 0;
        if (ddlFirstLevelVarList.SelectedIndex > 0)
        {
               // txtParentName.Visible = true;
                if (ddlFirstLevelVarList.SelectedValue != "0" && ddlFirstLevelVarList.SelectedValue != "00")
                {
                    ResetSecondLevelVar();
                    txtParentName.Text = ddlFirstLevelVarList.SelectedItem.Text;
                    Session["SubLevel1Index"] = ddlFirstLevelVarList.SelectedIndex;
                    FillSubLevel2Sections();
                }
                else
                { pnlsublevel2.Enabled = false; txtSectionName2.Text = ""; ResetSecondLevelVar(); }
        }
        else {pnlsublevel2.Enabled = false; txtSectionName2.Text = "";ResetSecondLevelVar();}
    }
    protected void ddlSectionCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSectionCategory.SelectedIndex > 0)
        {
            // txtSectionName.Visible = true;
            if (ddlSectionCategory.SelectedValue != "0")
            {
                if(Session["sectionCategoryIndex"]!=null)
                    if (ddlSectionCategory.SelectedValue != Session["sectionCategoryIndex"].ToString())
                    {
                        txtSectionName.Text = "";
                         Session["sectionIndex"] = null; 
                         ResetFirstLevelVar(); ResetSecondLevelVar();
                    }
                Session["sectionCategoryIndex"] = ddlSectionCategory.SelectedIndex;
                int sectionCategoryId = int.Parse(ddlSectionCategory.SelectedValue);
                FillSectionlist(sectionCategoryId);
            }
            else
            {
                pnlSublevel1.Enabled = false; pnlsublevel2.Enabled = false;
                txtSectionName2.Text = ""; ResetFirstLevelVar(); ResetSecondLevelVar();
            }
        }
        else
        {
            Session["sectionIndex"] = null; 
            Session["SecName"] = null; pnlSublevel1.Enabled = false; txtSectionName.Text = "";
            pnlsublevel2.Enabled = false; ResetFirstLevelVar(); ResetSecondLevelVar();
        }
    }
}

