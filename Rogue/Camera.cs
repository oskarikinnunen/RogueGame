using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rogue
{
    public class Camera
    {
        private float zoom;
        private Rectangle rectangle;
        private GameObject followable;
        private SpriteBatch spriteBatch;
        private Viewport viewport;
        
        public float Zoom { get { return zoom; } set { zoom = value; } }
        public Vector2 Location { get; set; }
        public float Rotation { get; set; }

        public Rectangle Bounds { get; set; }

        private Matrix transformMatrix
        {
            get
            {
                return
                   Matrix.CreateTranslation(new Vector3(-followable.Position.X, -followable.Position.Y, 0)) *
                   Matrix.CreateRotationZ(0) *
                   Matrix.CreateScale(zoom) *
                   Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            }
        }

        public static Camera Primary
        {
            get;
            set;
        }

        public Camera(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public Camera(SpriteBatch spriteBatch, float zoom)
        {
            this.spriteBatch = spriteBatch;
            this.zoom = zoom;
        }

        public void Follow(GameObject gameObject)
        {
            followable = gameObject;
        }

        public void Update() //Should be called on games update function
        {
            //followable = gameObject;
        }

        public void DrawTerrain(SpriteBatch spriteBatch)
        {
            Terrain[,] terrain = RogueGame.LoadedWorldScene.Terrain;
            for (int x = 0; x < terrain.GetLength(0); x++)
            {
                for (int y = 0; y < terrain.GetLength(1); y++)
                {
                   
                }
            }
                
        }
    }
}
