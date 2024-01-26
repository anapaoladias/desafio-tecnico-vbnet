<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="DesafioTecnicoVbNet.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="row">
            <div class="col-sm-6">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Cadastro de Associados</h5>
                        <p class="card-text">Permite a pesquisa, inclusão, edição e exclusão de associados.</p>
                        <a href="Associados/Lista" class="btn btn-primary">Acessar</a>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Cadastro de Empresas</h5>
                        <p class="card-text">Permite a pesquisa, inclusão, edição e exclusão de empresas</p>
                        <a href="Empresas/Lista" class="btn btn-primary">Acessar</a>
                    </div>
                </div>
            </div>
        </div>
    </main>

</asp:Content>
