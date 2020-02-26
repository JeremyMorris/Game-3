using System;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;

using TInput = ContentExtension.TilemapContent;

namespace ContentExtension
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>

    [ContentImporter(".tmx", DisplayName = "TMX Importer - Tiled", DefaultProcessor = "TilemapProcessor - Tiled")]
    public class TilemapImporter : ContentImporter<TInput>
    {

        public override TInput Import(string filename, ContentImporterContext context)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            
            XmlNode tilemap = document.SelectSingleNode("//map");
            var mapcontent = document.SelectSingleNode("//data");

            string name = filename;
            int width = int.Parse(tilemap.Attributes["width"].Value);
            int height = int.Parse(tilemap.Attributes["height"].Value);
            string content = mapcontent.InnerText;
            int[] mapData = Array.ConvertAll(content.Split(','), int.Parse);

            return new TilemapContent()
            {
                Name = name,
                Width = width,
                Height = height,
                MapData = mapData
            }; ;
        }

    }

}