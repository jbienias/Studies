package PageObjects;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.WebDriverWait;

public class PageObjectGitHub {
    public WebDriver driver;
    private WebDriverWait wait;

    public PageObjectGitHub(WebDriver driver) {
        this.driver = driver;
        driver.get("https://github.com/");
        wait = new WebDriverWait(driver, 10);
    }

    public boolean assertTitle() {
        Boolean result = driver.getTitle().contains("GitHub");
        return (result);
    }
}
