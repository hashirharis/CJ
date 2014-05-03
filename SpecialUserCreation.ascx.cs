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
using System.Data.OleDb;
using System.IO;
using System.Web.Mail;

public partial class SpecialUserCreation : System.Web.UI.UserControl
{
    DBManagementClass clsClass = new DBManagementClass();
    AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    int userCode = 0;
    int userId = 0;
    int groupid = 0;
    int organizationid = 0;
    string folderpath = "";
    string userName = "";
    int userstatus = 0;
    int testid = 0;
    string organizationName = "";
    DateTime loginfromdate, logintodate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            userId = int.Parse(Session["UserID"].ToString());
            if (Session["loginfromdate"] == null)
            {
                //var getLoginDate = from logindate in dataclasses.UserProfiles where logindate.UserId == userId select getLoginDate;
               
            //Session["loginfromdate"]=getlogindatedet.First().
            //Session["logintodate"]

                var logindet = from logDet in dataclasses.UserProfiles
                              where logDet.UserId == userId
                              select logDet;
                Session["loginfromdate"] = logindet.First().LoginFromDate.ToString();
                Session["logintodate"] = logindet.First().LoginToDate.ToString();
            }

            loginfromdate = DateTime.Parse(Session["loginfromdate"].ToString());
            logintodate=DateTime.Parse(Session["logintodate"].ToString());
        }
        if (Session["AdminOrganizationID"] != null)
            organizationid = int.Parse(Session["AdminOrganizationID"].ToString());

        if (Session["adminOrgName"] != null)
            organizationName = Session["adminOrgName"].ToString();
        else
        {
            var orgName = from orgDet in dataclasses.Organizations
                          where orgDet.OrganizationID == organizationid
                          select orgDet;
            if (orgName.Count() > 0)
            { organizationName = orgName.First().Name.ToString(); Session["adminOrgName"] = organizationName; }

        }

        if (Session["folderpath"] != null)
            folderpath = Session["folderpath"].ToString();
        else
        {
            string strfilename = "errorlist_" + DateTime.Parse(DateTime.Now.ToString()).ToString("dd-MMM-yyyy");
            strfilename += DateTime.Parse(DateTime.Now.ToString()).ToString("_hh-mm-ss") + ".txt";
            folderpath = Server.MapPath("images/") + strfilename;
            Session["folderpath"] = folderpath;
        }
        if (Session["buluserceration"] != null)
            if (Session["buluserceration"].ToString() == "True")
            { pnlBulkUserCreation.Visible = true; pnlUserCreation.Visible = false; return; }

        if (ViewState["Password"] != null)
            txtPassword.Attributes.Add("Value", ViewState["Password"].ToString());

        FillGroupList();
        if (!IsPostBack)
            fillValues();

        fillDataGrid();        
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text.Trim() == "" || ddlUserType.SelectedIndex <= 0 || txtPassword.Text.Trim() == "")// || ddlOrg.SelectedIndex < 0 || ddlUserGroup.SelectedIndex < 0)
        { lblMessage.Text = "Enter the values"; }
        else
        {
            

            if (txtPassword.Text != "")
                ViewState["Password"] = txtPassword.Text;
            string fromDate = ValidateDate(txtLoginFromDate.Value);
            string toDate = ValidateDate(txtLoginToDate.Value);

            DateTime dtFrom = DateTime.Now.AddDays(-1); //DateTime.Parse(ValidateDate(txtLoginFromDate.Value));
            DateTime dtTo = DateTime.Now.AddDays(-1);// DateTime.Parse(ValidateDate(txtLoginToDate.Value));

            if (fromDate != "")
                dtFrom = DateTime.Parse(fromDate);
            if (toDate != "")
                dtTo = DateTime.Parse(toDate);

            if (dtFrom > dtTo)
            { lblMessage.Text = "From Date must be less than Todate"; return; }

            // bipson 28082010
            if (dtFrom < loginfromdate)
            { lblMessage.Text = "From Date must be greater than or equal to allowed login date limit:" + loginfromdate.ToString("dd-MM-yyyy"); return; }
            if (dtTo > logintodate)
            { lblMessage.Text = "From Date must be less than or equal to allowed login date limit: " + logintodate.ToString("dd-MM-yyyy"); return; }

            if (Session["UserCode"] != null)
                userCode = int.Parse(Session["UserCode"].ToString());
            if (Session["UserID"] != null)
                userId = int.Parse(Session["UserID"].ToString());

            var checkDuplication = from userdetails in dataclasses.UserProfiles
                                   where (userdetails.UserName == txtUserName.Text.Trim() && userdetails.Password == txtPassword.Text.Trim()) && userdetails.UserId != userCode
                                   select userdetails;
            if (checkDuplication.Count() > 0)
            { lblMessage.Text = "Username/password already exists"; return; }
            int testid = 0;
            int testId1 = 0;
            if (ddlTestLists.SelectedIndex > 0)
                testid = int.Parse(ddlTestLists.SelectedValue);
            if (ddlTestlIst2.SelectedIndex > 0)
                testId1 = int.Parse(ddlTestlIst2.SelectedValue);
            if (ddlUserType.SelectedValue == "User" && testid == 0)// 03082010 bip
            { lblMessage.Text = "Please select a Test"; return; }
            ////dataclasses = new AssesmentDataClassesDataContext();

            if (ddlUserType.SelectedValue == "GrpAdmin")
                if (ddlUserGroup.SelectedIndex <= 0)
                { lblMessage.Text = "Please select a group"; return; }

            int status = int.Parse(ddlStatus.SelectedValue);
            string emailid = txtEmailId.Text.Trim();
          ////  dataclasses.AddUserCreation(userCode, txtUserName.Text, txtPassword.Text, ddlUserType.SelectedValue, organizationid, int.Parse(ddlUserGroup.SelectedValue), dtFrom, dtTo, status, userId, emailid, testid, 0, testId1);
            //string mailbody = GenerateMailBody(txtUserName.Text, txtPassword.Text);
            //if (emailid != "")
            //    SendEmail(emailid, "", "Login Details", mailbody);
            //if(emailid!="")
            // SendMail(emailid, "", "Login Details", mailbody);
            lblMessage.Text = "Values Saved";
            ClearControls();
            Session["UserCode"] = null;
            fillDataGrid();
             Session["GrpIndex"] = null;            
            ddlUserGroup.SelectedIndex = 0; ddlUserType.SelectedIndex = 0;
        }
    }
    private void fillValues()
    {
        // ClearControls();
        int userID = 0;
        if (Session["UserCode"] != null)
            userID = int.Parse(Session["UserCode"].ToString());
        if (userID > 0)
        {
            var details1 = from details in dataclasses.UserProfiles where details.UserId == userID select details;
            //&& Session["UserCode"] == null)
            //    lblMessage.Text = "Duplication,Enter new Values";
            //else 
            if (details1.Count() > 0)
            {
                if (details1.First().UserName != null)
                    txtUserName.Text = details1.First().UserName.ToString();
                txtPassword.Text = details1.First().Password.ToString();
                txtPassword.Attributes["Value"] = txtPassword.Text;
                ViewState["Password"] = txtPassword.Text;
                txtPassword.Attributes.Add("Value", ViewState["Password"].ToString());
                //ddlUserType.SelectedValue = details1.First().UserType.ToString();
                if (details1.First().UserType != null)
                    for (int i = 0; i < ddlUserType.Items.Count; i++)
                    {
                        if (ddlUserType.Items[i].Value == details1.First().UserType.ToString())
                        {
                            ddlUserType.SelectedIndex = i;
                            break;
                        }
                    }
                ////ddlOrg.SelectedValue = details1.First().OrganizationID.ToString();
                //if (details1.First().OrganizationID != null)
                //    for (int i = 0; i < ddlOrg.Items.Count; i++)
                //    {
                //        if (ddlOrg.Items[i].Value == details1.First().OrganizationID.ToString())
                //        {
                //            ddlOrg.SelectedIndex = i;
                //            Session["OrgIndex"] = i.ToString();
                //            FillTestLists(); FillGroupList();//fillComboValues();
                //            break;
                //        }
                //    }

                if (details1.First().TestId != null)
                    for (int i = 0; i < ddlTestLists.Items.Count; i++)
                    {
                        if (ddlTestLists.Items[i].Value == details1.First().TestId.ToString())
                        {
                            ddlTestLists.SelectedIndex = i;
                            Session["TestIndex"] = i.ToString();
                            break;
                        }
                    }

                //ddlUserGroup.SelectedValue = details1.First().GrpUserID.ToString();
                if (details1.First().GrpUserID != null)
                    for (int i = 0; i < ddlUserGroup.Items.Count; i++)
                    {
                        if (ddlUserGroup.Items[i].Value == details1.First().GrpUserID.ToString())
                        {
                            ddlUserGroup.SelectedIndex = i;
                            Session["GrpIndex"] = i.ToString();
                            break;
                        }
                    }

                if (details1.First().LoginFromDate != null)
                    txtLoginFromDate.Value = DateTime.Parse(details1.First().LoginFromDate.ToString()).ToString("dd-MM-yyyy");
                if (details1.First().LoginToDate != null)
                    txtLoginToDate.Value = DateTime.Parse(details1.First().LoginToDate.ToString()).ToString("dd-MM-yyyy");

                if (details1.First().Status != null)
                    ddlStatus.SelectedValue = details1.First().Status.ToString();
                if (details1.First().EmailId != null)
                    txtEmailId.Text = details1.First().EmailId.ToString();



            }
        }
    }

    private void fillDataGrid()
    {

        var userdetailfromdb = from userdet in dataclasses.View_UserDetails
                               where userdet.OrganizationID == organizationid && userdet.AdminAccess == 0
                               select userdet;// new {"UserName, Password, UserType, OrgName, GroupName, UserId, OrganizationID, GroupUserID, LoginFromDate, LoginToDate, ReportAccess, Status, EmailId"};

        gvwPasswordCreation.DataSource = userdetailfromdb;// UserLinqDataSource;
        //UserLinqDataSource.DataBind();
        gvwPasswordCreation.DataBind();

        if (gvwPasswordCreation.Rows.Count <= 0)
            btnDeleteAll.Visible = false;
    }

    protected void gvwPassword_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["UserCode"] = gvwPasswordCreation.SelectedRow.Cells[9].Text;
        fillValues();

    }
    private string ValidateDate(string ctrl)
    {
        try
        {
            if (ctrl.Length != 10)
            { lblMessage.Text = "Enter valid date"; return ""; }

            //DateTime dt = DateTime.Parse(txtPassword.Text);
            string dt = ctrl;
            string dateCheck = "";
            string date = "";
            string month = "";
            string year = "";
            for (int i = 0; i < dt.Length; i++)
            {
                char[] a = dt.ToCharArray();

                //if ((a[i]).ToString() != "-" && (a[i]).ToString() != "+")
                //{ int num = int.Parse((a[i]).ToString()); }
                //int num = int.Parse((a[i]).ToString());
                if (i < 2)
                    date += (a[i]).ToString();
                else if (i > 2 && i < 5)
                    month += (a[i]).ToString();
                else if (i > 5)
                    year += (a[i]).ToString();

            }
            dateCheck = month + "-" + date + "-" + year;
            string curdate = date + "-" + month + "-" + year;
            DateTime curdt = DateTime.Parse(dateCheck);
            //Session["dob"] = curdate;
            return curdt.ToString("MM-dd-yyyy");
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Enter valid date "; return "";
        }
    }

    private void ClearControls()
    {
        txtUserName.Text = "";
        txtPassword.Text = "";
        ddlUserType.SelectedIndex = 0;
        //ddlOrg.SelectedIndex = 0;
        //ddlUserGroup.SelectedIndex = 0;

        txtLoginFromDate.Value = "";
        txtLoginToDate.Value = "";
        txtEmailId.Text = "";
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearControls();
        Session["UserCode"] = null;
        txtUserName.Text = "";
        txtPassword.Text = "";
        ddlUserType.SelectedIndex = 0;
        
        ddlUserGroup.SelectedIndex = 0;

    }

    
    private void FillTestLists()
    {
        int testIndex = 0;
        if (Session["TestIndex"] != null)
        {
            testIndex = int.Parse(Session["TestIndex"].ToString());
        }
        ListItem listnew;
        listnew = new ListItem("--select--", "0");
        ddlTestLists.Items.Clear();
        ddlTestLists.Items.Add(listnew);
        //if (orgIndex > 0)
        //{
        var usertestdetailfromdb = from usertestdet in dataclasses.TestLists
                                   where usertestdet.OrganizationName == organizationName
                                   select new { usertestdet.TestName, usertestdet.TestId };
        //GrpUserLinqDataSource.Where = "OrganizationID=" + ddlOrg.SelectedValue;
        //ddlUserGroup.DataSource = GrpUserLinqDataSource;
        ddlTestLists.DataSource = usertestdetailfromdb;
        ddlTestLists.DataTextField = "TestName";
        ddlTestLists.DataValueField = "TestId";
        ddlTestLists.DataBind();
        if (testIndex > 0)
            ddlTestLists.SelectedIndex = testIndex;
        // }


    }

    private void FillGroupList()
    {
        FillTestLists();//bip 240909

        int grpIndex = 0;
        if (Session["GrpIndex"] != null)
        {
            grpIndex = int.Parse(Session["GrpIndex"].ToString());
        }
        ListItem listnew;
        listnew = new ListItem("--select--", "0");
        ddlUserGroup.Items.Clear();
        ddlUserGroup.Items.Add(listnew);
        //if (orgIndex > 0)
        //{
        var usergrpdetailfromdb = from usergrpdet in dataclasses.GroupUsers
                                  where usergrpdet.OrganizationID == organizationid
                                  select new { usergrpdet.GroupName, usergrpdet.GroupUserID };
        //GrpUserLinqDataSource.Where = "OrganizationID=" + ddlOrg.SelectedValue;
        //ddlUserGroup.DataSource = GrpUserLinqDataSource;
        ddlUserGroup.DataSource = usergrpdetailfromdb;
        ddlUserGroup.DataTextField = "GroupName";
        ddlUserGroup.DataValueField = "GroupUserId";
        ddlUserGroup.DataBind();
        if (grpIndex > 0)
            ddlUserGroup.SelectedIndex = grpIndex;
        // }

    }

   
    private void fillprofiles()
    {
        GrpUserLinqDataSource.Where = "OrganizationID=" + organizationid;
        ddlUserGroup.DataSource = GrpUserLinqDataSource;
        ddlUserGroup.DataTextField = "GroupName";
        ddlUserGroup.DataValueField = "GroupUserId";
        ddlUserGroup.DataBind();
        //ddlUserGroup.SelectedIndex = grpIndex;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["UserCode"] != null)
        {
            int seluserid = int.Parse(Session["UserCode"].ToString());
            dataclasses.DeleteUser(seluserid);
            ClearControls();
            Session["UserCode"] = null;
            fillDataGrid();
            lblMessage.Text = "Deleted successfully";

        }
        else lblMessage.Text = "Please select a user for deletion";
    }
    protected void btnImportUserList_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtfilename.Text.Trim() == "") { lblMessage0.Text = "Please select a file"; return; }
            //if (ddlOrganization_bulkuser.SelectedIndex <= 0) { lblMessage0.Text = "Please select an Organization"; return; }
            //if (ddlOrganization_bulkuser.SelectedItem.Text == "") { lblMessage0.Text = "Please select an Organization"; return; }
            if (txtPrefix.Text.Trim() == "") { lblMessage0.Text = "Please enter prefix for user name"; return; }
            if (txtSheetName.Text.Trim() == "") { lblMessage0.Text = "Please enter sheet name in the Excel file"; return; }
            if (!File.Exists(Server.MapPath("images/" + txtfilename.Text)))
            { lblMessage0.Text = "file not found"; return; }

            String[] strs = txtfilename.Text.Split(new char[] { '\\', '\\' });
            String imageFleName = strs[strs.Length - 1];
            String[] testImg = imageFleName.Split(new char[] { '.' });
            int index = testImg.Count() - 1;
            string ext;
            ext = testImg[index];
            if (ext != "xls")
            { lblMessage0.Text = "Please Select a Valid Excel File"; return; }


            // code to insert dataworking....
            OleDbConnection oconn = new OleDbConnection
        (@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
        Server.MapPath("images/" + txtfilename.Text) + ";Extended Properties=Excel 8.0");


            OleDbCommand ocmd = new OleDbCommand("select emailid,usertype,groupname,fromdate,todate,status,testname from [" + txtSheetName.Text + "$]", oconn);
            oconn.Open();  //Here [Sheet1$] is the name of the sheet 
            //in the Excel file where the data is present
            OleDbDataReader odr = ocmd.ExecuteReader();
            int status = 0;
            //string username = "";//emailid
            //string passsword = "";
            string usertype = "";
            string organization = "";
            string groupname = "";
            DateTime fromdate = DateTime.Now; DateTime todate = DateTime.Now;
            string emailid = "";
            string testname = "";
            while (odr.Read())
            {
                if ((odr[0]).ToString() != "" || (odr[1]).ToString() != "" || (odr[2]).ToString() != "" || (odr[3]).ToString() != "" || (odr[4]).ToString() != "" || odr[5].ToString() != "" || odr[6].ToString() != "")
                    if (CheckEntries((odr[0]).ToString(), (odr[1]).ToString(),  (odr[2]).ToString(), (odr[3]).ToString(), (odr[4]).ToString(), odr[5].ToString(), odr[6].ToString()) == false)
                    {
                        //id = int.Parse(odr[0].ToString());//Here we are calling the valid method
                        emailid = (odr[0]).ToString();
                        usertype = (odr[1]).ToString();
                       //bipson 03082010 organizationid = int.Parse(ddlOrganization_bulkuser.SelectedValue); // (odr[2]).ToString();
                        groupname = (odr[2]).ToString();
                        fromdate = DateTime.Parse(odr[3].ToString());
                        todate = DateTime.Parse(odr[4].ToString());
                        // status = int.Parse(odr[5].ToString());
                        string passwordToMail = "";
                        string[] strName = userName.Trim().Split(new char[] { '@' });
                        passwordToMail = Convert.ToString(strName[0]) + DateTime.Now.Hour.ToString() + DateTime.Now.Millisecond.ToString();
                        //username|password|emailid|usertype|organization|group|fromdate|todate|status
                        //if (CheckEntries(username, usertype, ddlOrganization_bulkuser.SelectedItem.Text, groupname, fromdate, todate, status) == false)
                        //{//Here using this method we are inserting the data into the database

                        // testid = (odr[6]).ToString();
                        insertdataintodb(userName, passwordToMail, emailid, usertype, groupid, fromdate, todate, userstatus, testid.ToString(),testid.ToString());
                    }
            }
            oconn.Close();
            lblMessage0.Text = "Successfully Saved the user details...";
            if (Session["errorexists"] != null)
                if (Session["errorexists"].ToString() == "True")
                    DisplayErrorDetails();
            //////

             txtPrefix.Text = ""; txtSheetName.Text = ""; txtfilename.Text = "";
            fillDataGrid();
        }
        catch (Exception ex) { lblMessage0.Text = ex.Message + " ... Please check the sheet name."; } //ex.Message; }
    }
    private void DisplayErrorDetails()
    {
        StreamReader fp = new StreamReader(folderpath);
        try
        {
            txtErrorList.Visible = true;
            fp = File.OpenText(folderpath); txtErrorList.Text = fp.ReadToEnd(); fp.Close();
            btndownload.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }

    private bool CheckEntries(string emailid, string usertype, string groupname, string fromdate, string todate, string status, string testname)
    {
        bool valueExists = false;
        var userexists = from usercheck in dataclasses.UserProfiles
                         select usercheck;
        if (userexists.Count() > 0)
        {
            int count = int.Parse(userexists.Count().ToString());
            userName = txtPrefix.Text + (count + 1);
        }
        else userName = txtPrefix.Text + "1";

        
        var groupexists = from groupcheck in dataclasses.GroupUsers
                          where groupcheck.GroupName == groupname && groupcheck.OrganizationID == organizationid
                          select groupcheck;
        if (groupexists.Count() <= 0)
        {
            int createdby = 0;
            if (Session["UserID"] != null)
                createdby = int.Parse(Session["UserID"].ToString());
            dataclasses.AddGroupUser(0, organizationid, 0, groupname, 1, createdby,0);

            var groupexists1 = from groupcheck1 in dataclasses.GroupUsers
                               where groupcheck1.GroupName == groupname && groupcheck1.OrganizationID == organizationid
                               select groupcheck1;
            if (groupexists1.Count() > 0)
            { groupid = int.Parse(groupexists1.First().GroupUserID.ToString()); }

        }
        else { groupid = int.Parse(groupexists.First().GroupUserID.ToString()); }
        
        if (testname != "")
        {
            //string orgName = "";
            //orgName = ddlOrganization_bulkuser.SelectedItem.Text;
            var testexists = from testcheck in dataclasses.TestLists
                             where testcheck.OrganizationName == organizationName && testcheck.TestName == testname
                             select testcheck;
            if (testexists.Count() <= 0)
            {
                int createdby = 0;
                if (Session["UserID"] != null)
                    createdby = int.Parse(Session["UserID"].ToString());
                dataclasses.AddTestLists(0, testname, organizationName, 1, "", "", 0, createdby, "",0,0);

                var testexists1 = from testcheck1 in dataclasses.TestLists
                                  where testcheck1.OrganizationName == organizationName && testcheck1.TestName == testname
                                  select testcheck1;
                if (testexists1.Count() > 0)
                { testid = int.Parse(testexists.First().TestId.ToString()); }

            }
            else { testid = int.Parse(testexists.First().TestId.ToString()); }
        }
        else testid = 0;

        try
        {
            DateTime dt = DateTime.Parse(fromdate);
            DateTime dt1 = DateTime.Parse(todate);
        }
        catch (Exception ex)
        {
            clsClass.WriteErrorDetails(emailid, usertype, organizationName, groupname, fromdate, todate, status, "Invalid date", folderpath);
            Session["errorexists"] = "True"; valueExists = true;
        }

        if (usertype != "Superadmin" && usertype != "OrgAdmin" && usertype != "GrpUser" && usertype != "User")
        {
            clsClass.WriteErrorDetails(emailid, usertype, organizationName, groupname, fromdate, todate, status, "Invalid usertype", folderpath);
            Session["errorexists"] = "True"; valueExists = true;
        }
        try
        {
            userstatus = int.Parse(status);
        }
        catch (Exception ex) { userstatus = 0; }
       
        return valueExists;

    }    
    public void insertdataintodb(string userName1, string password, string emailid, string usertype, int groupid, DateTime fromdate, DateTime todate, int status, string testid,string testid1)
    {
        int testid11 = 0;
        int testnameid = 0;
        try
        {
            testnameid = int.Parse(testid);
            testid11 = int.Parse(testid1);
        }
        catch (Exception ex) { }
        
        //int status = 1;
        int createdby = 0;
        if (Session["UserID"] != null)
            createdby = int.Parse(Session["UserID"].ToString());
        DateTime createdon = DateTime.Now;
        try
        {
            ////dataclasses.AddUserCreation(0, userName1, password, usertype, organizationid, groupid, fromdate, todate, status, createdby, emailid, testnameid,0,testid11);
            string mailbody = GenerateMailBody(userName1, password);
            if (emailid != "")
                SendEmail(emailid, "", "Login Details", mailbody);
            //if (emailid != "")
            //    SendMail(emailid, "", "Login Details", mailbody);
        }
        catch (Exception ex) { lblMessage0.Text = ex.Message + "...."; }
    }
    private string GenerateMailBody(string username, string pwd)
    {
        string siteid = ConfigurationManager.AppSettings["SiteId"].ToString();
        string mailBody = "<table border='0'><tr><td>&nbsp;</td></tr><tr><td>Hi &nbsp;&nbsp; ,</td></tr><tr><td>&nbsp;</td></tr>" +
        "<tr><td>Following are your Login details:</td></tr>" +
        "<tr><td>User Name : " + username + "&nbsp;</td></tr><tr><td >Password : " + pwd + "&nbsp;</td></tr><tr><td>&nbsp;</td></tr>" +
        "<tr><td>Thank You.</td></tr><tr><td>Please logon to &nbsp; <a href='" + siteid + "' style='color:Blue;'>" + siteid + "</a> </td></tr>" +
        "<tr><td>&nbsp;</td></tr></table>";
        return mailBody;
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtfilename.Text = ""; txtSheetName.Text = "";
    }
    protected void btnGoBack_Click(object sender, EventArgs e)
    {
        pnlBulkUserCreation.Visible = false; pnlUserCreation.Visible = true; Session["buluserceration"] = null;
    }
    protected void btnBulkCreation_Click(object sender, EventArgs e)
    {
        pnlUserCreation.Visible = false; pnlBulkUserCreation.Visible = true; Session["buluserceration"] = "True";
    }
    protected void btndownload_Click(object sender, EventArgs e)
    {
        try
        {
            if (File.Exists(folderpath))
            {
                //string[] filenameExt = folderpath.Split(new char[] { '\\' });
                string filename = "";// filenameExt[0] + ".xls";
                //int count = filenameExt.Length;

                //for (int i = 0; i < count - 1; i++)
                //{
                //    if (i > 0)
                //        filename += "\\";

                //    filename += filenameExt[i];
                //}
                if (File.Exists(Server.MapPath("images\\erroreport.csv")))
                    File.Delete(Server.MapPath("images\\erroreport.csv"));
                filename = Server.MapPath("images\\erroreport.csv");
                //filename = "images\\errorlist.xls";
                File.Copy(folderpath, filename);
                //File.Delete(folderpath);
                //string filepath = "images\\bulkusertest.xls";//"~/DownloadFiles/" + gvwDownloadFileList.SelectedRow.Cells[2].Text;  
                txtErrorList.Visible = false; btndownload.Visible = false;
                Response.Redirect("images\\erroreport.csv");

            }
        }
        catch (Exception ex) { }
    }
    protected void gvwPasswordCreation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvwPasswordCreation.PageIndex = e.NewPageIndex;
        fillDataGrid();
    }
    
    protected void btnSendmail_Click(object sender, EventArgs e)
    {
        try
        {
            SendEmail("bipson.thomas@ivasystems.co.in", "bipsonthomas@gmail.com", "test mail from site", "testing...." + DateTime.Now.ToString());
            //SendMail("bipson.thomas@ivasystems.co.in", "bipsonthomas@gmail.com", "test mail from site", "testing...." + DateTime.Now.ToString() );
            lblMessage0.Text = "mail send...";
        }
        catch (Exception ex) { lblMessage0.Text = ex.Message; }
    }

    public void SendMail(string to, string cc, string subject, string mailbody)
    {
        //code to send mail from systems..
        //try
        //{

        string fromAdd = ConfigurationManager.AppSettings["From"].ToString();
        string bccAdd = ConfigurationManager.AppSettings["BCC"].ToString();

        MailMessage objmail = new MailMessage();
        if (fromAdd != "")
            objmail.From = fromAdd;
        if (bccAdd.Trim() != "")
            objmail.Bcc = bccAdd;
        if (to != "")
            objmail.To = to;
        if (cc != "")
            objmail.Cc = cc;

        objmail.Subject = subject;// "Profile for Approval";
        objmail.Body = mailbody;// GenerateMailBody(userName, pwd);

        objmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/cdoSMTPServerPort", "25");
        objmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/cdoSMTPConnectionTimeout", "20");
        objmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/cdoSendUsingPort", "2");
        objmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication
        /**/
        objmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "info@careerjudge.com");  //"portaladmin@keralaenergy.gov.in");  // set your username here
        objmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "1info1"); // "emcportaladmin898"); //set your password here

        objmail.BodyFormat = MailFormat.Html;
        objmail.Priority = MailPriority.Normal;
        SmtpMail.SmtpServer = "smtpout.secureserver.net";// "smtp.keralaenergy.gov.in";//
        SmtpMail.Send(objmail);
        //}
        //catch (Exception ex) { }


    }
    private void SendEmail(string to, string cc, string subject, string mailbody)
    {
        //code to send mail from server..

        const string SERVER = "relay-hosting.secureserver.net";
        MailMessage oMail = new System.Web.Mail.MailMessage();
        oMail.From = "info@careerjudge.com";
        oMail.To = to;
        oMail.Subject = subject;
        oMail.BodyFormat = MailFormat.Html; // enumeration
        oMail.Priority = MailPriority.High; // enumeration
        oMail.Body = mailbody; //"Sent at: " + DateTime.Now;
        SmtpMail.SmtpServer = SERVER;
        SmtpMail.Send(oMail);
        oMail = null; // free up resources
    }
    /*

    



    */

    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        int selUserId = 0;
        int i = 0;
        bool itemchecked = false;
        foreach (GridViewRow gr in gvwPasswordCreation.Rows)
        {
            selUserId = int.Parse(gvwPasswordCreation.Rows[i].Cells[9].Text);
            CheckBox cb = new CheckBox();
            cb = (CheckBox)gr.Controls[0].FindControl("CheckBox1");
            if (cb.Checked)
            {
                itemchecked = true;
                dataclasses.DeleteUser(selUserId);
            }
            i = i + 1;

        }
        if (itemchecked == true)
        {
            ClearControls();
            Session["UserCode"] = null;
            fillDataGrid();
            lblMessageDelAll.Text = "Deletion successfull";
        }
        else lblMessageDelAll.Text = "please select user(s) from the above list";
    }
    protected void ddlTestlIst2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["TestIndex1"] = ddlTestlIst2.SelectedIndex; ;
        fillDataGrid();
    }

}
