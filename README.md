# Order-Process

Orders CRUD Application
By: Robert Sorkin
 
Table of Contents

Introduction

Starting Screen

Create New Order

Edit Order

Delete Order

 
## Introduction

This web application displays and manages new and existing orders

The values in the table on the starting screen go as follow:

-	ID

o	The specific ID of that Order


-	Customer ID

o	ID of the customer who placed the order


-	Update Time

o	Time of most recent create/update/delete of that order 


## Starting Screen

 
The starting screen displays all orders already in the system. Pressing the “Select” button selects the row you click and enables the “Edit Order” and “Delete order” buttons to edit and the delete that order respectively.
To add an order to the database, click the “Create new order” button.
To edit, select an entry in the display grid. Once an order is selected, the “Edit order” button becomes available to press.
To delete, select an entry in the display grid. Once an order is selected, the “Delete order” button becomes available to press.
 
## Create New Order


The values in the table on the “Create new order” screen go as follow:


-	ID

o	The number of the order line


-	F_Code

o	Item code


-	Description

o	Description of that order line


-	Quantity_Per

o	Amount of item per container


-	# of Cont

o	Number of containers ordered


-	Cont_Type

o	Type of container ordered


-	Total_RQ

o	Total amount of the item ordered


Once “Create new order” is pressed on the starting screen, a window will popup to create an order. Once you have filled everything in all required fields at the top of the page, press “Add” to add the item to the order. After doing so, the order will appear as the next row in the table and a delete button to its right along with it. Pressing delete will delete that specific item from the order. Once all the necessary items have been added, add the customer’s name in “Operator” and press “Submit” to add the order to the database.
 

## Edit Order

Select the order you wish to edit, then press "Edit." The form would then switch to the order line form with all previous order lines of that order filled out. Here you can add, edit, and delete order lines


## Delete Order

Once an order is selected on the starting screen, pressing the “Delete order” button will remove that order from the database. 
Delete Order.
