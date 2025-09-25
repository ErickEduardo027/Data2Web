// scripts.js
document.addEventListener("DOMContentLoaded", () => {
    // Highlight de navbar según página actual
    const links = document.querySelectorAll(".nav-link");
    const currentPage = window.location.pathname.split("/").pop();
    links.forEach(link => {
        if (link.getAttribute("href") === currentPage) {
            link.classList.add("active"); // Tailwind extra en styles.css
        }
    });

    // Validación del formulario de contacto
    const form = document.getElementById("contactForm");
    if (form) {
        form.addEventListener("submit", (e) => {
            e.preventDefault(); // simulación

            const nombre = form.nombre.value.trim();
            const email = form.email.value.trim();
            const mensaje = form.mensaje.value.trim();

            if (!nombre || !email || !mensaje) {
                alert("⚠️ Todos los campos son obligatorios.");
                return;
            }

            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (!emailRegex.test(email)) {
                alert("⚠️ Por favor ingresa un correo válido.");
                return;
            }

            alert("✅ Tu mensaje ha sido enviado (simulación).");
            form.reset();
        });
    }
});
