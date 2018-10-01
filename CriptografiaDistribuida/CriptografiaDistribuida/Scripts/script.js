


$(document).ready(function () {
   
    var validacaoChaveUsar = "";
    var chaveExecutada = "";

    //acao botao criptografar
    $("#crip").click(function () {

        var Url = "Codificacao/Criptografar/" + $("#entrada").val() + "/" + chaveExecutada;

        // enviando via ajax pelo metodo post
        $.get(Url, function (response, status) {
            if (status === "success") {
                $("#saida").val(response);
            }
        });

    });
    //acao botao desciptografar
    $("#descrip").click(function () {

        var Url = "Codificacao/Descriptografar";

        // enviando via ajax pelo metodo post
        $.post(Url, { palavra: $("#entrada").val(), chave: chaveExecutada }, function (response, status) {
            if (status === "success") {
                $("#saida").val(response);
            }
        });

    });
    //acao botao gerar chave
    $("#gera_chave").click(function () {
       

        var Url = "Chave/GeraChave";
        // enviando via ajax pelo metodo get
        $.get(Url, function (response, status) {
            if (status === "success") {
                $("#chave_gerar").val(response);
            }
        });
    });

    //acao botao amarzenar chave no banco e continuar para tela principal
    $("#ok_gera").click(function () {
        
        var Url = "Chave/AddChaveBanco";
        if ($("#chave_gerar").val() !== "") {
            // enviando via ajax pelo metodo get
            $.post(Url, { chave: $("#chave_gerar").val() }, function (response, status) {
                if (status === "success") {

                    swal({
                        title: "Chave armazenada com sucesso.",
                        text: "Caso não tenha salvo a chave, recomendasse voltar e memorizar a mesma. Caso já tenha salvo a chave, apenas click em continuar.",
                        icon: "success",
                        buttons: ["Voltar", "Continuar"],
                        dangerMode: false,
                    }).then((willDelete) => {
                            if (willDelete) {
                                $("#TelaPrincipal").removeClass("hidden");
                                $("#ModalPrincipal").addClass("hidden");
                                chaveExecutada = $("#chave_gerar").val();
                            }
                        });
                }
            });
        } else {
           
            swal({
                title: "",
                text: "Favor gerar uma chave para continuar.",
                icon: "error",
                button: "OK",
            });
       
        }
    });

    //acao continuar para tela principal se validacao chave no banco estiver correta
    $("#ok_usa").click(function () {

        
        if (validacaoChaveUsar === "true") {
            $("#TelaPrincipal").removeClass("hidden");
            $("#ModalPrincipal").addClass("hidden");
            chaveExecutada = $("#chave_usar").val();
            
        } else {
            swal({
                title: "",
                text: "Valide sua chave (já existente) para continuar.",
                icon: "error",
                button: "OK",
            });

           
        }
       
    });

   

    //acao botao buscar chave no banco 
    $("#usa_chave").click(function () {
        var Url = "Chave/GetChaveBanco";
        // enviando via ajax pelo metodo get
        
        if ($("#chave_usar").val() !== "") {
            $.post(Url, { chave: $("#chave_usar").val().trim() }, function (response, status) {
                if (status === "success") {
                    if (response === "true") {
                        validacaoChaveUsar = "true";
                        document.getElementById("chave_usar").disabled = true;
                        swal({
                            title: "",
                            text: "Chave validada com sucesso.",
                            icon: "success",
                            button: "OK",
                        });

                    }
                    else {
                        validacaoChaveUsar = "false";
                        swal({
                            title: "",
                            text: "Essa chave não existe.",
                            icon: "error",
                            button: "OK",
                        });
                    }
                }
            });
        } else {
            validacaoChaveUsar = "false";
            swal({
                title: "",
                text: "Campo chave em branco, favor preencher para continuar.",
                icon: "error",
                button: "OK",
            });
        }
    });

    //acao botao click gerar chave
    $('[name=opcao_c]').click(function () {
      
        $("#modal_gerarChave").removeClass("hidden");
        $("#continuar_gera").removeClass("hidden");
        $("#continuar_usa").addClass("hidden");
        $("#modal_usarChave").addClass("hidden");
        $("#modal_geral").addClass("hidden");
    });
    //acao botao click usar chave
    $('[name=opcao_u]').click(function () {
        $("#modal_usarChave").removeClass("hidden");
        $("#continuar_usa").removeClass("hidden");
        $("#continuar_gera").addClass("hidden");
        $("#modal_gerarChave").addClass("hidden");
        $("#modal_geral").addClass("hidden");
    });



  


});