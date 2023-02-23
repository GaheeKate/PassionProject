# PassionProject
CMS-based project using ASP.NET MVC with Entity framework
CMS System for Managing Sales in Burger Chains

## Tables
There are three tables in my system: Order, Burger, and Location. Each order can have many burgers, and each burger can be in many orders.

## API Demonstration
### Order List
The Order list displays the Order ID, Store ID, and Order date, as well as buttons for editing details and deleting the order. For clarity, I set it to show a maximum of three rows per page.

If you click on the details button for a specific order, you will see a list of the burgers that were ordered, the quantity of each burger, and the total sales for that store on that day. You can even change the quantity of each burger by clicking on the plus or minus button, and the updated total sales will be displayed below.

You can also click on the Burger ID to see the details of that specific burger.

Creating New Orders
On the Order page, you can create new orders by setting the Store ID and Order date. If you go to the last page, you can see the order that you just created. Clicking on the details button will show you the list of all burgers that were added to the order. You can modify the quantity of the sold burgers, and if you put the wrong Store ID or date, you can update it from this page or delete the order completely.

### Burger List
The Burger list table has the same layout as the Order page. If you click on the Burger details, you will see the recent order list for that specific burger, as well as the burger name and price. On this page, users can update the burger name or price, or delete the burger.

To create a new burger, click on the Create New button and set the name and price for the burger. Once you have done so, the burger will be successfully added. you can delete it by clicking on the Confirm button.

### Location Table
User can see a list of locations in the Location table, and by clicking on them, user can see more details about each location. User can also manipulate the information by using the Update or Delete button.
