const swiper = new Swiper('.swiper', {
    spaceBetween: 100,
    autoplay: {
        delay: 5000,
    },
    loop: true,
    pagination: {
        el: '.swiper-pagination',
    },

    // Navigation arrows
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },

});