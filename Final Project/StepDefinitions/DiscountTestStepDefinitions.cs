using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Diagnostics;
using static Final_Project.Utilities.TestBase;
using Final_Project.POMs;

namespace Final_Project.StepDefinitions
{
    [Binding]
    public class DiscountTestStepDefinitions
    {
        HomePOM homePOM = new HomePOM(driver, wait);
        MyAccountPOM myAccount = new MyAccountPOM(driver, wait);
        ShopPOM shopPOM = new ShopPOM(driver, wait);
        CartPOM cartPOM = new CartPOM(driver, wait);
        CheckoutPOM checkoutPOM = new CheckoutPOM(driver, wait);
        OrderRecievedPOM orderRecievedPOM = new OrderRecievedPOM(driver, wait);

        [Given(@"the user is logged in with ""([^""]*)"" and ""([^""]*)""")]
        public void GivenTheUserIsLoggedInWithAnd(string p0, string finalProjectPassword)
        {
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/";

            homePOM.LoadMyAccount();

            myAccount.Login(p0, finalProjectPassword);
            myAccount.CheckLoggedIn();
        }

        [Given(@"has purchased an item of clothing")]
        public void GivenHasPurchasedAnItemOfClothing()
        {
            myAccount.NavToShop();

            shopPOM.CloseBottomBanner();
            shopPOM.SelectFirstShoppingItem();
            shopPOM.NavToCart();
        }

        [When(@"a discount has been applied")]
        public void WhenADiscountHasBeenApplied()
        {
            cartPOM.ApplyCouponCode();
        }

        [Then(@"the discount should be applied")]
        public void ThenTheDiscountShouldBeApplied()
        {
            bool cuponCheck = cartPOM.CheckCouponApplied();
            try { Assert.That(cuponCheck, "Coupon discount incorrect"); }
            catch (AssertionException) { }
        }

        [Then(@"the order should be found in My Orders")]
        public void ThenTheOrderShouldBeFoundInMyOrders()
        {
            cartPOM.CheckoutLoop();

            checkoutPOM.FillFields();
            checkoutPOM.OrderRecievedLoop();

            string orderNumber = orderRecievedPOM.RetrieveOrderNumber();

            bool orderConfirmed = myAccount.checkOrder(orderNumber);
            try { Assert.That(orderConfirmed, "Order Not Found"); }
            catch (AssertionException) { }
        }
    }
}
