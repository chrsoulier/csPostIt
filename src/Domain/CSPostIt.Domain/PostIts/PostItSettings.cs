using SharedKernel.Core;

namespace CSPostIt.Domain.PostIts;

public sealed class PostItSettings
{
    public int TitleMaxLength { get; }

    private PostItSettings(int titleMaxLength)
    {
        TitleMaxLength = titleMaxLength;
    }

    public static Result<PostItSettings, PostIt.CreationFailed> Create(int titleMaxLength)
    {
        if (titleMaxLength < 0) return new PostIt.CreationFailed();
        return new PostItSettings(titleMaxLength);
    }
}