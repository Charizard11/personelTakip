<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RolDuzenle.aspx.cs" Inherits="PTS2.RolDuzenle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rol Düzenleme Sayfası</title>
    <link href="css/bootstrap.min.css" rel="stylesheet"/>
    <link href ="img/users.ico" rel="shortcut_icon"/>
     <style type="text/css">
   
            .kendiyazdigimCSS td {
                padding: 4px;
            }
            .container{
             margin-bottom:20px;
            margin-top:30px;
        }
 
    </style>
</head>
<body>
   <form id="form1" runat="server">

        <div class=" container">
          
            <div>
                <ul class="nav nav-tabs">
                    <li role="presentation" class="active"><a href="#">Rol Düzenle</a></li>
            </div>
        </div>
       
       <div class="row">
       <div class="col-md-6 col-md-offset-3">
        <table class="kendiyazdigimCSS">
            <tr>
                <td>Bölümü:</td>
                <td>
                    <asp:TextBox ID="TxtboxRolAdiDuzenle" CssClass="form-control" placeholder="Rol Adı Güncelle" runat="server" Width="235px"></asp:TextBox>

                </td>

            </tr>  
            <tr>
                <td>
                    <p>&nbsp</p>
                </td>
                <td>
                    <a href="RolEkle.aspx">
                        <button type="button" class="btn btn-success"> Geri</button>
                       
                    </a>
                    <asp:Button ID="btnRolDuzenle"  runat="server" CssClass="btn btn-primary" Text="Güncelle"  OnClick="btnRolDuzenle_Click"  />
  
                </td>
            </tr>
            
        </table>
        </div>
         </div>
    </form>
</body>
</html>