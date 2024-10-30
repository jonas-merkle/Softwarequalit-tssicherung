using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTestOfLSF;

public class UiTests : IDisposable
{
    #region private members

    private readonly IWebDriver _driver;
    
    #endregion

    #region ctor

    public UiTests()
    {
        // Initialize the WebDriver with Chrome
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");  // Open browser maximized
        options.AddArgument("--headless");         // Run tests in headless mode (optional)
        _driver = new ChromeDriver(options);

        // Set implicit wait time for finding elements
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    #endregion

    #region tests

    [Fact]
    public void Test_TopMenuInGerman()
    {
        _driver.Navigate().GoToUrl("https://campusonline.uni-ulm.de/qislsf/rds?state=user&type=0");

        // Locate the top menu items by their class and text content
        var menuItems = _driver.FindElements(By.CssSelector(".divlinks > a"));
        var expectedMenuItems = new List<string>
        {
            "Studentisches Leben",
            "Veranstaltungen",
            "Organisationseinheiten",
            "Studium",
            "Räume und Gebäude",
            "Personen"
        };

        foreach (var item in expectedMenuItems)
        {
            Assert.Contains(menuItems, i => i.Text.Contains(item));
        }
    }
    
    [Fact]
    public void Test_TopMenuInEnglish()
    {
        _driver.Navigate().GoToUrl("https://campusonline.uni-ulm.de/qislsf/rds?state=user&type=0");

        // Click on the English language icon
        _driver.FindElement(By.CssSelector("a[href*='language=en']")).Click();

        // Validate that menu items are displayed in English
        var menuItems = _driver.FindElements(By.CssSelector(".divlinks > a"));
        var expectedMenuItems = new List<string>
        {
            "Student's Corner",
            "Courses",
            "Organizational units",
            "Study",
            "Facilities",
            "Members"
        };

        foreach (var item in expectedMenuItems)
        {
            Assert.Contains(menuItems, i => i.Text.Contains(item));
        }
    }

    [Fact]
    public void Test_SearchForCourse()
    {
        //_driver.Navigate().GoToUrl("https://campusonline.uni-ulm.de/qislsf/rds?state=user&type=8&topitem=lectures&breadCrumbSource=");
        _driver.Navigate().GoToUrl("https://campusonline.uni-ulm.de/qislsf/rds?state=user&type=0");
        
        // Navigate to the course page
        _driver.FindElement(By.LinkText("Veranstaltungen")).Click();
        
        // Navigate to the course search page
        _driver.FindElement(By.LinkText("Suche nach Veranstaltungen")).Click(); // Update link text if necessary

        // Submit the form by clicking "Suche starten"
        // Find the form element
        var formElement = _driver.FindElement(By.XPath("//form[@action='https://campusonline.uni-ulm.de/qislsf/rds']"));

        // Fill in the "Veranstaltungsnummer" field
        var courseNumberField = formElement.FindElement(By.Id("veranstaltung.veranstnr"));
        courseNumberField.SendKeys("CS7251.000");
        
        var searchButton = _driver.FindElement(By.XPath("//input[@name='search_start']"));
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", searchButton);
        
        
        // Locate the table with summary "Übersicht über alle Veranstaltungen"
        var resultsTable = _driver.FindElement(By.CssSelector("table[summary='Übersicht über alle Veranstaltungen']"));
        
        
        // Find all rows in the table body
        var rows = resultsTable.FindElements(By.TagName("tr"));
    
        // Check if any row in the "Vst.-Nr." column contains "CS7251.000"
        bool courseFound = false;
        foreach (var row in rows)
        {
            // Locate the "Vst.-Nr." column within the current row (first column in each row)
            var cells = row.FindElements(By.TagName("td"));
            if (cells.Count > 0 && cells[0].Text.Contains("CS7251.000"))
            {
                courseFound = true;
                break;
            }
        }
        
        // Assert that the course number was found
        Assert.True(courseFound, "The course number 'CS7251.000' was not found in the 'Vst.-Nr.' column.");
        
    }

    [Fact]
    public void Test_LecturerNameAndID()
    {
        _driver.Navigate()
            .GoToUrl(
                "https://campusonline.uni-ulm.de/qislsf/rds?state=verpublish&status=init&vmfile=no&publishid=166077&moduleCall=webInfo&publishConfFile=webInfo&publishSubDir=veranstaltung");

        // Locate the table with corse information
        var resultsTable = _driver.FindElements(By.CssSelector("table"));


        // Find all rows in the table body
        var rows = resultsTable.First().FindElements(By.TagName("tr"));

        bool nameFound = false;
        bool idFound = false;
        foreach (var row in rows)
        {
            var cells1 = row.FindElements(By.CssSelector("td.mod"));
            var cells2 = row.FindElements(By.CssSelector("td.mod_n"));

            if (cells1.Count > 0 && cells1[0].Text.Contains("Titel") &&
                cells2.Count > 0 && cells2[0].Text.Contains("Softwarequalitätssicherung"))
            {
                nameFound = true;
            }

            if (cells1.Count > 0 && cells1[0].Text.Contains("Veranstaltungsnummer") &&
                cells2.Count > 0 && cells2[0].Text.Contains("CS7251.000"))
            {
                idFound = true;
            }

            if (nameFound && idFound)
            {
                break;
            }

        }

        // Assert that the course name was found
        Assert.True(nameFound, "The course name 'Softwarequalitätssicherung' was not found in the 'Titel' row.");
        
        // Assert that the course id was found
        Assert.True(idFound, "The course id 'CS7251.000' was not found in the 'Veranstaltungsnummer' row.");
    }

    [Fact]
    public void Test_LoginInputFieldsPresence()
    {
        _driver.Navigate().GoToUrl("https://campusonline.uni-ulm.de/qislsf/rds?state=user&type=0");

        // Check if username field is present
        var usernameField = _driver.FindElement(By.Id("asdf"));
        Assert.NotNull(usernameField);

        // Check if password field is present
        var passwordField = _driver.FindElement(By.Id("fdsa"));
        Assert.NotNull(passwordField);

        // Check if login button is present
        var loginButton = _driver.FindElement(By.Id("loginForm:login"));
        Assert.NotNull(loginButton);
    }
    
    #endregion

    #region Implementation of IDisposable interface

    public void Dispose()
    {
        // Clean up the WebDriver after test execution
        _driver.Quit();
    }

    #endregion
}
