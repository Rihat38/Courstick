const modal = document.getElementById('modal');
const openModalSettingsButton = document.getElementById('settings-button');
const openModalButton = document.getElementById('open-modal-button')
const closeButton = document.getElementById('modal_close_button');
const body = document.querySelector('body');


function closeModal() {
    modal.style.display = "none";
    body.style.overflow = 'scroll';
}

function openModal() {
    modal.style.display = 'block';
    body.style.overflow = 'hidden';
}

openModalButton.addEventListener('click', () => {
    openModal();
})

closeButton.addEventListener('click', closeModal);

window.onclick = function (event) {
    if (event.target == modal) {
        closeModal();
    }
}

window.addEventListener('keydown', (e) => {
    keyPress(e);
})

function keyPress(e) {
    if (e.key === "Escape") {
        closeModal();
    }
}

try {
    openModalSettingsButton.addEventListener('click', () => {
        openModal();
    });
} catch (e) {
    console.log('button doesnt exist');
}
