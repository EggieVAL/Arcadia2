using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;

namespace Yolk.GameObjects
{
    public abstract class GameObject
    {
        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime)
        {
        }
    }
}
