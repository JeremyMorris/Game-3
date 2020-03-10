using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using TRead = MapLibrary.Tilemap;

namespace MapLibrary
{
    public class TilemapReader : ContentTypeReader<TRead>
    {
        protected override TRead Read(ContentReader input, TRead existingInstance)
        {
            var height = input.ReadInt32();
            var width = input.ReadInt32();
            var count = width * height;

            // Read in the tiles - the number will vary based on the tilemap 
            var mapData = new int[count];
            for (int i = 0; i < count; i++)
            {
                mapData[i] = input.ReadInt32();
            }

            // Construct and return the tilemap
            return new Tilemap(width, height, mapData);
        }
    }
}