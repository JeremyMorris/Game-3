namespace MapLibrary
{
    public class Tilemap
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int[] MapData { get; set; }

        public Tilemap(int width, int height, int[] mapData)
        {
            this.Width = width;
            this.Height = height;
            this.MapData = mapData;
        }
    }
}