using CocosSharp;
using KNKL.Actor;
using KNKL.Core.Items;
using KNKL.Core.SaveLoad;
using KNKL.Core.Scene;
using KNKL.Core.Subtitles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core
{
    public class GameAdventure
    {
        public Dictionary<string, InventoryItem> ItemsDatabase { get; set; }
        public Dictionary<string, string> ScenesDescriptionsDatabase { get; set; }
        public GameScene CurrentScene { get; set; }
        public GameScene PreviousScene { get; set; }
        public Dictionary<string, bool> GameConditions { get; set; }
        public bool AllowSubtitles { get; set; }
        public string Language { get; set; }
        public CCWindow MainWindow { get; set; }
        public List<string> LanguagesList { get; set; }
        public float SoundsVolume { get; set; }
        public float MusicVolume { get; set; }
        public float DialoguesVolume { get; set; }

        private static GameAdventure instance;

        public static GameAdventure Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameAdventure();
                }

                return instance;
            }
        }

        /// <summary>
        /// Metoda, ktera se zavola po zmeneni jazyka. Zmeni se zaroven i popisky predmetu.
        /// </summary>
        /// <returns></returns>
        public async Task LanguageChanged()
        {
            await InitializeGame();
            var jenik = Jenik.Instance;

            foreach(var item in jenik.Inventory)
            {
                item.Description = this.GetItemDescription(item.Name);
            }
            //Zmenim jazyk titulek a popisu v dane scene
            CurrentScene.InitializeSceneLanguage();
        }

        /// <summary>
        /// Metoda, ktera restartuje hru. Vyprazdni herni podminky a postave vymaze herni predmety z inventare.
        /// </summary>
        public void RestartGame()
        {
            var jenik = Jenik.Instance;
            jenik.Inventory.Clear();
            this.GameConditions.Clear();
        }

        private GameAdventure()
        {
            GameConditions = new Dictionary<string, bool>();
            ItemsDatabase = new Dictionary<string, InventoryItem>();
            ScenesDescriptionsDatabase = new Dictionary<string, string>();
            AllowSubtitles = true;

            //Inicializuji si seznam jazyku
            this.LanguagesList = new List<string>();
            //Naplnim seznam jazyku
            this.LanguagesList.Add("cz");
            this.LanguagesList.Add("en");

            //Jako implicitni dam prvni jazyk
            this.Language = this.LanguagesList[0];

            //Nastavim hlasitosti
            this.SoundsVolume = 1;
            this.MusicVolume = 1;
            this.DialoguesVolume = 1;
        }

        /// <summary>
        /// Zkusi nastavit dalsi jazyk. Pokud ho nastavi, vrati true, jinak false.
        /// </summary>
        /// <returns>bool</returns>
        public bool NextLanguage()
        {
            try
            {
                this.Language = this.LanguagesList[(this.LanguagesList.IndexOf(this.Language)) + 1];
                return true;
            }
            catch (Exception e) {
                return false;
            } 
        }

        /// <summary>
        /// Zkusi nastavit predchozi jazyk. Pokud ho nastavi, vrati true, jinak false.
        /// </summary>
        /// <returns>bool</returns>
        public bool PreviousLanguage()
        {
            try
            {
                this.Language = this.LanguagesList[(this.LanguagesList.IndexOf(this.Language)) - 1];
                return true;
            }
            catch (Exception e) {
                return false;
            }  
        }

        /// <summary>
        /// Metoda, ktera nastavi danou scenu jako bezici a predchozi scenu.
        /// </summary>
        /// <param name="scene"></param>
        public void SetCurrentScene(GameScene scene)
        {
            this.PreviousScene = this.CurrentScene;
            this.CurrentScene = scene;
        }

        public async Task InitializeGame()
        {
            //Vymazu nejdriv obsah obou slovniku
            ItemsDatabase.Clear();
            ScenesDescriptionsDatabase.Clear();

            //Nahraju nastaveni
            if (Options.LoadFileExists())
            {
                Options.LoadOptions();
            }

            //Nactu data o predmetech a scenach ze souboru
            await InitializeItemsDatabase();
            await InitializeScenesDatabase();
        }

        public async Task InitializeItemsDatabase()
        {
            await Task.Run(() => {
                XmlParser.LoadItems();
            });
        }

        public async Task InitializeScenesDatabase()
        {
            await Task.Run(() =>
            {
                XmlParser.LoadScenes();
            });
        }

        public string GetSceneDescription(string sceneName)
        {
            string result = string.Empty;
            ScenesDescriptionsDatabase.TryGetValue(sceneName, out result);

            return result;
        }

        /// <summary>
        /// Vrati instanci daneho predmetu z databaze.
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public InventoryItem GetItem(string itemName)
        {
            InventoryItem item;
            ItemsDatabase.TryGetValue(itemName, out item);

            return item;
        }

        /// <summary>
        /// Vrati popis predmetu z databaze.
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public string GetItemDescription(string itemName)
        {
            InventoryItem item;
            ItemsDatabase.TryGetValue(itemName, out item);

            return item.Description;
        }

        public bool GetCondition(string key)
        {
            bool result;

            //Pokud podminku nenajdu ve slovniku, vlozim tam novou a vratim ji
            if(!GameConditions.TryGetValue(key, out result))
            {
                this.GameConditions.Add(key, false);
            }

            return result;
        }

        public void SetConditionValue(string key, bool value)
        {
            //Nejdriv zjistim, jestli je podminka ve slovnku. Pokud ne, vytvori se
            this.GetCondition(key);

            //Zmenim hodnotu
            this.GameConditions[key] = value;
        }
    }
}
