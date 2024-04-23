
/**
 *  funzione per catturare tramite js il ridimensionamento dello schermo
 */
export function resizeListener(dotnethelper) {
    $(window).resize(() => {
        console.log("resize js");
        let browserHeight = $(window).innerHeight();
        let browserWidth = $(window).innerWidth();
        // invoco il metodo di c# per settare la nuova dimensione dello schermo
        dotnethelper.invokeMethodAsync('SetScreenDimensions', browserWidth, browserHeight).then(() => {
        }).catch(error => {
            console.log("Errore durante il ridimensionamento dello schermo: " + error);
        });
    });
}

/**
 *  funzione per avere le dimensioni iniziali dello schermo
 */
export function getWindowSize() {
    console.log("get js");
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
};