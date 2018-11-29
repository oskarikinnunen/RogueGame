using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    public enum TerrainType
    {
        Void,
        Grass,
        Stone
    }

    public class Terrain
    {
        private TerrainType terrainType;
        private string path;
        private Texture2D texture2D;

        public TerrainType TerrainType { get => terrainType; set => terrainType = value; }
        public string Path { get => path; set => path = value; }
        public Texture2D Texture2D { get => texture2D; set => texture2D = value; }

        
        public Terrain(TerrainType terrainType, string path)
        {
            this.terrainType = terrainType;
            this.path = path;
        }

    }

    public static class ContentHelper
    {
        public static List<Terrain> Terrains
        {
            get
            {
                List<Terrain> terrains = new List<Terrain>{new Terrain(TerrainType.Stone, "Terrain/Stone0"),
                                                           new Terrain(TerrainType.Grass, "Terrain/Grass0"),
                                                           new Terrain(TerrainType.Void, "Terrain/Void0") };
                return terrains;
            }
        }

        public static void LoadTerrainTextures(ContentManager contentManager)
        {
            for (int i = 0; i <= Enum.GetNames(typeof(TerrainType)).Length; i++)
            {
                ContentHelper.Terrains[i].Texture2D = contentManager.Load<Texture2D>(Terrains[i].Path);
            }
        }
    }

    public static class WorldGenerator
    {
        public static WorldScene GeneratePlain (Vector2 size)
        {
            Terrain[,] terrain = new Terrain[(int)size.X, (int)size.Y];
            WorldScene worldScene = new WorldScene(size);

            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    terrain[x, y] = RandomTerrain();
                }               
            }
            worldScene.SetTerrain(terrain);
            // TODO: For each unique terrain type, load the texture2d for the Terrain class. Use it in Camera for rendering the terrain.
            return worldScene;
        }

        private static Terrain RandomTerrain ()
        {
            Random random = new Random(20);
            return ContentHelper.Terrains[random.Next(0, ContentHelper.Terrains.Count)];
        }

        
    }
}
