const modalSettings = document.getElementById('modal');
const openModalSettingsButton = document.getElementById('settings-button');
const closeButton = document.getElementById('modal_close_button');
const body = document.querySelector('body');




function closeModal() {
    modalSettings.style.display = "none";
    body.style.overflow = 'scroll';
}

function openModal() {
    modalSettings.style.display = 'block';
    body.style.overflow = 'hidden';
}

closeButton.addEventListener('click', closeModal);

openModalSettingsButton.addEventListener('click', () => {
    openModal();
});

window.onclick = function (event) {
    if (event.target == modalSettings) {
        closeModal();
    }
}

window.addEventListener('keydown', (e) => {
    keyPress(e);
})

function keyPress (e) {
    if(e.key === "Escape") {
        closeModal();
    }
}

function swapSettings() {
    
}