using UnityEngine;

namespace Assets.Scripts.CollisionHandlers
{
    public class CollisionHandler : MonoBehaviour {

        public delegate void CollisionEventHandler(object sender, bool pre, bool success);

        public event CollisionEventHandler Collision;

        public virtual void OnCollision(bool pre, bool success)
        {
            if (Collision != null)
            {
                Collision(this, pre, success);
            }
        }

    }
}
