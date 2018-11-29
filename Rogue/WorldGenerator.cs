using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using LibNoise;
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
        private static List<Terrain> terrains;

        public static void Initialize ()
        {
            terrains = new List<Terrain>{new Terrain(TerrainType.Stone, "Terrain/Grass1"), //TODO: Do anything else than this
                                         new Terrain(TerrainType.Grass, "Terrain/Grass2"),
                                         new Terrain(TerrainType.Void, "Terrain/Stone1") };
        }

        public static List<Terrain> Terrains
        {
            get
            {
                return terrains;
            }
        }

        public static Texture2D TerrainTexture(TerrainType terrainType)
        {
            for (int i = 0; i <= terrains.Count; i++)
            {
                if (terrains[i].TerrainType == terrainType)
                {
                    return terrains[i].Texture2D;
                }
            }
            return null;
        }

        public static void LoadTerrainTextures(ContentManager contentManager)
        {
            for (int i = 0; i < Enum.GetNames(typeof(TerrainType)).Length; i++)
            {
                terrains[i].Texture2D = contentManager.Load<Texture2D>(terrains[i].Path);
                Debug.WriteLine(i);
            }
        }
    }

    public static class WorldGenerator
    {
        public static WorldScene GeneratePlain (Vector2 size)
        {
            Terrain[,] terrain = new Terrain[(int)size.X, (int)size.Y];
            WorldScene worldScene = new WorldScene(size);
            Perlin perl = new Perlin(3.2,noiseQuality: NoiseQuality.High);
            
            for (int x = 0; x < terrain.GetLength(0); x++)
            {
                for (int y = 0; y < terrain.GetLength(1); y++)
                {
                    terrain[x, y] = ContentHelper.Terrains[(int)(perl.GetValue((int)x, (int)y, 0.0) + 1.0)];
                    terrain[x, y].Texture2D = ContentHelper.TerrainTexture(terrain[x, y].TerrainType);
                }
            }
            worldScene.SetTerrain(terrain);
            return worldScene;
        }

        private static Terrain RandomTerrain ()
        {
            Random randomseed = new Random();
            Random random = new Random();
            return ContentHelper.Terrains[random.Next(0, 2)];
        }

        
    }
}
