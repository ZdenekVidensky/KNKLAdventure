using KNKL.Actor;
using KNKL.Scenes.Nadrazi;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KNKL.Core.SaveLoad
{
    public class SaveLoadGame
    {
        public static bool SaveExists(string fileName)
        {
            var saveGame = AppDelegate.Container.Get<ISaveLoadXml>();
            return saveGame.SaveExists(fileName);
        }

        public static string SaveName(string fileName)
        {
            var loadGame = AppDelegate.Container.Get<ISaveLoadXml>();
            XElement root = loadGame.LoadGame(string.Format("{0}.xml", fileName));

            return root.Element("file_name").Value;
        }

        public static string SaveGame(string fileName)
        {
            var game = GameAdventure.Instance;
            var jenik = Jenik.Instance;
            Random rand = new Random();

            var gameDescription = game.CurrentScene.Description;
            var date = DateTime.Now;

            string file_name = string.Format("{0} - ({1: dd.MM.yyyy H:mm:ss})", gameDescription, date);

            var rootNode = new XElement("save",
                new XElement("file_name", file_name),
                new XElement("scene", new XAttribute("name", game.CurrentScene.Name),
                    new XAttribute("description", gameDescription)),
                new XElement("date", date));

            var jenikNode = new XElement("jenik",
                new XElement("x", jenik.Sprite.PositionX),
                new XElement("y", jenik.Sprite.PositionY),
                new XElement("direction", jenik.Direction.ToString()),
                new XElement("active_item", game.CurrentScene.ActiveItemSprite.Name));

            var inventoryNode = new XElement("inventory");
                foreach(var item in jenik.Inventory)
                {
                    inventoryNode.Add(new XElement("item", item.Name));
                }
            jenikNode.Add(inventoryNode);

            //Pridam herni podminky
            var conditionsNode = new XElement("conditions");
                foreach(var item in game.GameConditions)
                {
                    conditionsNode.Add(new XElement("condition", new XAttribute("name", item.Key), new XAttribute("value", item.Value)));
                }

            //Dam to vsechno dohromady
            rootNode.Add(jenikNode, conditionsNode);

            var saveGame = AppDelegate.Container.Get<ISaveLoadXml>();

            saveGame.SaveGame(rootNode, fileName);

            //Vratim jmeno souboru
            return file_name;
        }

        public static string LoadGame(string fileName)
        {
            var loadGame = AppDelegate.Container.Get<ISaveLoadXml>();
            var xmlData = loadGame.LoadGame(fileName);

            var jenik = Jenik.Instance;
            var game = GameAdventure.Instance;

            //Vyberu scenu a nahraj ji do instance Game
            var sceneName = xmlData.Element("scene").Attribute("name").Value;

            //Zpracuji Jenikova data
            var jenikNode = xmlData.Element("jenik");
            jenik.Sprite.PositionX = float.Parse(jenikNode.Element("x").Value);
            jenik.Sprite.PositionY = float.Parse(jenikNode.Element("y").Value);
            jenik.Direction = jenik.StringToDirection(jenikNode.Element("direction").Value);
            //game.CurrentScene.SetActiveItem(jenikNode.Element("active_item").Value);

            //Nahraju polozky z inventare
            var inventoryNodes = jenikNode.Element("inventory").Elements();
            foreach(var item in inventoryNodes)
            {
                jenik.AddItem(item.Value);
            }

            //Nahraju podminky
            var conditionsNode = xmlData.Element("conditions");
            foreach(var item in conditionsNode.Elements())
            {
                game.SetConditionValue(item.Attribute("name").Value, bool.Parse(item.Attribute("value").Value));
            }

            //Jakmile je vsechno nacteno, vratim nazev sceny
            return sceneName;
        }

        /// <summary>
        /// Podle nazvu sceny urci CurrentScene v instanci GameAdventure.
        /// </summary>
        /// <param name="sceneName"></param>
        public static void LoadScene(string sceneName)
        {
            var game = GameAdventure.Instance;

            switch (sceneName)
            {
                case "01_nadrazi":
                    game.CurrentScene = new NadraziScene(game.MainWindow);
                    break;
            }
        }
    }
}
