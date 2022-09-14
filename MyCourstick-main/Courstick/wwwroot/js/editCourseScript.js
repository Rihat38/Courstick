const preview = document.getElementsByClassName('course-image')[0];
const input = document.getElementById('img')
const addCourseButton = document.getElementById('add_course');
// const createInfoButton = document.getElementById('info_button');
// const createButton = document.getElementById('add-lesson-button');
const reader = new FileReader();
const fileByteArray = [];
const query = getUrlId(document.location.pathname);

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

const lessonsList = [];

function getUrlId(qs) {
    qs = qs.split('/');
    return qs[qs.length - 1];
}

window.onload = function () {
    if (preview.src === document.location.href)
        changeBackgroundColor('gray')
    uploadLessons();
};

function uploadLessons() {
    $.ajax({
        url: '/Lessons/GetLessonsByCourseId?courseId=' + query,
        type: 'GET',
        contentType: 'application/json',
        success: (r) => {
            r.forEach((e) => {
                addingLessonLine(0, e.movie, e.text)
            })
        },
        error: () => {
            alert('произошла ошибка при получении уроков :(');
        }
    })
}

function changeBackgroundColor(color) {
    let div = document.querySelector('.course-image-container-edit');
    div.style.background = color;
}


input.addEventListener('change', (e) => {
    changeBackgroundColor('none')
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
    preview.src = URL.createObjectURL(e.target.files[0])
})

addCourseButton.addEventListener('click', (item) => createCourse(item));

function createCourse(item) {
    const data = {
        Id: query,
        Name: escapeHtmlEntities(courseName.value),
        Description: escapeHtmlEntities(bigDesc.value),
        SmallDescription: escapeHtmlEntities(smallDesc.value),
        Price: escapeHtmlEntities(price.value),
        Image: fileByteArray
    };
    if (data.Name === "" || data.Description === "" || data.SmallDescription === "" || data.Price === "")
        return false;
    item.preventDefault();
    $.ajax({
        url: '/CourseSettings/EditCourse',
        type: 'POST',
        data: JSON.stringify(data),
        contentType: 'application/json',
        datatype: "json",
        success: (e) => {
            console.log(query);
            console.log(lessonsList);
            console.log(JSON.stringify({CourseId: query, Lessons: lessonsList}));
            $.ajax({
                url: '/CourseSettings/CreateLessons',
                contentType: 'application/json',
                datatype: "json",
                type: 'POST',
                data: JSON.stringify({CourseId: query, Lessons: lessonsList}),
                success: (r) => {
                    alert('Курс обновлен')
                }
            })
        },
        error: (e) => {
            item.preventDefault();
        },
        processData: false
    });
}

function escapeHtmlEntities(str) {
    return str
        .replace(/&/g, '&amp;')
        .replace(/>/g, '&gt;')
        .replace(/</g, '&lt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&apos;');
}

createInfoButton.addEventListener('click', () => {
    addingLessonLine(0, `Урок №${lessonsList.length + 1}`, `Содержимое урока №${lessonsList.length + 1}`)
})

function addingLessonLine(type, movie, text) {
    if (lessonsList.length >= 10) {
        alert('Максимальное количество уроков - 10')
    } else {
        lessonsList.push({
            Type: 0,
            Movie: movie,
            Text: text
        })
        lessonsListHtml.insertAdjacentHTML('beforeend', `<div id=${lessonsList.length - 1} class="lesson-container">\n` +
            '                        <div class="lesson_info_container">\n' +
            `                           <p id="lesson-info-${lessonsList.length - 1}">${movie}</p>\n` +
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

function openModal(modalType) {
    if (modalType === 'create') {
        console.log('modal opened');
        createModal.style.display = 'block';
        console.log(createModal);
    } else {
        modal.style.display = 'block';
    }
}

function closeModal() {
    modal.style.display = "none";
    createModal.style.display = "none";
}

function removeItem(item) {
    item.remove();
}

let mainNumber;
const deletedIds = []

document.addEventListener('click', (item) => {
    let number = item.path[0].id.slice(-1);
    if (item.path[0].id.includes('remove-button')) {
        item.path[0].removeEventListener('click', removeItem(item.path[2]));
        lessonsList.splice(getValueToDeleteWithOffset(number), 1);
    }
    if (item.path[0].id.includes('change-button')) {
        openModal('create');
        mainNumber = number;
        lessonSubmit.addEventListener('click', changeLesson);
    }
});

function getValueToDeleteWithOffset(id){
    let result = 0;
    deletedIds.push(id)
    deletedIds.forEach((e) => {
        if (e < id)
            result++;
    })
    return id - result;
}

function changeLesson() {
    if (document.getElementById('lesson-name-input').value.length > 100 ||
        document.getElementById('lesson-name-input').value.length < 5) {
        alert('Урок не может иметь длину названия больше чем 30 символов и меньше чем 2 символа');
        return false;
    }
    if (document.getElementById('lesson-content-input').value.length > 10000 ||
        document.getElementById('lesson-content-input').value.length < 10) {
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