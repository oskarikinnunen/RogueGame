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
        private double depth;

        public TerrainType TerrainType { get => terrainType; set => terrainType = value; }
        public string Path { get => path; set => path = value; }
        public double Depth { get => depth; set => depth = value; }
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
            terrains = new List<Terrain>{new Terrain(TerrainType.Stone, "Terrain/Grass1"), //TODO: Do anything else than this, eg. Save terrain attributes to JSON or XML
                                         new Terrain(TerrainType.Grass, "Terrain/Grass2"),
                                         new Terrain(TerrainType.Void, "Terrain/Stone1")};
        }

        public static List<Terrain> Terrains
        {
            get { return terrains ; }
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
            }
        }
    }

    public static class WorldGenerator
    {
        public static WorldScene NormalTerrain (Vector2 size)
        {
            Terrain[,] terrain = new Terrain[(int)size.X, (int)size.Y];
            WorldScene worldScene = new WorldScene(size);
            Perlin perl = new Perlin(5.5,noiseQuality: NoiseQuality.High);
            Debug.WriteLine(size + "size");
            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    double noiseValue = perl.GetValue(x, y, 0.0);
                    terrain[x,y] = ContentHelper.Terrains[(int)(noiseValue + 1.0)]; // -1 to 1 Range to 0-2 range
                    terrain[x,y].Depth = (noiseValue + 1) / 2; //-1 to 1 range to 0-1 range, This thing is possessed, don't touch
                    //terrain[x, y].Depth = 2.12124124124142124;s
                    Debug.WriteLine("Coordinates: " + x + "," + y + " --> " + terrain[x,y].Depth);
                }
                //Debug.WriteLine(terrain[x, 0].Depth);
            }

            worldScene.SetTerrain(terrain);
            return worldScene;
        }
        
    }
}
