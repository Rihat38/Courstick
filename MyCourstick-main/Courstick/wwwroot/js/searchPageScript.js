const search = document.getElementById('course-searcher');
const filters = document.getElementsByClassName('tab');


let active;
const divyArray = Array.from(filters);

window.addEventListener('load', () => {
    getAllCourses().then((r) => {
        r.forEach((i) => {
            console.log(i);
            $('#cards-list').append(createCard(i));
            console.log('created');
        })
        // $('#cards-list').html(r);
    }).catch((e) => {
        console.log(e);
    })
})

async function getAllCourses() {
    return $.ajax({
        url: '/Search/GetCourses',
        contentType: 'application/json',
        type: 'GET',
        success: (r) => {
            return r;
        }
    })
}

divyArray.forEach((item) => {
    item.addEventListener('click', () => {
        console.log(active != null);
        if (active) {
            //active.classList.remove('active_tab');
            active.style.border = 'none';
            active = item;
            item.style.border = '1px solid #F75E05'
            //item.classList.add('active_tab');

            getCourses(item.innerHTML.trim());

        } else {
            active = item;
            //item.classList.add('active_tab');
            item.style.border = '1px solid #F75E05'
            getCourses(item.innerHTML.trim());
        }

    })
});


function getCourses(name) {
    return $.ajax({
        url: '/Search/GetCourseByName?name=' + name,
        contentType: 'application/json',
        type: 'GET',
        success: (r) => {
            return r;
        }
    })
}

const createCard = (r) => {
    return `<a href="/Course/Course/${r.id}" class="card-search">
    <img class="card-image" src=${r.imageUrl}
    <div class="card-content">
        <div class="tag tag-color-cyan">
            Сварка)
        </div>

        <p class="course-name">
            ${r.name}
        </p>

        <div class="card-price-container">
            <p class="price">${r.price} ₽</p>
        
                <div class="card-author-block default_font_settings">
                            ${r.author ?? 'Автор'}
                </div>
        </div>
    </div>
</a>
`
}

search.addEventListener('keyup', async (e) => {
    getCourses(e.target.value).then((r) => {
        $("#cards-list").empty();
        r.forEach((i) => {
            console.log(i);
            $('#cards-list').append(createCard(i));
            console.log('created');
        })
    })
});
