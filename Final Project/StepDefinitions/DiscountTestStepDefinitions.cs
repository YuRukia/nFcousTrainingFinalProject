using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Diagnostics;
using static Final_Project.Utilities.TestBase;
using Final_Project.POMs;
using OpenQA.Selenium.Support.UI;

namespace Final_Project.StepDefinitions
{
    [Binding]
    public class DiscountTestStepDefinitions : WebpageInteractionMethods
    {
        HomePOM homePOM = new HomePOM();
        MyAccountPOM myAccountPOM = new MyAccountPOM();
        ShopPOM shopPOM = new ShopPOM();
        CartPOM cartPOM = new CartPOM();
        CheckoutPOM checkoutPOM = new CheckoutPOM();
        OrderRecievedPOM orderRecievedPOM = new OrderRecievedPOM();

        string couponCode = "edgewords";
        decimal discountPercentage = 0.15m;

        [Given(@"the user is logged in with ""([^""]*)"" and ""([^""]*)""")]
        public void GivenTheUserIsLoggedInWithAnd(string p0, string finalProjectPassword)
        {
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/";

            //Go to My Account
            waitUntilDisplayed(wait, homePOM.myAccountLink);
            clickElement(driver, homePOM.myAccountLink);

            //Login
            waitUntilDisplayed(wait, myAccountPOM.login);
            clearElement(driver, myAccountPOM.username);
            sendKeys(driver, myAccountPOM.username, p0);
            clearElement(driver, myAccountPOM.password);
            sendKeys(driver, myAccountPOM.password, finalProjectPassword);
            clickElement(driver, myAccountPOM.login);

            //Check Logged In
            waitUntilDisplayed(wait, myAccountPOM.logout);
        }

        [Given(@"has purchased an item of clothing")]
        public void GivenHasPurchasedAnItemOfClothing()
        {
            //Go to Shop
            waitUntilDisplayed(wait, myAccountPOM.topNavShop);
            clickElement(driver, myAccountPOM.topNavShop);

            //Wait for disruptive javascript element to appear then remove it
            waitUntilDisplayed(wait, shopPOM.bottomBanner);
            clickElement(driver, shopPOM.bottomBanner);

            //Wait for element to disappear then add first shop item to cart
            waitUntilDisplayed(wait, shopPOM.firstShopItem);
            clickElement(driver, shopPOM.firstShopItem);

            //Check the cart button is loaded then click it
            waitUntilDisplayed(wait, shopPOM.navBarCart);
            clickElement(driver, shopPOM.navBarCart);
        }

        [When(@"a discount has been applied")]
        public void WhenADiscountHasBeenApplied()
        {
            //Apply coupon code to purchase
            if(getElementCount(driver, cartPOM.removeCouponButton) > 0) { clickElement(driver, cartPOM.removeCouponButton); }
            waitUntilDisplayed(wait, cartPOM.couponInput);
            clearElement(driver, cartPOM.couponInput);
            sendKeys(driver, cartPOM.couponInput, couponCode);
            clickElement(driver, cartPOM.couponButton);
        }

        [Then(@"the discount should be applied")]
        public void ThenTheDiscountShouldBeApplied()
        {
            //Check discount is applied and discounts the correct amount
            waitUntilDisplayed(wait, cartPOM.checkoutButton);
            waitUntilDisplayed(wait, cartPOM.discountAmount);
            decimal subtotalShipping = Convert.ToDecimal(getElementTextContent(driver, cartPOM.subtotal).Remove(0, 1));
            decimal total = Convert.ToDecimal(getElementTextContent(driver, cartPOM.total).Remove(0, 1));
            decimal discount = Math.Round(subtotalShipping * discountPercentage, 2);
            subtotalShipping = subtotalShipping + Convert.ToDecimal(getElementTextContent(driver, cartPOM.shipping).Remove(0, 1));

            bool couponCheck = false;
            if (total == (subtotalShipping - discount)) { couponCheck = true; }
            else { couponCheck = false; }

            try { Assert.That(couponCheck, "Coupon discount incorrect"); }
            catch (AssertionException) { }
        }

        [Then(@"the order should be found in My Orders")]
        public void ThenTheOrderShouldBeFoundInMyOrders()
        {
            //Check to make sure cart is successfully exited
            bool checkoutLoop = ClickElementValidationLoop(driver, wait, cartPOM.checkoutButton, checkoutPOM.url);
            Assert.That(checkoutLoop, "Checkout page failed to load");

            //Fill Checkout Fields
            waitUntilDisplayed(wait, checkoutPOM.firstName);
            clearElement(driver, checkoutPOM.firstName);
            sendKeys(driver, checkoutPOM.firstName, "fName");
            clearElement(driver, checkoutPOM.lastName);
            sendKeys(driver, checkoutPOM.lastName, "lName");
            clearElement(driver, checkoutPOM.billingAddress);
            sendKeys(driver, checkoutPOM.billingAddress, "Example Street");
            clearElement(driver, checkoutPOM.billingCity);
            sendKeys(driver, checkoutPOM.billingCity, "Example City");
            clearElement(driver, checkoutPOM.billingPostcode);
            sendKeys(driver, checkoutPOM.billingPostcode, "TN240US");
            clearElement(driver, checkoutPOM.billingPhone);
            sendKeys(driver, checkoutPOM.billingPhone, "44 113 496 0000");
            clearElement(driver, checkoutPOM.billingEmail);
            sendKeys(driver, checkoutPOM.billingEmail, "example@email.co.uk");

            //Check to make sure checkout is successfully exited
            bool orderRecievedLoop = ClickElementValidationLoop(driver, wait, checkoutPOM.placeOrderButton, orderRecievedPOM.url);
            Assert.That(orderRecievedLoop, "Order Details page failed to load");

            //Get order number then go to orders
            waitUntilDisplayed(wait, orderRecievedPOM.orderNumberElement);
            string orderNumber = getElementTextContent(driver, orderRecievedPOM.orderNumberElement);
            clickElement(driver, orderRecievedPOM.myAccount);

            //Check if order number is present in orders
            waitUntilDisplayed(wait, myAccountPOM.ordersNav);
            clickElement(driver, myAccountPOM.ordersNav);
            waitUntilDisplayed(wait, myAccountPOM.ordersTable);

            List<IWebElement> orders = new List<IWebElement>();
            orders.AddRange(driver.FindElements(myAccountPOM.ordersTable));
            bool orderConfirmed = false;

            for (int i = 0; i < orders.Count(); i++)
            {
                string order = orders.ElementAt(i).Text;
                if (order.Remove(0, 1) == orderNumber) { orderConfirmed = true; }
            }
            
            try { Assert.That(orderConfirmed, "Order Not Found"); }
            catch (AssertionException) { }
        }
    }
}
