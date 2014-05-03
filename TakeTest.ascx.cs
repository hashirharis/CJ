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
using System.Web.Mail;
using System.Data.SqlClient;
public partial class TakeTest : System.Web.UI.UserControl
{
    //FJADataClassesDataContext dataclasses = new FJADataClassesDataContext();
    DBManagementClass clsClass = new DBManagementClass();
    AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowCurrentdetails();
    }
    private void ShowCurrentdetails()
    {
        if (Session["CurCategoryId"] != null)
        {
            int groupid = int.Parse(Session["CurCategoryId"].ToString());
            ShowTestNameList(groupid);
        }
    }
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    if (txtUsername.Value != "" && txtPassword.Value != "")
    //    {
    //        Session["loginusername"] = txtUsername.Value;
    //        Session["loginpassword"] = txtPassword.Value;
    //        //Session["SubCtrl"] = null;
    //        //Response.Redirect("TalentScout.htm");
    //       ////// LoginCheckTalentScout();
    //        LoginChecknew();
    //    }
    //    else lblMessage.Text = "Please enter username/password";
    //}

    //private void LoginChecknew()
    //{
    //    bool loginsucess = false;
    //    if (Session["loginusername"] != null && Session["loginpassword"] != null)
    //    { txtUsername.Value = Session["loginusername"].ToString(); txtPassword.Value = Session["loginpassword"].ToString(); }
    //    if (txtUsername.Value != "" && txtPassword.Value != "")
    //    {
    //        Session.Clear();
    //        var LoginDetails1 = from LoginDetails in dataclasses.UserProfiles
    //                            where LoginDetails.UserName == txtUsername.Value  && LoginDetails.Password == txtPassword.Value && LoginDetails.Status == 1
    //                            select LoginDetails;
    //        //var LoginDetails1 = from LoginDetails in dataclass.UserProfiles
    //        //                    where LoginDetails.UserName == "admin" && LoginDetails.Password == "123" && LoginDetails.Status == 1
    //        //                    select LoginDetails;
    //       // lblmessage.Text = LoginDetails1.Count().ToString();
    //        if (LoginDetails1.Count() > 0)

    //                //var LoginDetails1 = from LoginDetails in dataclass.UserProfiles
    //                //                    where LoginDetails.UserName == txtUsername.Value && LoginDetails.Password == txtPassword.Value
    //                //                    select LoginDetails;
    //                //if (LoginDetails1.Count() > 0)
    //                {
    //                    ////////////////

    //                    if (LoginDetails1.First().UserType.ToString() != "SuperAdmin")
    //                    {

    //                        Boolean dtAssigned = false;
    //                        //int userid = 0;
    //                        DateTime dtfrom = DateTime.Today;
    //                        if (LoginDetails1.First().LoginFromDate != null)
    //                        {
    //                            dtfrom = DateTime.Parse(LoginDetails1.First().LoginFromDate.ToString());
    //                            dtAssigned = true;
    //                        }
    //                        DateTime dtto = DateTime.Today;
    //                        if (LoginDetails1.First().LoginToDate != null)
    //                        {
    //                            dtto = DateTime.Parse(LoginDetails1.First().LoginToDate.ToString());
    //                            dtAssigned = true;
    //                        }
    //                        DateTime today = DateTime.Today;
    //                        if (dtAssigned == false)
    //                        { lblMessage.Text = "You cant Login now. Contact  site admin"; return; }
    //                        if (dtfrom <= today && dtto >= today)
    //                        { }
    //                        else { lblMessage.Text = "You cant Login now. Contact  site admin"; return; }
    //                    }
    //                    /////////////

    //                    int userid = 0;
    //                    //if (Session["UserID"] != null)
    //                    //    userid = int.Parse(Session["UserID"].ToString());

    //                    if (LoginDetails1.First().UserId != null)
    //                    {
    //                        Session["UserID"] = LoginDetails1.First().UserId.ToString();
    //                        userid = int.Parse(Session["UserID"].ToString());

    //                        //  Response.Redirect("http://talentscout.careerjudge.com/FJAHome.aspx?uid" + DateTime.Now.ToString("dd-MM-yyyy") + "=" + userid);
    //                        Response.Redirect("talentscout/FJAHome.aspx?uid" + DateTime.Now.ToString("dd-MM-yyyy") + "=" + userid);

    //                    }

    //                }
    //                else
    //                {
    //                    lblMessage.Text = "Incorrect UserName/Password! ";
    //                    return;
    //                }
    //    }
    //    else
    //    {
    //        lblMessage.Text = "Eneter UserName/Password";
    //        return;
    //    }
    //}

    //private void LoginCheckTalentScout()
    //{
    //    bool loginsucess = false;
    //    if (Session["loginusername"] != null && Session["loginpassword"] != null)
    //    { txtUsername.Value = Session["loginusername"].ToString(); txtPassword.Value = Session["loginpassword"].ToString(); }
    //    if (txtUsername.Value != "" && txtPassword.Value != "")
    //    {
    //        Session.Clear();
    //        //Session["SubCtrl"] = "TaskEntry.ascx";
    //        //Response.Redirect("FJAHome.aspx"); return;
    //        string connectionstring = "Data Source=talentscout.db.4433135.hostedresource.com;Initial Catalog=talentscout;User ID=talentscout;Password=Talent#12Scout;Integrated Security=False;";
    //        ////string connectionstring = "Data Source=Compaq2\\sqlexpress;Initial Catalog=talentscout;Integrated Security=True";
    //        string querystring = "select * from userprofile where username='" + txtUsername.Value + "' and password='" + txtPassword.Value + "'";
    //        SqlConnection sqlconn = new SqlConnection(connectionstring);
    //        sqlconn.Open();
    //        SqlCommand cmd = new SqlCommand(querystring, sqlconn);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds); sqlconn.Close();
    //        if (ds != null)
    //            if (ds.Tables.Count > 0)
    //                if (ds.Tables[0].Rows.Count > 0)

    //                //var LoginDetails1 = from LoginDetails in dataclass.UserProfiles
    //                //                    where LoginDetails.UserName == txtUsername.Value && LoginDetails.Password == txtPassword.Value
    //                //                    select LoginDetails;
    //                //if (LoginDetails1.Count() > 0)
    //                {
    //                    ////////////////

    //                    if (ds.Tables[0].Rows[0]["UserType"].ToString() != "SuperAdmin")
    //                    {

    //                        Boolean dtAssigned = false;
    //                        //int userid = 0;
    //                        DateTime dtfrom = DateTime.Today;
    //                        if (ds.Tables[0].Rows[0]["LoginFromDate"] != null)
    //                        {
    //                            dtfrom = DateTime.Parse(ds.Tables[0].Rows[0]["LoginFromDate"].ToString());
    //                            dtAssigned = true;
    //                        }
    //                        DateTime dtto = DateTime.Today;
    //                        if (ds.Tables[0].Rows[0]["LoginToDate"] != null)
    //                        {
    //                            dtto = DateTime.Parse(ds.Tables[0].Rows[0]["LoginToDate"].ToString());
    //                            dtAssigned = true;
    //                        }
    //                        DateTime today = DateTime.Today;
    //                        if (dtAssigned == false)
    //                        { lblMessage.Text = "You cant Login now. Contact  site admin"; return; }
    //                        if (dtfrom <= today && dtto >= today)
    //                        { }
    //                        else { lblMessage.Text = "You cant Login now. Contact  site admin"; return; }
    //                    }
    //                    /////////////

    //                    int userid = 0;
    //                    //if (Session["UserID"] != null)
    //                    //    userid = int.Parse(Session["UserID"].ToString());

    //                    if (ds.Tables[0].Rows[0]["UserId"] != null)
    //                    {
    //                        Session["UserID"] = ds.Tables[0].Rows[0]["UserId"].ToString();
    //                        userid = int.Parse(Session["UserID"].ToString());

    //                      //  Response.Redirect("http://talentscout.careerjudge.com/FJAHome.aspx?uid" + DateTime.Now.ToString("dd-MM-yyyy") + "=" + userid);
    //                        Response.Redirect("talentscout/FJAHome.aspx?uid" + DateTime.Now.ToString("dd-MM-yyyy") + "=" + userid);

    //                    }                                     
                        
    //                }
    //                else
    //                {
    //                    lblMessage.Text = "Incorrect UserName/Password! ";
    //                    return;
    //                }
    //    }
    //    else
    //    {
    //        lblMessage.Text = "Eneter UserName/Password";
    //        return;
    //    }
    //    // return loginsucess;
    //}
    protected void lbtnTalentSCOUT_Click(object sender, EventArgs e)
    {
        Session["SubCtrl"] = "TalentScout.ascx";
        Response.Redirect("FJAHome.aspx");
    }
    protected void imgbtnHRSelection_Click(object sender, ImageClickEventArgs e)
    {
        //Session["ContactSel"] = null;
        Session["CurCategoryId"] = "1";
        ShowTestNameList(1); 
    }
    protected void imgbtnManagerialLeadership_Click(object sender, ImageClickEventArgs e)
    {
        //Session["ContactSel"] = null;
        Session["CurCategoryId"] = "2";
        ShowTestNameList(2);
    }
    protected void imgbtnSoftSkills_Click(object sender, ImageClickEventArgs e)
    {
        //Session["ContactSel"] = null;
        Session["CurCategoryId"] = "3";
        ShowTestNameList(3);
    }
    protected void imgbtnCareerProfiling_Click(object sender, ImageClickEventArgs e)
    {
        //Session["ContactSel"] = null;
        Session["CurCategoryId"] = "4";
        ShowTestNameList(4);
    }
    protected void imgbtnGeneralAptitude_Click(object sender, ImageClickEventArgs e)
    {
       // Session["ContactSel"] = null;
        Session["CurCategoryId"] = "5";
        ShowTestNameList(5);
    }
    protected void imgbtnCreativeArt_Click(object sender, ImageClickEventArgs e)
    {
       // Session["ContactSel"] = null;
        Session["CurCategoryId"] = "6";
        ShowTestNameList(6);
    }
    protected void imgbtnSocialSkills_Click(object sender, ImageClickEventArgs e)
    {
       // Session["ContactSel"] = null;
        Session["CurCategoryId"] = "7";
        ShowTestNameList(7);
    }

    private void ShowTestNameList(int groupid)
    {
        //if (Session["CurCategoryId"] != null)
        //    groupid = int.Parse(Session["CurCategoryId"].ToString());

        Table dtTestListMain = new   Table();
        TableRow trTestListMain = new  TableRow();
        TableCell tcellTestListMain = new  TableCell();
        dtTestListMain.Width = 500;
        //tcellTestListMain.Style.Add("text-align", "center");
        tcellTestListMain.Style.Add("padding-top", "30px");
        HtmlTable dtTestList = new HtmlTable();
        HtmlTableRow trTestList;
        HtmlTableCell tcellTestList;
        var gettestnamelist = from gettestnamedet in dataclasses.TestDetails
                              where gettestnamedet.CategoryId == groupid
                              select gettestnamedet;
        if (gettestnamelist.Count() > 0)
        {
            int i = 1;
            foreach (var testnames in gettestnamelist)
            {
                trTestList = new HtmlTableRow();
                tcellTestList = new HtmlTableCell(); tcellTestList.Style.Add("text-align", "left");

                LinkButton lbtnTestName = new LinkButton();
                lbtnTestName.ID = "TestName_" + i.ToString();
                lbtnTestName.Text = testnames.TitleLine1 + " " + testnames.TitleLine2 + " " + testnames.TitleLine3;
                lbtnTestName.CommandName = "TestName" + i.ToString();
                lbtnTestName.CommandArgument = Convert.ToString(testnames.TestItemId);
                lbtnTestName.Click += new EventHandler(lbtnTestName_Click);
                tcellTestList.Controls.Add(lbtnTestName);
                trTestList.Cells.Add(tcellTestList); 
                dtTestList.Rows.Add(trTestList);

                i++;
            }
            tcellTestListMain.Controls.Add(dtTestList);
            trTestListMain.Cells.Add(tcellTestListMain);
            dtTestListMain.Rows.Add(trTestListMain);
            //////pnlTestList.Controls.Clear();
            //////pnlTestList.Controls.Add(dtTestListMain);
        }
        else
        {
            string errormessage = "<div style='text-align: center; vertical-align: middle; width: 200px; height: 200px; padding-top: 100px;'>No details found...</div>";
            trTestList = new HtmlTableRow();
            tcellTestList = new HtmlTableCell();
            tcellTestList.InnerHtml = errormessage;
            trTestList.Cells.Add(tcellTestList);
            dtTestList.Rows.Add(trTestList);
           ////// pnlTestList.Controls.Clear();
           ////// pnlTestList.Controls.Add(dtTestList);
            Session["CurCategoryId"] = null;
        }
      ////////  lbtnBack_1.Visible = true; lbtnBack.Visible = false;

    }

    protected void lbtnTestName_Click(object sender, EventArgs e)
    {
        //throw new NotImplementedException();
        LinkButton btn = (LinkButton)sender;
        int testitemid = int.Parse(btn.CommandArgument);
        ShowTestDetails(testitemid);
     //   Session["CurCategoryId"] = null;
    }

    private void ShowTestDetails(int testitemid)
    {
        try
        {
            
            HtmlTable dtTestList = new HtmlTable();
            HtmlTableRow trTestList;
            HtmlTableCell tcellTestList;
            var gettestnamelist = from gettestnamedet in dataclasses.TestDetails
                                  where gettestnamedet.TestItemId == testitemid
                                  select gettestnamedet;
            if (gettestnamelist.Count() > 0)
            {              
                foreach (var testnames in gettestnamelist)
                {
                    trTestList = new HtmlTableRow();
                    string title1 = testnames.TitleLine1.ToString();
                    string title2 = testnames.TitleLine2.ToString();
                    string title3 = testnames.TitleLine3.ToString();
                    string description1 = "";
                    if (testnames.TestDesctiption1 != null)
                        description1 = testnames.TestDesctiption1.ToString();
                    string description2 = "";
                    if (testnames.TestDesctiption2 != null)
                        description2 = testnames.TestDesctiption2.ToString();//

                    string testdetails1 = "<table cellpadding='0' cellspacing='0' style='width: 525px;'><tr><td style=' vertical-align: top; width: 175px;font-weight: bold; color: #FF0066;font-size: 13px;'>";
                    testdetails1 += "<div style='background-image:url(images/TestImages/" + testnames.ImageFileName + ");background-color:transparent;  background-repeat: no-repeat;background-position: left 20px; width: 175px; height: 170px; vertical-align: top; text-align: right; font-weight: bold;'> <table border='0' cellpadding='0' cellspacing='0' style='width:100%;'>";
                    testdetails1 += "<tr><td> " + title1 + "</td> </tr><tr> <td>" + title2 + "</td></tr> <tr><td>" + title3 + "</td>  </tr> <tr><td></td>  </tr></table></div> </td>";//background-image: url('images/TestImages/Take a test image 1.jpg')
                    testdetails1 += "<td style='vertical-align: top; padding-left: 10px;padding-right: 10px; text-align:left'>" + description1 + "</td> </tr> <tr><td colspan='2' style='vertical-align: top; padding-left: 10px; padding-right: 10px'>" + description2 + "</td> </tr></table>";
                    //
                    tcellTestList = new HtmlTableCell();
                    tcellTestList.InnerHtml = testdetails1;
                    trTestList.Cells.Add(tcellTestList);
                    dtTestList.Rows.Add(trTestList);
                }
                //////pnlTestList.Controls.Clear();
                //////pnlTestList.Controls.Add(dtTestList);

            }
            else
            {
                string errormessage = "<div style='text-align: center; vertical-align: middle; width: 200px; height: 200px; padding-top: 100px;'>No details found...</div>";
                trTestList = new HtmlTableRow();
                tcellTestList = new HtmlTableCell();
                tcellTestList.InnerHtml = errormessage;
                trTestList.Cells.Add(tcellTestList);
                dtTestList.Rows.Add(trTestList);
                //////pnlTestList.Controls.Clear();
                //////pnlTestList.Controls.Add(dtTestList);
            }
           ////// lbtnBack.Visible = true; lbtnBack_1.Visible = false;
        }
        catch (Exception ex) 
        { //////lblMessage.Text += ex.Message;
        }
    }   
    protected void imgbtnContactUs_Click(object sender, ImageClickEventArgs e)
    {
        Session["SubCtrl"] = "ContactUs.ascx";
        Response.Redirect("FJAHome.aspx");        
    }
    protected void lbtnBack_Click(object sender, EventArgs e)
    {
        ShowCurrentdetails();
    }
    protected void lbtnBack_1_Click(object sender, EventArgs e)
    {
        Session["CurCategoryId"] = null;
       ////// pnlTestList.Controls.Clear();
       ////// pnlTestList.Controls.Add(PnlDefaultText); lbtnBack_1.Visible = false;
    }
}
