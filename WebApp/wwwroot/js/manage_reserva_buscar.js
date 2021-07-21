$(document).ready(function () {
    createListenerGetReservas();
    createListenerBtnConfirmSearch();
});

function createListenerGetReservas() {
    var tbodyTable = $('#tbodyReservas');
    $.ajax({
        url: '/Reserva/Reservas',
        type: 'get',
        dataType: 'json',
        success: function (response) {
            tbodyTable.empty();
            console.log(response);
            response.forEach(element => {
                tbodyTable.append($('<tr id= "' + element['id'] + '">')
                    .append($('<td scope="row">"').append(element['id']))
                    .append($('<td id="specialityType' + element['nombreEstadia'] + '" class="nombre">').append(element['nombreEstadia']))
                    .append($('<td id="specialityType' + element['provincia'] + '" class="nombre">').append(element['provincia']))
                    .append($('<td id="healthCenter' + element['nombreCliente'] + '" class="provincia">').append(element['nombreCliente']))
                    .append($('<td id="description' + element['cedulaCliente'] + '" class="direccion">').append(element['cedulaCliente']))
                    .append($('<td id="precioNoche' + element['telefonoContacto'] + '" class="precioNoche">').append(element["telefonoContacto"]))
                    .append($('<td id="capacidad' + element['cantidadPersonas'] + '" class="capacidad">').append(element["cantidadPersonas"]))
                    .append($('<td id="tipoCategoria' + element['fechaEntrada'] + '" class="tipoCategoria">').append(element["fechaEntrada"]))
                    .append($('<td id="descripcion' + element['fechaSalida'] + '" class="descripcion">').append(element["fechaSalida"]))
                )
            });

        }
    });
}

function createListenerBtnConfirmSearch() {
    $("#btn_confirm").click(function () {
        var tbodyTable = $('#tbodyReservas');
        $.ajax({
            url: '/Reserva/ReservasFiltrada',
            type: 'get',
            dataType: 'json',
            data: {
                "provincia": $('#inProvinciaSa').val(),
                "fechaEntrada": $('#inFechaEntrada').val(),
                "fechaSalida": $('#inFechaSalida').val()
            },
            success: function (response) {
                if (response != null) {
                    tbodyTable.empty();
                    response.forEach(element => {
                        tbodyTable.append($('<tr id= "' + element['id'] + '">')
                            .append($('<td scope="row">"').append(element['id']))
                            .append($('<td id="specialityType' + element['nombreEstadia'] + '" class="nombre">').append(element['nombreEstadia']))
                            .append($('<td id="specialityType' + element['provincia'] + '" class="nombre">').append(element['provincia']))
                            .append($('<td id="healthCenter' + element['nombreCliente'] + '" class="provincia">').append(element['nombreCliente']))
                            .append($('<td id="description' + element['cedulaCliente'] + '" class="direccion">').append(element['cedulaCliente']))
                            .append($('<td id="precioNoche' + element['telefonoContacto'] + '" class="precioNoche">').append(element["telefonoContacto"]))
                            .append($('<td id="capacidad' + element['cantidadPersonas'] + '" class="capacidad">').append(element["cantidadPersonas"]))
                            .append($('<td id="tipoCategoria' + element['fechaEntrada'] + '" class="tipoCategoria">').append(element["fechaEntrada"]))
                            .append($('<td id="descripcion' + element['fechaSalida'] + '" class="descripcion">').append(element["fechaSalida"]))
                            )
                    });
                } else {
                    alert("Por favor llenar los espacios en blanco");
                }
            }
        });
    });
}