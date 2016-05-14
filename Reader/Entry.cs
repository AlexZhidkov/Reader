namespace ReaderLib
{
    public class Entry
    {
        public string Title { get; }
        public string Body { get; }
        public Entry(string title, string body)
        {
            Title = title;
            Body = body;
        }
    }
}
