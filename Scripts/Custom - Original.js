//var viewControllerRoute; // this will be used to keep track of what content must be there in the .content-wrapper
var contentRefresh; // this variable will store a refresh function to refresh .content-wrapper
//var tables = [];
//var selectedValueIn = {}; // in case if there is a value index selected in a table.
//var afterSelection = function () { console.log("unimplemented afterselection function"); };
//var afterSubmit = function () { console.log("unimplemented afterSubmit function"); };


/* 
@purpose - Ajaxify the sidebar
@parameters - none
@user - any logged in user
 */
function bindEventListeners() {
    // onclick of "sidebar-links > a"
    $("#sidebar-links").find('a').each(function () {
        $(this).click(function (event) {
            if (!$(this).prop('href').match("#")) {
                var href = $(this).attr('href');
                contentRefresh(href);
                event.preventDefault();
                return false;
            }
        });

        // onclick of refresh button clicked in the "main-header"
        $("#refresh").unbind().click(function () {
            contentRefresh(viewControllerRoute);
        });

    });
}


/* this method will load data into html elements in the "content-wrapper", can be called at any event where a content
  pane refresh is needed. This variable is declared in the layout. Anytime a refresh is needed just call contentRefresh(controllerRoute);
  controller route must be declared at the begining of Custom.js. This controller route must be reassigned "before" calling this method "everytime".
  */
contentRefresh = function updateContent(viewControllerRoute) {

    $(document).ready(function () {
        $.ajax({
            url: viewControllerRoute,
            type: "GET"
        }
    ).done(function (view) {
        $(".content-wrapper").html(view);
    });
    });
};


/*loading page from an api*/

function updateTableFromApi(table, apiUrl, afterSelection) {
    table.clear();

    // undefined legth property error appear if used datatable ajax functionality
    $.ajax({
        url: apiUrl,
        type: "GET",
        dataType: "json"
    }
        ).done(function (dataSet) {
            // iniitiation of table
            table.rows.add(dataSet).draw();
        });
    table.on('click', 'tr', function () {

        var selectedIndex = $(this).index(); // this index must be selected 

        $(this).siblings().each( // and all the siblings unselected
            function () {
                $(this).removeClass("selected")
            }
            );
        $(this).toggleClass("selected");
        afterSelection();
    });
}

function updateTable(table, apiUrl) {
    table.clear();

    // undefined legth property error appear if used datatable ajax functionality
    $.ajax({
        url: apiUrl,
        type: "GET",
        dataType: "json"
    }
        ).done(function (dataSet) {
            // iniitiation of table
            table.rows.add(dataSet).draw();
        });
    table.on('click', 'tr', function () {

        var selectedIndex = $(this).index(); // this index must be selected 

        $(this).siblings().each( // and all the siblings unselected
            function () {
                $(this).removeClass("selected")
            }
            );
        $(this).toggleClass("selected");
    });
}


function updateTableFromApi1(table, apiUrl, afterSelection, afterUnselection) {
    table.clear();

    // undefined legth property error appear if used datatable ajax functionality
    $.ajax({
        url: apiUrl,
        type: "GET",
        dataType: "json"
    }
        ).done(function (dataSet) {
            // iniitiation of table
            table.rows.add(dataSet).draw();
        });
    table.on('click', 'tr', function () {

        var selectedIndex = $(this).index(); // this index must be selected 

        $(this).siblings().each( // and all the siblings unselected
            function () {
                $(this).removeClass("selected")
            }
            );
        $(this).toggleClass("selected");
        if ($(this).hasClass("selected")) {
            afterSelection();
        } else {
            afterUnselection();
        }
    });
}


/*
@purpose - initiating a selectable data table
@user - any
@parameters 
- tableSelector - id attribute of the target table
- parameters - parameters that needs to be sent into the DataTable() function. Must be a raw object.
*/
function initiateTable(tableSelector, parameters) {

    // initiating the data table
    var table = $("#" + tableSelector).DataTable(parameters);

    // making the table selectable
    $("#" + tableSelector).on('click', 'tr',function () {

        
        $(this).siblings().each( // and all the siblings unselected
            function () {
                $(this).removeClass("selected")
                console.log("row unselected:");
            }
            );
        console.log("row selected:");
        $(this).toggleClass("selected");

    }
    );

    return table;
}

function initiateTable1(tableSelector, columns, onSelection, onUnSelection) {

    var table = $("#" + tableSelector).DataTable(columns);

    $("#" + tableSelector).on('click', 'tr', function () {

        var selectedIndex = $(this).index(); // this index must be selected 

        $(this).siblings().each( // and all the siblings unselected
            function () {
                $(this).removeClass("selected")
            }
            );
        $(this).toggleClass("selected");

        if ($(this).hasClass("selected")) {
            onSelection();
        } else {
            onUnSelection();
        }
    });

    return table;
}


