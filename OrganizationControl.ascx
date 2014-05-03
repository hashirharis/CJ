<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrganizationControl.ascx.cs" Inherits="OrganizationControl" %>
<div align="left" style="padding-top: 10px">
<table>
    <tr>
        <td align="left">


<table style="height: 350px">
        <tr>
            <td colspan="2" 
                valign="top">
                <div class="titlemain">
                Organization</div>
            </td>
        </tr>
        <tr>
            <td class="label" >
                Organization Name:</td>
            <td >
                <asp:TextBox ID="txOrgName" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label" >
                Image on left</td>
            <td >
                <input id="txtLogoLeft" style="width: 210px" type="text" runat="server" 
                    readonly="readonly"/><input id="Button2" 
                    type="button" value="Browse ..." onclick="dispQuestionFile('txtLogoLeft');" /><asp:Button 
                    ID="btnDeleteLogoLeft" runat="server" onclick="btnDeleteLogoLeft_Click" 
                    Text="Delete Logo" Width="90px" />
                            </td>
        </tr>
        <tr>
            <td class="label" >
                Image on Center</td>
            <td >
                <input id="txtLogoMiddle" style="width: 210px" type="text" runat="server" 
                    readonly="readonly"/><input id="Button3" 
                    type="button" value="Browse ..." onclick="dispQuestionFile('txtLogoMiddle');" /><asp:Button 
                    ID="btnDeleteLogoMiddle" runat="server" onclick="btnDeleteLogoMiddle_Click" 
                    Text="Delete Logo" Width="90px" />
                            </td>
        </tr>
        <tr>
            <td class="label" >
                Image on Right</td>
            <td >
                <input id="txtLogoRight" style="width: 210px" type="text" runat="server" 
                    readonly="readonly"/><input id="Button1" 
                    type="button" value="Browse ..." onclick="dispQuestionFile('txtLogoRight');" /><asp:Button ID="btnDeleteLogo" runat="server" onclick="btnDeleteLogo_Click" 
                    Text="Delete Logo" Width="90px" />
                            </td>
        </tr>
        <tr>
            <td class="label" >
                Organization Status:</td>
            <td >
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="1">Active</asp:ListItem>
                    <asp:ListItem Value="0">InActive</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td>
                <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td>
                <asp:Button ID="btnAdd" runat="server" Text="Submit" onclick="btnAdd_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Reset" 
                    onclick="btnClear_Click" />
                <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                    Text="Delete" />
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td >
                <asp:Panel ID="Panel1" runat="server" Height="275px" HorizontalAlign="Left" 
                    ScrollBars="Auto" Width="300px">
                    <asp:GridView ID="gvwOrganization" runat="server" AutoGenerateColumns="False" 
                    
    onselectedindexchanged="gvwOrganization_SelectedIndexChanged" BackColor="#E2E2E2" 
                        GridLines="None">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" SelectText="Edit" >
                                <ControlStyle Width="30px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="Name" HeaderText="Organization Name" 
                                SortExpression="Name" />
                            <asp:BoundField DataField="Status" SortExpression="Status">
                                <ItemStyle ForeColor="#E2E2E2" Width="1px" Font-Size="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OrganizationID" SortExpression="OrganizationID">
                                <ItemStyle ForeColor="#E2E2E2" Width="1px" Font-Size="1px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:LinqDataSource ID="Linqorg" runat="server" 
                        ContextTypeName="AssesmentDataClassesDataContext" 
                        Select="new (OrganizationID, Name, Status)" TableName="Organizations">
                    </asp:LinqDataSource>
                </asp:Panel>
            </td>
        </tr>
        </table>


        </td>
    </tr>
    
</table>
</div>