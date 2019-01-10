using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.XNAExtensions;
using Microsoft.Xna.Framework.Graphics;

namespace Rogue
{
    public static class UI
    {
        private static Texture2D whiteTex;

        public static void InitializeTextures()
        {
            //Create a 1x1 white texture for creating simple shapes in UI
            whiteTex = new Texture2D(RogueGame.Graphics.GraphicsDevice, 1, 1);
            whiteTex.SetData(new Color[1] { Color.White });
        }

        public static void DrawStringOnScreen(Vector2 location, string str)
        {
            RogueGame.UISpriteBatch.DrawString(RogueGame.SprFont, str, location, Color.White, 1f);
        }

        public static void DrawBoxForString(Vector2 location, string str)
        {
            const int fontSize = 16; //Size 12 font is 16 pixels wide-ish
            
            int width = str.Length * fontSize;
            Rectangle rect = new Rectangle(location.ToPoint(), new Point(width, 20));
            RogueGame.UISpriteBatch.Draw(whiteTex, rect, Color.Black);
        }

        public static void DrawBox(Vector2 location, Vector2 size)
        {
            Rectangle rect = new Rectangle(location.ToPoint(), size.ToPoint());
            RogueGame.UISpriteBatch.Draw(whiteTex, rect, Color.White);
        }
    }
}
