using System;
using Xunit;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Project.Tests
{

    public class UITestClass : IDisposable
    {
        private readonly IWebDriver chrome;
        private string webSiteUrl;

        public UITestClass()
        {
            webSiteUrl = "https://localhost:5001/";
            chrome = new ChromeDriver();
            chrome.Manage().Window.Maximize();
        }

        public void Dispose()
        {
            chrome.Quit();
            chrome.Dispose();
        }
        [Fact]
        public void CreateStudentTest()
        {
            chrome.Navigate().GoToUrl(webSiteUrl + "Student/Create");

            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            chrome.FindElement(By.Id("firstName")).SendKeys("Jovan");
            chrome.FindElement(By.Id("lastName")).SendKeys("Jovic");

            chrome.FindElement(By.CssSelector("input.btn.btn-primary")).Click();


            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
 
        
            Assert.Contains("Jovan",chrome.PageSource);
            Assert.Contains("Jovic",chrome.PageSource);

        }
        [Fact]
         public void DeleteStudentTest()
        {
            chrome.Navigate().GoToUrl(webSiteUrl + "Student/Delete/3");
            
            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
             
            chrome.FindElement(By.CssSelector("input.btn.btn-danger")).Click();


            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
 
        
            
            Assert.DoesNotContain("Uros",chrome.PageSource);
              Assert.DoesNotContain("Nikolic",chrome.PageSource);

        }

        
        [Fact]
        public void EditStudentTest()
        {
            chrome.Navigate().GoToUrl(webSiteUrl + "Student/Edit/4");

            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
               chrome.FindElement(By.Id("firstName")).Clear();
               chrome.FindElement(By.Id("lastName")).Clear();
            chrome.FindElement(By.Id("firstName")).SendKeys("Tamara");
            chrome.FindElement(By.Id("lastName")).SendKeys("Lukic");

            chrome.FindElement(By.CssSelector("input.btn.btn-primary")).Click();


            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);


 
        
            Assert.Contains("Tamara",chrome.PageSource);
              Assert.Contains("Lukic",chrome.PageSource);

        }

             [Fact]
        public void DetailsStudentTest()
        {
            chrome.Navigate().GoToUrl(webSiteUrl + "Student/Details/1");

            

            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);


 
        
            Assert.Contains("Marko",chrome.PageSource);
              Assert.Contains("Markovic",chrome.PageSource);

        }

        

        [Fact]
        public void CreateSubjectTest()
        {
            chrome.Navigate().GoToUrl(webSiteUrl + "Subject/Create");
            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);

            chrome.FindElement(By.Id("name")).SendKeys("Math 2");
            var list = chrome.FindElement(By.Name("professors"));
            SelectElement selectedElem = new SelectElement(list);
            selectedElem.SelectByValue("2");


            chrome.FindElement(By.CssSelector("input.btn.btn-primary")).Click();


            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);



            Assert.Contains("Math 2", chrome.PageSource); 



        }

         [Fact]
        public void EditSubjectTest()
        {
            chrome.Navigate().GoToUrl(webSiteUrl + "Subject/Edit/1");

            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            chrome.FindElement(By.Id("name")).SendKeys(" 1");
            var list = chrome.FindElement(By.Name("professors"));
            SelectElement selectedElem = new SelectElement(list);
            selectedElem.SelectByValue("2");


            chrome.FindElement(By.CssSelector("input.btn.btn-primary")).Click();


            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);

 
 
        
            Assert.Contains("Math 1",chrome.PageSource);
            

        }

           [Fact]
        public void DetailsSubjectTest()
        {
            chrome.Navigate().GoToUrl(webSiteUrl + "Subject/Details/1");

            

            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);

        
            Assert.Contains("Math",chrome.PageSource); 

        }


               [Fact]
        public void DeleteSubjectTest()
        {
            chrome.Navigate().GoToUrl(webSiteUrl + "Subject/Delete/5");
            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);

             chrome.FindElement(By.CssSelector("input.btn.btn-danger")).Click();


            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
 
        
              chrome.Navigate().GoToUrl(webSiteUrl + "Subject/Delete/5");
            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
            
              Assert.Contains("inactive",chrome.PageSource);

        }
        

        [Fact]
        public void RegisterExamTest()
        {
            chrome.Navigate().GoToUrl(webSiteUrl + "StudentExam/Create");
            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);

            chrome.FindElement(By.Id("idStud")).SendKeys("4");
            var list = chrome.FindElement(By.Id("idExam"));
            SelectElement selectedElem = new SelectElement(list);
            selectedElem.SelectByValue("8");


            chrome.FindElement(By.CssSelector("input.btn.btn-primary")).Click();


            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);



            Assert.Contains("Grade Students", chrome.PageSource); 



        } 
 
        
        [Fact]
        public void Register6TimesExamTest()
        {
            chrome.Navigate().GoToUrl(webSiteUrl + "StudentExam/Create");
            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);

            chrome.FindElement(By.Id("idStud")).SendKeys("4");
            var list = chrome.FindElement(By.Id("idExam"));
            SelectElement selectedElem = new SelectElement(list);
            selectedElem.SelectByValue("1");


            chrome.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            

            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
             chrome.Navigate().GoToUrl(webSiteUrl + "StudentExam/Create");
              chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            chrome.FindElement(By.Id("idStud")).SendKeys("4");
            var list1 = chrome.FindElement(By.Id("idExam"));
            SelectElement selectedElem1 = new SelectElement(list1);
            selectedElem1.SelectByValue("2");


            chrome.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);

    
            chrome.Navigate().GoToUrl(webSiteUrl + "StudentExam/Create");
            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);


            chrome.FindElement(By.Id("idStud")).SendKeys("4");
            var list2 = chrome.FindElement(By.Id("idExam"));
            SelectElement selectedElem2 = new SelectElement(list2);
            selectedElem2.SelectByValue("3");


            chrome.FindElement(By.CssSelector("input.btn.btn-primary")).Click();


            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);

            
            chrome.Navigate().GoToUrl(webSiteUrl + "StudentExam/Create");
            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            chrome.FindElement(By.Id("idStud")).SendKeys("4");
            var list3 = chrome.FindElement(By.Id("idExam"));
            SelectElement selectedElem3 = new SelectElement(list3);
            selectedElem3.SelectByValue("4");


            chrome.FindElement(By.CssSelector("input.btn.btn-primary")).Click();


            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
            
            chrome.Navigate().GoToUrl(webSiteUrl + "StudentExam/Create");
            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            chrome.FindElement(By.Id("idStud")).SendKeys("4");
            var list4 = chrome.FindElement(By.Id("idExam"));
            SelectElement selectedElem4 = new SelectElement(list4);
            selectedElem4.SelectByValue("5");


            chrome.FindElement(By.CssSelector("input.btn.btn-primary")).Click();


            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
            
            chrome.Navigate().GoToUrl(webSiteUrl + "StudentExam/Create");
            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            chrome.FindElement(By.Id("idStud")).SendKeys("4");
            var list5 = chrome.FindElement(By.Id("idExam"));
            SelectElement selectedElem5 = new SelectElement(list5);
            selectedElem5.SelectByValue("6");


            chrome.FindElement(By.CssSelector("input.btn.btn-primary")).Click();


            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);



            Assert.Contains("Registration for exam", chrome.PageSource); 



        } 


        
 
 

        
        

    }

}