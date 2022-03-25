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
            <div class="row">
                <div class="col-md-3">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h4 class="box-title">Funcionários</h4>
                        </div>
                        <div class="box-body">
                            <!-- the events -->
                            <div id="external-events">
                                <div class="external-event bg-green" data-toggle="modal" data-target="#modalCriarAlerta">Daiane</div>
                                <div class="external-event bg-aqua" data-toggle="modal" data-target="#modalCriarAlerta">Francisco</div>
                                <div class="external-event bg-light-blue" data-toggle="modal" data-target="#modalCriarAlerta">Manoela</div>
                                <div class="external-event bg-red" data-toggle="modal" data-target="#modalCriarAlerta">Roberto</div>
                                <%--                               <div class="external-event bg-yellow">2. Na data de vencimento</div>
                                <div class="external-event bg-aqua">3. enviar 5 dias após vencimento</div>
                                <div class="external-event bg-light-blue">4. enviar 8 dias após vencimento </div>
                                <div class="external-event bg-red">5. enviar 13 dias após vencimento</div>--%>
                            </div>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /. box -->

                </div>
                <!-- /.col -->
                <div class="col-md-9">
                    <div class="box box-info">
                        <div class="box-body no-padding">
                            <!-- THE CALENDAR -->
                            <div id="calendar"></div>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /. box -->
                </div>
                <!-- /.col -->
            </div>
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
                        title: 'Francisco',
                        start: new Date(y, m, 1),
                        backgroundColor: "#f56954", //red
                        borderColor: "#f56954" //red
                    },
                    {
                        title: 'Francisco',
                        start: new Date(y, m, 2),
                        backgroundColor: "#f56954", //red
                        borderColor: "#f56954" //red
                    },
                    {
                        title: 'Daiane',
                        start: new Date(y, m, 2),
                        backgroundColor: "#468499", //red
                        borderColor: "#f56954" //red
                    },
                    {
                        title: 'Daiane',
                        start: new Date(y, m, 6),
                        backgroundColor: "#468499", //red
                        borderColor: "#f56954" //red
                    },
                    {
                        title: 'Daiane',
                        start: new Date(y, m, 7),
                        backgroundColor: "#468499", //red
                        borderColor: "#f56954" //red
                    },
                    {
                        title: 'Daiane',
                        start: new Date(y, m, 8),
                        backgroundColor: "#468499", //red
                        borderColor: "#468499" //red
                    },
                    {
                        title: 'Daiane',
                        start: new Date(y, m, 9),
                        backgroundColor: "#468499", //red
                        borderColor: "#f56954" //red
                    },
                    {
                        title: 'Daiane',
                        start: new Date(y, m, 10),
                        backgroundColor: "#468499", //red
                        borderColor: "#f56954" //red
                    },
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
            <div class="modal-dialog modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Gerar Escala</h4>
                    </div>
                    <div class="modal-body">
                        <form class="form-horizontal">


                            <div class="box box-info">

                                <div class="box-body">


                                    <div class="form-group">
                                        <div class="btn-group" style="margin: 10px">

                                            <label style="margin-left: 30px">Período:</label>

                                            <div class="input-group">

                                                <i class="fa fa-calendar" style="margin: 10px"></i>

                                                <input id="date" type="date">

                                                <span style="margin-left: 10px; margin-right: 10px">a</span>

                                                <i class="fa fa-calendar" style="margin: 10px"></i>

                                                <input id="date" type="date">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">

                                        <div class="btn-group" style="margin: 10px">
          
                                        </div>


                                        <div class="btn-group" style="margin: 10px">
                                            <label for="cars">Horário de Entrada:</label>
                                            <br />

                                            <input type="time" id="appt" name="appt" class="form-control"
                                                min="09:00" max="18:00" required>
                                        </div>

                                        <div class="btn-group" style="margin: 10px">
                                            <label for="cars">Horário de Saída:</label>
                                            <br />

                                            <input type="time" id="appt" name="appt" class="form-control"
                                                min="09:00" max="18:00" required>
                                        </div>

                                        <div class="btn-group" style="margin: 10px">
                                            <label for="cars"></label>
                                            <br />
                                            <a class="btn btn-success" style="width:100px">Gerar</a>
                                        </div>

                                    </div>

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
