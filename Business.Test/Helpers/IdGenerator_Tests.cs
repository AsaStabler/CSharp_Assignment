using Business.Helpers;

namespace Business.Test.Helpers;

public class IdGenerator_Tests
{
    [Fact]
    public void GenerateUniqueId_ShouldReturnStringOfTypeGuid()
    {
        // Act
        var id = IdGenerator.GenerateUniqueId();

        // Assert
        Assert.False(string.IsNullOrEmpty(id));
        Assert.True(Guid.TryParse(id, out _));
    }
}
