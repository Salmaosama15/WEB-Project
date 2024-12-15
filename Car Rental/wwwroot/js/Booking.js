function calculatePrice() {
    const carSelect = document.getElementById("carSelection");
    const startDate = document.getElementById("startDate");
    const endDate = document.getElementById("endDate");


    if (carSelect.selectedIndex === -1 || !startDate.value || !endDate.value) {
        document.getElementById("totalPrice").value = "";

        return;
    }


    const carPrice = parseFloat(carSelect.options[carSelect.selectedIndex].getAttribute("data-price").replace(',', ''));


    const start = new Date(startDate.value);
    const end = new Date(endDate.value);


    const timeDiff = end - start;
    const days = timeDiff / (1000 * 3600 * 24);


    if (days < 1) {
        document.getElementById("totalPrice").value = "Invalid dates";

        return;
    }


    const totalPrice = carPrice * days;
    document.getElementById("totalPrice").value = totalPrice.toFixed(2) + " EGP"; // Change the currency symbol to EGP

}