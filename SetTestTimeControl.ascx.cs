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

public partial class SetTestTimeControl : System.Web.UI.UserControl
{
    DBManagementClass clsClasses = new DBManagementClass();
    AssesmentDataClassesDataContext assesDataClasses = new AssesmentDataClassesDataContext();

    bool specialadmin = false;
    int OrganizationID = 0;
    string organizationName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "SpecialAdmin")
        {
            lblOrganization.Visible = false; ddlOrganization.Visible = false;
            specialadmin = true; OrganizationID = int.Parse(Session["AdminOrganizationID"].ToString());

            if (Session["adminOrgName"] != null)
                organizationName = Session["adminOrgName"].ToString();
            else
            {
                var orgName = from orgDet in assesDataClasses.Organizations
                              where orgDet.OrganizationID == OrganizationID
                              select orgDet;
                if (orgName.Count() > 0)
                { organizationName = orgName.First().Name.ToString(); Session["adminOrgName"] = organizationName; }

            }
            FillTestList();
        }
        else
            FillOrganizationList();
    }
    
   private void FillOrganizationList()
   {
       ListItem listnew;
       listnew = new ListItem("--select--", "0");
       ddlOrganization.Items.Clear();
       ddlOrganization.Items.Add(listnew);
       int orgIndex = 0;

       if (Session["OrgIndex_Timer"] != null)
       {
           orgIndex = int.Parse(Session["OrgIndex_Timer"].ToString());
       }
        
       ddlOrganization.DataSource = LinqOrganization;
       ddlOrganization.DataTextField = "Name";
       ddlOrganization.DataValueField = "OrganizationID";
       ddlOrganization.DataBind();
       if (orgIndex > 0)
       { ddlOrganization.SelectedIndex = orgIndex; FillTestList(); }
       else
       {
           listnew = new ListItem("--select--", "0");
           ddlTestName.Items.Clear();
           ddlTestName.Items.Add(listnew);

           listnew = new ListItem("-- Select --", "0");
           ddlTestSectionName.Items.Clear();
           ddlTestSectionName.Items.Add(listnew);

           listnew = new ListItem("-- Select --", "0");
           ddlVariableList.Items.Clear();
           ddlVariableList.Items.Add(listnew);

       }
   }

   private void FillTestList()
   {
       ListItem listnew;
       listnew = new ListItem("--select--", "0");
       ddlTestName.Items.Clear();
       ddlTestName.Items.Add(listnew);
       int testIndex = 0;

       if (Session["TestIndex_Timer"] != null)
       {
           testIndex = int.Parse(Session["TestIndex_Timer"].ToString());
       }
       if(specialadmin==false)
           organizationName=ddlOrganization.SelectedItem.Text;

       var testTimeDetails = from testTimedet in assesDataClasses.TestLists
                             where testTimedet.OrganizationName == organizationName
                             select testTimedet;
       ddlTestName.DataSource = testTimeDetails;
       ddlTestName.DataTextField = "TestName";
       ddlTestName.DataValueField = "TestId";
       ddlTestName.DataBind();
       if (testIndex > 0)
       { ddlTestName.SelectedIndex = testIndex; FillTestsectionList(); } //BindGrid();} // bip 220110
       else
       {
           listnew = new ListItem("-- Select --", "0");
           ddlTestSectionName.Items.Clear();
           ddlTestSectionName.Items.Add(listnew);
           listnew = new ListItem("-- Select --", "0");
           ddlVariableList.Items.Clear();
           ddlVariableList.Items.Add(listnew);

           gvwTimeDetails.DataSource = "";
           gvwTimeDetails.DataBind();
       }
       if (testIndex == 0)
           FillTimeDetails();// 24-02-2010 bip
   }

   private void FillTestsectionList()
   {
       ListItem litem = new ListItem("-- Select --", "0");
       ddlTestSectionName.Items.Clear();
       ddlTestSectionName.Items.Add(litem);
       int tessecIndex = 0;
       if (ddlTestName.SelectedIndex > 0)
       {
          
           if (Session["TestsectionIndex_Timer"] != null)
               tessecIndex = int.Parse(Session["TestsectionIndex_Timer"].ToString());

           LinqTestSection.Where = "TestId=" + ddlTestName.SelectedValue;
           ddlTestSectionName.DataSource = LinqTestSection;
           ddlTestSectionName.DataTextField = "SectionName";
           ddlTestSectionName.DataValueField = "TestSectionId";
           ddlTestSectionName.DataBind();
           if (tessecIndex > 0)
           {
               ddlTestSectionName.SelectedIndex = tessecIndex;
               FillVariableList(0);
           }
           else
           {
               ListItem listnew = new ListItem("-- Select --", "0");
               ddlVariableList.Items.Clear();
               ddlVariableList.Items.Add(listnew);
           }
       }
       else
       {
           ListItem listnew = new ListItem("-- Select --", "0");
           ddlVariableList.Items.Clear();
           ddlVariableList.Items.Add(listnew);
       }
       if (tessecIndex == 0)
           FillTimeDetails();// 22-02-2010 bip
   }

   private void FillVariableList(int index)
   {
       ListItem listname;
       listname = new ListItem("-- select --", "0");
       ddlVariableList.Items.Clear();
       ddlVariableList.Items.Add(listname);
       int sectionIndex = 0;
       if (ddlTestSectionName.SelectedIndex > 0)
       {
           if (Session["variableIndex_timer"] != null)
               sectionIndex = int.Parse(Session["variableIndex_timer"].ToString());

           string querystring = "select distinct SectionName,SectionId from View_FirstLevelSectionList where ParentId=0 and TestId=" + ddlTestName.SelectedValue + " and TestSectionId=" + ddlTestSectionName.SelectedValue;
           DataSet dsFirstLevelSectionLists = new DataSet();
           dsFirstLevelSectionLists = clsClasses.GetValuesFromDB(querystring);
           if (dsFirstLevelSectionLists != null)
           {
               ddlVariableList.DataSource = dsFirstLevelSectionLists.Tables[0];
               ddlVariableList.DataTextField = "SectionName";
               ddlVariableList.DataValueField = "SectionId";
               ddlVariableList.DataBind();
           }

           if (sectionIndex > 0)
           {
               ddlVariableList.SelectedIndex = sectionIndex; //FillTimeDetails();
           }
       }
       if (index == 0)
           FillTimeDetails();
   }

   protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
   {
       if (ddlOrganization.SelectedIndex > 0)
       {
           if (Session["OrgIndex_Timer"] != null)
               if (Session["OrgIndex_Timer"].ToString() != ddlOrganization.SelectedIndex.ToString())
               {Session["TestIndex_Timer"] = null; ddlTestName.SelectedIndex = 0;}

           Session["OrgIndex_Timer"] = ddlOrganization.SelectedIndex.ToString();
           FillTestList();
       }
       else Session["OrgIndex_Timer"] = null;
   }
   protected void ddlTestName_SelectedIndexChanged(object sender, EventArgs e)
   {
       if (ddlTestName.SelectedIndex > 0)
       {
           if (Session["TestIndex_Timer"] != null)
               if (Session["TestIndex_Timer"].ToString() != ddlTestName.SelectedIndex.ToString())
               {Session["TestsectionIndex_Timer"] = null; ddlTestSectionName.SelectedIndex = 0;}

           Session["TestIndex_Timer"] = ddlTestName.SelectedIndex.ToString();} //FillTestsectionList(); //BindGrid();  }//  bip 220110
       else { Session["TestIndex_Timer"] = null;}// Session["TestsectionIndex_Timer"] = null; ddlTestSectionName.SelectedIndex = 0; FillTimeDetails(); }
       FillTestsectionList();
   }
   /*
   private void FillTimeDetails()
   {
       if (ddlTestSectionName.SelectedIndex > 0)
       {
           LinqTimerDetails.Where = "TestId=" + ddlTestName.SelectedValue + " && TestSectionId=" + ddlTestSectionName.SelectedValue;
           gvwTimeDetails.DataSource = LinqTimerDetails;
           gvwTimeDetails.DataBind();
       }
       else if (ddlTestName.SelectedIndex > 0)
       {
           LinqTimerDetails.Where = "TestId=" + ddlTestName.SelectedValue;
           gvwTimeDetails.DataSource = LinqTimerDetails;
           gvwTimeDetails.DataBind();
       }
   }
    */

   private int GetTimeValue(string value)
   {
       int time = 0;
       try
       {
           time = int.Parse(value);
       }
       catch (Exception ex)
       { time = 0; }
       return time;

   }
   protected void btnAddTime_Click(object sender, EventArgs e)
   {
       int userid = 0;
       if (Session["UserID"] != null)
           userid = int.Parse(Session["UserID"].ToString());
       if (ddlTestName.SelectedIndex > 0)
       {
           if ((txtHours.Text.Trim() != "0" && txtHours.Text.Trim() != "") || (txtMinutes.Text.Trim() != "0" && txtMinutes.Text.Trim() != ""))
           {
               int hrs = 0, min = 0;
               hrs = GetTimeValue(txtHours.Text); min = GetTimeValue(txtMinutes.Text);
               int testid = 0, testsecid = 0, variableid = 0;
               testid = int.Parse(ddlTestName.SelectedValue); testsecid = int.Parse(ddlTestSectionName.SelectedValue);
               variableid = int.Parse(ddlVariableList.SelectedValue);
               if (ddlVariableList.SelectedIndex > 0)
                   assesDataClasses.Procedure_TimerDetails(testid, testsecid, variableid, hrs, min, 1, userid, 3);
               else if (ddlTestSectionName.SelectedIndex > 0)
                   assesDataClasses.Procedure_TimerDetails(testid, testsecid, variableid, hrs, min, 1, userid, 2);
               else if (ddlTestName.SelectedIndex > 0)
                   assesDataClasses.Procedure_TimerDetails(testid, testsecid, variableid, hrs, min, 1, userid, 1);
               lblMessageTimes.Text = "Saved Successfully...";
               ClearTimerDetails(1);
           }
           else lblMessageTimes.Text = "Please enter Time.";
       }
       else lblMessageTimes.Text = "Please select a Test.";

   }
   protected void btnResetTime_Click(object sender, EventArgs e)
   {        
       ClearTimerDetails(0);
   }
   protected void btnDeleteTime_Click(object sender, EventArgs e)
   {
       int timerid = 0;
       if (Session["timerid"] != null)
       {
           timerid = int.Parse(Session["timerid"].ToString());
           assesDataClasses.Procedure_DeleteTimerDetails(timerid);
           lblMessageTimes.Text = "Deleted successfully...";
           ClearTimerDetails(1);
       }
       else lblMessageTimes.Text = "Please select a value for deletion.";
   }
   protected void gvwTimeDetails_SelectedIndexChanged(object sender, EventArgs e)
   {
       Session["timerid"] = gvwTimeDetails.SelectedRow.Cells[10].Text;
       for (int i = 0; i < ddlTestName.Items.Count; i++)
       {
           if (ddlTestName.Items[i].Value == gvwTimeDetails.SelectedRow.Cells[7].Text)
           { Session["TestIndex_Timer"] = i; ddlTestName.SelectedIndex = i; break; }

       }
       //ddlTestName.SelectedValue=gvwTimeDetails.SelectedRow.Cells[7].Text;
       for (int i = 0; i < ddlTestSectionName.Items.Count; i++)
       {
           if (ddlTestSectionName.Items[i].Value == gvwTimeDetails.SelectedRow.Cells[8].Text)
           { Session["TestsectionIndex_Timer"] = i; ddlTestSectionName.SelectedIndex = i;FillVariableList(1); break; }//  }

       }
       //ddlTestSectionName.SelectedValue=gvwTimeDetails.SelectedRow.Cells[8].Text;
       for (int i = 0; i < ddlVariableList.Items.Count; i++)
       {
           if (ddlVariableList.Items[i].Value == gvwTimeDetails.SelectedRow.Cells[9].Text)
           { Session["variableIndex_timer"] = i; ddlVariableList.SelectedIndex = i; break; }
       }
       //ddlVariableList.SelectedValue=gvwTimeDetails.SelectedRow.Cells[9].Text;
       txtHours.Text = gvwTimeDetails.SelectedRow.Cells[4].Text;
       txtMinutes.Text = gvwTimeDetails.SelectedRow.Cells[5].Text;

   }
   private void ClearTimerDetails(int index)
   {
       if (index == 0)
       {
           Session["OrgIndex_Timer"] = null; Session["TestIndex_Timer"] = null;
           ddlOrganization.SelectedIndex = 0; ddlTestName.SelectedIndex = 0;
           FillOrganizationList();
           gvwTimeDetails.DataSource = ""; gvwTimeDetails.DataBind();
       }
       Session["TestsectionIndex_Timer"] = null;
       ddlTestSectionName.SelectedIndex = 0;
       txtHours.Text = "0"; txtMinutes.Text = "0";
       Session["timerid"] = null;

       ddlVariableList.SelectedIndex = 0;
       Session["variableIndex_timer"] = null;

       if (index == 1)
           FillTimeDetails();

   }
   private void FillTimeDetails()
   {
       if (ddlVariableList.SelectedIndex > 0)
       {
           LinqTimerDetails.Where = "TestId=" + ddlTestName.SelectedValue + " && TestSectionId=" + ddlTestSectionName.SelectedValue + " && TestVariableId=" + ddlVariableList.SelectedValue;
           gvwTimeDetails.DataSource = LinqTimerDetails;
           gvwTimeDetails.DataBind();
       }
       else if (ddlTestSectionName.SelectedIndex > 0)
       {
           LinqTimerDetails.Where = "TestId=" + ddlTestName.SelectedValue + " && TestSectionId=" + ddlTestSectionName.SelectedValue;
           gvwTimeDetails.DataSource = LinqTimerDetails;
           gvwTimeDetails.DataBind();
       }
       else if (ddlTestName.SelectedIndex > 0)
       {
           LinqTimerDetails.Where = "TestId=" + ddlTestName.SelectedValue;
           gvwTimeDetails.DataSource = LinqTimerDetails;
           gvwTimeDetails.DataBind();
       }
       else
       {
           gvwTimeDetails.DataSource = "";
           gvwTimeDetails.DataBind();
       }
   }
   protected void ddlTestSectionName_SelectedIndexChanged(object sender, EventArgs e)
   {
       if (ddlTestName.SelectedIndex > 0)
       {
           if (Session["TestsectionIndex_Timer"] != null)
               if (Session["TestsectionIndex_Timer"].ToString() != ddlTestSectionName.SelectedIndex.ToString())
               { Session["variableIndex_timer"] = null; ddlVariableList.SelectedIndex = 0; }

           Session["TestsectionIndex_Timer"] = ddlTestSectionName.SelectedIndex.ToString(); //FillTimeDetails();
       } //BindGrid();  }//  bip 220110
       else Session["TestsectionIndex_Timer"] = null;

       FillVariableList(0);      
   }

   protected void ddlVariableList_SelectedIndexChanged(object sender, EventArgs e)
   {
       if (ddlVariableList.SelectedIndex > 0)
       {
           Session["variableIndex_timer"] = ddlVariableList.SelectedIndex;
           // FillTimeDetails();
       }
       else Session["variableIndex_timer"] = null;
      FillTimeDetails();
   }
}
