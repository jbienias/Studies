import PageObjects.PageObjectHeroku;
import PageObjects.PageObjectHerokuAdd;
import PageObjects.PageObjectHerokuEdit;
import org.assertj.core.api.Assertions;
import org.openqa.selenium.WebDriver;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;

import static org.assertj.core.api.AssertionsForClassTypes.assertThat;
import static org.junit.jupiter.api.Assertions.assertTrue;

public class HerokuTests {
    private static WebDriver driver;
    private PageObjectHeroku pageObjectHeroku;
    private PageObjectHerokuAdd pageObjectHerokuAdd;
    private PageObjectHerokuEdit pageObjectHerokuEdit;

    @BeforeEach
    public void setUp() {
        driver = BrowserManager.initializeBrowser(driver, "Chrome");
        driver.manage().window().maximize();
    }

    @Test
    public void checkTitle() {
        pageObjectHeroku = new PageObjectHeroku(driver);
        assertTrue(pageObjectHeroku.assertTitle());
    }

    @Test
    public void addCorrectlyFilledPlayer(){
        pageObjectHerokuAdd = new PageObjectHerokuAdd(driver);
        pageObjectHerokuAdd.addCorrectPlayer();
        String result = pageObjectHerokuAdd.getResultNotice();
        Assertions.assertThat(result).contains("success");
    }

    @Test
    public void addEmptyPlayer(){
        pageObjectHerokuAdd = new PageObjectHerokuAdd(driver);
        pageObjectHerokuAdd.addEmptyPlayer();
        String result = pageObjectHerokuAdd.getResultNotice();
        String numOfErrors = pageObjectHerokuAdd.getResultError();
        assertThat(result.isEmpty() && numOfErrors.contains("8 errors prohibited"));
    }

    @Test
    public void editPlayer() throws InterruptedException {
        pageObjectHerokuEdit = new PageObjectHerokuEdit(driver);
        pageObjectHerokuEdit.editPlayer();
        String result = pageObjectHerokuEdit.getEditedWebElement().getText();
        assertThat(result).contains("Nowy");
    }

    @Test
    public void editPlayerWrong() throws InterruptedException {
        pageObjectHerokuEdit = new PageObjectHerokuEdit(driver);
        pageObjectHerokuEdit.editPlayerWrong();
        assertThat(pageObjectHerokuEdit.currentUrlIsEdit()).isFalse();
    }

    @AfterEach
    public void tearDown() {
        driver.quit();
    }
}