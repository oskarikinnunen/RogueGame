using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    [Serializable]
    public class Item : Component
    {
        private GameObject worldObject;
        private string name;
        private int weight;
        private int value;

        public Item(string name = "name", int weight = 0, int value = 0)
        {
            this.name = name;
            this.weight = weight;
            this.value = value;
        }

        public override GameObject WorldObject { get => worldObject; set => worldObject = value; }

        public override void OnAddToGameObject(GameObject gameObject)
        {
            this.worldObject = gameObject;
        }

    }
}
