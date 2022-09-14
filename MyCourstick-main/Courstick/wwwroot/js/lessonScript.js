const lesson_buttons = document.querySelectorAll('.lesson-list-item');
const content_container = document.getElementById('content');
const lesson_title = document.getElementById('lesson-title')
const returnButton = document.getElementById('return-button');
const commentButton = document.getElementById('addComment');
const textInput = document.getElementById('text');
const lessonContainer = document.getElementById('lesson-container-show');

const query = getUrlId(document.location.pathname);

lesson_buttons.forEach((item, index) => {
    item.addEventListener('click', item => {
        console.log(item.target.id);
        getLesson(item.target.id, index + 1);
    })
})

let urlRegex = /(https?:\/\/[^\s]+)/g;

function getLesson(id, index) {
    $.ajax({
        url: '/Lessons/GetLesson?id=' + id,
        type: 'GET',
        contentType: 'application/json',
        success: (res) => {
            lessonContainer.innerText = '';
            lessonContainer.innerText += `${res.movie}\n\n${res.text}`;
        },
        error: () => {
            console.log('error');
        }
    });
}

window.addEventListener('load', () => getComments());

const getComments = () => {
    $.ajax({
        url: '/Lessons/GetComments?id=' + query,
        type: 'GET',
        contentType: 'application/json',
        success: (res) => {
            $('.comments-container').empty();
            res.forEach(item => {
                setComment(item);
            })
        },
        error: () => {
            console.log('error');
        }
    });
}

commentButton.addEventListener('click', (e) => {
    e.preventDefault();
    
    const data = {
        Text: escapeHtmlEntities(textInput.value.trim()),
        CourseId: query
    }
    
    createComment(data)
    
})

const createComment = (data) => {
    $.ajax({
        url: '/Lessons/SetComment',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: (res) => {
            getComments();
        },
        error: (err) => {
            console.log(err);
        }
    })
}

function getUrlId(qs) {
    qs = qs.split('/');
    return qs[qs.length - 1];
}

function setComment(item) {
    const img = `data:image/png;base64,${item.user.avatar}`
    $('.comments-container').append('<div class="comment">\n' +
        `                                <img src="${img}"class="comment_avatar">\n` +
        '                                <div class="comment-content">\n' +
        '                                    <p class="name">\n' +
        `                                        ${item.user.normalizedUserName}\n` +
        '                                    </p>\n' +
        '        \n' +
        `                                    <p class="comment-text"> ${item.text}` +
        '                                    </p>\n' +
        '                                </div>\n' +
        '                            </div>')
}

function escapeHtmlEntities (str) {
    return str
        .replace(/&/g, '&amp;')
        .replace(/>/g, '&gt;')
        .replace(/</g, '&lt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&apos;');
}

function urlify(text) {
    if(text.includes('youtu.be')) {
        text =  text.replace('youtu.be', 'www.youtube.com/embed');
    } else {
        text = text.replace("watch?v=", "embed/");
    }
    console.log(text);
    return text.replace(urlRegex, function(url) {
        console.log(url);
        return '<iframe class="video"  src="'+ url + '" target="_parent" > </iframe>'
    })
}