function initiateSynchronousTable(tableSelector, columns, formSelector) {

    var table = $("#" + tableSelector).DataTable(columns);

    $("#" + tableSelector).on('click', 'tr', function () {

        var selectedIndex = $(this).index(); // this index must be selected 

        $(this).siblings().each( // and all the siblings unselected
            function () {
                $(this).removeClass("selected")
            }
            );
        $(this).toggleClass("selected");
        if ($(this).hasClass("selected")) {
            // get selected row data
            var data = table.row($(this).index()).data();
            // set this data to form
            setFormValues($(formSelector));
        } else {
            // reset the form
            resetForm($(formSelector));
        }

    });

    return table;
}
function getTableSelectedRowKey(table) {
    var selectedKey;
    table.children("tbody").children(".selected").each(function () {
        selectedKey = $(this).children().first().html();
    });
    return selectedKey;
}

function getTableSelectedRowIndex(table) {
    var selectedIndex;
    table.children("tbody").children(".selected").each(function () {
        selectedIndex = $(this).index();

    });
    return selectedIndex;
}

function loadTableFromApi(tableId, apiUrl, dataToColumnMap) {

    $(document).ready(function () {
        $.ajax({
            url: apiUrl,
            type: "GET",
            dataType: "json"
        }
        ).done(function (dataSet) {
            var tableIndex = tableId.replace('#', '');

            // iniitiation of table
            tables[tableIndex] = $(tableId).DataTable({
                data: dataSet,
                "columns": dataToColumnMap
            });

            var tbody = tableId + " tbody";
            $(tbody).on('click', 'tr', function () {
                // unselect all the selected columns of the table

                var selectedIndex = $(this).index();

                $(this).siblings().each(
                    function () {
                        $(this).removeClass("selected")
                    }
                    );
                $(this).toggleClass("selected");

                var data = tables[tableIndex].row(selectedIndex).data();

                selectedValueIn[tableIndex] = data.VendorId;

                afterSelection();
            });
        });
    });
}

/*
binding an ajax form submission functinality to a form
*/

function bindajaxformsubmitevent(AjaxFormId, formPostUrl, afterSubmission) {

    $(document).ready(function () {
        var afterSubmit = afterSubmission;
        AjaxFormId = "#" + AjaxFormId;

        $(AjaxFormId).unbind().submit(function (event) {

            // get the form data
            var formdata = {};
            var inputsSelector = AjaxFormId + " .form-control";

            $(inputsSelector).each(function () {
                formdata[$(this).attr("name")] = $(this).val();
            });
            // send json object to the formPostUrl
            $.ajax({
                type: 'post', // define the type of http verb we want to use (post for our form)
                url: formPostUrl, // the url where we want to post
                data: formdata, // our data object
                datatype: 'json', // what type of data do we expect back from the server
                encode: true
            })
                // in the case of succesfull form submission
                .done(function (data) {
                    if (afterSubmit != null) {
                        afterSubmit(data);
                    }
                    // here we will handle errors and validation messages. Error handling better done in the failure event
                });
            return false;
        });
    });
}

// value must be the database key. option text must be the text comes within the html
function loadListBox(apiUrl, listBoxSelector, Value, OptionText) {
    $.ajax({
        url: apiUrl,
        type: "GET",
        dataType: "json"
    }
        ).done(function (vendors) {
            $.each(vendors, function (item) {
                $(listBoxSelector).append('<option value="' + this[Value] + '">' + this[OptionText] + '</option>');
            });
        }
            );
}

function loadListBoxFromApi(apiUrl, listBoxSelector, Id, OptionText) {
    $.ajax({
        url: apiUrl,
        type: "GET",
        dataType: "json"
    }
        ).done(function (items) {
            $.each(items, function (item) {
                $(listBoxSelector).append('<option value="' + this[Id] + '">' + this[OptionText] + '</option>');
            });
        }
            );
}
function getFormValues(formId) {
    var formSelector = "#" + formId + " .form-control";
    var nameAndValues = {};
    $("#" + formId + " select option:selected").each(function () {
        nameAndValues[$(this).parent().attr("id")] = $(this).html();
    });

    $(formSelector).each(function () {
        nameAndValues[$(this).attr("name")] = $(this).val();
    });
    return nameAndValues;
}

