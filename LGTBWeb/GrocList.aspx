<%@ Page Title="GrocList" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GrocList.aspx.cs" Inherits="LGTBWeb.GrocList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <asp:Label ID="ListLabel" runat="server" Font-Size="XX-Large" Text="My Grocery List:"></asp:Label>
    <asp:Label ID="EmptyLabel" runat="server" Font-Size="Large" Text="Label" Visible="False"></asp:Label>
        <asp:GridView ID="GroceryView" runat="server" Visible="False">
        </asp:GridView>
</div>
</asp:Content>
