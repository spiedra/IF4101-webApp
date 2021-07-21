var idEstancia;
var contentCells;

$(document).ready(function () {
    createListenerBtnSearch();
    createListenerBtnConfirm();
});

function createListenerBtnSearch() {
    $("#btn_search").click(function () {
        var tbodyTable = $('#tbodyCategoria')

        $.ajax({
            url: '/Estadia/Administrar_',
            type: 'post',
            data: {
                "nombreEstancia": $('#inNombreEstancia').val()
            },
            dataType: 'json',
            success: function (response) {
                if (response != null) {
                    tbodyTable.empty();
                    tbodyTable.append($('<tr id= "' + response['id'] + '">')
                        .append($('<td scope="row">"').append(response['id']))
                        .append($('<td id="specialityType' + response['nombre'] + '" class="nombre">').append(response['nombre']))
                        .append($('<td id="healthCenter' + response['provincia'] + '" class="provincia">').append(response['provincia']))
                        .append($('<td id="description' + response['direccion'] + '" class="direccion">').append(response['direccion']))
                        .append($('<td id="precioNoche' + response['precionNoche'] + '" class="precioNoche">').append(response["precionNoche"]))
                        .append($('<td id="capacidad' + response['capacidad'] + '" class="capacidad">').append(response["capacidad"]))
                        .append($('<td id="tipoCategoria' + response['tipoCategoria'] + '" class="tipoCategoria">').append(response["tipoCategoria"]))
                        .append($('<td id="descripcion' + response['descripcion'] + '" class="descripcion">').append(response["descripcion"]))
                        .append($('<td>').append(
                            $('<button class="btn btn-secondary mb-2 me-1 btn-update" data-bs-toggle="modal" data-bs-target="#updateAppointment"><i class="fas fa-cog fa-lg"></i></i></button>'
                                + '<button class= "btn btn-danger mb-2 btn-delete"> <i class="fas fa-trash-alt fa-lg"></i></button >')))
                    )
                    createListenerBtnUpdate();
                    createListenerBtnDelete();
                } else {
                    alert("La estancia ingresada no se encuentra registrada");
                }
            }
        });
    });
}

function createListenerBtnDelete() {
    $(".btn-delete").click(function () {
        var trTable = $(this).closest('tr');
        var tbodyTable = $('#tbodyCategoria')
        $.ajax({
            url: '/Estadia/Eliminar',
            type: 'post',
            data: {
                "estanciaId": trTable.attr('id')
            },
            dataType: 'json',
            success: function (response) {
                alert(response);
                tbodyTable.empty();
            }
        });
    });
}

function createListenerBtnUpdate() {
    $(".btn-update").click(function () {
        idEstancia = $(this).closest('tr').attr('id');

        contentCells = {
            "id": idEstancia,
            "nombre": $(this).closest('tr').children('td.nombre').text(),
            "provincia": $(this).closest('tr').children('td.provincia').text(),
            "direccion": $(this).closest('tr').children('td.direccion').text(),
            "precionNoche": $(this).closest('tr').children('td.precioNoche').text(),
            "capacidad": $(this).closest('tr').children('td.capacidad').text(),
            "tipoCategoria": $(this).closest('tr').children('td.descripcion').text(),
            "descripcion": $(this).closest('tr').children('td.tipoCategoria').text()
        };

        $('#inNombreEstadia').val($(this).closest('tr').children('td.nombre').text());
        $('#inProvincia').val($(this).closest('tr').children('td.provincia').text());
        $('#inDireccion').val($(this).closest('tr').children('td.direccion').text());
        $('#inPrecioNoche').val($(this).closest('tr').children('td.precioNoche').text());
        $('#inCapacidad').val($(this).closest('tr').children('td.capacidad').text());
        $('#inDescipcion').val($(this).closest('tr').children('td.descripcion').text());
        setCategorias($(this).closest('tr').children('td.tipoCategoria').text());
    });
}

function setCategorias(categoriaActual) {
    var selectCategoria = $('#selectCategoria');

    $.ajax({
        url: '/Estadia/Categorias',
        type: 'get',
        dataType: 'json',
        success: function (response) {
            console.log(response)
            selectCategoria.empty();
            response.forEach(element => {
                if (element == categoriaActual) {
                    selectCategoria.append('<option class="select_opcion" selected>' + element + '</option>');
                } else {
                    selectCategoria.append('<option class="select_opcion">' + element + '</option>');
                }
            });
        }
    });
}

function createListenerBtnConfirm() {
    $("#btn_confirmUpdate").click(function () {
        $.ajax({
            url: '/Estadia/Actualizar',
            type: 'post',
            data: {
                "id": idEstancia,
                "nombre": $('#inNombreEstadia').val(),
                "provincia": $('#inProvincia').val(),
                "direccion": $('#inDireccion').val(),
                "precionNoche": $('#inPrecioNoche').val(),
                "capacidad": $('#inCapacidad').val(),
                "tipoCategoria": $('#selectCategoria').val(),
                "descripcion": $('#inDescipcion').val()
            },
            dataType: 'json',
            success: function (response) {
                alert(response);
                var dataTable = $("#myTable");
                dataTable[0].rows[1].cells[1].innerHTML = $('#inNombreEstadia').val();
                dataTable[0].rows[1].cells[2].innerHTML = $('#inProvincia').val();
                dataTable[0].rows[1].cells[3].innerHTML = $('#inDireccion').val();
                dataTable[0].rows[1].cells[4].innerHTML = $('#inPrecioNoche').val();
                dataTable[0].rows[1].cells[5].innerHTML = $('#inCapacidad').val();
                dataTable[0].rows[1].cells[6].innerHTML = $('#selectCategoria').val();
                dataTable[0].rows[1].cells[7].innerHTML = $('#inDescipcion').val();
            }
        });
    });
}
