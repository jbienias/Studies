public class BrowserSelecor {
    public static void setBrowserProperty(String browser) {
        if (System.getProperty("os.name").contains("Windows")) {
            switch (browser) {
                case "Chrome":
                    System.setProperty("webdriver.chrome.driver", "resources/chromedriver.exe");
                    break;
                case "Firefox":
                    System.setProperty("webdriver.gecko.driver", "resources/geckodriver.exe");
                    break;
                case "Explorer":
                    System.setProperty("webdriver.ie.driver", "resources/IEDriverServer.exe");
                    break;
                case "Opera":
                    System.setProperty("webdriver.chrome.driver", "resources/operadriver.exe");
                    break;
            }
        } else {
            switch (browser) {
                case "Chrome":
                    System.setProperty("webdriver.chrome.driver", "resources/chromedriver");
                    break;
                case "Firefox":
                    System.setProperty("webdriver.gecko.driver", "resources/geckodriver");
                    break;
                case "Explorer":
                    throw new IllegalArgumentException("Explorer browser is accesed only on Windows");
                case "Opera":
                    break;
            }
        }
    }
}
