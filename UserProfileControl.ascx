<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserProfileControl.ascx.cs" Inherits="UserProfileControl" %>
<%--<link rel="stylesheet" type="text/css" href="FJAStyles.css" />--%>
<div align="center" style="padding-top: 10px; ">
<table>
    <tr>
        <td align="left">



    <table>
        <tr>
            <td colspan="2" 
                valign="top">
                <div class="titlemain">
                User Registration</div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="715">
                    <tr>
                        <td valign="top">
               <div style="width: 130px;color: Red; font-weight: bold;" align="right"> Privacy Statement:</div></td>
                        <td>
                                                        You are requested to enter the following personal details as part of the test 
                            administration </td>
                    
                    </tr>
                    <tr>
                        <td valign="top" colspan="2">
                            process. This information is required for data processing and communication 
                            purposes. Your personal information will not be transferred to any third parties 
                            or used for any unauthorized purposes.</td>
                    
                    </tr>
                </table>
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
                First Name:</td>
            <td>
                <asp:TextBox ID="txtFsName" runat="server" MaxLength="100" Width="350px"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="label">
                Middle Name:</td>
            <td>
                <asp:TextBox ID="txtMidName" runat="server" MaxLength="100" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                Last Name:</td>
            <td>
                <asp:TextBox ID="txtLstName" runat="server" MaxLength="100" Width="350px"></asp:TextBox>
                <asp:Label ID="Label5" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="label">
                Gender:</td>
            <td>
                <asp:DropDownList ID="ddlGender" runat="server" Width="80px">
                    <asp:ListItem>Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="label">
                Age:</td>
            <td>
                <asp:TextBox ID="txtAge" runat="server" Text="0"  
                                        onChange="myJSFunction(this);" 
                    inblur="myJSFunction(this);" MaxLength="3" Width="75px"></asp:TextBox>
                <asp:Label ID="Label6" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="label">
                Email:</td>
            <td>
                <asp:TextBox ID="txtEmailId" runat="server" MaxLength="100" Width="350px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtEmailId" ErrorMessage="Invalid email" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="label">
                Contact Number:</td>
            <td>
                <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="20" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                Name Of Organisation:</td>
            <td>
                <asp:DropDownList ID="ddlOrg" runat="server" AppendDataBoundItems="True" 
                    onselectedindexchanged="ddlOrg_SelectedIndexChanged" AutoPostBack="True" 
                    Enabled="False" Width="355px">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (Name, OrganizationID)" TableName="Organizations">
                </asp:LinqDataSource>
                <asp:TextBox ID="txtOrganization" runat="server" Visible="False" Width="350px" 
                    MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblUserGrp_label" runat="server" Text="User Group:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlGrpUser" runat="server" AppendDataBoundItems="True" 
                    AutoPostBack="True" 
                    onselectedindexchanged="ddlGrpUser_SelectedIndexChanged" Enabled="False" 
                    Width="355px">
                    <asp:ListItem Value="0">--select--</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="GrpUserLinqDataSource" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (GroupUserID, OrganizationID, JobCatID, GroupName)" 
                    TableName="GroupUsers">
                </asp:LinqDataSource>
            </td>
        </tr>
        <tr>
            <td class="label">
                Industry:</td>
            <td>
                <asp:DropDownList ID="ddlIndustry" runat="server" AppendDataBoundItems="True" 
                    AutoPostBack="True" 
                    onselectedindexchanged="ddlIndustry_SelectedIndexChanged" 
                    Width="355px">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqDataSource2" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (Name, IndustryID)" TableName="Industries">
                </asp:LinqDataSource>
                <br /><asp:TextBox ID="txtIndustry" runat="server" Visible="False" Width="350px" 
                    MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                Vocation:</td>
            <td>
                <asp:DropDownList ID="ddlJobCatgy" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="LinqDataSource3" DataTextField="Name" 
                    DataValueField="JobCatID" Width="355px">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqDataSource3" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" Select="new (Name, JobCatID)" 
                    TableName="JobCategories">
                </asp:LinqDataSource>
                <asp:Label ID="Label2" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="label">
                Designation:</td>
            <td>
                <asp:TextBox ID="txtJob" runat="server" MaxLength="100" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                Total Years of Experience:</td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlTotExpYears" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            Years</td>
                                        <td>
                                            <asp:DropDownList ID="ddlTotExpMonths" runat="server">
                                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Months</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">
                Experience in Present Job:</td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlCurExpYears" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            Years</td>
                        <td>
                                            <asp:DropDownList ID="ddlCurExpMonths" runat="server">
                                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                                
                                            </asp:DropDownList>
                                        </td>
                        <td>
                            Months</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">
                Educational Qualification:</td>
            <td>
                <asp:DropDownList ID="ddlQualification" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlQualification_SelectedIndexChanged" 
                    Width="350px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqQualifications" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (Qualification1, QualificationId)" TableName="Qualifications">
                </asp:LinqDataSource>
                <asp:Label ID="Label4" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                <br />
                <asp:TextBox ID="txtEduQual" runat="server" Visible="False" Width="350px" 
                    MaxLength="50"></asp:TextBox>
                
            </td>
        </tr>
        <tr>
            <td class="label">
                Professional Qualification :</td>
            <td>
                <asp:TextBox ID="txtEduQual_professional" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                Professional Certification:</td>
            <td>
                <asp:TextBox ID="txtProffQual" runat="server" TextMode="MultiLine" 
                    Height="100px" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td width="300">
                            &nbsp;</td>
                        <td>
                <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                    Text="Save and Proceed" />
                        </td>
                        <td>
                <asp:Button ID="btnExit" runat="server" Text="Exit" onclick="btnExit_Click" 
                                onclientclick="return confirm('You can break the session. You can resume the session from here, when you login again next time. Do you want to exit?')" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage0" runat="server" ForeColor="Red">* mandatory fields</asp:Label>
            </td>
        </tr>
    </table>


        </td>
    </tr>
</table>
</div>

