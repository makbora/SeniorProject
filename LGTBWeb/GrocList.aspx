<%@ Page Title="GrocList" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GrocList.aspx.cs" Inherits="LGTBWeb.GrocList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <asp:Label ID="ListLabel" runat="server" Font-Size="XX-Large" Text="My Grocery List:"></asp:Label>
    <asp:Label ID="EmptyLabel" runat="server" Font-Size="Large" Text="Label" Visible="False"></asp:Label>
        <asp:GridView ID="GroceryView" runat="server" Visible="False" BorderStyle="Solid" Width="706px" OnRowDeleting="GroceryView_RowDeleting" OnSelectedIndexChanged="GroceryView_SelectedIndexChanged">
            <Columns>
                <asp:ButtonField ButtonType="Button" CommandName="Select" Text="Remove" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="TotalLabel" runat="server" Text="Label" Visible="False"></asp:Label>
</div>
</asp:Content>
