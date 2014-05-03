<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportPrint.aspx.cs" Inherits="ReportPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Career Judge: Center For Career Profiling and Development</title>
    <script language="javascript" type="text/javascript">    
    window.print();
    </script>
    <link rel="stylesheet" type="text/css" href="FJAStyles.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="width: 100%">    
        <div align="left" id="divPrintLayer" style="width: 800px">
            <asp:Panel ID="Panel1" runat="server">
                <table align="left" style="width: 800px">
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%" >
                                <tr>
                                    <td runat="server" colspan="4">
                                        <div align="right">
                                            <table style="width: 200px;">
                                                <tr>
                                                    <td style="padding: 10px; text-align: center; height: 30px;">
                                                        <font class="header1">Career</font><font class="header2">Judge</font></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <img alt="" src="images/logo_fja.jpg" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" colspan="4">
                                        <img alt="" src="images/ReportMainBorder.jpg" 
                                            style="width: 100%; height: 25px;" /></td>
                                </tr>
                                <tr>
                                    <td runat="server" colspan="4" 
                                        style="text-align: center; vertical-align: middle; padding: 10px; text-align: left; vertical-align: top; color: brown; font-weight: bold; font-family: Arial; font-size: 20px;">
                                        <asp:Label ID="lblTestName" runat="server" Font-Names="Franklin Gothic Heavy" 
                                            Font-Size="20pt" ForeColor="Brown"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" colspan="4" 
                                        style="text-align: center; vertical-align: middle; padding: 10px; text-align: left; vertical-align: middle; color: #FF0000; font-weight: bold; font-family: Arial; font-size: 20px;">
                                        <%--<asp:Label ID="lblReportType" runat="server" Font-Names="Franklin Gothic Heavy" 
                                            Font-Size="16pt"></asp:Label>--%>
                                        <asp:Label ID="lblReportType" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" colspan="4" 
                                        style="color: #003366; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px; padding-left: 10px;">
                                        Developed by Career Judge: Center For Career Profiling and Development</td>
                                </tr>
                                <tr>
                                    <td runat="server" colspan="4" style="padding-top: 15px;">
                                        <img alt="" src="images/ReportSubBorder.jpg" 
                                            style="width: 100%; height: 10px;" /></td>
                                </tr>
                                <tr>
                                    <td runat="server" colspan="4">
                                        <div align="right" style="padding-right: 15px">
                                            <table>
                                                <tr>
                                                    <td style="text-align: left; color: #003366; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px;">
                                                        Report prepared for&nbsp;
                                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; color: #003366; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px;">
                                                        Generarted on&nbsp;
                                                        <asp:Label ID="lblReportDate" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td ID="Td1" runat="server" colspan="4" 
                                        
                                        style="padding: 30px 10px 10px 10px; text-align: center; vertical-align: middle;">
                                        <div align="center">
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
                                    <td colspan="4" ID="tcellDescription1" runat="server" 
                                        style="padding: 5px 15px 5px 15px; text-align: justify;">
                                        <asp:Label ID="lblSummary1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="padding: 10px">
                                        <div align="center">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="imgGraph" runat="server" />
                                                    </td>
                                                    <td ID="tcelGraphHelp" runat="server" 
                                                        style="text-align: left; vertical-align: middle">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td ID="tcellDescription2" runat="server" colspan="4" 
                                        style="padding: 5px 15px 5px 15px; text-align: justify;">
                                        <asp:Label ID="lblSummary2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td ID="tcellDescriptionReport" runat="server" colspan="4" 
                                        style="padding: 5px 15px 5px 15px; text-align: justify;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td ID="tcellBarGraph" runat="server" colspan="4" 
                                        style="padding: 5px 2px 5px 15px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td runat="server" colspan="4" 
                                        style="padding: 5px 15px 5px 15px; text-align: justify; " 
                                        ID="tcellConclution">
                                        <asp:Label ID="lblConclusion" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" colspan="4" 
                                        
                                        style="text-align: left; padding-left: 15px; color: #808080; font-family: Arial; font-size: 8pt;">
                                        <b style="color: #808080">Disclaimer:</b> This report should be used in 
                                        conjunction with professional judgment. The statements included in this report 
                                        should be viewed as hypotheses to be validated against other sources of data 
                                        such as interviews, biographical data and other assessment results. All 
                                        information in the report is confidential and should be treated responsibly. For 
                                        professional interpretation of the report and any clarifications, you may 
                                        contact Career Judge: Center For Career Profiling and Development.</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView1" runat="server" Visible="False">
                                        </asp:GridView>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table >
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtValues" runat="server" Visible="False" ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtParts" runat="server" Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    
    </div>
    </form>
</body>
</html>
