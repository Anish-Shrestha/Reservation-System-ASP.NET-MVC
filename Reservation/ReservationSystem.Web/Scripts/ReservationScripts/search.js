// display loading while ajax request
$(document).ajaxStart(function () {
    $('#loading').show();
}).ajaxStop(function () {
    $('#loading').hide();
});

$(document).ready(function () {

    $('#loading').hide(); // hide loading at first.

    // Client side validation  
    var $formvalidator = $("#form").validate({
        onkeyup: false,
        invalidHandler: function (event, validator) {
            $(".validation-summary-errors").html("");
        },
        rules: {
            "Location": {
                required: true,
                minlength: 1,
                citiesAPI: true, // custom function to validate location selected from api
                maxlength: 100
            },
            "CheckIn": {
                required: true,
                date: true,
                dateGTtoday: true // custom function to validate date greater than today
            },
            "CheckOut": {
                required: true,
                date: true,
                dateGTtoday: true,
                checkinFirst: true // custom function to validate check-in date must be less than check-out date
            }
        },
        messages: {
            "Location": {
                required: "* Location field is required.",
                minlength: "* Location is not valid.",
                maxlength: "* Location is not valid.",

            },
            "CheckIn": {
                required: "* Checkin Date field required.",
                dateGTtoday: "* Checkin date should not be less than today's date"

            },
            "CheckOut": {
                required: "* CheckOut Date field required.",
                dateGTtoday: "* CheckOut date should not be less than today's date"
            }


        },
        errorElement: 'p',
        errorLabelContainer: '.validation'
    });

    // custom function to validate check-in date must be less than check-out date
    jQuery.validator.addMethod("checkinFirst", function (value, element) {
        var checkin = new Date($("#CheckIn").val());
        var CheckOut = new Date($("#CheckOut").val())
        if (checkin > CheckOut) {
            return false;
        }

        return true;
    }, "* Check-in date must be less than Check-out date");

    // custom function to validate date greater than today
    jQuery.validator.addMethod("dateGTtoday", function (value, element) {
        var dateNow = new Date();
        if (new Date(value) < new Date(dateNow.getMonth() + "/" + dateNow.getDate() + "/" + dateNow.getFullYear())) {
            return false;
        }
        return true;
    }, "* Please select date greater than today");

    // custom function to validate location selected from api
    jQuery.validator.addMethod("citiesAPI", function (value, element) {
        var isSuccess = false;
        $('#loading').show();
        $.ajax({
            url: "https://api.teleport.org/api/cities/?search=" + value,
            async: false,
            type: 'GET',
            success:
                function (cities) {
                    var objectList = cities._embedded["city:search-results"];
                    if (jQuery.isEmptyObject(objectList)) return false;
                    var cities = $.map(objectList, function (city) {
                        if (city.matching_alternate_names[0])
                            return {
                                value: city.matching_alternate_names[0].name
                            };
                    });
                    if (cities == 'undefined' || cities == null) return false;
                    if (cities[0].value.toLowerCase() == value.toLowerCase()) {
                        isSuccess = true;
                    }
                    $('#loading').hide();
                }
        });
        return isSuccess;
    }, "* Please select location suggested");

    /*
        Below code is to maintain the value of the form, if there
        is validation error in server side, we need to maintain value
        of form element.
    */
    if (isValidationError) {
        var adultList = roomsAdultList.slice(0, -1).split(",");
        var childrenList = roomsChildrenList.slice(0, -1).split(",");
        for (var li in adultList) {
            var roomCount = parseInt(li) + 1;
            if (li > 0) {
                var panel = $("#SearchPanel").clone().addClass("clonedClass");
                panel.find("#Adult").val(adultList[li]);
                panel.find("#Children").val(childrenList[li]);
                panel.find("#RoomId").text("Room " + roomCount);
                panel.appendTo("#reservation");
            }
            else if (li == 0) {

                $("#SearchPanel").find("#RoomId").text("Room " + roomCount);
                $("#SearchPanel").find("#Adult").val(adultList[li]);
                $("#SearchPanel").find("#Children").val(childrenList[li]);
            }
        }
    }

    /*
        Below code is to dynamically generate the ReservationDetail panel element
        consisting of Room number, Adult, and Children, When the dropdown list of 
        number of room is changed.
    */
    $("#SelectRoomDropdown").change(function () {
        var roomToBook = $("#SelectRoomDropdown").val();
        var numberOfClonedClass = $(".clonedClass").length + 1;
        if (numberOfClonedClass > roomToBook) {
            $(".clonedClass").each(function (index) {
                if (index + 2 > roomToBook) {
                    $(this).remove();
                }
            });
            return false;
        };

        for (var index = 0; index < roomToBook - numberOfClonedClass; index++) {
            var panel = $("#SearchPanel").clone().addClass("clonedClass");
            panel.find("#Adult").val(1);
            panel.find("#Children").val(1);
            panel.find("#RoomId").text("Room " + parseInt(index + numberOfClonedClass + 1))
            panel.appendTo("#reservation");
        }
    });

    /*
        Below code is for datepicker in Check-in and Check-out and its validation.
        It uses bootstrap datepicker.
    */
    var yesterday = new Date(new Date().setDate(new Date().getDate() - 2));
    $('#CheckIn').datepicker('setStartDate', yesterday);
    $('#CheckIn').datepicker().on('changeDate', function (ev) {
        $('#CheckIn').datepicker('hide');
    });
    $('#CheckOut').datepicker('setStartDate', yesterday);
    $('#CheckOut').datepicker().on('changeDate', function (ev) {
        $('#CheckOut').datepicker('hide');
    });

    /*
        Below code is for autocomplete location. It uses typeheadJs
    */
    var cities = new Bloodhound({
        datumTokenizer: function (datum) {
            return Bloodhound.tokenizers.whitespace(datum.value);
        },
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: 'https://api.teleport.org/api/cities/?search=%QUERY',
            filter: function (cities) {
                var objectList = cities._embedded["city:search-results"];
                return $.map(objectList, function (city) {
                    if (city.matching_alternate_names[0])
                        return {
                            value: city.matching_alternate_names[0].name
                        };
                });
            }
        }
    });

    cities.initialize();

    $('.typeahead').typeahead(null, {
        displayKey: 'value',
        source: cities.ttAdapter()
    });

    /*
        Below code is for displaying success message.
    */
    if (!isEmpty(success))
        $("#SuccessMessageLabl").addClass("alert-success").html(success);
    else
        $("#SuccessMessageLabl").removeClass("alert-success");

    // Helper Methods
    function isEmpty(value) {
        return typeof value == 'string' && !value.trim() || typeof value == 'undefined' || value === null;
    }

});