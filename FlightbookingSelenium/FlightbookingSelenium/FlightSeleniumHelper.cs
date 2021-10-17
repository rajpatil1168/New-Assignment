using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace FlightbookingSelenium
{
    
    class FlightSeleniumHelper
    {
        public static void OpenUrl(IWebDriver driver, string url)
        {
            try
            {
                driver.Navigate().GoToUrl(url);
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {

                Console.WriteLine("FAILURE::URL did not load/valid: " + Global.test_url);
                throw new TestException("URL did not load");
            }
        }

       public static void TravolookLogin(IWebDriver driver)
        {
            try
            {
                Thread.Sleep(2000);
                string email = "abhipatil1168@gmail.com";
                string password = "Ready2wrk@NVIDIA";
                //driver.FindElement(By.XPath("//*[@id='root']/div/div/header/div/div/div/div/div/div[2]/div[3]/div/button/div/p")).Click();
                ClickIfPresent(By.XPath("//*[@id='root']/div/div/header/div/div/div/div/div/div[2]/div[3]/div/button/div/p"), driver);
                ClickIfPresent(By.XPath("//*[@id='root']/div/div/header/div/div/div/div/div/div[2]/div[3]/div/div/ul/li/div[1]/button"), driver);


                driver.FindElement(By.XPath("//*[@data-testid='email']")).Clear();
                driver.FindElement(By.XPath("//*[@data-testid='email']")).SendKeys(email);
                driver.FindElement(By.XPath("//*[@data-testid='password']")).Clear();
                driver.FindElement(By.XPath("//*[@data-testid='password']")).SendKeys(password);


                string tempxpath = "//*[@class='bg-secondary-500 hover:bg-secondary-600 c-white bc-transparent c-pointer w-100p py-2 px-4 h-9 fs-4 fw-600 t-all button bs-solid tp-color td-500 bw-1 br-4 lh-solid box-border']";
                ClickIfPresent(By.XPath(tempxpath), driver);

                Thread.Sleep(1000);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
       
            }
            catch (Exception e)
            {
                Console.WriteLine("FAILURE: {0}:FlightLogin did not load.", e);

            }

        }
        public static bool IsElementPresent(By by, IWebDriver driver)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                throw new TestException("IsElementPresent did not load");
            }
        }

        public static void ClickIfPresent(By by, IWebDriver driver)
        {
            try
            {
                if (IsElementPresent(by, driver))
                {
                    //do if exists
                    driver.FindElement(by).Click();
                }
                else
                {
                    //do if does not exists
                    Console.WriteLine("NOT CLICKED {0} ", by);
                }
            }
            catch
            {
                throw new TestException("ClickIfPresent did not load");

            }
        }

        public static void GetInfo(out bool oneway, out bool roundtrip, out string fromCityName, out string toCityName, out DateTime departuredate, out DateTime returndate, out string nameclass, out UInt32 numberofAdults, out UInt32 numberofChildren, out UInt32 numberofInfants)
        {           

                Console.WriteLine("\n________________________________________________________________________________________________________________");
                string ch;
                string classnumber;
                do
                {
                    Console.WriteLine("Enter Which TripType you want: \nOneWay: Press 1\nRoundTrip: Press 2:");
                    ch = Console.ReadLine();
                } while (!(String.Equals(ch, "1") || String.Equals(ch, "2")));

                if (String.Equals(ch, "1"))
                {
                    oneway = true;
                    roundtrip = false;
                }
                else
                {
                    oneway = false;
                    roundtrip = true;
                }
                Console.WriteLine("\n________________________________________________________________________________________________________________");

                do
                {
                    Console.WriteLine("\n\nEnter From** which city/airport you want departure:[more than 3 character]");
                    fromCityName = Console.ReadLine();
                    fromCityName = new string(fromCityName.Where(c => Char.IsLetter(c) || Char.IsWhiteSpace(c)).ToArray());
                } while (fromCityName.Length < 3);
                Console.WriteLine("\n________________________________________________________________________________________________________________");

                do
                {
                    Console.WriteLine("\n\nEnter To** which city/airport you want departure:[more than 3 character]");
                    toCityName = Console.ReadLine();
                    toCityName = new string(toCityName.Where(c => Char.IsLetter(c) || Char.IsWhiteSpace(c)).ToArray());

                } while (toCityName.Length < 3);


                Console.WriteLine("\n________________________________________________________________________________________________________________");

                Console.Write("\nEnter DEPARTURE** date and time of flight (e.g. '{0}'  (24hr clock)):\n ", DateTime.Now);
                //DateTime inputtedDateTime = DateTime.Parse(Console.ReadLine());
                while (!(DateTime.TryParse(Console.ReadLine(), out departuredate) && departuredate.Date >= DateTime.Now))
                    Console.Write("The value must be in proper type:(e.g. 10/22/1987), try again:");
                Console.WriteLine(departuredate);
                returndate = DateTime.Today.AddDays(-1);
                Console.WriteLine("\n________________________________________________________________________________________________________________");

                if (roundtrip)
                {
                    Console.Write("\nEnter RETURN** date and time of flight (e.g. 10/22/1987) & must be after {0}: \n", departuredate.Date);
                    //DateTime inputtedDateTime = DateTime.Parse(Console.ReadLine());
                    while (!(DateTime.TryParse(Console.ReadLine(), out returndate) && departuredate < returndate))
                        Console.Write("The value must be in proper type:(e.g. 10/22/1987)& after{0}, try again:", departuredate);
                   Console.WriteLine(returndate);
                }

                Console.WriteLine("\n________________________________________________________________________________________________________________\n");


                do
                {
                    Console.Write("Enter Class number(1/2/3/4): 1.Economy     2.Premium Economy      3.Business      4.First\n ");
                    classnumber = Console.ReadLine();
                } while (!(String.Equals(classnumber, "1") || String.Equals(classnumber, "2") || String.Equals(classnumber, "3") || String.Equals(classnumber, "4")));
                if (String.Equals(classnumber, "1"))
                {
                    nameclass = "Economy";
                }
                else if (String.Equals(classnumber, "2"))
                {
                    nameclass = "Premium Economy";
                }
                else if (String.Equals(classnumber, "3"))
                {
                    nameclass = "Business";
                }
                else
                {
                    nameclass = "First";
                }
                Console.WriteLine("\n________________________________________________________________________________________________________________\n");

                Console.Write("Enter Number of ADULTS: \n ");
                while (!(UInt32.TryParse(Console.ReadLine(), out numberofAdults)&& numberofAdults<=9))
                    Console.Write("The value must be of positive integer type, try again:\n");

                Console.Write("Enter Number of Children: \n ");
                while (!(UInt32.TryParse(Console.ReadLine(), out numberofChildren) && (numberofAdults+ numberofChildren) <= 9))
                    Console.Write("The value must be of positive integer type, try again:\n");

                Console.Write("Enter Number of Infants: \n ");
                while (!(UInt32.TryParse(Console.ReadLine(), out numberofInfants) && numberofInfants <= numberofAdults))
                    Console.Write("The value must be of positive integer type, try again:\n");
                Console.WriteLine("\n________________________________________________________________________________________________________________");
                Console.WriteLine("\n________________________________________________________________________________________________________________\n");

        }

        public static void PrintVariables(bool oneway, bool roundtrip, string fromCityName, string toCityName, DateTime departuredate, DateTime returndate, string nameclass, UInt32 numberofAdults, UInt32 numberofChildren, UInt32 numberofInfants)
        {
            try
            {
                Console.WriteLine("\n__________________________________________PrintVariables_____________________________________________________");

                if (oneway)
                {
                    Console.WriteLine("\nTRIP DURATION/type                            : ONEWAY");
                }
                if (roundtrip)
                {
                    Console.WriteLine("\nTRIP DURATION/type                            : Roundtrip");
                }
                Console.WriteLine("\nTRIP -from-City                               : {0}", fromCityName);
                Console.WriteLine("\nTRIP -from-City                               : {0}", toCityName);
                Console.WriteLine("\nTRIP -departuredate                           : {0}", departuredate);
                if (roundtrip)
                {
                    Console.WriteLine("\nTRIP -returndate                              : {0}", returndate);
                }
                Console.WriteLine("\nTRIP -Nameclass                               : {0}", nameclass);
                Console.WriteLine("\nTRIP -numberofAdults                          : {0}", numberofAdults);
                Console.WriteLine("\nTRIP -numberofChildren                        : {0}", numberofChildren);
                Console.WriteLine("\nTRIP -numberofInfants                         : {0}", numberofInfants);
                Console.WriteLine();

                Console.WriteLine("\n________________________________________________________________________________________________________________\n");


            }
            catch
            {
                Console.WriteLine("Print FUNCTION ERROR!");
                Console.WriteLine("\n________________________________________________________________________________________________________________\n");

            }
        }


        public static void Searchflight(IWebDriver driver)
        {

            ClickIfPresent(By.Id("flying_from_N"), driver);

        }
        public static void Bookflight(IWebDriver driver)
        {
            string lowest = driver.FindElement(By.XPath("//*[@id='root']/div/main/div/div/div[2]/div[2]/div[8]/div[1]/div[1]/div[2]/div[3]/div[2]/p")).Text;
            driver.FindElement(By.XPath("//*[@id='root']/div/main/div/div/div[2]/div[2]/div[7]/div[1]/div[3]/a/svg")).Click();
            string highest = driver.FindElement(By.XPath("//*[@id='root']/div/main/div/div/div[2]/div[2]/div[8]/div[1]/div[1]/div[2]/div[3]/div[2]/p")).Text;
            driver.FindElement(By.XPath("//*[@id='root']/div/main/div/div/div[2]/div[2]/div[7]/div[1]/div[3]/a/svg")).Click();
            Console.WriteLine(highest + " " + lowest);
            driver.FindElement(By.XPath("//*[@id='root']/div/main/div/div/div[2]/div[2]/div[8]/div[1]/div[1]/div[2]/div[4]/button")).Click();

            //*[@id="root"]/div/div[2]/div/div[1]/main/div[2]/div/div/div[15]/div[1]/div/div[1]
            driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div[1]/main/div[2]/div/div/div[15]/div[1]/div/div[1]")).Click();

            //*[@id="root"]/div/div[2]/div/div[1]/main/div[2]/div/div/div[23]/div[1]/button
            driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div[1]/main/div[2]/div/div/div[23]/div[1]/button")).Click();

        }







        public static void InsertSearch(IWebDriver driver, bool oneway, bool roundtrip, string fromCityName, string toCityName, DateTime departuredate, DateTime returndate, string nameclass, UInt32 numberofAdults, UInt32 numberofChildren, UInt32 numberofInfants)
        {
            try {
                fromCityName = "BOM - Mumbai, IN ";
                toCityName = "CCU - Kolkata, IN ";
                Thread.Sleep(2000);
                if (oneway)
                {
                    driver.FindElement(By.XPath("//*[@id='root']/div/div/div[1]/div/div[2]/div/div[2]/label[1]/div[1]/span")).Click();
                }
                if (roundtrip)
                {
                    driver.FindElement(By.XPath("//*[@id='root']/div/div/div[1]/div/div[2]/div/div[2]/label[2]/div[1]/span")).Click();
                }
                int i = 0;

                driver.FindElement(By.XPath("//*[@id='root']/div/div/div[1]/div/div[2]/div/div[3]/div[1]/div[1]/div/div/div/input")).SendKeys(fromCityName);
                driver.FindElement(By.XPath("//*[@id='root']/div/div/div[1]/div/div[2]/div/div[3]/div[1]/div[1]/div/div/div[2]/ul/li[1]")).Click();
                Thread.Sleep(2000);
                Console.WriteLine(i++);

                driver.FindElement(By.XPath("//*[@id='root']/div/div/div[1]/div/div[2]/div/div[3]/div[1]/div[5]/div/div/div[1]/input")).SendKeys(toCityName);
                driver.FindElement(By.XPath("//*[@id='root']/div/div/div[1]/div/div[2]/div/div[3]/div[1]/div[5]/div/div/div[2]/ul/li[2]")).Click();
                Thread.Sleep(2000);
                Console.WriteLine(i++);
               
                //Thread.Sleep(1000);
                if (roundtrip)
                {
                    driver.FindElement(By.XPath("//*[@id='root']/div/div/div[1]/div/div[2]/div/div[3]/div[3]/div/div/div/div/button[2]")).Click();
                    Thread.Sleep(1000);
                }
                
                //*[@id='root']/div/div/div[1]/div/div[2]/div/div[3]/div[3]/div/div/div/div/div[2]/ul/div[2]/div/div[1]/div[2]/svg/g/path[2]
                //driver.FindElement(By.XPath("//*[@id='root']/div/div/div[1]/div/div[2]/div/div[3]/div[3]/div/div/div/div/div[2]/ul/div[2]/div/div[2]/div[2]/div[3]/div[1]/div[4]")).Click();
                //Thread.Sleep(1000);

                //*[@id="root"]/div/div/div[1]/div/div[2]/div/div[3]/div[3]/div/div/div/div/div[2]/ul/div[2]/div/div[2]/div[2]/div[3]/div[1]/div[4]/div








                //*[@id="root"]/div/div/div[1]/div/div[2]/div/div[4]/div/div[1]/select
                driver.FindElement(By.XPath("//*[@id='root']/div/div/div[1]/div/div[2]/div/div[4]/div/div[1]/select")).SendKeys(Convert.ToString(numberofAdults));
                Console.WriteLine("5");

                driver.FindElement(By.XPath("//*[@id='root']/div/div/div[1]/div/div[2]/div/div[4]/div/div[3]/select")).SendKeys(Convert.ToString(numberofChildren));
                Console.WriteLine("6");

                driver.FindElement(By.XPath("//*[@id='root']/div/div/div[1]/div/div[2]/div/div[4]/div/div[5]/select")).SendKeys(Convert.ToString(numberofInfants));

                //*[@id="root"]/div/div/div[1]/div/div[2]/div/div[7]/div[2]/button  //*[@id="root"]/div/div/div[1]/div/div[2]/div/div[7]/div[2]/button   Search flights
                //driver.FindElement(By.XPath("//*[@id='root']/div/div/div[1]/div/div[2]/div/div[7]/div[2]/button")).Click();
                //class="px-7 bg-primary-500 hover:bg-primary-600 c-white bc-transparent c-pointer w-100p py-2 px-5 h-10 fs-4 fw-600 t-all button bs-solid tp-color td-500 bw-1 br-4 lh-solid box-border"

               // driver.FindElement(By.CssSelector("px-7 bg-primary-500 hover:bg-primary-600 c-white bc-transparent c-pointer w-100p py-2 px-5 h-10 fs-4 fw-600 t-all button bs-solid tp-color td-500 bw-1 br-4 lh-solid box-border")).Click();
                ////*[@id="root"]/div/div/div[1]/div/div[2]/div/div[7]/div[2]/button
                driver.FindElement(By.XPath("//button[.='Search flights']")).Click();
                //class="col flex flex-middle"
               // driver.FindElement(By.XPath("//*[@id='root']/div/div/div[1]/div/div[2]/div/div[7]/div[2]/button")).Click();


                Thread.Sleep(5000);
                driver.SwitchTo().Window(driver.WindowHandles.Last());


                string lowest = driver.FindElement(By.XPath("//div[2]/div[3]/div[2]/p")).Text;
                driver.FindElement(By.XPath("//*[@id='root']/div/main/div/div/div[2]/div[2]/div[7]/div[1]/div[3]/a/svg")).Click();
                string highest = driver.FindElement(By.XPath("//div[2]/div[3]/div[2]/p")).Text;
                driver.FindElement(By.XPath("//*[@id='root']/div/main/div/div/div[2]/div[2]/div[7]/div[1]/div[3]/a/svg")).Click();
                Console.WriteLine(highest + " " + lowest);
                //*[@id="root"]/div/main/div/div/div[2]/div[2]/div[8]/div[1]/div[1]/div[2]/div[4]/button
                driver.FindElement(By.XPath("//*[@id='root']/div/main/div/div/div[2]/div[2]/div[8]/div[1]/div[1]/div[2]/div[4]/button")).Click();

                //*[@id="root"]/div/div[2]/div/div[1]/main/div[2]/div/div/div[15]/div[1]/div/div[1]
                driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div[1]/main/div[2]/div/div/div[15]/div[1]/div/div[1]")).Click();

                //*[@id="root"]/div/div[2]/div/div[1]/main/div[2]/div/div/div[23]/div[1]/button
                driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div[1]/main/div[2]/div/div/div[23]/div[1]/button")).Click();


            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            }


    }
}
