import PageObjects.PageObjectGitHub;
import PageObjects.PageObjectGitHubLogin;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.WebDriver;

import static org.assertj.core.api.AssertionsForClassTypes.assertThat;
import static org.junit.jupiter.api.Assertions.assertTrue;

public class GitHubTests {

    private WebDriver driver;
    private PageObjectGitHub pageObjectGitHub;
    private PageObjectGitHubLogin pageObjectGitHubLogin;

    @BeforeEach
    public void setUp() {
        driver = BrowserManager.initializeBrowser(driver, "Firefox");
        driver.manage().window().maximize();
    }

    @Test
    public void titlePageContainsGitHubString() {
        pageObjectGitHub = new PageObjectGitHub(driver);
        assertTrue(pageObjectGitHub.assertTitle());
    }

    @Test
    public void loginCorrectData() {
        pageObjectGitHubLogin = new PageObjectGitHubLogin(driver);
        pageObjectGitHubLogin.loginToExistingAccount();
        assertThat(pageObjectGitHubLogin.getLoggedInUrl()).endsWith(".com/");
    }

    @Test
    public void loginIncorrectData() {
        pageObjectGitHubLogin = new PageObjectGitHubLogin(driver);
        pageObjectGitHubLogin.loginToNonExistingAccount();
        assertThat(pageObjectGitHubLogin.getErrorMessage()).contains("Incorrect");
    }

    @AfterEach
    public void tearDown() {
        driver.quit();
    }
}
