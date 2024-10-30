using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTestOfLSF;

/// <summary>
///     A class containing UI tests for the university's Campus Online system.
/// </summary>
public class UiTests : IDisposable
{
    #region private members

    /// <summary>
    ///     WebDriver instance for interacting with the browser.
    /// </summary>
    private readonly IWebDriver _driver;

    #endregion

    #region ctor

    /// <summary>
    ///     Initializes a new instance of the <see cref="UiTests" /> class,
    ///     setting up a ChromeDriver with specific options.
    /// </summary>
    public UiTests()
    {
        var options = new ChromeOptions(); // Create a new ChromeOptions object to set browser options.
        options.AddArgument("--start-maximized"); // Maximize the browser window.
        options.AddArgument("--headless"); // Run tests in headless mode (no UI).

        _driver = new ChromeDriver(options); // Initialize ChromeDriver with options.
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Set an implicit wait of 10 seconds.
    }

    #endregion

    #region Implementation of IDisposable interface

    /// <summary>
    ///     Disposes of resources, closing the WebDriver after test execution.
    /// </summary>
    public void Dispose()
    {
        _driver.Quit(); // Close and quit the WebDriver, releasing resources.
    }

    #endregion

    #region tests

    /// <summary>
    ///     Verifies that the top menu items are displayed in German by default.
    /// </summary>
    [Fact]
    public void Test_TopMenuInGerman()
    {
        // Arrange
        _driver.Navigate()
            .GoToUrl("https://campusonline.uni-ulm.de/qislsf/rds?state=user&type=0");

        // Define expected German menu items.
        var expectedMenuItems = new List<string>
        {
            "Studentisches Leben",
            "Veranstaltungen",
            "Organisationseinheiten",
            "Studium",
            "Räume und Gebäude",
            "Personen"
        };

        // Act
        var menuItems = _driver.FindElements(By.CssSelector(".divlinks > a")) // Find all top menu items.
            .Select(i => i.Text) // Get the text of each menu item.
            .ToList(); // Convert the collection to a list.

        // Assert
        menuItems.Should()
            .Contain(expectedMenuItems,
                "the German top menu should display specific options."); // Verify that the German menu items are present.
    }

    /// <summary>
    ///     Verifies that the top menu items switch to English after clicking the English language icon.
    /// </summary>
    [Fact]
    public void Test_TopMenuInEnglish()
    {
        // Arrange
        _driver.Navigate()
            .GoToUrl("https://campusonline.uni-ulm.de/qislsf/rds?state=user&type=0");

        // Define expected English menu items.
        var expectedMenuItems = new List<string>
        {
            "Student's Corner",
            "Courses",
            "Organizational units",
            "Study",
            "Facilities",
            "Members"
        };

        // Act
        _driver.FindElement(By.CssSelector("a[href*='language=en']")).Click(); // Click the English language icon.
        var menuItems = _driver.FindElements(By.CssSelector(".divlinks > a")) // Find all top menu items.
            .Select(i => i.Text) // Get the text of each menu item.
            .ToList(); // Convert the collection to a list.

        // Assert
        menuItems.Should()
            .Contain(expectedMenuItems,
                "the English top menu should display specific options."); // Verify that the English menu items are present.
    }

    /// <summary>
    ///     Searches for a specific course by its number and verifies that it appears in the results.
    /// </summary>
    [Fact]
    public void Test_SearchForCourse()
    {
        // Arrange
        _driver.Navigate()
            .GoToUrl("https://campusonline.uni-ulm.de/qislsf/rds?state=user&type=0");

        // Act
        _driver.FindElement(By.LinkText("Veranstaltungen"))
            .Click(); // Click the "Veranstaltungen" link to navigate to courses.
        _driver.FindElement(By.LinkText("Suche nach Veranstaltungen"))
            .Click(); // Click the "Suche nach Veranstaltungen" link to open the search form.

        var formElement =
            _driver.FindElement(
                By.XPath(
                    "//form[@action='https://campusonline.uni-ulm.de/qislsf/rds']")); // Locate the course search form.
        formElement.FindElement(By.Id("veranstaltung.veranstnr"))
            .SendKeys("CS7251.000"); // Enter course number into the search field.

        var searchButton = _driver.FindElement(By.XPath("//input[@name='search_start']")); // Locate the search button.
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();",
            searchButton); // Click the search button using JavaScript.

        // Assert
        var resultsTable =
            _driver.FindElement(
                By.CssSelector("table[summary='Übersicht über alle Veranstaltungen']")); // Locate the results table.
        var courseFound = resultsTable.FindElements(By.TagName("tr")) // Find all rows in the table.
            .Any(row => row.FindElements(By.TagName("td")).FirstOrDefault()?.Text.Contains("CS7251.000") ==
                        true); // Check if any row contains the course number.

        courseFound.Should()
            .BeTrue(
                "the course number 'CS7251.000' should be found in the 'Vst.-Nr.' column."); // Verify that the course number is found in the results.
    }

    /// <summary>
    ///     Verifies that a course's lecturer name and ID are displayed correctly on the course information page.
    /// </summary>
    [Fact]
    public void Test_LecturerNameAndID()
    {
        // Arrange
        _driver.Navigate()
            .GoToUrl(
                "https://campusonline.uni-ulm.de/qislsf/rds?state=verpublish&status=init&vmfile=no&publishid=166077&moduleCall=webInfo&publishConfFile=webInfo&publishSubDir=veranstaltung");

        // Act
        var rows = _driver.FindElements(By.CssSelector("table")).First()
            .FindElements(By.TagName("tr")); // Locate the first table and retrieve all rows.
        var nameFound =
            rows.Any(row =>
                row.Text.Contains("Titel") &&
                row.Text.Contains("Softwarequalitätssicherung")); // Check if any row has the correct course name.
        var idFound =
            rows.Any(row =>
                row.Text.Contains("Veranstaltungsnummer") &&
                row.Text.Contains("CS7251.000")); // Check if any row has the correct course ID.

        // Assert
        nameFound.Should()
            .BeTrue(
                "the course name 'Softwarequalitätssicherung' should be in the 'Titel' row."); // Verify that the course name is present.
        idFound.Should()
            .BeTrue(
                "the course id 'CS7251.000' should be in the 'Veranstaltungsnummer' row."); // Verify that the course ID is present.
    }

    /// <summary>
    ///     Verifies the presence of username, password, and login button fields on the login page.
    /// </summary>
    [Fact]
    public void Test_LoginInputFieldsPresence()
    {
        // Arrange
        _driver.Navigate()
            .GoToUrl("https://campusonline.uni-ulm.de/qislsf/rds?state=user&type=0");

        // Act
        var usernameField = _driver.FindElement(By.Id("asdf")); // Locate the username input field.
        var passwordField = _driver.FindElement(By.Id("fdsa")); // Locate the password input field.
        var loginButton = _driver.FindElement(By.Id("loginForm:login")); // Locate the login button.

        // Assert
        usernameField.Should()
            .NotBeNull("a username input field should be present."); // Verify that the username field exists.
        passwordField.Should()
            .NotBeNull("a password input field should be present."); // Verify that the password field exists.
        loginButton.Should().NotBeNull("a login button should be present."); // Verify that the login button exists.
    }

    #endregion
}