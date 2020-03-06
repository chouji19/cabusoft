<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ActivarUsuario.aspx.cs" Inherits="CabuSoft.ActivarUsuario" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <asp:Panel ID="pnlAdvertencia" CssClass="alert-dismissible" role="alert" runat="server" Visible="False">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <asp:Label ID="lblResultadoProceso" runat="server" Text="Resultado Proceso"></asp:Label>
        </asp:Panel>
        <h3>Bienvenido a CabuSoft.Com Software de inventarios</h3>
        <p>Por favor Ingrese una nueva contraseña con la que podra ingresar al Portal Web</p>

        <div class="col-lg-5 col-lg-offset-3">
            <h4>Actualizar la contraseña para el usuario:
                <asp:Label ID="lblUsuario" runat="server" Text=""></asp:Label></h4>
            <div class="form-horizontal">
                <asp:Panel ID="pnlPassword" runat="server" CssClass="form-group  has-feedback">
                    <asp:Label ID="Label15" runat="server" Text="Contraseña:" AssociatedControlID="txtPassword" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" Width="80%" TextMode="Password"
                        OnTextChanged="txtPassword_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" CssClass="text-danger"
                        ErrorMessage="La contraseña no puede estar vacia" ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblErrorPass" runat="server" CssClass="text-danger" Visible="false" Text="Debe ingresar una Contraseña que contenga una Mayuscula, una minuscula y un numero"></asp:Label>
                </asp:Panel>
                <div class="form-group">
                    <asp:Label ID="Label16" runat="server" Text="Repita Contraseña" AssociatedControlID="txtRePassword"></asp:Label>
                    <asp:TextBox ID="txtRePassword" CssClass="form-control" runat="server" Width="80%" TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator ID="comparePasswords" CssClass="text-danger"
                        runat="server"
                        ControlToCompare="txtPassword"
                        ControlToValidate="txtRePassword"
                        ErrorMessage="Las contraseñas no coinciden!"
                        Display="Dynamic" />
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="form-group">
                            <asp:Label ID="Label10" runat="server" Text="Ingrese los datos que ve en la imagen" AssociatedControlID="Captcha1"></asp:Label>
                            <cc1:CaptchaControl ID="Captcha1" runat="server"
                                CaptchaBackgroundNoise="Low" CaptchaLength="7"
                                CaptchaHeight="60" CaptchaWidth="200"
                                CaptchaLineNoise="None" CaptchaMinTimeout="5"
                                CaptchaMaxTimeout="240" FontColor="#529E00" />
                            <asp:TextBox ID="txtCaptcha" CssClass="form-control" runat="server" Width="40%"></asp:TextBox>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-danger"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:LinkButton ID="lknGuardar" runat="server" CssClass="btn btn-info" OnClick="lknGuardar_Click">
                    Guardar
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                </asp:LinkButton>
            </div>

        </div>
    </div>
</asp:Content>
