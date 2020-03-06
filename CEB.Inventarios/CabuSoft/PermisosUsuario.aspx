<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PermisosUsuario.aspx.cs" Inherits="CabuSoft.PermisosUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-10">
            <asp:Panel ID="pnlAdvertencia" CssClass="alert-dismissible" role="alert" runat="server" Visible="False">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <asp:Label ID="lblResultadoProceso" runat="server" Text="Resultado Proceso"></asp:Label>
            </asp:Panel>
            <h3>Permisos del Usuario <asp:Label ID="lblUsuario" runat="server" Text=""></asp:Label></h3>
            <asp:CheckBoxList ID="cbPermisos" runat="server"></asp:CheckBoxList>
            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-info" OnClick="btnActualizar_Click" />
        </div>
    </div>
</asp:Content>