function getFormData(form) {
    var data = {};
    form.children("input").each(function (item) {
        data[$(this).attr("name")] = $(this).val();
        $(this).val("");
    });

    form.children("option:selected").each(function () {
        data[$(this).parent().attr("id")] = $(this).attr("val");
    });
    return data;
}

function loadListBoxFromArray(apiUrl, listBoxSelector, ValueId) {
    $.ajax({
        url: apiUrl,
        type: "GET",
        dataType: "json"
    }
        ).done(function (listItems) {
            $.each(listItems, function (item) {
                $(listBoxSelector).append('<option value="' + this[ValueId] + '">' + this + '</option>');
            });
        });
}
function getTableData(table) {
    var tableData = table.rows().data();
    var rowData = {};

    var i = 0;// current iteration index
    for (var row in tableData) {
        // rowdata has unwanted objects, we are skipping them using length attribute limit
        if (i < tableData.length) {
            rowData[i] = tableData[i];
        } else {
            break;
        }
        i++;
    }
    return rowData;
}

function syncFormWithTable(formId, table, tableIndexColumn, beforeSubmit, onCompletion) {

    $("#" + formId).on("submit", function (event) {
        beforeSubmit();
        var data = {};
        data = getFormValues(formId);

        var rowdata = table.rows().data(); // row data of the table

        var i = 0; // for iteration
        for (var material in rowdata) {
            // rowdata has unwanted objects, we are skipping them using length attribute limit
            if (i < rowdata.length) {
                // if table already has the material, remove it.
                if (rowdata["" + i + ""]["" + tableIndexColumn + ""] == data["" + tableIndexColumn + ""]) {

                    // get the index
                    var index = table.row(material).index();
                    var index = parseInt(index);

                    table.row(index).remove();
                    break;
                }
            } else {
                break;
            }
            i++;
        }

        // add material to the row
        table.row.add(data).draw(false);
        onCompletion();
        event.preventDefault();
    });
}

function sendComplexObjectOnClick(buttonId, table, tableDataName, formId, apiUrl, beforeSubmit, onCompletion) {

    $("#" + buttonId).on('click', function (event) {
        beforeSubmit();
        var submitData = {}; // the data that will be sent using ajax

        var tableData = getTableData(table); // the table that will be attached to the data

        table.fnDestroy();

        $("#" + formId + " .form-control").each(function () {
            submitData[$(this).attr2("name")] = $(this).val();
            $(this).val("");
        });

        submitData["" + tableDataName + ""] = tableData; // adding the materials in datatable

        $.ajax({
            type: 'post',
            url: apiUrl,
            data: JSON.stringify(submitData), // need to be strigified to avoid browser hang
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                onCompletion();
            }
        });



        event.preventDefault();
    });
}

function setFormValues(form, data) {
    // form is a jquery object
    // data is an object
    $(form.selector + " input").each(function () {

        $(this).val(data[$(this).attr("name")]);
    }
    );
}


function initiateTableActionButtons() {
    $("#action-buttons").css("pointer-events", "none");
    $("#action-buttons").children().each(function () {
        $(this).on('click', function () { // editor-forms
            // hiding all the forms
            $("#editor-forms").children().each(function () {
                $(this).addClass("hidden");
            });

            // resetting the colors of buttons to btn-primary
            $("#action-buttons").children().each(function () {
                $(this).removeClass("btn-warning").addClass("btn-primary");
            });

            // setting the selected button color to btn-warning
            $(this).addClass("btn-warning");

            var form = $(this).attr("for");
            $("#" + form).removeClass("hidden");

        });
    });
}

function resetForm(form) {
    $(form.selector + " input").each(function () {
        $(this).val("");
    }
       );
}
// use this variable for processing , only use the html5 local storage api for crud operations with local storage
var ProductsInQuotation = []; // self explanatory , to be stored in the local storage

function getLocalStorageData(key) {
    if (localStorage) {
        if (localStorage.getItem(key)) {
            //set local storage 'object' to save products Enterprise choose
            return JSON.parse(localStorage.getItem(key));
        } else {
            console.log("Local Storage empty or not accessible : key : " + key);
        }
    } else {
        console.log("Browser doesn't have local storage");
    }
    return;
}

