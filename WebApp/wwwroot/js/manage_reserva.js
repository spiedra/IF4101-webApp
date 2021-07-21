var idEstanciaa;

$(document).ready(function () {
    setReservaInfo();
    createListenerBtnConfirmSearch();
    createListenerBtnConfirmReserva();
    createListenerBtnSearch();

});

function setReservaInfo() {
    var tbodyTable = $('#tbodyReserva');
    $.ajax({
        url: '/Reserva/Estadias',
        type: 'get',
        dataType: 'json',
        success: function (response) {
            tbodyTable.empty();
            response.forEach(element => {
                tbodyTable.append($('<tr id= "' + element['id'] + '">')
                    .append($('<td scope="row">"').append(element['id']))
                    .append($('<td id="specialityType' + element['nombre'] + '" class="nombre">').append(element['nombre']))
                    .append($('<td id="healthCenter' + element['provincia'] + '" class="provincia">').append(element['provincia']))
                    .append($('<td id="description' + element['direccion'] + '" class="direccion">').append(element['direccion']))
                    .append($('<td id="precioNoche' + element['precionNoche'] + '" class="precioNoche">').append(element["precionNoche"]))
                    .append($('<td id="capacidad' + element['capacidad'] + '" class="capacidad">').append(element["capacidad"]))
                    .append($('<td id="tipoCategoria' + element['tipoCategoria'] + '" class="tipoCategoria">').append(element["tipoCategoria"]))
                    .append($('<td id="descripcion' + element['descripcion'] + '" class="descripcion">').append(element["descripcion"]))
                    .append($('<td>').append(
                        $('<button id="btnReservar" class="btn btn-success mb-2 me-1 btn-reservar" data-bs-toggle="modal" data-bs-target="#modalReservar">Reservar</button>'))))
            });
        }
    });
}

function createListenerBtnSearch() {
    $("#btn_search").click(function () {
        idEstanciaa = $(this).closest('tr').attr('id');
        alert(idEstanciaa)
        //setCategorias();
    });
}

function createListenerBtnConfirmReserva() {
    $("#btn_confirmReserva").click(function () {
        alert("aja  "+ 1)
        $.ajax({
            url: '/Reserva/Registrar_',
            type: 'post',
            data: {
                "idEstancia": 1,
                "cedula": $('#inCedula').val(),
                "nombreCompleto": $('#inNombreCompleto').val(),
                "telefono": $('#inTelefonoContacto').val(),
                "cantidadPersonas": $('#inCantidadPersonas').val(),
                "fechaEntrada": $('#inFechaEntrada').val(),
                "fechaSalida": $('#inFechaSalida').val(),
            },
            dataType: 'json',
            success: function (response) {
                alert(response);
            }
        });
    });
}

function createListenerBtnConfirmSearch() {
    $("#btn_confirm").click(function () {
        var tbodyTable = $('#tbodyReserva');

        $.ajax({
            url: '/Reserva/EstadiasFiltrada',
            type: 'get',
            dataType: 'json',
            data: {
                "nombreEstadia": $('#inNombreEstadiaSa').val(),
                "provincia": $('#inProvinciaSa').val(),
                "precioRango": $('#inPrecioNocheSa').val(),
                "tipoCategoria": $('#selectCategoriaSa').val(),
                "capacidad": $('#inCapacidadSa').val()
            },
            success: function (response) {
                if (response != null) {
                    tbodyTable.empty();
                    response.forEach(element => {
                        tbodyTable.append($('<tr id= "' + element['id'] + '">')
                            .append($('<td scope="row">"').append(element['id']))
                            .append($('<td id="specialityType' + element['nombre'] + '" class="nombre">').append(element['nombre']))
                            .append($('<td id="healthCenter' + element['provincia'] + '" class="provincia">').append(element['provincia']))
                            .append($('<td id="description' + element['direccion'] + '" class="direccion">').append(element['direccion']))
                            .append($('<td id="precioNoche' + element['precionNoche'] + '" class="precioNoche">').append(element["precionNoche"]))
                            .append($('<td id="capacidad' + element['capacidad'] + '" class="capacidad">').append(element["capacidad"]))
                            .append($('<td id="tipoCategoria' + element['tipoCategoria'] + '" class="tipoCategoria">').append(element["tipoCategoria"]))
                            .append($('<td id="descripcion' + element['descripcion'] + '" class="descripcion">').append(element["descripcion"]))
                            .append($('<td>').append(
                                $('<button class="btn btn-success mb-2 me-1 btn-reservar" data-bs-toggle="modal" data-bs-target="#modalReservar">Reservar</button>'))))
                    });
                } else {
                    alert("Por favor llenar los espacios en blanco");
                }
            }
        });
    });
}

function setCategorias() {
    var selectCategoria = $('#selectCategoriaSa');

    $.ajax({
        url: '/Estadia/Categorias',
        type: 'get',
        dataType: 'json',
        success: function (response) {
            console.log(response)
            selectCategoria.empty();
            selectCategoria.append('<option class="select_opcion" selected disabled="disabled">Seleccione algún tipo</option>');
            response.forEach(element => {
                selectCategoria.append('<option class="select_opcion">' + element + '</option>');
            });
        }
    });
}