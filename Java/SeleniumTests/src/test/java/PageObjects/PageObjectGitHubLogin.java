package PageObjects;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

public class PageObjectGitHubLogin {
    public WebDriver driver;
    private WebDriverWait wait;

    public PageObjectGitHubLogin(WebDriver driver) {
        this.driver = driver;
        driver.get("https://github.com/login");
        wait = new WebDriverWait(driver, 10);
    }

    public void loginToExistingAccount() throws InterruptedException {
        driver.findElement(By.id("login_field")).sendKeys("zbychujanusz");
        driver.findElement(By.name("password")).sendKeys("supertajnehaslo123");
        driver.findElement(By.name("commit")).click();
        wait.until(ExpectedConditions.urlMatches("https://github.com/"));
    }

    public void loginToNonExistingAccount() throws InterruptedException {
        driver.findElement(By.id("login_field")).sendKeys("423 43 q");
        driver.findElement(By.name("password")).sendKeys("1");
        driver.findElement(By.name("commit")).click();
        wait.until(ExpectedConditions.urlContains("https://github.com/session"));
    }

    public String getLoggedInUrl(){
        return driver.getCurrentUrl();
    }

    public String getErrorMessage(){
        return driver.findElement(By.xpath("/html/body[@class='logged-out env-production page-responsive min-width-0 session-authentication']/div[@class='application-main ']/div[@id='js-pjax-container']/div[@id='login']/form/div[@id='js-flash-container']/div[@class='flash flash-full flash-error']/div[@class='container']")).getText();
    }
}
