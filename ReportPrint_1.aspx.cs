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

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.ComponentModel;

public partial class ReportPrint : System.Web.UI.Page
{
    DBManagementClass clsClasses = new DBManagementClass();
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    DataTable dt = new DataTable();
    int userid = 0; int testid = 0;
    DataTable dtEmptySessionList = new DataTable();
    string scoringType; string scoretype;
    int totalQuestionMark = 0;

    DataTable dtTestSection;
    Rectangle objRectangle_3 = new Rectangle(250, 200, 50, 25);

   // DataTable dtTestSection = new DataTable();
    DataTable dtvariablevalues = new DataTable();
   // string scoretype; int userid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        testid = int.Parse(Session["UserTestID_Report"].ToString());

        if (Session["TestName"] != null)
            lblTestName.Text = Session["TestName"].ToString();
        else
        {
            var GetTestName = from testnamedet in dataclass.TestLists
                              where testnamedet.TestId == testid
                              select testnamedet;
            if (GetTestName.Count() > 0)
            {
                if (GetTestName.First().TestName != null)
                {
                    lblTestName.Text = GetTestName.First().TestName.ToString();
                    Session["TestName"] = GetTestName.First().TestName.ToString();
                }
            }
        }

        userid = int.Parse(Session["UserId_Report"].ToString());
        if (Session["ScoringType"] != null)
            scoretype = Session["ScoringType"].ToString();
        if (Session["totalparts"] != null)
            txtParts.Text = Session["totalparts"].ToString();
        if (Session["displayvalues"] != null)
            txtValues.Text = Session["displayvalues"].ToString();
        if (Session["testsectionreportvalues"] != null)
            dtTestSection = (DataTable)Session["testsectionreportvalues"];

        FillUserReportDetails(); FillReportDescriptionDetails();

