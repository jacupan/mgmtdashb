
var dateFields = [];

var dateFields3001 = [];

var dateFields6000 = [];

function alertMSG(msg) {


    alert(msg);
}

function generateGrid3001(gridData) {

    var model = generateAssyModel3995(gridData[0]);
    var columns = generateColumnsAssyOuts(gridData[0]);

    var colLenght = columns.length;

    var aggregates_ = generateAggregaAssyOuts(gridData[0]);

    var parseFunction;
    if (dateFields3001.length > 0) {
        parseFunction = function (response) {
            for (var i = 0; i < response.length; i++) {
                for (var fieldIndex = 0; fieldIndex < dateFields.length; fieldIndex++) {
                    var record = response[i];
                    record[dateFields[fieldIndex]] = kendo.parseDate(record[dateFields[fieldIndex]]);
                }
            }
            return response;
        };
    }

    var schema;
    if (parseFunction) {
        schema = {
            model: model,
            parse: parseFunction
        };
    }
    else {
        schema = {
            model: model
        };
    }

    $("#gridAssyOutput3001").kendoGrid({
        toolbar: ["excel"],
        dataSource: {
            data: gridData,
            schema: schema,
            pageSize: 100,
            group: { field: "ProductGroup", aggregates: aggregates_ },
            aggregate: aggregates_
        },
        pageable: {
            refresh: true,
            pageSizes: false,
            buttonCount: 5
        },
        columns: columns,
        sortable: true,
        resizable: true,
        selectable: "single cell",
        excelExport: function (e) {

            var timestamp = new Date().format("yyyyMMddHHmmss");

            //                                            var rows = e.workbook.sheets[0].rows;

            var sheet = e.workbook.sheets[0];

            colLenght = colLenght - 1;

            for (var cellIndex = 2; cellIndex < colLenght; cellIndex++) {

                // row.cells[cellIndex].format = "[Blue]#,##0.0_);[Red](#,##0.0);0.0;"

                // var celVal =  sheet.rows[row].cells[cellIndex].value;

                var celVal = 0;

                var grandTotal = 0;

                for (var rowIndex = 2; rowIndex < sheet.rows.length; rowIndex++) {

                    var row = sheet.rows[rowIndex];

                    //  alert(row.type);

                    //  var celVal =  row.cells[cellIndex].value;

                    if (row.type == "data") {
                        var DataCell = row.cells[cellIndex];

                        // var dataCelVal = ;

                        if (typeof (row.cells[cellIndex]) === "undefined") {

                            alert("undefined");

                        }

                        else {



                            celVal = celVal + (row.cells[cellIndex].value);

                            DataCell.value = row.cells[cellIndex].value;


                        }



                    }

                    else if (row.type == "group-footer") {

                        var groupFooterCell = row.cells[cellIndex];

                        groupFooterCell.value = celVal;

                        grandTotal = grandTotal + row.cells[cellIndex].value;

                        celVal = 0;


                    }
                    else if (row.type == "footer") {

                        var footerCell = row.cells[cellIndex];

                        footerCell.value = grandTotal;

                        grandTotal = 0;
                    }


                }


            }


            e.workbook.fileName = "AssemblyOuts3001" + "_" + timestamp + ".xlsx";
        },


        dataBound: function () {
            this.hideColumn("Connector");

            $.ajax({

                url: '/Grid/GetAssemblyID/',
                type: 'POST',
                data: JSON.stringify({ operation: "3001" }),
                datatype: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {

                    var gridassy = $("#gridAssyOutput3001").data("kendoGrid");

                    //  grid.hideColumn("Connector");

                    $.each(data, function (i, item) {

                        var colnameassy = item.ID.toString();

                        colnameassy = colnameassy.split("_").join(" ");

                        var colnameIndex1 = colnameassy.lastIndexOf("W");

                        var connectorID1 = colnameassy.substring(0, colnameIndex1 - 1).trim();

                        colnameassy = colnameassy.substring(colnameIndex1 - 1);

                        var colIndex1 = gridassy.thead.find("th[data-field='" + colnameassy + "']").index();

                        var dataItem1 = gridassy.dataSource.get(connectorID1);

                        if (typeof dataItem1 === "undefined") {

                            alert("ConnectorID : " + connectorID1 + " is undefined. Please contact your IT Administrator");

                        }

                        var rowIndex1 = gridassy.tbody.find("tr[data-uid='" + dataItem1.uid + "']").index();

                        loadAssyDirtyflag(rowIndex1, colIndex1 + 1);


                    });

                }  // end of success function

            }); //end of ajax request       

        } // end of databound


    })

    $("#gridAssyOutput3001").data("kendoGrid").dataSource.read();


    var grid = $("#gridAssyOutput3001").data("kendoGrid");

    grid.table.kendoTooltip({
        filter: ".assyOuts",
        width: 200,
        beforeShow: function (e) {


            if ($("#varAssyComments").val() === "" || $("#varAssyComments").val() === null) {
                // don't show the tooltip if the name attribute contains null
                e.preventDefault();

            }
        },
        content: function (e) {

            var r = $("#varAssyComments").val();

            return r;

            // ----- return function for getting remarks ------------

        },

        hide: function () {

            $("#varAssyComments").val(null);

        }


    });   //  end of kendo tooltip


} // end function grid


