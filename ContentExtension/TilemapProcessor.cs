using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;

using TInput = ContentExtension.TilemapContent;
using TOutput = ContentExtension.TilemapContent;

namespace ContentExtension
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to tilemap data 
    /// </summary>
    [ContentProcessor(DisplayName = "Tilemap Processor - Tiled")]
    public class TilemapProcessor : ContentProcessor<TInput, TOutput>
    {

        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            return input;
        }
    }
}