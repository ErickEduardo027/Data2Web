using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Presentation.Assets.JS
{
    // Highlight de la página actual (extra por seguridad si falla helper)
    document.addEventListener("DOMContentLoaded", () => {
        // Highlight de navbar
        const links = document.querySelectorAll(".nav-link");
        const currentPage = window.location.pathname.split("/").pop();
        links.forEach(link => {
            if (link.getAttribute("href") === currentPage) {
                link.classList.add("active");
            }
        });

        // Validación formulario contacto
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

}
