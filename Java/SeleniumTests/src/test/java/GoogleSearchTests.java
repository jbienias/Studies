import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.WebDriverWait;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.junit.jupiter.api.Assertions.assertNull;

public class GoogleSearchTests {
    private WebDriver driver;
    private WebDriverWait wait;

    @BeforeEach
    public void setUp() {
        driver = BrowserManager.initializeBrowser(driver, "Chrome");
        driver.manage().window().maximize();
        driver.get("https://google.com");
        wait = new WebDriverWait(driver, 15);
    }

    @Test
    public void goToNonExistingSiteInGoogle(){
        String crazyString = "3 1as f dasa  eqk' Z .[f \\3.[ 4; 24 .l2p ,kwq.d]psam o1-2p1=.4 [ 3k ;llfsmjkl hsj fge;iuifuiqweuoeiqppeo2op e21 p[eq l/a;s";
        driver.findElement(By.id("lst-ib")).sendKeys(crazyString);
        driver.findElement(By.name("btnK")).click();

        Boolean result = driver.findElements(By.xpath("(//h3)[1]/a")).size() > 1;
        assertFalse(result);
    }

    @Test
    public void goToYouTubeByFirstResultInGoogle(){
        WebElement webElement = driver.findElement(By.id("lst-ib"));
        webElement.sendKeys("YouTube");
        webElement.sendKeys(Keys.ENTER);

        driver.findElement(By.xpath("(//h3)[1]/a")).click();
        assertEquals("YouTube", driver.getTitle());
    }

    @AfterEach
    public void tearDown(){
        driver.quit();
    }
}
