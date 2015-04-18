using UnityEngine;

namespace Assets.Scripts.CollisionHandlers
{
    public class CollisionHandler : MonoBehaviour {

        public delegate void CollisionEventHandler(object sender, bool pre);

        public event CollisionEventHandler Collision;

        public virtual void OnCollision(bool pre)
        {
            if (Collision != null)
            {
                Collision(this, pre);
            }
        }

    }
}
