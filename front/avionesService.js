function listarAviones(){

    fetch("https://localhost:7028/GetAll")
    .then(respuesta => respuesta.json())
    .then(respuesta => {
        if(!respuesta.success){
            alert("error success")
        }

        var cuerpoTabla = document.querySelector("tbody")
        respuesta.data.forEach((element) => {
            
            var fila = document.createElement("tr")
            fila.innerHTML += "<td>"+element.modelo+"</td>"
            fila.innerHTML += "<td>"+element.cantidadPasajeros+"</td>"
            fila.innerHTML += "<td>"+element.matricula+"</td>"
            fila.innerHTML += "<td>"+element.fechaAlta+"</td>"
            fila.innerHTML += "<td>"+element.marcaAvion.name+"</td>"
            cuerpoTabla.append(fila)
        });

    }).catch(err => {
        alert("algo salio mal")
    })
}

function buscarAvion(){

    var id = document.getElementById("idAvion").value

    if(id === null){
        alert("id no casteado")
        return false;
    }

    var url = "https://localhost:7028/getById/"+id

    fetch(url)
    .then(respuesta => respuesta.json())
    .then(respuesta =>{
        if(!respuesta.success){
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Something went wrong!",
                footer: '<a href="#">Why do I have this issue?</a>'
              });
              return false
        }

        var modelo = document.getElementById("modelo")
        var cantPasajeros = document.getElementById("cantidadPasajeros")
        var matricula = document.getElementById("matricula")
        var fechaAlta = document.getElementById("fechaAlta")
        var marcaAvion = document.getElementById("marcaAvion")

        modelo.value = respuesta.data.modelo
        cantPasajeros.value = respuesta.data.cantidadPasajeros
        matricula.value = respuesta.data.matricula
        fechaAlta.value = respuesta.data.fechaAlta
        marcaAvion.value = respuesta.data.marcaAvion.name
    })
    .catch(err =>{
        alert("algo salio mal")
    })
}

function actualizarAvion() {
    var id = document.getElementById("idAvion").value;
    if (id === null || id === "") {
        alert("ID del avion no valido");
        return false;
    }

    var url = "https://localhost:7028/update/" + id.trim()

    var modelo1 = document.getElementById("modelo").value
    var cantPasajeros1 = document.getElementById("cantidadPasajeros").value
    var matricula1 = document.getElementById("matricula").value
    var fechaAlta1 = document.getElementById("fechaAlta").value
    var marcaAvion1 = document.getElementById("marcaAvion").value

    if (!modelo1 || !cantPasajeros1 || !matricula1 || !fechaAlta1 || !marcaAvion1) {
        alert("complete todos los campos");
        return false;
    }

    var data = {
        modelo: modelo1,
        cantidadPasajeros: cantPasajeros1,
        matricula: matricula1,
        fechaAlta: fechaAlta1,
        idMarca: marcaAvion1
    }

    fetch(url, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data) 
    })
    .then(response => response.json())
    .then(response => {
        if (response.success) {
            Swal.fire({
                title: "Avion actualizado exitosamente!",
                icon: "success"
              });
            
        } else {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Revisar campos, en particular 'Marca del Avión / ID' -> debe ser un numero ",
                footer: '<a href="#">Why do I have this issue?</a>'
              });
        }
    })
    .catch(error => {
        alert("algo salio mal")
    });
}

function crearAvion(){
   
    var url = "https://localhost:7028/crearAvion"

    var modelo1 = document.getElementById("modelo").value
    var cantPasajeros1 = document.getElementById("cantidadPasajeros").value
    var matricula1 = document.getElementById("matricula").value
    var fechaAlta1 = document.getElementById("fechaAlta").value
    var idMarca1 = document.getElementById("marcaAvion").value

    if (!modelo1 || !cantPasajeros1 || !matricula1 || !fechaAlta1) {
        alert("complete todos los campos")
        return false
    }

    var data = {
        modelo: modelo1,
        cantidadPasajeros: cantPasajeros1,
        matricula: matricula1,
        fechaAlta: fechaAlta1,
        idMarca: idMarca1
    };

    fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data) 
    })
    .then(response => response.json())
    .then(response => {
        if (response.success) {
            Swal.fire({
                title: "Avion creado exitosamente!",
                icon: "success"
              });
              
            
        } else{
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Something went wrong!",
                footer: '<a href="#">Why do I have this issue?</a>'
              });
        }
    })
    .catch(error => {
        alert("algo salio mal");
    });
    
}

function eliminarAvion(){

    var id = document.getElementById("idAvion").value;
    if (id === null || id === "") {
        alert("ID del avion no valido");
        return false;
    }

    var url = "https://localhost:7028/delete/" + id.trim()
    fetch(url,{
        method: "DELETE"
    })
    .then((respuesta) => respuesta.json())
    .then((respuesta) =>{
        if(respuesta.success){
            Swal.fire({
                title: "Avion eliminado exitosamente!",
                icon: "success"
              });
        }
        else{
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Something went wrong!",
                footer: '<a href="#">Why do I have this issue?</a>'
              });
        }
    })
    .catch(err =>{
        alert("algo salio mal")
    })
}