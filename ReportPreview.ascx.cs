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

using System.Data.SqlClient;
using System.IO;


using System.ComponentModel;




public partial class ReportPreview : System.Web.UI.UserControl
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
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["rep"] = "rep";
        //Drawellipse(); return;
       //DrawPieChart(); return;
       // DrawScaleBar(); return;//
        userid = int.Parse(Session["UserId_Report"].ToString());
        testid = int.Parse(Session["UserTestID_Report"].ToString());
        //lblName.Text = userid.ToString();
        //return;
        scoretype = Session["ScoringType"].ToString();
        //if (scoretype == "Percentile")
        //    GetPercentileScore("");
        //else

        //----------------------------------------------------------------
        if (Session["rep"] == "rep")
        {
            // new function
            GetReportGraphDetailsFromDB1();
            FillUserReportDetails();
            FillReportDescriptionDetails();
        }
        else
        {
            GetReportGraphDetailsFromDB();
            FillUserReportDetails(); 
            FillReportDescriptionDetails();
        }





        //-----------------------------------------------------------------
           
    }
    private void Drawellipse()
    {
        Bitmap objBitmap = new Bitmap(200, 200);

        Graphics objGraphic = Graphics.FromImage(objBitmap);

        SolidBrush redBrush = new SolidBrush(Color.Red);
        Pen pen = new Pen(redBrush);
        objGraphic.DrawRectangle(pen, 0, 0, 200, 200);
        objBitmap.Save(Server.MapPath("Images\\NewPiechart1.jpg"), ImageFormat.Jpeg);
        objGraphic.Dispose();
        objBitmap.Dispose();
        imgGraph.ImageUrl = "~/Images/NewPiechart1.jpg";
    }
    
    private void DrawPieChart()
    {
       
        //
       // if (txtParts.Text.Trim() == "" || txtValues.Text.Trim() == "") return;

// txtValues.Text = "10,20,30,40,50,60,70,80,90,100,110,120,130,140,150,160,170,180,190,200,210,220,230,240,250"; txtParts.Text = "25";
// txtValues.Text = "10,20,30,40,50,60,70,80,90,100,110,120,130,140,150,160,170,180,190,200,210,220,230,240"; txtParts.Text = "24";
// txtValues.Text = "10,20,30,40,50,60,70,80,90,100,110,120,130,140,150,160,170,180,190,200,210,220,230"; txtParts.Text = "23";
// txtValues.Text = "10,20,30,40,50,60,70,80,90,100,110,120,130,140,150,160,170,180,190,200,210,220"; txtParts.Text = "22";
// txtValues.Text = "10,20,30,40,50,60,70,80,90,100,110,120,130,140,150,160,170,180,190,200,210"; txtParts.Text = "21";
// txtValues.Text = "10,20,30,40,50,60,70,80,90,100,110,120,130,140,150,160,170,180,190,200"; txtParts.Text = "20";
// txtValues.Text = "10,20,30,40,50,60,70,80,90,100,110,120,130,140,150,160,170,180,190"; txtParts.Text = "19";
// txtValues.Text = "10,20,30,40,50,60,70,80,90,100,110,120,130,140,150,160,170,180"; txtParts.Text = "18";
// txtValues.Text = "10,20,30,40,50,60,70,80,90,100,110,120,130,140,150,160,170"; txtParts.Text = "17";
        txtValues.Text = "10,20,30,40,50,60,70,80,90,100,10,20,30,40,50,60"; txtParts.Text = "16";
       // txtValues.Text = "10,20,30,40,50,60,70,80,90,100,10,20,30,40,50"; txtParts.Text = "15";
        //txtValues.Text = "10,20,30,40,50,60,70,80,90,100,10,20,30,40"; txtParts.Text = "14";
        // txtValues.Text = "10,20,30,40,50,60,70,80,90,100,10,20,30"; txtParts.Text = "13";
        //txtValues.Text = "10,20,30,40,50,60,70,80,90,100,10,20"; txtParts.Text = "12";
        // txtValues.Text = "10,20,30,40,50,60,70,80,90,100,10"; txtParts.Text = "11";
       // txtValues.Text = "10,20,30,40,50,60,70,80,90,100"; txtParts.Text = "10";
        //txtValues.Text = "10,20,30,40,50,60,70,80,90"; txtParts.Text = "9";
       // txtValues.Text = "10,20,30,40,50,60,70,80"; txtParts.Text = "8";
        //txtValues.Text = "10,20,30,40,50,60,70"; txtParts.Text = "7";
        //txtValues.Text = "10,20,30,40,50,60"; txtParts.Text = "6";
        //txtValues.Text = "10,20,30,40,50"; txtParts.Text = "5";
        //txtValues.Text = "10,20,30,40"; txtParts.Text = "4";
        //txtValues.Text = "10,20,30"; txtParts.Text = "3";
        //txtValues.Text = "10,20"; txtParts.Text = "2";
        //txtValues.Text = "10"; txtParts.Text = "1";

        //txtValues.Text = "10,20,30,40,50,60"; txtParts.Text = "6";
        //txtValues.Text = "14,3,6,50";txtParts.Text = "4";
        Bitmap objBitmap = new Bitmap(350, 350);
        //Bitmap objBitmap = new Bitmap(100, 100);
        Graphics objGraphic = Graphics.FromImage(objBitmap);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush objBrush = new SolidBrush(Color.Aqua);
        SolidBrush blackBrush = new SolidBrush(Color.DarkGray);
        SolidBrush objBrushText = new SolidBrush(Color.Brown);
        SolidBrush blueBrush = new SolidBrush(Color.BlueViolet);
        SolidBrush blueBrush1 = new SolidBrush(Color.BlanchedAlmond);
        Pen blackPen = new Pen(Color.Black, 2);

        Single startAngle = 0;
        float sweepAngle = 0;

        float total = 0;
        Random rand = new Random();
        objGraphic.SmoothingMode = SmoothingMode.Default;// SmoothingMode.AntiAlias;
        Int32 Radius = 100;
    Int32 X_Coordinate_upper_left_corner = 0;
   Int32 Y_Coordinate_upper_left_corner = 0;
   Int32 Width = 2 * Radius;
   Int32 Height = Radius;
        objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
             
        Font fontTitle = new Font("Verdana", 10, FontStyle.Bold) ;
        //Rectangle objRectangle_0 = new Rectangle(0, 0, 300, 300);
        objGraphic.FillRectangle(whiteBrush, 0, 0, 350, 350);

        //objGraphic.DrawEllipse(blackPen, 25, 25, 250, 250);
        
        objGraphic.FillEllipse(blueBrush1, 5, 5, 340, 340);
       // objGraphic.DrawEllipse(blackPen, 5, 5, 290, 290);
        objGraphic.FillEllipse(blackBrush, 25, 25, 300, 300);       


        Table tblDisplay = new Table();
        try
        {
            bool blankspace = false;
            int i;
            lblMessage.Text = "";
            int parts = Convert.ToInt16(txtParts.Text);
            //
            string[] values = txtValues.Text.Split(',');
            float newvalue = 0;

            float newtotal = 0;
            //for (i = 0; i < parts; i++)
            //{
            //    newtotal = newtotal + float.Parse(values[i]);
            //}

            //if (newtotal < 100)
            //{
            //    newvalue = 100 - newtotal;
            //    txtValues.Text += "," + newvalue;
            //    string[] values2 = txtValues.Text.Split(',');
            //    values = values2;
            //    parts++;
            //    blankspace = true;
            //    //
            //}

            if (parts == 0)
            {
                newtotal = 100;
                txtValues.Text += "," + newvalue;
                string[] values2 = txtValues.Text.Split(',');
                values = values2;
                parts++;
                blankspace = true;
                //
            }


            for (i = 0; i < parts; i++)
            {
                total = total + float.Parse(values[i]);
            }

            int totnum = parts;
            //int width_1 = 360 / parts;

            float pparts = float.Parse(parts.ToString("0.00"));
            float totcirclearea = 360;
            float width = 0;
            float width_1 = 0; width = totcirclearea / pparts;//.ToString());// parts; //width_1 = 360 / parts;
            //if (totalparts > 0)
            //{
            //    pparts = float.Parse(totalparts.ToString("0.00"));
            //    width = totcirclearea / pparts;// parts;
            //}

            width_1 = float.Parse(width.ToString("0.00"));



            float f = 360 / total;
            int deg = 360 / totnum;
            int FaceRadius = 450;

            TableRow tblRow = new TableRow();
            TableCell tblCell = new TableCell();

            Label label = new Label();
            label.Text = "";
            label.Font.Size = 12;
            tblCell.Controls.Add(label);
            tblRow.Cells.Add(tblCell);

            tblDisplay.Rows.Add(tblRow);
            float x, y;// = 50, y = 50;

            if (parts == values.Length)
            {
                for (int j = 0; j < parts; j++)
                {
                    if (j >= 20)
                        objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                    else
                        objBrush.Color = GetFillColour(j);

                    if (j == parts - 1)
                    //{
                        if (blankspace == true)
                            objBrush.Color = Color.White;
                        //else objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                    //}
                    //else
                    //    objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));

                    string height_3 = values[j];
                    int height_1 = 0;// int.Parse(values[j]);// + (j + 5);
                    
                    //float hgtNew = float.Parse(height_3);
                    //if (hgtNew <= 50)
                    //    hgtNew = hgtNew / 300;
                    //hgtNew = width_1 + 100 / hgtNew;
                    ////if((hgtNew+10)<100)
                    ////hgtNew += 10;
                    //if (hgtNew > 100)
                    //    hgtNew = 100;

                   // float height_2 = float.Parse(height_3) / 100  * 300;
                    double tValue = 300 * 300 * float.Parse(height_3) / 100;
                    double height_22 = Math.Sqrt(tValue);// / 2 * 90000 * float.Parse(height_3) / 100;
                   
                    string heightval = height_22.ToString();// height_2.ToString();
                    string[] heightval_1 = heightval.Split(new char[] { '.' });

                    height_1 = int.Parse(heightval_1[0]);
                   // int newheight = height_1 / 100 * 180;
                    //height_1 = 125 - height_1;
                    //if (height_1 < 80)
                    //    height_1 += 10;

                    //Math.Sqrt(
                    int xaxis = 0, yaxis = 0;
                    //xaxis = GetrectWidth(height_3);

                    float hgt = float.Parse(height_3);
                    xaxis = GetRectWidth(height_1); //GetRectWidth(height_2);
                    int benchmark = 0;
                    int newxaxis = 0;
                    float newbenchmark = float.Parse("60") / 100 * 300;
                    string strbenchmark = newbenchmark.ToString();
                    string[] strbench = strbenchmark.Split(new char[] { '.' });
                    benchmark = int.Parse(strbench[0]);
                    newxaxis = GetRectWidth(benchmark);

                    //int newxaxis = 0;
                    //newxaxis = GetRectWidth(height_2 + 10);

                    Color c=ColorTranslator.FromHtml("#000000");
                    Pen pen5 = new Pen(c,2);// (Color.Brown, 2);
                    
                    Rectangle objRectangle3 = new Rectangle(xaxis, xaxis, height_1, height_1);

                    if (height_1 <= 0)
                    {
                        objRectangle3 = new Rectangle(25, 25, 300, 300);
                        objBrush.Color = Color.White;
                    }
                    objGraphic.FillPie(objBrush, objRectangle3, startAngle, width_1);
                    ////code to draw Benchmark
                    
                    if (benchmark > 0)
                    {
                        objRectangle3 = new Rectangle(newxaxis, newxaxis, benchmark, benchmark);
                        Pen penBenchmark = new Pen(Color.Violet, 2);
                        objGraphic.DrawArc(penBenchmark, objRectangle3, startAngle, width_1);

                        SetBenchMarkPossition(parts, j);
                        objBrush.Color = Color.Black;
                        objGraphic.DrawString((j + 1).ToString(), fontTitle, objBrush, objRectangle_3);
                    }
                    if (j >= 0)
                    {/*
                        objBrush.Color = Color.Black;
                        Rectangle objRectangle_3 = new Rectangle(250, 200, 50, 25);
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
                        else if (j == 7) objRectangle_3 = new Rectangle(160,5, 50, 25);
                        // objGraphic.DrawString("107", fontTitle, objBrush, objRectangle_3);
                        else if (j == 8) objRectangle_3 = new Rectangle(270, 40, 50, 25);
                        //objGraphic.DrawString("107", fontTitle, objBrush, objRectangle_3);
                        else if (j == 9) objRectangle_3 = new Rectangle(320, 130, 50, 25);
                        // objGraphic.DrawString("108", fontTitle, objBrush, objRectangle_3);
                        */
                            /*
                        // objGraphic.DrawString("103", fontTitle, objBrush, objRectangle_3);
                        else if (j == 4) objRectangle_3 = new Rectangle(50, 200, 50, 25);
                        // objGraphic.DrawString("104", fontTitle, objBrush, objRectangle_3);

                        else if (j == 5) objRectangle_3 = new Rectangle(60, 130, 50, 25);
                        //objGraphic.DrawString("105", fontTitle, objBrush, objRectangle_3);
                        else if (j == 6) objRectangle_3 = new Rectangle(100, 90, 50, 25);
                        //objGraphic.DrawString("106", fontTitle, objBrush, objRectangle_3);
                        else if (j == 7) objRectangle_3 = new Rectangle(160, 70, 50, 25);
                        // objGraphic.DrawString("107", fontTitle, objBrush, objRectangle_3);
                        else if (j == 8) objRectangle_3 = new Rectangle(220, 90, 50, 25);
                        //objGraphic.DrawString("107", fontTitle, objBrush, objRectangle_3);
                        else if (j == 9) objRectangle_3 = new Rectangle(250, 130, 50, 25);
                        // objGraphic.DrawString("108", fontTitle, objBrush, objRectangle_3);

                        */

                        SetRectanclePossition(parts, j);
                        objBrush.Color = Color.Black;
                        objGraphic.DrawString((j + 1).ToString(), fontTitle, objBrush, objRectangle_3);

                        

                    }
                    objRectangle3 = new Rectangle(25, 25, 300,300);
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

        //objBrush.Color = Color.Black;

      //Rectangle  objRectangle_3 = new Rectangle(250, 200, 50, 25);        
      //  objGraphic.DrawString("100", fontTitle, objBrush, objRectangle_3);
      //  objRectangle_3 = new Rectangle(220, 240, 50, 25);
      //  objGraphic.DrawString("101", fontTitle, objBrush, objRectangle_3);
      //  objRectangle_3 = new Rectangle(160, 260, 50, 25);
      //  objGraphic.DrawString("102", fontTitle, objBrush, objRectangle_3);
      //  objRectangle_3 = new Rectangle(100, 240, 50, 25);
      //  objGraphic.DrawString("103", fontTitle, objBrush, objRectangle_3);
      //  objRectangle_3 = new Rectangle(70, 200, 50, 25);
      //  objGraphic.DrawString("104", fontTitle, objBrush, objRectangle_3);

      //  objRectangle_3 = new Rectangle(60, 130, 50, 25);
      //  objGraphic.DrawString("105", fontTitle, objBrush, objRectangle_3);
      //  objRectangle_3 = new Rectangle(100, 90, 50, 25);
      //  objGraphic.DrawString("106", fontTitle, objBrush, objRectangle_3);
      //  objRectangle_3 = new Rectangle(160, 70, 50, 25);
      //  objGraphic.DrawString("107", fontTitle, objBrush, objRectangle_3);
      //  objRectangle_3 = new Rectangle(220, 90, 50, 25);
      //  objGraphic.DrawString("107", fontTitle, objBrush, objRectangle_3);
      //  objRectangle_3 = new Rectangle(250, 130, 50, 25);
      //  objGraphic.DrawString("108", fontTitle, objBrush, objRectangle_3);

        tcelGraphHelp.Controls.Add(tblDisplay);
        imgGraph.ImageUrl = "";

        //File.Delete(Server.MapPath("Images\\NewPiechart1.jpg"));
        objBitmap.Save(Server.MapPath("Images\\NewPiechart1_1.jpg"), ImageFormat.Jpeg);
        objGraphic.Dispose();
        objBitmap.Dispose();
        imgGraph.ImageUrl = "~/Images/NewPiechart1_1.jpg";

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
         else if (totalcount == 16)
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
        else if (totalcount == 16)
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

       else  if (totalcount == 2)//ok
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
        else  if (totalcount == 10)//ok
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

  //

    int Width = 300, Height = 300;
    private void SetScale(Graphics g)
    {
        g.TranslateTransform(Width / 2, Height / 2);

        float inches = Math.Min(Width / g.DpiX, Height / g.DpiX);

        g.ScaleTransform(inches * g.DpiX / 2000, inches * g.DpiY / 2000);
    }

    private void DrawFace(Graphics g)
    {
        Brush brush = new SolidBrush(Color.Black);
        Font font = new Font("Arial", 80);

        float x, y;

        const int numHours = 4;
        const int deg = 360 / numHours;
        const int FaceRadius = 450;

        for (int i = 1; i <= numHours; i++)
        {
            SizeF stringSize =
              g.MeasureString(i.ToString(), font);

            //x = GetCos(1 * deg + 90) * FaceRadius;
            //x += stringSize.Width / 2;
            //y = GetSin(1 * deg + 90) * FaceRadius;
            //y += stringSize.Height / 2;
            //if (i == 1)
            //{
                x = GetCos(i * deg + 150) * FaceRadius;
                x += stringSize.Width / 2;
                y = GetSin(i * deg + 150) * FaceRadius;
                y += stringSize.Height / 2;
            //}
            //else
            //{
            //    x = GetCos(i * deg + 90) * FaceRadius;
            //    x += stringSize.Width / 2;
            //    y = GetSin(i * deg + 90) * FaceRadius;
            //    y += stringSize.Height / 2;
            //}

            g.DrawString(i.ToString()+ " SS ", font, brush, -x, -y);

        }
        brush.Dispose();
        font.Dispose();
    }

    /*

    private void DrawFace(Graphics g)
    {
        Brush brush = new SolidBrush(ForeColor);
        Font font = new Font("Arial", 40);

        float x, y;

        const int numHours = 12;
        const int deg = 360 / numHours;
        const int FaceRadius = 450;

        for (int i = 1; i <= numHours; i++)
        {
            x = GetCos(i * deg + 90) * FaceRadius;
            y = GetSin(i * deg + 90) * FaceRadius;

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            g.DrawString(i.ToString(), font, brush, -x, -y, format);

        }
        brush.Dispose();
        font.Dispose();
    }

    */

    private static float GetSin(float degAngle)
    {
        return (float)Math.Sin(Math.PI * degAngle / 180f);
    }

    private static float GetCos(float degAngle)
    {
        return (float)Math.Cos(Math.PI * degAngle / 180f);
    }
    

    //
    

    private int GetRectWidth(float value)
    {
        string val = "";
        //val = value.ToString();
        //string[] newvalue = val.Split(new char[] { '.' });
        //int selvalue = 0;
        //selvalue = int.Parse(newvalue[0]);

        float newval = 0;
        int widthvalue = 0;
        //if (value >= 200)
        //{
           // newval = (250 - value) / 2 + 25;
            newval = (300 - value) / 2 + 25;val = newval.ToString();
            string[] curval = val.Split(new char[] { '.' });
            widthvalue = int.Parse(curval[0]);
       // }

        return widthvalue;
    }

    
    void Draw3DPieChart(ref Graphics objGraphics)
    {
        int iLoop, iLoop2=0;

        // Create location and size  of ellipse.

        int x = 50;
        int y = 20;
        int width = 200;
        int height = 100;

        // Create start and sweep angles.

        int startAngle = 0;
        int sweepAngle = 45;
        SolidBrush objBrush = new SolidBrush(Color.Aqua);

        Random rand = new Random();
        objGraphics.SmoothingMode = SmoothingMode.AntiAlias;

        //Loop through 180 back around to 135 degress so it gets drawn
        // correctly.

        for (iLoop = 0; iLoop <= 315; iLoop += 45)
        {
            startAngle = (iLoop + 180) % 360;
            objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255),
                rand.Next(255));

            // On degrees from 0 to 180 draw 10 Hatched brush slices to show
            // depth

            if ((startAngle < 135) || (startAngle == 180))
            {
                for (iLoop2 = 0; iLoop2 < 10; iLoop2++)
                    objGraphics.FillPie(new HatchBrush(HatchStyle.Percent50,
                        objBrush.Color), x,
                        y + iLoop2, width, height, startAngle, sweepAngle);
            }

            // Displace this pie slice from pie.

            if (startAngle == 135)
            {
                // Show Depth
                objGraphics.FillPie(new HatchBrush(HatchStyle.Percent50, objBrush.Color), x - 30, y + iLoop2 + 15, width, height, startAngle, sweepAngle);
                objGraphics.FillPie(objBrush, x - 30, y + 15, width, height, startAngle, sweepAngle);
            }
            else // Draw normally

                objGraphics.FillPie(objBrush, x, y, width,
                    height, startAngle, sweepAngle);
        }
    }
    /*
    private void DrawScaleBar()
    {
        Bitmap objBitmap = new Bitmap(270, 270);
        //Bitmap objBitmap = new Bitmap(100, 100);
        Graphics objGraphic = Graphics.FromImage(objBitmap);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush objBrush = new SolidBrush(Color.Aqua);

        //Pen blackPen = new Pen(Color.Chocolate, 2);

        Pen blackPen = new Pen(Color.White, 2);

        Single startAngle = 0;
        float sweepAngle = 0;
        txtValues.Text = "125,28,75,222,450";
        string[] values = txtValues.Text.Split(',');
        
        float total = 0;
        Random rand = new Random();
        objGraphic.SmoothingMode = SmoothingMode.Default;
        int j = 0, k = 0;
        for (int i = 0; i < 5; i++)
        {
            total = total + float.Parse(values[i]);
        }
        //if (total < 100)
        //    newvalue = 100 - total;
        Font fontTitle = new Font("Verdana", 14, FontStyle.Bold) ;
        SolidBrush blackBrush = new SolidBrush(Color.Black);

        StringFormat stringFormat  = new StringFormat();
stringFormat.Alignment = StringAlignment.Center;
stringFormat.LineAlignment = StringAlignment.Center;

        float f = 360 / total;
        for (int i = 0; i < 5; i++)
        {
            sweepAngle = float.Parse(values[i]) * f;
            objGraphic.DrawString("Year Wise Sales Report", fontTitle, blackBrush, new RectangleF(0, 0, 500, 34), stringFormat);
            if (i == 0)
            {
                objBrush.Color = Color.Red;// Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                //objBrush = new SolidBrush(Color.Blue);
                objGraphic.FillPie(objBrush, 5, 5, 250, 250, startAngle, sweepAngle);
            }
            else if (i == 1)
            {
                objBrush.Color = Color.Brown; ;// Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                //objBrush = new SolidBrush(Color.Blue);
                objGraphic.FillPie(objBrush, 5, 5, 250, 250, startAngle, sweepAngle);
            }
            else if (i == 2)
            {
                objBrush.Color = Color.Chocolate;// Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                //objBrush = new SolidBrush(Color.Blue);
                objGraphic.FillPie(objBrush, 5, 5, 250, 250, startAngle, sweepAngle);
            }
            else
            {
                objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                objGraphic.FillPie(objBrush, 5, 5, 250, 250, startAngle, sweepAngle);
            }
            //objGraphic.FillPie(objBrush, 10, 10, 250, 250, startAngle, sweepAngle);
            //objGraphic.FillRegion(FillRectangle(objBrush, 20, 20, 200, 100);//, startAngle, sweepAngle);
            startAngle = startAngle + sweepAngle;
        }

        objBitmap.Save(Server.MapPath("Images\\NewPiechart1.jpg"), ImageFormat.Jpeg);
        objGraphic.Dispose();
        objBitmap.Dispose();
        imgGraph.ImageUrl = "~/Images/NewPiechart1.jpg";


        return;


        //Bitmap objBitmap = new Bitmap(500, 20);
        ////Bitmap objBitmap = new Bitmap(100, 100);
        //Graphics objGraphic = Graphics.FromImage(objBitmap);
        ////SolidBrush whiteBrush = new SolidBrush(Color.White);
        ////SolidBrush objBrush = new SolidBrush(Color.Aqua);

        ////Pen blackPen = new Pen(Color.Chocolate, 2);

        ////Pen blackPen = new Pen(Color.White, 2);

        //// Single startAngle = 0;
        //// float sweepAngle = 0;

        ////string[] values = txtValues.Text.Split(',');
        //// float total = 0;
        //// Random rand = new Random();
        //// objGraphic.SmoothingMode = SmoothingMode.Default;

        ////objGraphic.DrawRectangle(Pens.Red, 1, 1, 2, 20);






        //int j = 0, k = 0;
        //for (int i = 0; i < 100; i++)
        //{
        //    j++;
        //    if (j == 5)
        //    {
        //        //j = 0;
        //        objGraphic.DrawRectangle(Pens.Red, 1 + (i * 5), 1, 2, 10);
        //    }
        //    else if (j == 10)
        //    {
        //        j = 0;
        //        objGraphic.DrawRectangle(Pens.Red, 1 + (i * 5), 1, 2, 20);
        //    }
        //    //else objGraphic.DrawRectangle(Pens.Red, 1 + (i * 2), 5, 2, 10);
        //}
        //objBitmap.Save(Server.MapPath("Images\\Scale1.jpg"), ImageFormat.Jpeg);
        //objGraphic.Dispose();
        //objBitmap.Dispose();
        ////imgGraph.ImageUrl = "~/Images/Scale1.jpg";        


    }
    */
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

                    int tableftcellwidth = 75;
                    int tabrightcellwidth = 300;

                    //for (int count = 0; count < 5; count++)
                    //{
                    //    switch (count)
                    //    {
                    //        case 0:
                    //            {
                    tblRow = new TableRow();
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "Name";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.Width = tabrightcellwidth;
                    tblCell.BorderWidth = 2;
                    label = new Label();
                    label.Text = ds.Tables[0].Rows[0]["Name"].ToString();
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
                    label.Text = "Organization";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label();
                    label.Text = ds.Tables[0].Rows[0]["OrganizationName"].ToString();

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
                    label.Text = "JobCategory";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label();
                    label.Text = ds.Tables[0].Rows[0]["JobCategory"].ToString();

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
                    label.Text = "Designation";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label();
                    label.Text = ds.Tables[0].Rows[0]["Designation"].ToString();

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
                    label.Text = "GroupName";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label();
                    label.Text = ds.Tables[0].Rows[0]["GroupName"].ToString();

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
                lblSummary1.Text = SummaryDetails1.First().Summary1.ToString();
            if (SummaryDetails1.First().Summary2 != null)
                lblSummary2.Text = SummaryDetails1.First().Summary2.ToString();
            if (SummaryDetails1.First().DescriptiveReport != null)
                tcellDescriptionReport.InnerHtml = SummaryDetails1.First().DescriptiveReport.ToString();
            if (SummaryDetails1.First().Conclusion != null)
                lblConclusion.Text = SummaryDetails1.First().Conclusion.ToString();
            if (SummaryDetails1.First().ScoringType != null)
                scoringType = SummaryDetails1.First().ScoringType.ToString();
        }

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
            
            tblDisplay.Width = 650;
            tblDisplay.CellPadding = 0;
            tblDisplay.CellSpacing = 0;
            tblDisplay.BorderWidth = 0;
            string mark = "0"; string benchmark = "";
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                if (GridView1.Rows[j].Cells[1].Text != "&nbsp;")
                {
                    if (GridView1.Rows[j].Cells[4].Text != "0")
                        mark = GridView1.Rows[j].Cells[4].Text;

                    //lblMessage.Text += "," + mark;

                    //float totalMarksectionwise = 0, currentmark = 0;
                    //totalMarksectionwise = GetSectionwiseTotalQuestionMarks(GridView1.Rows[j].Cells[1].Text);

                    //currentmark = (mark / totalMarksectionwise) * 100;
                    //currentmark =float.Parse( currentmark.ToString("0.00"));
                    ////TestVariableResultBands
                    string remarks = "";
                    benchmark = "";
                    int secid = GetSectionId(GridView1.Rows[j].Cells[1].Text);
                    string querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid;
                    querystring1 += " AND (" + mark + " > MarkFrom AND  " + mark + " <= MarkTo)";//   MarkFrom <= " + mark + " AND  MarkTo >= " + mark;
                    querystring1 += " AND VariableId = " + secid + "";
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
                    //mark = ((mark * 100) / 360);

                   //// lblMessage.Text = querystring1; return;//021209 bip

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
                    label = new Label();
                    label.BackColor = Color.Green;//.LightBlue;
                    //mark = mark.ToString("0");
                    double dblmark = double.Parse(mark);
                    dblmark = dblmark * 5;
                    label.Width = int.Parse(dblmark.ToString("0"));
                    label.Height = 30;
                    tblCell.Controls.Add(label);
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
            //DrawPieGraph("imgReportGraph1" + userid + ".jpg", 1);
            //tcellBarGraph.Width = "200";
            //tcellBarGraph.Controls.Add(tblDisplay);
            //imgGraph.Visible = false;
       // }

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

        tcellBarGraph.Width = "200";
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

        /*
       1. #00FF00
2.#FF0080
3.#0000FF
4.#FF8000
5.#5C5C5C
6.#FFFF00
7.#CBA274
8.#008000
9.#D51500
10.#80FFFF
11.#FF00FF
12.#808000
13.#FF8080
14.#408080
15.#400080
16.#D6D6D6
17.#004080
18.#800000
19.#FFFFC6
20.#400000


        */
    }
    private int GetBenchMarkVariablewise(string variableName, string curmark)
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
                querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid;
                querystring1 += " AND (0 >= MarkFrom AND 0 <= MarkTo)";
                querystring1 += " AND VariableId = " + secid;
            }
            else
            {
                querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid;
                querystring1 += " AND (" + mark + " > MarkFrom AND  " + mark + " <= MarkTo)";//   MarkFrom <= " + mark + " AND  MarkTo >= " + mark;
                querystring1 += " AND VariableId = " + secid + "";
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
    private void DrawPieGraph(string filename, int index)
    {
        if (txtParts.Text.Trim() == "" || txtValues.Text.Trim() == "") return;
        Font fontTitle = new Font("Verdana", 10, FontStyle.Bold) ;
       // txtValues.Text = "14,3,6,50"; txtParts.Text = "4";

        Bitmap objBitmap = new Bitmap(350, 350);
        //Bitmap objBitmap = new Bitmap(100, 100);
        Graphics objGraphic = Graphics.FromImage(objBitmap);
        //SolidBrush whiteBrush = new SolidBrush(Color.White);
        //SolidBrush objBrush = new SolidBrush(Color.Aqua);

        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush objBrush = new SolidBrush(Color.Aqua);
        SolidBrush blackBrush = new SolidBrush(Color.DarkGray);
        SolidBrush objBrushText = new SolidBrush(Color.Brown);
        SolidBrush blueBrush = new SolidBrush(Color.BlueViolet);
        SolidBrush blueBrush1 = new SolidBrush(Color.BlanchedAlmond);
        Pen blackPen = new Pen(Color.Black, 2);

        //Pen blackPen = new Pen(Color.Chocolate, 2);
        //Pen blackPen = new Pen(Color.White, 2);

        Single startAngle = 0;
        float sweepAngle = 0;

        //string[] values = txtValues.Text.Split(',');
        float total = 0;
        Random rand = new Random();
        objGraphic.SmoothingMode = SmoothingMode.Default;// SmoothingMode.AntiAlias;

        //objGraphic.DrawEllipse(blackPen, 25, 25, 250, 250);
        //objGraphic.FillEllipse(whiteBrush, 25, 25, 250, 250);
        //objGraphic.DrawEllipse(blackPen, 5, 5, 290, 290);

        objGraphic.FillRectangle(whiteBrush, 0, 0, 350, 350);
        objGraphic.FillEllipse(blueBrush1, 5, 5, 340, 340);
        //objGraphic.DrawEllipse(blackPen, 5, 5, 290, 290);
        objGraphic.FillEllipse(blackBrush, 25, 25, 300, 300);


        Table tblDisplay = new Table();
        try
        {
            bool blankspace = false;
            int i; int totalparts = 0;
            //lblMessage.Text = "";
            int parts = Convert.ToInt16(txtParts.Text); 
            if (dtEmptySessionList.Rows.Count > 0)
            {               
                totalparts = dtEmptySessionList.Rows.Count;
                totalparts += parts;
            }
            //
            string[] values = txtValues.Text.Split(',');
            float newvalue = 0;
            
                float newtotal = 0;
                //for (i = 0; i < parts; i++)
                //{
                //    newtotal = newtotal + float.Parse(values[i]);
                //}
                
                //if (newtotal < 100)
                //{
                //    newvalue = 100 - newtotal;
                //    txtValues.Text += "," + newvalue;
                //    string[] values2 = txtValues.Text.Split(',');
                //    values = values2;
                //    parts++;
                //    blankspace = true;
                //    //
                //}
            if(totalparts<=0)
                if (parts <= 1)
                {
                    newtotal = 100;
                    txtValues.Text += "," + newvalue;
                    string[] values2 = txtValues.Text.Split(',');
                    values = values2;
                    parts++;
                    blankspace = true;
                    //
                }

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

            //lblMessage.Text = " parts=" + parts.ToString();
            //lblMessage.Text += "width=" + width.ToString();

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
                    objBrush.Color = GetFillColour(j);
                    //if (j == parts - 1)
                    //    //{
                    //    if (blankspace == true)
                    //        objBrush.Color = blackBrush.Color;// Color.White;//.BlanchedAlmond;//
                    ////    else objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                    ////}
                    ////else
                    ////    objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));

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
                        tblCell.Style.Value = "text-align: right; font-weight: bold";
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

                        benchmark = GetBenchMarkVariablewise(GridView1.Rows[j].Cells[1].Text, GridView1.Rows[j].Cells[4].Text);
                        orgBenchmark = benchmark;
                    }                   

                    string height_3 = values[j];
                    int height_1 = 0;// int.Parse(values[j]);// + (j + 5);

                    //float height_2 = float.Parse(height_3) / 100 * 300;
                    //

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

                    //


                    //string heightval = height_2.ToString();
                    //string[] heightval_1 = heightval.Split(new char[] { '.' });

                    //height_1 = int.Parse(heightval_1[0]);

                    //int xaxis = 0, yaxis = 0;
                    ////xaxis = GetrectWidth(height_3);
                    //float hgt = float.Parse(height_3);
                    //xaxis = GetRectWidth(height_2);

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
                   //// Rectangle objRectangle3 = new Rectangle(xaxis, 90,height_1, height_1); 
                    if (height_1 <= 0)
                    {
                        objRectangle3 = new Rectangle(25, 25, 300, 300);
                        objBrush.Color = Color.White; //lblMessage.Text += "height=" + height_1.ToString();
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
                            objGraphic.DrawString(orgBenchmark.ToString(), fontTitle, objBrush, objRectangle_3);
                        }
                    }
                    SetRectanclePossition(parts, j);
                    objBrush.Color = Color.Black;
                    objGraphic.DrawString((j + 1).ToString(), fontTitle, objBrush, objRectangle_3);

                    objRectangle3 = new Rectangle(25, 25, 300, 300);
                    objGraphic.DrawPie(pen5, objRectangle3, startAngle, width_1);
                    
                    startAngle = startAngle + width_1;
                }
                if (dtEmptySessionList.Rows.Count > 0)
                {
                    for (int k = 0; k < dtEmptySessionList.Rows.Count; k++)
                    {
                        objBrush.Color = blackBrush.Color;// Color.White; // Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));

                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        label = new Label();
                        label.Width = 50;
                        label.BackColor = objBrush.Color;
                        tblCell.Controls.Add(label);
                        tblRow.Cells.Add(tblCell);
                        tblCell = new TableCell();
                        label = new Label();
                        label.Text = dtEmptySessionList.Rows[k][0].ToString() + "= 0";
                        label.Font.Size = 12;
                        tblCell.Controls.Add(label);
                        tblRow.Cells.Add(tblCell);
                        tblDisplay.Rows.Add(tblRow);

                        tblRow = new TableRow();
                        tblCell = new TableCell(); tblCell.ColumnSpan = 2;
                        label = new Label();
                        label = new Label();
                        label.Text = dtEmptySessionList.Rows[k][1].ToString();
                        label.Font.Size = 12;
                        tblCell.Controls.Add(label);
                        tblRow.Cells.Add(tblCell);
                        tblDisplay.Rows.Add(tblRow);

                        Rectangle objRectangle3 = new Rectangle(25, 25, 300, 300);
                       // objBrush.Color = Color.White;

                        objGraphic.FillPie(objBrush, objRectangle3, startAngle, width_1);
                        //objGraphic.DrawArc(pen5, objRectangle3, startAngle, width_1);
                        int benchmark = 0; int orgbenchmark = 0;
                        benchmark = GetBenchMarkVariablewise(dtEmptySessionList.Rows[k][0].ToString(), "0");
                        orgbenchmark = benchmark;
                        int newxaxis = 0;
                        float newbenchmark = float.Parse(benchmark.ToString()) / 100 * 300;
                        string strbenchmark = newbenchmark.ToString();
                        string[] strbench = strbenchmark.Split(new char[] { '.' });
                        benchmark = int.Parse(strbench[0]);
                        newxaxis = GetRectWidth(benchmark);
                        if (benchmark > 0)
                        {
                            objRectangle3 = new Rectangle(newxaxis, newxaxis, benchmark, benchmark);
                            Pen penBenchmark = new Pen(Color.White,2);//.Violet, 2);
                            objGraphic.DrawArc(penBenchmark, objRectangle3, startAngle, width_1);
                        }

                        Pen pen5 = new Pen(Color.Black, 2);
                        //objRectangle3 = new Rectangle(5, 5, 290, 290);
                        objRectangle3 = new Rectangle(25, 25, 300, 300);
                        objGraphic.DrawPie(pen5, objRectangle3, startAngle, width_1);

                        startAngle = startAngle + width_1;

                    }
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
        if (File.Exists(Server.MapPath("Images\\" + filename)))
            File.Delete(Server.MapPath("Images\\" + filename));

        objBitmap.Save(Server.MapPath("Images\\" + filename), ImageFormat.Jpeg);
        objGraphic.Dispose();
        objBitmap.Dispose();
        imgGraph.ImageUrl = "~/Images/" + filename;
    }

    private void DrawPieGraph_TestSecwise_1(string filename)
    {
        if (txtParts.Text.Trim() == "" || txtValues.Text.Trim() == "") return;

        Bitmap objBitmap = new Bitmap(350, 350);
        //Bitmap objBitmap = new Bitmap(100, 100);
        Graphics objGraphic = Graphics.FromImage(objBitmap);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush objBrush = new SolidBrush(Color.Aqua);
        SolidBrush blackBrush = new SolidBrush(Color.DarkGray);
        objGraphic.FillRectangle(whiteBrush, 0, 0, 350, 350);
        objGraphic.FillEllipse(blackBrush, 25, 25, 300, 300);
        Pen blackPen = new Pen(Color.White, 2);

        Single startAngle = 0;
        float sweepAngle = 0;

        float total = 0;
        Random rand = new Random();
        objGraphic.SmoothingMode = SmoothingMode.Default;// SmoothingMode.AntiAlias;
        Table tblDisplay = new Table();
        try
        {
            bool blankspace = false;
            int i;
            lblMessage.Text = "";
            int parts = Convert.ToInt16(txtParts.Text);
            //
            string[] values = txtValues.Text.Split(',');
            float newvalue = 0;

            float newtotal = 0;
            for (i = 0; i < parts; i++)
            {
                newtotal = newtotal + float.Parse(values[i]);
            }

            if (newtotal < 100)
            {
                newvalue = 100 - newtotal;
                txtValues.Text += "," + newvalue;
                string[] values2 = txtValues.Text.Split(',');
                values = values2;
                parts++;
                blankspace = true;
                //
            }

            for (i = 0; i < parts; i++)
            {
                total = total + float.Parse(values[i]);
            }

            float f = 360 / total;


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
                    objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                    if (j == parts - 1)
                        //{
                        if (blankspace == true)
                            objBrush.Color = blackBrush.Color;// Color.White;
                    //    else objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                    //}
                    //else
                    //    objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                    if (j < dtTestSection.Rows.Count)
                    {
                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        label = new Label();
                        label.Width = 50;
                        label.BackColor = objBrush.Color;
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


                    }

                    sweepAngle = float.Parse(values[j]) * f;
                    objGraphic.FillPie(objBrush, 25, 25, 300, 300, startAngle, sweepAngle);
                    startAngle = startAngle + sweepAngle;
                }
                //if (dtEmptySessionList.Rows.Count > 0)
                //{
                //    for (int k = 0; k < dtEmptySessionList.Rows.Count; k++)
                //    {
                //        objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));

                //        tblRow = new TableRow();
                //        tblCell = new TableCell();
                //        label = new Label();
                //        label.Width = 50;
                //        label.BackColor = objBrush.Color;
                //        tblCell.Controls.Add(label);
                //        tblRow.Cells.Add(tblCell);
                //        tblCell = new TableCell();
                //        label = new Label();
                //        label.Text = dtEmptySessionList.Rows[k][0].ToString() + "= 0";
                //        label.Font.Size = 12;
                //        tblCell.Controls.Add(label);
                //        tblRow.Cells.Add(tblCell);
                //        tblDisplay.Rows.Add(tblRow);

                //        tblRow = new TableRow();
                //        tblCell = new TableCell(); tblCell.ColumnSpan = 2;
                //        label = new Label();
                //        label = new Label();
                //        label.Text = dtEmptySessionList.Rows[k][1].ToString();
                //        label.Font.Size = 12;
                //        tblCell.Controls.Add(label);
                //        tblRow.Cells.Add(tblCell);
                //        tblDisplay.Rows.Add(tblRow);

                //    }
                //}
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
        if (File.Exists(Server.MapPath("Images\\" + filename)))
            File.Delete(Server.MapPath("Images\\" + filename));

        objBitmap.Save(Server.MapPath("Images\\" + filename), ImageFormat.Jpeg);
        objGraphic.Dispose();
        objBitmap.Dispose();
        imgGraph.ImageUrl = "~/Images/" + filename;
    }

    private void DrawPieGraph_TestSecwise(string filename)
    {
        if (txtParts.Text.Trim() == "" || txtValues.Text.Trim() == "") return;

        // txtValues.Text = "14,3,6,50"; txtParts.Text = "4";

        Bitmap objBitmap = new Bitmap(350, 350);
        //Bitmap objBitmap = new Bitmap(100, 100);
        Graphics objGraphic = Graphics.FromImage(objBitmap);
        //SolidBrush whiteBrush = new SolidBrush(Color.White);
        //SolidBrush objBrush = new SolidBrush(Color.Aqua);

        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush objBrush = new SolidBrush(Color.Aqua);
        SolidBrush blackBrush = new SolidBrush(Color.DarkGray);
        SolidBrush objBrushText = new SolidBrush(Color.Brown);
        SolidBrush blueBrush = new SolidBrush(Color.BlueViolet);
        SolidBrush blueBrush1 = new SolidBrush(Color.BlanchedAlmond);
        Pen blackPen = new Pen(Color.Black, 2);

        //Pen blackPen = new Pen(Color.Chocolate, 2);
        //Pen blackPen = new Pen(Color.White, 2);

        Single startAngle = 0;
        float sweepAngle = 0;

        //string[] values = txtValues.Text.Split(',');
        float total = 0;
        Random rand = new Random();
        objGraphic.SmoothingMode = SmoothingMode.Default;// SmoothingMode.AntiAlias;

        //objGraphic.DrawEllipse(blackPen, 25, 25, 250, 250);
        //objGraphic.FillEllipse(whiteBrush, 25, 25, 250, 250);
        //objGraphic.DrawEllipse(blackPen, 5, 5, 290, 290);

        objGraphic.FillRectangle(whiteBrush, 0, 0, 350, 350);
        objGraphic.FillEllipse(blueBrush1, 5, 5, 340, 340);
        //objGraphic.DrawEllipse(blackPen, 5, 5, 290, 290);
        objGraphic.FillEllipse(blackBrush, 25, 25, 300, 300);


        Table tblDisplay = new Table();
        try
        {
            bool blankspace = false;
            int i; int totalparts = 0;
            lblMessage.Text = "";
            int parts = Convert.ToInt16(txtParts.Text);
            //if (dtEmptySessionList.Rows.Count > 0)
            //{
            //    totalparts = dtEmptySessionList.Rows.Count;
            //    totalparts += parts;
            //}
            //
            string[] values = txtValues.Text.Split(',');
            float newvalue = 0;

            float newtotal = 0;
            //for (i = 0; i < parts; i++)
            //{
            //    newtotal = newtotal + float.Parse(values[i]);
            //}

            //if (newtotal < 100)
            //{
            //    newvalue = 100 - newtotal;
            //    txtValues.Text += "," + newvalue;
            //    string[] values2 = txtValues.Text.Split(',');
            //    values = values2;
            //    parts++;
            //    blankspace = true;
            //    //
            //}
            if (totalparts <= 0)
                if (parts <= 1)
                {
                    newtotal = 100;
                    txtValues.Text += "," + newvalue;
                    string[] values2 = txtValues.Text.Split(',');
                    values = values2;
                    parts++;
                    blankspace = true;
                    //
                }

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

            //lblMessage.Text = " parts=" + parts.ToString();
            //lblMessage.Text += "width=" + width.ToString();

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
                    if (j == parts - 1)
                        //{
                        if (blankspace == true)
                            objBrush.Color = Color.White;//.BlanchedAlmond;//
                    //    else objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                    //}
                    //else
                    //    objBrush.Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));

                    int benchmark = 0;

                    if (j < dtTestSection.Rows.Count)
                    {
                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        label = new Label();
                        label.Width = 50;
                        label.BackColor = objBrush.Color;
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
                    }


                    string height_3 = values[j];
                    int height_1 = 0;// int.Parse(values[j]);// + (j + 5);

                    //float height_2 = float.Parse(height_3) / 100 * 300;
                    //

                    double tValue = 300 * 300 * float.Parse(height_3) / 100;
                    double height_22 = Math.Sqrt(tValue);// / 2 * 90000 * float.Parse(height_3) / 100;

                    string heightval = height_22.ToString();// height_2.ToString();
                    string[] heightval_1 = heightval.Split(new char[] { '.' });

                    height_1 = int.Parse(heightval_1[0]);

                    int xaxis = 0, yaxis = 0;
                    //xaxis = GetrectWidth(height_3);

                    float hgt = float.Parse(height_3);
                    xaxis = GetRectWidth(height_1);

                    //


                    //string heightval = height_2.ToString();
                    //string[] heightval_1 = heightval.Split(new char[] { '.' });

                    //height_1 = int.Parse(heightval_1[0]);

                    //int xaxis = 0, yaxis = 0;
                    ////xaxis = GetrectWidth(height_3);
                    //float hgt = float.Parse(height_3);
                    //xaxis = GetRectWidth(height_2);

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
                        objBrush.Color = Color.White; //lblMessage.Text += "height=" + height_1.ToString();
                    }
                    objGraphic.FillPie(objBrush, objRectangle3, startAngle, width_1);
                    if (benchmark > 0)
                    {
                        objRectangle3 = new Rectangle(newxaxis, newxaxis, benchmark, benchmark);
                        //Pen penBenchmark = new Pen(Color.Violet, 2);
                        Pen penBenchmark = new Pen(Color.White, 2);
                        objGraphic.DrawArc(penBenchmark, objRectangle3, startAngle, width_1);
                    }
                    objRectangle3 = new Rectangle(25, 25, 300, 300);
                    objGraphic.DrawPie(pen5, objRectangle3, startAngle, width_1);

                    startAngle = startAngle + width_1;
                }
                //if (dtEmptySessionList.Rows.Count > 0)
                //{
                //    for (int k = 0; k < dtEmptySessionList.Rows.Count; k++)
                //    {
                //        objBrush.Color = Color.White; // Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));

                //        tblRow = new TableRow();
                //        tblCell = new TableCell();
                //        label = new Label();
                //        label.Width = 50;
                //        label.BackColor = objBrush.Color;
                //        tblCell.Controls.Add(label);
                //        tblRow.Cells.Add(tblCell);
                //        tblCell = new TableCell();
                //        label = new Label();
                //        label.Text = dtEmptySessionList.Rows[k][0].ToString() + "= 0";
                //        label.Font.Size = 12;
                //        tblCell.Controls.Add(label);
                //        tblRow.Cells.Add(tblCell);
                //        tblDisplay.Rows.Add(tblRow);

                //        tblRow = new TableRow();
                //        tblCell = new TableCell(); tblCell.ColumnSpan = 2;
                //        label = new Label();
                //        label = new Label();
                //        label.Text = dtEmptySessionList.Rows[k][1].ToString();
                //        label.Font.Size = 12;
                //        tblCell.Controls.Add(label);
                //        tblRow.Cells.Add(tblCell);
                //        tblDisplay.Rows.Add(tblRow);

                //        Rectangle objRectangle3 = new Rectangle(25, 25, 300, 300);
                //        // objBrush.Color = Color.White;

                //        objGraphic.FillPie(objBrush, objRectangle3, startAngle, width_1);
                //        //objGraphic.DrawArc(pen5, objRectangle3, startAngle, width_1);
                //        int benchmark = 0;
                //        benchmark = GetBenchMarkVariablewise(dtEmptySessionList.Rows[k][0].ToString(), "0");
                //        int newxaxis = 0;
                //        float newbenchmark = float.Parse(benchmark.ToString()) / 100 * 300;
                //        string strbenchmark = newbenchmark.ToString();
                //        string[] strbench = strbenchmark.Split(new char[] { '.' });
                //        benchmark = int.Parse(strbench[0]);
                //        newxaxis = GetRectWidth(benchmark);
                //        if (benchmark > 0)
                //        {
                //            objRectangle3 = new Rectangle(newxaxis, newxaxis, benchmark, benchmark);
                //            Pen penBenchmark = new Pen(Color.Violet, 2);
                //            objGraphic.DrawArc(penBenchmark, objRectangle3, startAngle, width_1);
                //        }

                //        Pen pen5 = new Pen(Color.Black, 2);
                //        //objRectangle3 = new Rectangle(5, 5, 290, 290);
                //        objRectangle3 = new Rectangle(25, 25, 300, 300);
                //        objGraphic.DrawPie(pen5, objRectangle3, startAngle, width_1);

                //        startAngle = startAngle + width_1;

                //    }
                //}
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
        if (File.Exists(Server.MapPath("Images\\" + filename)))
            File.Delete(Server.MapPath("Images\\" + filename));

        objBitmap.Save(Server.MapPath("Images\\" + filename), ImageFormat.Jpeg);
        objGraphic.Dispose();
        objBitmap.Dispose();
        imgGraph.ImageUrl = "~/Images/" + filename;
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
    private void GetReportGraphDetailsFromDB()
    {
        try
        {
            dtTestSection = new DataTable();
            dtTestSection.Columns.Add("TestSectionID");
            dtTestSection.Columns.Add("TotalMark_sec");
            DataRow drTestSection;


            DataRow dr;
            dt.Columns.Add("SectionID");
            dt.Columns.Add("SectionName");
            dt.Columns.Add("Marks");
            dt.Columns.Add("TestID");
            dt.Columns.Add("TotalMarks");
            dt.Columns.Add("BandDescription");

            DataRow drEmptySession;
            dtEmptySessionList.Columns.Add("SectionName");
            dtEmptySessionList.Columns.Add("BandDescription");
            //DataRow dr1;
            //dt1.Columns.Add("BenchMark");
            //dt1.Columns.Add("TotalMark");
            //dt1.Columns.Add("TestID");

            //int userid = int.Parse(Session["UserId_Report"].ToString());
            int MemmoryTestImage = 0;
            int MemmoryTestText = 0;
            int QuesCollection = 0;
            string sectionname = ""; int TestSecionID = 0;
            //SqlConnection conn;
            //SqlCommand cmd;
            //SqlDataAdapter da;
            DataSet ds;
            Table tblDisplay = new Table();
            TableCell tblCell = new TableCell();
            TableRow tblRow;
            Label label;
            //int i = 0;
            int rowid = 0;
            int totalmarks = 0;
            int sectionid = 0;
            ////int testid = int.Parse(Session["UserTestID_Report"].ToString());

            string quesrystring = "SELECT DISTINCT EvaluationResult.QuestionID, TestBaseQuestionList.TestId, TestBaseQuestionList.TestSectionId,TestBaseQuestionList.Status, EvaluationResult.Question, " +
                      " EvaluationResult.Answer, EvaluationResult.UserId, EvaluationResult.Category, TestBaseQuestionList.SectionId, " +
                      " TestBaseQuestionList.FirstVariableId, TestBaseQuestionList.SecondVariableId, TestBaseQuestionList.ThirdVariableId FROM  EvaluationResult INNER JOIN TestBaseQuestionList ON EvaluationResult.QuestionID = TestBaseQuestionList.QuestionId " +
                      " WHERE     (TestBaseQuestionList.Status = 1) and EvaluationResult.UserId=" + userid;

            DataSet dsEvaluationdetails = new DataSet();
            dsEvaluationdetails = clsClasses.GetValuesFromDB(quesrystring);
            if (dsEvaluationdetails != null)
                if (dsEvaluationdetails.Tables.Count > 0)
                    if (dsEvaluationdetails.Tables[0].Rows.Count > 0)
                    {
                        //var UserAnsws1 = from UserAnsws in dataclass.EvaluationResults
                        //                 where UserAnsws.UserId == userid   // && UserAnsws.TestId == testid
                        //                 select UserAnsws;

                        //if (UserAnsws1.Count() > 0)
                        //{
                        // foreach (var UserAnswers in UserAnsws1)
                        for (int i = 0; i < dsEvaluationdetails.Tables[0].Rows.Count; i++)
                        {
                            int marks = 0;
                            int currentmarks = 0;
                            sectionid = 0;
                            //if (UserAnswers.Category == "MemTestWords")
                            if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "MemTestWords")
                            {
                                var MemWords1 = from MemWords in dataclass.MemmoryTestTextQuesCollections
                                                where MemWords.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && MemWords.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString()) //UserAnswers.Question && MemWords.QuestionID == UserAnswers.QuestionID
                                                select MemWords;
                                if (MemWords1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                        TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = MemWords1.First().SectionName.ToString();
                                    sectionid = int.Parse(MemWords1.First().SectionId.ToString());
                                    if (sectionid > 0)// bip 05122009
                                        if (MemWords1.First().Answer == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())// UserAnswers.Answer)
                                            marks = 1;
                                }
                            }
                            else if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "MemTestImages")
                            {
                                var MemImages1 = from MemImages in dataclass.MemmoryTestImageQuesCollections
                                                 where MemImages.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && MemImages.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && MemImages.QuestionID == UserAnswers.QuestionID
                                                 select MemImages;
                                if (MemImages1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                        TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = MemImages1.First().SectionName.ToString();
                                    sectionid = int.Parse(MemImages1.First().SectionId.ToString());
                                    if (sectionid > 0)// bip 05122009
                                        if (MemImages1.First().Answer == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())// UserAnswers.Answer)
                                            marks = 1;
                                }
                            }
                            else if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "FillBlanks")
                            {
                                var FillQues1 = from FillQues in dataclass.QuestionCollections
                                                where FillQues.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && FillQues.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && FillQues.QuestionID == UserAnswers.QuestionID
                                                select FillQues;
                                if (FillQues1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                        TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = FillQues1.First().SectionName.ToString();
                                    sectionid = int.Parse(FillQues1.First().SectionId.ToString());

                                    //if (FillQues1.First().Option1 == UserAnswers.Answer || FillQues1.First().Option2 == UserAnswers.Answer ||
                                    //    FillQues1.First().Option3 == UserAnswers.Answer || FillQues1.First().Option4 == UserAnswers.Answer ||
                                    //    FillQues1.First().Option5 == UserAnswers.Answer)
                                    if (sectionid > 0)// bip 05122009
                                        if (FillQues1.First().Option1 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() || FillQues1.First().Option2 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() ||
                                             FillQues1.First().Option3 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() || FillQues1.First().Option4 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() ||
                                             FillQues1.First().Option5 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())
                                            marks = 1;
                                }
                            }
                            else if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "RatingType")
                            {
                                var FillQues1 = from FillQues in dataclass.QuestionCollections
                                                where FillQues.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && FillQues.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && FillQues.QuestionID == UserAnswers.QuestionID
                                                select FillQues;
                                if (FillQues1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                        TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = FillQues1.First().SectionName.ToString();
                                    sectionid = int.Parse(FillQues1.First().SectionId.ToString());
                                    ////if (sectionname == "Test2")
                                    ////    sectionid = 40;
                                    //if (FillQues1.First().Answer == UserAnswers.Answer)
                                    //{
                                    if (sectionid > 0)// bip 05122009
                                        marks = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString());// int.Parse(UserAnswers.Answer.ToString());

                                    //if (sectionname == "Clarity of Communication")
                                    //    lblMessage.Text += " " + sectionname + " mark " + marks.ToString(); // 021209 bip
                                    //// marks = ratingmark;
                                    // dataclass.ProcSectionMarks(userid, testid, sectionid, sectionname, marks);
                                    ////}
                                    //}
                                }
                            }
                            else
                            {
                                var OtherQues1 = from OtherQues in dataclass.QuestionCollections
                                                 where OtherQues.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && OtherQues.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && OtherQues.QuestionID == UserAnswers.QuestionID
                                                 select OtherQues;
                                if (OtherQues1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                        TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString()); //int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = OtherQues1.First().SectionName.ToString();
                                    sectionid = int.Parse(OtherQues1.First().SectionId.ToString());
                                    if (sectionid > 0)// bip 05122009
                                        if (OtherQues1.First().Answer == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())// UserAnswers.Answer)
                                            marks = 1;
                                }
                            }
                            //if (sectionname == "Clarity of Communication")
                            if (marks > 0)
                            {
                                bool testsecExists = false; int j = 0;
                                if (dtTestSection.Rows.Count > 0)
                                {
                                    for (int n = 0; n < dtTestSection.Rows.Count; n++)
                                    {
                                        if (dtTestSection.Rows[n]["TestSectionID"].ToString() == TestSecionID.ToString())
                                        { testsecExists = true; j = n; break; }
                                    }
                                }
                                if (testsecExists == true)
                                {
                                    int currentmarks_testsecid = 0;
                                    drTestSection = dtTestSection.Rows[j];
                                    currentmarks_testsecid = int.Parse(drTestSection["TotalMark_sec"].ToString());
                                    currentmarks_testsecid = currentmarks_testsecid + marks;
                                    drTestSection["TotalMark_sec"] = currentmarks_testsecid.ToString();
                                }
                                else
                                {
                                    drTestSection = dtTestSection.NewRow();
                                    drTestSection["TestSectionID"] = TestSecionID.ToString();
                                    drTestSection["TotalMark_sec"] = marks.ToString();
                                    dtTestSection.Rows.Add(drTestSection);
                                }

                                Boolean exists = CheckExistence(sectionname);
                                if (exists == true)
                                {
                                    rowid = int.Parse(Session["RowID"].ToString());
                                    dr = dt.Rows[rowid];
                                    currentmarks = int.Parse(dr["TotalMarks"].ToString());
                                    currentmarks = currentmarks + marks;
                                    dr["TotalMarks"] = currentmarks;

                                    // lblMessage.Text += " || " + sectionname + " totmarknew= " + currentmarks + " || ";
                                }
                                else
                                {
                                    //if (currentmarks > 0)
                                    //    marks = currentmarks;

                                    dr = dt.NewRow();
                                    //dr["SectionID"] = sectionid;
                                    dr["SectionName"] = sectionname;
                                    dr["TestID"] = testid;
                                    dr["Marks"] = marks;
                                    if (exists == false)
                                        dr["TotalMarks"] = marks;
                                    else
                                        dr["TotalMarks"] = "0";
                                    dr["BandDescription"] = "";
                                    dt.Rows.Add(dr);
                                }
                            }
                            else
                            {

                                bool sesExists = false;
                                if (dtEmptySessionList.Rows.Count > 0)
                                {
                                    for (int e = 0; e < dtEmptySessionList.Rows.Count; e++)
                                    {
                                        if (dtEmptySessionList.Rows[e][0].ToString() == sectionname)
                                            sesExists = true;
                                    }
                                }
                                if (sesExists == false)
                                {
                                    drEmptySession = dtEmptySessionList.NewRow();
                                    drEmptySession["SectionName"] = sectionname;

                                    string remarks = GetBandDescription(0, sectionname);
                                    drEmptySession["BandDescription"] = remarks;
                                    dtEmptySessionList.Rows.Add(drEmptySession);


                                }
                                if (dtEmptySessionList.Rows.Count > 0)
                                {
                                    for (int e = 0; e < dtEmptySessionList.Rows.Count; e++)
                                    {
                                        Boolean exists = CheckExistence(dtEmptySessionList.Rows[e][0].ToString());
                                        if (exists == true)
                                        {
                                            dtEmptySessionList.Rows[e].Delete();
                                        }
                                    }
                                }
                            }//
                        }
                    }


            GridView1.DataSource = dt;
            GridView1.DataBind();
            // lblMessage.Text = scoretype; return;
            //if (Session["DiagramType"].ToString() == "PieGraph")
            //{
            if (GridView1.Rows.Count > 0)
            {
                if (scoretype == "Percentage")
                {

                    for (int k = 0; k < GridView1.Rows.Count; k++)
                    {
                        int curmark = int.Parse(GridView1.Rows[k].Cells[4].Text);
                        string remarks = GetBandDescription(curmark, GridView1.Rows[k].Cells[1].Text);
                        GridView1.Rows[k].Cells[5].Text = remarks;

                        if (GridView1.Rows[k].Cells[4].Text != "0" && GridView1.Rows[k].Cells[4].Text != "&nbsp;")
                        {
                            float mark1 = float.Parse(GridView1.Rows[k].Cells[4].Text);
                            //mark1 = ((mark1 * 100) / 360);
                            float totalMarksectionwise = 0, currentmark = 0;
                            totalMarksectionwise = GetSectionwiseTotalQuestionMarks(GridView1.Rows[k].Cells[1].Text, 0);
                            if (totalMarksectionwise > 0)
                            {
                                //mark1 = (mark1 / totalMarksectionwise) * 100;
                                //lblMessage.Text += " tot_F=" + totalMarksectionwise + " &,& " + currentmark.ToString();
                                // lblMessage.Text += GridView1.Rows[k].Cells[1].Text +" tot=" + totalMarksectionwise ;//+ " &,& " 
                                currentmark = (mark1 / totalMarksectionwise) * 100;
                                // lblMessage.Text += " mark=" + mark1.ToString() + "curMark &,& " + currentmark.ToString();
                                currentmark = float.Parse(currentmark.ToString("0.00"));
                                GridView1.Rows[k].Cells[4].Text = currentmark.ToString();

                                // lblMessage.Text += GridView1.Rows[k].Cells[1].Text + " currentmark &&& " + currentmark.ToString();
                            }
                        }

                    }
                    if (Session["ReportType"].ToString() == "TestSectionwise")
                    {
                        //testsection mark details

                        for (int k = 0; k < dtTestSection.Rows.Count; k++)
                        {
                            int curmark = int.Parse(dtTestSection.Rows[k]["TotalMark_sec"].ToString());
                            int testsecid = int.Parse(dtTestSection.Rows[k]["TestSectionID"].ToString());
                            if (curmark > 0)
                            {
                                float mark1 = float.Parse(curmark.ToString());
                                //mark1 = ((mark1 * 100) / 360);
                                float totMarktestsecwise = 0, currentmark = 0;
                                totMarktestsecwise = GetSectionwiseTotalQuestionMarks("", testsecid);
                                //mark1 = (mark1 / totalMarksectionwise) * 100;
                                currentmark = (mark1 / totMarktestsecwise) * 100;
                                currentmark = float.Parse(currentmark.ToString("0.00"));
                                drTestSection = dtTestSection.Rows[k];
                                drTestSection["TotalMark_sec"] = currentmark.ToString();
                            }
                        }

                        for (int k = 0; k < dtTestSection.Rows.Count; k++)
                        {
                            if (dtTestSection.Rows[k][1].ToString() != "")
                            {
                                float curmark = float.Parse(dtTestSection.Rows[k][1].ToString());
                                if (txtValues.Text.Trim() != "")
                                    txtValues.Text = txtValues.Text.Trim() + ",";
                                txtValues.Text = txtValues.Text.Trim() + curmark.ToString();//currentmark;// mark1;//
                                txtParts.Text = (k + 1).ToString();
                            }
                        }

                        DrawPieGraph_TestSecwise("imgReportGraph1_" + userid + ".jpg");
                        DrawBarGraph();
                        return;
                    }

                }
                else
                {
                    lblMessage.Text += "hi...";
                    for (int k = 0; k < GridView1.Rows.Count; k++)
                    {
                        int curmark = int.Parse(GridView1.Rows[k].Cells[4].Text);
                        string remarks = GetBandDescription(curmark, GridView1.Rows[k].Cells[1].Text);
                        GridView1.Rows[k].Cells[5].Text = remarks;

                        if (GridView1.Rows[k].Cells[4].Text != "0")
                        {
                            float mark1 = float.Parse(GridView1.Rows[k].Cells[4].Text);
                            ////mark1 = ((mark1 * 100) / 360);
                            float currentmark = 0;//,totalMarksectionwise = 0 ;
                            //totalMarksectionwise = GetSectionwiseTotalQuestionMarks(GridView1.Rows[k].Cells[1].Text);
                            ////mark1 = (mark1 / totalMarksectionwise) * 100;
                            //currentmark = (mark1 / totalMarksectionwise) * 100;
                            //currentmark = float.Parse(currentmark.ToString("0.00"));

                            currentmark = GetPercentileScoreUserwise(mark1, GridView1.Rows[k].Cells[1].Text);
                            currentmark = float.Parse(currentmark.ToString("0.00"));
                            GridView1.Rows[k].Cells[4].Text = currentmark.ToString();

                            lblMessage.Text += "," + currentmark.ToString();
                        }
                    }


                    if (Session["ReportType"].ToString() == "TestSectionwise")
                    {
                        //testsection mark details

                        for (int k = 0; k < dtTestSection.Rows.Count; k++)
                        {
                            int curmark = int.Parse(dtTestSection.Rows[k]["TotalMark_sec"].ToString());
                            int testsecid = int.Parse(dtTestSection.Rows[k]["TestSectionID"].ToString());
                            if (curmark > 0)
                            {
                                float mark1 = float.Parse(curmark.ToString());
                                //mark1 = ((mark1 * 100) / 360);
                                float currentmark = 0;//totMarktestsecwise = 0, 

                                currentmark = GetPercentileScoreUserwise_testsec(mark1, testsecid);
                                currentmark = float.Parse(currentmark.ToString("0.00"));
                                drTestSection = dtTestSection.Rows[k];
                                drTestSection["TotalMark_sec"] = currentmark.ToString();
                            }
                        }

                        for (int k = 0; k < dtTestSection.Rows.Count; k++)
                        {
                            if (dtTestSection.Rows[k][1].ToString() != "")
                            {
                                float curmark = float.Parse(dtTestSection.Rows[k][1].ToString());
                                if (txtValues.Text.Trim() != "")
                                    txtValues.Text = txtValues.Text.Trim() + ",";
                                txtValues.Text = txtValues.Text.Trim() + curmark.ToString();//currentmark;// mark1;//
                                txtParts.Text = (k + 1).ToString();
                            }
                        }

                        DrawPieGraph_TestSecwise("imgReportGraph1_" + userid + ".jpg");
                        DrawBarGraph();
                        return;
                    }
                }

                for (int j = 0; j < GridView1.Rows.Count; j++)
                {
                    if (GridView1.Rows[j].Cells[1].Text != "&nbsp;")
                        if (GridView1.Rows[j].Cells[4].Text != "0")
                        {
                            if (txtValues.Text.Trim() != "")
                                txtValues.Text = txtValues.Text.Trim() + ",";
                            txtValues.Text = txtValues.Text.Trim() + GridView1.Rows[j].Cells[4].Text;//currentmark;// mark1;//
                            txtParts.Text = (j + 1).ToString();
                        }
                }
            }

            DrawPieGraph("imgReportGraph1_" + userid + ".jpg", 1);
            DrawBarGraph();
        }
        catch (Exception ex) { lblMessage.Text += ex.Message; }

    }
    //
    private void GetReportGraphDetailsFromDB1()
    {
        try
        {
            dtTestSection = new DataTable();
            dtTestSection.Columns.Add("TestSectionID");
            dtTestSection.Columns.Add("TotalMark_sec");
            DataRow drTestSection;


            DataRow dr;
            dt.Columns.Add("SectionID");
            dt.Columns.Add("SectionName");
            dt.Columns.Add("Marks");
            dt.Columns.Add("TestID");
            dt.Columns.Add("TotalMarks");
            dt.Columns.Add("BandDescription");

            DataRow drEmptySession;
            dtEmptySessionList.Columns.Add("SectionName");
            dtEmptySessionList.Columns.Add("BandDescription");
            //DataRow dr1;
            //dt1.Columns.Add("BenchMark");
            //dt1.Columns.Add("TotalMark");
            //dt1.Columns.Add("TestID");

            //int userid = int.Parse(Session["UserId_Report"].ToString());
            int MemmoryTestImage = 0;
            int MemmoryTestText = 0;
            int QuesCollection = 0;
            string sectionname = ""; int TestSecionID = 0;
            //SqlConnection conn;
            //SqlCommand cmd;
            //SqlDataAdapter da;
            DataSet ds;
            Table tblDisplay = new Table();
            TableCell tblCell = new TableCell();
            TableRow tblRow;
            Label label;
            //int i = 0;
            int rowid = 0;
            int totalmarks = 0;
            int sectionid = 0;
            ////int testid = int.Parse(Session["UserTestID_Report"].ToString());

            string quesrystring = "SELECT DISTINCT EvaluationResult.QuestionID, TestBaseQuestionList.TestId, TestBaseQuestionList.TestSectionId,TestBaseQuestionList.Status, EvaluationResult.Question, " +
                      " EvaluationResult.Answer, EvaluationResult.UserId, EvaluationResult.Category, TestBaseQuestionList.SectionId, " +
                      " TestBaseQuestionList.FirstVariableId, TestBaseQuestionList.SecondVariableId, TestBaseQuestionList.ThirdVariableId FROM  EvaluationResult INNER JOIN TestBaseQuestionList ON EvaluationResult.QuestionID = TestBaseQuestionList.QuestionId " +
                      " WHERE     (TestBaseQuestionList.Status = 1) and EvaluationResult.UserId=" + userid;

            DataSet dsEvaluationdetails = new DataSet();
            dsEvaluationdetails = clsClasses.GetValuesFromDB(quesrystring);
            if (dsEvaluationdetails != null)
                if (dsEvaluationdetails.Tables.Count > 0)
                    if (dsEvaluationdetails.Tables[0].Rows.Count > 0)
                    {
                        //var UserAnsws1 = from UserAnsws in dataclass.EvaluationResults
                        //                 where UserAnsws.UserId == userid   // && UserAnsws.TestId == testid
                        //                 select UserAnsws;

                        //if (UserAnsws1.Count() > 0)
                        //{
                        // foreach (var UserAnswers in UserAnsws1)
                        for (int i = 0; i < dsEvaluationdetails.Tables[0].Rows.Count; i++)
                        {
                            int marks = 0;
                            int currentmarks = 0;
                            sectionid = 0;
                            //if (UserAnswers.Category == "MemTestWords")
                            if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "MemTestWords")
                            {
                                var MemWords1 = from MemWords in dataclass.MemmoryTestTextQuesCollections
                                                where MemWords.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && MemWords.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString()) //UserAnswers.Question && MemWords.QuestionID == UserAnswers.QuestionID
                                                select MemWords;
                                if (MemWords1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                    TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = MemWords1.First().SectionName.ToString();
                                    sectionid = int.Parse(MemWords1.First().SectionId.ToString());
                                    if (sectionid > 0)// bip 05122009
                                        if (MemWords1.First().Answer == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())// UserAnswers.Answer)
                                            marks = 1;
                                }
                            }
                            else if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "MemTestImages")
                            {
                                var MemImages1 = from MemImages in dataclass.MemmoryTestImageQuesCollections
                                                 where MemImages.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && MemImages.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && MemImages.QuestionID == UserAnswers.QuestionID
                                                 select MemImages;
                                if (MemImages1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                    TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = MemImages1.First().SectionName.ToString();
                                    sectionid = int.Parse(MemImages1.First().SectionId.ToString());
                                    if (sectionid > 0)// bip 05122009
                                        if (MemImages1.First().Answer == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())// UserAnswers.Answer)
                                            marks = 1;
                                }
                            }
                            else if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "FillBlanks")
                            {
                                var FillQues1 = from FillQues in dataclass.QuestionCollections
                                                where FillQues.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && FillQues.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && FillQues.QuestionID == UserAnswers.QuestionID
                                                select FillQues;
                                if (FillQues1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                    TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = FillQues1.First().SectionName.ToString();
                                    sectionid = int.Parse(FillQues1.First().SectionId.ToString());

                                    //if (FillQues1.First().Option1 == UserAnswers.Answer || FillQues1.First().Option2 == UserAnswers.Answer ||
                                    //    FillQues1.First().Option3 == UserAnswers.Answer || FillQues1.First().Option4 == UserAnswers.Answer ||
                                    //    FillQues1.First().Option5 == UserAnswers.Answer)
                                    if (sectionid > 0)// bip 05122009
                                        if (FillQues1.First().Option1 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() || FillQues1.First().Option2 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() ||
                                             FillQues1.First().Option3 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() || FillQues1.First().Option4 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() ||
                                             FillQues1.First().Option5 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())
                                            marks = 1;
                                }
                            }
                            else if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "RatingType")
                            {
                                var FillQues1 = from FillQues in dataclass.QuestionCollections
                                                where FillQues.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && FillQues.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && FillQues.QuestionID == UserAnswers.QuestionID
                                                select FillQues;
                                if (FillQues1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                    TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = FillQues1.First().SectionName.ToString();
                                    sectionid = int.Parse(FillQues1.First().SectionId.ToString());
                                    ////if (sectionname == "Test2")
                                    ////    sectionid = 40;
                                    //if (FillQues1.First().Answer == UserAnswers.Answer)
                                    //{
                                    if (sectionid > 0)// bip 05122009
                                    marks = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString());// int.Parse(UserAnswers.Answer.ToString());

                                    //if (sectionname == "Clarity of Communication")
                                    //    lblMessage.Text += " " + sectionname + " mark " + marks.ToString(); // 021209 bip
                                    //// marks = ratingmark;
                                    // dataclass.ProcSectionMarks(userid, testid, sectionid, sectionname, marks);
                                    ////}
                                    //}
                                }
                            }
                            else
                            {
                                var OtherQues1 = from OtherQues in dataclass.QuestionCollections
                                                 where OtherQues.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && OtherQues.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && OtherQues.QuestionID == UserAnswers.QuestionID
                                                 select OtherQues;
                                if (OtherQues1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() !="")
                                        TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString()); //int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = OtherQues1.First().SectionName.ToString();
                                    sectionid = int.Parse(OtherQues1.First().SectionId.ToString());
                                    if (sectionid > 0)// bip 05122009
                                        if (OtherQues1.First().Answer == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())// UserAnswers.Answer)
                                            marks = 1;
                                }
                            }
                            //if (sectionname == "Clarity of Communication")
                            if (marks > 0)
                            {
                                bool testsecExists = false; int j = 0;
                                if (dtTestSection.Rows.Count > 0)
                                {
                                    for (int n = 0; n < dtTestSection.Rows.Count; n++)
                                    {
                                        if (dtTestSection.Rows[n]["TestSectionID"].ToString() == TestSecionID.ToString())
                                        { testsecExists = true; j = n; break; }
                                    }
                                }
                                if (testsecExists == true)
                                {
                                    int currentmarks_testsecid = 0;
                                    drTestSection = dtTestSection.Rows[j];
                                    currentmarks_testsecid = int.Parse(drTestSection["TotalMark_sec"].ToString());
                                    currentmarks_testsecid = currentmarks_testsecid + marks;
                                    drTestSection["TotalMark_sec"] = currentmarks_testsecid.ToString();
                                }
                                else
                                {
                                    drTestSection = dtTestSection.NewRow();
                                    drTestSection["TestSectionID"] = TestSecionID.ToString();
                                    drTestSection["TotalMark_sec"] = marks.ToString();
                                    dtTestSection.Rows.Add(drTestSection);
                                }

                                Boolean exists = CheckExistence(sectionname);
                                if (exists == true)
                                {
                                    rowid = int.Parse(Session["RowID"].ToString());
                                    dr = dt.Rows[rowid];
                                    currentmarks = int.Parse(dr["TotalMarks"].ToString());
                                    currentmarks = currentmarks + marks;
                                    dr["TotalMarks"] = currentmarks;

                                    // lblMessage.Text += " || " + sectionname + " totmarknew= " + currentmarks + " || ";
                                }
                                else
                                {
                                    //if (currentmarks > 0)
                                    //    marks = currentmarks;

                                    dr = dt.NewRow();
                                    //dr["SectionID"] = sectionid;
                                    dr["SectionName"] = sectionname;
                                    dr["TestID"] = testid;
                                    dr["Marks"] = marks;
                                    if (exists == false)
                                        dr["TotalMarks"] = marks;
                                    else
                                        dr["TotalMarks"] = "0";
                                    dr["BandDescription"] = "";
                                    dt.Rows.Add(dr);
                                }
                            }
                            else
                            {

                                bool sesExists = false;
                                if (dtEmptySessionList.Rows.Count > 0)
                                {
                                    for (int e = 0; e < dtEmptySessionList.Rows.Count; e++)
                                    {
                                        if (dtEmptySessionList.Rows[e][0].ToString() == sectionname)
                                            sesExists = true;
                                    }
                                }
                                if (sesExists == false)
                                {
                                    drEmptySession = dtEmptySessionList.NewRow();
                                    drEmptySession["SectionName"] = sectionname;

                                    string remarks = GetBandDescription(0, sectionname);
                                    drEmptySession["BandDescription"] = remarks;
                                    dtEmptySessionList.Rows.Add(drEmptySession);


                                }
                                if (dtEmptySessionList.Rows.Count > 0)
                                {
                                    for (int e = 0; e < dtEmptySessionList.Rows.Count; e++)
                                    {
                                        Boolean exists = CheckExistence(dtEmptySessionList.Rows[e][0].ToString());
                                        if (exists == true)
                                        {
                                            dtEmptySessionList.Rows[e].Delete();
                                        }
                                    }
                                }
                            }//
                        }
                    }


            GridView1.DataSource = dt;
            GridView1.DataBind();
            // lblMessage.Text = scoretype; return;
            //if (Session["DiagramType"].ToString() == "PieGraph")
            //{
            if (GridView1.Rows.Count > 0)
            {
                if (scoretype == "Percentage")
                {

                    for (int k = 0; k < GridView1.Rows.Count; k++)
                    {
                        int curmark = int.Parse(GridView1.Rows[k].Cells[4].Text);
                        string remarks = GetBandDescription(curmark, GridView1.Rows[k].Cells[1].Text);
                        GridView1.Rows[k].Cells[5].Text = remarks;

                        if (GridView1.Rows[k].Cells[4].Text != "0" && GridView1.Rows[k].Cells[4].Text != "&nbsp;")
                        {
                            float mark1 = float.Parse(GridView1.Rows[k].Cells[4].Text);
                            //mark1 = ((mark1 * 100) / 360);
                            float totalMarksectionwise = 0, currentmark = 0;
                            totalMarksectionwise = GetSectionwiseTotalQuestionMarks(GridView1.Rows[k].Cells[1].Text, 0);
                            if (totalMarksectionwise > 0)
                            {
                                //mark1 = (mark1 / totalMarksectionwise) * 100;
                                //lblMessage.Text += " tot_F=" + totalMarksectionwise + " &,& " + currentmark.ToString();
                                // lblMessage.Text += GridView1.Rows[k].Cells[1].Text +" tot=" + totalMarksectionwise ;//+ " &,& " 
                                currentmark = (mark1 / totalMarksectionwise) * 100;
                                // lblMessage.Text += " mark=" + mark1.ToString() + "curMark &,& " + currentmark.ToString();
                                currentmark = float.Parse(currentmark.ToString("0.00"));
                                GridView1.Rows[k].Cells[4].Text = currentmark.ToString();

                                // lblMessage.Text += GridView1.Rows[k].Cells[1].Text + " currentmark &&& " + currentmark.ToString();
                            }
                        }

                    }
                    if (Session["ReportType"].ToString() == "TestSectionwise")
                    {
                        //testsection mark details

                        for (int k = 0; k < dtTestSection.Rows.Count; k++)
                        {
                            int curmark = int.Parse(dtTestSection.Rows[k]["TotalMark_sec"].ToString());
                            int testsecid = int.Parse(dtTestSection.Rows[k]["TestSectionID"].ToString());
                            if (curmark > 0)
                            {
                                float mark1 = float.Parse(curmark.ToString());
                                //mark1 = ((mark1 * 100) / 360);
                                float totMarktestsecwise = 0, currentmark = 0;
                                totMarktestsecwise = GetSectionwiseTotalQuestionMarks("", testsecid);
                                //mark1 = (mark1 / totalMarksectionwise) * 100;
                                currentmark = (mark1 / totMarktestsecwise) * 100;
                                currentmark = float.Parse(currentmark.ToString("0.00"));
                                drTestSection = dtTestSection.Rows[k];
                                drTestSection["TotalMark_sec"] = currentmark.ToString();
                            }
                        }

                        for (int k = 0; k < dtTestSection.Rows.Count; k++)
                        {
                            if (dtTestSection.Rows[k][1].ToString() != "")
                            {
                                float curmark = float.Parse(dtTestSection.Rows[k][1].ToString());
                                if (txtValues.Text.Trim() != "")
                                    txtValues.Text = txtValues.Text.Trim() + ",";
                                txtValues.Text = txtValues.Text.Trim() + curmark.ToString();//currentmark;// mark1;//
                                txtParts.Text = (k + 1).ToString();
                            }
                        }

                        DrawPieGraph_TestSecwise("imgReportGraph1_" + userid + ".jpg");
                        DrawBarGraph();
                        return;
                    }

                }
                else
                {
                    lblMessage.Text += "hi...";
                    for (int k = 0; k < GridView1.Rows.Count; k++)
                    {
                        int curmark = int.Parse(GridView1.Rows[k].Cells[4].Text);
                        string remarks = GetBandDescription(curmark, GridView1.Rows[k].Cells[1].Text);
                        GridView1.Rows[k].Cells[5].Text = remarks;

                        if (GridView1.Rows[k].Cells[4].Text != "0")
                        {
                            float mark1 = float.Parse(GridView1.Rows[k].Cells[4].Text);
                            ////mark1 = ((mark1 * 100) / 360);
                            float currentmark = 0;//,totalMarksectionwise = 0 ;
                            //totalMarksectionwise = GetSectionwiseTotalQuestionMarks(GridView1.Rows[k].Cells[1].Text);
                            ////mark1 = (mark1 / totalMarksectionwise) * 100;
                            //currentmark = (mark1 / totalMarksectionwise) * 100;
                            //currentmark = float.Parse(currentmark.ToString("0.00"));

                            currentmark = GetPercentileScoreUserwise(mark1, GridView1.Rows[k].Cells[1].Text);
                            currentmark = float.Parse(currentmark.ToString("0.00"));
                            GridView1.Rows[k].Cells[4].Text = currentmark.ToString();

                            lblMessage.Text += "," + currentmark.ToString();
                        }
                    }


                    if (Session["ReportType"].ToString() == "TestSectionwise")
                    {
                        //testsection mark details

                        for (int k = 0; k < dtTestSection.Rows.Count; k++)
                        {
                            int curmark = int.Parse(dtTestSection.Rows[k]["TotalMark_sec"].ToString());
                            int testsecid = int.Parse(dtTestSection.Rows[k]["TestSectionID"].ToString());
                            if (curmark > 0)
                            {
                                float mark1 = float.Parse(curmark.ToString());
                                //mark1 = ((mark1 * 100) / 360);
                                float currentmark = 0;//totMarktestsecwise = 0, 

                                currentmark = GetPercentileScoreUserwise_testsec(mark1, testsecid);
                                currentmark = float.Parse(currentmark.ToString("0.00"));
                                drTestSection = dtTestSection.Rows[k];
                                drTestSection["TotalMark_sec"] = currentmark.ToString();
                            }
                        }

                        for (int k = 0; k < dtTestSection.Rows.Count; k++)
                        {
                            if (dtTestSection.Rows[k][1].ToString() != "")
                            {
                                float curmark = float.Parse(dtTestSection.Rows[k][1].ToString());
                                if (txtValues.Text.Trim() != "")
                                    txtValues.Text = txtValues.Text.Trim() + ",";
                                txtValues.Text = txtValues.Text.Trim() + curmark.ToString();//currentmark;// mark1;//
                                txtParts.Text = (k + 1).ToString();
                            }
                        }

                        DrawPieGraph_TestSecwise("imgReportGraph1_" + userid + ".jpg");
                        DrawBarGraph();
                        return;
                    }
                }

                for (int j = 0; j < GridView1.Rows.Count; j++)
                {
                    if (GridView1.Rows[j].Cells[1].Text != "&nbsp;")
                        if (GridView1.Rows[j].Cells[4].Text != "0")
                        {
                            if (txtValues.Text.Trim() != "")
                                txtValues.Text = txtValues.Text.Trim() + ",";
                            txtValues.Text = txtValues.Text.Trim() + GridView1.Rows[j].Cells[4].Text;//currentmark;// mark1;//
                            txtParts.Text = (j + 1).ToString();
                        }
                }
            }

            DrawPieGraph("imgReportGraph1_" + userid + ".jpg", 1);
            DrawBarGraph();
        }
        catch (Exception ex) { lblMessage.Text += ex.Message; }

    }

    //

    /*

    private void GetReportGraphDetailsFromDB()
    {
        dtTestSection = new DataTable();
        dtTestSection.Columns.Add("TestSectionID");
        dtTestSection.Columns.Add("TotalMark");
        DataRow drTestSection;


        DataRow dr;
        dt.Columns.Add("SectionID");
        dt.Columns.Add("SectionName");
        dt.Columns.Add("Marks");
        dt.Columns.Add("TestID");
        dt.Columns.Add("TotalMarks");
        dt.Columns.Add("BandDescription");

        DataRow drEmptySession;
        dtEmptySessionList.Columns.Add("SectionName");
        dtEmptySessionList.Columns.Add("BandDescription");
        //DataRow dr1;
        //dt1.Columns.Add("BenchMark");
        //dt1.Columns.Add("TotalMark");
        //dt1.Columns.Add("TestID");

        //int userid = int.Parse(Session["UserId_Report"].ToString());
        int MemmoryTestImage = 0;
        int MemmoryTestText = 0;
        int QuesCollection = 0;
        string sectionname = ""; int TestSecionID = 0;
        //SqlConnection conn;
        //SqlCommand cmd;
        //SqlDataAdapter da;
        DataSet ds;
        Table tblDisplay = new Table();
        TableCell tblCell = new TableCell();
        TableRow tblRow;
        Label label;
        int i = 0;
        int rowid = 0;
        int totalmarks = 0;
        int sectionid = 0;
        ////int testid = int.Parse(Session["UserTestID_Report"].ToString());
        
        var UserAnsws1 = from UserAnsws in dataclass.EvaluationResults
                         where UserAnsws.UserId == userid   // && UserAnsws.TestId == testid
                         select UserAnsws;

        if (UserAnsws1.Count() > 0)
        {
            foreach (var UserAnswers in UserAnsws1)
            {
                int marks = 0;
                int currentmarks = 0;
                sectionid = 0;
                if (UserAnswers.Category == "MemTestWords")
                {
                    var MemWords1 = from MemWords in dataclass.MemmoryTestTextQuesCollections
                                    where MemWords.Question == UserAnswers.Question && MemWords.QuestionID == UserAnswers.QuestionID
                                    select MemWords;
                    if (MemWords1.Count() > 0)
                    {
                        TestSecionID = int.Parse(UserAnswers.TestSectionId.ToString());
                        sectionname = MemWords1.First().SectionName.ToString();
                        sectionid = int.Parse(MemWords1.First().SectionId.ToString());
                        if (MemWords1.First().Answer == UserAnswers.Answer)
                            marks = 1;
                    }
                }
                else if (UserAnswers.Category == "MemTestImages")
                {
                    var MemImages1 = from MemImages in dataclass.MemmoryTestImageQuesCollections
                                     where MemImages.Question == UserAnswers.Question && MemImages.QuestionID == UserAnswers.QuestionID
                                     select MemImages;
                    if (MemImages1.Count() > 0)
                    {
                        TestSecionID = int.Parse(UserAnswers.TestSectionId.ToString());
                        sectionname = MemImages1.First().SectionName.ToString();
                        sectionid = int.Parse(MemImages1.First().SectionId.ToString());
                        if (MemImages1.First().Answer == UserAnswers.Answer)
                            marks = 1;
                    }
                }
                else if (UserAnswers.Category == "FillBlanks")
                {
                    var FillQues1 = from FillQues in dataclass.QuestionCollections
                                    where FillQues.Question == UserAnswers.Question && FillQues.QuestionID == UserAnswers.QuestionID
                                    select FillQues;
                    if (FillQues1.Count() > 0)
                    {
                        TestSecionID = int.Parse(UserAnswers.TestSectionId.ToString());
                        sectionname = FillQues1.First().SectionName.ToString();
                        sectionid = int.Parse(FillQues1.First().SectionId.ToString());

                        if (FillQues1.First().Option1 == UserAnswers.Answer || FillQues1.First().Option2 == UserAnswers.Answer ||
                            FillQues1.First().Option3 == UserAnswers.Answer || FillQues1.First().Option4 == UserAnswers.Answer ||
                            FillQues1.First().Option5 == UserAnswers.Answer)
                            marks = 1;
                    }
                }
                else if (UserAnswers.Category == "RatingType")
                {
                    var FillQues1 = from FillQues in dataclass.QuestionCollections
                                    where FillQues.Question == UserAnswers.Question && FillQues.QuestionID == UserAnswers.QuestionID
                                    select FillQues;
                    if (FillQues1.Count() > 0)
                    {
                        TestSecionID = int.Parse(UserAnswers.TestSectionId.ToString());
                        sectionname = FillQues1.First().SectionName.ToString();
                        sectionid = int.Parse(FillQues1.First().SectionId.ToString());
                        ////if (sectionname == "Test2")
                        ////    sectionid = 40;
                        if (FillQues1.First().Answer == UserAnswers.Answer)
                        {
                            marks = int.Parse(UserAnswers.Answer.ToString());
                            //// marks = ratingmark;
                            // dataclass.ProcSectionMarks(userid, testid, sectionid, sectionname, marks);
                            ////}
                        }
                    }
                }
                else
                {
                    var OtherQues1 = from OtherQues in dataclass.QuestionCollections
                                     where OtherQues.Question == UserAnswers.Question && OtherQues.QuestionID == UserAnswers.QuestionID
                                     select OtherQues;
                    if (OtherQues1.Count() > 0)
                    {
                        TestSecionID = int.Parse(UserAnswers.TestSectionId.ToString());
                        sectionname = OtherQues1.First().SectionName.ToString();
                        sectionid = int.Parse(OtherQues1.First().SectionId.ToString());
                        if (OtherQues1.First().Answer == UserAnswers.Answer)
                            marks = 1;
                    }

                }
                if (marks > 0)
                {
                    bool testsecExists = false; int j = 0;
                    if (dtTestSection.Rows.Count > 0)
                    {
                        for(int n=0;n<dtTestSection.Rows.Count;n++)
                        {
                            if (dtTestSection.Rows[n]["TestSectionID"].ToString() == TestSecionID.ToString())
                            { testsecExists = true; j = n; break; }
                        }
                    }
                    if (testsecExists == true)
                    {
                        int currentmarks_testsecid = 0;
                        drTestSection = dtTestSection.Rows[j];
                        currentmarks_testsecid = int.Parse(drTestSection["TotalMark"].ToString());
                        currentmarks_testsecid = currentmarks_testsecid + marks;
                        drTestSection["TotalMark"] = currentmarks_testsecid.ToString();  
                    }
                    else
                    {
                        drTestSection = dtTestSection.NewRow();
                        drTestSection["TestSectionID"] = TestSecionID.ToString();
                        drTestSection["TotalMark"] = marks.ToString();
                        dtTestSection.Rows.Add(drTestSection);
                    }

                    Boolean exists = CheckExistence(sectionname);
                    if (exists == true)
                    {
                        rowid = int.Parse(Session["RowID"].ToString());
                        dr = dt.Rows[rowid];
                        currentmarks = int.Parse(dr["TotalMarks"].ToString());
                        currentmarks = currentmarks + marks;
                        dr["TotalMarks"] = currentmarks;
                    }
                    else
                    {
                        //if (currentmarks > 0)
                        //    marks = currentmarks;

                        dr = dt.NewRow();
                        //dr["SectionID"] = sectionid;
                        dr["SectionName"] = sectionname;
                        dr["TestID"] = testid;
                        dr["Marks"] = marks;
                        if (exists == false)
                            dr["TotalMarks"] = marks;
                        else
                            dr["TotalMarks"] = "0";
                        dr["BandDescription"] = "";
                        dt.Rows.Add(dr);
                    }
                }
                else
                {

                    bool sesExists = false;
                    if (dtEmptySessionList.Rows.Count > 0)
                    {
                        for (int e = 0; e < dtEmptySessionList.Rows.Count; e++)
                        {
                            if (dtEmptySessionList.Rows[e][0].ToString() == sectionname)
                                sesExists = true;
                        }
                    }
                    if (sesExists == false)
                    {
                        drEmptySession = dtEmptySessionList.NewRow();
                        drEmptySession["SectionName"] = sectionname;

                        string remarks = GetBandDescription(0, sectionname);
                        drEmptySession["BandDescription"] = remarks;
                        dtEmptySessionList.Rows.Add(drEmptySession);


                    }
                    if (dtEmptySessionList.Rows.Count > 0)
                    {
                        for (int e = 0; e < dtEmptySessionList.Rows.Count; e++)
                        {
                            Boolean exists = CheckExistence(dtEmptySessionList.Rows[e][0].ToString());
                            if (exists == true)
                            {
                                dtEmptySessionList.Rows[e].Delete();
                            }
                        }
                    }
                }//
            }
        }

        
        GridView1.DataSource = dt;
        GridView1.DataBind();
        
        //if (Session["DiagramType"].ToString() == "PieGraph")
        //{
        if (GridView1.Rows.Count > 0)
        {
            if (scoretype == "Percentage")
            {
                
                    for (int k = 0; k < GridView1.Rows.Count; k++)
                    {
                        int curmark = int.Parse(GridView1.Rows[k].Cells[4].Text);
                        string remarks = GetBandDescription(curmark, GridView1.Rows[k].Cells[1].Text);
                        GridView1.Rows[k].Cells[5].Text = remarks;

                        if (GridView1.Rows[k].Cells[4].Text != "0")
                        {
                            float mark1 = float.Parse(GridView1.Rows[k].Cells[4].Text);
                            //mark1 = ((mark1 * 100) / 360);
                            float totalMarksectionwise = 0, currentmark = 0;
                            totalMarksectionwise = GetSectionwiseTotalQuestionMarks(GridView1.Rows[k].Cells[1].Text,0);
                            //mark1 = (mark1 / totalMarksectionwise) * 100;
                            currentmark = (mark1 / totalMarksectionwise) * 100;
                            currentmark = float.Parse(currentmark.ToString("0.00"));
                            GridView1.Rows[k].Cells[4].Text = currentmark.ToString();
                        }

                    }
                    if (Session["ReportType"].ToString() == "TestSectionwise")
                    {
                        //testsection mark details

                        for (int k = 0; k < dtTestSection.Rows.Count; k++)
                        {
                            int curmark = int.Parse(dtTestSection.Rows[k]["TotalMark"].ToString());
                            int testsecid = int.Parse(dtTestSection.Rows[k]["TestSectionID"].ToString());
                            if (curmark > 0)
                            {
                                float mark1 = float.Parse(curmark.ToString());
                                //mark1 = ((mark1 * 100) / 360);
                                float totMarktestsecwise = 0, currentmark = 0;
                                totMarktestsecwise = GetSectionwiseTotalQuestionMarks("", testsecid);
                                //mark1 = (mark1 / totalMarksectionwise) * 100;
                                currentmark = (mark1 / totMarktestsecwise) * 100;
                                currentmark = float.Parse(currentmark.ToString("0.00"));
                                drTestSection = dtTestSection.Rows[k];
                                drTestSection["TotalMark"] = currentmark.ToString();
                            }
                        }

                        for (int k = 0; k < dtTestSection.Rows.Count; k++)
                        {
                            if (dtTestSection.Rows[k][1].ToString() != "")
                            {
                                float curmark = float.Parse(dtTestSection.Rows[k][1].ToString());
                                if (txtValues.Text.Trim() != "")
                                    txtValues.Text = txtValues.Text.Trim() + ",";
                                txtValues.Text = txtValues.Text.Trim() + curmark.ToString();//currentmark;// mark1;//
                                txtParts.Text = (k + 1).ToString();
                            }
                        }

                        DrawPieGraph_TestSecwise("imgReportGraph1_" + userid + ".jpg");
                        DrawBarGraph();
                        return;
                    }
                
            }
            else
            {

                for (int k = 0; k < GridView1.Rows.Count; k++)
                {
                    int curmark = int.Parse(GridView1.Rows[k].Cells[4].Text);
                    string remarks = GetBandDescription(curmark, GridView1.Rows[k].Cells[1].Text);
                    GridView1.Rows[k].Cells[5].Text = remarks;

                    if (GridView1.Rows[k].Cells[4].Text != "0")
                    {
                        float mark1 = float.Parse(GridView1.Rows[k].Cells[4].Text);
                        ////mark1 = ((mark1 * 100) / 360);
                        float currentmark = 0;//,totalMarksectionwise = 0 ;
                        //totalMarksectionwise = GetSectionwiseTotalQuestionMarks(GridView1.Rows[k].Cells[1].Text);
                        ////mark1 = (mark1 / totalMarksectionwise) * 100;
                        //currentmark = (mark1 / totalMarksectionwise) * 100;
                        //currentmark = float.Parse(currentmark.ToString("0.00"));

                        currentmark = GetPercentileScoreUserwise(mark1, GridView1.Rows[k].Cells[1].Text);
                        currentmark = float.Parse(currentmark.ToString("0.00"));
                        GridView1.Rows[k].Cells[4].Text = currentmark.ToString();
                    }
                }


                if (Session["ReportType"].ToString() == "TestSectionwise")
                {
                    //testsection mark details

                    for (int k = 0; k < dtTestSection.Rows.Count; k++)
                    {
                        int curmark = int.Parse(dtTestSection.Rows[k]["TotalMark"].ToString());
                        int testsecid = int.Parse(dtTestSection.Rows[k]["TestSectionID"].ToString());
                        if (curmark > 0)
                        {
                            float mark1 = float.Parse(curmark.ToString());
                            //mark1 = ((mark1 * 100) / 360);
                            float currentmark = 0;//totMarktestsecwise = 0, 
                                                       
                            currentmark = GetPercentileScoreUserwise_testsec(mark1, testsecid);
                            currentmark = float.Parse(currentmark.ToString("0.00"));
                            drTestSection = dtTestSection.Rows[k];
                            drTestSection["TotalMark"] = currentmark.ToString();
                        }
                    }

                    for (int k = 0; k < dtTestSection.Rows.Count; k++)
                    {
                        if (dtTestSection.Rows[k][1].ToString() != "")
                        {
                            float curmark = float.Parse(dtTestSection.Rows[k][1].ToString());
                            if (txtValues.Text.Trim() != "")
                                txtValues.Text = txtValues.Text.Trim() + ",";
                            txtValues.Text = txtValues.Text.Trim() + curmark.ToString();//currentmark;// mark1;//
                            txtParts.Text = (k + 1).ToString();
                        }
                    }

                    DrawPieGraph_TestSecwise("imgReportGraph1_" + userid + ".jpg");
                    DrawBarGraph();
                    return;
                }
                



            }
            
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                if (GridView1.Rows[j].Cells[1].Text != "&nbsp;")
                    if (GridView1.Rows[j].Cells[4].Text != "0")
                    {
                        if (txtValues.Text.Trim() != "")
                            txtValues.Text = txtValues.Text.Trim() + ",";
                        txtValues.Text = txtValues.Text.Trim() + GridView1.Rows[j].Cells[4].Text;//currentmark;// mark1;//
                        txtParts.Text = (j + 1).ToString();
                    }
            }
        }
        
        DrawPieGraph("imgReportGraph1_" + userid + ".jpg", 1);
        DrawBarGraph();
        

    }

    */

    private float GetPercentileScoreUserwise(float totalmark,string sectionname)
    {
        double score = 0;
        int XF = 1, CF = 0, totnum = 1;
         var GetPercentileScore = from perScore in dataclass.ScoreTables
                                         where perScore.TestId == testid && perScore.SectionName==sectionname
                                         select perScore;
         if (GetPercentileScore.Count() > 0)
         {
             totnum = int.Parse(GetPercentileScore.Count().ToString());
             foreach (var totalmarkdet in GetPercentileScore)
             {
                 float mark = float.Parse(totalmarkdet.TotalScore.ToString());
                 if (mark >= (totalmark - .5) && mark <= (totalmark + .5))
                     XF = XF + 1;
                 else if (mark < (totalmark - .5))
                     CF = CF + 1;
             }
             score = (CF + (.5 * XF) * 100) / totnum;
         }
         float scoretotal = 0;
         scoretotal = float.Parse(score.ToString());
         return scoretotal;
    }

    private float GetPercentileScoreUserwise_testsec(float totalmark, int testsecionid)
    {
        double score = 0;
        int XF = 1, CF = 0, totnum = 0;
        string quesrystring = "select sum(TotalScore) as SecTotalScore,userid from ScoreTable where TestId=" + testid + " and TestSectionId=" + testsecionid + " group by Userid";
        DataSet dssecdet = new DataSet();
        dssecdet = clsClasses.GetValuesFromDB(quesrystring);
        if(dssecdet!=null)
            if(dssecdet.Tables.Count>0)
                if (dssecdet.Tables[0].Rows.Count > 0)
                {
                    totnum = dssecdet.Tables[0].Rows.Count;
                    for(int i=0;i<dssecdet.Tables[0].Rows.Count;i++)
                    //foreach (var totalmarkdet in GetPercentileScore)
                    {
                        float mark = float.Parse(dssecdet.Tables[0].Rows[i]["SecTotalScore"].ToString());
                        if (mark >= (totalmark - .5) && mark <= (totalmark + .5))
                            XF = XF + 1;
                        else if (mark < (totalmark - .5))
                            CF = CF + 1;
                    }
                    score = (CF + (.5 * XF) * 100) / totnum;
                }
        float scoretotal = 0;
        scoretotal = float.Parse(score.ToString());
        return scoretotal;
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

    private String GetBandDescription(int mark, string secName)
    {
        string remarks = "";
        int secid = GetSectionId(secName);
        string querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid;
        querystring1 += " AND (" + mark + " > MarkFrom AND  " + mark + " <= MarkTo)";//MarkFrom <= " + mark + " AND  MarkTo >= " + mark;
        querystring1 += " AND VariableId = " + secid;
        DataSet ds1 = new DataSet();
        ds1 = clsClasses.GetValuesFromDB(querystring1);
        if (ds1 != null)
            if (ds1.Tables.Count > 0)
                if (ds1.Tables[0].Rows.Count > 0)
                    if (ds1.Tables[0].Rows[0]["DisplayName"] != "")
                        remarks = ds1.Tables[0].Rows[0]["DisplayName"].ToString();

        return remarks;
    }


    private Boolean CheckExistence(string section)
    {
        Boolean result = false;
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["SectionName"].ToString() == section)
                {
                    result = true;
                    Session["RowID"] = i;
                }
            }
        }
        return result;
    }

    private void GetTotalQuestionMarks()
    {
        //  View_TestBaseQuestionList_memImages
        //  View_TestBaseQuestionList_memWords
        //  View_TestBaseQuestionList
        DataSet dsCount = new DataSet();
        int memimageQuesValue = 0, memWordsQuesValue = 0, quesValue = 0, ratingQuesValue = 0;

        // memImages questions
        string quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList_memImages where status=1 and TestId=" + testid;
        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                        memimageQuesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());

        //memWords questions
        quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList_memWords where status=1 and  TestId=" + testid;
        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                        memWordsQuesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());

        //Rating questions
        quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList  where TestBaseQuestionStatus=1 and Category = 'RatingType' and TestId=" + testid;
        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        int totalrate = 0;
                        ratingQuesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());

                        var optionCount = from optcount in dataclass.View_TestBaseQuestionLists
                                          where optcount.TestId == testid && optcount.TestBaseQuestionStatus == 1
                                          select optcount;
                        if (optionCount.Count() > 0)
                        {
                            if (optionCount.First().Option1 != null && optionCount.First().Option1 != "")
                                totalrate++;
                            if (optionCount.First().Option2 != null && optionCount.First().Option2 != "")
                                totalrate++;
                            if (optionCount.First().Option3 != null && optionCount.First().Option3 != "")
                                totalrate++;
                            if (optionCount.First().Option4 != null && optionCount.First().Option4 != "")
                                totalrate++;
                            if (optionCount.First().Option5 != null && optionCount.First().Option5 != "")
                                totalrate++;
                            if (optionCount.First().Option6 != null && optionCount.First().Option6 != "")
                                totalrate++;
                            if (optionCount.First().Option7 != null && optionCount.First().Option7 != "")
                                totalrate++;
                            if (optionCount.First().Option8 != null && optionCount.First().Option8 != "")
                                totalrate++;
                            if (optionCount.First().Option9 != null && optionCount.First().Option9 != "")
                                totalrate++;
                            if (optionCount.First().Option10 != null && optionCount.First().Option10 != "")
                                totalrate++;

                            ratingQuesValue = ratingQuesValue * totalrate;
                        }
                    }

        //other type questions
        quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category <> 'RatingType' and TestId=" + testid;
        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                        quesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());

        totalQuestionMark = memimageQuesValue + memWordsQuesValue + quesValue + ratingQuesValue;

    }

    private int GetSectionwiseTotalQuestionMarks(string sectioname, int testsectionid)
    {
        int totalmark = 0;
        DataSet dsCount = new DataSet();
        int memimageQuesValue = 0, memWordsQuesValue = 0, quesValue = 0, ratingQuesValue = 0;
        string quesryString = "";
        // memImages questions
        if (testsectionid > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList_memImages where status=1 and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList_memImages where status=1 and TestId=" + testid + " and SectionName='" + sectioname + "'";

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                        memimageQuesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());

        //memWords questions
        if (testsectionid > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList_memWords where status=1 and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList_memWords where status=1 and TestId=" + testid + " and SectionName='" + sectioname + "'";
        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                        memWordsQuesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());

        //Rating questions
        if (testsectionid > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'RatingType' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'RatingType' and TestId=" + testid + " and SectionName='" + sectioname + "'";

        //lblMessage.Text += quesryString;
        
        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        int totalrate = 0; int questionid = 0;
                        ratingQuesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        // questionid = int.Parse(dsCount.Tables[0].Rows[0][1].ToString());
                        // lblMessage.Text += sectioname + " ratingquesCount= " + ratingQuesValue.ToString();// 021209 bip
                        if (testsectionid > 0)
                        {
                            var optionCount = from optcount in dataclass.View_TestBaseQuestionLists
                                              where optcount.TestSectionId == testsectionid && optcount.TestId == testid && optcount.Category == "RatingType" && optcount.TestBaseQuestionStatus == 1
                                              select optcount;
                            if (optionCount.Count() > 0)
                            {
                                if (optionCount.First().Option1 != null && optionCount.First().Option1.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option2 != null && optionCount.First().Option2.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option3 != null && optionCount.First().Option3.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option4 != null && optionCount.First().Option4.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option5 != null && optionCount.First().Option5.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option6 != null && optionCount.First().Option6.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option7 != null && optionCount.First().Option7.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option8 != null && optionCount.First().Option8.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option9 != null && optionCount.First().Option9.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option10 != null && optionCount.First().Option10.ToString() != "")
                                    totalrate++;
                                //if (totalrate > 0)
                                ratingQuesValue = ratingQuesValue * totalrate;

                                // lblMessage.Text += " totaloptions= " + totalrate + " TotRate= " + ratingQuesValue.ToString() + " ... ";
                            }
                        }
                        else
                        {
                            var optionCount = from optcount in dataclass.View_TestBaseQuestionLists
                                              where optcount.SectionName == sectioname && optcount.TestId == testid && optcount.Category == "RatingType" && optcount.TestBaseQuestionStatus == 1
                                              select optcount;
                            if (optionCount.Count() > 0)
                            {
                                if (optionCount.First().Option1 != null && optionCount.First().Option1.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option2 != null && optionCount.First().Option2.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option3 != null && optionCount.First().Option3.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option4 != null && optionCount.First().Option4.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option5 != null && optionCount.First().Option5.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option6 != null && optionCount.First().Option6.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option7 != null && optionCount.First().Option7.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option8 != null && optionCount.First().Option8.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option9 != null && optionCount.First().Option9.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option10 != null && optionCount.First().Option10.ToString() != "")
                                    totalrate++;
                                //if (totalrate > 0)
                                ratingQuesValue = ratingQuesValue * totalrate;
                            }
                        }
                    }
        //other type questions
        if (testsectionid > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category <> 'RatingType' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and  Category <> 'RatingType' and TestId=" + testid + " and SectionName='" + sectioname + "'";
        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                        quesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());

        totalmark = memimageQuesValue + memWordsQuesValue + quesValue + ratingQuesValue;
        
        return totalmark;
    }

    /*
    private double GetPercentileScore(string sectionname)
    {
        double percentilescore = 0;
        DataTable dtNomeValue = new DataTable();
        dtNomeValue.Columns.Add("SectionName");
        dtNomeValue.Columns.Add("QuestionID");
        dtNomeValue.Columns.Add("Question");
        dtNomeValue.Columns.Add("Answer");
        dtNomeValue.Columns.Add("UserAnswer");
        dtNomeValue.Columns.Add("Category");
        dtNomeValue.Columns.Add("UserId");
        dtNomeValue.Columns.Add("TestId");
        dtNomeValue.Columns.Add("TestSectionId");

        DataRow drNomeValue;

        //1. normal questions
        string querystring = "SELECT QuestionCollection.SectionName, EvaluationResult.QuestionID, EvaluationResult.Question, QuestionCollection.Answer, " +
                      "EvaluationResult.Answer AS UserAnswer, QuestionCollection.Category, EvaluationResult.UserId, EvaluationResult.TestId, " +
                      "EvaluationResult.TestSectionId FROM QuestionCollection INNER JOIN EvaluationResult ON QuestionCollection.QuestionID = EvaluationResult.QuestionID " +
                      "where TestId=" + testid;// +" and SectionName='" + sectionname + "'";
        DataSet dsNormValues = new DataSet();
        dsNormValues = clsClasses.GetValuesFromDB(querystring);
        if (dsNormValues != null)
            if (dsNormValues.Tables.Count > 0)
                if (dsNormValues.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsNormValues.Tables[0].Rows.Count; i++)
                    {
                        drNomeValue = dtNomeValue.NewRow();
                        drNomeValue["SectionName"] = dsNormValues.Tables[0].Rows[i]["SectionName"];
                        drNomeValue["QuestionID"] = dsNormValues.Tables[0].Rows[i]["QuestionID"];
                        drNomeValue["Question"] = dsNormValues.Tables[0].Rows[i]["Question"];
                        drNomeValue["Answer"] = dsNormValues.Tables[0].Rows[i]["Answer"];
                        drNomeValue["UserAnswer"] = dsNormValues.Tables[0].Rows[i]["UserAnswer"];
                        drNomeValue["Category"] = dsNormValues.Tables[0].Rows[i]["Category"];
                        drNomeValue["UserId"] = dsNormValues.Tables[0].Rows[i]["UserId"];
                        drNomeValue["TestId"] = dsNormValues.Tables[0].Rows[i]["TestId"];
                        drNomeValue["TestSectionId"] = dsNormValues.Tables[0].Rows[i]["TestSectionId"];

                        dtNomeValue.Rows.Add(drNomeValue);
                    }
                }


        //2.memmory type image questions
        querystring = "SELECT MemmoryTestImageQuesCollection.SectionName,EvaluationResult.QuestionID, EvaluationResult.Question, MemmoryTestImageQuesCollection.Answer, " +
                        "EvaluationResult.Answer AS UserAnswer, EvaluationResult.UserId, EvaluationResult.TestId, EvaluationResult.TestSectionId " +
                        "FROM MemmoryTestImageQuesCollection INNER JOIN EvaluationResult ON MemmoryTestImageQuesCollection.QuestionID = EvaluationResult.QuestionID " +
                        "WHERE EvaluationResult.Category = 'MemTestImages' and TestId=" + testid;// +" and SectionName='" + sectionname + "'";

        dsNormValues = new DataSet();
        dsNormValues = clsClasses.GetValuesFromDB(querystring);
        if (dsNormValues != null)
            if (dsNormValues.Tables.Count > 0)
                if (dsNormValues.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsNormValues.Tables[0].Rows.Count; i++)
                    {
                        drNomeValue = dtNomeValue.NewRow();
                        drNomeValue["SectionName"] = dsNormValues.Tables[0].Rows[i]["SectionName"];
                        drNomeValue["QuestionID"] = dsNormValues.Tables[0].Rows[i]["QuestionID"];
                        drNomeValue["Question"] = dsNormValues.Tables[0].Rows[i]["Question"];
                        drNomeValue["Answer"] = dsNormValues.Tables[0].Rows[i]["Answer"];
                        drNomeValue["UserAnswer"] = dsNormValues.Tables[0].Rows[i]["UserAnswer"];
                        drNomeValue["Category"] = "MemTestImages";// dsNormValues.Tables[0].Rows[i]["Category"];
                        drNomeValue["UserId"] = dsNormValues.Tables[0].Rows[i]["UserId"];
                        drNomeValue["TestId"] = dsNormValues.Tables[0].Rows[i]["TestId"];
                        drNomeValue["TestSectionId"] = dsNormValues.Tables[0].Rows[i]["TestSectionId"];

                        dtNomeValue.Rows.Add(drNomeValue);
                    }
                }

        //3.memmorytype word questions
        querystring = "SELECT MemmoryTestTextQuesCollection.SectionName, EvaluationResult.QuestionID, EvaluationResult.Question, " +
                        "MemmoryTestTextQuesCollection.Answer, EvaluationResult.Answer AS UserAnswer,EvaluationResult.UserId, EvaluationResult.TestId, EvaluationResult.TestSectionId " +
                        "FROM MemmoryTestTextQuesCollection INNER JOIN EvaluationResult ON MemmoryTestTextQuesCollection.QuestionID = EvaluationResult.QuestionID " +
                        "WHERE EvaluationResult.Category = 'MemTestWords' and TestId=" + testid;// +" and SectionName='" + sectionname + "'";

        dsNormValues = new DataSet();
        dsNormValues = clsClasses.GetValuesFromDB(querystring);
        if (dsNormValues != null)
            if (dsNormValues.Tables.Count > 0)
                if (dsNormValues.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsNormValues.Tables[0].Rows.Count; i++)
                    {
                        drNomeValue = dtNomeValue.NewRow();
                        drNomeValue["SectionName"] = dsNormValues.Tables[0].Rows[i]["SectionName"];//0
                        drNomeValue["QuestionID"] = dsNormValues.Tables[0].Rows[i]["QuestionID"];//1
                        drNomeValue["Question"] = dsNormValues.Tables[0].Rows[i]["Question"];//2
                        drNomeValue["Answer"] = dsNormValues.Tables[0].Rows[i]["Answer"];//3
                        drNomeValue["UserAnswer"] = dsNormValues.Tables[0].Rows[i]["UserAnswer"];//4
                        drNomeValue["Category"] = "MemTestWords";// dsNormValues.Tables[0].Rows[i]["Category"];//5
                        drNomeValue["UserId"] = dsNormValues.Tables[0].Rows[i]["UserId"];//6
                        drNomeValue["TestId"] = dsNormValues.Tables[0].Rows[i]["TestId"];//7
                        drNomeValue["TestSectionId"] = dsNormValues.Tables[0].Rows[i]["TestSectionId"];//8

                        dtNomeValue.Rows.Add(drNomeValue);
                    }
                }
        GridView2.DataSource = dtNomeValue;
        GridView2.DataBind();

        percentilescore = UserMarkSummarydetails();
        return percentilescore;

        
    }
    private double UserMarkSummarydetails()
    {
        double percentilescore = 0;
        DataTable dtUserIdColl = new DataTable();
        dtUserIdColl.Columns.Add("UserId");
        dtUserIdColl.Columns.Add("SectionName");
        DataRow drUserId;
        if (GridView2.Rows.Count > 0)
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                drUserId = dtUserIdColl.NewRow();
                string selUserId = ""; bool useridExists = false; string sectionname = "";
                selUserId = GridView2.Rows[i].Cells[6].Text; sectionname = GridView2.Rows[i].Cells[0].Text;
                if (dtUserIdColl.Rows.Count > 0)
                    for (int k = 0; k < dtUserIdColl.Rows.Count; k++)
                    {
                        if (selUserId == dtUserIdColl.Rows[k]["UserId"].ToString() && sectionname == dtUserIdColl.Rows[k]["SectionName"].ToString())
                        { useridExists = true; break; }
                    }

                if (useridExists == false)
                { drUserId["UserId"] = selUserId; drUserId["SectionName"] = sectionname; dtUserIdColl.Rows.Add(drUserId); }
            }

        DataTable dtMarkSummary = new DataTable();
        dtMarkSummary.Columns.Add("UserId");
        dtMarkSummary.Columns.Add("TotalMark");
        dtMarkSummary.Columns.Add("SectionName");
        DataRow drMarkSummary;

        dtEmptySessionList = new DataTable();
        DataRow drEmptySession;
        dtEmptySessionList.Columns.Add("SectionName");
        dtEmptySessionList.Columns.Add("BandDescription");

        int selusermark = 0;
        if (dtUserIdColl.Rows.Count > 0)
            for (int j = 0; j < dtUserIdColl.Rows.Count; j++)
            {
                string curUserid = "", sectionName = "";
                curUserid = dtUserIdColl.Rows[j]["UserId"].ToString();
                sectionName = dtUserIdColl.Rows[j]["SectionName"].ToString();
                int totalmarks = 0;
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    if (GridView2.Rows[i].Cells[6].Text == curUserid && GridView2.Rows[i].Cells[0].Text == sectionName)
                    {
                        if (GridView2.Rows[i].Cells[5].Text == "RatingType")
                        {
                            if (GridView2.Rows[i].Cells[3].Text != "" && GridView2.Rows[i].Cells[3].Text != "&nbsp;")
                                if (GridView2.Rows[i].Cells[4].Text == GridView2.Rows[i].Cells[3].Text)
                                    totalmarks = totalmarks + int.Parse(GridView2.Rows[i].Cells[3].Text);
                        }
                        else if (GridView2.Rows[i].Cells[5].Text == "FillBlanks")
                        {
                            string useranswer = GridView2.Rows[i].Cells[4].Text;
                            if (GridView2.Rows[i].Cells[3].Text != "" && GridView2.Rows[i].Cells[3].Text != "&nbsp;")
                                if (GridView2.Rows[i].Cells[3].Text.Contains(useranswer))
                                    totalmarks = totalmarks + 1;
                        }
                        else
                            if (GridView2.Rows[i].Cells[3].Text != "" && GridView2.Rows[i].Cells[3].Text != "&nbsp;")
                                if (GridView2.Rows[i].Cells[4].Text == GridView2.Rows[i].Cells[3].Text)
                                    totalmarks = totalmarks + 1;
                    }
                }
                if (totalmarks > 0)
                {
                    drMarkSummary = dtMarkSummary.NewRow();
                    drMarkSummary["UserId"] = curUserid;
                    drMarkSummary["TotalMark"] = totalmarks.ToString();
                    drMarkSummary["SectionName"] = sectionName;
                    dtMarkSummary.Rows.Add(drMarkSummary);

                    if (curUserid == userid.ToString())
                        selusermark = totalmarks;
                }
                else
                {

                    bool sesExists = false;
                    if (dtEmptySessionList.Rows.Count > 0)
                    {
                        for (int e = 0; e < dtEmptySessionList.Rows.Count; e++)
                        {
                            if (dtEmptySessionList.Rows[e]["SectionName"].ToString() == sectionName)
                                sesExists = true;
                        }
                    }
                    if (sesExists == false)
                        if (sectionName != "&nbsp;")
                        {
                            drEmptySession = dtEmptySessionList.NewRow();
                            drEmptySession["SectionName"] = sectionName;

                            string remarks = GetBandDescription(0, sectionName);
                            drEmptySession["BandDescription"] = remarks;
                            dtEmptySessionList.Rows.Add(drEmptySession);
                        }
                    
                }//
                //
                if (dtEmptySessionList.Rows.Count > 0)
                {
                    for (int e = 0; e < dtEmptySessionList.Rows.Count; e++)
                    {
                        Boolean exists = false;// CheckExistence(dtEmptySessionList.Rows[e][0].ToString());
                        if (dtMarkSummary.Rows.Count > 0)
                        {
                            for (int h = 0; h < dtMarkSummary.Rows.Count; h++)
                            {
                                sectionName = dtMarkSummary.Rows[h]["SectionName"].ToString();
                                if (dtEmptySessionList.Rows[e][0].ToString() == sectionName)
                                    exists = true;
                            }
                        }

                        if (exists == true)
                        {
                            dtEmptySessionList.Rows[e].Delete();
                        }
                    }
                }

                //
            }
        percentilescore = CalculatePercentileValue(dtMarkSummary, selusermark);
        return percentilescore;

    }
    private double CalculatePercentileValue(DataTable dtNomeValues, int totalmark)
    {
        dt = new DataTable();
        dt.Columns.Add("SectionID");
        dt.Columns.Add("SectionName");
        dt.Columns.Add("Marks");
        dt.Columns.Add("TestID");
        dt.Columns.Add("TotalMarks");
        dt.Columns.Add("BandDescription");
        DataRow dr;

        
        double score = 0;
        int XF = 0, CF = 0,totnum=0;
        if (dtNomeValues.Rows.Count > 0)
        {totnum = dtNomeValues.Rows.Count;

        DataTable dtSectionNameList = new DataTable();
        dtSectionNameList.Columns.Add("SectionName");
        dtSectionNameList.Columns.Add("UserId");
        DataRow drsectionName;
        string secName = ""; string seluserid = "";
        for (int j = 0; j < dtNomeValues.Rows.Count; j++)
        {
            secName = dtNomeValues.Rows[j]["SectionName"].ToString();
            seluserid = dtNomeValues.Rows[j]["UserId"].ToString();
            bool sectionExists = false;
            if (dtSectionNameList.Rows.Count > 0)            
                for (int i = 0; i < dtSectionNameList.Rows.Count; i++)
                {
                    if (dtSectionNameList.Rows[i]["SectionName"].ToString() == secName)
                    {sectionExists = true; break;}
                }
            
            if (sectionExists == false)
            {
                drsectionName = dtSectionNameList.NewRow();
                drsectionName["SectionName"] = secName;
                drsectionName["UserId"] = seluserid;
                dtSectionNameList.Rows.Add(drsectionName);
            }
        }
            for (int j = 0; j < dtSectionNameList.Rows.Count; j++)
            {
                //count++;
                string sectionname, useridsel;
                sectionname = dtSectionNameList.Rows[j]["SectionName"].ToString();
                useridsel=dtSectionNameList.Rows[j]["UserId"].ToString();
                for (int i = 0; i < dtNomeValues.Rows.Count; i++)
                {
                    if (dtNomeValues.Rows[i]["SectionName"].ToString() == sectionname)// && dtNomeValues.Rows[i]["UserId"].ToString() == useridsel)
                    {
                        double mark = double.Parse(dtNomeValues.Rows[i]["TotalMark"].ToString());
                        if (mark >= (totalmark - .5) && mark <= (totalmark + .5))
                            XF = XF + 1;
                        else if (mark < (totalmark - .5))
                            CF = CF + 1;
                    }
                }
                score = (CF + (.5 * XF) * 100) / totnum;
                //

                dr = dt.NewRow();
                dr["SectionName"] = sectionname;
                dr["TotalMarks"] = score.ToString();
                dt.Rows.Add(dr);
                if (txtValues.Text.Trim() != "")
                    txtValues.Text = txtValues.Text.Trim() + ",";
                txtValues.Text = txtValues.Text.Trim() + score.ToString();
                txtParts.Text = (j + 1).ToString();
            }

            GridView1.DataSource = dt; GridView1.DataBind();
            DrawPieGraph("imgReportGraph1Percentile" + userid + ".jpg", 1);
            DrawBarGraph();
        }
        return score;
    }

    */
}
