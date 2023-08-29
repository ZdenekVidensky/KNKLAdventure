using CocosDenshion;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KNKL.Core.SaveLoad
{
    public class Options
    {
        public static void SaveOptions()
        {
            var game = GameAdventure.Instance;

            var rootNode = new XElement("options",
                new XElement("subtitles", game.AllowSubtitles),
                new XElement("language", game.Language),
                new XElement("sounds", game.SoundsVolume),
                new XElement("music", game.MusicVolume),
                new XElement("dialogues", game.DialoguesVolume));

            var saveOptions = AppDelegate.Container.Get<ISaveLoadXml>();

            saveOptions.SaveOptions(rootNode);
        }

        public static void LoadOptions()
        {
            var game = GameAdventure.Instance;
            var loadOptions = AppDelegate.Container.Get<ISaveLoadXml>();

            var xmlData = loadOptions.LoadOptions();

            game.AllowSubtitles = bool.Parse(xmlData.Element("subtitles").Value);
            game.Language = xmlData.Element("language").Value;
            game.SoundsVolume = float.Parse(xmlData.Element("sounds").Value);
            game.MusicVolume = float.Parse(xmlData.Element("music").Value);
            game.DialoguesVolume = float.Parse(xmlData.Element("dialogues").Value);

            CCSimpleAudioEngine.SharedEngine.BackgroundMusicVolume = game.MusicVolume;
        }

        public static bool LoadFileExists()
        {
            var loadOptions = AppDelegate.Container.Get<ISaveLoadXml>();
            return loadOptions.OptionsFileExists();
        }
    }
}
