# Übungsblatt 02

|   |            |
|---|------------|
| Abgabe von: | **Jonas Merkle** [[jonas.merkle@uni-ulm.de](mailto:jonas.merkle@uni-ulm.de?subject=%C3%9Cbung%3A%20Softwarequalit%C3%A4tssicherung)] |
| Abgabe bis: | XX.XX.2024 08:00 |
| Repository: | [https://github.com/jonas-merkle/Softwarequalitaetssicherung](https://github.com/jonas-merkle/Softwarequalitaetssicherung) |
| Dateien:    | [PDF](https://jonas-merkle.github.io/Softwarequalitaetssicherung/Uebungsblatt02/Uebungsblatt02_Jonas-Merkle.pdf), [ZIP](https://jonas-merkle.github.io/Softwarequalitaetssicherung/Uebungsblatt02/Uebungsblatt02_Jonas-Merkle.zip), [HTML](https://jonas-merkle.github.io/Softwarequalitaetssicherung/Uebungsblatt02/Uebungsblatt02_Jonas-Merkle.html) |

## Inhaltsverzeichnis

- [Übungsblatt 02](#übungsblatt-02)
  - [Inhaltsverzeichnis](#inhaltsverzeichnis)
  - [Aufgabe 1](#aufgabe-1)
    - [Project Structure](#project-structure)
    - [Test Cases](#test-cases)
    - [Setup and Execution](#setup-and-execution)
    - [Notes](#notes)

## Aufgabe 1

The test project [SeleniumTestOfLSF](./src/SeleniumTestOfLSF/) contains UI tests for the **LSF website** of Ulm University, implemented using **Xunit** and **Selenium WebDriver**. The tests verify the functionality of various UI components on the website, ensuring that elements such as the navigation menu, course search, and login fields work as expected.

### Project Structure

- **UiTests.cs**: Contains multiple test cases, each following the Arrange-Act-Assert pattern.
- **Dependencies**:
  - [Selenium.WebDriver](https://www.nuget.org/packages/Selenium.WebDriver/)
  - [Selenium.WebDriver.ChromeDriver](https://www.nuget.org/packages/Selenium.WebDriver.ChromeDriver/)
  - [FluentAssertions](https://www.nuget.org/packages/fluentassertions/)
  - [Xunit](https://www.nuget.org/packages/xunit/)

### Test Cases

1. **Test_TopMenuInGerman**: Verifies that the default top menu items are displayed in German.
2. **Test_TopMenuInEnglish**: Checks that the top menu items switch to English after selecting the English language option.
3. **Test_SearchForCourse**: Searches for a specific course by course number and confirms its presence in the results.
4. **Test_CourseNameAndID**: Validates that the course title and ID are displayed correctly on the course details page.
5. **Test_LoginInputFieldsPresence**: Confirms that the username, password, and login button fields are present on the login page.

### Setup and Execution

1. **Install Chrome**: Make sure Chrome is installed on the machine running the tests.
2. **Install Dependencies**:
   - Use NuGet Package Manager or run:

     ```sh
     dotnet add package Selenium.WebDriver
     dotnet add package Selenium.WebDriver.ChromeDriver
     dotnet add package FluentAssertions
     dotnet add package xunit
     ```

3. **Run Tests**:
   - To execute tests, use the following command in the terminal:

     ```sh
     dotnet test
     ```

### Notes

- The tests are configured to run in **headless mode** by default. You can remove the `--headless` argument in `UiTests.cs` to view the browser during tests.
- ChromeDriver is set to maximize the browser window to ensure all elements are visible for interaction.
