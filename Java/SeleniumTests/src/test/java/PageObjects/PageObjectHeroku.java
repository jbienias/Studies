package PageObjects;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.WebDriverWait;

public class PageObjectHeroku {
    public WebDriver driver;
    private WebDriverWait wait;

    public PageObjectHeroku(WebDriver driver) {
        this.driver = driver;
        driver.get("https://csgocrud.herokuapp.com/");
        this.wait = new WebDriverWait(driver, 15);
    }

    public boolean assertTitle() {
        Boolean result = driver.getTitle().contains("CS:GO CRUD");
        return (result);
    }
}
