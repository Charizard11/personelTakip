<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Izin.aspx.cs" Inherits="PTS2.Izin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>İzin Ekle Sayfası</title>
    <link href="img/users.ico" rel="shortcut_icon" />

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/default.min.css" rel="stylesheet" />
    <link href="css/alertify.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/moment-with-locales.js"></script>
    <script src="js/validator.js"></script>
    <script src="js/bootstrap-datetimepicker.js"></script>
    <script src="js/alertify.min.js"></script>
    <link href="css/bootstrap-datetimepicker.css" rel="stylesheet" />
    <style type="text/css">
        .kendiyazdigimCSS {
            margin-left: 20px;
        }

        .container {
            margin-bottom: 20px;
            margin-top: 20px;
        }

        .kendiyazdigimCSS td {
            padding: 2px;
        }

        .tablo {
            width: 325px;
        }

        .btn {
            margin-bottom: 10px;
        }

        .icon-input-btn {
            display: inline-block;
            position: relative;
        }

            .icon-input-btn input[type="submit"] {
                padding-left: 2em;
            }

            .icon-input-btn .glyphicon {
                display: inline-block;
                position: absolute;
                left: 0.65em;
                top: 30%;
            }
    </style>
    <script type="text/javascript">
        $(function () {
            debugger;
            $('#datetimepicker1').datetimepicker(
                
                {
                    format: 'DD.MM.YYYY HH:mm'
                    
                }

            );
            $('#datetimepicker2').datetimepicker(
                {
                    format: 'DD.MM.YYYY HH:mm'
                }
            );

        });

        $(document).ready(function () {
            $(".icon-input-btn").each(function () {
                var btnFont = $(this).find(".btn").css("font-size");
                var btnColor = $(this).find(".btn").css("color");
                $(this).find(".glyphicon").css("font-size", btnFont);
                $(this).find(".glyphicon").css("color", btnColor);
                if ($(this).find(".btn-xs").length) {
                    $(this).find(".glyphicon").css("top", "24%");
                }
            });
        });

        
        
    </script>
</head>
<body>
    <form id="form1" runat="server">


        <div class=" container">
            <div>
                <ul class="nav nav-tabs">
                    <li role="presentation" class="active"><a href="#">İzin Kayıt</a></li>
                </ul>
            </div>
        </div>

        <div class="row">
            <div class='col-md-3 col-md-offset-2'>
                <div class=" container">
                    <table class="kendiyazdigimCSS">
                        <asp:Panel ID="pnlIzin" runat="server">
                        <tr>
                            <td style="width: 15%; padding-bottom: 20px"><b>Personel AdSoyad:</b></td>
                            <td style="padding-bottom: 20px; padding-left: 5px; color: #d43f3a">
                                <asp:Label ID="lblPersonelAdi" runat="server"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td style="width: 20%; padding-bottom: 20px"></td>
                            <td class="btn">
                                <div class="dropdown">
                                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">
                                        İzin Durumu
                                     <span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu">
                                     
                                        <li><a href="KullaniciIzinDurumu.aspx?personelID=<%#Eval("personelID") %>">İzin Durumunu Gör</a></li>
                                        

                                    </ul>
                                </div>
                            </td>
                        </tr>
                          <tr>  <td style="width: 20%; padding-bottom: 20px">İzin Başlangıç:</td>
                            <td>
                                <div class="form-group">
                                    <div class='input-group date' id='datetimepicker1' style="width: 150px" runat="server">
                                        <asp:TextBox ID="txtBaslangicTarihi" CssClass="form-control" data-format="dd.MM.yyyy"
                                            placeholder="Başlangıç Tarihi.." runat="server" Width="140px" required></asp:TextBox>
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                    </div>
                                </div>
                            </td> </tr>
                         <tr> <td style="width: 25%; padding-bottom: 20px">İzin Bitiş:</td>
                            <td>
                                <div class="form-group">
                                    <div class='input-group date' id='datetimepicker2' style="width: 150px" runat="server">

                                        <asp:TextBox ID="txtBitisTarihi" CssClass="form-control" data-format="dd.MM.yyyy" placeholder="Bitiş Tarihi.."
                                             runat="server" Width="140px" required></asp:TextBox>

                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                    </div>
                                </div>
                            </td> </tr>

                       

                        <tr>
                            <td style="width: 25%">İzin Türü:</td>
                            <td>

                                <asp:DropDownList ID="ddlIzinTuru" Width="178px" runat="server" AppendDataBoundItems="true" required>
                                </asp:DropDownList>

                            </td>

                        </tr>

                        <tr>
                            <td style="width: 25%">Açıklama:</td>
                            <td style="padding-top: 20px">

                                <asp:TextBox ID="txtboxAciklama" runat="server" Height="97px" TextMode="MultiLine" Width="235px"></asp:TextBox>

                            </td>

                        </tr>
                        
                       

                        <tr>
                            <td></td>
                            <td>


                                <a href="IzinListesi.aspx">
                                    <button id="btnGeri" type="button" class="btn btn-success" runat="server"><span class="glyphicon glyphicon-arrow-left"></span> Geri</button>
                                </a>
                               
                                
                                <asp:Button ID="btnCıkış" Text=" Güvenli Çıkış" CssClass="btn btn-danger cancel" runat="server" OnClick="btnCıkış_Click" Visible="false" formnovalidate />
                              
                               
                               
                                 <asp:Button ID="btnEkle" runat="server" CssClass="btn btn-primary" Text="İzin Ekle" OnClick="btnEkle_Click" />
                                


                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td class="tablo">

                                <asp:Literal ID="LiteralGuncelleme" runat="server" Visible="false"></asp:Literal>
                            </td>
                        </tr>

                            </asp:Panel>
                    </table>
                </div>
            </div>
        </div>


    </form>
</body>
</html>
