using SharedKernel.Core;

namespace CSPostIt.Domain.PostIts;

public sealed class PostIt
{
    public PostItTitle Title { get; }

    private PostIt(PostItSettings settings)
    {
        Title = new PostItTitle(settings.TitleMaxLength);
    }

    public static Result<PostIt, CreationFailed> Create(PostItSettings settings)
    {
        return new PostIt(settings);
    }

    public record CreationFailed;
}

public sealed class PostItTitle(int textMaxLength)
{
    public string Text { get; private set; } = string.Empty;
    private int TextMaxLength { get; } = textMaxLength;

    public void SetText(string newText)
    {
        Text = newText.Length <= TextMaxLength
            ? newText
            : newText.Substring(0, TextMaxLength);
    }
}