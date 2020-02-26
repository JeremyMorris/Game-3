namespace ContentExtension
{
    public class TilemapContent
    {
        public TilemapContent()
        {
        }

        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int[] MapData { get; set; }
    }
}