<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportPreviewControl.ascx.cs" Inherits="ReportPreviewControl" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<style type="text/css">

 p.MsoNormal
	{margin-bottom:.0001pt;
	font-size:12.0pt;
	font-family:"Times New Roman";
	    margin-left: 0in;
        margin-right: 0in;
        margin-top: 0in;
    }
</style>

<body style="text-decoration: underline">
    <asp:Panel ID="Panel2" runat="server">
        <table style="width:100%;" align="center">
            <tr>
                <td align="center" 
                    style="color: #000000; font-size: 18px; font-family: Arial, Helvetica, sans-serif">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="color: #FFFF00; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 20px; text-align: center; vertical-align: middle;">
                    Common IT Aptitude Test Report of&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblName" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="color:#4F6228; font-weight: bold; font-family: Arial, Helvetica, sans-serif">
                    Developed by Career Judge: Center For Career 
                        Profiling and Development
                </td>
            </tr>
            <tr>
                <td style="color: #FF9900; font-weight: bold; font-family: Arial, Helvetica, sans-serif">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="color: #FF9900; font-weight: bold; font-family: Arial, Helvetica, sans-serif;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center; vertical-align: middle">
                    <div>
                        <table>
                            <tr>
                                <td ID="tcellUserDetails" runat="server" 
                                    style="text-align: left; vertical-align: middle">
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" height="10px" align="left" bgcolor="White" 
                    style="padding: 5px 15px 5px 15px" valign="middle">
                    <asp:Label ID="lblSummary1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" height="10px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" style="vertical-align: middle; text-align: center">                    
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" 
                        BorderColor="White" DocumentMapCollapsed="True" DocumentMapWidth="100%" 
                        Enabled="False" Font-Names="Verdana" Font-Size="8pt" InternalBorderWidth="0px" 
                        PromptAreaCollapsed="True" ShowExportControls="False" ShowFindControls="False" 
                        ShowPageNavigationControls="False" ShowPrintButton="False" 
                        ShowPromptAreaButton="False" ShowRefreshButton="False" ShowZoomControl="False" 
                        style="margin-top: 0px; margin-right: 0px;" Width="750px">
                        <localreport reportpath="Report.rdlc">
                            <datasources>
                                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                                    Name="DataSet1_SectionMarks" />
                            </datasources>
                        </localreport>
                    </rsweb:ReportViewer>
                </td>
            </tr>
            <tr>
                <td colspan="3" height="10px">
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td colspan="3" style="vertical-align: middle; text-align: center">
                    <asp:Panel ID="pnlreportview" runat="server">
                    </asp:Panel>
                </td>
            </tr>
           
            <tr>
                <td colspan="3" align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" align="left" style="padding: 5px 15px 5px 15px">
                    <asp:Label ID="lblSummary2" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" colspan="3" 
                    
                    
                    style="padding: 5px 15px 5px 15px; color: #00FFFF; font-weight: bold; font-size: 16px">
                    <asp:Label ID="lblConclusion" runat="server" Font-Bold="False" 
                        ForeColor="Black"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" 
                    style="color: #00FFFF; font-weight: bold; font-size: 16px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" colspan="3" 
                    style="color: #00FFFF; font-weight: bold; font-size: 16px">
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>





    






    







    






