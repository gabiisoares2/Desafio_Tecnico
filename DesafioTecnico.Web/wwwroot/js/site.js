let qtde = null;
let porcentagem = 0;
let IdDivida = null;

function btnCalcular_Onclick() {

    if (validacaoInputs()) {
        $("#ModalCalculo").load("../Home/Resultado", { 'qtde': qtde, 'porcentagem': porcentagem, 'idDivida' : IdDivida }, function () {
            $("#ModalCalculo").modal({
                show: true
            })
        })

    } else {
        alert('Por favor, verifique se todos valores foram preenchidos corretamente.');
    }
}

function validacaoInputs() {

    regex = /^[0-9]*$/;

    qtde = document.getElementById("qtdeParcelas").value;

    porcentagem = document.getElementById("porcentagem").value;

    IdDivida = $("#selectLista option:selected").val();

    if (qtde == "" || qtde == null || qtde.match(regex) == null) {
        return false;
    }
    if (porcentagem == "" || porcentagem == null || porcentagem.match(regex) == null) {
        return false;
    }
    if (IdDivida == null || IdDivida == 0) {
        return false;
    }
    return true;
}