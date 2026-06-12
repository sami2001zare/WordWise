namespace WordWise.Core.Media.Book;

public sealed class Book : MediaBase
{
    private Book(Guid id, string title, Guid languageId, DateTime createDateTime, BookCategory category, string author) : base(id, title, languageId, createDateTime)
    {
        Category = category;
        Author = author;
    }

    public BookCategory Category { get; private set; }

    public string Author { get; private set; }

    public static Book Create(Guid id, string title, Guid languageId, DateTime createDateTime, BookCategory category, string author)
    {
        Book series = new(id, title, languageId, createDateTime, category, author);

        return series;
    }
}
