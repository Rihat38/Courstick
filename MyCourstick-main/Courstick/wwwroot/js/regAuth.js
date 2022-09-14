const email = document.getElementById('email');
const password = document.getElementById('password');
const registrationBtn = document.getElementById('regbutton');
const repPassword = document.getElementById('repeat_password');
const validRegexEmail = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
const validRegexPassword = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;
const errorEmail = document.getElementById('email-error');
const errorPassword = document.getElementById('pass-error')
const errorRepPassword = document.getElementById('reppassword-error')
const login = document.getElementById('login');
const loginErrorRow = document.getElementById('login-error');
const result = document.getElementById('result_row');
let errorEmailFlag = true;
let errorPassFlag = true;
let errorRepeatFlag = true;
let errorLogin = true;


email.addEventListener('change', (e) => {
    result.style.display = 'none';
    if(e.target.value.match(validRegexEmail)) {
        errorEmailFlag = false;
        email.style.border = '1px solid rgba(0, 11, 38, 0.16)';
        errorEmail.style.display = 'none';
    } else {
        errorEmailFlag = true;
        email.style.border = '1px solid red';
        errorEmail.style.display = 'flex';
    }
})

password.addEventListener('change', (e) => {
    result.style.display = 'none';
    if(e.target.value.match(validRegexPassword)) {
        errorPassFlag = false;
        password.style.border = '1px solid rgba(0, 11, 38, 0.16)'
        errorPassword.style.display = 'none';
    } else {
        errorPassFlag = true;
        password.style.border = '1px solid red'
        errorPassword.style.display = 'flex';
    }

    if(e.target.value === repPassword.value) {
        repPassword.style.border = '1px solid rgba(0, 11, 38, 0.16)'
        errorRepPassword.style.display = 'none';
        errorRepeatFlag = false;
    }
})


repPassword.addEventListener('change', (e) => {
    result.style.display = 'none';
    if(e.target.value !== password.value) {
        errorRepPassword.style.display = 'flex';
        repPassword.style.border = '1px solid red';
        errorRepeatFlag = true;
    } else {
        repPassword.style.border = '1px solid rgba(0, 11, 38, 0.16)'
        errorRepPassword.style.display = 'none';
        errorRepeatFlag = false;
    }
})
    
login.addEventListener('change', (e) => {
    result.style.display = 'none';
    if(e.target.value.length > 3) {
        errorLogin = false;
        login.style.border = '1px solid rgba(0, 11, 38, 0.16)';
        loginErrorRow.style.display = 'none';
    } else {
        errorLogin = true;
        login.style.border = '1px solid red';
        loginErrorRow.style.display = 'flex';
    }
})


function setErrors(err) {
    login.style.border = '1px solid red';
    password.style.border = '1px solid red'
    errorPassword.innerText = err.responseJSON.message;
    errorPassword.style.display = 'flex';
}

function removeErrors() {
    login.style.border = '1px solid rgba(0, 11, 38, 0.16)';
    password.style.border = '1px solid rgba(0, 11, 38, 0.16)'
    errorPassword.style.display = 'none';
}

const successReg = (res) => {
    console.log(res);
    result.style.color = '#4BB543';
    result.style.display = 'flex';
    result.innerText = res;
    
}

const failedReg = (err) => {
    console.log(err);
    result.innerText = "Данный пользователь уже существует";
    result.style.color = 'red';
    result.style.display = 'flex';
}

registrationBtn.addEventListener('click', (e) => {
    e.preventDefault();
    if(!errorEmailFlag && !errorRepeatFlag && !errorPassFlag && !errorLogin) {
        const data = {
            Email: escapeHtmlEntities(email.value),
            Password: escapeHtmlEntities(password.value),
            Login: escapeHtmlEntities(login.value)
        }
        $.ajax({
            url: '/Auth/Registration',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: (res) => {
                successReg(res);
            },
            error: (er) => {
                failedReg(er);
            }
        })
    } else {
        alert('Заполните все поля');
    }
})

function escapeHtmlEntities (str) {
    return str
        .replace(/&/g, '&amp;')
        .replace(/>/g, '&gt;')
        .replace(/</g, '&lt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&apos;');
}



    // $.ajax({
    //     url: '/Auth/Authorization',
    //     type: 'POST',
    //     contentType: 'application/json',
    //     data: loginSubmit(),
    //     success: (res) => {
    //         removeErrors();
    //         alert(res);
    //     },
    //     error:  (err) => setErrors(err)
    // })

