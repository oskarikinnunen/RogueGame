using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    namespace XNAExtensions
    {
        public static class XNAExtensions
        {
            public static void DrawString(this SpriteBatch batch, SpriteFont spriteFont, string text, Vector2 position, Color color, float depth)
            {
                batch.DrawString(spriteFont, text, position, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, depth);
            }

            public static Point ToPoint (this Vector2 vector2)
            {
                return new Point((int)vector2.X, (int)vector2.Y);
            }

            public static Vector2 ToVector2 (this Point point)
            {
                return new Vector2(point.X, point.X);
            }

            public static Color Multiply (this Color color, int multiplier)
            {
                int newR = Math.Min(color.R * multiplier, 255);
                int newG = Math.Min(color.G * multiplier, 255);
                int newB = Math.Min(color.B * multiplier, 255);
                return new Color(newR,newG,newB, color.A);
            }

            public static Color Multiply(this Color color, float multiplier)
            {
                int newR = (int)Math.Min(color.R * multiplier, 255);
                int newG = (int)Math.Min(color.G * multiplier, 255);
                int newB = (int)Math.Min(color.B * multiplier, 255);
                return new Color(newR, newG, newB, color.A);
            }
        }
    }
}
