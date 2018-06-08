$(document).ready(function () {

    //check role if admin then show button and edit otherwise not.
    var role = "True" === $('#hdnRole').val();

    /***********************************
    *      jQuery Datatable      *
    ***********************************/

    var responsiveDt;
    var breakpointDefinition = {
        tablet: 1024, /*1024 for tablet changed to 1360*/
        phone: 480
    };

    //jQuery datatable
    $('#tblEmployee').dataTable({
        "bAutoWidth": false,
        cache: false,
        "bServerSide": true,
        //"pagingType": "full_numbers",
        "bProcessing": true,
        "bJqueryUI": true,
        "iDisplayLength": 10,
        "aLengthMenu": [[5, 7, 10, 25, 50], [5, 7, 10, 25, 50]],
        "aaSorting": [[0, "desc"]],
        "sAjaxSource": '/Employee/EmployeeDataTable',
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveDt) {
                responsiveDt = new ResponsiveDatatablesHelper($('#tblEmployee'), breakpointDefinition);
            }
        },
        "rowCallback": function (nRow, data) {
            responsiveDt.createExpandIcon(nRow);
        },
        "drawCallback": function () {
            responsiveDt.respond();
            $("a.edit").tooltip({
                placement: 'top',
                trigger: 'hover',
                title: 'Edit'
            });
            $("a.edit").removeAttr("title");
            $("a.delete").tooltip({
                placement: 'top',
                trigger: 'hover',
                title: 'Delete'
            });
            $("a.delete").removeAttr("title");
            $("a.view").tooltip({
                placement: 'top',
                trigger: 'hover',
                title: 'Details'
            });
            $("a.view").removeAttr("title");
        },
        "aoColumns": [
            { "sName": "Id", "sClass": "hidden" },
            {
                "sName": "FirstName", "sClass": "center",
                "mRender": function (data, type, full) {
                    return '<a href="/Patient/Details?id=' +
                        full[0] +
                        '" class="view"><span class="avatar avatar-xs"><img class="box-shadow-2" src = "/images/portrait/small/avatar-s-4.png" alt = "avatar" ></span ><span> ' + data + '</span></a>';
                }
            },
            { "sName": "Number", "sClass": "center" },
            { "sName": "Grade", "sClass": "center" },
            { "sName": "Appraisal Period", "sClass": "center" },
            {
                "sName": "Department", "sClass": "center",
                "mRender": function (data, type, full) {
                    return '<button type="button" class="btn btn-sm btn-outline-danger round">' + data + '</button>';
                }
            },
            {
                "sName": "Action", "sClass": "p-1 width-85",
                "bSortable": false,
                "mRender": function (data, type, full) {
                    if (role) {
                        return '<a href="#" class="edit btn btn-sm btn-icon btn-primary"><i class="la la-edit"></i></a>&nbsp;' +
                            '<a href="#" class="delete btn btn-sm btn-icon btn-danger" onclick="Delete(' +
                            full[0] +
                            ')"><i class="la la-trash"></i></a>&nbsp;';
                    } else {
                        return '<a href="#" class="edit btn btn-sm btn-icon btn-primary"><i class="la la-edit"></i></a>';
                    }

                }
            }
        ]
    });

    //Delete button click
    window.Delete = function (id) {
        //confirm delete
        swal({
            title: "Are you sure?",
            text: "Your will not be able to recover this record!",
            type: "warning",
            showCancelButton: true,
            cancelButtonColor: "#FFF",
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!"
        },
            function () {
                var url = '/Patient/Delete?id=' + id;
                $.ajax({
                    url: url,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    //data: JSON.stringify({ patientId: id }),
                    success: function (response) {
                        if (response.success) {
                            window.location.reload();
                        }
                    },
                    error: function (xhr, textStatus, error) {
                        if (typeof console === "object") {
                            console.log(xhr.status + "," + xhr.responseText + "," + textStatus + "," + error);
                        }
                    }
                });
            });
    }
});