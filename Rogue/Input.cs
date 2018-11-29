using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    /// <summary>
    /// Class for all kinds of Input helper methods
    /// </summary>
    public static class Input
    {
        //static KeyboardState keyboardState;
        private static KeyValuePair<Keys, bool>[] keyStates;
        private static List<Keys> thisFramePushedKeys; //keys that are being pushed down this frame and haven't been pushed down for a longer duration

        public static void Update(KeyboardState keyboardState)
        {
            thisFramePushedKeys = new List<Keys>();
            if (keyStates == null) //No need for initialization method this way
            {
                keyStates = new KeyValuePair<Keys, bool>[Enum.GetValues(typeof(Keys)).Length];
            }
            KeyValuePair<Keys, bool>[] oldKeyStates = keyStates;
            keyStates = new KeyValuePair<Keys, bool>[Enum.GetValues(typeof(Keys)).Length];

            for (int i = 0; i < Enum.GetValues(typeof(Keys)).Length; i++ )
            {
                
                Keys current = (Keys)i;
                keyStates[i] = new KeyValuePair<Keys, bool>(current, keyboardState.IsKeyDown(current));

                if (oldKeyStates[i].Value == false && keyStates[i].Value == true) //if this key was not pressed the previous frame and is pressed this frame
                {                                                                    //,
                    thisFramePushedKeys.Add(current);                                //add it to the list
                }
            }
        }

        public static bool KeyPushed(Keys key) => thisFramePushedKeys.Contains(key);

    }
}
