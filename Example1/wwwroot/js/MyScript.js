function confirmDelete(Id, setClick) {
    var confirmDelete = 'DeleteSpan_' + Id;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + Id;

    if (setClick) {
        $('#' + confirmDelete).hide();
        $('#' + confirmDeleteSpan).show();
    } else {
        $('#' + confirmDelete).show();
        $('#' + confirmDelete).hide();
    }
}