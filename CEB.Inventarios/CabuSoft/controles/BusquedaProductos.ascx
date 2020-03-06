<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BusquedaProductos.ascx.cs" Inherits="CabuSoft.controles.BusquedaProductos" %>
<div class="input-group">
                <asp:TextBox ID="txtBusqueda" CssClass="form-control" runat="server" placeholder="buscar..."></asp:TextBox>
                <span class="input-group-btn">
                    <asp:LinkButton ID="lknBusqueda" runat="server" CssClass="btn btn-default" OnClick="lknBusqueda_Click">
                            <span class="glyphicon glyphicon-search"></span>
                    </asp:LinkButton>
                </span>
            </div>
            <hr class="success" />
            <asp:ListBox ID="txtListaProductos" runat="server" CssClass="form-control lista100 hidden-xs" DataSourceID="dsListaProductos" DataTextField="nombre" DataValueField="id_producto" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
            <asp:EntityDataSource ID="dsListaProductos" runat="server" ConnectionString="name=CABUConection" DefaultContainerName="CABUConection" EnableFlattening="False" EntitySetName="tblProductos" Where="it.[nombre] like'%' + @filtro +'%' or it.[codigo] like'%' + @filtro +'%' or it.[descripcion] like'%' + @filtro +'%' or it.[marca]  like'%' + @filtro +'%'" OrderBy="it.[nombre]">
                <WhereParameters>
                    <asp:Parameter DbType="String" Name="filtro" ConvertEmptyStringToNull="False" DefaultValue="" />
                </WhereParameters>
            </asp:EntityDataSource>