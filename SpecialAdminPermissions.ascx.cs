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

public partial class SpecialAdminPermissions : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    int createdby = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["UserID"] != null)
        {
            createdby = int.Parse(Session["UserID"].ToString());
        }
        BindGrid();
    }
    protected void ddlOrganizations_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = ddlOrganizations.SelectedIndex;
        if (Session["orgIndex_type"] != null)
            if (Session["orgIndex_type"].ToString() == index.ToString())
                return;
        //UncheckGrid();
        //BindGrid(); 
        Session["orgIndex_type"] = index; Session["admIndex_type"] = null; checkQuestionTypes(); UncheckGrid();
    }
    protected void ddlAdminList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = ddlAdminList.SelectedIndex;
        if (Session["admIndex_type"] != null)
            if (Session["admIndex_type"].ToString() == index.ToString())
                return;
        //UncheckGrid();
        //BindGrid(); 
        Session["admIndex_type"] = index; BindGrid();
    }
    private void UncheckGrid()
    {
        if (gvwUserPermissions.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gvwUserPermissions.Rows)
            {
                CheckBox cb = new CheckBox();
                cb = (CheckBox)gr.Controls[0].FindControl("CheckBox1");
                cb.Checked = false; 
               
            }
        }
    }
    private void BindGrid()
    {
        int userid = 0;
        if (ddlAdminList.SelectedIndex > 0)
            userid = int.Parse(ddlAdminList.SelectedValue);

        if (userid > 0)
        {            

            var permissions = from userpermissiondet in dataclass.UserPermissions
                              where userpermissiondet.UserId == userid
                              select userpermissiondet;
            
            int menuid = 0;
            int i = 0;
            if (gvwUserPermissions.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvwUserPermissions.Rows)
                {
                    CheckBox cb = new CheckBox();
                    cb = (CheckBox)gr.Controls[0].FindControl("CheckBox1");
                    menuid = int.Parse(gvwUserPermissions.Rows[i].Cells[1].Text);
                    
                    if (permissions.Count() > 0)
                        foreach (var assignPermissions in permissions)
                        {
                            if (assignPermissions.MenuId == menuid)
                            { cb.Checked = true; break; }
                        }

                    i++;
                }
            }            
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        if (ddlAdminList.SelectedIndex == 0 || ddlOrganizations.SelectedIndex == 0)
        { lblMessage.Text = "Please select organization/Admin"; return; }
        
        bool assignstatus = false;
        
        int userid = int.Parse(ddlAdminList.SelectedValue);

        dataclass.Procedure_DeletUserPermissions(userid);
        int i = 0;
        int menuid = 0;
        foreach (GridViewRow gr in gvwUserPermissions.Rows)
        {
            menuid = int.Parse(gvwUserPermissions.Rows[i].Cells[1].Text);
            CheckBox cb = new CheckBox();
            cb = (CheckBox)gr.Controls[0].FindControl("CheckBox1");
            if (cb.Checked)
            {
                assignstatus = true;
                dataclass.Procedure_UserPermissions(userid, menuid, createdby);
            }

            i = i + 1;
        } Session["admIndex_type"] = null; Session["orgIndex_type"] = null;
        if (assignstatus == true)
        {lblMessage.Text = "permission(s) saved Successfully";resetValues(0);}
        else lblMessage.Text = "no permission(s) assign for the selected organization admin";
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlAdminList.SelectedIndex > 0)
        {
            int userid = int.Parse(ddlAdminList.SelectedValue);
            dataclass.Procedure_DeletUserPermissions(userid);
            //dataclass.Procedure_DeleteOrganizationQuestionTypes(userid);
            lblMessage.Text = "deleted Successfully";
            resetValues(0);
        }
        else lblMessage.Text = "please select an Admin";
    }
    private void resetValues(int index)
    {
        UncheckGrid();
        ddlAdminList.SelectedIndex=0;        
        if (index == 1)
        {
            ddlAdminList.Items.Clear();
            ddlAdminList.Items.Add("-- select --");
            chblQuestionTypes.ClearSelection();
            ddlOrganizations.SelectedIndex = 0;
            Session["orgIndex_type"] = null;
            Session["admIndex_type"] = null;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        resetValues(0);
    }
    protected void btnAssign_type_Click(object sender, EventArgs e)
    {
        if (ddlOrganizations.SelectedIndex > 0)
        {
            bool isAssigned = false;
            int orgid = int.Parse(ddlOrganizations.SelectedValue);
            dataclass.Procedure_DeleteOrganizationQuestionTypes(orgid);
            for (int i = 0; i < chblQuestionTypes.Items.Count; i++)
            {
                if (chblQuestionTypes.Items[i].Selected == true)
                { isAssigned = true; dataclass.Procedure_OrganizationQuestionTypes(orgid, chblQuestionTypes.Items[i].Value, chblQuestionTypes.Items[i].Text, createdby); }

            } Session["orgIndex_type"] = null;
            if(isAssigned==true)
            lblMessage_type.Text = "Question type(s) assigned successfully."; 
            else lblMessage_type.Text = "Please select question type(s)."; 
        }else lblMessage_type.Text = "Please select Organization."; 
    }
    protected void btnDelete_type_Click(object sender, EventArgs e)
    {
        if (ddlOrganizations.SelectedIndex > 0)
        {
            int orgid = int.Parse(ddlOrganizations.SelectedValue);
            dataclass.Procedure_DeleteOrganizationQuestionTypes(orgid);
            chblQuestionTypes.ClearSelection();
            lblMessage_type.Text = "Question types deleted successfully."; 
        }else lblMessage_type.Text = "Please select Organization."; 
    }
    protected void btnReset_type_Click(object sender, EventArgs e)
    {
        resetValues(1);
    }
    private void checkQuestionTypes()
    {
        chblQuestionTypes.ClearSelection();
        if (ddlOrganizations.SelectedIndex > 0)
        {
            int orgid = int.Parse(ddlOrganizations.SelectedValue);
            var orgQuestionPermissions = from quesPermissiondet in dataclass.OrganizationQuestionTypes
                                         where quesPermissiondet.OrganizationId == orgid
                                         select quesPermissiondet;
            if (orgQuestionPermissions.Count() > 0)
            {
                for (int i = 0; i < chblQuestionTypes.Items.Count; i++)
                {
                    foreach (var quesTypes in orgQuestionPermissions)
                    {
                        if (quesTypes.QuestionTypeName == chblQuestionTypes.Items[i].Value)
                        { chblQuestionTypes.Items[i].Selected = true; break;}
                    }
                }
            }
        }
    }


}
