<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="CabuSoft.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Administración de Categorias</h3>
    <asp:HiddenField ID="txtId" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lknBusqueda"
                EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lbCategorias"
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="lbSubcategorias"
                EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
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
                    <asp:ListBox ID="lbCategorias" runat="server" CssClass="form-control lista100 hidden-xs" DataSourceID="dsListaCategorias" DataTextField="nombre" DataValueField="id_categoria" OnSelectedIndexChanged="txtListaProductos_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                    <asp:EntityDataSource ID="dsListaCategorias" runat="server" ConnectionString="name=CABUConection" DefaultContainerName="CABUConection" EnableFlattening="False" EntitySetName="tblCategorias" Where="it.[nombre] like'%' + @filtro +'%' and   it.[id_empresa] = @id_empresa" OrderBy="it.[nombre]">
                        <WhereParameters>
                            <asp:Parameter DbType="String" Name="filtro" ConvertEmptyStringToNull="False" DefaultValue="" />
                            <asp:Parameter DbType="Int32" Name="id_empresa" />
                        </WhereParameters>
                    </asp:EntityDataSource>
                </div>
                <div class="col-lg-9">
                    <div class="media">
                        <div class="media-body">
                            <asp:LinkButton ID="lknAgregar" runat="server" CssClass="btn btn-info" OnClick="lknAgregar_Click"  ValidationGroup="categoria">Agregar <span class="glyphicon glyphicon-plus" ></span></asp:LinkButton>
                            <asp:LinkButton ID="lknGuardar" runat="server" CssClass="btn btn-info" OnClick="lknGuardar_Click" Visible="False"  ValidationGroup="categoria">
                                <asp:Label ID="lblGuardar" runat="server" Text="Guardar"></asp:Label>
                                <span class="glyphicon glyphicon-floppy-disk"></span>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lknEliminar" runat="server" CssClass="btn btn-info" OnClick="lknEliminar_Click" Visible="False">Eliminar <span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            Categorias
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-horizontal">
                                    <div class="col-lg-7 col-lg-offset-2" style="margin-left: 15px; margin-right: 15px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Text="Nombre Categoria:" AssociatedControlID="txtNombre"></asp:Label>
                                            <asp:TextBox ID="txtNombre" CssClass="form-control " runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ErrorMessage="Debe ingresar un nombre" ControlToValidate="txtNombre" ValidationGroup="categoria" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <span class="clearfix"></span>
                                    <div class="col-lg-9">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                Sub-Categorias
                                            </div>
                                            <div class="panel-body">
                                                <div class="media">
                                                    <div class="media-body">
                                                        <asp:LinkButton ID="lknAgregarSub" runat="server" CssClass="btn btn-info" OnClick="lknAgregarSub_Click" Visible="False"  ValidationGroup="subcategoria">Agregar <span class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                                                        <asp:LinkButton ID="lknGuardarSub" runat="server" CssClass="btn btn-info" OnClick="lknGuardarSub_Click" Visible="False"  ValidationGroup="subcategoria">
                                                            <asp:Label ID="lblSave" runat="server" Text="Guardar"></asp:Label>
                                                            <span class="glyphicon glyphicon-floppy-disk"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lknEliminarSub" runat="server" CssClass="btn btn-info" OnClick="lknEliminarSub_Click" Visible="False">Eliminar <span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="form-group" style="margin-left: 15px; margin-right: 15px;">
                                                    <asp:Label ID="Label2" runat="server" Text="Nombre SubCategoria:" AssociatedControlID="txtNombre"></asp:Label>
                                                    <asp:TextBox ID="txtSubCategoria" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                    ErrorMessage="Debe ingresar un nombre" ControlToValidate="txtSubCategoria" ValidationGroup="subcategoria" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group" style="margin-left: 15px; margin-right: 15px;">
                                                    <asp:ListBox ID="lbSubcategorias" runat="server" CssClass="form-control" OnSelectedIndexChanged="lbSubcategorias_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
