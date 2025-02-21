document.addEventListener("DOMContentLoaded", function () {
    cargarElementos();

    document.getElementById("formCalculo").addEventListener("submit", function (event) {
        event.preventDefault();
        calcularCombinacionOptima();
    });
});

async function cargarElementos() {
    const elementosTable = document.getElementById("elementosTable");

    try {
        const response = await fetch("http://localhost:5269/api/escalada/elementos"); 
        if (!response.ok) throw new Error("No se pudieron cargar los elementos.");

        const elementos = await response.json();
        elementosTable.innerHTML = "";

        elementos.forEach((item) => {
            const row = `<tr>
                <td>${item.nombre}</td>
                <td>${item.peso}</td>
                <td>${item.calorias}</td>
            </tr>`;
            elementosTable.innerHTML += row;
        });
    } catch (error) {
        console.error(error);
        elementosTable.innerHTML = "<tr><td colspan='3'>Error al cargar elementos.</td></tr>";
    }
}

async function calcularCombinacionOptima() {
    const minCalorias = document.getElementById("minCalorias").value;
    const maxPeso = document.getElementById("maxPeso").value;
    const combinacionLista = document.getElementById("combinacionOptima");
    const combinacionContainer = document.getElementById("combinacionOptimaContainer");

    combinacionLista.innerHTML = "";
    combinacionContainer.style.display = "none";

    if (!minCalorias || !maxPeso) {
        alert("Por favor, ingresa todos los valores.");
        return;
    }

    try {
        const response = await fetch("http://localhost:5269/api/escalada/calcular", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                minCalorias: parseInt(minCalorias),
                maxPeso: parseInt(maxPeso)
            })
        });

        if (!response.ok) throw new Error("Error en la solicitud");

        const resultado = await response.json();

        if (resultado.length > 0) {
            resultado.forEach((item) => {
                const li = document.createElement("li");
                li.classList.add("list-group-item");
                li.textContent = `${item.nombre} - Peso: ${item.peso}, Calorías: ${item.calorias}`;
                combinacionLista.appendChild(li);
            });
            combinacionContainer.style.display = "block";
        } else {
            combinacionLista.innerHTML = "<li class='list-group-item'>No se encontraron combinaciones óptimas.</li>";
            combinacionContainer.style.display = "block";
        }
    } catch (error) {
        console.error(error);
        alert("Error al calcular la combinación óptima.");
    }
}

window.calcularCombinacionOptima = calcularCombinacionOptima;