function generateColumnsAssyOuts(gridData) {

    // initiate the column array
    var columns = [];

    // iterate all of the data items in the first element of the returned JSON data
    for (var dataItem in gridData) {

        var colTitle = dataItem;

        // check column datatype

        switch (typeof gridData[dataItem]) {
            case "string":
                if (dataItem == "ProductGroup") {
                    columns.push({
                        field: dataItem,
                        title: colTitle,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: center; font-weight: bold" },
                        groupHeaderTemplate: "#= value #",
                        visible: true,
                        hidden: true

                    });
                }
                else if (dataItem == "PackageGroup") {
                    columns.push({
                        field: dataItem,
                        title: colTitle,
                        width: 160,
                        locked: true,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: left; font-weight:bold; font-size:small;" },
                        footerAttributes: { style: "text-align: left; font-size:small;" },
                        groupFooterTemplate: "Total : ",
                        footerTemplate: "Grand Total : "
                    });
                }

                else if (dataItem == "Connector") {
                    columns.push({
                        field: dataItem.trim(),
                        title: colTitle,
                        width: 200
                    });
                }

                else {
                    columns.push({
                        field: dataItem,
                        title: colTitle,
                        width: 100,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: center; font-weight:bold; font-size:small;" }
                    });
                }
                break;

            case "number":
            default:
                if (dataItem.includes('Target'))
                    columns.push({
                        field: dataItem,
                        title: "Target",
                        width: 100,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: center; font-weight:bold; font-size:small; color: gray" },
                        format: "{0:N0}",
                        aggregates: ["sum"],
                        footerAttributes: { style: "text-align: center; font-size:small; color: Blue" },
                        groupFooterTemplate: "#: kendo.format('{0:N0}', sum) #",
                        footerTemplate: "#: kendo.format('{0:N0}', sum) #"

                    });
                else {
                    columns.push({
                        field: dataItem,
                        title: colTitle,
                        width: 100,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: center; font-weight:bold; font-size:small;" },
                        //template: "#= workWeekDetails(data, '" + dataItem + "') #",
                        template: "<a class='assyOuts' ; workWeek='" + dataItem + "' href='\\#'>#= workWeekDetailsAssyOutput(data, '" + dataItem + "') #</a>",
                        aggregates: ["sum"],
                        footerAttributes: { style: "text-align: center; font-size:small; color: Blue" },
                        groupFooterTemplate: "#: kendo.format('{0:N0}', sum/1000) #",
                        footerTemplate: "#: kendo.format('{0:N0}', sum/1000) #"
                    });
                }
        }
    }

    return columns;

} // end of columns definition  


function loadAssyDirtyflag(rowIndex, colIndex) {

    var gviewAssy = $("#gridAssyOutput3001").data("kendoGrid");

    gviewAssy.tbody.find("tr:eq(" + rowIndex + ") td:nth-child(" + colIndex + ")").addClass("note");

}

//==============================================================================================================================================

