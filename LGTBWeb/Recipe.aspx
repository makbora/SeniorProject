﻿<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Recipe.aspx.cs" Inherits="LGTBWeb.Recipe" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label" Font-Bold="True" Font-Size="X-Large"></asp:Label>
    <div style="height: 263px">
        <div class ="row" style="margin-top: 2em;">
            <div class="col" style="margin-right: 1em;">
                <div style="float:left; margin-right: 1em;">
                    <asp:DataList ID="InstrList" runat="server" DataSourceID="DirDS" Width="650px">
                        <ItemTemplate>
                            <asp:Label ID="directionLabel" runat="server" Text='<%# Eval("direction") %>' />
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
            <div class="col">
                <div style="float:left;">
                    <asp:GridView ID="IngView" runat="server" AutoGenerateColumns="False" CssClass="mydatagrid" DataSourceID="ReqDS">
                        <Columns>
                            <asp:BoundField DataField="amount" HeaderText="amount" SortExpression="amount" />
                            <asp:BoundField DataField="measurement" HeaderText="measurement" SortExpression="measurement" />
                            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                            <asp:BoundField DataField="price" HeaderText="price" SortExpression="price" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="ReqDS" runat="server" ConnectionString="<%$ ConnectionStrings:LGTBConnectionString %>" ProviderName="<%$ ConnectionStrings:LGTBConnectionString.ProviderName %>" SelectCommand="SELECT amount,measurement,name,price FROM requirements , ingredients where recid=1081 AND requirements.ingid=ingredients.ingid;"></asp:SqlDataSource>
    <asp:SqlDataSource ID="DirDS" runat="server" ConnectionString="<%$ ConnectionStrings:LGTBConnectionString %>" ProviderName="<%$ ConnectionStrings:LGTBConnectionString.ProviderName %>" SelectCommand="SELECT &quot;direction&quot; FROM &quot;instructions&quot; ORDER BY &quot;seq&quot;"></asp:SqlDataSource> 
</asp:Content>
