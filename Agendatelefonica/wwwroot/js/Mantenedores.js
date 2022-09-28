//----------filtrar Mantenedores----------------------------//

const mantenedor = document.getElementById("electromecanica");

function filtrarMantenedor() {

    const urlmantenedor = "/Electromecanica/Mantenedores/?mantenedor=" + mantenedor.value;


    listarMantenedores(urlmantenedor);

}

mantenedor.addEventListener("keyup", filtrarMantenedor);




//------------------buscar mantenedor a actualizar---------------//

function buscarActualizar(id) {

    const urlActualizar = "Mantenedores/Buscarmantenedor/?id=" + id;

    fetch(urlActualizar)
        .then(res => res.json())
        .then(data => {




            const id_Mantenedor = document.getElementById("idmantenedor");
            const mantenedor = document.getElementById("mant_mantenedorActualizar");
            const nombre = document.getElementById("mant_nombreActualizar");
            const funcion = document.getElementById("mant_funcionActualizar");
            const extension = document.getElementById("mant_extensionActualizar");
            const radioTetra = document.getElementById("mant_radioTetraActualizar");
            const telefono = document.getElementById("mant_telefonoActualizar");
            const subsistema = document.getElementById("mant_subsistemaActualizar");


            id_Mantenedor.value = data.id;
            mantenedor.value = data.mantenedor;
            nombre.value = data.nombre;
            funcion.value = data.funcion;
            extension.value = data.extension;
            telefono.value = data.telefono;
            radioTetra.value = data.radioTetra;
            subsistema.value = data.subsistema;

        })




}




//------------------ Actualizar mantenedor---------------//

const actualizarMantenedor = document.getElementById("form_ActualizarMantenedor");

function EditarMantenedor(e) {

    e.preventDefault();


    const id_Mantenedor = Number(document.getElementById("idmantenedor").value);
    const mantenedor = document.getElementById("mant_mantenedorActualizar");
    const nombre = document.getElementById("mant_nombreActualizar");
    const funcion = document.getElementById("mant_funcionActualizar");
    const extension = document.getElementById("mant_extensionActualizar");
    const radioTetra = document.getElementById("mant_radioTetraActualizar");
    const telefono = document.getElementById("mant_telefonoActualizar");
    const subsistema = document.getElementById("mant_subsistemaActualizar");



    const errorMantenedor = document.getElementById("mant_error_mantenedorActualizar");
    const errorNombre = document.getElementById("mant_error_nombreActualizar");
    const errorFuncion = document.getElementById("mant_error_funcionActualizar");
    const errorTelefono = document.getElementById("mant_error_telefonoActualizar");
    const errorSubsistema = document.getElementById("mant_error_subsistemaActualizar");



    mantenedor.value == "" ? errorMantenedor.classList.add("campo_incorrecto") : errorMantenedor.classList.remove("campo_incorrecto");
    nombre.value == "" ? errorNombre.classList.add("campo_incorrecto") : errorNombre.classList.remove("campo_incorrecto");
    funcion.value == "" ? errorFuncion.classList.add("campo_incorrecto") : errorFuncion.classList.remove("campo_incorrecto");
    telefono.value == "" ? errorTelefono.classList.add("campo_incorrecto") : errorTelefono.classList.remove("campo_incorrecto");
    subsistema.value == "" ? errorSubsistema.classList.add("campo_incorrecto") : errorSubsistema.classList.remove("campo_incorrecto");





    const divpadre = document.getElementById("mant_divpadre_campos_obligatoriosaActualizar");

    //convertimos en array la collecion que nos trae la clase campos-obligatorios
    const camposObligatorios = Array.prototype.slice.apply(document.querySelectorAll(".mant_campos_obligatoriosActualizar"));

    //convertimos en array la collecion que nos trae la clase hijos
    const iconoError_camposObligatorios = Array.prototype.slice.apply(document.querySelectorAll(".mant_iconoError_campos_obligatoriosActualizar"));




    //aqui agregamos o quitamos cla clase error si el campo esta lleno o vacio
    function agregarQuitarMantenedorActualizar(e) {




        // aqui decimos que cuando un campo obligatorio dispara el evento ejecute algo
        if (e.target.classList.contains("mant_campos_obligatoriosActualizar")) {

            //obtenemos el index del campo que disparo el evento para asi saber a cual se le mostrara el error y acual se le quitara
            const Index = camposObligatorios.indexOf(e.target);


            //aqui decimos si el campo que disparo el evento esta vacio agregale la clase error si esta lleno quitase

            e.target.value.trim() != "" ? iconoError_camposObligatorios[Index].classList.remove("campo_incorrecto") : iconoError_camposObligatorios[Index].classList.add("campo_incorrecto")


        }

    }


    divpadre.addEventListener("keyup", agregarQuitarMantenedorActualizar)





    const alert_camposObligatorios = document.getElementById("mantAlert_camposObligatoriosActualizar");

    if (mantenedor.value == "" || nombre.value == "" || funcion.value == "" || telefono.value == "" || subsistema.value == "") {



        alert_camposObligatorios.classList.remove("alert-campos-obligatorios")

    }
    else {

        const url = "Mantenedores/CrearActualizar/?mantenedores";

        fetch(url, {

            method: "POST",

            headers: {

                'Content-Type': 'application/json'

            },

            body: JSON.stringify({

                id: id_Mantenedor,
                mantenedor: mantenedor.value,
                nombre: nombre.value,
                funcion: funcion.value,
                telefono: telefono.value,
                extension: extension.value,
                radioTetra: radioTetra.value,
                subsistema: subsistema.value



            })



        })
            .then(res => res.json())
            .then(data => {

                if (data == 2) {


                    // para que el modal se cierre automaticamente al actualizar
                    const btncerrar = document.getElementById("mant_btncerrar_actualizar");
                    btncerrar.click();

                    //alerta de actualizacion
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Su registro ha sido actualizado',
                        showConfirmButton: false,
                        timer: 1500
                    })






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
            }).catch(error => error)
    }






}

