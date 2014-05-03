<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobCategoryControl.ascx.cs" Inherits="JobCategoryControl" %>
<div align="left" style="padding-top: 10px">
<table>
    <tr>
        <td align="left" valign="top">

    <table style="height: 350px">
        <tr>
            <td 
                colspan="2" valign="top">
                <div class="titlemain">
                Occupation Category</div>
            </td>
        </tr>
        <tr>
            <td height="10" valign="top" class="label">
                Occupation:</td>
            <td height="10" valign="top">
                <asp:TextBox ID="txtJobCategoryName" runat="server" Width="250px"></asp:TextBox>
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
            <td colspan="2" height="20" valign="top">
                <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="10" valign="top">
                &nbsp;</td>
            <td height="10" valign="top">
                <asp:Button ID="btnAdd" runat="server" Text="Submit" onclick="btnAdd_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Reset" 
                    onclick="btnClear_Click" />
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
                    <asp:GridView ID="gvwJobCategory" runat="server" AutoGenerateColumns="False" 
                    onselectedindexchanged="gvwJobCategory_SelectedIndexChanged" 
                    style="margin-bottom: 21px" BackColor="#E2E2E2" GridLines="None">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" SelectText="Edit" >
                                <ControlStyle Width="30px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="Name" HeaderText="Occupation Category" 
                                SortExpression="Name" />
                            <asp:BoundField DataField="Status" SortExpression="Status">
                                <ItemStyle ForeColor="#E2E2E2" Width="1px" Font-Size="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="JobCatID" SortExpression="JobCatID">
                                <ItemStyle ForeColor="#E2E2E2" Width="1px" Font-Size="1px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:LinqDataSource ID="LinqJobCat" runat="server" 
                        ContextTypeName="AssesmentDataClassesDataContext" 
                        Select="new (JobCatID, Name, Status)" TableName="JobCategories">
                    </asp:LinqDataSource>
                </asp:Panel>
            </td>
        </tr>
        </table>


        </td>
    </tr>
    
</table>
</div>