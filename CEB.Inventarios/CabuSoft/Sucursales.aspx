<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Sucursales.aspx.cs" Inherits="CabuSoft.Sucursales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-10">
            <h2>Administración de Sucursales</h2>
            <br />
            <asp:GridView ID="gvSucrusales" runat="server" AutoGenerateColumns="False" CssClass="table table-stripped" DataKeyNames="id_sucursal" DataSourceID="dsSucursales" OnRowUpdating="GridView1_RowUpdating" OnRowCommand="gvSucrusales_RowCommand">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" CancelText="Cancelar" DeleteText="Eliminar" EditText="Editar" NewText="Nuevo" UpdateText="Aceptar" />
                    <asp:BoundField DataField="id_sucursal" HeaderText="id_sucursal" ReadOnly="True" SortExpression="id_sucursal" Visible="False" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                    <asp:BoundField DataField="direccion" HeaderText="Dirección" SortExpression="direccion" />
                    <asp:BoundField DataField="telefono" HeaderText="Teléfono" SortExpression="telefono" />
                    <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                    <asp:BoundField DataField="id_empresa" HeaderText="id_empresa" SortExpression="id_empresa" Visible="False" />
                    <asp:ButtonField CommandName="Usuarios" Text="Usuarios" />
                </Columns>
            </asp:GridView>
            <h3>Agregar Sucursal</h3>
            <table class="table table-stripped">
                <tr>
                    <td>Nombre:<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"  />
                    </td>
                    <td>Dirección:<br />
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"  />
                    </td>
                    <td>Teléfono:<br />
                        <asp:TextBox ID="txtTelefono" runat="server"  CssClass="form-control"  />
                    </td>
                    <td>Email:<br />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"  />
                        <asp:RegularExpressionValidator ID="validateEmail"
                                        runat="server" ErrorMessage="Debe ingresar un Email Valido."
                                        ControlToValidate="txtEmail"  CssClass="text-danger"
                                        ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
                    </td>
                    <td><br />
                        <asp:Button ID="btnAdd" runat="server" Text="Agregar" OnClick="Insert" CssClass="btn btn-info"  />
                    </td>
                </tr>
            </table>
            <asp:EntityDataSource ID="dsSucursales" runat="server" ConnectionString="name=CABUConection" DefaultContainerName="CABUConection" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="tblSucursales" EntityTypeFilter="" Select="" Where="it.[id_empresa] = @id_empresa">
                <WhereParameters>
                    <asp:Parameter DbType="Int32" Name="id_empresa" />
                </WhereParameters>
            </asp:EntityDataSource>
        </div>
    </div>
</asp:Content>
