document.addEventListener('DOMContentLoaded', function () {
    const filterPrice = document.getElementById('price-range');
    const filterOrigin = document.getElementById('origin-select');
    const carItems = document.querySelectorAll('.car-item');

    function filterCars() {

        const selectedPrice = filterPrice.value;
        const selectedOrigin = filterOrigin.value;

        carItems.forEach(function (carItem) {

            const carPrice = carItem.getAttribute('data-price');
            const carOrigin = carItem.getAttribute('data-origin');



            const matchesPrice = selectedPrice === 'all' || carPrice === selectedPrice;
            const matchesOrigin = selectedOrigin === 'all' || carOrigin === selectedOrigin;


            if (matchesPrice && matchesOrigin) {
                carItem.style.display = 'block';
            }
            else {
                carItem.style.display = 'none';
            }
        }
        );
    }



    filterPrice.addEventListener('change', filterCars);
    filterOrigin.addEventListener('change', filterCars);


    filterCars();
}
);
