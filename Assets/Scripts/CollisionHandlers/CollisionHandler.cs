using UnityEngine;

namespace Assets.Scripts.CollisionHandlers
{
    public class CollisionHandler : MonoBehaviour {

        public delegate void CollisionEventHandler(object sender);

        public event CollisionEventHandler Collision;

        public virtual void OnCollision()
        {
            if (Collision != null)
            {
                Collision(this);
            }
        }

    }
}
