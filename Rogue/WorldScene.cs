using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    [Serializable()]
    public class WorldScene
    {

        private Vector2 size;
        private List<GameObject> gameObjects;
        private Terrain[,] terrain;

        public Terrain[,] Terrain { get => terrain; }

        public WorldScene(Vector2 size)
        {
            gameObjects = new List<GameObject>();
            this.size = size;
        }
        
        public void SetTerrain (Terrain[,] terrain)
        {
            this.terrain = terrain;
        }

        public void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }
    }
}