function addToQuote(product, wholesalerId) {
    var Quotation = {};
    var ProductsInQuotation = {};
    if (localStorage) {
        if (!localStorage.getItem('Quotation')) {
            // if the localstorage for Quotation is not created already, create it
            localStorage.setItem('Quotation', JSON.stringify({}));
        }
        Quotation = JSON.parse(localStorage.getItem('Quotation'));
        if (Quotation[wholesalerId]) {
            // quotation with the wholesaler exist already, we assume that the Products in Quotation object is created already
            ProductsInQuotation  = Quotation[wholesalerId]["ProductsInQuotation"]
        } else {
            // this is a new quotation for this wholesaler
            Quotation[wholesalerId] = {};
            Quotation[wholesalerId]["ProductsInQuotation"] = {};
        }


    } else {
        console.log("Browser doesn't support local storage method");
        return ;
    }

    // check if the product already exist
    if (ProductsInQuotation[product.ProductId]) {
        ProductsInQuotation[product.ProductId]["Quantity"] = parseInt(ProductsInQuotation[product.ProductId]["Quantity"]) + parseInt(product.Quantity); // new quantity
        ProductsInQuotation[product.ProductId]["UnitPrice"] = parseInt(product.UnitPrice); // new price

        Quotation[wholesalerId]["ProductsInQuotation"] = ProductsInQuotation;
        console.log(Quotation);
        localStorage.setItem('Quotation', JSON.stringify(Quotation));
        return;
    }

    ProductsInQuotation[product.ProductId] = product;
    Quotation[wholesalerId]["ProductsInQuotation"] = ProductsInQuotation;

    localStorage.setItem('Quotation', JSON.stringify(Quotation));

    console.log(Quotation);
}



function addToOrder(product, wholesalerId) {
    var Order = {};
    var ProductsInOrder = {};
    if (localStorage) {
        if (!localStorage.getItem('Order')) {
            // if the localstorage for order is not created already, create it
            localStorage.setItem('Order', JSON.stringify({}));
        }
        Order = JSON.parse(localStorage.getItem('Order'));
        if (Order[wholesalerId]) {
            // order with the wholesaler exist already, we assume that the Products in order object is created already
            ProductsInOrder = Order[wholesalerId]["ProductsInOrder"]
        } else {
            // this is a new order for this wholesaler
            Order[wholesalerId] = {};
            Order[wholesalerId]["ProductsInOrder"] = {};
        }


    } else {
        console.log("Browser doesn't support local storage method");
        return;
    }

    // check if the product already exist
    if (ProductsInOrder[product.ProductId]) {
        ProductsInOrder[product.ProductId]["Quantity"] = parseInt(ProductsInOrder[product.ProductId]["Quantity"]) + parseInt(product.Quantity); // new quantity
        ProductsInOrder[product.ProductId]["UnitPrice"] = parseInt(product.UnitPrice); // new price

        Order[wholesalerId]["ProductsInOrder"] = ProductsInOrder;
        console.log(Order);
        localStorage.setItem('Order', JSON.stringify(Order));
        return;
    }

    ProductsInOrder[product.ProductId] = product;
    Order[wholesalerId]["ProductsInOrder"] = ProductsInOrder;

    localStorage.setItem('Order', JSON.stringify(Order));

    console.log(Order);
}

function addToRetailOrder(product, EnterpriseId) {
    var retailOrder = {};
    var ProductsInRetailOrder = {};
    if (localStorage) {
        if (!localStorage.getItem('retailOrder')) {
            // if the localstorage for retailOrder is not created already, create it
            localStorage.setItem('retailOrder', JSON.stringify({}));
        }
        retailOrder = JSON.parse(localStorage.getItem('retailOrder'));
        if (retailOrder[EnterpriseId]) {
            // retailOrder with the wholesaler exist already, we assume that the Products in retailOrder object is created already
            ProductsInRetailOrder = retailOrder[EnterpriseId]["ProductsInRetailOrder"]
        } else {
            // this is a new retailOrder for this wholesaler
            retailOrder[EnterpriseId] = {};
            retailOrder[EnterpriseId]["ProductsInRetailOrder"] = {};
        }


    } else {
        console.log("Browser doesn't support local storage method");
        return;
    }

    // check if the product already exist
    if (ProductsInRetailOrder[product.ProductId]) {
        ProductsInRetailOrder[product.ProductId]["Quantity"] = parseInt(ProductsInRetailOrder[product.ProductId]["Quantity"]) + parseInt(product.Quantity); // new quantity
        ProductsInRetailOrder[product.ProductId]["UnitPrice"] = parseInt(product.UnitPrice); // new price

        retailOrder[EnterpriseId]["ProductsInRetailOrder"] = ProductsInRetailOrder;
        console.log(retailOrder);
        localStorage.setItem('retailOrder', JSON.stringify(retailOrder));
        return;
    }

    ProductsInRetailOrder[product.ProductId] = product;
    retailOrder[EnterpriseId]["ProductsInRetailOrder"] = ProductsInRetailOrder;

    localStorage.setItem('retailOrder', JSON.stringify(retailOrder));

    console.log(retailOrder);
}