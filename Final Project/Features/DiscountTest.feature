Feature: DiscountTest
Login to website as registered user, purchase item of clothing, apply discount code, check total is correct-
-after discount and shipping. If correct, place order, then go to My Orders and check order number is present.

Background: The user is logged in

@DiscountTests
Scenario: Apply discount to purchase
	Given the user is logged in with <username> and <password>
	And has purchased an item of clothing
	When a discount has been applied
	Then the discount should be applied
	And the order should be found in My Orders

Examples: 
| username                      | password               |
| "connor.dawkins@nfocus.co.uk" | "finalProjectPassword" |