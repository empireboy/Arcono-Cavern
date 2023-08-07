using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;

namespace Engine
{
    public class AssetManager
    {
        protected ContentManager contentManager;

        public AssetManager(ContentManager content)
        {
            contentManager = content;
        }

        public Texture2D GetSprite(string assetName)
        {
            if (assetName == "")
            {
                return null;
            }
            return contentManager.Load<Texture2D>(assetName);
        }

        public void PlaySound(string assetName)
        {
            SoundEffect snd = contentManager.Load<SoundEffect>(assetName);
            snd.Play();
        }

        public void PlaySound(string assetName, float volume = 1, float pitch = 0, float pan = 0)
        {
            SoundEffect snd = contentManager.Load<SoundEffect>(assetName);
            snd.Play(volume, pitch, pan);
        }

        public void PlayMusic(string assetName, bool repeat = true)
        {
            string songFileName = @"Content/" + assetName + ".ogg";
            var uri = new Uri(songFileName, UriKind.Relative);
            var song = Song.FromUri(assetName, uri);
			MediaPlayer.IsRepeating = repeat;
			MediaPlayer.Play(song);//.Load<Song>(assetName));
        }

        public ContentManager Content
        {
            get { return contentManager; }
        }
    }
}