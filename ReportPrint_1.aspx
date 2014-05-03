<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportPrint.aspx.cs" Inherits="ReportPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Career Judge: Center For Career Profiling and Development</title>
    <script language="javascript" type="text/javascript">    
    window.print();
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <div align="left">
            <asp:Panel ID="Panel1" runat="server">
                <table align="left" width="600">
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table >
                                <tr>
                                    <td ID="tcellReportTitle" runat="server" colspan="4" 
                                        style="text-align: center; vertical-align: middle">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" 
                                        
                                        
                                        style="text-align: center; vertical-align: middle; color: brown; font-weight: bold; font-family: Arial; font-size: 20px; text-align: center; vertical-align: top; height: 40px;">
                                        <asp:Label ID="lblTestName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" 
                                        style="color: #FF0000; font-weight: bold; font-family: Arial; font-size: 20px; text-align: center; vertical-align: middle;">
                                        &nbsp;&nbsp;Test Report for&nbsp;&nbsp;
                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" 
                                        style="color: #76923C; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px; text-align: center; vertical-align: middle;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" 
                                        
                                        style="color: #003366; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px; text-align: center; vertical-align: middle;">
                                        Developed by Career Judge: Center For Career Profiling and Development</td>
                                </tr>
                                <tr>
                                    <td colspan="4" 
                                        
                                        style="color: #003366; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px; text-align: center; vertical-align: middle;">
                                        Dated :
                                        <asp:Label ID="lblReportDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" 
                                        style="text-align: center; vertical-align: middle; color: #FFFF00; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 20px;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td ID="Td1" runat="server" colspan="4" 
                                        style="text-align: center; vertical-align: middle;">
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
                                    <td ID="tcellDescription1" runat="server" colspan="4" 
                                        style="padding: 5px 15px 5px 15px; text-align: justify;">
                                        <asp:Label ID="lblSummary1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
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
                                        style="padding: 5px 25px 5px 30px; text-align: justify;">
                                        <asp:Label ID="lblSummary2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td ID="tcellDescriptionReport" runat="server" colspan="4" 
                                        style="padding: 5px 25px 5px 30px; text-align: justify;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td ID="tcellBarGraph" runat="server" colspan="4" 
                                        style="padding: 5px 25px 5px 30px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td ID="Td2" runat="server" colspan="4">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td ID="tcellConclution" runat="server" colspan="4" 
                                        style="padding: 5px 15px 5px 15px; text-align: justify;">
                                        <asp:Label ID="lblConclusion" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" colspan="4" 
                                        style="text-align: left; padding-left: 15px; color: #8484FF;">
                                        <b style="color: #808080">Disclaimer:</b> This report should be used in conjunction with professional 
                                        judgment. The statements included in this report should be viewed as hypotheses 
                                        to be validated against other sources of data such as interviews, biographical 
                                        data and other assessment results. All information in the report is confidential 
                                        and should be treated responsibly. For professional interpretation of the report 
                                        and any clarifications, you may contact Career Judge: Center For Career 
                                        Profiling and Development.</td>
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
