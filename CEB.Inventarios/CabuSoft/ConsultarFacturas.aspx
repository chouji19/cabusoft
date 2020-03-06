<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ConsultarFacturas.aspx.cs" Inherits="CabuSoft.ConsultarFacturas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-lg-10">
            <h2 class="text-info">Buscar Facturas</h2>
            <div class="form-inline">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Fecha Inicial" AssociatedControlID="txtInicio"></asp:Label>
                    <asp:TextBox ID="txtInicio" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Debe ingresar una fecha valida"  Type="Date" Operator="DataTypeCheck"
                                                ControlToValidate="txtInicio" ValidationGroup="busqueda" Display="Dynamic" CssClass="text-danger"></asp:CompareValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Fecha Final" AssociatedControlID="txtFin"></asp:Label>
                    <asp:TextBox ID="txtFin" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Debe ingresar una fecha valida"  Type="Date" Operator="DataTypeCheck"
                                                ControlToValidate="txtFin" ValidationGroup="busqueda" Display="Dynamic" CssClass="text-danger"></asp:CompareValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Busqueda por coincidencia" AssociatedControlID="txtFiltro"></asp:Label>
                    <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:LinkButton ID="lknBusqueda" runat="server" CssClass="btn btn-default" OnClick="lknBusqueda_Click" ValidationGroup="busqueda">
                            Buscar <span class="glyphicon glyphicon-search"></span>
                    </asp:LinkButton>
                </div>
            </div>
            <span class="clearfix"></span><br />

            <div class="col-lg-12">
                <asp:GridView ID="gvFacturas" runat="server" CssClass="table table-striped" 
                    AutoGenerateColumns="False" OnRowCommand="gvFacturas_RowCommand"
                    AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvFacturas_PageIndexChanging" DataKeyNames="id_factura">
                    <Columns>
                        <asp:ButtonField CommandName="Ver" Text="Ver">
                        <ControlStyle CssClass="btn btn-info" />
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="Print" Text="Imprimir">
                        <ControlStyle CssClass="btn btn-info" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="celular" HeaderText="Celular" />
                        <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                        <asp:BoundField DataField="valor_bruto" HeaderText="Valor Bruto" />
                        <asp:BoundField DataField="iva" HeaderText="IVA" />
                        <asp:BoundField DataField="valor_neto" HeaderText="Valor Neto" />
                        <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
