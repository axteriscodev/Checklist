
/**
 * Metodo usato per creare il PDF della scheda strumento
 * @param {any} filename il nome del file
 * @returns il file pdf come download dal browser
 */
export async function generaPDFDocumento2(filename) {

    const content = document.getElementById('stampaPDF');
    const opt = {
        margin: 10,
        filename: filename,
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { scale: 2 },
        jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' }
    };
    return new Promise((resolve, reject) => {
        html2pdf().from(content).set(opt).save();
        resolve();
    });

}

/**
 * Metodo usato per creare il PDF della scheda strumento
 * @param {any} filename il nome del file
 * @returns il file pdf come download dal browser
 */
export async function generaPDFDocumento(filename) {
    console.log("Dentro genera PDF")
    const doc = new jspdf.jsPDF({
        orientation: 'p',
        unit: 'pt',
        format: 'a4',
        width: 570, //target width in the PDF document
        windowWidth: 900 //window width in CSS pixels
    });
    var htmlOrElement1 = document.getElementById("stampaPDF1");
    var htmlOrElement2 = document.getElementById("stampaPDF2");
    var htmlOrElement3 = document.getElementById("stampaPDF3");
    var htmlOrElement4 = document.getElementById("stampaPDF4");
    var htmlOrElement5 = document.getElementById("stampaPDF5");

    let pageHeight = doc.internal.pageSize.getHeight();

    await doc.html(htmlOrElement1, {
        callback: function (doc) {
            return doc;
        },
        x: 10,
        y: 10,
        width: 570,
        windowWidth: 900,
        autoPaging: 'text'
    });
    await doc.html(htmlOrElement2, {
        callback: function (doc) {
            return doc;
        },
        x: 10,
        y: 10 + pageHeight ,
        width: 570,
        windowWidth: 900,
        autoPaging: 'text'
    });
    await doc.html(htmlOrElement3, {
        callback: function (doc) {
            return doc;
        },
        x: 10,
        y: 10 + pageHeight * 2,
        width: 570,
        windowWidth: 900,
        autoPaging: 'text'
    });
    await doc.html(htmlOrElement4, {
        callback: function (doc) {
            return doc;
        },
        x: 10,
        y: 10 + pageHeight * 3,
        width: 570,
        windowWidth: 900,
        autoPaging: 'text'
    });
    //await doc.html(htmlOrElement5, {
    //    callback: function (doc) {
    //        return doc;
    //    },
    //    x: 10,
    //    y: 10,
    //    width: 570,
    //    windowWidth: 900,
    //    autoPaging: 'text'
    //});

    return new Promise((resolve, reject) => {
        doc.save(filename);
        resolve();
        });

 /*   return new Promise((resolve, reject) => {

        doc.html(htmlOrElement, {
            callback: function(doc) {
                doc.save(filename);
                resolve();
            },

        });
    });*/
}



export function CreateDocumentPages() {
    var total = document.getElementById("stampaPDF");
    var totalHeight = total.offsetHeight;
    console.log("tot: " + totalHeight);
    var pageHeight = 840;
    var pages = Math.floor(totalHeight / pageHeight);
    console.log("pages " + pages);
}