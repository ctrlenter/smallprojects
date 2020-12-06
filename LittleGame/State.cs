using System;
using System.Collections.Generic;
using System.Text;

namespace LittleGame
{
    public class State
    {
        public virtual void DrawScreen()
        {

        }

        public virtual void HandleInput(string command)
        {

        }

        public void WriteLine(object message)
        {
            Console.WriteLine(message);
        }

        public void Write(object message)
        {
            Console.Write(message);
        }
    }
}
