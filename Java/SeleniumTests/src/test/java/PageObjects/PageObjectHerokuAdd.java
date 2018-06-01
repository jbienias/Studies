package PageObjects;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.WebDriverWait;

public class PageObjectHerokuAdd {
    public WebDriver driver;
    private WebDriverWait wait;

    public PageObjectHerokuAdd(WebDriver driver) {
        this.driver = driver;
        driver.get("https://csgocrud.herokuapp.com/players/new");
        this.wait = new WebDriverWait(driver, 15);
    }

    public void addCorrectPlayer() {
        String name = "Adam";
        String surname = "Nawalka";
        String nickname = "Elo";
        String salary = "10000";
        driver.findElement(By.id("player_name")).sendKeys(name);
        driver.findElement(By.id("player_surname")).sendKeys(surname);
        driver.findElement(By.id("player_nickname")).sendKeys(nickname);
        driver.findElement(By.id("player_salary")).sendKeys(salary);
        driver.findElement(By.name("commit")).click();
    }

    public void addEmptyPlayer() {
        driver.findElement(By.name("commit")).click();
    }

    public String getResultNotice() {
        String result = driver.findElement(By.id("notice")).getText();
        return (result);
    }

    public String getResultError(){
        String result = driver.findElement(By.id("error_explanation")).getText();
        return (result);
    }
}
