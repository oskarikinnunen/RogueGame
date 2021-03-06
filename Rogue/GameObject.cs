﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    [Serializable]
    public class GameObject
    {
        private Vector2 position;

        public List<Component> Components;

        public Vector2 Position { get => position; set => position = value; }

        public GameObject (Vector2 position)
        {
            Components = new List<Component>();
            this.position = position;
            RogueGame.LoadedWorldScene.AddGameObject(this);
        }

        public GameObject(Vector2 position, Component component1)
        {
            this.position = position;

            Components = new List<Component>();            
            this.Components.Add(component1);
            component1.OnAddToGameObject(this);
            
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

        public Drawable DrawableComponent
        {
            get
            {
                foreach (Component comp in Components)
                {
                    Drawable d = comp as Drawable;
                    if (d != null)
                        return d;
                }
                return null;
            }
        }

    }

    public abstract class Component //TODO: Move to own file
    {
        public abstract GameObject WorldObject { get; set; }

        public abstract void OnAddToGameObject(GameObject gameObject);
    }

    public interface Drawable
    {
        void Draw();
    }

}
