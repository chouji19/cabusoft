<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ReporteIngresosGastos.aspx.cs" Inherits="CabuSoft.ReporteIngresosGastos" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="form-inline">
                <div class="form-group">
                    <asp:Label ID="Label11" runat="server" Text="Desde:" AssociatedControlID="txtDesde"></asp:Label>
                    <asp:TextBox ID="txtDesde" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                         CssClass="text-danger" ErrorMessage="Debe ingresar una fecha inicial" ControlToValidate="txtDesde" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Debe ingresar una fecha valida" Type="Date" Operator="DataTypeCheck"
                        ControlToValidate="txtDesde" ValidationGroup="movimiento" Display="Dynamic" CssClass="text-danger"></asp:CompareValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Hasta:" AssociatedControlID="txtHasta"></asp:Label>
                    <asp:TextBox ID="txtHasta" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True"
                         CssClass="text-danger" ErrorMessage="Debe ingresar una fecha Final" ControlToValidate="txtHasta" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Debe ingresar una fecha valida" Type="Date" Operator="DataTypeCheck"
                        ControlToValidate="txtHasta" ValidationGroup="movimiento" Display="Dynamic" CssClass="text-danger"></asp:CompareValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label12" runat="server" Text="Tipo:" AssociatedControlID="txtTipo"></asp:Label>
                    <div class="input-group">
                        <asp:DropDownList ID="txtTipo" runat="server" CssClass="form-control">
                            <asp:ListItem Selected="True" Value="">Select...</asp:ListItem>
                            <asp:ListItem Value="Entrada">Entrada</asp:ListItem>
                            <asp:ListItem Value="Salida">Salida</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Descripción:" AssociatedControlID="txtTipo"></asp:Label>
                    <div class="input-group">
                        <asp:DropDownList ID="ddDescripcion" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                            <Items>
                                <asp:ListItem Text="Select" Value="" Selected="True" />
                            </Items>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <asp:LinkButton ID="lknBusqueda" runat="server" CssClass="btn btn-default" ValidationGroup="busqeuda" OnClick="lknBusqueda_Click">
                            Buscar <span class="glyphicon glyphicon-search"></span>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        <span class="clearfix"></span>
        <br />
        <div class="col-lg-12">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"  Height="600px" Width="100%" Visible="False">
                <LocalReport ReportEmbeddedResource="CabuSoft.rptIngresosGastos.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsIngresosGastos" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="dsIngresosGastosTableAdapters.vIngresosGastosTableAdapter"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
