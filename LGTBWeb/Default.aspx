<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LGTBWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div style="height: 576px; width: 1120px; padding-top: 2em;">
        <div style="float:left;width:191px; ">
            <asp:Label ID="Label1" runat="server" Text="Genre:"></asp:Label>
            <br />
            <asp:ListBox ID="GenreBox" runat="server" DataSourceID="GenreDS" DataTextField="kind" DataValueField="kind" Height="263px" SelectionMode="Multiple" Width="184px" AppendDataBoundItems="True" CssClass="myListBox">
                <asp:ListItem>All</asp:ListItem>
            </asp:ListBox>
            <asp:Button ID="GenreSelect" runat="server" Text="Select" OnClick="GenreSelect_Click" />
            <asp:SqlDataSource ID="GenreDS" runat="server" ConnectionString="<%$ ConnectionStrings:LGTBConnectionString %>" ProviderName="<%$ ConnectionStrings:LGTBConnectionString.ProviderName %>" SelectCommand="SELECT DISTINCT &quot;kind&quot; FROM &quot;recipes&quot; ORDER BY &quot;kind&quot;"></asp:SqlDataSource>
        </div>
        <div style="float:left;width:924px; height: 613px;">
            <asp:Label ID="Label2" runat="server" Text="Results:"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            <asp:GridView ID="RecipeTable" runat="server" Width="757px" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="RecipesDS" Height="536px" EnableSortingAndPagingCallbacks="True" CssClass="myListBox">
                <Columns>
                    <asp:BoundField DataField="title" HeaderText="Recipe" SortExpression="title">
                    </asp:BoundField>
                    <asp:BoundField DataField="kind" HeaderText="Genre" SortExpression="kind">
                    </asp:BoundField>
                    <asp:BoundField DataField="link" HeaderText="Link" SortExpression="link"></asp:BoundField>
                </Columns>
                <PagerSettings Mode="NextPrevious" />
                <pagerstyle forecolor="Blue"
          backcolor="LightBlue"/>
            </asp:GridView>
            
            <asp:SqlDataSource ID="RecipesDS" runat="server" ConnectionString="<%$ ConnectionStrings:LGTBConnectionString %>" ProviderName="<%$ ConnectionStrings:LGTBConnectionString.ProviderName %>" SelectCommand="SELECT &quot;title&quot;, &quot;kind&quot;, &quot;link&quot; FROM &quot;recipes&quot; ORDER BY &quot;title&quot;"></asp:SqlDataSource>
            
        </div>
    </div>

</asp:Content>
