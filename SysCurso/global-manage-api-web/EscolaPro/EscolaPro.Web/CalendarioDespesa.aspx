<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendario.aspx.cs" Inherits="EscolaPro.Web.TesteWebForm" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>AdminLTE 2 | Calendar</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.4 -->
    <link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <script src="https://kit.fontawesome.com/e5f771b658.js" crossorigin="anonymous"></script>
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
    <!-- fullCalendar 2.2.5-->
    <link rel="stylesheet" href="../plugins/fullcalendar/fullcalendar.min.css">
    <link rel="stylesheet" href="../plugins/fullcalendar/fullcalendar.print.css" media="print">
    <!-- Theme style -->
    <link rel="stylesheet" href="../dist/css/AdminLTE.min.css">
    <!-- AdminLTE Skins. Choose a skin from the css/skins
         folder instead of downloading all of them to reduce the load. -->
    <link rel="stylesheet" href="../dist/css/skins/_all-skins.min.css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body class="skin-blue sidebar-mini">

    <div class="wrapper">

        <!-- Main content -->
        <section class="content" style="background-color: #FFFFFF">

            <div class="box-body no-padding">
                <!-- THE CALENDAR -->
                <div id="calendar"></div>
            </div>
            <!-- /.box-body -->
            <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>







    <!-- CALENDARIO -->

    <!-- jQuery 2.1.4 -->
    <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <!-- Bootstrap 3.3.4 -->
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <!-- jQuery UI 1.11.4 -->
    <script src="https://code.jquery.com/ui/1.11.4/jquery-ui.min.js"></script>
    <!-- Slimscroll -->
    <script src="plugins/slimScroll/jquery.slimscroll.min.js"></script>
    <!-- FastClick -->
    <script src="plugins/fastclick/fastclick.min.js"></script>
    <!-- fullCalendar 2.2.5 -->
    <script src="dist/js/moment.min.js"></script>

    <script src="plugins/fullcalendar/fullcalendar.min.js"></script>
    <!-- Page specific script -->
    <script>
        $(function () {

            /* initialize the external events
             -----------------------------------------------------------------*/
            function ini_events(ele) {
                ele.each(function () {

                    // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
                    // it doesn't need to have a start or end
                    var eventObject = {
                        title: $.trim($(this).text()) // use the element's text as the event title
                    };

                    // store the Event Object in the DOM element so we can get to it later
                    $(this).data('eventObject', eventObject);

                    // make the event draggable using jQuery UI
                    $(this).draggable({
                        zIndex: 1070,
                        revert: true, // will cause the event to go back to its
                        revertDuration: 0  //  original position after the drag
                    });

                });
            }
            ini_events($('#external-events div.external-event'));

            /* initialize the calendar
             -----------------------------------------------------------------*/
            //Date for the calendar events (dummy data)
            var date = new Date();
            var d = date.getDate(),
                m = date.getMonth(),
                y = date.getFullYear();
            $('#calendar').fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                buttonText: {
                    today: 'Hoje',
                    month: 'Mês',
                    week: 'Semana',
                    day: 'Dia'
                },
                //Random default events
                events: [
                    {
                        title: '2 Despesas',
                        start: new Date(y, m, 1),
                        backgroundColor: "#f56954", //red
                        borderColor: "#f56954" //red
                    }
                ],
                editable: true,
                droppable: true, // this allows things to be dropped onto the calendar !!!
                drop: function (date, allDay) { // this function is called when something is dropped

                    // retrieve the dropped element's stored Event Object
                    var originalEventObject = $(this).data('eventObject');


                    // we need to copy it, so that multiple events don't have a reference to the same object
                    var copiedEventObject = $.extend({}, originalEventObject);

                    // assign it the date that was reported
                    copiedEventObject.start = date;
                    copiedEventObject.allDay = allDay;

                    copiedEventObject.backgroundColor = $(this).css("background-color");
                    copiedEventObject.borderColor = $(this).css("border-color");


                    // render the event on the calendar
                    // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
                    $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);

                    // is the "remove after drop" checkbox checked?
                    if ($('#drop-remove').is(':checked')) {
                        // if so, remove the element from the "Draggable Events" list

                        $(this).remove();
                    }

                }
            });

            /* ADDING EVENTS */
            var currColor = "#3c8dbc"; //Red by default
            //Color chooser button
            var colorChooser = $("#color-chooser-btn");
            $("#color-chooser > li > a").click(function (e) {
                e.preventDefault();
                //Save color
                currColor = $(this).css("color");

                //Add color effect to button
                $('#add-new-event').css({ "background-color": currColor, "border-color": currColor });
            });
            $("#add-new-event").click(function (e) {
                e.preventDefault();
                //Get value and make sure it is not null
                var val = $("#new-event").val();
                if (val.length == 0) {
                    return;
                }

                //Create events
                var event = $("<div />").attr("data-toggle", "modal").attr("data-target", "#modalCriarAlerta");
                //val.$("#ricardinho").attr("data-toggle", "modal");
                //val.$("#ricardinho").attr("data-target", "#modalCriarAlerta");

                event.css({ "background-color": currColor, "border-color": currColor, "color": "#fff", "data-toggle": "modal", "data-target": "#modalCriarAlerta" }).addClass("external-event");
                event.html(val);

                //event.$("#ricardinho").attr("tagnova", "chapinha");

                $('#external-events').prepend(event);

                //Add draggable funtionality
                ini_events(event);
                //Remove event from text input
                $("#new-event").val("");
            });
        });
    </script>




    <div class="container">
        <div class="modal fade" id="modalCriarAlerta" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Criar Alerta de Notificação</h4>
                    </div>
                    <div class="modal-body">
                        <form class="form-horizontal">
                            <div class="box-body">


                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Curso:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Modalidade:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Ano:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Semestre:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>


                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Dia de Semana:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Período:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>


                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Horário:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Sala:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>


                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Assunto:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um assunto</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>


                                <div class="btn-group" style="margin-left: 150px">


                                    <div class="box-tools pull-right" data-toggle="tooltip" title="" data-original-title="Tipo da Mensagem">
                                        <div class="btn-group" data-toggle="btn-toggle">
                                            <button type="button" class="btn btn-default btn-sm active">
                                                <span class="fas fa-border-all"></span>
                                                <span style="font-size: small">Todos</span>
                                            </button>
                                            <button type="button" class="btn btn-default btn-sm active">
                                                <span class="fab fa-facebook-messenger"></span>
                                                <span style="font-size: small">Facebook</span>
                                            </button>
                                            <button type="button" class="btn btn-default btn-sm active">
                                                <span class="fab fa-whatsapp"></span>
                                                <span style="font-size: small">WhatsApp</span>
                                            </button>
                                            <button type="button" class="btn btn-default btn-sm active">
                                                <span class="fab fa-instagram"></span>
                                                <span style="font-size: small">Instagram</span>
                                            </button>
                                            <button type="button" class="btn btn-default btn-sm active">
                                                <span class="fas fa-sms"></span>
                                                <span style="font-size: small">SMS</span>
                                            </button>
                                            <button type="button" class="btn btn-default btn-sm">
                                                <i class="fas fa-envelope"></i>
                                                <span style="font-size: small">E-mail</span>
                                            </button>
                                        </div>
                                    </div>

                                </div>


                                <div class="btn-group" style="margin-left: 150px">

                                    <textarea class="form-control" placeholder="Escreva sua mensagem..." style="width: 460px; height: 102px">

                                    </textarea>

                                </div>

                                <div class="btn-group" style="margin-left: 150px">
                                    <br />
                                    <a class="btn btn-danger pull-left"><i class="fas fa-file-upload" style="margin-right: 10px;" aria-hidden="true"></i>Anexar Arquivo</a>
                                    <a class="btn btn-success pull-right" style="margin-left: 220px; width: 100px">Salvar</a>
                                </div>


                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>




</body>
</html>