function generateGrid(gridData) {

    var model = generateAssyModel3995(gridData[0]);
    var columns = generateColumnsAssyOuts3995(gridData[0]);

    var colLenght = columns.length;

    var aggregates_ = generateAggregaAssyOuts3995(gridData[0]);

    var parseFunction;
    if (dateFields.length > 0) {
        parseFunction = function (response) {
            for (var i = 0; i < response.length; i++) {
                for (var fieldIndex = 0; fieldIndex < dateFields.length; fieldIndex++) {
                    var record = response[i];
                    record[dateFields[fieldIndex]] = kendo.parseDate(record[dateFields[fieldIndex]]);
                }
            }
            return response;
        };
    }

    var schema;
    if (parseFunction) {
        schema = {
            model: model,
            parse: parseFunction
        };
    }
    else {
        schema = {
            model: model
        };
    }

    $("#gridAssyOutput3995").kendoGrid({
        toolbar: ["excel"],
        dataSource: {
            data: gridData,
            schema: schema,
            pageSize: 100,
            group: { field: "ProductGroup", aggregates: aggregates_ },
            aggregate: aggregates_
        },
        pageable: {
            refresh: true,
            pageSizes: false,
            buttonCount: 5
        },
        columns: columns,
        sortable: true,
        resizable: true,
        selectable: "single cell",
        excelExport: function (e) {
           
            var timestamp = new Date().format("yyyyMMddHHmmss");

            var sheet = e.workbook.sheets[0];

            colLenght = colLenght - 1;

            for (var cellIndex = 2; cellIndex < colLenght; cellIndex++) {

                // row.cells[cellIndex].format = "[Blue]#,##0.0_);[Red](#,##0.0);0.0;"

                // var celVal =  sheet.rows[row].cells[cellIndex].value;

                var celVal = 0;

                var grandTotal = 0;

                for (var rowIndex = 2; rowIndex < sheet.rows.length; rowIndex++) {

                    var row = sheet.rows[rowIndex];

                    //  alert(row.type);

                    //  var celVal =  row.cells[cellIndex].value;

                    if (row.type == "data") {
                        var DataCell = row.cells[cellIndex];

                        // var dataCelVal = ;

                        if (typeof (row.cells[cellIndex]) === "undefined") {

                            alert("undefined");

                        }

                        else {



                            celVal = celVal + (row.cells[cellIndex].value);

                            DataCell.value = row.cells[cellIndex].value;


                        }



                    }

                    else if (row.type == "group-footer") {

                        var groupFooterCell = row.cells[cellIndex];

                        groupFooterCell.value = celVal;

                        grandTotal = grandTotal + row.cells[cellIndex].value;

                        celVal = 0;


                    }
                    else if (row.type == "footer") {

                        var footerCell = row.cells[cellIndex];

                        footerCell.value = grandTotal;

                        grandTotal = 0;
                    }


                }


            }






            e.workbook.fileName = "AssemblyOuts3995" + "_" + timestamp + ".xlsx";
        
        
        },

        dataBound: function () {
            this.hideColumn("Connector");

            $.ajax({

                url: '/Grid/GetAssemblyID/',
                type: 'POST',
                data: JSON.stringify({ operation: "3995" }),
                datatype: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {

                    var gridassy = $("#gridAssyOutput3995").data("kendoGrid");

                    //  grid.hideColumn("Connector");

                    $.each(data, function (i, item) {


                        var colnameassy = item.ID.toString();

                        colnameassy = colnameassy.split("_").join(" ");

                        var colnameIndex1 = colnameassy.lastIndexOf("W");

                        var connectorID1 = colnameassy.substring(0, colnameIndex1 - 1).trim();

                        colnameassy = colnameassy.substring(colnameIndex1 - 1);

                        var colIndex1 = gridassy.thead.find("th[data-field='" + colnameassy + "']").index();

                        var dataItem1 = gridassy.dataSource.get(connectorID1);

                        if (typeof dataItem1 === "undefined") {

                            alert("ConnectorID : " + connectorID1 + " is undefined. Please contact your IT Administrator");

                        }

                        var rowIndex1 = gridassy.tbody.find("tr[data-uid='" + dataItem1.uid + "']").index();

                        loadAssyDirtyflag3995(rowIndex1, colIndex1 + 1);


                    });

                }  // end of success function

            }); //end of ajax request       

        } // end of databound

    })

    $("#gridAssyOutput3995").data("kendoGrid").dataSource.read();

    var grid3995 = $("#gridAssyOutput3995").data("kendoGrid");

    grid3995.table.kendoTooltip({
        filter: ".assyOuts3995",
        width: 200,
        beforeShow: function (e) {

           
            if ($("#varAssyComments").val() === "" || $("#varAssyComments").val() === null) {
                // don't show the tooltip if the name attribute contains null
                e.preventDefault();

            }
        },
        content: function (e) {

            var r = $("#varAssyComments").val();

            return r;

            // ----- return function for getting remarks ------------

        },

        hide: function () {

            $("#varAssyComments").val(null);

        }


    });    //  end of kendo tooltip

}



