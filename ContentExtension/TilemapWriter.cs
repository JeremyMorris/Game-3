using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using TWrite = ContentExtension.TilemapContent;

namespace ContentExtension
{
    /// <summary>
    /// A ContentTypeWriter for the TiledSpriteSheetContent type
    /// </summary>
    [ContentTypeWriter]
    public class TilemapWriter : ContentTypeWriter<TWrite>
    {

        /// <summary>
        /// Write the binary (xnb) file corresponding to the supplied 
        /// TilemapContent that will be imported into our game
        /// as a Tilemap
        /// </summary>
        /// <param name="output">The ContentWriter that writes the binary output</param>
        /// <param name="value">The TilemapContent we are writing</param>
        protected override void Write(ContentWriter output, TWrite value)
        {
            // Write the tile width & height 
            output.Write(value.Height);
            output.Write(value.Width);

            int count = value.Height * value.Width;

            // Write the individual tile data
            for (int i = 0; i < count; i++)
            {
                output.Write(value.MapData[i]);
            }

        }

        /// <summary>
        /// Gets the reader needed to read the binary content written by this writer
        /// </summary>
        /// <param name="targetPlatform"></param>
        /// <returns></returns>
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "MapLibrary.TilemapReader, MapLibrary";
        }
    }
}