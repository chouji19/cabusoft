<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="CabuSoft.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Administración de Productos</h3>
    <asp:HiddenField ID="txtId" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Panel ID="pnlAdvertencia" CssClass="alert-dismissible" role="alert" runat="server" Visible="False">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <asp:Label ID="lblResultadoProceso" runat="server" Text="Resultado Proceso"></asp:Label>
    </asp:Panel>
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
            <asp:EntityDataSource ID="dsListaProductos" runat="server" ConnectionString="name=CABUConection" DefaultContainerName="CABUConection" EnableFlattening="False" EntitySetName="tblProductos" Where="(it.[nombre] like'%' + @filtro +'%' or it.[codigo] like'%' + @filtro +'%' or it.[descripcion] like'%' + @filtro +'%' or it.[marca]  like'%' + @filtro +'%') and it.[id_sucursal] = @id_sucursal" OrderBy="it.[nombre]" EntityTypeFilter="" Select="">
                <WhereParameters>
                    <asp:Parameter DbType="String" Name="filtro" ConvertEmptyStringToNull="False" DefaultValue="" />
                    <asp:Parameter DbType="Int32" Name="id_sucursal" />
                </WhereParameters>
            </asp:EntityDataSource>
        </div>
        <div class="col-lg-9">
            <div class="media">

                <div class="media-body">
                    <h4 class="media-heading">Mi Producto</h4>
                    <asp:LinkButton ID="lknNuevo" runat="server" CssClass="btn btn-info" OnClick="lknNuevo_Click">Nuevo <span class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                    <asp:LinkButton ID="lknGuardar" runat="server" CssClass="btn btn-info" OnClick="lknGuardar_Click" ValidationGroup="producto">
                        <asp:Label ID="lblGuardar" runat="server" Text="Guardar"></asp:Label>
                        <span class="glyphicon glyphicon-floppy-disk"></span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lknEliminar" runat="server" CssClass="btn btn-info" OnClick="lknEliminar_Click">Eliminar <span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                </div>
                <div class="media-right">
                    <a href="#">
                        <asp:Image ID="txtArchivo" runat="server" Width="100" ImageUrl="Images/default_product.png" />
                    </a>
                </div>
            </div>
            <%--Media--%>
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="row">

                        <div class="form-horizontal">
                            <div class="col-lg-5 col-lg-offset-1">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="Codigo De Barras:" AssociatedControlID="txtCodigoBarras"></asp:Label>
                                    <asp:TextBox ID="txtCodigoBarras" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtEsServicio"
                                            EventName="CheckedChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <asp:Label ID="Label15" runat="server" Text="Servicio:" AssociatedControlID="txtEsServicio"></asp:Label>
                                            <asp:CheckBox ID="txtEsServicio" runat="server" OnCheckedChanged="txtEsServicio_CheckedChanged" AutoPostBack="true" />
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" Text="Nombre:*" AssociatedControlID="txtNombre"></asp:Label>
                                            <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True" CssClass="text-danger" ErrorMessage="Debe ingresar un Nombre" ControlToValidate="txtNombre" ValidationGroup="producto" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" Text="Descripción:" AssociatedControlID="txtDescripcion"></asp:Label>
                                            <asp:TextBox ID="txtDescripcion" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" Text="Precio Compra:" AssociatedControlID="txtPrecioCompra"></asp:Label>
                                            <div class="input-group">
                                                <span class="input-group-addon">$</span>
                                                <asp:TextBox ID="txtPrecioCompra" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="El valor Debe Ser Numerico" Type="Double"
                                                ControlToValidate="txtPrecioCompra" ValidationGroup="producto" Display="Dynamic" CssClass="text-danger"></asp:CompareValidator>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" Text="Precio Venta:*" AssociatedControlID="txtPrecio"></asp:Label>
                                            <div class="input-group">
                                                <span class="input-group-addon">$</span>
                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="El valor Debe Ser Numerico" Type="Double"
                                                ControlToValidate="txtPrecio" ValidationGroup="producto" Display="Dynamic" CssClass="text-danger"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="True" runat="server" CssClass="text-danger" ErrorMessage="Debe ingresar un Precio de Venta" ControlToValidate="txtPrecio" ValidationGroup="producto" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label6" runat="server" Text="Unidades Disponibles:" AssociatedControlID="txtUnidades"></asp:Label>
                                            <asp:TextBox ID="txtUnidades" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="El valor Debe Ser Numerico" Type="Double"
                                                ControlToValidate="txtUnidades" ValidationGroup="producto" Display="Dynamic" CssClass="text-danger"></asp:CompareValidator>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" Text="Marca:" AssociatedControlID="txtMarca"></asp:Label>
                                    <asp:TextBox ID="txtMarca" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-5" style="margin-left: 15px;">
                                <div class="form-group">
                                    <asp:Label ID="Label10" runat="server" Text="Imagen:" AssociatedControlID="txtImagen"></asp:Label>
                                    <asp:FileUpload ID="txtImagen" runat="server" CssClass="btn btn-default" />
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="Label9" runat="server" Text="Estado:" AssociatedControlID="txtCbEstado"></asp:Label>
                                    <asp:DropDownList ID="txtCbEstado" runat="server" CssClass="form-control" DataSourceID="dsEstadoProducto" DataTextField="nombre" DataValueField="id_estado"></asp:DropDownList>
                                    <asp:EntityDataSource ID="dsEstadoProducto" runat="server" ConnectionString="name=CABUConection" DefaultContainerName="CABUConection" EnableFlattening="False" EntitySetName="tblEstadosProductos">
                                    </asp:EntityDataSource>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label11" runat="server" Text="Fecha Ingreso:" AssociatedControlID="txtDpFecha"></asp:Label>
                                    <asp:TextBox ID="txtDpFecha" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Debe ingresar una fecha valida" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtDpFecha" ValidationGroup="producto" Display="Dynamic" CssClass="text-danger"></asp:CompareValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server" Text="Estado:" AssociatedControlID="txtCbEstado"></asp:Label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" DataSourceID="dsEstadoProducto" DataTextField="nombre" DataValueField="id_estado"></asp:DropDownList>
                                    <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=CABUConection" DefaultContainerName="CABUConection" EnableFlattening="False" EntitySetName="tblEstadosProductos">
                                    </asp:EntityDataSource>
                                </div>

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtCbCategoria"
                                            EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="lknBtnAddCategoria"
                                            EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="lknBtnAddCategoria"
                                            EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="lknBtnAddSubCategoria"
                                            EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="lknBtnAddSubCategoria"
                                            EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
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
                                                <span class="input-group-btn">
                                                    <asp:LinkButton ID="lknBtnAddCategoria" runat="server" CssClass="btn btn-success" OnClick="lknBtnAddCategoria_Click"><span class="glyphicon glyphicon-plus" ></asp:LinkButton>
                                                </span>
                                            </div>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtCategoriaNueva" runat="server" CssClass="form-control" Visible="false" placeholder="nombre categoria..."></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <asp:LinkButton ID="lknAddCategoria" runat="server" CssClass="btn btn-success" OnClick="lknAddCategoria_Click" Visible="false"><span class="glyphicon glyphicon-ok" ></asp:LinkButton>
                                                </span>
                                            </div>
                                            <asp:EntityDataSource ID="dsCategorias" runat="server" ConnectionString="name=CABUConection" DefaultContainerName="CABUConection" EnableFlattening="False" EntitySetName="tblCategorias" EntityTypeFilter="" OrderBy="it.[nombre]" Select="" Where="it.[id_empresa] = @id_empresa">
                                                <WhereParameters>
                                                    <asp:Parameter DbType="Int32" Name="id_empresa" />
                                                </WhereParameters>
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
                                                <span class="input-group-btn">
                                                    <asp:LinkButton ID="lknBtnAddSubCategoria" runat="server" CssClass="btn btn-success" OnClick="lknBtnAddSubCategoria_Click"><span class="glyphicon glyphicon-plus" ></asp:LinkButton>
                                                </span>
                                            </div>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtSubcategoriaNueva" runat="server" CssClass="form-control" Visible="false" placeholder="nombre subcategoria..."></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <asp:LinkButton ID="lknAddSubcategoria" runat="server" CssClass="btn btn-success" OnClick="lknAddSubcategoria_Click" Visible="false"><span class="glyphicon glyphicon-ok" ></asp:LinkButton>
                                                </span>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="form-group">
                                    <asp:Label ID="Label14" runat="server" Text="Activo:" AssociatedControlID="txtCBActivo"></asp:Label>
                                    <asp:CheckBox ID="txtCBActivo" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
