<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CriarAulasOnline.aspx.cs" Inherits="EscolaPro.Web.CriarAulasOnline" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <style>
        img {
            border-radius: 50%;
            border: 10px solid transparent;
        }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Criação de Matérias On-line</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>


            <div class="box-body">


                <div class="form-group">

                    <div class="btn-group" style="margin: 10px; width: 20%">
                        <label for="cars">Unidade:</label>
                        <br />
                        <select class="custom-select form-control">
                            <option value="1">Campinas</option>
                            <option value="2">Jundiaí</option>
                            <option value="3">São Paulo</option>
                        </select>
                    </div>

                    <div class="btn-group" style="margin: 10px; width: 20%">
                        <label for="cars"></label>
                        <br />
                        <a class="btn btn-dropbox"><span class="fa fa-search" style="margin-right: 10px"></span>Buscar</a>
                    </div>

                    <div class="btn-group pull-right" style="margin: 10px;">
                        <label for="cars"></label>
                        <br />
                        <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalCriarMeta">Adicionar Nova Matéria</a>
                    </div>

                </div>


                <br />
                <table class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>Unidade</th>
                            <th>Título do Matéria</th>
                            <th>Professor</th>
                            <th>Matéria do Professor</th>
                            <th style="width: 2%"></th>
                            <th style="width: 2%"></th>
                            <th style="width: 2%"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Campinas</td>
                            <td>Física</td>
                            <td>Fernando Sousa</td>
                            <td>Professor de Física</td>
                            <td>
                                <a data-toggle="modal" data-target="#modalCriarAula"><i data-toggle="tooltip" class="far fa-file-video" style="color: dodgerblue; margin-left: 10px; font-size: large" title="Adicionar Aulas"></i></a>
                            </td>
                            <td>
                                <a data-toggle="modal" data-target="#modalCriarMeta"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
                            </td>
                            <td>
                                <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                            </td>
                        </tr>
                        <tr>
                            <td>Campinas</td>
                            <td>Matemática</td>
                            <td>Fernando Sousa</td>
                            <td>Professor de Matemática</td>
                            <td>
                                <a data-toggle="modal" data-target="#modalCriarAula"><i data-toggle="tooltip" class="far fa-file-video" style="color: dodgerblue; margin-left: 10px; font-size: large" title="Adicionar Aulas"></i></a>
                            </td>
                            <td>
                                <a data-toggle="modal" data-target="#modalCriarMeta"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
                            </td>
                            <td>
                                <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>








    <!-- Modal de Novo Curso -->

    <div class="container">
        <div class="modal fade" id="modalCriarAula" role="dialog">
            <div class="modal-dialog modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Criar Aula</h4>
                    </div>

                    <div class="modal-body">

                        <div class="box box-info">




                            <div class="form-group">

                                <div class="btn-group" style="margin: 10px; width: 75%">
                                    <label for="cars">Título da Aula</label>
                                    <br />
                                    <input type="email" class="form-control" placeholder="Descrição">
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 75%">
                                    <label for="cars">URL do Vídeo</label>
                                    <br />
                                    <input type="email" class="form-control" placeholder="URL">
                                </div>

                                <div class="btn-group" style="margin: 10px;">
                                    <label for="cars"></label>
                                    <br />
                                    <a class="btn btn-success">Adicionar</a>
                                </div>

                            </div>
                            <br />

                            <table class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Matéria</th>
                                        <th>Título da Aula</th>
                                        <th>URL</th>
                                        <th>Assistir</th>
                                        <th style="width: 2%"></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Física</td>
                                        <td>Aula</td>
                                        <td class="text-center center-block"><a>https://youtube.com/embed/ATnxMuB6U4I</a></td>
                                        <td><a href="https://youtube.com/embed/ATnxMuB6U4I"><span class="fas fa-eye"></span></a></td>
                                        <td>
                                            <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>




                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>











    <!-- Modal de Novo Curso -->

    <div class="container">
        <div class="modal fade" id="modalCriarMeta" role="dialog">
            <div class="modal-dialog modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Adicionar Nova Matéria</h4>
                    </div>

                    <div class="modal-body">

                        <div class="box box-info">




                            <img src="dist/img/user1-128x128.jpg" style="margin: 20px;" />


                            <div class="form-group">



                                <div class="btn-group" style="margin: 10px; width: 45%">
                                    <label for="cars">Unidade:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option value="0">Todos</option>
                                        <option value="1">Campinas</option>
                                        <option value="2">Jundiaí</option>
                                        <option value="3">São Paulo</option>
                                    </select>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 45%">
                                    <label for="cars">Curso:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option value="1">Ensino Médio</option>
                                        <option value="2">Ensino Fudamental</option>
                                        <option value="3">Técnico em Administração</option>
                                    </select>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 45%">
                                    <label for="cars">Nome do Professor</label>
                                    <br />
                                    <input type="email" class="form-control" placeholder="Nome">
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 45%">
                                    <label for="cars">Matéria do Professor</label>
                                    <br />
                                    <input type="email" class="form-control" placeholder="Matéria">
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 75%">
                                    <label for="cars">Título do Matéria</label>
                                    <br />
                                    <input type="email" class="form-control" placeholder="Descrição">
                                </div>

                                <div class="btn-group" style="margin: 10px;">
                                    <label for="cars"></label>
                                    <br />
                                    <a class="btn btn-success">Salvar</a>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>








</asp:Content>