function generateColumnsAssyOuts3995(gridData) {

    // initiate the column array
    var columns = [];

    // iterate all of the data items in the first element of the returned JSON data
    for (var dataItem in gridData) {

        var colTitle = dataItem;

        // check column datatype

        switch (typeof gridData[dataItem]) {
            case "string":
                if (dataItem == "ProductGroup") {
                    columns.push({
                        field: dataItem,
                        title: colTitle,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: center; font-weight: bold" },
                        groupHeaderTemplate: "#= value #",
                        visible: true,
                        hidden: true

                    });
                }
                else if (dataItem == "PackageGroup") {
                    columns.push({
                        field: dataItem,
                        title: colTitle,
                        width: 160,
                        locked: true,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: left; font-weight:bold; font-size:small;" },
                        footerAttributes: { style: "text-align: left; font-size:small;" },
                        groupFooterTemplate: "Total : ",
                        footerTemplate: "Grand Total : "
                    });
                }

                else if (dataItem == "Connector") {
                    columns.push({
                        field: dataItem.trim(),
                        title: colTitle,
                        width: 200
                    });
                }

                else {
                    columns.push({
                        field: dataItem,
                        title: colTitle,
                        width: 100,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: center; font-weight:bold; font-size:small;" }
                    });
                }
                break;

            case "number":
            default:
                if (dataItem.includes('Target'))
                    columns.push({
                        field: dataItem,
                        title: "Target",
                        width: 100,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: center; font-weight:bold; font-size:small; color: gray" },
                        format: "{0:N0}",
                        aggregates: ["sum"],
                        footerAttributes: { style: "text-align: center; font-size:small; color: Blue" },
                        groupFooterTemplate: "#: kendo.format('{0:N0}', sum) #",
                        footerTemplate: "#: kendo.format('{0:N0}', sum) #"

                    });
                else {
                    columns.push({
                        field: dataItem,
                        title: colTitle,
                        width: 100,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: center; font-weight:bold; font-size:small;" },
                        //template: "#= workWeekDetails(data, '" + dataItem + "') #",
                        template: "<a class='assyOuts3995' ; workWeek='" + dataItem + "' href='\\#'>#= workWeekDetailsAssyOutput(data, '" + dataItem + "') #</a>",
                        aggregates: ["sum"],
                        footerAttributes: { style: "text-align: center; font-size:small; color: Blue" },
                        groupFooterTemplate: "#: kendo.format('{0:N0}', sum/1000) #",
                        footerTemplate: "#: kendo.format('{0:N0}', sum/1000) #"
                    });
                }
        }
    }

    return columns;

} // end of columns definition 



// end of AssyModel3001

function loadAssyDirtyflag3995(rowIndex, colIndex) {

    var gviewAssy = $("#gridAssyOutput3995").data("kendoGrid");

    gviewAssy.tbody.find("tr:eq(" + rowIndex + ") td:nth-child(" + colIndex + ")").addClass("note");

}


/////////////////////// ------------------------------------- AssyOuts6000 -----------------------------------------------------------------------------------------------////////////////////////////

