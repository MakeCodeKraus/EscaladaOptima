// Función para calcular la combinación óptima
function calcularCombinacionOptima() {
    const minCalorias = parseInt(document.getElementById("minCalorias").value);
    const maxPeso = parseInt(document.getElementById("maxPeso").value);

    // Enviar solicitud al backend
    fetch("/Home/CalcularCombinacionOptima", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ minCalorias, maxPeso })
    })
    .then(response => response.json())
    .then(data => {
        // Verificar si se recibieron datos válidos
        if (data.length > 0) {
            // Mostrar la combinación óptima
            const lista = document.getElementById("combinacionOptima");
            lista.innerHTML = data.map(e => `
                <li class="list-group-item">${e.nombre} (Peso: ${e.peso}, Calorías: ${e.calorias})</li>
            `).join("");

            // Mostrar la sección con la combinación óptima
            document.getElementById("combinacionOptimaContainer").style.display = "block";
        } else {
            // Si no hay combinación óptima, ocultar la sección y mostrar mensaje
            document.getElementById("combinacionOptimaContainer").style.display = "none";
            alert("No se encontró una combinación óptima con los valores ingresados.");
        }
    })
    .catch(error => console.error("Error:", error));
}

// Mostrar los elementos al cargar la página
document.addEventListener("DOMContentLoaded", () => {
    const elementos = [
        { nombre: "E1", peso: 5, calorias: 3 },
        { nombre: "E2", peso: 3, calorias: 5 },
        { nombre: "E3", peso: 5, calorias: 2 },
        { nombre: "E4", peso: 1, calorias: 8 },
        { nombre: "E5", peso: 2, calorias: 3 }
    ];

    const tbody = document.getElementById("elementosTable");
    tbody.innerHTML = elementos.map(elemento => `
        <tr>
            <td>${elemento.nombre}</td>
            <td>${elemento.peso}</td>
            <td>${elemento.calorias}</td>
        </tr>
    `).join("");
});