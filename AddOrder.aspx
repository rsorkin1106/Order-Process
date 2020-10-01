<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddOrder.aspx.cs" Inherits="infragistics.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style9 {
            width: 1099px;
            height: 799px;
        }
        .auto-style10 {
            height: 263px;
            width: 549px;
        }
        .auto-style11 {
            height: 66px;
        }
        .auto-style12 {
            height: 281px;
        }
        .auto-style13 {
            height: 263px;
            width: 548px;
        }
        .auto-style14 {
            width: 100%;
            height: 300px;
        }
        .auto-style15 {
            width: 70px;
        }
        .auto-style16 {
            width: 271px;
        }
        .auto-style17 {
            width: 85px;
        }
        .auto-style19 {
            width: 130px;
        }
        .auto-style20 {
            width: 181px;
        }
        .auto-style21 {
            width: 117px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <div align="center">
        <asp:Label ID="Label9" runat="server" Text="Add Next Line" Font-Bold="True" Font-Names="Elephant" Font-Size="Large"></asp:Label>
            </div>
        <br />
        <table class="auto-style9">
            <tr>
                <td class="auto-style12" colspan="2">
                    <table class="auto-style14">
                        <tr>
                            <td class="auto-style15">
                                <asp:Label ID="Label1" runat="server" Text="Operator"></asp:Label>
                            </td>
                            <td class="auto-style16">
                                <asp:TextBox ID="tbOp" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="F Code"></asp:Label>
                            </td>
                            <td class="auto-style19">
                                <asp:Label ID="Label4" runat="server" Text="Qty Per"></asp:Label>
                            </td>
                            <td class="auto-style17">
                                <asp:Label ID="Label5" runat="server" Text="# of Cont."></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Cont. Type"></asp:Label>
                            </td>
                            <td class="auto-style21">
                                <asp:Label ID="Label7" runat="server" Text="UOM"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Priority"></asp:Label>
                            </td>
                            <td class="auto-style20">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style15">
                                <asp:Label ID="Label2" runat="server" Text="Order #"></asp:Label>
                            </td>
                            <td class="auto-style16">
                                <asp:Label ID="lblOrderNum" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbFCode" runat="server"></asp:TextBox>
                            </td>
                            <td class="auto-style19">
                                <asp:TextBox ID="tbQtyPer" runat="server"></asp:TextBox>
                            </td>
                            <td class="auto-style17">
                                <asp:DropDownList ID="ddlNum" runat="server" Height="20px" Width="65px">
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
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlContType" runat="server" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="name">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TestConnectionString %>" SelectCommand="SELECT * FROM [InfraContainers]"></asp:SqlDataSource>
                            </td>
                            <td class="auto-style21">
                                <asp:DropDownList ID="ddlUOM" runat="server" Height="25px" Width="97px">
                                    <asp:ListItem>KG</asp:ListItem>
                                    <asp:ListItem>G</asp:ListItem>
                                    <asp:ListItem>MG</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPrio" runat="server">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style20">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" Width="114px" OnClick="btnAdd_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="auto-style13">
                    <asp:GridView ID="gv1" runat="server" Height="20px" Width="848px" BorderStyle="Solid" BorderWidth="2px">
                        <rowstyle Height="20px" />
                        <alternatingrowstyle  Height="20px"/>
                    </asp:GridView>
                </td>
                <td class="auto-style10">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
            <tr>
                <td class="auto-style11" colspan="2">
                    <div align="center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Height="68px" Width="240px" OnClick="btnSubmit_Click1" />
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
