<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IzinListesi.aspx.cs" Inherits="PTS2.IzinListesi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>İzin Listesi</title>
    <link href="img/users.ico" rel="shortcut_icon" />
    <script src="js/jquery.min.js"></script>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
        <link href="css/default.min.css" rel="stylesheet" />
    <link href="css/alertify.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
     <script src="js/alertify.min.js"></script>
    <link href="css/footable.min.css" rel="stylesheet" />
    <style type="text/css">
        .ustkısım {
            margin-bottom: 20px;
            margin-top: 20px;
        }
       
        .btn {
            margin-bottom: 10px;
        }

        .example th {
            text-align: center;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#example').DataTable();
        });

        $(function () {
            $('#tblCustomers').footable();
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div class=" container">
            <div class="ustkısım">
                <ul class="nav nav-tabs">
                    <li role="presentation" class="active"><a href="#">Personel İzin Listesi</a></li>
                </ul>

            </div>

            <div class="btn">
                <a href="Izin.aspx">
                    <button type="button" id="btnIzinn" class="btn btn-primary " runat="server" ><span class="glyphicon glyphicon-plus"></span> İzin Ekle</button>

                </a>
                <asp:Button ID="btnCikis" Text="Güvenli Çıkış" runat="server" CssClass="btn btn-danger" OnClick="btnCikis_Click" />
               
                <a href="admin.aspx">
                    <button id="btnVisibilty" runat="server" type="button" class="btn btn-success" visible="false"><span class="glyphicon glyphicon-arrow-left"></span> Geri</button>
                </a>
                

              
            </div>
            <div style="margin-bottom: 20px">

                <asp:CheckBox ID="cbOnaylananlar" runat="server" Text="Onaylananlar" OnCheckedChanged="cbOnaylananlar_CheckedChanged" AutoPostBack="true" />

                <asp:CheckBox ID="cbReddedilenler" runat="server" Text="Reddedilenler" OnCheckedChanged="cbReddedilenler_CheckedChanged" AutoPostBack="true" />


                <asp:CheckBox ID="cbIslemdeOlanlar" runat="server" Text="Henüz İşlemde Olanlar" OnCheckedChanged="cbIslemdeOlanlar_CheckedChanged" AutoPostBack="true" />
               
            </div>
            <div class="list-group">
              
                <table id="example" class="display  footable table table-bordered">
                    <thead>
                        <tr role="row">
                            <th style="width: 5%">Ad</th>
                            <th style="width: 5%">Soyad</th>
                            <th style="width: 7%">Bölüm</th>
                            <th style="width: 10%">İzin Başl.</th>
                            <th style="width: 8%">İzin Bitiş</th>
                            <th style="width: 10%">İzin Türü</th>
                            <th style="width: 10%">Açıklama</th>
                            <th style="width: 8%">Durumu</th>
                            <th style="width: 10%">İşlemi Yapan</th>
                            <th style="width: 10%">İşlem Tarihi</th>
                            <th style="width: 12%">İzin Al. Tarihi</th>
                            <th id="thOnayla" runat="server" style="width: 5%">İşlem</th>
                            

                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="dtIzinlerListesi" runat="server" OnItemCommand="dtIzinlerListesi_ItemCommand" OnItemDataBound="dtIzinlerListesi_ItemDataBound">

                            <ItemTemplate>
                                <tr>
                                    <td style="width: 5%">
                                        <%#Eval("ad") %>
                                    </td>
                                    <td style="width: 5%">
                                        <%#Eval("soyad") %>
                                    </td>
                                    <td style="width: 7%">
                                        <%#Eval("bolumAdi") %>
                                    </td>
                                    <td style="width: 10%">
                                        <%#Eval("baslamaTarihi") %>
                                    </td>
                                    <td style="width: 8%">
                                        <%#Eval("bitisTarihi") %>
                                    </td>
                                    <td style="width: 10%">
                                     <%--veritabanında boş olursa bu kısım hata verir--%>
                                    <%# PTS2.Web.Utilities.EnumExtensionMethods.GetDescription((PTS2.Common.Enumlar.IzinTipi)Convert.ToInt32( Eval("izinTuru"))) %>
                                    </td>
                                    <td style="width: 10%">
                                        <%#Eval("aciklama") %>
                                    </td>
                                    <td style="width: 8%">
                                        <%# PTS2.Web.Utilities.EnumExtensionMethods.GetDescription((PTS2.Common.Enumlar.IzinDurum)Convert.ToInt32( Eval("durum"))) %>
                                    </td>
                                    <td style="width: 10%">
                                        <%#Eval("onaylayan") %>
                                    </td>
                                    <td style="width: 10%">
                                        <%#Eval("onayTarihi") %>
                                    </td>
                                    <td style="width: 12%">
                                        <%#Eval("islemTarihi") %>
                                    </td>
                                    
                                     <td style="width: 5%" id="tdOnayla" runat="server">

                                          <asp:LinkButton ID="lbOnayla" runat="server"  CommandName="Onayla" onclick="lbOnayla_Click" CommandArgument='<%#Eval("izinID") %>'><span id="spanOnayla" runat="server" class="glyphicon glyphicon-ok-circle"></span>Onayla</asp:LinkButton>
                                          <asp:LinkButton ID="lbReddet" runat="server"  CommandName="Reddet" onclick="lbReddet_Click" CommandArgument='<%#Eval("izinID") %>'><span id="spanReddet" runat="server" class="glyphicon glyphicon-remove-circle"></span>Reddet</asp:LinkButton>
                                          <asp:LinkButton ID="lbKaldir" runat="server" Visible="false"  CommandName="Kaldir"  OnClick="lbKaldir_Click" CommandArgument='<%#Eval("izinID") %>'><span id="spanKaldir" runat="server" class="glyphicon glyphicon-remove-ban-circle"></span>Kaldır</asp:LinkButton>
                                        
                                    </td>
       
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>

        </div>


    </form>
</body>
</html>
