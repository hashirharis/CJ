<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddTestPassMarkControl.ascx.cs" Inherits="TestPassMarkControl" %>

<div align="left" style="padding-top: 10px; ">
    <table>
        <tr>
            <td align="left">
    <table style="width: 500px; height: 350px">
        <tr>
            <td colspan="2" 
                valign="top">
                <div class="titlemain">
                Evaluation Test Pass Mark</div>
            </td>
        </tr>
        <tr>
            <td class="label">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="label">
                Pass mark:</td>
            <td>
                <asp:TextBox ID="txtpassmark" runat="server" MaxLength="5" ></asp:TextBox>
                (%)</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblMesasage" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                    Text="Submit" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                    onclick="btnCancel_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" height="300" valign="top">
                &nbsp;</td>
        </tr>
    </table>

                </td>
            </tr>
        </table>

</div>