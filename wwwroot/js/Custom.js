let index = 0;

function AddTag() {
    // Get a reference to the TagEntry input element
    var tagEntry = document.getElementById("TagEntry");

    let searchResult = search(tagEntry.value);
    if (searchResult != null) {
        // Trigger SweetAlert for the error
        swalWithDarkButton.fire({
            html: `<span class='font-weight-bolder'>${searchResult.toUpperCase()}</span>`
        });
    } else {
        // Create a new Select Option
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;
    }

    // Clear out the TagEntry control
    tagEntry.value = "";

    return true;
}

function DeleteTag() {
    let tagCount = 1;
    let tagList = document.getElementById("TagList");
    if (!tagList) return false;

    if (tagList.selectedIndex == -1) {
        swalWithDarkButton.fire({
            html: "<span class='font-weight-bolder'>CHOOSE A TAG BEFORE DELETING</span>"
        });
        return true;
    }

    while (tagCount > 0) {
        let selectedIndex = tagList.selectedIndex;
        if (selectedIndex >= 0) {
            tagList.options[selectedIndex] = null;
            --tagCount;
        } else {
            tagCount = 0;
        }
        index--;
    }
}

$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
})

// For 'Edit' View: If there are tagValues, populate the Tag Select List with these tags
if (tagValues != '') {
    let tagArray = tagValues.split(",");
    for (let i = 0; i < tagArray.length; i++) {
        let tagText = tagArray[i];
        let newOption = new Option(tagText, tagText);
        document.getElementById("TagList").options[index++] = newOption;
    }
}

// Detect either an empty Tag, or a duplicate Tag on the same Post
//  and return an error string if an error is detected
function search(str) {

    if (str == "") {
        return 'Empty tags are not permitted';
    }

    var tagsEl = document.getElementById('TagList');
    if (tagsEl) {
        let options = tagsEl.options;
        for (let i = 0; i < options.length; i++) {
            if (options[i].value == str) {
                return `The Tag #${str} was detected as a duplicate and not permitted`;
            }
        }
    }
}

const swalWithDarkButton = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-danger btn-sm btn-block btn-outline-dark'
    },
    imageUrl: '/images/oops.png',
    timer: 3000,
    buttonStyling: false
})