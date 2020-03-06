<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="IngresosGastos.aspx.cs" Inherits="CabuSoft.IngresosGastos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <asp:Panel ID="pnlAdvertencia" CssClass="alert-dismissible" role="alert" runat="server" Visible="False">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <asp:Label ID="lblResultadoProceso" runat="server" Text="Resultado Proceso"></asp:Label>
            </asp:Panel>
            <h3>Ingresos/Gastos</h3>
            <div class="form-inline">
                <div class="form-group">
                    <asp:Label ID="Label11" runat="server" Text="Fecha:" AssociatedControlID="txtDpFecha"></asp:Label>
                    <asp:TextBox ID="txtDpFecha" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Debe ingresar una fecha valida" Type="Date" Operator="DataTypeCheck"
                        ControlToValidate="txtDpFecha" ValidationGroup="movimiento" Display="Dynamic" CssClass="text-danger"></asp:CompareValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label12" runat="server" Text="Tipo:" AssociatedControlID="txtTipo"></asp:Label>
                    <div class="input-group">
                        <asp:DropDownList ID="txtTipo" runat="server" CssClass="form-control">
                            <asp:ListItem Selected="True" Value="">Select...</asp:ListItem>
                            <asp:ListItem Value="Entrada">Entrada</asp:ListItem>
                            <asp:ListItem Value="Salida">Salida</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
                <div class="form-group">
                    <asp:Label ID="Label5" runat="server" Text="Valor:" AssociatedControlID="txtPrecio"></asp:Label>
                    <div class="input-group">
                        <span class="input-group-addon">$</span>
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="True" runat="server" CssClass="text-danger" ErrorMessage="Debe ingresar un Valor" ControlToValidate="txtPrecio" ValidationGroup="producto" Display="Dynamic"></asp:RequiredFieldValidator>
                    <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="El valor Debe Ser Numerico" Type="Double"
                        ControlToValidate="txtPrecio" ValidationGroup="movimiento" Display="Dynamic" CssClass="text-danger"></asp:CompareValidator>--%>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lknBtnAddDescripcion"
                            EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="lknAddDescripcion"
                            EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="Descripción:" AssociatedControlID="txtTipo"></asp:Label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddDescripcion" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                    <Items>
                                        <asp:ListItem Text="Select" Value="" Selected="True" />
                                    </Items>
                                </asp:DropDownList>
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="lknBtnAddDescripcion" runat="server" CssClass="btn btn-success" OnClick="lknBtnAddCategoria_Click">Nuevo <span class="glyphicon glyphicon-plus" ></asp:LinkButton>
                                </span>
                            </div>
                            <div class="input-group">
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" Visible="false" placeholder="descripción..."></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="lknAddDescripcion" runat="server" CssClass="btn btn-success" OnClick="lknAddDescripcion_Click" Visible="false"><span class="glyphicon glyphicon-ok" ></asp:LinkButton>
                                </span>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="form-group">
                <asp:LinkButton ID="lknInsertar" runat="server" CssClass="btn btn-default" ValidationGroup="movimiento" OnClick="lknInsertar_Click">
                            Insertar <span class="glyphicon glyphicon-plus-sign"></span>
                </asp:LinkButton>
                <asp:LinkButton ID="lknBusqueda" runat="server" CssClass="btn btn-default" ValidationGroup="busqeuda" OnClick="lknBusqueda_Click">
                            Buscar <span class="glyphicon glyphicon-search"></span>
                </asp:LinkButton>


            </div>
        </div>
        <div class="col-lg-12">
            <asp:GridView ID="gvIngresos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" AllowPaging="True" OnPageIndexChanging="gvIngresos_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                    <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                    <asp:BoundField DataField="valor" DataFormatString="{0:C2}" HeaderText="Valor" />
                    <asp:BoundField DataField="fecha_ingreso" HeaderText="Fecha Registro" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
