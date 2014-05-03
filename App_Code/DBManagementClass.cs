using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;

using System.IO;
using System.Web.Mail;
/// <summary>
/// Summary description for DBManagementClass
/// </summary>
public class DBManagementClass
{
    SqlCommand _sqlCommand;
    SqlConnection _sqlConnenction;
    SqlDataAdapter _sqlDataAdapter;
    SqlDataReader _sqlDataReader;
    String _connString;
    DataSet _dataSet;
    SqlParameter _sqlPararmeter;
    bool bLogin = false;
	public DBManagementClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string GetConnection()
    {
        return "Data Source=Compaq2\\sqlexpress;Initial Catalog=FJA2009;Integrated Security=True";
    }

    
    public void InsertValuesToDB(String[] _values, string _tableName)
    {
        try
        {
            _sqlConnenction = new SqlConnection(ConfigurationManager.AppSettings["conn"]);
            if (_sqlConnenction.State != ConnectionState.Open)
            {
                _sqlConnenction.Open();
            }
            _sqlCommand = new SqlCommand("SELECT * FROM [" + _tableName + "] where 1=0", _sqlConnenction);
            _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
            _dataSet = new DataSet();
            _sqlDataAdapter.Fill(_dataSet);
            _sqlCommand = new SqlCommand("sp_InsertInTo" + _tableName, _sqlConnenction);
            _sqlCommand.CommandType = CommandType.StoredProcedure;

            for (int i = 1; i < _dataSet.Tables[0].Columns.Count; i++)
            {

                _sqlPararmeter = new SqlParameter("@" + _dataSet.Tables[0].Columns[i].ColumnName, _values[i - 1]);
                _sqlCommand.Parameters.Add(_sqlPararmeter);
            }
            //_sqlCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

        }
        finally
        {
            _sqlConnenction.Close();

        }
    }
    public DataSet GetValues(string FieldName, string TableName)
    {
        try
        {
            _sqlConnenction = new SqlConnection(ConfigurationManager.AppSettings["conn"]);
            if (_sqlConnenction.State != ConnectionState.Open)
            {
                _sqlConnenction.Open();
            }
            _sqlCommand = new SqlCommand("SELECT Id," + FieldName + "  FROM [" + TableName + "]", _sqlConnenction);
            _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
            _dataSet = new DataSet();
            _sqlDataAdapter.Fill(_dataSet);
            return _dataSet;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataSet GetValuesFromDB(string queryString)
    {
        //try
        //{
            _sqlConnenction = new SqlConnection(ConfigurationManager.ConnectionStrings["talentscoutConnectionString"].ConnectionString);//.AppSettings["conn"]);
            if (_sqlConnenction.State != ConnectionState.Open)
            {
                _sqlConnenction.Open();
            }
            _sqlCommand = new SqlCommand(queryString, _sqlConnenction);
            _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
            _dataSet = new DataSet();
            _sqlDataAdapter.Fill(_dataSet);
            _sqlConnenction.Close();
            return _dataSet;
        //}
        //    catch (Exception ex)
        //{
        //    _sqlConnenction.Close();
        //    return null;
        //}
        
    }
    public void  ExcecuteDB(string queryString)
    {
        try
        {
            _sqlConnenction = new SqlConnection(ConfigurationManager.ConnectionStrings["talentscoutConnectionString"].ConnectionString);//.AppSettings["conn"]);
            if (_sqlConnenction.State != ConnectionState.Open)
            {
                _sqlConnenction.Open();
            }
            _sqlCommand = new SqlCommand(queryString, _sqlConnenction);
            _sqlCommand.ExecuteNonQuery();
           // _sqlConnenction.Close();
        }
        catch (Exception ex)
        {
          //  _sqlConnenction.Close();            
        }
        _sqlConnenction.Close();

    }
    public DataSet GetValues(string FieldName, string TableName, string Condition)
    {
        try
        {
            _sqlConnenction = new SqlConnection(ConfigurationManager.AppSettings["conn"]);
            if (_sqlConnenction.State != ConnectionState.Open)
            {
                _sqlConnenction.Open();
            }
            _sqlCommand = new SqlCommand("SELECT Id," + FieldName + "  FROM " + TableName + " where " + Condition, _sqlConnenction);
            _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
            _dataSet = new DataSet();
            _sqlDataAdapter.Fill(_dataSet);
            return _dataSet;
        }
        catch (Exception ex)
        {
            return null;
        }
        

    }
    public DataSet GetValues(string TableName)
    {
        try
        {
            _sqlConnenction = new SqlConnection(ConfigurationManager.AppSettings["conn"]);
            if (_sqlConnenction.State != ConnectionState.Open)
            {
                _sqlConnenction.Open();
            }
            _sqlCommand = new SqlCommand("SELECT *  FROM " + TableName, _sqlConnenction);
            _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
            _dataSet = new DataSet();
            _sqlDataAdapter.Fill(_dataSet);
            return _dataSet;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public void EmptyTextBoxes(Control parent)
    {
        // Loop through all the controls on the page
        foreach (Control c in parent.Controls)
        {
            // Check and see if it's a textbox
            if ((c.GetType() == typeof(TextBox)))
            {
                // Since its a textbox clear out the text    
                ((TextBox)(c)).Text = "";
                //((TextBox)(c)).ReadOnly = state;
            }
            // Now we need to call itself (recursive) because
            // all items (Panel, GroupBox, etc) is a container
            // so we need to check all containers for any
            // textboxes so we can clear them
            if (c.HasControls())
            {
                EmptyTextBoxes(c);
            }
        }
    }

    public void ReadOnlyTextBoxes(Control parent, Boolean state)
    {
        // Loop through all the controls on the page
        foreach (Control c in parent.Controls)
        {
            // Check and see if it's a textbox
            if ((c.GetType() == typeof(TextBox)))
            {
                // Since its a textbox clear out the text    
                //((TextBox)(c)).Text = "";
                ((TextBox)(c)).ReadOnly = state;
            }
            // Now we need to call itself (recursive) because
            // all items (Panel, GroupBox, etc) is a container
            // so we need to check all containers for any
            // textboxes so we can clear them
            if (c.HasControls())
            {
                ReadOnlyTextBoxes(c, state);
            }
        }
    }

    public void EnableDropDownList(Control parent, Boolean state)
    {
        // Loop through all the controls on the page
        foreach (Control c in parent.Controls)
        {
            // Check and see if it's a textbox
            if ((c.GetType() == typeof(DropDownList)))
            {
                // Since its a textbox clear out the text    
                //((TextBox)(c)).Text = "";
                ((DropDownList)(c)).Enabled = state;
            }
            // Now we need to call itself (recursive) because
            // all items (Panel, GroupBox, etc) is a container
            // so we need to check all containers for any
            // textboxes so we can clear them
            if (c.HasControls())
            {
                EnableDropDownList(c, state);
            }
        }
    }

    public void EnableCheckBox(Control parent, Boolean state)
    {
        // Loop through all the controls on the page
        foreach (Control c in parent.Controls)
        {
            // Check and see if it's a textbox
            if ((c.GetType() == typeof(CheckBox)))
            {
                // Since its a textbox clear out the text    
                //((TextBox)(c)).Text = "";
                ((CheckBox)(c)).Enabled= state;
            }
            // Now we need to call itself (recursive) because
            // all items (Panel, GroupBox, etc) is a container
            // so we need to check all containers for any
            // textboxes so we can clear them
            if (c.HasControls())
            {
                EnableCheckBox(c, state);
            }
        }
    }
    public void InsertUpdateValues(String[] _paramenters, String[] _values, String procName)
    {
        try
        {
            _sqlConnenction = new SqlConnection(ConfigurationManager.AppSettings["conn"]);
            if (_sqlConnenction.State != ConnectionState.Open)
            {
                _sqlConnenction.Open();
            }
            _sqlCommand = new SqlCommand(procName, _sqlConnenction);
            _sqlCommand.CommandType = CommandType.StoredProcedure;

            for (int i = 0; i < _paramenters.Length; i++)
            {
                _sqlPararmeter = new SqlParameter(_paramenters[i], _values[i]);
                _sqlCommand.Parameters.Add(_sqlPararmeter);
            }
            _sqlCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

        }
        finally
        {
            _sqlConnenction.Close();

        }
    }

    public bool Login(string UserID, string Password)
    {

        try
        {

            _sqlConnenction = new SqlConnection(ConfigurationManager.AppSettings["conn"]);
            if (_sqlConnenction.State != ConnectionState.Open)
            {
                _sqlConnenction.Open();
            }

            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnenction;
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.CommandText = "sp_NewLogin";
            _sqlCommand.Parameters.Add(new SqlParameter("@UserName", UserID));
            _sqlCommand.Parameters.Add(new SqlParameter("@Password", Password));
            _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
            _dataSet = new DataSet();
            _sqlDataAdapter.Fill(_dataSet);
            if (_dataSet.Tables[0].Rows.Count != 0)
                bLogin = true;
            else
                bLogin = false;


            ////////_sqlCommand = new SqlCommand();
            ////////_sqlCommand = new SqlConnection(strConn);
            ////////_sqlCommand.CommandType = CommandType.StoredProcedure;
            ////////_sqlCommand.CommandText = "sp_UserLogin";
            ////////_sqlCommand.Connection = _sqlConnenction;
            ////////_sqlConnenction.Open();
            ////////SqlParameter objcontactid = new SqlParameter("@ContactId", OleDbType.BigInt);//.Direction=ParameterDirection.Output;
            ////////_sqlCommand.Parameters.Add(objcontactid);
            ////////_sqlCommand.Parameters["@ContactId"].Direction = ParameterDirection.Output;
            ////////SqlParameter objuserName = new SqlParameter("@Username", OleDbType.VarChar);
            ////////objuserName.Value = txtUserName.Text;
            ////////_sqlCommand.Parameters.Add(objuserName);
            ////////SqlParameter objpassword = new SqlParameter("@Password", OleDbType.VarChar);
            ////////objpassword.Value = txtPassword.Text;
            ////////_sqlCommand.Parameters.Add(objpassword);
            ////////_sqlCommand.ExecuteNonQuery();
            ////////returnvalue = _sqlCommand.Parameters["@ContactId"].Value;

        }
        catch (SqlException ex)
        {

        }
        finally
        {
            _sqlConnenction.Close();
        }
        return bLogin;
    }
    

    public void SendMail(string to,string cc, string subject, string mailbody)
    {
        try
        {
           
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
            objmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "info@careerjudge.com");  //set your username here
            objmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "1info1"); //set your password here

            objmail.BodyFormat = MailFormat.Html;
            objmail.Priority = MailPriority.Normal;
            SmtpMail.SmtpServer = "smtpout.secureserver.net";//"smtpout.keralaenergy.net";//
            SmtpMail.Send(objmail);
        }
        catch (Exception ex) {  }


    }

    public void SendMail(string to, string cc, string subject, string name, string entityname, string username, string pwd, string ipadd, string browserdet, string osdet, string locipadd)
    {
        try
        {
            string mailBody = "<table border='1'><tr><td>Hi sir &nbsp; ,</td></tr><tr><td>&nbsp;</td></tr>" +
        "<tr><td>New User registered in EMC with Following Login details, please activate</td></tr>" +
        "<tr><td>&nbsp;</td></tr><tr><td>Name : " + name + "&nbsp;</td></tr>" + "<tr><td>EntityName : " + entityname + "&nbsp;</td></tr>" +
            "<tr><td >UserName : " + username + "&nbsp;</td></tr><tr><td >Password : " + pwd + "&nbsp;</td></tr><tr><td>&nbsp;</td></tr>" +
        "<tr><td>IPAddress : " + ipadd + "&nbsp;</td></tr>" + "<tr><td>BrowserDetail : " + browserdet + "&nbsp;</td></tr>" +
        "<tr><td>OperatingSystemDetail : " + osdet + "&nbsp;</td></tr>" + "<tr><td>LocalIPAddress : " + locipadd + "&nbsp;</td></tr>" + "<tr><td>&nbsp;</td></tr>" +        
        "<tr><td>Thank You.</td></tr><tr><td>&nbsp;</td></tr></table>";        
            //***********************************

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

            objmail.Subject = subject;
            objmail.Body = mailBody;

            objmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/cdoSMTPServerPort", "25");
            objmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/cdoSMTPConnectionTimeout", "20");
            objmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/cdoSendUsingPort", "2");
            objmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication
            
            objmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "info@careerjudge.com");  //"bipson.thomas@ivasystems.co.in"set your username here
            objmail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "1info1"); //set your password here

            objmail.BodyFormat = MailFormat.Html;
            objmail.Priority = MailPriority.Normal;
            SmtpMail.SmtpServer = "smtpout.secureserver.net";//"smtpout.keralaenergy.net";//
            SmtpMail.Send(objmail);
        }
        catch (Exception ex) { }


    }
    string indexnumbers = "";
    public string GenerateIndexNumber(int taskid, int subtaskid)
    {

        try
        {
            string queryString = "select * from subtaskactivity where taskid=" + taskid + " and activityid=0 order by subtaskid asc";
            _sqlConnenction = new SqlConnection(ConfigurationManager.ConnectionStrings["talentscoutConnectionString"].ConnectionString);//.AppSettings["conn"]);
            if (_sqlConnenction.State != ConnectionState.Open)
            {
                _sqlConnenction.Open();
            }
            _sqlCommand = new SqlCommand(queryString, _sqlConnenction);
            _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
            _dataSet = new DataSet();
            _sqlDataAdapter.Fill(_dataSet);
            _sqlConnenction.Close();
            if (_dataSet != null)
                if (_dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < _dataSet.Tables[0].Rows.Count; i++)
                    {
                        if (_dataSet.Tables[0].Rows[i]["SubTaskId"].ToString() == subtaskid.ToString()) { }


                    }
                }

        }
        catch (Exception ex)
        {
            _sqlConnenction.Close();
           // return null;
        }
        return indexnumbers;

    }

    public void WriteErrorDetails(string emailid, string usertype, string organization, string group, string fromdate, string todate, string status, string comments,string folderpath)
    {
        try
        {

            //string strfilename ="errorlist_" + DateTime.Parse(DateTime.Today.ToShortDateString()).ToString("dd-MM-yyyy") + ".txt";
            System.IO.StreamWriter StreamWriter1;
            string actiondateandtime = "";
            actiondateandtime = DateTime.Parse(DateTime.Now.ToString()).ToString("dd-MMM-yyyy hh:mm:ss tt");
            actiondateandtime = actiondateandtime.Replace(":", ".");
            if (File.Exists( folderpath))// + strfilename))
            {
                if (!File.Exists(folderpath))// + strfilename))
                {
                    //File.Copy(folderpath + "LogDetails.txt", folderpath + strfilename);
                    //File.Delete(folderpath + "LogDetails.txt");
                    StreamWriter1 = File.AppendText(folderpath);// + strfilename); //new System.IO.StreamWriter(Server.MapPath("test.txt"));
                    StreamWriter1.WriteLine("emailid" + "," + "userType" + "," + "Organization" + "," + "Group" + "," + "FromDate" + "," + "ToDate" + "," + "Status" + "," + "Comments");
                    StreamWriter1.WriteLine(DateTime.Parse(DateTime.Now.ToShortDateString()).ToString("dd-MMM-yyyy"));

                    StreamWriter1.WriteLine(emailid + "," + usertype + "," + organization + "," + group + "," + fromdate + "," + todate + "," + status + "," + comments);
                    StreamWriter1.WriteLine("-------------------------------");

                    StreamWriter1.Close();

                }
                else
                {
                    StreamWriter1 = File.AppendText(folderpath);// + strfilename); //new System.IO.StreamWriter(Server.MapPath("test.txt"));
                    StreamWriter1.WriteLine(emailid + "," + usertype + "," + organization + "," + group + "," + fromdate + "," + todate + "," + status + "," + comments);
                    StreamWriter1.WriteLine("-------------------------------");

                    StreamWriter1.Close();
                }
            }
            else
            {
                StreamWriter1 = File.AppendText(folderpath);// + strfilename); //new System.IO.StreamWriter(Server.MapPath("test.txt"));
                StreamWriter1.WriteLine("emailid" + "," + "userType" + "," + "Organization" + "," + "Group" + "," + "FromDate" + "," + "ToDate" + "," + "Status" + "," + "Comments");
                StreamWriter1.WriteLine(DateTime.Parse(DateTime.Now.ToShortDateString()).ToString("dd-MMM-yyyy"));
                StreamWriter1.WriteLine(emailid + "," + usertype + "," + organization + "," + group + "," + fromdate + "," + todate + "," + status + "," + comments);
                StreamWriter1.WriteLine("-------------------------------");

                StreamWriter1.Close();
                
            }
        }
        catch (Exception ex) { }

    }

}
