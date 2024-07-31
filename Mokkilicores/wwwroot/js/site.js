
function DeleteItem(btn) {

    var tabla = document.getElementById('tablaP');
    var fila = tabla.getElementsByTagName('tr');

    if (fila.length == 2) {

        alert("Al menos debe tener un producto");
        return;

    }

    var btnE = btn.id.replaceAll('btnremoveP-', '');
    var btnInput = btnE + "__EsEliminado";
    var cambioEstado = document.querySelector("[id$='" + btnInput + "']").id;

    document.getElementById(cambioEstado).value = "true";


    $(btn).closest('tr').hide();
}


function AddProductoPedido(btn) {

    var tabla = document.getElementById('tablaP');
    var fila = tabla.getElementsByTagName('tr');

    var codigoFila = fila[fila.length - 1].outerHTML;

    var ultimafila = fila.length - 2 //document.getElementById("UltimoItem").value;
    var proximafila = eval(ultimafila) + 1;

    //document.getElementById("UltimoItem").value = proximafila;

    codigoFila = codigoFila.replaceAll('_' + ultimafila + '_', '_' + proximafila + '_');
    codigoFila = codigoFila.replaceAll('[' + ultimafila + ']', '[' + proximafila + ']');
    codigoFila = codigoFila.replaceAll('-' + ultimafila, '-' + proximafila);

    var nuevaFila = tabla.insertRow();
    nuevaFila.innerHTML = codigoFila;

    // Para limpiar la nuevas lineas
    var x = document.getElementsByTagName("INPUT");

    for (var n = 0; n < x.length; n++) {
        if (x[n].type == "text" && x[n].id.indexOf('_' + proximafila + '_') > 0) {
            x[n].value = '';
        } else if (x[n].type == "number" && x[n].id.indexOf('_' + proximafila + '_') > 0) {
            x[n].value = 0;
        }
    }


}

function actualizarCostoTotal(element) {
    var row = element.closest('tr');
    var cantidad = parseFloat(row.querySelector('[name$=".Cantidad"]').value) || 0;
    var costo = parseFloat(row.querySelector('[name$=".Costo"]').value) || 0;
    var costoTotal = (cantidad * costo) + ((cantidad * costo) * 0.13);
    row.querySelector('[name$=".CostoTotal"]').value = costoTotal.toFixed(2);
}

