using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;

namespace Yolk.Engine
{
    public sealed class CoroutineManager
    {
        private List<Coroutine> coroutines = new();

        public int Count => coroutines.Count;

        public static bool IsPaused(CoroutineKey key)
        {
            return key.IsPaused();
        }

        public static bool IsRunning(CoroutineKey key)
        {
            return key.IsRunning();
        }

        public static bool Terminate(CoroutineKey key)
        {
            return key.Terminate();
        }

        public bool IsRunning(Coroutine coroutine)
        {
            return coroutines.Contains(coroutine);
        }

        public CoroutineKey Run(Coroutine coroutine)
        {
            coroutines.Add(coroutine);
            return new CoroutineKey(this, coroutine);
        }

        public CoroutineKey Run(IEnumerator task, float delay)
        {
            return Run(new Coroutine(task, delay));
        }

        public CoroutineKey Run(IEnumerator task)
        {
            return Run(new Coroutine(task));
        }

        public bool Terminate(Coroutine coroutine)
        {
            for (int i = 0; i < Count; ++i)
            {
                if (coroutines[i] == coroutine)
                {
                    coroutines.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void Terminate()
        {
            coroutines.Clear();
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < Count; ++i)
            {
                if (coroutines[i].Update(gameTime))
                {
                    coroutines.RemoveAt(i--);
                }
            }
        }
    }
}
