function changeMarkerColor(element) {
  let attrValue = $(element).attr('aria-controls');
  let markerIndex = attrValue.split("-")[1]

  isInfoRequested = Boolean($(element).attr('complements-called'));
  if (isInfoRequested === false){
    $(element).attr('complements-called', true)
    searchDetails(searchResult.Results[parseInt(markerIndex)].Fsq_id, markerIndex)
    searchTips(searchResult.Results[parseInt(markerIndex)].Fsq_id, markerIndex)
  }

  for (let i = 0; i < markers.length; i++ ){
    if (i === parseInt(markerIndex)){
      markers[i].setIcon(L.icon({
        iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-red.png',
        shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
      }));
      markers[i].setOpacity(1);
      markers[i].setZIndexOffset(1000);
    }else{
      markers[i].setIcon(L.icon({
        iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-blue.png',
        shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
      }));
      markers[i].setOpacity(0.75);
      markers[i].setZIndexOffset(0);
    }
  }
}

function easySearch(element){
  $('#query')[0].value = $(element)[0].innerText;
  $('#searchForm').submit();
}

function searchDetails(elementId, index){
  $.ajax({
    url: `https://api.foursquare.com/v3/places/${elementId}/photos?limit=6`,
    type: "GET",
    dataType: "json",
    headers: {
      "Authorization": "", //TODO env variable
      "Accept-Language": "es"
    },
    success: function (response) {
      let element = $(`#elements--pictures--${index}`);
      element.empty();
      if(response.length > 0){
        response.forEach((image) => {
          element.append(`<img src="${image.prefix}100x100${image.suffix}" />`);
        })
      }else if(response.length === 0){
        element.append(`<div class="alert alert-warning" role="alert">No se encontraron imagenes</div>`);
      }
    },
    error: function (xhr, status, error) {
      console.error(error);
    }
  });
}

function searchTips(elementId, index) {
  $.ajax({
    url: `https://api.foursquare.com/v3/places/${elementId}/tips?limit=6`,
    type: "GET",
    dataType: "json",
    headers: {
      "Authorization": "", //TODO env variable
      "Accept-Language": "es"
    },
    success: function (response) {
      let element = $(`#elements--comments--${index}`);
      element.empty();
      if (response.length > 0) {
        response.forEach((comment) => {
          let newElement = `
            <figure>
              <blockquote class="blockquote">
                <p>${comment.text}</p>
              </blockquote>
              <figcaption class="blockquote-footer">
                ${formatDate(comment.created_at)}
              </figcaption>
            </figure>
          `;
          element.append(newElement);
        })
      } else if (response.length === 0) {
        element.append(`<div class="alert alert-warning" role="alert">No se encontraron imagenes</div>`);
      }
    },
    error: function (xhr, status, error) {
      console.error(error);
    }
  });
}

function formatDate(dateString) {
  var date = new Date(dateString);
  var monthNames = [
    "Enero", "Febrero", "Marzo",
    "Abril", "Mayo", "Junio", "Julio",
    "Agosto", "Septiembre", "Octubre",
    "Noviembre", "Diciembre"
  ];

  var day = date.getDate();
  var monthIndex = date.getMonth();
  var year = date.getFullYear();

  var formattedDate = `${day} de ${monthNames[monthIndex]} de ${year}`;

  return formattedDate;
}

$(function(){
  $('[data-bs-toggle=tooltip').tooltip();
})

function addToFavorites(name, fsq_id, address, element){
  let payload= JSON.stringify({
    "User_id": 1,
    "Name": name,
    "Fsq_id": fsq_id,
    "address": address
  })
  console.log("test: ", payload)
  $.ajax({
    url: `http://localhost:5113/api/Favorites`,
    type: "POST",
    dataType: "json",
    contentType: "application/json",
    data: payload,
    success: function (response) {
      let newToast;
      let randomNumber = Math.floor(Math.random() * (10000 - 0 + 1)) + 0;
      if(response.success){
        let svg = $(element).closest('span').find('svg');
        let path = svg.find('path');
        path.attr('fill', '#FFD700');
        newToast = `
        <div id="liveToast-${randomNumber}" class="toast text-bg-success" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="toast-header">
                <strong class="me-auto">Bootstrap</strong>
                <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body">
                ${response.message}
            </div>
        </div>
        `;
        $('#toast-container').append(newToast);
        triggerToast(randomNumber);
      }else{
        newToast = `
        <div id="liveToast-${randomNumber}" class="toast text-bg-warning" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="toast-header">
                <strong class="me-auto">Bootstrap</strong>
                <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body">
                ${response.message}
            </div>
        </div>
        `;
        $('#toast-container').append(newToast);
        triggerToast(randomNumber);
      }
    },
    error: function (xhr, status, error) {
      let randomNumber = Math.floor(Math.random() * (10000 - 0 + 1)) + 0;
      newToast = `
        <div id="liveToast-${randomNumber}" class="toast text-bg-danger" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="toast-header">
                <strong class="me-auto">Error ${error}</strong>
                <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body">
                Ha ocurrido un error al procesar tu solicitu
            </div>
        </div>
        `;
      $('#toast-container').append(newToast);
      triggerToast(randomNumber);
      console.error(error);
    }
  });
}

function triggerToast(randomNumber){
  const toastLiveExample = document.getElementById(`liveToast-${randomNumber}`)
  console.log("test: ", toastLiveExample);
  const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample)
  toastBootstrap.show();
}