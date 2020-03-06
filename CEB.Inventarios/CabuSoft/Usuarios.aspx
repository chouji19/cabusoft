<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="CabuSoft.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-10">
            <h2>Usuarios:
                <asp:Label ID="lblSucursal" runat="server" Text=""></asp:Label></h2>
            <br />
            <asp:GridView ID="gvUsuarios" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="id_usuario" DataSourceID="dsUsuarios" OnRowCommand="gvUsuarios_RowCommand">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:ButtonField CommandName="Permisos" Text="Permisos" />
                    <asp:BoundField DataField="id_usuario" HeaderText="id_usuario" ReadOnly="True" SortExpression="id_usuario" Visible="False" />
                    <asp:BoundField DataField="username" HeaderText="Usuario" SortExpression="username" />
                    <asp:BoundField DataField="identificacion" HeaderText="Identificación" SortExpression="identificacion" />
                    <asp:BoundField DataField="nombres" HeaderText="Nombres" SortExpression="nombres" />
                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos" SortExpression="apellidos" />
                    <asp:BoundField DataField="celular" HeaderText="Celular" SortExpression="celular" />
                    <asp:BoundField DataField="direccion" HeaderText="Dirección" SortExpression="direccion" />
                    <asp:BoundField DataField="password" HeaderText="password" SortExpression="password" Visible="False" />
                    <asp:BoundField DataField="id_rol" HeaderText="id_rol" SortExpression="id_rol" Visible="False" />
                    <asp:BoundField DataField="fecha_ingreso" HeaderText="Ingreso" ReadOnly="True" SortExpression="fecha_ingreso" />
                    <asp:CheckBoxField DataField="activo" HeaderText="Activo" SortExpression="activo" />
                    <asp:BoundField DataField="id_sucursal" HeaderText="id_sucursal" SortExpression="id_sucursal" Visible="False" />
                    <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                </Columns>
            </asp:GridView>
            <h3>Agregar Usuario</h3>
            <asp:Panel ID="pnlAdvertencia" CssClass="alert-dismissible" role="alert" runat="server" Visible="False">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <asp:Label ID="lblResultadoProceso" runat="server" Text="Resultado Proceso"></asp:Label>
                    </asp:Panel>
            <table class="table table-stripped">
                <tr>
                    <td>Identificación:<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="text-danger"
                            ErrorMessage="Debe ingresar un numero de identificación" ControlToValidate="txtIdentificacion" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td>Nombres:<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True" CssClass="text-danger" ErrorMessage="Debe ingresar un Nombre" ControlToValidate="txtNombre" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td>Apellidos:<br />
                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" />
                    </td>
                    <td>Celular:<br />
                        <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control" />
                    </td>
                </tr>
                <tr>
                    <td>Dirección:<br />
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                    </td>
                    <td>Email:<br />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                        <asp:RegularExpressionValidator ID="validateEmail"
                            runat="server" ErrorMessage="Debe ingresar un Email Valido."
                            ControlToValidate="txtEmail" CssClass="text-danger"
                            ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="text-danger"
                            ErrorMessage="Debe ingresar una Dirección De correo Electronico" ControlToValidate="txtEmail" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td>Username:<br />
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
                        <asp:Label ID="lblUserExiste" runat="server" CssClass="text-danger" Visible="false" Text="El usuario ya existe"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="text-danger"
                                        ErrorMessage="Debe ingresar un nonmbre de usuario" ControlToValidate="txtUsername" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <br />
                        <asp:Button ID="btnAdd" runat="server" Text="Crear" OnClick="Insert" CssClass="btn btn-info" />
                    </td>
                </tr>
            </table>
            <asp:EntityDataSource ID="dsUsuarios" runat="server" ConnectionString="name=CABUConection" DefaultContainerName="CABUConection" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="tblUsuarios" Where="it.[id_sucursal] = @id_sucursal">
                <WhereParameters>
                    <asp:Parameter DbType="Int32" Name="id_sucursal" />
                </WhereParameters>
            </asp:EntityDataSource>

        </div>
    </div>
</asp:Content>
