

function showAlert(message) {
    alert(message);
}



function getComponentPartial() {
    try {
        $.get({
            url: '../Shared/Components/Component',
            dataType: 'HTML',
            success: function (data) {
                $('#componentPlaceholder').html(data);
            }
        });
    }
    catch (e) {
        console.log(e.message);
    }
}

