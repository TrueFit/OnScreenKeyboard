namespace Simple.Library.Misc
{
    /// <summary>
    /// POCO to return direction and delta change for keyboard traversal
    /// </summary>
    public class SearchMove
    {
        public string Direction { get; set; }
        public int Delta { get; set; }
    }
}