        if (Session["variablereportvalues"] != null)
        {
            dtvariablevalues = (DataTable)Session["variablereportvalues"];
            GridView1.DataSource = dtvariablevalues;
            GridView1.DataBind();
        }
        if (Session["ReportType"].ToString() == "TestSectionwise")
        {
            DrawPieGraph_TestSecwise("imgReportGraph1_" + userid + "_" + DateTime.Now.Millisecond.ToString() + ".jpg");
            DrawBarGraph();
           // return;
        }
        else
        {
            DrawPieGraph("imgReportGraph1_" + userid + "_" + DateTime.Now.Millisecond.ToString() + ".jpg", 1);
            DrawBarGraph();
        }
        this.Dispose();
    }
    private void FillUserReportDetails()
    {
        Table tblDisplay = new Table();
        TableCell tblCell = new TableCell();
        TableRow tblRow;
        Label label;
        tblDisplay = new Table();
        //return;

        //string connectionstring = ConfigurationManager.ConnectionStrings["talentscoutConnectionString"].ConnectionString;
        //conn = new SqlConnection(connectionstring);
        //conn.Open();
        string querystring = "SELECT Name, OrganizationName,JobCategory,GroupName,Designation FROM View_UserProfileDetails WHERE UserID =  " + userid;
        //      string querystring = "SELECT Name, Organization,JobCategory,GroupName,Designation FROM UserProfileView WHERE UserID =  " + userid;

        //cmd = new SqlCommand(querystring, conn);
        //da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        //da.Fill(ds);
        ds = clsClasses.GetValuesFromDB(querystring);
        tblDisplay.CellSpacing = 0;
        tblDisplay.CellPadding = 2;
        tblDisplay.BorderWidth = 2;
        //tblDisplay.Width = 400;

        if (ds != null)
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblRow = new TableRow();
                    tblCell = new TableCell();
                    //tblCell.Width = 200;
                    tblCell.ColumnSpan = 2;
                    tblCell.BackColor = Color.LightBlue;
                    label = new Label();
                    label.Text = "Candidate’s DemographicDetails";
                    label.Font.Size = 14;

                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblCell.Style.Value = "text-align: center; vertical-align: middle";
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    int tableftcellwidth = 100;
                    int tabrightcellwidth = 350;

                    //for (int count = 0; count < 5; count++)
                    //{
                    //    switch (count)
                    //    {
                    //        case 0:
                    //            {

                    tblRow = new TableRow();
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2; //tblCell.Style.Add("padding-left:", "20px");
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; Name";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.Width = tabrightcellwidth;
                    tblCell.BorderWidth = 2;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["Name"].ToString();
                    lblName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //        break;
                    //    }
                    //case 1:
                    //    {
                    tblRow = new TableRow();
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; Organization";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["OrganizationName"].ToString();

                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //        break;
                    //    }
                    //case 2:
                    //    {
                    tblRow = new TableRow();
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; JobCategory";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["JobCategory"].ToString();

                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //        break;
                    //    }
                    //case 3:
                    //    {
                    tblRow = new TableRow();
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; Designation";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["Designation"].ToString();

                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //        break;
                    //    }
                    //case 4:
                    //    {
                    tblRow = new TableRow();
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; GroupName";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["GroupName"].ToString();

                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //            break;
                    //        }
                }
        tcellUserDetails.Controls.Clear();
        tcellUserDetails.Controls.Add(tblDisplay);
        // }
        //}
    }

    private void FillReportDescriptionDetails()
    {
        lblReportDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");//.ToShortDateString();

        var SummaryDetails1 = from SummaryDetails in dataclass.ReportDescriptions
                              where SummaryDetails.TestId == testid
                              select SummaryDetails;
        if (SummaryDetails1.Count() > 0)
        {
            if (SummaryDetails1.First().Summary1 != null)
                //lblSummary1.Text = SummaryDetails1.First().Summary1.ToString();  
                tcellDescription1.InnerHtml = SummaryDetails1.First().Summary1.ToString();
            if (SummaryDetails1.First().Summary2 != null)
                //lblSummary2.Text = SummaryDetails1.First().Summary2.ToString();
                tcellDescription2.InnerHtml=SummaryDetails1.First().Summary2.ToString();
            if (SummaryDetails1.First().DescriptiveReport != null)
                tcellDescriptionReport.InnerHtml = SummaryDetails1.First().DescriptiveReport.ToString();
            if (SummaryDetails1.First().Conclusion != null)
                //lblConclusion.Text = SummaryDetails1.First().Conclusion.ToString();
                tcellConclution.InnerHtml=SummaryDetails1.First().Conclusion.ToString();
            if (SummaryDetails1.First().ScoringType != null)
                scoringType = SummaryDetails1.First().ScoringType.ToString();
        }

    }

    private void DrawPieGraph(string filename, int index)
    {        
        if (txtParts.Text.Trim() == "" || txtValues.Text.Trim() == "") return;
        Font fontTitle = new Font("Verdana", 8, FontStyle.Regular);
        Bitmap objBitmap = new Bitmap(350, 350);        
        Graphics objGraphic = Graphics.FromImage(objBitmap);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush objBrush = new SolidBrush(Color.Aqua);
        SolidBrush blackBrush = new SolidBrush(Color.DarkGray);
        SolidBrush objBrushText = new SolidBrush(Color.Brown);
        SolidBrush blueBrush = new SolidBrush(Color.BlueViolet);
        SolidBrush blueBrush1 = new SolidBrush(Color.BlanchedAlmond);
        Pen blackPen = new Pen(Color.Black, 2); Pen outerPen = new Pen(Color.Maroon, 1);
        Single startAngle = 0;
        float sweepAngle = 0;
        float total = 0;
        Random rand = new Random();
        objGraphic.SmoothingMode = SmoothingMode.Default;// SmoothingMode.AntiAlias;
        objGraphic.FillRectangle(whiteBrush, 0, 0, 350, 350);
        objGraphic.FillEllipse(blueBrush1, 5, 5, 340, 340);
        objGraphic.DrawEllipse(outerPen, 5, 5, 340, 340);
        objGraphic.FillEllipse(blackBrush, 25, 25, 300, 300);

        Table tblDisplay = new Table();
        try
        {
            bool blankspace = false;
            int i; int totalparts = 0;            
            int parts = Convert.ToInt16(txtParts.Text);            
            string[] values = txtValues.Text.Split(',');
            float newvalue = 0;
            float newtotal = 0;
            for (i = 0; i < parts; i++)
            {
                if (i < values.Length)
                    total = total + float.Parse(values[i]);
            }
            float f = 360 / total;           
            float pparts = float.Parse(parts.ToString("0.00"));
            float totcirclearea = 360;
            float width = 0;
            float width_1 = 0; width = totcirclearea / pparts;//.ToString());// parts; //width_1 = 360 / parts;
            if (totalparts > 0)
            {
                pparts = float.Parse(totalparts.ToString("0.00"));
                width = totcirclearea / pparts;// parts;
            }

            width_1 = float.Parse(width.ToString("0.00"));
            TableRow tblRow = new TableRow();
            TableCell tblCell = new TableCell();

            Label label = new Label();
            label.Text = "";
            label.Font.Size = 12;
            tblCell.Controls.Add(label);
            tblRow.Cells.Add(tblCell);

            tblDisplay.Rows.Add(tblRow);
            int curIndex = 0;
            if (parts == values.Length)
            {                
                for (int j = 0; j < parts; j++)
                {
                    curIndex = j;
                    objBrush.Color = GetFillColour(j); //Color.Red;// 
                    if (j >= 20)
                        objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));

                    int benchmark = 0;
                    int orgBenchmark = 0;

                    if (j < GridView1.Rows.Count)
                    {
                        if (j == 0)
                        {
                            SolidBrush objBenchMark = new SolidBrush(Color.White);
                            tblRow = new TableRow();
                            tblCell = new TableCell();
                            tblCell.Style.Value = "text-align: right; font-weight: bold";
                            tblCell.BackColor = blackBrush.Color;
                            label = new Label();
                            label.Width = 50; label.Height = 5;
                            label.BackColor = objBenchMark.Color;//.White; 
                            //label.Text = "BenchMark";
                            tblCell.Controls.Add(label);
                            tblRow.Cells.Add(tblCell);
                            tblCell = new TableCell();
                            label = new Label();
                            label.Text = "Bench Mark";
                            label.Font.Size = 12;
                            tblCell.Controls.Add(label);
                            tblRow.Cells.Add(tblCell);
                            tblDisplay.Rows.Add(tblRow);
                        }

                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.Style.Value = "text-align: right";//; font-weight: bold";
                        label = new Label();
                        label.Width = 50;
                        //label.BackColor = objBrush.Color;
                        label.Text = (j + 1).ToString() + ".";
                        tblCell.Controls.Add(label);
                        tblRow.Cells.Add(tblCell);
                        tblCell = new TableCell();
                        label = new Label();
                        label.Text = GridView1.Rows[j].Cells[1].Text + "=" + GridView1.Rows[j].Cells[4].Text;// +" % ";
                        if (scoretype == "Percentage") label.Text += " % ";
                        label.Font.Size = 12;
                        tblCell.Controls.Add(label);
                        tblRow.Cells.Add(tblCell);
                        tblDisplay.Rows.Add(tblRow);

                        benchmark = GetBenchMarkVariablewise(GridView1.Rows[j].Cells[1].Text, GridView1.Rows[j].Cells[4].Text, GridView1.Rows[j].Cells[2].Text);
                        orgBenchmark = benchmark;
                    }
                    string height_3 = values[j];
                    int height_1 = 0;
                    double tValue = 300 * 300 * float.Parse(height_3) / 100;
                    double height_22 = Math.Sqrt(tValue);// / 2 * 90000 * float.Parse(height_3) / 100;

                    string heightval = height_22.ToString();// height_2.ToString();
                    string[] heightval_1 = heightval.Split(new char[] { '.' });

                    height_1 = int.Parse(heightval_1[0]);

                    int xaxis = 0, yaxis = 0;
                    //xaxis = GetrectWidth(height_3);

                    float hgt = float.Parse(height_3);
                    //if(j>0)
                    xaxis = GetRectWidth(height_1);                    

                    int newxaxis = 0;
                    
                    double newbenchmark = 300 * 300 * benchmark / 100;
                    double newbenchmark_1 = Math.Sqrt(newbenchmark);

                    string strbenchmark = newbenchmark_1.ToString();
                    string[] strbench = strbenchmark.Split(new char[] { '.' });
                    benchmark = int.Parse(strbench[0]);
                    newxaxis = GetRectWidth(benchmark);

                    Pen pen5 = new Pen(Color.Black, 2);
                    Rectangle objRectangle3 = new Rectangle(xaxis, xaxis, height_1, height_1);
                    //// Rectangle objRectangle3 = new Rectangle(xaxis, 90,height_1, height_1); 
                    if (height_1 <= 0)
                    {
                        objRectangle3 = new Rectangle(25, 25, 300, 300);
                        objBrush.Color = blackBrush.Color;// Color.White; //lblMessage.Text += "height=" + height_1.ToString();
                    }
                    objGraphic.FillPie(objBrush, objRectangle3, startAngle, width_1);
                    //objGraphic.DrawString(j.ToString(), fontTitle, objBrush, objRectangle3);
                    if (benchmark > 0)
                    {
                        objRectangle3 = new Rectangle(newxaxis, newxaxis, benchmark, benchmark);
                        //Pen penBenchmark = new Pen(Color.Violet, 2);
                        Pen penBenchmark = new Pen(Color.White, 2);
                        objGraphic.DrawArc(penBenchmark, objRectangle3, startAngle, width_1);
                        if (orgBenchmark > 0)
                        {
                            SetBenchMarkPossition(parts, j);
                            objBrush.Color = Color.Black;
                            objGraphic.DrawString(orgBenchmark.ToString(), fontTitle, objBrush, objRectangle_3);//objRectangle3);// 
                        }
                    }
                    SetRectanclePossition(parts, j);
                    objBrush.Color = Color.Black;
                    objGraphic.DrawString((j + 1).ToString(), fontTitle, objBrush, objRectangle_3);

                    objRectangle3 = new Rectangle(25, 25, 300, 300);
                    objGraphic.DrawPie(pen5, objRectangle3, startAngle, width_1);

                    startAngle = startAngle + width_1;
                }
            }
            else
                lblMessage.Text = "Please enter " + txtParts.Text + " values only";
        }
        catch (Exception ex)
        {
            lblMessage.Text = " error .." + ex.Message;// "Please enter " + txtParts.Text + " values only";
        }

        tcelGraphHelp.Controls.Add(tblDisplay);
        imgGraph.ImageUrl = "";

        // code to clear graph images  // bip 19-12-2009
        string[] files = Directory.GetFiles(Server.MapPath("Images\\graphFiles"));
        foreach (string file in files)
            File.Delete(file);
        //

        objBitmap.Save(Server.MapPath("Images\\graphFiles\\" + filename), ImageFormat.Jpeg);
        objGraphic.Dispose();
        objBitmap.Dispose();
        imgGraph.ImageUrl = "~/Images/graphFiles/" + filename;        
    }

    private void DrawPieGraph_TestSecwise(string filename)
    {
        if (txtParts.Text.Trim() == "" || txtValues.Text.Trim() == "") return;
        Font fontTitle = new Font("Verdana", 8, FontStyle.Regular);
        Bitmap objBitmap = new Bitmap(350, 350);        
        Graphics objGraphic = Graphics.FromImage(objBitmap);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush objBrush = new SolidBrush(Color.Aqua);
        SolidBrush blackBrush = new SolidBrush(Color.DarkGray);
        SolidBrush objBrushText = new SolidBrush(Color.Brown);
        SolidBrush blueBrush = new SolidBrush(Color.BlueViolet);
        SolidBrush blueBrush1 = new SolidBrush(Color.BlanchedAlmond);
        Pen blackPen = new Pen(Color.Black, 2); Pen outerPen = new Pen(Color.Maroon, 1);
        Single startAngle = 0;
        float sweepAngle = 0;
        float total = 0;
        Random rand = new Random();
        objGraphic.SmoothingMode = SmoothingMode.Default;// SmoothingMode.AntiAlias;
        objGraphic.FillRectangle(whiteBrush, 0, 0, 350, 350);
        objGraphic.FillEllipse(blueBrush1, 5, 5, 340, 340);
        objGraphic.DrawEllipse(outerPen, 5, 5, 340, 340);
        objGraphic.FillEllipse(blackBrush, 25, 25, 300, 300);

        Table tblDisplay = new Table();
        try
        {
            bool blankspace = false;
            int i; int totalparts = 0;
            //lblMessage.Text = "";
            int parts = Convert.ToInt16(txtParts.Text);
            
            string[] values = txtValues.Text.Split(',');
            float newvalue = 0;
            float newtotal = 0;

            for (i = 0; i < parts; i++)
            {
                total = total + float.Parse(values[i]);
            }

            float f = 360 / total;
            //parts = 14;
            float pparts = float.Parse(parts.ToString("0.00"));
            float totcirclearea = 360;
            float width = 0;
            float width_1 = 0; width = totcirclearea / pparts;//.ToString());// parts; //width_1 = 360 / parts;
            if (totalparts > 0)
            {
                pparts = float.Parse(totalparts.ToString("0.00"));
                width = totcirclearea / pparts;// parts;
            }

            width_1 = float.Parse(width.ToString("0.00"));
            TableRow tblRow = new TableRow();
            TableCell tblCell = new TableCell();

            Label label = new Label();
            label.Text = "";
            label.Font.Size = 12;
            tblCell.Controls.Add(label);
            tblRow.Cells.Add(tblCell);

            tblDisplay.Rows.Add(tblRow);

            if (parts == values.Length)
            {               
                for (int j = 0; j < parts; j++)
                {
                    objBrush.Color = GetFillColour(j);                    
                    int benchmark = 0, orgBenchmark = 0;

                    if (j < dtTestSection.Rows.Count)
                    {
                        if (j == 0)
                        {
                            SolidBrush objBenchMark = new SolidBrush(Color.White);
                            tblRow = new TableRow();
                            tblCell = new TableCell();
                            tblCell.Style.Value = "text-align: right;";// font-weight: bold";
                            tblCell.BackColor = blackBrush.Color;
                            label = new Label();
                            label.Width = 50; //label.Height = 2;
                            label.BackColor = objBenchMark.Color;//.White; 
                            //label.Text = "BenchMark";
                            tblCell.Controls.Add(label);
                            tblRow.Cells.Add(tblCell);
                            tblCell = new TableCell();
                            label = new Label();
                            label.Text = "Bench Mark";
                            label.Font.Size = 12;
                            tblCell.Controls.Add(label);
                            tblRow.Cells.Add(tblCell);
                            tblDisplay.Rows.Add(tblRow);
                        }


                        tblRow = new TableRow();
                        tblCell = new TableCell(); tblCell.Style.Value = "text-align: right";
                        label = new Label();
                        label.Width = 50;
                        //label.BackColor = objBrush.Color;
                        label.Text = (j + 1).ToString() + ".";
                        tblCell.Controls.Add(label);
                        tblRow.Cells.Add(tblCell);
                        tblCell = new TableCell();
                        label = new Label();
                        int testsecid = int.Parse(dtTestSection.Rows[j][0].ToString());
                        string testsectionname = "";
                        testsectionname = GetTestSectionName(testsecid);
                        label.Text = testsectionname + "=" + dtTestSection.Rows[j][1].ToString();// +" % ";
                        if (scoretype == "Percentage") label.Text += " % ";
                        label.Font.Size = 12;
                        tblCell.Controls.Add(label);
                        tblRow.Cells.Add(tblCell);
                        tblDisplay.Rows.Add(tblRow);

                        benchmark = GetBenchMarkTestSectionwise(dtTestSection.Rows[j]["TotalMark_sec"].ToString(), dtTestSection.Rows[j]["TestSectionID"].ToString());
                        orgBenchmark = benchmark;

                    }


                    string height_3 = values[j];
                    int height_1 = 0;// int.Parse(values[j]);// + (j + 5);

                    double tValue = 300 * 300 * float.Parse(height_3) / 100;
                    double height_22 = Math.Sqrt(tValue);// / 2 * 90000 * float.Parse(height_3) / 100;

                    string heightval = height_22.ToString();// height_2.ToString();
                    string[] heightval_1 = heightval.Split(new char[] { '.' });

                    height_1 = int.Parse(heightval_1[0]);

                    int xaxis = 0, yaxis = 0;
                    //xaxis = GetrectWidth(height_3);

                    float hgt = float.Parse(height_3);
                    xaxis = GetRectWidth(height_1);

                    int newxaxis = 0;
                    // float newbenchmark =float.Parse( benchmark.ToString()) / 100 * 300;
                    double newbenchmark = 300 * 300 * benchmark / 100;
                    double newbenchmark_1 = Math.Sqrt(newbenchmark);

                    string strbenchmark = newbenchmark_1.ToString();
                    string[] strbench = strbenchmark.Split(new char[] { '.' });
                    benchmark = int.Parse(strbench[0]);
                    newxaxis = GetRectWidth(benchmark);

                    Pen pen5 = new Pen(Color.Black, 2);
                    Rectangle objRectangle3 = new Rectangle(xaxis, xaxis, height_1, height_1);
                    if (height_1 <= 0)
                    {
                        objRectangle3 = new Rectangle(25, 25, 300, 300);
                        objBrush.Color = blackBrush.Color;// Color.White; //lblMessage.Text += "height=" + height_1.ToString();
                    }
                    objGraphic.FillPie(objBrush, objRectangle3, startAngle, width_1);
                    if (benchmark > 0)
                    {
                        objRectangle3 = new Rectangle(newxaxis, newxaxis, benchmark, benchmark);
                        //Pen penBenchmark = new Pen(Color.Violet, 2);
                        Pen penBenchmark = new Pen(Color.White, 2);
                        objGraphic.DrawArc(penBenchmark, objRectangle3, startAngle, width_1);

                        if (orgBenchmark > 0)
                        {
                            SetBenchMarkPossition(parts, j);
                            objBrush.Color = Color.Black;
                            objGraphic.DrawString(orgBenchmark.ToString(), fontTitle, objBrush, objRectangle_3);//objRectangle3);// 
                        }
                    }

                    SetRectanclePossition(parts, j);
                    objBrush.Color = Color.Black;
                    objGraphic.DrawString((j + 1).ToString(), fontTitle, objBrush, objRectangle_3);
                    objRectangle3 = new Rectangle(25, 25, 300, 300);
                    objGraphic.DrawPie(pen5, objRectangle3, startAngle, width_1);
                    startAngle = startAngle + width_1;
                }
            }
            else
                lblMessage.Text = "Please enter " + txtParts.Text + " values only";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;// "Please enter " + txtParts.Text + " values only";
        }

        tcelGraphHelp.Controls.Add(tblDisplay);
        imgGraph.ImageUrl = "";
        // code to clear graph images  // bip 19-12-2009
        string[] files = Directory.GetFiles(Server.MapPath("Images\\graphFiles"));
        foreach (string file in files)
            File.Delete(file);
        //

        objBitmap.Save(Server.MapPath("Images\\" + filename), ImageFormat.Jpeg);
        objGraphic.Dispose();
        objBitmap.Dispose();
        imgGraph.ImageUrl = "~/Images/" + filename;       
    }

    private void DrawBarGraph()
    {
        Table tblDisplay = new Table();
        TableCell tblCell = new TableCell();
        TableRow tblRow;
        Label label;
        int i = 0;
        int rowid = 0;
        int totalmarks = 0;
        int sectionid = 0;
        // code to draw section(variable)wise bargraph 

        //tblDisplay.Width = 650;
        tblDisplay.CellPadding = 0;
        tblDisplay.CellSpacing = 0;
        tblDisplay.BorderWidth = 0;
        string mark = "0"; string benchmark = "";

        // lblMessage.Text += " bargrphParts= " + GridView1.Rows.Count.ToString() + " bargrphValues= ";
        string bargrapgimagename="imgReportBarGraph_" + userid + "_" + DateTime.Now.Millisecond.ToString();// + ".jpg"
        for (int j = 0; j < GridView1.Rows.Count; j++)
        {

            if (GridView1.Rows[j].Cells[1].Text != "&nbsp;")
            {
                string TESTSECTIONID = GridView1.Rows[j].Cells[2].Text;
                mark = "0";
                if (GridView1.Rows[j].Cells[4].Text != "0" && GridView1.Rows[j].Cells[4].Text != "&nbsp;")
                    mark = GridView1.Rows[j].Cells[4].Text;
                //  lblMessage.Text += " , " + mark;
                //lblMessage.Text += "," + mark;

                //float totalMarksectionwise = 0, currentmark = 0;
                //totalMarksectionwise = GetSectionwiseTotalQuestionMarks(GridView1.Rows[j].Cells[1].Text);

                //currentmark = (mark / totalMarksectionwise) * 100;
                //currentmark =float.Parse( currentmark.ToString("0.00"));
                ////TestVariableResultBands
                string remarks = ""; string querystring1 = "";
                benchmark = "";
                int secid = GetSectionId(GridView1.Rows[j].Cells[1].Text);
                if (mark == "0")
                {
                    querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid + " AND TestSectionId = " + TESTSECTIONID;
                    querystring1 += " AND (0 >= MarkFrom AND  0 <= MarkTo)";//   MarkFrom <= " + mark + " AND  MarkTo >= " + mark;
                    querystring1 += " AND VariableId = " + secid + "";
                }
                else
                {
                    querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid + " AND TestSectionId = " + TESTSECTIONID;
                    querystring1 += " AND (" + mark + " > MarkFrom AND  " + mark + " <= MarkTo)";//   MarkFrom <= " + mark + " AND  MarkTo >= " + mark;
                    querystring1 += " AND VariableId = " + secid + "";
                }

                DataSet ds1 = new DataSet(); bool benchmarkexists = false;
                ds1 = clsClasses.GetValuesFromDB(querystring1);
                if (ds1 != null)
                    if (ds1.Tables.Count > 0)
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            if (ds1.Tables[0].Rows[0]["BenchMark"] != "")
                                benchmark = ds1.Tables[0].Rows[0]["BenchMark"].ToString();
                            remarks = ds1.Tables[0].Rows[0]["DisplayName"].ToString();
                            benchmarkexists = true;
                        }
                //mark = ((mark * 100) / 360);

                // lblMessage.Text += querystring1; ////return;//021209 bip

                if (benchmarkexists == false)
                {

                    if (mark == "0")
                    {
                        querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestSectionResultBands WHERE TestID = " + testid;
                        querystring1 += " AND (0 >= MarkFrom AND 0 <= MarkTo)";
                        querystring1 += " AND SectionId = " + TESTSECTIONID;
                    }
                    else
                    {
                        querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestSectionResultBands WHERE TestID = " + testid;
                        querystring1 += " AND (" + mark + " > MarkFrom AND  " + mark + " <= MarkTo)";//   MarkFrom <= " + mark + " AND  MarkTo >= " + mark;
                        querystring1 += " AND SectionId = " + TESTSECTIONID;
                    }
                    ds1 = new DataSet();
                    ds1 = clsClasses.GetValuesFromDB(querystring1);
                    if (ds1 != null)
                        if (ds1.Tables.Count > 0)
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                if (ds1.Tables[0].Rows[0]["BenchMark"].ToString() != "")
                                    benchmark = ds1.Tables[0].Rows[0]["BenchMark"].ToString();
                                remarks = ds1.Tables[0].Rows[0]["DisplayName"].ToString();

                            }
                }



                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.ColumnSpan = 2;
                label = new Label();
                label.Font.Size = 15;
                label.Font.Bold = true;
                label.Text = GridView1.Rows[j].Cells[1].Text;

                tblCell.Controls.Add(label);
                //tblCell.Style.Value = "text-align: center; vertical-align: middle";
                tblRow.Cells.Add(tblCell);
                tblDisplay.Rows.Add(tblRow);

                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.ColumnSpan = 2;
                label = new Label();
                label.Font.Size = 15;
                label.Font.Bold = true;
                label.Text = "";
                label.Width = 30;
                tblCell.Controls.Add(label);
                //tblCell.Style.Value = "text-align: center; vertical-align: middle";
                tblRow.Cells.Add(tblCell);
                tblDisplay.Rows.Add(tblRow);

                //tblCell.BackColor = Color.LightBlue;

                tblRow = new TableRow();
                tblCell = new TableCell(); //tblCell.ColumnSpan = 2;
                ///////////
                label = new Label();
                //label.BackColor = Color.Green;//.LightBlue;
                ////mark = mark.ToString("0");
                double dblmark = double.Parse(mark);
                dblmark = dblmark * 5;
                label.Width = 10;// int.Parse(dblmark.ToString("0"));
                label.Height = 30;
                //////////////
                HtmlImage imgbargraph = new HtmlImage();

                int grphwidth = int.Parse(dblmark.ToString("0"));
                int grphheight = 30;
                Bitmap objBitmap = new Bitmap(grphheight, grphheight);
                Graphics objGraphic = Graphics.FromImage(objBitmap);
                SolidBrush greenBrush = new SolidBrush(Color.Green);
                
                objGraphic.SmoothingMode = SmoothingMode.Default;// SmoothingMode.AntiAlias;
                if (dblmark > 0)
                {
                    objBitmap = new Bitmap(grphwidth, grphheight);
                     objGraphic = Graphics.FromImage(objBitmap);
                     greenBrush = new SolidBrush(Color.Green);
                
                    objGraphic.FillRectangle(greenBrush, 0, 0, grphwidth, grphheight);
                    //objGraphic.FillEllipse(blueBrush1, 5, 5, 340, 340);
                    //objGraphic.DrawEllipse(outerPen, 5, 5, 340, 340);
                    //objGraphic.FillEllipse(blackBrush, 25, 25, 300, 300);
                    objBitmap.Save(Server.MapPath("Images\\graphFiles\\" + bargrapgimagename + "_" + (j * 2).ToString() + ".jpg"), ImageFormat.Jpeg);
                    objGraphic.Dispose();
                    objBitmap.Dispose();
                    imgbargraph.Src = "~/Images/graphFiles/" + bargrapgimagename + "_" + (j * 2).ToString() + ".jpg";
                    tblCell.Controls.Add(imgbargraph);
                }
                else tblCell.Controls.Add(label);
                /////////////
               // tblCell.Controls.Add(label);
                tblCell.Style.Value = "text-align: left; vertical-align: middle";
                tblRow.Cells.Add(tblCell);

                tblCell = new TableCell();
                label = new Label();
                label.Text = "Your Score =" + mark;// mark.ToString();
                if (scoretype == "Percentage")
                    if (int.Parse(dblmark.ToString("0")) > 0)
                        label.Text = label.Text + " % ";
                tblCell.Controls.Add(label);
                //tblCell.Style.Value = "text-align: center; vertical-align: middle";
                tblRow.Cells.Add(tblCell);

                tblDisplay.Rows.Add(tblRow);

                //display name
                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.ColumnSpan = 2;
                label = new Label();
                label.Text = "";
                tblCell.Controls.Add(label);
                tblCell.Style.Value = "text-align: left; vertical-align: middle";
                tblRow.Cells.Add(tblCell);

                //

                tblRow = new TableRow();
                tblCell = new TableCell();
                ///////
                //tblCell.BackColor = Color.LightBlue;
                label = new Label();
                //label.BackColor = Color.Blue;//.Brown;
                //if (benchmark != "")
                    label.Width = 10;// (int.Parse(benchmark) * 5);
                label.Height = 30;
               // tblCell.Controls.Add(label);

                ////////
                if (benchmark != "")
                {
                    imgbargraph = new HtmlImage();
                    grphwidth = (int.Parse(benchmark) * 5);
                    objBitmap = new Bitmap(grphwidth, grphheight);
                    objGraphic = Graphics.FromImage(objBitmap);
                    SolidBrush blueBrush = new SolidBrush(Color.Blue);

                    objGraphic.SmoothingMode = SmoothingMode.Default;// SmoothingMode.AntiAlias;
                    objGraphic.FillRectangle(blueBrush, 0, 0, grphwidth, grphheight);
                    //objGraphic.FillEllipse(blueBrush1, 5, 5, 340, 340);
                    //objGraphic.DrawEllipse(outerPen, 5, 5, 340, 340);
                    //objGraphic.FillEllipse(blackBrush, 25, 25, 300, 300);
                    objBitmap.Save(Server.MapPath("Images\\graphFiles\\" + bargrapgimagename + "_" + ((j * 2)+1).ToString() + ".jpg"), ImageFormat.Jpeg);
                    objGraphic.Dispose();
                    objBitmap.Dispose();
                    imgbargraph.Src = "~/Images/graphFiles/" + bargrapgimagename + "_" + ((j * 2) + 1).ToString() + ".jpg";
                    tblCell.Controls.Add(imgbargraph);
                }
                else tblCell.Controls.Add(label);
                ///////
                tblCell.Style.Value = "text-align: left; vertical-align: middle";
                tblRow.Cells.Add(tblCell);

                tblCell = new TableCell();
                label = new Label();
                if (benchmark != "")
                    label.Text = "Benchmark=" + benchmark;

                tblCell.Controls.Add(label);
                //tblCell.Style.Value = "text-align: center; vertical-align: middle";
                tblRow.Cells.Add(tblCell);

                tblDisplay.Rows.Add(tblRow);

                //ImageUrl = "~/Images/Scale1.jpg";

                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.ColumnSpan = 2;
                System.Web.UI.WebControls.Image imgScale = new System.Web.UI.WebControls.Image();
                imgScale.ImageUrl = "~/Images/ReportScale.jpg"; //"~/Images/Scale1.jpg";
                tblCell.Controls.Add(imgScale);
                //tblCell.Style.Value = "text-align: center; vertical-align: middle";
                tblRow.Cells.Add(tblCell);
                tblDisplay.Rows.Add(tblRow);

                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.ColumnSpan = 2;
                label = new Label();
                label.Font.Size = 15;
                label.Font.Bold = true;
                if (scoretype == "Percentage")
                    label.Text = "Percentage Score";
                else label.Text = "Percentile Score";

                //label.Width = 30;
                tblCell.Controls.Add(label);
                tblCell.Style.Value = "text-align: center; vertical-align: middle";
                tblRow.Cells.Add(tblCell);
                tblDisplay.Rows.Add(tblRow);


                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.ColumnSpan = 2;
                label = new Label();
                label.Font.Size = 12;
                //label.Font.Bold = true;
                label.Text = remarks;
                //label.Width = 30;
                tblCell.Controls.Add(label);
                tblCell.Style.Value = "text-align: left; vertical-align: middle";
                tblRow.Cells.Add(tblCell);
                tblDisplay.Rows.Add(tblRow);

                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.ColumnSpan = 2;
                label = new Label();
                label.Font.Size = 15;
                label.Font.Bold = true;
                label.Text = "";
                label.Width = 30;
                tblCell.Controls.Add(label);
                //tblCell.Style.Value = "text-align: center; vertical-align: middle";
                tblRow.Cells.Add(tblCell);
                tblDisplay.Rows.Add(tblRow);
            }
        }
        
        if (dtEmptySessionList.Rows.Count > 0)
        {
            for (int k = 0; k < dtEmptySessionList.Rows.Count; k++)
            {
                //TestVariableResultBands
                string remarks = "";
                benchmark = "";

                int secid = GetSectionId(dtEmptySessionList.Rows[k][0].ToString());

                string querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid;
                querystring1 += " AND (0 >= MarkFrom AND 0 <= MarkTo)";
                querystring1 += " AND VariableId = " + secid;
                DataSet ds1 = new DataSet();
                ds1 = clsClasses.GetValuesFromDB(querystring1);
                if (ds1 != null)
                    if (ds1.Tables.Count > 0)
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            if (ds1.Tables[0].Rows[0]["BenchMark"] != "")
                                benchmark = ds1.Tables[0].Rows[0]["BenchMark"].ToString();
                            remarks = ds1.Tables[0].Rows[0]["DisplayName"].ToString();
                        }

                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.ColumnSpan = 2;
                label = new Label();
                label.Font.Size = 15;
                label.Font.Bold = true;
                label.Text = dtEmptySessionList.Rows[k][0].ToString();

                tblCell.Controls.Add(label);
                //tblCell.Style.Value = "text-align: center; vertical-align: middle";
                tblRow.Cells.Add(tblCell);
                tblDisplay.Rows.Add(tblRow);

                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.ColumnSpan = 2;
                label = new Label();
                label.Font.Size = 15;
                label.Font.Bold = true;
                label.Text = "";
                label.Width = 30;
                tblCell.Controls.Add(label);
                //tblCell.Style.Value = "text-align: center; vertical-align: middle";
                tblRow.Cells.Add(tblCell);
                tblDisplay.Rows.Add(tblRow);


                //tblCell.BackColor = Color.LightBlue;

                tblRow = new TableRow();
                tblCell = new TableCell();
                label = new Label();
                label.BackColor = Color.Green;//.LightBlue;
                label.Width = 0;
                label.Height = 30;
                tblCell.Controls.Add(label);
                tblCell.Style.Value = "text-align: left; vertical-align: middle";
                tblRow.Cells.Add(tblCell);

                tblCell = new TableCell();
                label = new Label();
                label.Text = "Your Score =0";

                tblCell.Controls.Add(label);
                //tblCell.Style.Value = "text-align: center; vertical-align: middle";
                tblRow.Cells.Add(tblCell);

                tblDisplay.Rows.Add(tblRow);

                tblRow = new TableRow();
                tblCell = new TableCell();
                //tblCell.BackColor = Color.LightBlue;
                label = new Label();
                label.BackColor = Color.Blue;//.Brown;
                if (benchmark != "")
                    label.Width = (int.Parse(benchmark) * 5);
                label.Height = 30;
                tblCell.Controls.Add(label);
                tblCell.Style.Value = "text-align: left; vertical-align: middle";
                tblRow.Cells.Add(tblCell);

                tblCell = new TableCell();
                label = new Label();
                if (benchmark != "")
                    label.Text = "Benchmark=" + benchmark;

                tblCell.Controls.Add(label);
                //tblCell.Style.Value = "text-align: center; vertical-align: middle";
                tblRow.Cells.Add(tblCell);

                tblDisplay.Rows.Add(tblRow);

                //
                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.ColumnSpan = 2;
                System.Web.UI.WebControls.Image imgScale = new System.Web.UI.WebControls.Image();
                imgScale.ImageUrl = "~/Images/ReportScale.jpg";
                tblCell.Controls.Add(imgScale);
                //tblCell.Style.Value = "text-align: center; vertical-align: middle";
                tblRow.Cells.Add(tblCell);
                tblDisplay.Rows.Add(tblRow);
                //

                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.ColumnSpan = 2;
                label = new Label();
                label.Font.Size = 15;
                label.Font.Bold = true;
                if (scoretype == "Percentage")
                    label.Text = "Percentage Score";
                else label.Text = "Percentile Score";
                //label.Width = 30;
                tblCell.Controls.Add(label);
                tblCell.Style.Value = "text-align: center; vertical-align: middle";
                tblRow.Cells.Add(tblCell);
                tblDisplay.Rows.Add(tblRow);


                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.ColumnSpan = 2;
                label = new Label();
                label.Font.Size = 12;
                // label.Font.Bold = true;
                label.Text = remarks;
                //label.Width = 30;
                tblCell.Controls.Add(label);
                tblCell.Style.Value = "text-align: left; vertical-align: middle";
                tblRow.Cells.Add(tblCell);
                tblDisplay.Rows.Add(tblRow);

                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.ColumnSpan = 2;
                label = new Label();
                label.Font.Size = 15;
                label.Font.Bold = true;
                label.Text = "";
                label.Width = 30;
                tblCell.Controls.Add(label);
                //tblCell.Style.Value = "text-align: center; vertical-align: middle";
                tblRow.Cells.Add(tblCell);
                tblDisplay.Rows.Add(tblRow);

            }
        }

        // tcellBarGraph.Width = "200";
        tcellBarGraph.Controls.Add(tblDisplay);
        // imgGraph.Visible = false;


    }
    private Color GetFillColour(int index)
    {
        Color currentColor = ColorTranslator.FromHtml("#FFFFFF");
        if (index == 0)
            currentColor = ColorTranslator.FromHtml("#00FF00");
        else if (index == 1)
            currentColor = ColorTranslator.FromHtml("#FF0080");
        else if (index == 2)
            currentColor = ColorTranslator.FromHtml("#0000FF");
        else if (index == 3)
            currentColor = ColorTranslator.FromHtml("#FF8000");
        else if (index == 4)
            currentColor = ColorTranslator.FromHtml("#5C5C5C");
        else if (index == 5)
            currentColor = ColorTranslator.FromHtml("#FFFF00");
        else if (index == 6)
            currentColor = ColorTranslator.FromHtml("#CBA274");
        else if (index == 7)
            currentColor = ColorTranslator.FromHtml("#008000");
        else if (index == 8)
            currentColor = ColorTranslator.FromHtml("#D51500");
        else if (index == 9)
            currentColor = ColorTranslator.FromHtml("#80FFFF");
        else if (index == 10)
            currentColor = ColorTranslator.FromHtml("#FF00FF");
        else if (index == 11)
            currentColor = ColorTranslator.FromHtml("#808000");
        else if (index == 12)
            currentColor = ColorTranslator.FromHtml("#FF8080");
        else if (index == 13)
            currentColor = ColorTranslator.FromHtml("#408080");
        else if (index == 14)
            currentColor = ColorTranslator.FromHtml("#400080");
        else if (index == 15)
            currentColor = ColorTranslator.FromHtml("#D6D6D6");
        else if (index == 16)
            currentColor = ColorTranslator.FromHtml("#004080");
        else if (index == 17)
            currentColor = ColorTranslator.FromHtml("#800000");
        else if (index == 18)
            currentColor = ColorTranslator.FromHtml("#FFFFC6");
        else if (index == 19)
            currentColor = ColorTranslator.FromHtml("#400000");

        return currentColor;
        
    }

    private int GetBenchMarkVariablewise(string variableName, string curmark, string TESTSECTIONID)
    {
        int benchmark = 0;
        string remarks = "";
        try
        {
            string[] strValue = curmark.Split(new char[] { '.' });
            int mark = int.Parse(strValue[0]);

            int secid = GetSectionId(variableName);
            string querystring1 = "";
            if (curmark == "0")
            {
                querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid + " AND TestSectionId = " + TESTSECTIONID;
                querystring1 += " AND (0 >= MarkFrom AND 0 <= MarkTo)";
                querystring1 += " AND VariableId = " + secid;
            }
            else
            {
                querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid + " AND TestSectionId = " + TESTSECTIONID;
                querystring1 += " AND (" + mark + " > MarkFrom AND  " + mark + " <= MarkTo)";//   MarkFrom <= " + mark + " AND  MarkTo >= " + mark;
                querystring1 += " AND VariableId = " + secid + "";
            }
            bool benchmarkexists = false;
            DataSet ds1 = new DataSet();
            ds1 = clsClasses.GetValuesFromDB(querystring1);
            if (ds1 != null)
                if (ds1.Tables.Count > 0)
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["BenchMark"] != "")
                            benchmark = int.Parse(ds1.Tables[0].Rows[0]["BenchMark"].ToString());
                        remarks = ds1.Tables[0].Rows[0]["DisplayName"].ToString();
                        benchmarkexists = true;
                    }

            if (benchmarkexists == false)
            {
                benchmark = GetBenchMarkTestSectionwise(curmark, TESTSECTIONID);
            }

            // lblMessage.Text += querystring1;

        }
        catch (Exception ex) { benchmark = 0; }

        return benchmark;
    }

    private int GetBenchMarkTestSectionwise(string curmark, string TESTSECTIONID)
    {
        int benchmark = 0;
        string remarks = "";
        try
        {
            string[] strValue = curmark.Split(new char[] { '.' });
            int mark = int.Parse(strValue[0]);

            // int secid = GetSectionId(variableName);
            string querystring1 = "";

            if (curmark == "0")
            {
                querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestSectionResultBands WHERE TestID = " + testid;
                querystring1 += " AND (0 >= MarkFrom AND 0 <= MarkTo)";
                querystring1 += " AND SectionId = " + TESTSECTIONID;
            }
            else
            {
                querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestSectionResultBands WHERE TestID = " + testid;
                querystring1 += " AND (" + mark + " > MarkFrom AND  " + mark + " <= MarkTo)";//   MarkFrom <= " + mark + " AND  MarkTo >= " + mark;
                querystring1 += " AND SectionId = " + TESTSECTIONID;
            }
            DataSet ds1 = new DataSet();
            ds1 = clsClasses.GetValuesFromDB(querystring1);
            if (ds1 != null)
                if (ds1.Tables.Count > 0)
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["BenchMark"] != "")
                            benchmark = int.Parse(ds1.Tables[0].Rows[0]["BenchMark"].ToString());
                        remarks = ds1.Tables[0].Rows[0]["DisplayName"].ToString();

                    }
        }
        catch (Exception ex) { benchmark = 0; }

        return benchmark;
    }
    private int GetRectWidth(float value)
    {
        string val = "";       
        float newval = 0;
        int widthvalue = 0;        
        newval = (300 - value) / 2 + 25; val = newval.ToString();
        string[] curval = val.Split(new char[] { '.' });
        widthvalue = int.Parse(curval[0]);
        // }

        return widthvalue;
    }

    private void SetRectanclePossition(int totalcount, int curIndex)
    {
        if (totalcount == 1)
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(180, 330, 50, 25);
            }
        }
        else if (totalcount == 20)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(325, 200, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(310, 245, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(285, 280, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(240, 310, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(185, 325, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(140, 325, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(90, 310, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(50, 280, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(25, 240, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(5, 190, 40, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(5, 140, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(15, 100, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(45, 60, 50, 25);
                else if (j == 13) objRectangle_3 = new Rectangle(90, 25, 50, 25);
                else if (j == 14) objRectangle_3 = new Rectangle(130, 10, 50, 25);
                else if (j == 15) objRectangle_3 = new Rectangle(190, 10, 50, 25);
                else if (j == 16) objRectangle_3 = new Rectangle(235, 25, 50, 25);
                else if (j == 17) objRectangle_3 = new Rectangle(275, 55, 50, 25);
                else if (j == 18) objRectangle_3 = new Rectangle(305, 90, 50, 25);
                else if (j == 19) objRectangle_3 = new Rectangle(320, 135, 50, 25);

            }
        }
        else if (totalcount == 19)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(325, 200, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(310, 245, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(270, 290, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(240, 310, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(175, 325, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(130, 320, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(75, 300, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(40, 260, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(20, 220, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(5, 160, 40, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(15, 110, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(35, 70, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(70, 35, 50, 25);
                else if (j == 13) objRectangle_3 = new Rectangle(115, 15, 50, 25);
                else if (j == 14) objRectangle_3 = new Rectangle(170, 05, 50, 25);
                else if (j == 15) objRectangle_3 = new Rectangle(225, 20, 50, 25);
                else if (j == 16) objRectangle_3 = new Rectangle(275, 45, 50, 25);
                else if (j == 17) objRectangle_3 = new Rectangle(305, 90, 50, 25);
                else if (j == 18) objRectangle_3 = new Rectangle(320, 135, 50, 25);
            }
        }
        else if (totalcount == 18)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(330, 200, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(300, 255, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(270, 290, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(220, 320, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(165, 325, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(120, 320, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(55, 280, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(30, 250, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(10, 200, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(5, 150, 40, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(20, 90, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(50, 50, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(105, 15, 50, 25);
                else if (j == 13) objRectangle_3 = new Rectangle(160, 05, 50, 25);
                else if (j == 14) objRectangle_3 = new Rectangle(220, 15, 50, 25);
                else if (j == 15) objRectangle_3 = new Rectangle(265, 40, 50, 25);
                else if (j == 16) objRectangle_3 = new Rectangle(305, 90, 50, 25);
                else if (j == 17) objRectangle_3 = new Rectangle(320, 130, 50, 25);
            }
        }
        else if (totalcount == 17)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(320, 210, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(300, 255, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(270, 290, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(210, 320, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(145, 325, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(90, 305, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(55, 280, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(20, 230, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(10, 170, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(15, 100, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(50, 55, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(90, 20, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(140, 10, 50, 25);
                else if (j == 13) objRectangle_3 = new Rectangle(200, 10, 50, 25);
                else if (j == 14) objRectangle_3 = new Rectangle(255, 35, 50, 25);
                else if (j == 15) objRectangle_3 = new Rectangle(295, 75, 50, 25);
                else if (j == 16) objRectangle_3 = new Rectangle(320, 130, 50, 25);
            }
        }
        else if (totalcount == 16)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(320, 210, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(300, 260, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(250, 305, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(200, 325, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(145, 325, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(70, 295, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(35, 260, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(10, 200, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(10, 140, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(25, 80, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(65, 35, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(130, 10, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(200, 10, 50, 25);
                else if (j == 13) objRectangle_3 = new Rectangle(255, 35, 50, 25);
                else if (j == 14) objRectangle_3 = new Rectangle(295, 75, 50, 25);
                else if (j == 15) objRectangle_3 = new Rectangle(320, 130, 50, 25);
            }
        }
        else if (totalcount == 15)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(320, 210, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(300, 260, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(240, 310, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(180, 325, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(125, 320, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(55, 280, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(25, 240, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(10, 170, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(20, 110, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(50, 50, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(110, 15, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(170, 10, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(240, 25, 50, 25);
                else if (j == 13) objRectangle_3 = new Rectangle(295, 70, 50, 25);
                else if (j == 14) objRectangle_3 = new Rectangle(320, 130, 50, 25);
            }
        }
        else if (totalcount == 14)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(320, 220, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(290, 270, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(230, 315, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(160, 325, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(90, 310, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(45, 270, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(10, 210, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(15, 130, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(40, 70, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(80, 30, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(150, 10, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(230, 20, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(285, 60, 50, 25);
                else if (j == 13) objRectangle_3 = new Rectangle(320, 130, 50, 25);
            }
        }
        else if (totalcount == 13)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(320, 220, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(280, 280, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(230, 315, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(140, 325, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(70, 295, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(30, 250, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(10, 170, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(20, 100, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(80, 30, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(150, 10, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(230, 20, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(285, 60, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(320, 130, 50, 25);
            }
        }
        else if (totalcount == 12)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(320, 220, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(280, 280, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(210, 325, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(120, 320, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(45, 270, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(15, 210, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(15, 130, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(50, 60, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(130, 10, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(200, 10, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(285, 60, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(320, 130, 50, 25);
            }
        }
        else if (totalcount == 11)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(320, 220, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(280, 280, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(190, 325, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(100, 310, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(30, 250, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(10, 170, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(35, 80, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(100, 25, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(180, 10, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(260, 40, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(320, 130, 50, 25);
            }
        }
        else if (totalcount == 2)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(180, 330, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(180, 10, 50, 25);

            }

        }
        else if (totalcount == 3)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(250, 300, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(10, 150, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(280, 50, 50, 25);

            }

        }
        else if (totalcount == 4)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(280, 280, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(40, 270, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(45, 60, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(280, 50, 50, 25);

            }

        }
        else if (totalcount == 5)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(300, 260, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(140, 325, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(10, 180, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(140, 10, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(300, 80, 50, 25);
            }
        }
        else if (totalcount == 6)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(300, 260, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(170, 325, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(20, 230, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(25, 100, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(180, 10, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(310, 90, 50, 25);

            }

        }
        else if (totalcount == 7)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(320, 230, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(210, 320, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(55, 280, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(10, 170, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(55, 50, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(210, 10, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(320, 110, 50, 25);

            }

        }
        else if (totalcount == 8)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(320, 230, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(230, 315, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(100, 310, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(20, 220, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(20, 110, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(100, 20, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(230, 20, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(320, 120, 50, 25);

            }

        }
        else if (totalcount == 9)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(320, 220, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(260, 300, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(150, 325, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(40, 260, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(10, 170, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(50, 60, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(140, 10, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(250, 30, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(320, 120, 50, 25);

            }

        }

        else if (totalcount == 10)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(320, 220, 50, 25);
                //objGraphic.DrawString("10"+j.ToString(), fontTitle, objBrush, objRectangle_3);
                else if (j == 1) objRectangle_3 = new Rectangle(260, 300, 50, 25);
                //objGraphic.DrawString("101", fontTitle, objBrush, objRectangle_3);
                else if (j == 2) objRectangle_3 = new Rectangle(165, 330, 50, 25);
                //objGraphic.DrawString("102", fontTitle, objBrush, objRectangle_3);
                else if (j == 3) objRectangle_3 = new Rectangle(60, 290, 50, 25);
                // objGraphic.DrawString("103", fontTitle, objBrush, objRectangle_3);
                else if (j == 4) objRectangle_3 = new Rectangle(10, 200, 50, 25);
                // objGraphic.DrawString("104", fontTitle, objBrush, objRectangle_3);

                else if (j == 5) objRectangle_3 = new Rectangle(10, 130, 50, 25);
                //objGraphic.DrawString("105", fontTitle, objBrush, objRectangle_3);
                else if (j == 6) objRectangle_3 = new Rectangle(65, 40, 50, 25);
                //objGraphic.DrawString("106", fontTitle, objBrush, objRectangle_3);
                else if (j == 7) objRectangle_3 = new Rectangle(160, 5, 50, 25);
                // objGraphic.DrawString("107", fontTitle, objBrush, objRectangle_3);
                else if (j == 8) objRectangle_3 = new Rectangle(270, 40, 50, 25);
                //objGraphic.DrawString("107", fontTitle, objBrush, objRectangle_3);
                else if (j == 9) objRectangle_3 = new Rectangle(320, 130, 50, 25);
                // objGraphic.DrawString("108", fontTitle, objBrush, objRectangle_3);

                //objGraphic.DrawString((j + 1).ToString(), fontTitle, objBrush, objRectangle_3);
            }
        }

    }

    private void SetBenchMarkPossition(int totalcount, int curIndex)
    {
        if (totalcount == 1)
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(160, 200, 50, 25);
            }
        }
        else if (totalcount == 20)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(270, 180, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(270, 220, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(250, 250, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(220, 280, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(180, 280, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(145, 280, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(110, 270, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(75, 255, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(60, 220, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(50, 180, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(50, 150, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(60, 120, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(80, 90, 50, 25);
                else if (j == 13) objRectangle_3 = new Rectangle(105, 60, 50, 25);
                else if (j == 14) objRectangle_3 = new Rectangle(140, 50, 50, 25);
                else if (j == 15) objRectangle_3 = new Rectangle(180, 50, 50, 25);
                else if (j == 16) objRectangle_3 = new Rectangle(210, 70, 50, 25);
                else if (j == 17) objRectangle_3 = new Rectangle(240, 90, 50, 25);
                else if (j == 18) objRectangle_3 = new Rectangle(260, 120, 50, 25);
                else if (j == 19) objRectangle_3 = new Rectangle(260, 150, 50, 25);

            }
        }
        else if (totalcount == 19)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(270, 190, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(270, 230, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(240, 260, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(210, 280, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(180, 280, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(140, 280, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(95, 270, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(75, 240, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(60, 210, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(50, 170, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(50, 130, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(70, 90, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(100, 70, 50, 25);
                else if (j == 13) objRectangle_3 = new Rectangle(130, 50, 50, 25);
                else if (j == 14) objRectangle_3 = new Rectangle(170, 50, 50, 25);
                else if (j == 15) objRectangle_3 = new Rectangle(210, 50, 50, 25);
                else if (j == 16) objRectangle_3 = new Rectangle(240, 80, 50, 25);
                else if (j == 17) objRectangle_3 = new Rectangle(260, 110, 50, 25);
                else if (j == 18) objRectangle_3 = new Rectangle(260, 150, 50, 25);
            }
        }
        else if (totalcount == 18)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(270, 190, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(270, 230, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(240, 260, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(210, 280, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(160, 280, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(130, 280, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(80, 260, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(60, 230, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(60, 190, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(50, 150, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(60, 110, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(90, 80, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(120, 60, 50, 25);
                else if (j == 13) objRectangle_3 = new Rectangle(160, 50, 50, 25);
                else if (j == 14) objRectangle_3 = new Rectangle(200, 60, 50, 25);
                else if (j == 15) objRectangle_3 = new Rectangle(230, 80, 50, 25);
                else if (j == 16) objRectangle_3 = new Rectangle(260, 110, 50, 25);
                else if (j == 17) objRectangle_3 = new Rectangle(260, 150, 50, 25);
            }
        }
        else if (totalcount == 17)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(270, 190, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(250, 230, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(230, 270, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(190, 280, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(150, 280, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(110, 260, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(70, 240, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(60, 210, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(60, 170, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(60, 120, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(70, 80, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(110, 60, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(150, 50, 50, 25);
                else if (j == 13) objRectangle_3 = new Rectangle(190, 60, 50, 25);
                else if (j == 14) objRectangle_3 = new Rectangle(230, 70, 50, 25);
                else if (j == 15) objRectangle_3 = new Rectangle(260, 110, 50, 25);
                else if (j == 16) objRectangle_3 = new Rectangle(260, 150, 50, 25);
            }
        }
        else if (totalcount == 16)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(270, 190, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(250, 230, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(230, 270, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(190, 280, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(150, 280, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(110, 260, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(70, 240, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(60, 190, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(60, 150, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(80, 110, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(100, 70, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(140, 50, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(190, 50, 50, 25);
                else if (j == 13) objRectangle_3 = new Rectangle(220, 75, 50, 25);
                else if (j == 14) objRectangle_3 = new Rectangle(250, 100, 50, 25);
                else if (j == 15) objRectangle_3 = new Rectangle(260, 140, 50, 25);
            }
        }
        else if (totalcount == 15)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(270, 190, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(250, 230, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(220, 270, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(170, 280, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(120, 280, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(80, 260, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(50, 220, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(60, 160, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(60, 120, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(80, 90, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(130, 50, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(170, 50, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(220, 60, 50, 25);
                else if (j == 13) objRectangle_3 = new Rectangle(250, 100, 50, 25);
                else if (j == 14) objRectangle_3 = new Rectangle(260, 140, 50, 25);
            }
        }
        else if (totalcount == 14)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(270, 200, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(240, 230, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(220, 270, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(170, 270, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(120, 270, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(70, 240, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(50, 200, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(50, 140, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(80, 100, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(110, 70, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(160, 50, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(210, 60, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(240, 100, 50, 25);
                else if (j == 13) objRectangle_3 = new Rectangle(260, 140, 50, 25);
            }
        }
        else if (totalcount == 13)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(270, 200, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(240, 250, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(200, 270, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(150, 270, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(100, 250, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(50, 230, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(50, 170, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(70, 120, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(100, 70, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(150, 50, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(200, 60, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(240, 100, 50, 25);
                else if (j == 12) objRectangle_3 = new Rectangle(260, 140, 50, 25);

            }
        }
        else if (totalcount == 12)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(270, 200, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(240, 250, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(185, 270, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(130, 270, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(75, 250, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(50, 200, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(50, 130, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(70, 80, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(130, 60, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(180, 50, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(230, 90, 50, 25);
                else if (j == 11) objRectangle_3 = new Rectangle(250, 140, 50, 25);
            }
        }
        else if (totalcount == 11)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(270, 200, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(240, 260, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(185, 270, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(120, 270, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(70, 230, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(60, 170, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(60, 110, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(110, 70, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(180, 60, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(230, 90, 50, 25);
                else if (j == 10) objRectangle_3 = new Rectangle(250, 140, 50, 25);
            }
        }

        else if (totalcount == 2)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(160, 260, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(160, 60, 50, 25);
            }
        }
        else if (totalcount == 3)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(220, 230, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(80, 170, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(220, 90, 50, 25);
            }
        }
        else if (totalcount == 4)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(220, 230, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(80, 230, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(100, 90, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(225, 120, 50, 25);
            }
        }
        else if (totalcount == 5)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(240, 210, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(140, 260, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(70, 170, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(135, 70, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(250, 100, 50, 25);
            }
        }
        else if (totalcount == 6)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(240, 210, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(160, 260, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(70, 200, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(70, 120, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(160, 70, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(250, 100, 50, 25);
            }
        }
        else if (totalcount == 7)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(260, 200, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(180, 260, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(100, 240, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(60, 160, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(100, 70, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(190, 70, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(260, 130, 50, 25);

            }

        }
        else if (totalcount == 8)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(270, 200, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(200, 260, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(120, 270, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(60, 200, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(55, 120, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(130, 70, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(190, 70, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(260, 130, 50, 25);

            }

        }

        else if (totalcount == 9)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                //SolidBrush objBrush = new SolidBrush(Color.Black);

                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(270, 200, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(220, 260, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(145, 270, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(80, 230, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(50, 170, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(80, 90, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(150, 70, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(220, 90, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(260, 140, 50, 25);

            }

        }
        else if (totalcount == 10)//ok
        {
            int j = curIndex;
            if (j >= 0)
            {
                objRectangle_3 = new Rectangle(250, 200, 50, 25);
                if (j == 0)
                    objRectangle_3 = new Rectangle(270, 200, 50, 25);
                else if (j == 1) objRectangle_3 = new Rectangle(240, 260, 50, 25);
                else if (j == 2) objRectangle_3 = new Rectangle(165, 270, 50, 25);
                else if (j == 3) objRectangle_3 = new Rectangle(90, 250, 50, 25);
                else if (j == 4) objRectangle_3 = new Rectangle(50, 200, 50, 25);
                else if (j == 5) objRectangle_3 = new Rectangle(60, 130, 50, 25);
                else if (j == 6) objRectangle_3 = new Rectangle(100, 90, 50, 25);
                else if (j == 7) objRectangle_3 = new Rectangle(160, 70, 50, 25);
                else if (j == 8) objRectangle_3 = new Rectangle(220, 90, 50, 25);
                else if (j == 9) objRectangle_3 = new Rectangle(250, 130, 50, 25);

            }
        }
    }

    private int GetSectionId(string sectionname)
    {
        int secID = 0;
        string querystring1 = "select SectionId from SectionDetail where ParentId=0 and SectionName='" + sectionname + "'";

        DataSet ds1 = new DataSet();
        ds1 = clsClasses.GetValuesFromDB(querystring1);
        if (ds1 != null)
            if (ds1.Tables.Count > 0)
                if (ds1.Tables[0].Rows.Count > 0)
                    if (ds1.Tables[0].Rows[0]["SectionId"] != "")
                        secID = int.Parse(ds1.Tables[0].Rows[0]["SectionId"].ToString());

        return secID;
    }

    private string GetTestSectionName(int testsecionID)
    {
        string secName = "";
        var testsectionname = from testsecname in dataclass.TestSectionsLists
                              where testsecname.TestSectionId == testsecionID
                              select testsecname;
        if (testsectionname.Count() > 0)
        {
            if (testsectionname.First().SectionName != null && testsectionname.First().SectionName != "")
                secName = testsectionname.First().SectionName.ToString();
        }
        return secName;
    }

}
