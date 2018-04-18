// Write your JavaScript code.
$(document).ready(function(){
         $("#search").keyup(function(e){
             
            e.preventDefault();

            console.log(this.value)
            
        console.log(e);
        console.log($(e.currentTarget.form).serialize());


        $.ajax({
            url: "/search-name", /* Where should this go? */
            method: 'post', /* Which HTTP verb? */
            data: $(e.currentTarget.form).serialize(), /* Any data to send along? */
            success: function(res) { /* What code should we run when the server responds? */
                //$('#placeholder3').html(serverResponse)
                console.log(res);

                var strRow = `
                            <tr>
                                <th>Customer Name</th>
                                <th>Created Date</th>
                                <th>Actions</th>
                            </tr>
                            `
                res.forEach(e => {
                    strRow += `
                            <tr>
                                <form action="/remove-customer" method="POST">
                                    <input type="hidden" name="customer_id" value="${e.customerid}">
                                    <td>${e["name"]}</td>
                                    <td>${e["displayDate"]}</td>
                                    <td><input type="submit" value="Remove"></td>
                                </form>
                            </tr>
                            `
                });
                $("#clist").html(strRow);
            }
        });

        });
    });