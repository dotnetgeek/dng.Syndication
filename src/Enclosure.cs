namespace dng.Syndication
{
    public class Enclosure
    {
        public Enclosure()
        {
            Length = 0;
        }

        public string Url { get; set; }

        public int Length { get; set; }

        public string MediaType { get; set; }
    }
}