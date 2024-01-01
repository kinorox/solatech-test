$(document).ready(function () {
    $("#addressInput").on("input", function () {
        var searchTerm = $(this).val();

        $.ajax({
            type: "POST",
            url: "/Address/SearchAddresses",
            data: { address: searchTerm },
            success: function (data) {
                displayAddresses(data.Results);
            }
        });
    });

    $("#addressInput").on("focusout", function () {
        $("#addressList").hide();
    });

    $(".dropdown-item").on("click", function () {
        var address = $(this).val();

        $("#addressInput").val(address);
    });

    $("#zipCode").on("input", function () {
        var zipCodeVal = $(this).val();

        $.ajax({
            type: "POST",
            url: "/Address/SearchByZipCode",
            data: { zipCode: zipCodeVal },
            success: function (data) {

                if (!data) {
                    $("#zipCode").addClass("is-invalid");
                    $("#zipCode").removeClass("is-valid");
                } else {
                    $("#zipCode").addClass("is-valid");
                    $("#zipCode").removeClass("is-invalid");
                }

                $("#city").val(data.City);
                $("#country").val(data.Country);
                $("#state").val(data.State);
                $("#neighborhood").val(data.Neighborhood);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('Error:', textStatus, errorThrown);
            }
        });
    });

    $("#validateAddressInput").on("input", function () {
        var searchTerm = $(this).val();
        $(this).removeClass("is-invalid is-valid");

        $.ajax({
            type: "POST",
            url: "/Address/Validate",
            data: { address: searchTerm },
            success: function (data) {
                if (data == true) {
                    $("#validateAddressInput").addClass("is-valid");
                    $("#validateAddressInput").removeClass("is-invalid");
                } else {
                    $("#validateAddressInput").addClass("is-invalid");
                    $("#validateAddressInput").removeClass("is-valid");
                }
            }
        });
    });
});

function displayAddresses(addresses) {
    var addressList = $("#addressDropdown .dropdown-menu");
    addressList.empty();

    if (addresses.length > 0) {
        addresses.forEach(function (address) {
            var listItem = $("<li class='dropdown-item'></li>").text(address.formatted_address);
            addressList.append(listItem);
        });
    } else {
        var listItem = $("<li class='dropdown-item'></li>").text("No results found");
        addressList.append(listItem);
    }

    $("#addressList").show();
}