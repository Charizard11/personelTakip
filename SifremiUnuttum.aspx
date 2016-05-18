   <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SifremiUnuttum.aspx.cs" Inherits="PTS2.SifremiUnuttum" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Şifremi Unuttum Sayfası</title>
    <link href="img/users.ico" rel="shortcut_icon" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/default.min.css" rel="stylesheet" />
    <link href="css/alertify.min.css" rel="stylesheet" />
    <script src="js/jquery-2.1.1.min.js"></script>
    <script src="js/alertify.min.js"></script>
    <style type="text/css">
        .area {
            margin-top: 20px;
            position: relative;
            margin-left: auto;
            margin-right: auto;
        }

        .container {
            margin-bottom: 20px;
            margin-top: 20px;
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

        <div class="container">


            <ul class="nav nav-tabs" role="tablist">
                <li role="presentation" class="active"><a href="#">Şifremi Unuttum</a></li>
            </ul>


            <div class="area">
                <table style="position: relative; margin: auto; width: 450px">
                    <asp:Panel ID="Pnl" runat="server">
                        <tr>
                            <td style="width: 20%"><b>E-Posta :</b></td>
                            <td style="width: 30%">
                                <asp:TextBox ID="txtboxSifreGonder" CssClass="form-control" placeholder="E-Posta Giriniz.." runat="server" required></asp:TextBox>
                            </td>
                            <td style="width: 3%">
                                <span class="form-control">@</span>
                            </td>
                            <td style="width: 20%">
                                <asp:DropDownList ID="ddlEmail" runat="server" AppendDataBoundItems="true" CssClass="form-control" required>
                                    <asp:ListItem Text="-Seçiniz-" Value=""></asp:ListItem>
                                    <asp:ListItem Text="ogr.dpu.edu.tr" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="hotmail.com" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="gmail.com" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="yahoo.com" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%"></td>
                            <td>
                                <a href="Login.aspx">
                                    <button type="button" class="btn btn-danger"><span class="glyphicon glyphicon-remove-circle"></span> Çıkış</button>
                                </a>

                                <span class="icon-input-btn"><span class="glyphicon glyphicon-envelope" style="color: white"></span>
                                    <asp:Button ID="btnSifreGonder" CssClass="btn btn-success" Text="Gönder" runat="server" OnClick="btnSifreGonder_Click" />
                                </span>
                            </td>
                        </tr>

                    </asp:Panel>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
