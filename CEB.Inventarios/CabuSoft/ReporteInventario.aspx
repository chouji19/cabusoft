<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ReporteInventario.aspx.cs" Inherits="CabuSoft.ReporteInventario" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">

        <div class="col-lg-12">
            <h2 class="text-info">Reporte Inventarios</h2>
            <div class="form-inline">
                <div class="form-group">
                    <asp:Label ID="Label12" runat="server" Text="Categoria:" AssociatedControlID="txtCbCategoria"></asp:Label>
                    <div class="input-group">
                        <asp:DropDownList ID="txtCbCategoria" runat="server" CssClass="form-control"
                            DataSourceID="dsCategorias" DataTextField="nombre"
                            DataValueField="id_categoria" OnSelectedIndexChanged="txtCbCategoria_SelectedIndexChanged"
                            AutoPostBack="true" AppendDataBoundItems="true">
                            <Items>
                                <asp:ListItem Text="Select" Value="" Selected="True" />
                            </Items>
                        </asp:DropDownList>
                    </div>
                    <asp:EntityDataSource ID="dsCategorias" runat="server" ConnectionString="name=CABUConection" DefaultContainerName="CABUConection" EnableFlattening="False" EntitySetName="tblCategorias" EntityTypeFilter="" OrderBy="it.[nombre]" Select="">
                    </asp:EntityDataSource>

                </div>
                <div class="form-group">
                    <asp:Label ID="Label13" runat="server" Text="SubCategoria:" AssociatedControlID="txtCbSubCategoria"></asp:Label>
                    <div class="input-group">
                        <asp:DropDownList ID="txtCbSubCategoria" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <Items>
                                <asp:ListItem Text="Select" Value="" Selected="True" />
                            </Items>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <asp:LinkButton ID="lknBusqueda" runat="server" CssClass="btn btn-default" OnClick="lknBusqueda_Click" ValidationGroup="busqueda">
                            Buscar <span class="glyphicon glyphicon-search"></span>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        <span class="clearfix"></span>
        <br />
        <div class="col-lg-12">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="600px" Width="100%">
                <LocalReport ReportEmbeddedResource="CabuSoft.rptInventario.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="dbProductosTableAdapters.vProductosTableAdapter"></asp:ObjectDataSource>
</asp:Content>
