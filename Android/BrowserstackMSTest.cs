namespace Android;

public class BrowserstackMSTest
{
    public AndroidDriver<AndroidElement>? driver;

    public BrowserstackMSTest() { }

    [TestInitialize]
    public void TestSetup()
    {
            // Lấy các capabilities từ cấu hình
            var capabilities = BrowserstackConfig.GetCapabilities();

            // URL kết nối đến BrowserStack
            var browserStackUrl = new Uri($"https://{BrowserstackConfig.Username}:{BrowserstackConfig.AccessKey}@hub-cloud.browserstack.com/wd/hub");

            // Thiết lập WebDriver với desired capabilities
            AppiumOptions appiumOptions = new AppiumOptions();

            foreach (var capability in capabilities)
            {
                appiumOptions.AddAdditionalCapability(capability.Key, capability.Value);
            }
            driver = new AndroidDriver<AndroidElement>(browserStackUrl, appiumOptions);

    }

    [TestCleanup]
    public void TestTeardown()
    {
        if (driver != null)
            driver.Quit();
    }
}

internal static class BrowserstackConfig
{
    public const string Username = "User_Name";
    public const string AccessKey = "Access_Key";
    public const string AppUrl = "bs://b611df9b62ebe4716cd90d2f4d0d27bf24594237";

    public static Dictionary<string, object> GetCapabilities()
    {
        return new Dictionary<string, object>
        {
            ["platformName"] = "Android",
            ["osVersion"] = "12.0",
            ["deviceName"] = "Samsung Galaxy S22 Ultra",
            ["app"] = AppUrl,
            ["browserstack.user"] = Username,
            ["browserstack.key"] = AccessKey,
            ["projectName"] = "BrowserStack Sample",
            ["build"] = "MSTest Build",
            ["name"] = "Sample Test",
            ["buildIdentifier"] = "#${BUILD_NUMBER}"
        };
    }
}