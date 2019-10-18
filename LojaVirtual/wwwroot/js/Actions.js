$(document).ready(function () {
    $(".btn-danger").click(function (e) {
        var resultado = confirm("Tem certeza que deseja realizar esta operação?");

        if (resultado == false) {
            e.preventDefault();
        }
    });

    $('.dinheiro').mask('000.000.000.000.000,00', { reverse: true }); 

    AjaxUploadImagemProduto();
});

function AjaxUploadImagemProduto() {
    $(".img-upload").click(function () {
        $(this).parent().find(".input-file").click();
    });

    $(".btn-imagem-excluir").click(function () {

        var campoHidden = $(this).parent().find("input[name=imagem]");
        var imagem = $(this).parent().find(".img-upload");
        var btnExcluir = $(this).parent().find(".btn-imagem-excluir");
        var InputFile = $(this).parent().find(".input-file");

        //Requisição AJAX
        $.ajax({
            type: "GET",
            url: "/Colaborador/Imagem/Deletar?caminho=" + campoHidden.val(),

            error: function () {
                alert("Erro no envio do arquivo!");
            },
            success: function (data) {
                imagem.attr("src", "/img/imagem-padrao.png");
                btnExcluir.addClass("btn-ocultar");
                campoHidden.val("");
                InputFile.val("");
            }
        });
    });

    $(".input-file").change(function () {

        //Formulario de dados
        var Formulario = new FormData();    
        var Binario = $(this)[0].files[0];

        Formulario.append("file", Binario);

        var campoHidden = $(this).parent().find("input[name=imagem]");
        var imagem = $(this).parent().find(".img-upload");

        var btnExcluir = $(this).parent().find(".btn-imagem-excluir");

        //Requisição AJAX
        $.ajax({
            type: "POST",
            url: "/Colaborador/Imagem/Armazenar",
            contentType: false,
            data: Formulario,
            processData: false,
            error: function () {
                alert("Erro no envio do arquivo!");
            },
            success: function (data) {

                var caminho = data.caminho;

                campoHidden.val(caminho);
                imagem.attr("src", caminho);
                btnExcluir.removeClass("btn-ocultar");
            }
        });
    });
}