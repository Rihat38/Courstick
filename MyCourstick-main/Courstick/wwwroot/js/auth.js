const email = document.getElementById('email');
const password = document.getElementById('password');
const login = document.getElementById('login');
const repPassword = document.getElementById('repeat_password');
const validRegexEmail = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
const validRegexPassword = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;
const errorEmail = document.getElementById('email-error');
const errorPassword = document.getElementById('pass-error')
const errorRepPassword = document.getElementById('reppassword-error')
const loginSubmitButton = document.getElementById('login-submit');
let errorEmailFlag = false;
let errorPassFlag = false;
let errorRepeatFlag = false;


// email.addEventListener('change', (e) => {
//     if(e.target.value.match(validRegexEmail)) {
//         errorEmailFlag = false;
//         email.style.border = '1px solid rgba(0, 11, 38, 0.16)';
//         errorEmail.style.display = 'none';
//     } else {
//         errorEmailFlag = true;
//         email.style.border = '1px solid red';
//         errorEmail.style.display = 'flex';
//     }
// })
//
// password.addEventListener('change', (e) => {
//     if(e.target.value.match(validRegexPassword)) {
//         errorPassFlag = false;
//         password.style.border = '1px solid rgba(0, 11, 38, 0.16)'
//         errorPassword.style.display = 'none';
//     } else {
//         errorPassFlag = true;
//         password.style.border = '1px solid red'
//         errorPassword.style.display = 'flex';
//     }
//
//     if(e.target.value === repPassword.value) {
//         repPassword.style.border = '1px solid rgba(0, 11, 38, 0.16)'
//         errorRepPassword.style.display = 'none';
//     }
// })
//
//
// repPassword.addEventListener('change', (e) => {
//     if(e.target.value !== password.value) {
//         errorRepPassword.style.display = 'flex';
//         repPassword.style.border = '1px solid red';
//     } else {
//         repPassword.style.border = '1px solid rgba(0, 11, 38, 0.16)'
//         errorRepPassword.style.display = 'none';
//     }
// })

function loginSubmit() {
    const data = JSON.stringify({
        'login': escapeHtmlEntities(login.value),
        'password': escapeHtmlEntities(password.value),
        'isRemember': false
    });
    return data;
}

function setErrors(err) {
    login.style.border = '1px solid red';
    password.style.border = '1px solid red'
    errorPassword.innerText = 'Неправильный логин или пароль';
    errorPassword.style.display = 'flex';
}

function removeErrors() {
    login.style.border = '1px solid rgba(0, 11, 38, 0.16)';
    password.style.border = '1px solid rgba(0, 11, 38, 0.16)'
    errorPassword.style.display = 'none';
    console.log(window.location.href);
}

login.addEventListener('click', () => {
    removeErrors();
})

loginSubmitButton.addEventListener('click', (e) => {
    e.preventDefault();
    $.ajax({
        url: '/Auth/Authorization',
        type: 'POST',
        contentType: 'application/json',
        data: loginSubmit(),
        success: (res) => {
            removeErrors();
            window.location.href = '/Profile/profile';
        },
        error: (err) => setErrors(err)
    })
})

function escapeHtmlEntities (str) {
    return str
        .replace(/&/g, '&amp;')
        .replace(/>/g, '&gt;')
        .replace(/</g, '&lt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&apos;');
}

