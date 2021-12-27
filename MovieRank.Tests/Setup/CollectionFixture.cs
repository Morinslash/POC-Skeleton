using Xunit;

namespace MovieRank.Tests.Setup
{
    [CollectionDefinition(("api"))]
    public class CollectionFixture : ICollectionFixture<TestContext>
    {
        
    }
}