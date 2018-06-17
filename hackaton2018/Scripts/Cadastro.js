

var ultimaLocalizacao = null;

function updateLocation() {
    function showPosition(position) {
        latlon = position.coords.latitude + "," + position.coords.longitude;
        var urlAdress = 'https://maps.googleapis.com/maps/api/geocode/json?latlng=' + latlon + '&key=AIzaSyAKLztlPCwKlwb23U_ay6_pxu5KQV76qxo';
        $.ajax({
            url: urlAdress,
            complete: function (res) {
                var data = JSON.parse(res.responseText);
                ultimaLocalizacao = data.results[0].address_components[3].long_name;
            }
        });       
    }
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    }
}
updateLocation();

//document.querySelector('#enviaDados').addEventListener("click",function () {
if ($("#erro").val() == 1) {
    $('#myModalErro').modal('show');
}

$('#enviaDados').click(function () {
            $('#myModal').modal({ show: true });
});
$('#finalizar').click(function () {

    $('#myModal').modal('hide');
    $.ajax({
        type: "POST",
        url: "/Cadastro/Compra",
        data: { compra: $("#compra").val(), localizacao: ultimaLocalizacao },
        error: function (jqXHR, textStatus, errorThrown) {
        },
        success: function (data) {
            alert($("#compra").val() + " Foi Cadastrado com Sucesso!!!")
            if (data != null) {
            }
        }
    });
});
$("#BuscaModal").click(function () {
    $('#myModalCPF').modal({ show: true });
});
$('#finalizarCPF').click(function () {
    $('#myModalCPF').modal('hide');
    $.ajax({
        type: "POST",
        url: "/Cadastro/Recepcao",
        data: { cpf: $("#cpf").val() },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(jqXHR)
            console.log(textStatus)
            console.log(errorThrown)
            $("#myModalErro").modal('show')
        },
        success: function (data) {
            console.log(data)
            if (data != null) {
                window.location = "/Cadastro/Compra"; 
           }
        }
    });
});


