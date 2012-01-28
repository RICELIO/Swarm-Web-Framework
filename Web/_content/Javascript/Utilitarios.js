function AbrirJanela(uri, nomeJanela, altura, largura) {
    posTOP = (screen.availHeight / 2) - (altura / 2);
    posTOP -= (posTOP / 2)
    posLEFT = (screen.availWidth / 2) - (largura / 2);

    janela = window.open(uri, nomeJanela, 'top=' + posTOP + ', left=' + posLEFT + ', width=' + largura + ', height=' + altura + ', scrollbars=1, resizable=1, status=1, menubar=1');
    janela.focus();
}

function AbriJanelaPopup(uri, nomeJanela, altura, largura) {
    posTOP = (screen.availHeight / 2) - (altura / 2);
    posTOP -= (posTOP / 2)
    posLEFT = (screen.availWidth / 2) - (largura / 2);

    janela = window.open(uri, nomeJanela, 'top=' + posTOP + ', left=' + posLEFT + ', width=' + largura + ', height=' + altura + ', scrollbars=0, resizable=0, status=0, menubar=0');
    janela.focus();
}

function AbrirJanelaTelaCheia(uri, nomeJanela) {
    janela = window.open(uri, nomeJanela, 'scrollbars=1, resizable=1, status=1, menubar=1');
    janela.moveTo(0, 0);
    janela.resizeTo(screen.availWidth, screen.availHeight);
    janela.focus();
}