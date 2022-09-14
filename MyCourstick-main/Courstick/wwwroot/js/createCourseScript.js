const createButton = document.getElementById('add-lesson-button');
const modal = document.getElementById('modal');
const createModal = document.getElementById('createLessonModal');
const createInfoButton = document.getElementById('info_button');
const lessonsListHtml = document.querySelector('.lessons');
const lessonInput = document.getElementById('lesson-name-input');
const lessonContentInput = document.getElementById('lesson-content-input');
const lessonSubmit = document.getElementById('lessonSubmit');
let nodes = Array.prototype.slice.call(lessonsListHtml.children);
const courseName = document.getElementById('courseName');
const smallDesc = document.getElementById('smallDesc');
const bigDesc = document.getElementById('bigDesc');
const image = document.getElementById('img');
const price = document.getElementById('price');
const courseImage = document.querySelector('.course-image');


const addCourseButton = document.getElementById('add_course');

const lessonsList = [];

let removeButtons;


function closeModal() {
    modal.style.display = "none";
    createModal.style.display = "none";
}


function openModal(modalType) {
    if (modalType === 'create') {
        console.log('modal opened');
        createModal.style.display = 'block';
        console.log(createModal);
    } else {
        modal.style.display = 'block';
    }
}

createInfoButton.addEventListener('click', () => {
    if (lessonsList.length >= 10) {
        alert('Максимальное количество уроков - 10')
    } else {
        lessonsList.push({
            Type: 0,
            Movie: `Урок №${lessonsList.length + 1}`,
            Text: `Содержимое урока №${lessonsList.length + 1}`
        })
        lessonsListHtml.insertAdjacentHTML('beforeend', `<div id=${lessonsList.length - 1} class="lesson-container">\n` +
            '                        <div class="lesson_info_container">\n' +
            `                           <p id="lesson-info-${lessonsList.length - 1}">Урок №${lessonsList.length}</p>\n` +
            '                        </div>\n' +
            '\n' +
            '                        <div>\n' +
            `                            <button id="change-button-${lessonsList.length - 1}" class="medium-button orange-button change-button" onclick="event.preventDefault()">\n` +
            '                                Изменить урок\n' +
            '                            </button>\n' +
            '\n' +
            `                            <button id="remove-button-${lessonsList.length - 1}" class="medium-button border-button remove-button">\n` +
            '                                Удалить урок\n' +
            '                            </button>\n' +
            '                        </div>\n' +
            '                    </div>')
    }
    removeButtons = document.querySelectorAll('.remove-button');
    closeModal();
})

function removeItem(item) {
    console.log(item);
    item.remove();
}

let mainNumber;

document.addEventListener('click', (item) => {
    let number = item.path[0].id.slice(-1);
    if (item.path[0].id.includes('remove-button')) {
        item.path[0].removeEventListener('click', removeItem(item.path[2]));
        lessonsList.splice(number, 1);
    }
    if (item.path[0].id.includes('change-button')) {
        openModal('create');
        mainNumber = number;
        lessonSubmit.addEventListener('click', changeLesson);
    }
})

function changeLesson() {
    if (document.getElementById('lesson-name-input').value.length > 30 ||
        document.getElementById('lesson-name-input').value.length < 2) {
        alert('Урок не может иметь длину названия больше чем 30 символов и меньше чем 2 символа');
        return false;
    }
    if (document.getElementById('lesson-content-input').value.length > 1000 ||
        document.getElementById('lesson-content-input').value.length < 2) {
        alert('Урок не может иметь длину содержимого больше чем 1000 символов и меньше чем 2 символа');
        return false;
    }
    const value = lessonInput.value;
    const content = lessonContentInput.value;
    const id = `lesson-info-${mainNumber}`;
    const info = document.getElementById(id);
    lessonsList[mainNumber].Movie = escapeHtmlEntities(value.toString());
    lessonsList[mainNumber].Text = escapeHtmlEntities(content.toString());

    info.innerText = lessonsList[mainNumber].Movie;
    console.log(lessonsList);
    lessonInput.value = '';
    lessonContentInput.value = '';
    createModal.style.display = "none";
    console.log(JSON.stringify(lessonsList));
}

createButton.addEventListener('click', () => {
    openModal();
    event.preventDefault();
});

window.onclick = function (event) {
    if (event.target == modal || event.target == createModal) {
        closeModal();
    }
}

const input = document.getElementById('img')
const reader = new FileReader();
const fileByteArray = [];

input.addEventListener('change', (e) => {
    changeBackgroundColor();
    courseImage.src = URL.createObjectURL(e.target.files[0])
    reader.readAsArrayBuffer(e.target.files[0]);
    reader.onloadend = (evt) => {
        if (evt.target.readyState === FileReader.DONE) {
            const arrayBuffer = evt.target.result,
                array = new Uint8Array(arrayBuffer);
            for (const a of array) {
                fileByteArray.push(a);
            }
        }
    }
})

function changeBackgroundColor(){
    let div = document.querySelector('.course-image-container');
    div.style.background = 'none';
}

function createCourse(item) {
    const data = {
        Name: escapeHtmlEntities(courseName.value),
        Description: escapeHtmlEntities(bigDesc.value),
        SmallDescription: escapeHtmlEntities(smallDesc.value),
        Price: escapeHtmlEntities(price.value),
        Type: 1,
        Image: fileByteArray
    };
    if (data.Name === "" || data.Description === "" || data.SmallDescription === "" || data.Price === "")
        return false;


    item.preventDefault();
    $.ajax({
        url: '/CourseSettings/CreateCourse',
        type: 'POST',
        data: JSON.stringify(data),
        contentType: 'application/json',
        datatype: "json",
        success: (e) => {
            let wrapper = [];
            lessonsList.forEach((item) => {
                wrapper.push(JSON.stringify(item));
            })

            $.ajax({
                url: '/CourseSettings/CreateLessons',
                contentType: 'application/json',
                datatype: "json",
                type: 'POST',
                data: JSON.stringify({CourseId: e, Lessons: lessonsList}),
                success: (r) => {
                    alert('Курс успешно добавлен')
                    window.location.href = window.location.href.replace("CreateCourse", "YourCourses");
                }
            })
        },
        error: (e) => {
            item.preventDefault();
        },
        processData: false
    });
}


addCourseButton.addEventListener('click', (item) => createCourse(item));

function escapeHtmlEntities(str) {
    return str
        .replace(/&/g, '&amp;')
        .replace(/>/g, '&gt;')
        .replace(/</g, '&lt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&apos;');
}



