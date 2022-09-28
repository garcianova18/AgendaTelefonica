
//----------filtrar Electromecanica----------------------------//

const electromecanica = document.getElementById("electromecanica");

function filtrarElectromecanica() {

    const url = "/Electromecanica/Electromecanica/?electromecanico=" + electromecanica.value;


    listarElectromecanica(url);


}

electromecanica.addEventListener("keyup", filtrarElectromecanica);



//------------------ Agregar  Electromecanica ---------------//

const agregarElectromecanica = document.getElementById("form_AgregarElectromecanica");

function CrearElectromecanica(e) {

    e.preventDefault();



    const nombre = document.getElementById("elect_nombreAgregar");
    const telefono = document.getElementById("elect_telefonoAgregar");
    const correo = document.getElementById("elect_correoAgregar");
    const extension = document.getElementById("elect_extensionAgregar");
    const subsistema = document.getElementById("elect_subsistemaAgregar");


    const errorNombre = document.getElementById("elect_error_nombre");
    const errorTelefono = document.getElementById("elect_error_telefono");
    const errorSubsistema = document.getElementById("elect_error_subsistema");



    nombre.value == "" ? errorNombre.classList.add("campo_incorrecto") : errorNombre.classList.remove("campo_incorrecto");
    telefono.value == "" ? errorTelefono.classList.add("campo_incorrecto") : errorTelefono.classList.remove("campo_incorrecto");
    subsistema.value == "" ? errorSubsistema.classList.add("campo_incorrecto") : errorSubsistema.classList.remove("campo_incorrecto")




    const divpadre = document.getElementById("elect_divpadre_campos_obligatorios");

    //convertimos en array la collecion de Input que nos trae la clase elect_campos_obligatorios
    const camposObligatorios = Array.prototype.slice.apply(document.querySelectorAll(".elect_campos_obligatorios"));


    //convertimos en array la collecion de Span  que nos trae la clase iconoError_campos_obligatorios
    const iconoError_camposObligatiros = Array.prototype.slice.apply(document.querySelectorAll(".iconoError_campos_obligatorios"));


    //fufncion para agregamos o quitamos la clase error si el campo esta lleno o vacio
    function agregarQuitarelectromecanica(e) {


        // aqui decimos que cuando un campo obligatorio dispara el evento ejecute algo
        if (e.target.classList.contains("elect_campos_obligatorios")) {


            //obtenemos el index del campo que disparo el evento para asi saber a cual se le mostrara el error y acual se le quitara
            const Index = camposObligatorios.indexOf(e.target);


            //aqui decimos si el campo que disparo el evento esta vacio agregale la clase error si esta lleno quitase

            e.target.value.trim() != "" ? iconoError_camposObligatiros[Index].classList.remove("campo_incorrecto") : iconoError_camposObligatiros[Index].classList.add("campo_incorrecto")


        }

    }


    divpadre.addEventListener("keyup", agregarQuitarelectromecanica)




    //alerta que indica que los marcados con el icono son obligatorios
    const alert_camposObligatorios = document.getElementById("electAlert_camposObligatorios");



    if (nombre.value == "" || telefono.value == "" || subsistema.value == "") {



        alert_camposObligatorios.classList.remove("alert-campos-obligatorios");

    }
    else {

        alert_camposObligatorios.classList.add("alert-campos-obligatorios");

        const url = "Electromecanica/CrearActualizar/electromecanica";

        fetch(url, {

            method: "POST",

            headers: {

                'Content-Type': 'application/json'

            },

            body: JSON.stringify({



                nombre: nombre.value,
                telefono: telefono.value,
                correo: correo.value,
                extension: extension.value,
                subsistema: subsistema.value


            })



        })
            .then(res => res.json())
            .then(data => {

                if (data == 1) {
                    // para que el modal se cierre automaticamente al agregar
                    const elect_btncerrar = document.getElementById("elect_btncerrar");
                    elect_btncerrar.click();



                    //alerta cuando se agregar un nuevo mantenedor
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Se ha agregado correctamente',
                        showConfirmButton: false,
                        timer: 1500
                    })

                    //para Limpiar los campos al agregarun nuevo registro
                    document.getElementById("elect_nombreAgregar").value = "";
                    document.getElementById("elect_telefonoAgregar").value = "";
                    document.getElementById("elect_correoAgregar").value = "";
                    document.getElementById("elect_extensionAgregar").value = "";
                    document.getElementById("elect_subsistemaAgregar").value = "";


                    ////para que refresque la tabla y nuestre lo nuevo que se agrego
                    //const url = "Electromecanica/Electromecanica";
                    //listarElectromecanica(url);



                }

                else {

                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: 'Ocurrio algun error intentelo de nuevo',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })
            .catch(e => e)


    }





}

