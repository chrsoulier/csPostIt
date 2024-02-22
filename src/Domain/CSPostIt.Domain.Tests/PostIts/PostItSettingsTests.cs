using CSPostIt.Domain.PostIts;
using NFluent;

namespace CSPostIt.Domain.Tests.PostIts;

public class PostItSettingsTests
{
    [Fact]
    public void TitleMaxLength_MustBeGreaterOrEqualsToZero()
    {
        var outcome = PostItSettings.Create(-1);
        Check.That(outcome.IsError).IsTrue();
    }
}