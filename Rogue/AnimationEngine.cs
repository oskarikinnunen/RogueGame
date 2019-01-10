using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rogue
{
    /// <summary>
    /// Class that counts time for AnimatedTexture2Ds. Can be used to slow down or speed up animations without affecting gametime.
    /// </summary>
    public static class AnimationEngine
    {
        private static List<AnimatedTexture2D> animatedTextures;
        private static GameTime gameTime;
        private static SpriteBatch spriteBatch;
        private static float defaultPlaybackSpeed = 1.4f;
        private static float playbackSpeed;

        public static SpriteBatch SpriteBatch { get => spriteBatch; set => spriteBatch = value; }
        public static float PlaybackSpeed { get => playbackSpeed; set => playbackSpeed = value; }

        public static void Initialize()
        {
            playbackSpeed = defaultPlaybackSpeed;
        }

        public static void Update(GameTime gameTime)
        {
            AnimationEngine.gameTime = gameTime;

            float totalSecs = (float)gameTime.TotalGameTime.TotalSeconds;
            foreach (var at in animatedTextures)
            {
                at.Update(totalSecs);
            }
        }

        public static void AddAnimatedTexture(AnimatedTexture2D animatedTexture2D)
        {
            if (animatedTextures == null)
            {
                animatedTextures = new List<AnimatedTexture2D>();
            }
            animatedTextures.Add(animatedTexture2D);
        }
    }
}
