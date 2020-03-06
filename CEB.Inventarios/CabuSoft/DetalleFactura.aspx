<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DetalleFactura.aspx.cs" Inherits="CabuSoft.DetalleFactura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="main row">
        <h2>Detalles de la Factura</h2>
        <div class="col-lg-6">
            
            <table style="width: 100%;">
                <tr>
                    <th>Factura Número:</th>
                    <td>
                        <asp:Label runat="server" ID="lblIdFactura"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>Celular:</th>
                    <td>
                        <asp:Label runat="server" ID="lblCelular"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>Valor Bruto:</th>
                    <td>
                        <asp:Label runat="server" ID="lblValorBruto"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>IVA:</th>
                    <td>
                        <asp:Label runat="server" ID="lblIva"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>Valor Neto:</th>
                    <td>
                        <asp:Label runat="server" ID="lblValorNeto"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="col-lg-6">
            <table style="width: 100%;">
                <tr>
                    <th>Comprador:</th>
                    <td>
                        <asp:Label runat="server" ID="lblComprador"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>Dirección:</th>
                    <td>
                        <asp:Label runat="server" ID="lblDireccion"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>Fecha:</th>
                    <td>
                        <asp:Label runat="server" ID="lblFecha"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>Usuario:</th>
                    <td>
                        <asp:Label runat="server" ID="lblUsuario"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

        <span class="clearfix"></span>
        <div class="col-lg-12">
            <br />
            <h3>Productos</h3>
            <asp:GridView ID="gvVentas" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="tblProductos.nombre" HeaderText="Producto" />
                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                    <asp:BoundField DataField="valor_unitario" DataFormatString="{0:C2}" HeaderText="Valor Unitario" />
                    <asp:BoundField DataField="valor_total" DataFormatString="{0:c2}" HeaderText="Total" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:LinkButton ID="lknImprimir" runat="server" CssClass="btn btn-info" OnClick="LinkButton1_Click" >Imprimir <span class="glyphicon glyphicon-print"></span></asp:LinkButton>
    </div>
</asp:Content>
