namespace Rockaway.WebApp.Tests;

public class StatusControllerTests {
    
    [Fact]
    public void Status_Index_Returns_Message() {
        var testDateTime = new DateTime(2023,4,5,6,7,8);
        var clock = new TestClock(testDateTime);
        var c = new StatusController(clock);
        var result = c.Index() as ViewResult;
        result.ShouldNotBeNull();
        var model = result.Model as SystemStatus;
        model.ShouldNotBeNull();
        model.Message.ShouldBe("Rockaway.WebApp is online");
    }

    [Fact]
    public void Status_Index_Returns_DateTime() {
        var testDateTime = new DateTime(2023,4,5,6,7,8);
        var clock = new TestClock(testDateTime);
        var c = new StatusController(clock);
        var result = c.Index() as ViewResult;
        result.ShouldNotBeNull();
        var model = result.Model as SystemStatus;
        model.ShouldNotBeNull();
        model.SystemTime.ShouldBe(testDateTime);
    }
}