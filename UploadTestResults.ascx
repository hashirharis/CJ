<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadTestResults.ascx.cs" Inherits="UploadTestResults" %>


<div align="left">
    <table>
        <tr>
        <td colspan="2" 
                            valign="top">
                            <div class="titlemain">
                            Import Test Results data from an Excel File 
                            </div>
            </td>            
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="label">
                Test Result File(select excel files only)</td>
            <td>
                <asp:TextBox ID="txtfilename" runat="server" Width="200px" BackColor="White" 
                    Enabled="False"></asp:TextBox>
                <input id="btnBrowse" type="button" 
                    value="Browse file ...." onclick="dispExcelFile('txtfilename')" /></td>
        </tr>
        <tr>
            <td class="label">
                Excel Sheet Name</td>
            <td>
                <asp:TextBox ID="txtSheetName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;<div align="center">
                <asp:Button ID="btnUpdateAndImport" runat="server" onclick="btnUpdateAndImport_Click" 
                    Text="Update and Import Data from Excel file" Width="250px" 
                        ToolTip="Click here to update existing data and import the fresh data to DB" />
            &nbsp;&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
    </table>
</div>
