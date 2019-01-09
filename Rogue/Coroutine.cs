using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rogue
{

    public class WaitMilliSeconds
    {
        public float Time;

        public WaitMilliSeconds(float time)
        {
            Time = time;
        }
    }

    public static class Coroutines
    {
        private static List<IEnumerator> enumerators;

        public static void Start(IEnumerator enumerator)
        {
            if (enumerators == null)
            {
                enumerators = new List<IEnumerator>();
            }
            enumerators.Add(enumerator);
        }

        public static void Enumerate(GameTime gameTime)
        {
            if (enumerators.Count <= 0)
                return;
            
            foreach (IEnumerator enumerator in enumerators)
            {
                if (enumerator.MoveNext())
                {
                    System.Diagnostics.Debug.WriteLine(enumerator.ToString() + " yielded null");
                }
            }
        }
    }
}
