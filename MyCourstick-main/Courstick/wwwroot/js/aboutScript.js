ymaps.ready(init);

function init() {
    let myMap = new ymaps.Map("map", {
            center: [55.795947, 49.105922],
            zoom: 10
        }, {
            searchControlProvider: 'yandex#search'
        }),
        
        myGeoObject = new ymaps.GeoObject({
            geometry: {
                type: "Point",
                coordinates: [55.792324, 49.122235]
            },
            properties: {
                hintContent: 'Это двойка!'
            }
        }, {
            preset: 'islands#blackStretchyIcon',
            draggable: false
        });
       

    myMap.geoObjects
        .add(myGeoObject);
}
