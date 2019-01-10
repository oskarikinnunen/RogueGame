using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Rogue
{
    /// <summary>
    /// Class for playing back horizontally laid out spritesheets.
    /// </summary>
    public class AnimatedTexture2D : Component, Drawable
    {
        private GameObject worldObject;

        private Texture2D baseTexture;
        private readonly int width;
        private float speed; // Frames per second
        private readonly int totalFrames; // How many frames this spritesheet contains.
        private int frameIndex; //Index of current frame
        private float nextFrameChange; //Gametime in seconds when the next framechange is due

        private bool pingPong = false;
        private bool reverse;

        public Texture2D BaseTexture { get => baseTexture; set => baseTexture = value; }
        public override GameObject WorldObject { get => worldObject; set => worldObject = value; }

        /// <summary>
        /// If set to true, animation reverses after reaching the last frame
        /// </summary>
        public bool PingPong { get => pingPong; set => pingPong = value; }


        public void Draw()
        {
            AnimationEngine.SpriteBatch.Draw(baseTexture, new Rectangle(new Point((int)worldObject.Position.X,(int)worldObject.Position.Y), new Point(32, 32)),
                                                          new Rectangle(new Point(frameIndex * width, 0), new Point(32, 32)),
                                                          Color.White);
        }

        /// <param name="width">Width of a single texture in spritesheet. </param>
        public AnimatedTexture2D(Texture2D texture, int width)
        {
            this.baseTexture = texture;
            this.width = width;
            this.totalFrames = texture.Width / width;
            this.StartAnimation(3f);
            AnimationEngine.AddAnimatedTexture(this);
        }

        public void Update(float time)
        {
            if (!pingPong)
            {
                if (time > nextFrameChange) //Checks if it is time to change to the next frame of animation
                {
                    frameIndex++;
                    nextFrameChange = time + 1 / (speed * AnimationEngine.PlaybackSpeed);
                    if (totalFrames - 1 < frameIndex) // loops back to the first frame
                    {
                        frameIndex = 0;
                    }
                }
            }
            else
            {
                if (time > nextFrameChange) //Checks if it is time to change to the next frame of animation
                {
                    if (!reverse)
                    {
                        frameIndex++;
                    }
                    else
                    {
                        frameIndex--;
                    }

                    nextFrameChange = time + 1 / (speed * AnimationEngine.PlaybackSpeed);

                    if (totalFrames - 1 < frameIndex + 1 || frameIndex < 1) // starts playing backwards
                    {
                        //frameIndex = Math.Min(Math.Max(frameIndex, 0), totalFrames - 1);
                        reverse = !reverse;
                    }
                }
            }
            //Draw();
        }

        public override void OnAddToGameObject(GameObject gameObject)
        {
            this.WorldObject = gameObject;
        }

        public void StartAnimation()
        {
            this.speed = AnimationEngine.PlaybackSpeed;
            frameIndex = 0;
        }

        /// <param name="speed">Frames per second of the animation. </param>
        public void StartAnimation(float speed)
        {
            this.speed = speed;
            frameIndex = 0;
        }

        /// <param name="startFrame">Frame where the animation starts from. </param>
        public void StartAnimation(int startFrame)
        {
            this.speed = AnimationEngine.PlaybackSpeed;
            frameIndex = 0;
        }

        /// <param name="speed">Frames per second of the animation. </param>
        /// <param name="startFrame">Frame where the animation starts from. </param>
        public void StartAnimation(float speed, int startFrame)
        {
            this.speed = speed;
            frameIndex = startFrame;
        }

    }
}
