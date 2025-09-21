using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

document.addEventListener("DOMContentLoaded", () => {
    const items = document.querySelectorAll(".timeline-item");

    items.forEach((item, index) => {
        item.style.opacity = 0;
        item.style.transform = "translateY(20px)";

        setTimeout(() => {
            item.style.transition = "all 0.5s ease";
            item.style.opacity = 1;
            item.style.transform = "translateY(0)";
        }, index * 200);
    });
});

}