function generateGridAssyOuts6000(gridData) {

    var model = generateAssyModel3995(gridData[0]);
    var columns = generateColumnsAssyOuts6000(gridData[0]);

    var colLenght = columns.length;

    var aggregates_ = generateAggregaAssyOuts3995(gridData[0]);

    var parseFunction;
    if (dateFields6000.length > 0) {
        parseFunction = function (response) {
            for (var i = 0; i < response.length; i++) {
                for (var fieldIndex = 0; fieldIndex < dateFields.length; fieldIndex++) {
                    var record = response[i];
                    record[dateFields[fieldIndex]] = kendo.parseDate(record[dateFields[fieldIndex]]);
                }
            }
            return response;
        };
    }

    var schema;
    if (parseFunction) {
        schema = {
            model: model,
            parse: parseFunction
        };
    }
    else {
        schema = {
            model: model
        };
    }

    $("#gridAssyOutput6000").kendoGrid({
        toolbar: ["excel"],
        dataSource: {
            data: gridData,
            schema: schema,
            pageSize: 100,
            group: { field: "ProductGroup", aggregates: aggregates_ },
            aggregate: aggregates_
        },
        pageable: {
            refresh: true,
            pageSizes: false,
            buttonCount: 5
        },
        columns: columns,
        sortable: true,
        resizable: true,
        selectable: "single cell",
        excelExport: function (e) {
            var timestamp = new Date().format("yyyyMMddHHmmss");


            var sheet = e.workbook.sheets[0];

            colLenght = colLenght - 1;

            for (var cellIndex = 2; cellIndex < colLenght; cellIndex++) {

                // row.cells[cellIndex].format = "[Blue]#,##0.0_);[Red](#,##0.0);0.0;"

                // var celVal =  sheet.rows[row].cells[cellIndex].value;

                var celVal = 0;

                var grandTotal = 0;

                for (var rowIndex = 2; rowIndex < sheet.rows.length; rowIndex++) {

                    var row = sheet.rows[rowIndex];

                    //  alert(row.type);

                    //  var celVal =  row.cells[cellIndex].value;

                    if (row.type == "data") {
                        var DataCell = row.cells[cellIndex];

                        // var dataCelVal = ;

                        if (typeof (row.cells[cellIndex]) === "undefined") {

                            alert("undefined");

                        }

                        else {



                            celVal = celVal + (row.cells[cellIndex].value);

                            DataCell.value = row.cells[cellIndex].value;


                        }



                    }

                    else if (row.type == "group-footer") {

                        var groupFooterCell = row.cells[cellIndex];

                        groupFooterCell.value = celVal;

                        grandTotal = grandTotal + row.cells[cellIndex].value;

                        celVal = 0;


                    }
                    else if (row.type == "footer") {

                        var footerCell = row.cells[cellIndex];

                        footerCell.value = grandTotal;

                        grandTotal = 0;
                    }


                }


            }



            e.workbook.fileName = "AssemblyOuts6000" + "_" + timestamp + ".xlsx";
        },

        dataBound: function () 
        {
            this.hideColumn("Connector");

            $.ajax({

                url: '/Grid/GetAssemblyID/',
                type: 'POST',
                data: JSON.stringify({ operation: "6000" }),
                datatype: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {

                    var gridassy = $("#gridAssyOutput6000").data("kendoGrid");

                    //  grid.hideColumn("Connector");

                    $.each(data, function (i, item) {

                        var colnameassy = item.ID.toString();

                        colnameassy = colnameassy.split("_").join(" ");

                        var colnameIndex1 = colnameassy.lastIndexOf("W");

                        var connectorID1 = colnameassy.substring(0, colnameIndex1 - 1).trim();

                        colnameassy = colnameassy.substring(colnameIndex1 - 1);

                        var colIndex1 = gridassy.thead.find("th[data-field='" + colnameassy + "']").index();

                        var dataItem1 = gridassy.dataSource.get(connectorID1);

                        if (typeof dataItem1 === "undefined") {

                            alert("ConnectorID : " + connectorID1 + " is undefined. Please contact your IT Administrator");

                        }

                        var rowIndex1 = gridassy.tbody.find("tr[data-uid='" + dataItem1.uid + "']").index();

                        loadAssyDirtyflag6000(rowIndex1, colIndex1 + 1);


                    });

                }  // end of success function

            }); //end of ajax request      


        }
    })

    $("#gridAssyOutput6000").data("kendoGrid").dataSource.read();

    var grid3995 = $("#gridAssyOutput6000").data("kendoGrid");

    grid3995.table.kendoTooltip({
        filter: ".assyOuts6000",
        width: 200,
        beforeShow: function (e) {


            if ($("#varAssyComments").val() === "" || $("#varAssyComments").val() === null) {
                // don't show the tooltip if the name attribute contains null
                e.preventDefault();

            }
        },
        content: function (e) {

            var r = $("#varAssyComments").val();

            return r;

            // ----- return function for getting remarks ------------

        },

        hide: function () {

            $("#varAssyComments").val(null);

        }


    });    //  end of kendo tooltip
}


