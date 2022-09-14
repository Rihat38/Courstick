const changeEmail = document.getElementById('change-email')
const changePassword = document.getElementById('change-password')
const changeLogin = document.getElementById('change-login')
const changeEmailInput = document.getElementById('change-email-input');
const changePasswordInput = document.getElementById('change-password-input');
const changeLoginInput = document.getElementById('change-login-input');
const errorEmail = document.getElementById('errorEmail');
const errorPassword = document.getElementById('errorPassword')
const fileInput = document.getElementById('fileInput');
const avatar = document.getElementById('avatar');
const submitButton = document.getElementById('submitButton');


const validRegexEmail = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
const validRegexPassword = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;

let emailFlag = false;
let passwordFlag = false;
let loginFlag = false;

function showInput(input, item, error, flag) {
    console.log(input.style.display);
    if(input.style.display == 'none') {
        input.style.display = 'block';
        item.target.value = 'Скрыть';
        flag = true;
    } else {
        input.style.display = 'none';
        item.target.value = 'Изменить';
        hideErrors(input, error);
    }
}

function hideErrors(input, item, flag) {
    item.style.display = 'none';
    input.style.border = '1px solid rgba(0, 11, 38, 0.16)';
    input.value = '';
    flag = false;
}


changePassword.addEventListener('click', (item) => {
    item.preventDefault();
    console.log('clicked');
    showInput(changePasswordInput, item, errorPassword, passwordFlag, passwordFlag);
});

changeLogin.addEventListener('click', (item) => {
    item.preventDefault();
    console.log('clicked');
});

changeEmail.addEventListener('click', (item) => {
    item.preventDefault();
    console.log('clicked');
    showInput(changeEmailInput, item, errorEmail, emailFlag);
});


changePasswordInput.addEventListener('change', (e) => {
     if(e.target.value.match(validRegexPassword)) {
         changePasswordInput.style.border = '1px solid rgba(0, 11, 38, 0.16)'
         errorPassword.style.display = 'none';
         passwordFlag = false;
     } else {
         changePasswordInput.style.border = '1px solid red'
         errorPassword.style.display = 'flex';
         passwordFlag = true;
     }
});

changeEmailInput.addEventListener('change', (e) => {
    if(e.target.value.match(validRegexEmail)) {
        changeEmailInput.style.border = '1px solid rgba(0, 11, 38, 0.16)';
        errorEmail.style.display = 'none';
        emailFlag = false;
    } else {
        changeEmailInput.style.border = '1px solid red';
        errorEmail.style.display = 'flex';
        emailFlag = true;
    }
})
let fdata = new FormData();
let image;

fileInput.addEventListener('change', (item) => {
    item.preventDefault();
    let reader = new FileReader();
    reader.onload = function (e) {
        $('#avatar').attr('src', e.target.result);
    }
    let formData = new FormData();
    formData.append('file', fileInput.files[0]); // myFile is the input type="file" control
    reader.readAsDataURL(fileInput.files[0]);
    fdata.append("Image", fileInput.files[0]);
    // for (i = 0; i < fileInput.files.length; i++) {
    //     //Appending each file to FormData object
    //     fdata.append(fileInput.files[i].name, fileInput.files[i]);
    // }
    // for (var p of fdata) {
    //     console.log(p);
    // }
    
    image = fileInput.files[0];
})

// submitButton.addEventListener('click', (item) => {
//     item.preventDefault();
//     if(changeEmailInput.value === '' && changePasswordInput.value === '' &&
//         changeLoginInput.value === '' && !fileInput.files[0]) {
//         alert('Заполните одно из полей')
//     } else {
//         if(!passwordFlag && !emailFlag) {
//             // fdata.append('Login', changeLoginInput.value);
//             // fdata.append('Password', changePasswordInput.value);
//             // fdata.append('Email', changeEmailInput.value);
//             const data = JSON.stringify({
//                 Login: changeLoginInput.value,
//                 Password: changePasswordInput.value,
//                 Email: changeEmailInput.value,
//                 Image: fileInput.files[0]
//             });
//             console.log(data);
//            
//             $.ajax({
//                 url: '/Profile/ChangeInfo',
//                 type: 'POST',
//                 contentType: 'application/json',
//                 data: data,
//                 success: () => {
//                     alert('ok');
//                 },
//                 error: (e) => {
//                     console.log(e);
//                     alert(e.responseText);
//                 }
//             })
//         }
//     }
// })

