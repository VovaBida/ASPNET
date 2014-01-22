<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="firstgit.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="Stylesheet" href="Style.css" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>

        <asp:GridView ID="Grid" runat="server" Height="187px" Width="507px" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="ID" DataSourceID="SqlDataSource1" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                <asp:BoundField DataField="Manufacture" HeaderText="Manufacture" SortExpression="Manufacture" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
                <asp:BoundField DataField="Size" HeaderText="Size" SortExpression="Size" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ASP_DBConnectionString %>" SelectCommand="SELECT * FROM [Product]"></asp:SqlDataSource>
        <br />
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