function generateColumnsAssyOuts6000(gridData) {

    // initiate the column array
    var columns = [];

    // iterate all of the data items in the first element of the returned JSON data
    for (var dataItem in gridData) {

        var colTitle = dataItem;

        // check column datatype

        switch (typeof gridData[dataItem]) {
            case "string":
                if (dataItem == "ProductGroup") {
                    columns.push({
                        field: dataItem,
                        title: colTitle,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: center; font-weight: bold" },
                        groupHeaderTemplate: "#= value #",
                        visible: true,
                        hidden: true

                    });
                }
                else if (dataItem == "PackageGroup") {
                    columns.push({
                        field: dataItem,
                        title: colTitle,
                        width: 160,
                        locked: true,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: left; font-weight:bold; font-size:small;" },
                        footerAttributes: { style: "text-align: left; font-size:small;" },
                        groupFooterTemplate: "Total : ",
                        footerTemplate: "Grand Total : "
                    });
                }

                else if (dataItem == "Connector") {
                    columns.push({
                        field: dataItem.trim(),
                        title: colTitle,
                        width: 200
                    });
                }

                else {
                    columns.push({
                        field: dataItem,
                        title: colTitle,
                        width: 100,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: center; font-weight:bold; font-size:small;" }
                    });
                }
                break;

            case "number":
            default:
                if (dataItem.includes('Target'))
                    columns.push({
                        field: dataItem,
                        title: "Target",
                        width: 100,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: center; font-weight:bold; font-size:small; color: gray" },
                        format: "{0:N0}",
                        aggregates: ["sum"],
                        footerAttributes: { style: "text-align: center; font-size:small; color: Blue" },
                        groupFooterTemplate: "#: kendo.format('{0:N0}', sum) #",
                        footerTemplate: "#: kendo.format('{0:N0}', sum) #"

                    });
                else {
                    columns.push({
                        field: dataItem,
                        title: colTitle,
                        width: 100,
                        headerAttributes: { style: "text-align: center; font-weight: bold" },
                        attributes: { style: "text-align: center; font-weight:bold; font-size:small;" },
                        //template: "#= workWeekDetails(data, '" + dataItem + "') #",
                        template: "<a class='assyOuts6000' ; workWeek='" + dataItem + "' href='\\#'>#= workWeekDetailsAssyOutput(data, '" + dataItem + "') #</a>",
                        aggregates: ["sum"],
                        footerAttributes: { style: "text-align: center; font-size:small; color: Blue" },
                        groupFooterTemplate: "#: kendo.format('{0:N0}', sum/1000) #",
                        footerTemplate: "#: kendo.format('{0:N0}', sum/1000) #"
                    });
                }
        }
    }

    return columns;

} // end of columns definition Assy 6000

function loadAssyDirtyflag6000(rowIndex, colIndex) {

    var gviewAssy = $("#gridAssyOutput6000").data("kendoGrid");

    gviewAssy.tbody.find("tr:eq(" + rowIndex + ") td:nth-child(" + colIndex + ")").addClass("note");

}


//------------------------------------------------------------- Model and Aggregates Config --------------------------------------------------------------------------


