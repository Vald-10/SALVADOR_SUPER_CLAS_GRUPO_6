document.addEventListener("DOMContentLoaded", function () {
    const asientosLibres = document.querySelectorAll(".asiento-libre");
    asientosLibres.forEach(asiento => {
        asiento.addEventListener("click", function () {
            const idAsiento = this.getAttribute("data-id-asiento");
            window.location.href = `/Venta/FormularioVenta?idAsiento=${idAsiento}`;
        });
    });
});