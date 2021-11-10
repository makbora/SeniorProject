<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div style="height: 576px; width: 1120px">
        <div style="float:left;width:191px; height: 303px;">
            <asp:Label ID="Label1" runat="server" Text="Genre:"></asp:Label>
            <br />
            <asp:ListBox ID="ListBox1" runat="server" DataSourceID="GenreDS" DataTextField="kind" DataValueField="kind" Height="263px" SelectionMode="Multiple" Width="184px"></asp:ListBox>
            <asp:Button ID="Button1" runat="server" Text="Select" />
            <asp:SqlDataSource ID="GenreDS" runat="server" ConnectionString="<%$ ConnectionStrings:LGTBConnectionString %>" ProviderName="<%$ ConnectionStrings:LGTBConnectionString.ProviderName %>" SelectCommand="SELECT DISTINCT &quot;kind&quot; FROM &quot;recipes&quot; ORDER BY &quot;kind&quot;"></asp:SqlDataSource>
        </div>
        <div style="float:left;width:924px; height: 613px;">
            <asp:Label ID="Label2" runat="server" Text="Results:"></asp:Label>
            <asp:GridView ID="GridView1" runat="server" Width="757px" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="RecipesDS" Height="536px">
                <Columns>
                    <asp:BoundField DataField="title" HeaderText="Recipe" SortExpression="title">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="kind" HeaderText="Genre" SortExpression="kind">
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="link" HeaderText="Link" SortExpression="link"></asp:BoundField>
                </Columns>
                <PagerSettings Mode="NextPrevious" />
            </asp:GridView>
            
            <asp:SqlDataSource ID="RecipesDS" runat="server" ConnectionString="<%$ ConnectionStrings:LGTBConnectionString %>" ProviderName="<%$ ConnectionStrings:LGTBConnectionString.ProviderName %>" SelectCommand="SELECT &quot;title&quot;, &quot;kind&quot;, &quot;link&quot; FROM &quot;recipes&quot; ORDER BY &quot;title&quot;"></asp:SqlDataSource>
            
        </div>
    </div>
    
</asp:Content>
