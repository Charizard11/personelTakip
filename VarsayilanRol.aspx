<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VarsayilanRol.aspx.cs" Inherits="PTS2.VarsayilanRol" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="img/users.ico" rel="shortcut_icon" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/default.min.css" rel="stylesheet" />
    <link href="css/alertify.min.css" rel="stylesheet" />
    <script src="js/jquery-2.1.1.min.js"></script>
    <script src="js/alertify.min.js"></script>
    <style type="text/css">
         

        .container {
            margin-bottom: 20px;
            margin-top: 20px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">


            <ul class="nav nav-tabs" role="tablist">
                <li role="presentation" class="active"><a href="#">Varsayılan Rolü Güncelle</a></li>
            </ul>
  </div>

            <div class="area">
                <table style="position: relative; margin: auto; width: 450px">
                    <asp:Panel ID="Pnl" runat="server">
                        <tr>
                            <td style="width: 20%"><b>Varsayılan Rol :</b></td>
                            <td style="width: 30%">
                                
                                <asp:DropDownList ID="ddlVarsayilanRolGuncelle" runat="server" CssClass="form-control" Width="235px" AutoPostBack="True" required>
                            </asp:DropDownList>
                            </td>
                            
                        </tr>

                        <tr>
                            <td style="width: 20%"><b>Üst Rol :</b></td>
                            <td style="width: 30%">
                               
                                <asp:DropDownList ID="ddlUstRolGuncelle" runat="server" CssClass="form-control" Width="235px" AutoPostBack="True" required>
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
                                <a href="RolEkle.aspx">
                                    <button type="button" class="btn btn-success"><span class="glyphicon glyphicon-arrow-left"></span> Geri</button>
                                </a>

                               
                                    <asp:Button ID="btnVarsayilanRolGuncelle" CssClass="btn btn-primary" Text="Güncelle" runat="server" onclick="btnVarsayilanRolGuncelle_Click" />
                                
                            </td>
                        </tr>

                    </asp:Panel>
                </table>
            </div>
    
  
    </form>
</body>
</html>