agregarElectromecanica.addEventListener("submit", CrearElectromecanica)



//------------------ buscar Electromecanico a actualizar ---------------//

function buscarActualizar_elect(id) {

    const urlActualizar = "Electromecanica/BuscarElectromecanico/?id=" + id;

    fetch(urlActualizar)
        .then(res => res.json())
        .then(data => {



            const idelectomecanica = document.getElementById("elect_id");
            const nombre = document.getElementById("elect_nombreActualizar");
            const telefono = document.getElementById("elect_telefonoActualizar");
            const correo = document.getElementById("elect_correoActualizar");
            const extension = document.getElementById("elect_extensionActualizar");
            const subsistema = document.getElementById("elect_subsistemaActualizar");


            idelectomecanica.value = data.id;
            nombre.value = data.nombre;
            telefono.value = data.telefono;
            correo.value = data.correo;
            extension.value = data.extension;
            subsistema.value = data.subsistema;

        })
        .catch(e => e)



}



//------------------ Actualizar Electromecanica ---------------//

const actualizarElectromecanica = document.getElementById("form_ActualizarElectromecanica");

function EditarElectromecanica(e) {

    e.preventDefault();

    const id_Electomecanica = Number(document.getElementById("elect_id").value);
    const nombre = document.getElementById("elect_nombreActualizar");
    const telefono = document.getElementById("elect_telefonoActualizar");
    const correo = document.getElementById("elect_correoActualizar");
    const extension = document.getElementById("elect_extensionActualizar");
    const subsistema = document.getElementById("elect_subsistemaActualizar");

    const errorNombre = document.getElementById("elect_error_nombreActualizar");
    const errorTelefono = document.getElementById("elect_error_telefonoActualizar");
    const errorSubsistema = document.getElementById("elect_error_subsistemaActualizar");




    nombre.value == "" ? errorNombre.classList.add("campo_incorrecto") : errorNombre.classList.remove("campo_incorrecto");
    telefono.value == "" ? errorTelefono.classList.add("campo_incorrecto") : errorTelefono.classList.remove("campo_incorrecto");
    subsistema.value == "" ? errorSubsistema.classList.add("campo_incorrecto") : errorSubsistema.classList.remove("campo_incorrecto")




    const divpadre = document.getElementById("elect_divpadre_campos_obligatoriosActualizar");




    //convertimos en array la collecion que nos trae la clase campos-obligatorios
    const camposObligatorios = Array.prototype.slice.apply(document.querySelectorAll(".elect_campos_obligatoriosActualizar"));

    //convertimos en array la collecion que nos trae la clase hijos
    const iconoError_CamposObligatorios = Array.prototype.slice.apply(document.querySelectorAll(".iconoError_campos_obligatoriosActualizar"));



    //aqui agregamos o quitamos la clase error si el campo esta lleno o vacio
    function agregarQuitar_electromecanicaActualizar(e) {


        // aqui decimos que cuando un campo obligatorio dispara el evento ejecute algo
        if (e.target.classList.contains("elect_campos_obligatoriosActualizar")) {




            //obtenemos el index del campo que disparo el evento para asi saber a cual se le mostrara el error y acual se le quitara
            const Index = camposObligatorios.indexOf(e.target);


            //aqui decimos si el campo que disparo el evento esta vacio agregale la clase error si esta lleno quitase

            e.target.value.trim() != "" ? iconoError_CamposObligatorios[Index].classList.remove("campo_incorrecto") : iconoError_CamposObligatorios[Index].classList.add("campo_incorrecto")


        }

    }


    divpadre.addEventListener("keyup", agregarQuitar_electromecanicaActualizar)





    const alert_camposObligatorios = document.getElementById("electAlert_camposObligatoriosActualizar");



    if (nombre.value == "" || telefono.value == "" || subsistema.value == "") {



        alert_camposObligatorios.classList.remove("alert-campos-obligatorios");

    }
    else {

        const url = "Electromecanica/CrearActualizar/electromecanica";

        fetch(url, {

            method: "POST",

            headers: {

                'Content-Type': 'application/json'

            },

            body: JSON.stringify({

                id: id_Electomecanica,
                nombre: nombre.value,
                telefono: telefono.value,
                correo: correo.value,
                extension: extension.value,
                subsistema: subsistema.value

            })



        })
            .then(res => res.json())
            .then(data => {



                if (data == 2) {
                    // para que el modal se cierre automaticamente al actualizar
                    const btncerrar = document.getElementById("elect_btncerrar_Actualizar");
                    btncerrar.click();

                    //alerta de actualizacion
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Su registro ha sido actualizado',
                        showConfirmButton: false,
                        timer: 1500
                    })


                    //para que refresque la tabla y nuestre lo nuevo que se agrego
                    const url = "Electromecanica/Electromecanica";
                    listarElectromecanica(url);





                }

                else {

                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: 'Ocurrio algun error intentelo de nuevo',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })
            .catch(error => error)

    }



}

