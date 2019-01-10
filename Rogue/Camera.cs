using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue.XNAExtensions;

namespace Rogue
{
    public class Camera //TODO: Add method that draws all AnimatedTexture2Ds in view
    {
        private float zoom;
        private GameObject followable;
        private SpriteBatch spriteBatch;
        private Viewport viewport;
        private Vector3 offset;
        
        public float Zoom { get => zoom; set => zoom = value; }
        public Vector2 Location { get; set; }
        public float Rotation { get; set; }

        private Matrix transformMatrix
        {
            get
            {
                return
                   Matrix.CreateTranslation((new Vector3(-followable.Position.X - 16, -followable.Position.Y -16, 0)) + offset) *         //Hardcoded +16 is added because the sprites are 32x32 and we want the camera centered on the tile
                                            Matrix.CreateRotationZ(0) *
                                            Matrix.CreateScale(zoom) *
                                            Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
            }
        }

        public Matrix TransformMatrix { get { return transformMatrix; } }

        public static Camera Primary
        {
            get;
            set;
        }

        public Camera(SpriteBatch spriteBatch, Viewport viewport)
        {
            this.spriteBatch = spriteBatch;
            this.viewport = viewport;
            zoom = 1f;
            offset = Vector3.Zero;
        }

        public IEnumerator InventoryZoom()
        {
            int time = 0;
            while (time < 10)
            {
                time++;
                Debug.WriteLine(time);
                yield return null;
            }
            Debug.WriteLine(time + "ended");
        }

        public void Follow(GameObject gameObject)
        {
            followable = gameObject;
        }

        public void DrawGameObjects ()
        {
            List<GameObject> list = RogueGame.LoadedWorldScene.GameObjects;
            foreach (GameObject go in list)
            {
                Drawable drComponent = go.DrawableComponent;
                if (drComponent != null)
                {
                    drComponent.Draw();
                }
            }
        }

        public void DrawTerrain()
        {
            Terrain[,] terrain = RogueGame.LoadedWorldScene.Terrain;
            //Debug.WriteLine(terrain[0, 13].Depth);
            for (int x = 0; x < terrain.GetLength(0); x++)
            {
                for (int y = 0; y < terrain.GetLength(1); y++)
                {
                    //Debug.WriteLine(terrain[x, y].Depth);
                    //Color depthColor = MultiplyColor(Color.White, terrain[x,y].Depth);
                    AnimationEngine.SpriteBatch.Draw(terrain[x,y].Texture2D, new Rectangle(new Point(x * 32, y * 32), new Point(32, 32)), Color.Brown.Multiply(4f));
                }
            }
            //AnimationEngine.SpriteBatch.DrawString(spriteFont, )
        }
    }
}
