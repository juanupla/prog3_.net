var url = "https://localhost:7028/usuarios/GetAll"

function listarUsuario(){

    fetch(url)
    .then(resp => resp.json())
    .then(resp => {
        if(!resp.success){
            alert("error al consmir la api")
        }

        const cuerpoTabla = document.querySelector("tbody")
        resp.data.forEach((usuario) => {

            const fila = document.createElement('tr')
            fila.innerHTML += '<td>'+(usuario.email)+'</td>'
            fila.innerHTML += '<td>'+(usuario.nombreUsuario)+'</td>'

            cuerpoTabla.append(fila)
    })
    }).catch(err => {
        alert("algo salio mal")
    })
    
}