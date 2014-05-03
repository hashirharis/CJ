<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddQualificationControl.ascx.cs" Inherits="AddQualificationControl" %>
<div align="left" style="padding-top: 10px">
    <table>
        <tr>
            <td align="left" valign="top">
    <table >
        
        <tr>
            <td colspan="2" 
                valign="top">
                <div class="titlemain">
                Add Qualifications</div>
            </td>
        </tr>
        <tr>
            <td class="label">
                Qualification</td>
            <td>
                <asp:TextBox ID="txtQualification" runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                Status</td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="1">Active</asp:ListItem>
                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="Submit" />
                <asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" 
                    Text="Reset" />
                <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                    Text="Delete" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblMessage" runat="server" ForeColor="#CC3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Panel ID="Panel1" runat="server" Height="275px" HorizontalAlign="Left" 
                    ScrollBars="Auto" Width="300px">
                    <asp:GridView ID="gvwQualifications" runat="server" AutoGenerateColumns="False" 
                    onselectedindexchanged="gvwQualifications_SelectedIndexChanged" 
                    DataSourceID="LinqQualifications" BackColor="#E2E2E2" GridLines="None">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" SelectText="Edit" >
                                <ControlStyle Width="30px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="Qualification1" HeaderText="Qualification" 
                            SortExpression="Qualification1" ReadOnly="True" />
                            <asp:BoundField DataField="Status" SortExpression="Status" ReadOnly="True">
                                <ItemStyle Font-Size="1px" ForeColor="#E2E2E2" Width="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="QualificationId" SortExpression="QualificationId" 
                            ReadOnly="True">
                                <ItemStyle Font-Size="1px" ForeColor="#E2E2E2" Width="1px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:LinqDataSource ID="LinqQualifications" runat="server" 
                        ContextTypeName="AssesmentDataClassesDataContext" 
                        Select="new (Qualification1, QualificationId, Status)" 
                        TableName="Qualifications">
                    </asp:LinqDataSource>
                </asp:Panel>
            </td>
        </tr>
        </table>

            </td>
        </tr>
    </table>

</div>