actualizarMantenedor.addEventListener("submit", EditarMantenedor)



//------------------ Eliminar mantenedor ---------------//

function Eliminar(id) {

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

            const urlEliminar = "Mantenedores/EliminarMantenedor/?id=" + id;

            fetch(urlEliminar)
                .then(res => res.json())
                .then(data => {

                    if (data == 1) {

                        const urlmantenedor = "Electromecanica/Mantenedores";

                        listarMantenedores(urlmantenedor);


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


        }
    })



}




//------------------ Agregar Mantenedor ---------------//

const agregarMantenedor = document.getElementById("formulario");

function CrearMantenedor(e) {

    e.preventDefault();



    const mantenedor = document.getElementById("mant_mantenedorAgregar");
    const nombre = document.getElementById("mant_nombreAgregar");
    const funcion = document.getElementById("mant_funcionAgregar");
    const telefono = document.getElementById("mant_telefonoAgregar");
    const subsistema = document.getElementById("mant_subsistemaAgregar");
    const extension = document.getElementById("mant_extensionAgregar");
    const radioTetra = document.getElementById("mant_radiotetraAgregar");

    const errorMantenedor = document.getElementById("mant_error_mantenedor");
    const errorNombre = document.getElementById("mant_error_nombre");
    const errorFuncion = document.getElementById("mant_error_funcion");
    const errorTelefono = document.getElementById("mant_error_telefono");
    const errorSubsistema = document.getElementById("mant_error_subsistema");




    mantenedor.value == "" ? errorMantenedor.classList.add("campo_incorrecto") : errorMantenedor.classList.remove("campo_incorrecto");
    nombre.value == "" ? errorNombre.classList.add("campo_incorrecto") : errorNombre.classList.remove("campo_incorrecto");
    funcion.value == "" ? errorFuncion.classList.add("campo_incorrecto") : errorFuncion.classList.remove("campo_incorrecto");
    telefono.value == "" ? errorTelefono.classList.add("campo_incorrecto") : errorTelefono.classList.remove("campo_incorrecto");
    subsistema.value == "" ? errorSubsistema.classList.add("campo_incorrecto") : errorSubsistema.classList.remove("campo_incorrecto");






    const divpadre = document.getElementById("mant_divpadre_campos_obligatorios");

    //convertimos en array la collecion que nos trae la clase campos-obligatorios
    const camposobligatorios = Array.prototype.slice.apply(document.querySelectorAll(".mant_campos_obligatorios"));

    //convertimos en array la collecion que nos trae la clase hijos
    const iconoError_camposObligatorios = Array.prototype.slice.apply(document.querySelectorAll(".mant_iconoError_campos_obligatorios"));




    //aqui agregamos o quitamos cla clase error si el campo esta lleno o vacio
    function agregarQuitarMantenedor(e) {


        // aqui decimos que cuando un campo obligatorio dispara el evento ejecute algo
        if (e.target.classList.contains("mant_campos_obligatorios")) {


            //obtenemos el index del campo que disparo el evento para asi saber a cual se le mostrara el error y acual se le quitara
            const Index = camposobligatorios.indexOf(e.target);


            //aqui decimos si el campo que disparo el evento esta vacio agregale la clase error si esta lleno quitase

            e.target.value.trim() != "" ? iconoError_camposObligatorios[Index].classList.remove("campo_incorrecto") : iconoError_camposObligatorios[Index].classList.add("campo_incorrecto")



        }

    }


    divpadre.addEventListener("keyup", agregarQuitarMantenedor)





    const alert_camposObligatorios = document.getElementById("mant_alert_camposObligatorios");

    if (mantenedor.value == "" || nombre.value == "" || funcion.value == "" || telefono.value == "" || subsistema.value == "") {



        alert_camposObligatorios.classList.remove("alert-campos-obligatorios")

    }
    else {

        alert_camposObligatorios.classList.add("alert-campos-obligatorios")

        const url = "Mantenedores/CrearActualizar/mantenedores";

        fetch(url, {

            method: "POST",

            headers: {

                'Content-Type': 'application/json'

            },

            body: JSON.stringify({


                mantenedor: mantenedor.value,
                nombre: nombre.value,
                funcion: funcion.value,
                telefono: telefono.value,
                extension: extension.value,
                radioTetra: radioTetra.value,
                subsistema: subsistema.value


            })



        })
            .then(res => res.json())
            .then(data => {

                if (data == 1) {
                    // para que el modal se cierre automaticamente al agregar
                    const btncerrar = document.getElementById("Mant_Btncerrar");
                    btncerrar.click();

                    if (btncerrar.click() == true) {


                    }

                    //alerta cuando se agregar un nuevo mantenedor
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Se ha agregado correctamente',
                        showConfirmButton: false,
                        timer: 1500
                    })

                    //para Limpiar los campos al agregarun nuevo registro

                    document.getElementById("mant_mantenedorAgregar").value = "";
                    document.getElementById("mant_nombreAgregar").value = "";
                    document.getElementById("mant_funcionAgregar").value = "";
                    document.getElementById("mant_telefonoAgregar").value = "";
                    document.getElementById("mant_subsistemaAgregar").value = "";
                    document.getElementById("mant_extensionAgregar").value = "";
                    document.getElementById("mant_radiotetraAgregar").value = "";



                    //para que refresque la tabla y nuestre lo nuevo que se agrego
                    //const urlmantenedor = "Mantenedores/Mantenedores";

                    //listarMantenedores(urlmantenedor);



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


    }





}

agregarMantenedor.addEventListener("submit", CrearMantenedor);





// funcion para que al hacer click en el boton cerrar del modal se limpien los campos y las alertas
//esta funcion pertenece al modal de crear mantenedor

const MantBtncerrar = document.getElementById("Mant_Btncerrar");
MantBtncerrar.addEventListener("click", () => {


    document.getElementById("mant_mantenedorAgregar").value = "";
    document.getElementById("mant_nombreAgregar").value = "";
    document.getElementById("mant_funcionAgregar").value = "";
    document.getElementById("mant_telefonoAgregar").value = "";
    document.getElementById("mant_subsistemaAgregar").value = "";
    document.getElementById("mant_extensionAgregar").value = "";
    document.getElementById("mant_radiotetraAgregar").value = "";


    document.getElementById("mant_alert_camposObligatorios").classList.add("alert-campos-obligatorios");;



    document.getElementById("mant_error_mantenedor").classList.remove("campo_incorrecto");
    document.getElementById("mant_error_nombre").classList.remove("campo_incorrecto");
    document.getElementById("mant_error_funcion").classList.remove("campo_incorrecto");
    document.getElementById("mant_error_telefono").classList.remove("campo_incorrecto");
    document.getElementById("mant_error_subsistema").classList.remove("campo_incorrecto");


})


// funcion para que al hacer click en el boton cerrar del modal se limpien los campos y las alertas
//esta funcion pertenece al modal de actualizar mantenedor

const MantBtncerrarActualizar = document.getElementById("mant_btncerrar_actualizar");
MantBtncerrarActualizar.addEventListener("click", () => {


    document.getElementById("mant_mantenedorActualizar").value = "";
    document.getElementById("mant_nombreActualizar").value = "";
    document.getElementById("mant_funcionActualizar").value = "";
    document.getElementById("mant_telefonoActualizar").value = "";
    document.getElementById("mant_subsistemaActualizar").value = "";
    document.getElementById("mant_extensionActualizar").value = "";
    document.getElementById("mant_radioTetraActualizar").value = "";


    document.getElementById("mantAlert_camposObligatoriosActualizar").classList.add("alert-campos-obligatorios");;



    document.getElementById("mant_error_mantenedorActualizar").classList.remove("campo_incorrecto");
    document.getElementById("mant_error_nombreActualizar").classList.remove("campo_incorrecto");
    document.getElementById("mant_error_funcionActualizar").classList.remove("campo_incorrecto");
    document.getElementById("mant_error_telefonoActualizar").classList.remove("campo_incorrecto");
    document.getElementById("mant_error_subsistemaActualizar").classList.remove("campo_incorrecto");


})