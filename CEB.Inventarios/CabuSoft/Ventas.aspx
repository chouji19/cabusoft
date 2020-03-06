<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="CabuSoft.Ventas" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="txtId" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Panel ID="pnlAdvertencia" CssClass="alert-dismissible" role="alert" runat="server" Visible="False">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <asp:Label ID="lblResultadoProceso" runat="server" Text="Resultado Proceso"></asp:Label>
    </asp:Panel>
    <asp:HiddenField ID="txtIdProducto" runat="server" />
    <div class="row">
        <div class="col-lg-3">
            <div class="input-group">
                <asp:TextBox ID="txtBusqueda" CssClass="form-control" runat="server" placeholder="buscar..."></asp:TextBox>
                <span class="input-group-btn">
                    <asp:LinkButton ID="lknBusqueda" runat="server" CssClass="btn btn-default" OnClick="lknBusqueda_Click">
                            <span class="glyphicon glyphicon-search"></span>
                    </asp:LinkButton>
                </span>
            </div>
            <hr class="success" />
            <asp:ListBox ID="txtListaProductos" runat="server" CssClass="form-control lista100" DataSourceID="dsListaProductos" DataTextField="nombre" DataValueField="id_producto" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
            <asp:EntityDataSource ID="dsListaProductos" runat="server" ConnectionString="name=CABUConection" DefaultContainerName="CABUConection" EnableFlattening="False" EntitySetName="tblProductos" Where="it.[existencias]>0 and it.[activo] and   it.[id_sucursal] = @id_sucursal and   (it.[nombre] like'%' + @filtro +'%' or it.[codigo] like'%' + @filtro +'%' or it.[descripcion] like'%' + @filtro +'%' or it.[marca]  like'%' + @filtro +'%')" OrderBy="it.[nombre]">
                <WhereParameters>
                    <asp:Parameter DbType="String" Name="filtro" ConvertEmptyStringToNull="False" DefaultValue="" />
                    <asp:Parameter DbType="Int32" Name="id_sucursal" />
                </WhereParameters>
            </asp:EntityDataSource>
        </div>
        <div class="col-lg-9">
            <div class="media">

                <div class="media-body">
                    <h4 class="media-heading">Realizar Venta</h4>
                    <asp:LinkButton ID="lknLimpiar" runat="server" CssClass="btn btn-info" OnClick="lknLimpiar_Click">Limpiar <span class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                    <asp:LinkButton ID="lknAgregar" runat="server" CssClass="btn btn-info" OnClick="lknAgregar_Click">Agregar <span class="glyphicon glyphicon-floppy-disk"></span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lknTerminar" runat="server" CssClass="btn btn-info" OnClick="lknTerminar_Click" ValidationGroup="venta">Terminar <span class="glyphicon glyphicon-ok"></span></asp:LinkButton>

                </div>
                <div class="media-right">
                    <a href="#">
                        <asp:Image ID="txtArchivo" runat="server" Width="100" ImageUrl="Images/default_product.png" />
                    </a>
                </div>
            </div>

            <%--<asp:LinkButton ID="lknModal" runat="server" data-toggle="modal" data-target="#modalProductos" ></asp:LinkButton>--%>
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="row">

                        <div class="col-lg-5 col-lg-offset-1 left">
                            <table>
                                <tr>
                                    <th>Nombre:
                                    </th>
                                    <td style="padding-left: 15px;">
                                        <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>Categoria</th>
                                    <td style="padding-left: 15px;">
                                        <asp:Label ID="lblCategoria" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>SubCategoria</th>
                                    <td style="padding-left: 15px;">
                                        <asp:Label ID="lblSubCategoria" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-lg-5 col-lg-offset-1">
                            <table>
                                <tr>
                                    <th>Precío:</th>
                                    <td style="padding-left: 15px;">
                                        <asp:Label ID="lblPrecio" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>Descripción:</th>
                                    <td style="padding-left: 15px;">
                                        <asp:Label ID="lblDescripcion" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>Marca:</th>
                                    <td style="padding-left: 15px;">
                                        <asp:Label ID="lblMarca" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <span class="clearfix"></span>
                        <br />
                        <div class="form-horizontal">

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtCodigoBarras"
                                        EventName="TextChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="right" style="text-align: right; padding-right: 20px;">
                                        <h2>
                                            <asp:Label ID="lblValorFactura" runat="server" Text=""></asp:Label>
                                        </h2>
                                        <h2>
                                            <asp:Label ID="lblCambioTitulo" runat="server" Text=""></asp:Label>
                                        </h2>
                                    </div>
                                    <span class="clearfix"></span>
                                    <div class="col-lg-5 col-lg-offset-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Text="Codigo De Barras:" AssociatedControlID="txtCodigoBarras"></asp:Label>
                                            <asp:TextBox ID="txtCodigoBarras" CssClass="form-control" runat="server" OnTextChanged="txtCodigoBarras_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblUnidades" runat="server" Text="Unidades:" AssociatedControlID="txtUnidades"></asp:Label>
                                            <asp:TextBox ID="txtUnidades" CssClass="form-control" runat="server" TextMode="Number" Text="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ErrorMessage="Debe ingresar una Cantidad" ControlToValidate="txtUnidades" ValidationGroup="venta" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="El valor Debe Ser Numerico"  Type="Double"
                                                ControlToValidate="txtUnidades" ValidationGroup="venta" Display="Dynamic" CssClass="text-danger"></asp:CompareValidator>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" Text="Dirección:" AssociatedControlID="txtDireccion"></asp:Label>
                                            <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-5" style="margin-left: 15px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" Text="Comprador:" AssociatedControlID="txtNombre"></asp:Label>
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label10" runat="server" Text="Teléfono:" AssociatedControlID="txtCelular"></asp:Label>
                                            <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label11" runat="server" Text="Monto Recibido:" AssociatedControlID="txtRecibido"></asp:Label>
                                            <asp:TextBox ID="txtRecibido" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ErrorMessage="Debe ingresar el monto Recibido" ControlToValidate="txtRecibido" ValidationGroup="venta" CssClass="text-danger"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="El valor Debe Ser Numerico"  Type="Double"
                                                ControlToValidate="txtRecibido" ValidationGroup="venta" Display="Dynamic" CssClass="text-danger"></asp:CompareValidator>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="El valor Recibido no puede ser inferior al Valor de la factura"  Type="Double" ControlToCompare="txtTotal" Operator="LessThan"
                                                ControlToValidate="txtRecibido" ValidationGroup="venta" Display="Dynamic" CssClass="text-danger"></asp:CompareValidator>
                                            <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" TextMode="Number" Visible="false" Text="0"></asp:TextBox>
                                        </div>
                                    </div>
                                    <span class="clearfix"></span>
                                    <div class="col-lg-offset-1 col-lg-10">
                                        <asp:GridView ID="gvVentas" runat="server" CssClass="table   table-striped" OnRowCommand="gvVentas_RowCommand" AutoGenerateColumns="False" DataKeyNames="idProducto" OnRowEditing="gvVentas_RowEditing" OnRowUpdating="gvVentas_RowUpdating">
                                            <Columns>
                                                <asp:CommandField ButtonType="Button" CancelText="Cancelar" DeleteText="Eliminar" EditText="Editar" ShowEditButton="True">
                                                    <ControlStyle CssClass="btn btn-info" />
                                                </asp:CommandField>
                                                <asp:ButtonField Text="Eliminar" CommandName="Eliminar">
                                                    <ControlStyle CssClass="btn btn-danger" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre" ReadOnly="True" />
                                                <asp:TemplateField HeaderText="Cantidad">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditCantidad" runat="server" CssClass="form-control" Text='<%# Bind("cantidad") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("cantidad") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="valor" HeaderText="Valor" ReadOnly="True" DataFormatString="{0:C2}" />
                                                <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="True" DataFormatString="{0:C2}" />
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>



                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
