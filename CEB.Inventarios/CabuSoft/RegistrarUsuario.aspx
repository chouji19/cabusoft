<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RegistrarUsuario.aspx.cs" Inherits="CabuSoft.RegistrarUsuario" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <asp:Wizard ID="Wizard1" runat="server" CssClass="col-lg-10" Width="100%" DisplaySideBar="False" OnFinishButtonClick="Wizard1_FinishButtonClick" ActiveStepIndex="0" CancelButtonText="Cancelar" FinishCompleteButtonText="Finalizar" FinishPreviousButtonText="Anterior" StepNextButtonText="Avanzar">
            <NavigationButtonStyle CssClass="btn btn-info" />
            <StartNavigationTemplate>
                <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="Avanzar" CssClass="btn btn-info" />
            </StartNavigationTemplate>
            <StepNavigationTemplate>
                <asp:Button ID="SteptNextButton" runat="server" CommandName="MoveNext" Text="Next" CssClass="btn btn-info" />
            </StepNavigationTemplate>
            <WizardSteps>
                <asp:WizardStep ID="WizardStep1" runat="server" Title="Step 1" StepType="Start">

                    <div class="progress">
                        <%--<div class="progress-bar  progress-bar-striped" role="progressbar" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100" style="width: 30%;">
                        </div>--%>
                        <asp:Panel ID="pnlProgress1" runat="server" CssClass="progress-bar  progress-bar-striped" role="progressbar" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100" Style="width: 30%;"></asp:Panel>
                    </div>
                    <h2>Datos de la Empresa</h2>
                    <div class="form-horizontal">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" Text="Nombre:*" AssociatedControlID="txtNombre"></asp:Label>
                                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" Width="80%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True" CssClass="text-danger" ErrorMessage="Debe ingresar un Nombre" ControlToValidate="txtNombre" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" Text="Tipo Identificación:*" AssociatedControlID="txtDDTipoId"></asp:Label>
                                    <asp:DropDownList ID="txtDDTipoId" runat="server" CssClass="form-control" Width="80%">
                                        <asp:ListItem Value="1">Cedula de Ciudadanía</asp:ListItem>
                                        <asp:ListItem Value="2">NIT</asp:ListItem>
                                        <asp:ListItem Value="3">Cedula Extranjeria</asp:ListItem>
                                        <asp:ListItem Value="4">Pasaporte</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" Text="Número:" AssociatedControlID="txtIdentificacion"></asp:Label>
                                    <asp:TextBox ID="txtIdentificacion" CssClass="form-control" runat="server" Width="80%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True" CssClass="text-danger"
                                        ErrorMessage="Debe ingresar un Numero de identificación" ControlToValidate="txtIdentificacion" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" Text="Dirección:" AssociatedControlID="txtDireccion"></asp:Label>
                                    <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server" Width="80%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="text-danger"
                                        ErrorMessage="Debe ingresar una Dirección Principal" ControlToValidate="txtDireccion" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" Text="Nombre Contacto:" AssociatedControlID="txtContacto"></asp:Label>
                                    <asp:TextBox ID="txtContacto" CssClass="form-control" runat="server" Width="80%"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" Text="Teléfono:" AssociatedControlID="txtTelefono"></asp:Label>
                                    <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server" Width="80%"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server" Text="Celular:" AssociatedControlID="txtCelular"></asp:Label>
                                    <asp:TextBox ID="txtCelular" CssClass="form-control" runat="server" Width="80%"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label9" runat="server" Text="Correo Electrónico:" AssociatedControlID="txtMail"></asp:Label>
                                    <asp:TextBox ID="txtMail" CssClass="form-control" runat="server" Width="80%" TextMode="Email"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="validateEmail"
                                        runat="server" ErrorMessage="Debe ingresar un Email Valido."
                                        ControlToValidate="txtMail"  CssClass="text-danger"
                                        ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="text-danger"
                                        ErrorMessage="Debe ingresar una Dirección De correo Electronico" ControlToValidate="txtMail" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                    </div>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep2" runat="server" Title="Step 2" StepType="Finish">
                    <div class="progress">
                        <%--<div class="progress-bar  progress-bar-striped" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%;">
                        </div>--%>
                        <asp:Panel ID="pnlProgress2" runat="server" CssClass="progress-bar  progress-bar-striped" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" Style="width: 60%;"></asp:Panel>
                    </div>
                    <h2>Datos de Usuario</h2>
                    <asp:Panel ID="pnlAdvertencia" CssClass="alert-dismissible" role="alert" runat="server" Visible="False">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <asp:Label ID="lblResultadoProceso" runat="server" Text="Resultado Proceso"></asp:Label>
                    </asp:Panel>
                    <div class="form-horizontal">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="Identifación:*" AssociatedControlID="txtIdUsuario"></asp:Label>
                                    <asp:TextBox ID="txtIdUsuario" CssClass="form-control" runat="server" Width="80%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="text-danger"
                                        ErrorMessage="Debe ingresar un numero de identificación" ControlToValidate="txtIdUsuario" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label11" runat="server" Text="Nombres:" AssociatedControlID="txtUNombres"></asp:Label>
                                    <asp:TextBox ID="txtUNombres" CssClass="form-control" runat="server" Width="80%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="text-danger"
                                        ErrorMessage="Debe ingresar un nombre" ControlToValidate="txtUNombres" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label12" runat="server" Text="Apellidos:" AssociatedControlID="txtApellidos"></asp:Label>
                                    <asp:TextBox ID="txtApellidos" CssClass="form-control" runat="server" Width="80%"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label18" runat="server" Text="Email:" AssociatedControlID="txtUEmail"></asp:Label>
                                    <asp:TextBox ID="txtUEmail" CssClass="form-control" runat="server" Width="80%"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                        runat="server" ErrorMessage="Debe ingresar un Email Valido."
                                        ControlToValidate="txtUEmail"  CssClass="text-danger"
                                        ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="text-danger"
                                        ErrorMessage="Debe ingresar una Dirección De correo Electronico" ControlToValidate="txtUEmail" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label13" runat="server" Text="Teléfono:" AssociatedControlID="txtUTelefono"></asp:Label>
                                    <asp:TextBox ID="txtUTelefono" CssClass="form-control" runat="server" Width="80%"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label14" runat="server" Text="Celular:" AssociatedControlID="txtUCelular"></asp:Label>
                                    <asp:TextBox ID="txtUCelular" CssClass="form-control" runat="server" Width="80%"></asp:TextBox>
                                </div>

                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:Label ID="Label17" runat="server" Text="Nombre de usuario:" AssociatedControlID="txtUsername"></asp:Label>
                                    <asp:TextBox ID="txtUsername" CssClass="form-control" runat="server" Width="80%"></asp:TextBox>
                                    <asp:Label ID="lblUserExiste" runat="server" CssClass="text-danger" Visible="false" Text="El usuario ya existe"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="text-danger"
                                        ErrorMessage="Debe ingresar un nonmbre de usuario" ControlToValidate="txtUsername" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
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
                                            <asp:TextBox ID="txtCaptcha" CssClass="form-control" runat="server"  Width="40%" ></asp:TextBox>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-danger"></asp:Label>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
    </div>
</asp:Content>
