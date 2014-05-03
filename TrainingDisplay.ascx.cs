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

public partial class TrainingDisplay : System.Web.UI.UserControl
{
   AssesmentDataClassesDataContext dataclassses = new AssesmentDataClassesDataContext();
    int trainingtypeid;
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckTrainingWindow();
    }
    private void CheckTrainingWindow()
    {
        if (Session["trainingtypeid"] == null)
        {
            Session["trainingtypeid"] = 1;
            Session["trainingtype"] = "text";
            FillText();

        }
        else
        {
            if (Session["trainingtype"].ToString() == "text")
                FillText();
            else
                FillVideo();

        }

    }
    //Session["trainingtype"] = null;Session["trainingtypeid"]=null
    private void FillText()
    {
        Panel1.Visible = true;
        Panel2.Visible = false;
        Session["demofile"] = null;
        container.InnerHtml = "";
        tcellDesc1.InnerHtml = ""; tcellDesc2.InnerHtml = ""; tcellVideoplayer.InnerHtml = "";

        if (Session["trainingtypeid"] != null)
            trainingtypeid = int.Parse(Session["trainingtypeid"].ToString());
        var Details = from det in dataclassses.UserTrainingDetails
                      where det.TrainingTypeId == trainingtypeid
                      select det;
        if (Details.Count() > 0)
        {
            string trainingdetails = "<div>";
            if (Details.First().Description1 != null && Details.First().Description1 != "")
                trainingdetails += Details.First().Description1.ToString();
            if (Details.First().Image1 != null && Details.First().Image1 != "")
                trainingdetails += "<br/>" + "<div align='center'><img Class='mainimage' Src=" + "images/" + Details.First().Image1.ToString() + " /></div>";
            if (Details.First().Description2 != null && Details.First().Description2 != "")
                trainingdetails += "<br/>" + Details.First().Description2.ToString();
            if (Details.First().Image2 != null && Details.First().Image2 != "")
                trainingdetails += "<br/>" + "<div align='center'><img Class='mainimage' Src=" + "images/" + Details.First().Image2.ToString() + " /></div>";
            if (Details.First().Description3 != null && Details.First().Description3 != "")
                trainingdetails += "<br/>" + Details.First().Description3.ToString();
            if (Details.First().Image3 != null && Details.First().Image3 != "")
                trainingdetails += "<br/>" + "<div align='center'><img Class='mainimage' Src=" + "images/" + Details.First().Image3.ToString() + " /></div>";

            if (Details.First().Description4 != null && Details.First().Description4 != "")
                trainingdetails += "<br/>" + Details.First().Description4.ToString();
            if (Details.First().Image4 != null && Details.First().Image4 != "")
                trainingdetails += "<br/>" + "<div align='center'><img Class='mainimage' Src=" + "images/" + Details.First().Image4.ToString() + " /></div>";

            if (Details.First().Description5 != null && Details.First().Description5 != "")
                trainingdetails += "<br/>" + Details.First().Description5.ToString();
            if (Details.First().Image5 != null && Details.First().Image5 != "")
                trainingdetails += "<br/>" + "<div align='center'><img Class='mainimage' Src=" + "images/" + Details.First().Image5.ToString() + " /></div>";
            if (trainingdetails == "<div>")
            {
               GoToNext(0);
            }
            else
                container.InnerHtml = trainingdetails;
        }
        else
        {
            GoToNext(0);
        }

    }
    private void GoToNext(int index)
    {
        if (index == 0)
        {
            Session["trainingtype"] = "video";
            if (Session["evaldirection"] != null)
                if (Session["evaldirection"] == "Previous")
                {
                    if (trainingtypeid == 1)
                    {
                        Session["SubCtrl"] = "UserTrainingControl.ascx";
                        Response.Redirect("FJAHome.aspx");
                        Session["trainingtypeid"] = null;
                    }


                    trainingtypeid--; Session["trainingtypeid"] = trainingtypeid;
                }

            FillVideo();
        }
        else if (index == 1)
        {
            Session["trainingtype"] = "text";
            if (Session["evaldirection"] != null)
                if (Session["evaldirection"] == "Next")
                {
                    if (trainingtypeid == 7)
                    {
                        Session["trainingtypeid"] = null;
                        Session["SubCtrl"] = "ObjectiveQuestns.ascx"; //"QuestionairIntroductionControl.ascx";
                        Response.Redirect("FJAHome.aspx");
                    }
                    trainingtypeid++; Session["trainingtypeid"] = trainingtypeid;
                }

            FillText();
           // ShowNextControl();

        }
       // ShowNextControl();

    }
    private void PlayVideo()
    {
        string FilePath = "";
       
        if (Session["demofile"] != null)
        {
            FilePath = "images/" + Session["demofile"].ToString();
            string bstr = "<object id='MediaPlayer' classid='CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95' standby='Loading Microsoft Windows Media Player components...' type='application/x-oleobject' > <param name='FileName' value='" + FilePath + "'> <param name='AnimationatStart' value='true'> <param name='TransparentatStart' value='true'> <param name='AutoStart' value='true'> <param name='ShowControls' value='1'></object> ";
            tcellVideoplayer.InnerHtml = bstr;
            lbtnreplay.Visible = true;
        }        

    }
    private void FillVideo()
    {
        if (Session["trainingtypeid"] != null)
            trainingtypeid = int.Parse(Session["trainingtypeid"].ToString());
        Panel1.Visible = false;
        Panel2.Visible = true;
        container.InnerHtml = "";
        bool valueexists = false;
        string filename = "";
    //    int trainingtypeid = 4;
        var Details = from det in dataclassses.UserTrainingDetails
                      where det.TrainingTypeId == trainingtypeid
                      select det;
        if (Details.Count() > 0)
        {
            string trainingdetails = "<div>";
            if (Details.First().VideoDescription1 != null && Details.First().VideoDescription1 != "")
            { tcellDesc1.InnerHtml = "<div>" + Details.First().Description1.ToString() + "</div>"; valueexists = true; }
            if (Details.First().VideoFile1 != null && Details.First().VideoFile1 != "")
            {
                valueexists = true;
                Session["demofile"] = Details.First().VideoFile1.ToString();
                PlayVideo();
            }
            //else tcellVideoplayer.InnerHtml = "<div align=Center><font color=Red>  Demo file not existing..</font></div>";
            if (Details.First().VideoDescription2 != null && Details.First().VideoDescription2 != "")
            { tcellDesc2.InnerHtml = "<div>" + Details.First().Description2.ToString() + "</div>"; valueexists = true; }
            

        }
        if (valueexists == false)
        {
            GoToNext(1);
        }

    }
    

    private void ShowPreviousControl()
    {
        Session["SubCtrl"] = "UserTrainingControl.ascx";
        Response.Redirect("FJAHome.aspx");
    }

    private void ShowNextControl()
    {
        if (Session["trainingtypeid"] != null)
            trainingtypeid = int.Parse(Session["trainingtypeid"].ToString());
        if (trainingtypeid == 7 && Session["trainingtype"].ToString() == "video")
        {
            Session["SubCtrl"] = "QuestionairIntroductionControl.ascx";
            Response.Redirect("FJAHome.aspx");

        }
        else
        {
            if (Session["trainingtype"].ToString() == "text")
            {
                Session["trainingtype"] = "video";
            }
            else
            {
                Session["trainingtype"] = "text";
                if(Session["evaldirection"] != "Previous")
                trainingtypeid++;
                Session["trainingtypeid"] = trainingtypeid;


            }
        }


       // Session["evaldirection"] = "Next";
        CheckTrainingWindow();


        //if (Session["evaldirection"] != null)
        //    if (Session["evaldirection"].ToString() == "Previous")
        //        ShowPreviousControl();



        //Session["SubCtrl"] = "TraingImpFacts.ascx";
        //Response.Redirect("FJAHome.aspx");
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
       
        if (Session["trainingtypeid"] != null)
            trainingtypeid = int.Parse(Session["trainingtypeid"].ToString());
        if (trainingtypeid == 7 && Session["trainingtype"].ToString() == "video")
        {
            Session["trainingtypeid"] = null;
            if (Session["evalResult"] != null)
                if (Session["evalResult"].ToString() == "Passed")
                {
                    Session["SubCtrl"] = "FreeFlowTaskEntryPhaseCtrl.ascx";
                    Response.Redirect("FJAHome.aspx");
                    return;
                }

            Session["SubCtrl"] = "ObjectiveQuestns.ascx";
            Response.Redirect("FJAHome.aspx");

            //Session["SubCtrl"] = "QuestionairIntroductionControl.ascx";
            //Response.Redirect("FJAHome.aspx");
            //Session["trainingtypeid"] = null;
        }
        else
        {            
            if (Session["trainingtype"].ToString() == "text")
            {
                Session["trainingtype"] = "video"; 
            }
            else
            {
                Session["trainingtype"] = "text";
                trainingtypeid++;
                Session["trainingtypeid"] = trainingtypeid; 
            }
        }

        Session["evaldirection"] = "Next";
        CheckTrainingWindow();
   //     ShowNextControl();
    }
    protected void ptnPrevious_Click(object sender, EventArgs e)
    {   
        if (Session["trainingtypeid"] != null)
            trainingtypeid = int.Parse(Session["trainingtypeid"].ToString());
        if (trainingtypeid == 1 && Session["trainingtype"].ToString() == "text")
        {
            Session["SubCtrl"] = "UserTrainingControl.ascx";
            Response.Redirect("FJAHome.aspx");
            Session["trainingtypeid"] = null;
        }
        else
        {            
            if (Session["trainingtype"].ToString() == "text")
            {
                trainingtypeid--;
                Session["trainingtype"] = "video";
                Session["trainingtypeid"] = trainingtypeid; 
            }
            else
            {
                Session["trainingtype"] = "text"; 
            }
        }
        Session["evaldirection"] = "Previous";
        CheckTrainingWindow();
    //    ShowPreviousControl();
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session["SubCtrl"] = null;
        //Session["MasterCtrl"] = "~/MasterPage.master";
        Response.Redirect("FJAHome.aspx"); 
    }
    protected void lbtnreplay_Click(object sender, EventArgs e)
    {

    }
}
