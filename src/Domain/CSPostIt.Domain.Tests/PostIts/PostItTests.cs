using CSPostIt.Domain.PostIts;
using NFluent;
using SharedKernel.TestCommon;

namespace CSPostIt.Domain.Tests.PostIts;

public class PostItTests
{
    [Fact]
    public void WhenTitleTextIsTooLong_ThenTruncateIt()
    {
        var settingsResult = PostItSettings.Create(10);
        var settings = settingsResult.GetValueOrThrowIfIsError();

        var outcome = PostIt.Create(settings);
        var postIt = outcome.GetValueOrThrowIfIsError();

        postIt.Title.SetText("0123456789TooLong");
        Check.That(postIt.Title.Text).HasSize(settings.TitleMaxLength);
    }
}