function generateAggregaAssyOuts(gridData) {

    var aggregates = [];

    // iterate all of the data items in the first element of the returned JSON data
    for (var dataItem in gridData) {

        // check column datatype
        switch (typeof gridData[dataItem]) {
            case "string":
                aggregates.push({
                    field: dataItem,
                    aggregate: "count"
                });
                break;
            case "number":
            default:
                if (dataItem.includes('Target'))
                    aggregates.push({
                        field: dataItem,
                        aggregate: "sum"
                    });
                else {
                    aggregates.push({
                        field: dataItem,
                        aggregate: "sum"
                    });
                }
        }
    }
    return aggregates;
}

                   

                  






function generateAggregaAssyOuts3995(gridData) {

    var aggregates = [];

    // iterate all of the data items in the first element of the returned JSON data
    for (var dataItem in gridData) {

        // check column datatype
        switch (typeof gridData[dataItem]) {
            case "string":
                aggregates.push({
                    field: dataItem,
                    aggregate: "count"
                });
                break;
            case "number":
            default:
                if (dataItem.includes('Target'))
                    aggregates.push({
                        field: dataItem,
                        aggregate: "sum"
                    });
                else {
                    aggregates.push({
                        field: dataItem,
                        aggregate: "sum"
                    });
                }
        }
    }
    return aggregates;
}


function generateAssyModel3995(gridData) {
    var model = {};
    model.id = "Connector";
    var fields = {};
    for (var property in gridData) {
        var propType = typeof gridData[property];

        if (propType == "number") {
            fields[property] = {
                type: "number",
                validation: {
                    required: true
                }
            };
        } else if (propType == "boolean") {
            fields[property] = {
                type: "boolean",
                validation: {
                    required: true
                }
            };
        } else if (propType == "string") {
            var parsedDate = kendo.parseDate(gridData[property]);
            if (parsedDate) {
                fields[property] = {
                    type: "date",
                    validation: {
                        required: true
                    }
                };
                dateFields.push(property);
            } else {
                fields[property] = {
                    validation: {
                        required: true
                    }
                };
            }
        } else {
            fields[property] = {
                validation: {
                    required: true
                }
            };
        }
    }
    model.fields = fields;

    return model;
}


//------- Start Tab4  -----------------------------------------------------------------------------------------

function onShowTab4(e) {
    kendoConsole.log("Shown: " + $(e.item).find("> .k-link").text());
}

function onSelectTab4(e) {

    // kendoConsole.log("Selected: " + $(e.item).find("> .k-link").text());

    var selectTab4 = $(e.item).find("> .k-link").text().toLowerCase().toString().trim();

    if (selectTab4 == "outs per operation") {
        $('#fontawesome7').css("color", "white");
        $('#tabOutsPerOperation1').css("color", "white");

        $('#fontawesome8').css("color", "#555");
        $('#tabShiftlyMovesToFG1').css("color", "#555");

        $('#fontawesome9').css("color", "#555");
        $('#tabEU1').css("color", "#555");



    }
    else if (selectTab4 == "shiftly moves to fg") {
        $('#fontawesome8').css("color", "white");
        $('#tabShiftlyMovesToFG1').css("color", "white");

        $('#fontawesome7').css("color", "#555");
        $('#tabOutsPerOperation1').css("color", "#555");

        $('#fontawesome9').css("color", "#555");
        $('#tabEU1').css("color", "#555");


    }

    else if (selectTab4 == "equipment utilization") {

        $('#fontawesome9').css("color", "white");
        $('#tabEU1').css("color", "white");

        $('#fontawesome7').css("color", "#555");
        $('#tabOutsPerOperation1').css("color", "#555");

        $('#fontawesome8').css("color", "#555");
        $('#tabShiftlyMovesToFG1').css("color", "#555");

    }

}

function onActivateTab4(e) {
    kendoConsole.log("Activated: " + $(e.item).find("> .k-link").text());
}

function onContentLoadTab4(e) {
    kendoConsole.log("Content loaded in <b>" + $(e.item).find("> .k-link").text() + "</b> and starts with <b>" + $(e.contentElement).text().substr(0, 20) + "...</b>");

}

function onErrorTab4(e) {
    kendoConsole.error("Loading failed with " + e.xhr.statusText + " " + e.xhr.status);
}

//------- End Tab4  -----------------------------------------------------------------------------------------