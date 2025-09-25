document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("contactForm");
    const nombre = document.getElementById("nombre");
    const correo = document.getElementById("correo");
    const mensaje = document.getElementById("mensaje");
    const formMessage = document.getElementById("formMessage");

    form.addEventListener("submit", function (e) {
        e.preventDefault();
        formMessage.innerHTML = "";
        formMessage.className = "mt-4 text-center font-semibold";

        let errores = [];

        if (!nombre.value.trim()) {
            errores.push("⚠️ El nombre no se puede enviar vacío");
        }
        if (!correo.value.trim()) {
            errores.push("⚠️ El correo electrónico no se puede enviar vacío");
        } else if (!validarCorreo(correo.value)) {
            errores.push("⚠️ El correo electrónico no es válido");
        }
        if (!mensaje.value.trim()) {
            errores.push("⚠️ La descripción no se puede enviar vacía");
        }

        if (errores.length > 0) {
            mostrarErrores(errores);
            return;
        }

        // ✅ Si no hay errores
        mostrarExito("✅ ¡Mensaje enviado con éxito!");
        form.reset();
    });

    function mostrarErrores(lista) {
        formMessage.innerHTML = lista.map(e => `<p class="text-red-600">${e}</p>`).join("");
    }

    function mostrarExito(texto) {
        formMessage.textContent = texto;
        formMessage.classList.add("text-green-600");
    }

    function validarCorreo(email) {
        const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return re.test(email.toLowerCase());
    }
});
