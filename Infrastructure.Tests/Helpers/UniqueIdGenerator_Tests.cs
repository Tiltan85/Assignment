
using Infrastructure.Helpers;

namespace Infrastructure.Tests.Helpers;

public class UniqueIdGenerator_Tests
{
    [Fact]
    public void Gënerate_ShouldReturnGuidAsString_WhenSuccessful()
    {
        // Act
        // request a GUID from Generate
        string result = UniqueIdGenerator.Generate();

        //Assert
        //Check so its not empty
        Assert.NotEmpty(result);
        // Try to convert string to GUID
        Assert.True(Guid.TryParse(result, out _));
    }
}
