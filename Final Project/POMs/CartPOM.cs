using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Final_Project.POMs
{
    public class CartPOM
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public CartPOM(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public string url = "https://www.edgewordstraining.co.uk/demo-site/cart/";

        //Apply Coupon Code
        public IWebElement couponInput;
        public IWebElement couponButton;
        public IWebElement removeCouponButton;

        //CheckCouponApplied
        public string subtotal;
        public string discount;
        public string shipping;
        public string total;
        public decimal discountPercenrage = 0.15m;

        public void ApplyCouponCode()
        {
            wait.Until(drv => drv.FindElement(By.Id("coupon_code")).Displayed);
            couponInput = driver.FindElement(By.Id("coupon_code"));
            couponButton = driver.FindElement(By.CssSelector("button[name='apply_coupon']"));

            if (driver.FindElements(By.CssSelector(".woocommerce-remove-coupon")).Count() > 0)
            {
                removeCouponButton = driver.FindElement(By.CssSelector(".woocommerce-remove-coupon"));
                removeCouponButton.Click();
            }

            wait.Until(drv => couponInput.Displayed);
            couponInput.Clear();
            couponInput.SendKeys("edgewords");
            couponButton.Click();
        }

        public bool CheckCouponApplied()
        {
            wait.Until(drv => drv.FindElement(By.CssSelector(".alt.button.checkout-button.wc-forward")).Displayed);
            wait.Until(drv => drv.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount")).Displayed);
            subtotal = driver.FindElement(By.XPath("/html//article[@id='post-5']/div[@class='entry-content']/div[@class='woocommerce']//table[@class='shop_table shop_table_responsive']//tr[@class='cart-subtotal']/td/span")).GetAttribute("textContent");
            total = driver.FindElement(By.XPath("/html//article[@id='post-5']/div[@class='entry-content']/div[@class='woocommerce']//table[@class='shop_table shop_table_responsive']//strong/span")).GetAttribute("textContent");
            discount = driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount")).GetAttribute("textContent");
            shipping = driver.FindElement(By.XPath("./html//article[@id='post-5']/div[@class='entry-content']/div[@class='woocommerce']//table[@class='shop_table shop_table_responsive']//tr[@class='shipping']/td/span")).GetAttribute("textContent");

            decimal subtotalShipping = Convert.ToDecimal(subtotal.Remove(0, 1));
            decimal dblTotal = Convert.ToDecimal(total.Remove(0, 1));
            decimal dblDiscount = Math.Round(subtotalShipping * discountPercenrage, 2);
            subtotalShipping = subtotalShipping + Convert.ToDecimal(shipping.Remove(0, 1));

            if (dblTotal == (subtotalShipping - dblDiscount)) { return true; }
            else { return false; }
        }

        public void CheckoutLoop()
        {

            while (!driver.Url.Contains("checkout"))
            {
                wait.Until(drv =>
                {
                    if (driver.Url.Contains("checkout")) { return true; }

                    try
                    {
                        driver.FindElement(By.CssSelector(".alt.button.checkout-button.wc-forward")).Click();
                        return true;
                    }
                    catch (StaleElementReferenceException) { return false; }
                    catch (NoSuchElementException) { return false; }
                    catch (ElementClickInterceptedException) { return false; }
                });
            }
        }
    }
}
