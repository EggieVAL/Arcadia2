using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Yolk.Engine
{
    public sealed class Coroutine : IEquatable<Coroutine>
    {
        private IEnumerator task;
        private float delay;

        public Coroutine(IEnumerator task, float delay)
        {
            this.task = task;
            this.delay = delay;
            Paused = false;
        }

        public bool Paused { get; set; }

        public Coroutine(IEnumerator task) : this(task, 0f)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Coroutine coroutine, Coroutine other)
        {
            return coroutine.task == other.task;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Coroutine coroutine, Coroutine other)
        {
            return !(coroutine == other);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([NotNullWhen(true)] object obj)
        {
            return (obj is Coroutine other) && Equals(other);
        }

        public bool Equals(Coroutine other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return task.GetHashCode();
        }

        public bool Update(GameTime gameTime)
        {
            if (Paused)
            {
                return false;
            }
            if (delay > 0f)
            {
                delay -= gameTime.ElapsedGameTime.Milliseconds;
                return false;
            }
            return task is null || !MoveNext(task);
        }

        private bool MoveNext(IEnumerator task)
        {
            if (task.Current is IEnumerator current)
            {
                if (MoveNext(current))
                {
                    return true;
                }
                this.delay = 0f;
            }

            bool result = task.MoveNext();
            if (task.Current is float delay)
            {
                this.delay = delay;
            }
            return result;
        }
    }
}
