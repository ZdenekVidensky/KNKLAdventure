using System.IO;
using KNKL.Core.SaveLoad;
using System.Xml.Linq;
namespace KNKL.Droid
{
    public class SaveLoadXml : ISaveLoadXml
    {
        /// <summary>
        /// Metoda pro ulozeni data ve formatu XML do souboru
        /// </summary>
        /// <param name="xmlData"></param>
        /// <param name="fileName"></param>
        public void SaveGame(XElement xmlData, string fileName)
        {
            var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var fullFileName = Path.Combine(documents, string.Format("{0}.xml", fileName));

            xmlData.Save(fullFileName);
        }

        /// <summary>
        /// Metoda, ktera vrati data prectena ze souboru v XML formatu.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public XElement LoadGame(string fileName)
        {
            var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var fullFileName = Path.Combine(documents, fileName);

            var result = XElement.Load(fullFileName);

            return result;
        }

        /// <summary>
        /// Vrati pravdivostni hodnotu o tom, jestli soubor existuje.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool SaveExists(string fileName)
        {
            var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var fullFileName = Path.Combine(documents, string.Format("{0}.xml",fileName));

            return File.Exists(fullFileName);
        }

        /// <summary>
        /// Metoda, ktera ulozi nastaveni v XML formatu do souboru options.xml
        /// </summary>
        /// <param name="xmlData"></param>
        public void SaveOptions(XElement xmlData)
        {
            var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var fullFilename = Path.Combine(documents, "options.xml");

            xmlData.Save(fullFilename);
        }

        /// <summary>
        /// Metoda, ktera vrati nastaveni v XML formatu ze souboru options.xml.
        /// </summary>
        /// <returns></returns>
        public XElement LoadOptions()
        {
            var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var fullFileName = Path.Combine(documents, "options.xml");

            var result = XElement.Load(fullFileName);

            return result;
        }

        /// <summary>
        /// Metoda, ktera vrati true, pokud existuje soubor options.xml, jinak false.
        /// </summary>
        /// <returns></returns>
        public bool OptionsFileExists()
        {
            var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var fullFileName = Path.Combine(documents, "options.xml");

            return File.Exists(fullFileName);
        }
    }
}