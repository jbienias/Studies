package PageObjects;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.WebDriverWait;


public class PageObjectHerokuEdit {
    public WebDriver driver;
    private WebDriverWait wait;

    public PageObjectHerokuEdit(WebDriver driver){
        this.driver = driver;
        driver.get("https://csgocrud.herokuapp.com/players");
        this.wait = new WebDriverWait(driver, 15);
    }

    public void editPlayer() throws InterruptedException {
        WebElement element = driver.findElement(By.xpath("//tr[td[1]//text()[contains(., 'Adam')]]/td[5]/a[@class='btn btn-success btn-sm']"));
        element.click();
        //wait.until(ExpectedConditions.urlContains("edit")); <- przepraszam, tu musialem, nie wiem czemu nie widzi tego
        Thread.sleep(5000);
        driver.findElement(By.id("player_name")).sendKeys("Nowy");
        driver.findElement(By.name("commit")).click();
    }

    public void editPlayerWrong() throws InterruptedException {
        WebElement element = driver.findElement(By.xpath("//tr[td[1]//text()[contains(., 'Adam')]]/td[5]/a[@class='btn btn-success btn-sm']"));
        element.click();
        //wait.until(ExpectedConditions.urlContains("edit"));  <- tu również
        Thread.sleep(5000);
        driver.findElement(By.id("player_name")).clear();
        driver.findElement(By.name("commit")).click();
    }

    public WebElement getEditedWebElement(){
        return driver.findElement(By.xpath("/html/body/div[@class='container']/p[1]"));
    }

    public boolean currentUrlIsEdit(){
        return driver.getCurrentUrl().contains("edit");
    }
}
