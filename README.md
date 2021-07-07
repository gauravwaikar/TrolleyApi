# **TrolleyApi**

The API consists of three endpoints
* User : Gets the user details for the token
* Sort : Sorts the products in following order
o Price – High/Low
o Product Name : Ascending/Descending
o Product Popularity : Based on shopping history
* Trolleytotal : I have implemented the logic provided by api/resorce/trolleycalculator.. Following is the logic
o If the purchase list has specials then apply the special price on the purchase list items in the multiples of the special list item quantity. The remaining items will have the normal price

**Third Party Libraries**
Refit : It provides a type safe HttpClientFactory which is used to get the Products and Shopping hostory data.
**Improvements possible**

If I get more time, I would like to implement following improvements to the solution
* Implement logging and exception handling. The logs and exception can be analysed using Azure ApplicationInsight.
* Cache the results from the Product and ShoppingHistory api in a distributed cache like Redis so that performance can be improved.

Following snapshots shows that all the tests for the three exercises runs successfully.

![image](https://user-images.githubusercontent.com/85163069/124707670-7e01a180-df3c-11eb-9e5a-7e90ba1e7e43.png)
![image](https://user-images.githubusercontent.com/85163069/124707688-8528af80-df3c-11eb-81de-8e7c2d49cdf6.png)
![image](https://user-images.githubusercontent.com/85163069/124707707-8a85fa00-df3c-11eb-9785-7f870495bd73.png)
![image](https://user-images.githubusercontent.com/85163069/124707717-8fe34480-df3c-11eb-8086-fdb13e845a29.png)

