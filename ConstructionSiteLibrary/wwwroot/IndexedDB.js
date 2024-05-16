

export function CheckDBSupport() {

    let exist = false;

    if (window.indexedDB) {
        exist = true;
    }
    console.log(exist);
    return exist;
}

export function Test() {
    return "ciaoooo";
}