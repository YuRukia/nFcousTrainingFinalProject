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
        public string url = "https://www.edgewordstraining.co.uk/demo-site/cart/";
        public By couponInput = By.Id("coupon_code");
        public By couponButton = By.CssSelector("button[name='apply_coupon']");
        public By removeCouponButton = By.CssSelector(".woocommerce-remove-coupon");
        public By checkoutButton = By.CssSelector(".alt.button.checkout-button.wc-forward");
        public By discountAmount = By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount");
        public By subtotal = By.XPath("/html//article[@id='post-5']/div[@class='entry-content']/div[@class='woocommerce']//table[@class='shop_table shop_table_responsive']//tr[@class='cart-subtotal']/td/span");
        public By shipping = By.XPath("./html//article[@id='post-5']/div[@class='entry-content']/div[@class='woocommerce']//table[@class='shop_table shop_table_responsive']//tr[@class='shipping']/td/span");
        public By total = By.XPath("/html//article[@id='post-5']/div[@class='entry-content']/div[@class='woocommerce']//table[@class='shop_table shop_table_responsive']//strong/span");
    }
}
