document.addEventListener("DOMContentLoaded", () => {
    // Hover: efecto visual en los items
    document.querySelectorAll(".timeline li div").forEach(item => {
        item.addEventListener("mouseenter", () => {
            item.classList.add("bg-blue-50");
        });
        item.addEventListener("mouseleave", () => {
            item.classList.remove("bg-blue-50");
        });
    });
});

// 👇 Esta función la llama el botón "Ver imágenes" de cada evento
function mostrarImagenes(eventoId) {
    const galeria = document.getElementById("galeria");
    galeria.innerHTML = "Cargando imágenes...";

    // 👇 Usa EventoId, no EventId
    const evento = window.timelineEventos.find(e => e.EventoId === eventoId);

    if (evento && evento.Imagenes && evento.Imagenes.length > 0) {
        galeria.innerHTML = evento.Imagenes
            .slice(0, 3) // máximo 3
            .map(url => `
                <img src="${url}" 
                     class="w-96 h-72 object-cover rounded shadow m-2 transition transform hover:scale-105" />
            `)
            .join("");
    } else {
        galeria.innerHTML = `<p class="text-gray-500">⚠️ No hay imágenes para este evento.</p>`;
    }
}
