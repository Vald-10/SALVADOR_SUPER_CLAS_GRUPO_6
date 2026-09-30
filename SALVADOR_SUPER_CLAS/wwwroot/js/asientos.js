document.addEventListener("DOMContentLoaded", function () {
    const croquis = document.getElementById("croquis");
    if (!croquis) return;

    const urlEstado = croquis.dataset.urlEstado;
    const urlFormulario = croquis.dataset.urlFormulario;
    const etiquetaAsiento = document.getElementById("asientoElegido");
    const mensaje = document.getElementById("mensajeSeleccion");
    const contadorLibres = document.getElementById("contadorLibres");
    const btnContinuar = document.getElementById("btnContinuar");

    let seleccionado = null;

    function actualizarPanel() {
        if (seleccionado) {
            etiquetaAsiento.textContent = "N° " + seleccionado.dataset.numero;
            mensaje.textContent = "Asiento listo para registrar al pasajero.";
            btnContinuar.classList.remove("disabled");
            btnContinuar.removeAttribute("aria-disabled");
            btnContinuar.href = urlFormulario + "?idAsiento=" + seleccionado.dataset.idAsiento;
        } else {
            etiquetaAsiento.textContent = "—";
            mensaje.textContent = "Haz clic en un asiento verde del croquis.";
            btnContinuar.classList.add("disabled");
            btnContinuar.setAttribute("aria-disabled", "true");
            btnContinuar.href = "#";
        }
    }

    croquis.addEventListener("click", function (evento) {
        const asiento = evento.target.closest(".asiento");
        if (!asiento || asiento.classList.contains("asiento-ocupado")) return;

        if (seleccionado === asiento) {
            asiento.classList.remove("asiento-seleccionado");
            asiento.classList.add("asiento-libre");
            seleccionado = null;
        } else {
            if (seleccionado) {
                seleccionado.classList.remove("asiento-seleccionado");
                seleccionado.classList.add("asiento-libre");
            }
            asiento.classList.remove("asiento-libre");
            asiento.classList.add("asiento-seleccionado");
            seleccionado = asiento;
        }
        actualizarPanel();
    });

    async function refrescarEstado() {
        try {
            const respuesta = await fetch(urlEstado, { headers: { "Accept": "application/json" } });
            if (!respuesta.ok) return;
            const estados = await respuesta.json();
            let libres = 0;

            estados.forEach(function (e) {
                const boton = croquis.querySelector('[data-id-asiento="' + e.id + '"]');
                if (!boton) return;

                if (e.vendido && !boton.classList.contains("asiento-ocupado")) {
                    if (seleccionado === boton) {
                        seleccionado = null;
                        actualizarPanel();
                        mensaje.textContent = "¡El asiento que elegiste acaba de venderse en otra ventanilla! Elige otro.";
                    }
                    boton.classList.remove("asiento-libre", "asiento-seleccionado");
                    boton.classList.add("asiento-ocupado");
                    boton.disabled = true;
                    boton.title = "Asiento " + boton.dataset.numero + " - Vendido";
                }
                if (!e.vendido) libres++;
            });

            contadorLibres.textContent = libres + " de " + estados.length;
        } catch (error) {
        }
    }

    setInterval(refrescarEstado, 5000);
});
