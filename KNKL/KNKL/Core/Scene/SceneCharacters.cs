using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core.Scene
{
    public partial class GameScene : CCScene
    {
        private Dictionary<string, Character> Characters { get; set; }

        public void AddCharacterToScene(Character character)
        {
            this.Characters.Add(character.Name, character);
        }

        public Character GetCharacterFromScene(string name)
        {
            Character result;
            this.Characters.TryGetValue(name, out result);
            return result;
        }
    }
}
