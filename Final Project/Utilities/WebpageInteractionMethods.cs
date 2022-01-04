using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Final_Project.POMs
{
    public class WebpageInteractionMethods
    {
        public void waitUntilDisplayed(WebDriverWait wait, By element) { wait.Until(drv => drv.FindElement(element).Displayed); }
        public void clearElement(IWebDriver driver, By element) { driver.FindElement(element).Clear(); }
        public void sendKeys(IWebDriver driver, By element, string input) { driver.FindElement(element).SendKeys(input); }
        public void clickElement(IWebDriver driver, By element) { driver.FindElement(element).Click(); }
        public int getElementCount(IWebDriver driver, By element) { return driver.FindElements(element).Count(); }
        public string getElementTextContent(IWebDriver driver, By element) { return driver.FindElement(element).GetAttribute("textContent"); }
        public bool ClickElementValidationLoop(IWebDriver driver, WebDriverWait wait, By element, string targetURL)
        {
            int loopCount = 0;
            bool loadingStatus = true;
            while (!driver.Url.Contains(targetURL))
            {
                if (loopCount >= 10 && !driver.Url.Contains(targetURL)) { loadingStatus = false; break; }

                wait.Until(drv =>
                {
                    if (driver.Url.Contains(targetURL)) { return true; }

                    try
                    {
                        clickElement(driver, element);
                        return true;
                    }
                    catch (StaleElementReferenceException) { return false; }
                    catch (NoSuchElementException) { return false; }
                    catch (ElementClickInterceptedException) { return false; }
                });

                loopCount++;
            }

            return loadingStatus;
        }
    }
}
