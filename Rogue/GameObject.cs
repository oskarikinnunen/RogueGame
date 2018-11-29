using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    public class GameObject
    {
        private Vector2 position;

        public List<Component> Components;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public GameObject (Vector2 position) {
            Components = new List<Component>();
            this.position = position;
            RogueGame.LoadedWorldScene.AddGameObject(this);
        }

        public GameObject(Vector2 position, Component component1)
        {
            Components = new List<Component>();
            this.position = position;
            this.Components.Add(component1);
            Console.WriteLine(component1.GetType().ToString());
            RogueGame.LoadedWorldScene.AddGameObject(this);
        }

        public Component GetComponent(Type type)
        {
            foreach (Component component in Components)
            {
                if (component.GetType() == type)
                {
                    return component;
                }
            }
            return null;
        }

        public void AddComponent<T> (T t) where T : Component
        {
            Components.Add(t);
            t.OnAddToGameObject(this);
        }

    }

    public abstract class Component
    {
        public abstract GameObject WorldObject { get; set; }

        public abstract void OnAddToGameObject(GameObject gameObject);
    }
}
