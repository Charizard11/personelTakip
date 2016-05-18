<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RolEkle.aspx.cs" Inherits="PTS2.RolEkle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rol Ekleme Sayfası</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="img/users.ico" rel="shortcut_icon" />
    <link href="css/footable.min.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="css/alertify.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/validator.js"></script>
    <script src="js/footable.min.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/alertify.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#tblRol').footable();
        });
    </script>
    <style type="text/css">
        .container {
            margin-bottom: 20px;
            margin-top: 20px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <div class=" container">
            <div>
                <ul class="nav nav-tabs">
                    <li role="presentation" class="active"><a href="#">Rol Ekle</a></li>
                </ul>
            </div>
        </div>
        <div class="col-md-4 col-md-offset-3">
            <table style="margin: auto; position: relative">
                <tr>
                    <td>Varsayılan Rolü :</td>
                    <td style="padding-left:10px">    
                        <asp:Literal ID="LiteralRolGuncelle" runat="server"></asp:Literal>
                     </td>
                </tr>
                <tr>
                    <td>Rol Ekle :</td>
                    <td style="padding-left: 10px">
                        <asp:TextBox ID="txtboxRolEkle" CssClass="form-control" placeholder="Rol Ekleyiniz.." runat="server" Width="235px" required />
                    </td>
                </tr>
                <tr>
                    <td>Üst Rol Seçiniz :</td>
                    <td style="padding-left: 10px">
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control" required>
                        </asp:DropDownList>
                    </td>
                </tr>
             
                <tr>
                    <td></td>
                    <td style="padding: 10px">
                        <a href="PersonelListe.aspx">
                            <button type="button" class="btn btn-success"><span class="glyphicon glyphicon-arrow-left"></span> Geri</button>
                        </a>
                        <asp:Button ID="btnRolEkle" CssClass="btn btn-primary" Text="Ekle" runat="server" OnClick="btnRolEkle_Click" />
                    </td>
                </tr>
            </table>
            <table id="tblRol" class="footable table table-bordered ">

                <asp:Repeater ID="rpRoller" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 40%">
                                <%#Eval("Adi") %>
                                
                            </td>
                            <td style="width: 10%">
                                <a href="RolDuzenle.aspx?RolID=<%#Eval("RolID")%>"><span class="glyphicon glyphicon-edit"></span>Düzenle</a>
                            </td>
                            <td style="width: 10%">
                                <a onclick="return confirm('Silmek İstiyormusunuz ?')" href="RolEkle.aspx?RolID=<%#Eval("RolID")%>&islem=sil"><span class="glyphicon glyphicon-trash"></span>Sil</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </table>
        </div>

    </form>
</body>
</html>
