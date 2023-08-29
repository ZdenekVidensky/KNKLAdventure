using CocosSharp;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core.Media
{
    public class VoicePlayer
    {
        SoundEffect effect;
        SoundEffectInstance sfxInstance;

        public bool Playing
        {
            get
            {
                if(sfxInstance != null)
                {
                    return (sfxInstance.State == SoundState.Playing);
                }
                return false;
            }
        }

        public void Open(string fileName)
        {
            try
            {
                effect = CCContentManager.SharedContentManager.Load<SoundEffect>(fileName);
            }
            catch (Exception)
            {
                string srcfile = fileName;
                if (srcfile.IndexOf('.') > -1)
                {
                    srcfile = srcfile.Substring(0, srcfile.LastIndexOf('.'));
                    effect = CCContentManager.SharedContentManager.Load<SoundEffect>(srcfile);
                }
            }
        }

        public void Play(bool loop = false)
        {
            if(effect == null)
            {
                return;
            }

           sfxInstance = effect.CreateInstance();

            sfxInstance.IsLooped = loop;

            if(sfxInstance != null)
            {
                sfxInstance.Play();
            }
        }

        public void Stop()
        {
            if(sfxInstance != null)
            {
                if(sfxInstance.State == SoundState.Playing)
                {
                    sfxInstance.Stop();
                }
            }
        }
    }
}