actualizarElectromecanica.addEventListener("submit", EditarElectromecanica)



//------------------ Eliminar Electromecanica ---------------//

function Eliminar_elect(id) {


    Swal.fire({
        title: 'Estas seguro?',
        text: "No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar!'

    }).then((result) => {

        if (result.isConfirmed) {

            const urlEliminar = "Electromecanica/EliminarElectromecanica/?id=" + id;

            fetch(urlEliminar)
                .then(res => res.json())
                .then(data => {

                    if (data == 1) {

                        const url = "Electromecanica/Electromecanica";
                        listarElectromecanica(url);;


                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Su registro ha sido eliminado',
                            showConfirmButton: false,
                            timer: 1500
                        })




                    }
                    else {

                        Swal.fire({
                            position: 'center',
                            icon: 'error',
                            title: 'Ocurrio algun error y el registro no pudo ser eliminado',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }



                })
                .catch(e => e)


        }
    })



}


// funcion para que al hacer click en el boton cerrar del modal se limpien los campos y las alertas
//esta funcion pertenece al modal de crear Electromecanica
const ElectBtncerrar = document.getElementById("elect_btncerrar")
ElectBtncerrar.addEventListener("click", () => {


    document.getElementById("elect_nombreAgregar").value = "";
    document.getElementById("elect_telefonoAgregar").value = "";
    document.getElementById("elect_correoAgregar").value = "";
    document.getElementById("elect_extensionAgregar").value = "";
    document.getElementById("elect_subsistemaAgregar").value = "";



    document.getElementById("electAlert_camposObligatorios").classList.add("alert-campos-obligatorios");;


    document.getElementById("elect_error_nombre").classList.remove("campo_incorrecto");
    document.getElementById("elect_error_telefono").classList.remove("campo_incorrecto");
    document.getElementById("elect_error_subsistema").classList.remove("campo_incorrecto");



})


// funcion para que al hacer click en el boton cerrar del modal se limpien los campos y las alertas
//esta funcion pertenece al modal de Actualizar Electromecanica
const ElectBtncerrar_actualizar = document.getElementById("elect_btncerrar_Actualizar")
ElectBtncerrar_actualizar.addEventListener("click", () => {


    document.getElementById("electAlert_camposObligatoriosActualizar").classList.add("alert-campos-obligatorios");;


    document.getElementById("elect_error_nombreActualizar").classList.remove("campo_incorrecto");
    document.getElementById("elect_error_telefonoActualizar").classList.remove("campo_incorrecto");
    document.getElementById("elect_error_subsistemaActualizar").classList.remove("campo_incorrecto");

})
