using CocosSharp;
using KNKL.Core.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace KNKL.Core.Subtitles
{
    public class XmlParser
    {
        /// <summary>
        /// Nahraje do pameti texty pro danou scenu podle jazyka
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="Cache"></param>
        public static void LoadText(string fileName, Dictionary<int, Subtitle> Cache)
        {
            //Vytahnu si instanci GameAdventure kvuli zobrazovanemu jazyku
            var game = GameAdventure.Instance;

            Stream stream = CCContentManager.SharedContentManager.GetAssetStream(string.Format("Subtitles/{0}/{1}.xml", game.Language, fileName));

            using (XmlReader reader = XmlReader.Create(stream))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if(reader.Name == "talk")
                    {
                        XElement element = XNode.ReadFrom(reader) as XElement;
                        
                        int Id = int.Parse(element.Element("id").Value);
                        string text = element.Element("text").Value.ToString();
                        string sound = element.Element("sound").Value.ToString();

                        Cache.Add(Id, new Subtitle(){ Text = text, Sound = sound });
                    }
                }
            }
        }

        /// <summary>
        /// Nahraje do pameti predmety podle jazyka
        /// </summary>
        public static void LoadItems()
        {
            //Vytahnu si instanci GameAdventure kvuli zobrazovanemu jazyku
            var game = GameAdventure.Instance;

            Stream stream = CCContentManager.SharedContentManager.GetAssetStream(string.Format("Subtitles/{0}/Items.xml", game.Language));

            using (XmlReader reader = XmlReader.Create(stream))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.Name == "item")
                    {
                        XElement element = XNode.ReadFrom(reader) as XElement;

                        string name = element.Element("name").Value.ToString();
                        string description = element.Element("description").Value.ToString();

                        game.ItemsDatabase.Add(name, new InventoryItem(name, description));
                    }
                }
            }
        }

        /// <summary>
        /// Nahraje do pameti predmety podle jazyka
        /// </summary>
        public static void LoadScenes()
        {
            //Vytahnu si instanci GameAdventure kvuli zobrazovanemu jazyku
            var game = GameAdventure.Instance;

            Stream stream = CCContentManager.SharedContentManager.GetAssetStream(string.Format("Subtitles/{0}/Scenes.xml", game.Language));

            using (XmlReader reader = XmlReader.Create(stream))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.Name == "scene")
                    {
                        XElement element = XNode.ReadFrom(reader) as XElement;

                        string name = element.Element("name").Value.ToString();
                        string description = element.Element("description").Value.ToString();

                        game.ScenesDescriptionsDatabase.Add(name, description);
                    }
                }
            }
        }
    }
}
