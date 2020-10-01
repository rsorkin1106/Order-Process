<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="infragistics.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 498px;
        }
        .auto-style2 {
            width: 100%;
            height: 76px;
        }
        .auto-style3 {
            height: 407px;
        }
        .auto-style4 {
            height: 95px;
        }
        .auto-style5 {
            width: 382px;
        }
        .auto-style6 {
            width: 381px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="Label1" runat="server" Font-Names="Times New Roman" Font-Size="XX-Large" Text="Orders:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:GridView ID="gvOrders" runat="server" Height="20px" OnSelectedIndexChanged="gvOrders_SelectedIndexChanged" Width="1088px" AutoGenerateSelectButton="True">
                        <EditRowStyle Height="20px" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="auto-style2">
                        <tr>
                            <td class="auto-style5">
                                <asp:Button ID="btnAdd" runat="server" BorderStyle="Double" Text="Create new order" Width="200px" OnClick="btnAdd_Click" />
                            </td>
                            <td class="auto-style6">
                                <asp:Button ID="btnEdit" runat="server" BorderStyle="Double" Text="Edit order" Width="200px" OnClick="btnEdit_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" BorderStyle="Double" Text="Delete order" Width="200px" OnClick="btnDelete_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
