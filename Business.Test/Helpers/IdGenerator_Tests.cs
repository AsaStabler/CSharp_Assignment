using Business.Helpers;

namespace Business.Test.Helpers;

public class IdGenerator_Tests
{
    [Fact]
    public void GenerateUniqueId_ShouldReturnStringOfTypeGuid()
    {
        //act
        string id = IdGenerator.GenerateUniqueId();

        //assert
        Assert.False(string.IsNullOrEmpty(id));
        Assert.True(Guid.TryParse(id, out _));
    }
}
