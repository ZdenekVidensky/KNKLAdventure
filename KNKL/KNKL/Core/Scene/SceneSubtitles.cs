using CocosSharp;
using KNKL.Core.Subtitles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core.Scene
{
    public partial class GameScene : CCScene
    {
        public Dictionary<int, Subtitle> SceneSubtitles { get; set; }

        public Subtitle GetSubtitles(int id)
        {
            Subtitle result;
            if(!SceneSubtitles.TryGetValue(id, out result))
            {
                throw new Exception("Neexistujici titulky!");
            }

            return result;
        }
    }
}
