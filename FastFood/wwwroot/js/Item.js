var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/item/getall' },
        "columns": [
            { data: 'title', "width": "15%" },
            { data: 'description', "width": "30%" },
            { data: 'price', "width": "10%" },
            { data: 'category', "width": "10%" },
            { data: 'subCategory', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div >
                     <a href="/admin/item/edit?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>               
                     <a onClick=Delete('/admin/item/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'Delete',
                success: function (data) {
                    if (data.success) {
                        swal.fire(
                            'Deleted!',
                            data.message,
                            'success'
                        ).then(() => {
                            // Optionally remove the row from the table or reload the page
                            dataTable.ajax.reload();
                        });
                    }
                    else {
                        swal.fire(
                            'Error!',
                            data.message,
                            'error'
                        );
                    }
                }
            })
        }
    });
}

