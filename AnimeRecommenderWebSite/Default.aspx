<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Anime Recommender</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div class="container">
                <div class="row text-center justify-content-center section-intro">
                    <div class="col-12 col-md-10 col-lg-8">
                        <h1 class="display-3">Anime Recommender</h1>
                    </div>
                    <!--end of col-->
                </div>
                <!--end of row-->
                <div class="row justify-content-center">
                    <div class="col-12 col-md-10 col-lg-8">
                        <div class="card card-sm">
                            <div class="card-body row no-gutters align-items-center">
                                <!--end of col-->
                                <div class="col">
                                    <asp:TextBox ID="UserName" runat="server"
                                        CssClass="form-control form-control-lg form-control-borderless"
                                        placeholder="Search topics or keywords"></asp:TextBox>
                                </div>
                                <!--end of col-->
                                <div class="col-auto">
                                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click"
                                        CssClass="btn btn-lg btn-success" />
                                </div>
                                <!--end of col-->
                            </div>
                        </div>
                    </div>
                    <!--end of col-->
                </div>
                <!--end of row-->

                <div id="recommendationContainer" runat="server" visible="false" class="row justify-content-center">
                    <div class="col-12 col-md-10 col-lg-8">
                        <h3>We recommend</h3>
                        <br />
                        <asp:BulletedList ID="Recommendations" runat="server"></asp:BulletedList>
                    </div>
                </div>

                <div id="watchHistoryContainer" runat="server" visible="false" class="row justify-content-center">
                    <div class="col-12 col-md-10 col-lg-8" style="overflow-x: auto;">
                        <h3>Based on your watch history</h3>
                        <div class="container-fluid">
                            <div class="row">
                                <asp:ListView ID="WatchHistoryList" runat="server" DataKeyNames="Title" ItemType="Anime">
                                    <ItemTemplate>
                                        <div class="col-3" style=" display: inline-block; float: none;">
                                            <div class="card">
                                                <img class="card-img-top" src="<%#:Item.ImageUrl%>" alt="<%#:Item.Title%>">
                                                <div class="card-body">
                                                    <h5 class="card-title"><%#:Item.Title%></h5>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>

