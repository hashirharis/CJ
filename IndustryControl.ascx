<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IndustryControl.ascx.cs" Inherits="IndustryControl" %>

<div align="left" style="padding-top: 10px">

    <table>
        <tr>
            <td align="left" valign="top">

<table style="height: 350px">
            <tr>
                <td colspan="2" valign="top">
                    
                    <div class="titlemain">
                    
                    Industry</div>
                </td>
            </tr>
            <tr>
                <td height="10" valign="top" class="label">
                    Industry Name:</td>
                <td height="10" valign="top">
                    <asp:TextBox ID="txtIndustryName" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="10" valign="top" class="label">
                    Status:</td>
                <td height="10" valign="top">
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value="1">Active</asp:ListItem>
                        <asp:ListItem Value="0">InActive</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="10" valign="top">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="10" valign="top">
                    &nbsp;</td>
                <td height="10" valign="top">
                    <asp:Button ID="btnAdd" runat="server" Text="Submit" onclick="btnAdd_Click" />
                    <asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" 
                        Text="Reset" />
                    <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                        Text="Delete" />
                </td>
            </tr>
            <tr>
                <td height="10" valign="top">
                    &nbsp;</td>
                <td height="10" valign="top">
                    <asp:Panel ID="Panel1" runat="server" Height="275px" HorizontalAlign="Left" 
                        ScrollBars="Auto" Width="300px">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        
    onselectedindexchanged="GridView1_SelectedIndexChanged" BackColor="#E2E2E2" GridLines="None">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" SelectText="Edit" >
                                    <ControlStyle Width="30px" />
                                </asp:CommandField>
                                <asp:BoundField DataField="Name" HeaderText="Industry Name" ReadOnly="True" 
                                SortExpression="Name" />
                                <asp:BoundField DataField="Status" ReadOnly="True" SortExpression="Status">
                                    <ItemStyle ForeColor="#E2E2E2" Width="1px" Font-Size="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IndustryID" ReadOnly="True" 
                                SortExpression="IndustryID">
                                    <ItemStyle ForeColor="#E2E2E2" Width="1px" Font-Size="1px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
                            ContextTypeName="AssesmentDataClassesDataContext" 
                            Select="new (Name, IndustryID, Status)" TableName="Industries">
                        </asp:LinqDataSource>
                    </asp:Panel>
                </td>
            </tr>
            </table>
                </td>
            </tr>
        </table>
</div>