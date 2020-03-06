<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CabuSoft.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Login ID="Login1" runat="server"
        LoginButtonText="Ingresar"
 PasswordLabelText="Contraseña:" RememberMeText="Recordarme la proxima vez." TitleText=""
 UserNameLabelText="Usuario:" OnAuthenticate="Login1_Authenticate" UserNameRequiredErrorMessage="Debe Ingresar Nombre de usuario">
        <LoginButtonStyle CssClass="btn btn-info" />
        <TextBoxStyle CssClass="form-control" />
    </asp:Login>
</asp:Content>
