let total = document.getElementsByClassName("total");
let countHeader = document.getElementById("ProNo");
function Remove(item, productCount, productId) {
    item.parentNode.parentNode.parentNode.removeChild(item.parentNode.parentNode);
    countHeader.innerHTML = `Cart - ${productCount - 1} items`;
    CallAction(productId);
}
function CallAction(id) {
    $.ajax({
        url:"/Cart/Remove",
        data: { "id": id },
        success: function (result) {
            changeTotalInPage(result);
        }
    });
}


function changeQuantity(input, price, index) {
    input.parentNode.parentNode.parentNode.childNodes[9].innerHTML = `<strong>$${input.value * price}</strong>`;
    
    CallEditTotalPrice(index, input.value);
}
function CallEditTotalPrice(index, quantity) {
    $.ajax({
        url: "/Cart/EditTotalPrice",
        data: { "index": index, "quantity": quantity },
        success: function (result) {
            changeTotalInPage(result);
        }
    });
}
function changeTotalInPage(result) {
    total[0].innerHTML = "$" + result;
    total[1].innerHTML = "$" + result;
}