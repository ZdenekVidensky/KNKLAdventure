using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KNKL.Core.SaveLoad
{
    public interface ISaveLoadXml
    {
        void SaveGame(XElement xmlData, string fileName);
        XElement LoadGame(string fileName);
        bool SaveExists(string fileName);
        void SaveOptions(XElement xmlData);
        XElement LoadOptions();
        bool OptionsFileExists();
    }
}
