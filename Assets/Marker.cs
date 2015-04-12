using UnityEngine;

namespace Assets
{

    [RequireComponent(typeof(Circle))]
    public class Marker : MonoBehaviour
    {

        public Transform Target;

        private float _radius;

        private Circle _circle;

        // Use this for initialization
        void Start ()
        {

            _circle = GetComponent<Circle>();
            _radius = _circle.Radius;

        }
	
        // Update is called once per frame
        void LateUpdate ()
        {

            var dz = transform.position.z - Target.transform.position.z;

  //          if (dz > 0f)
    //        {
                _circle.Radius = _radius + Mathf.Abs(dz);
                _circle.Refresh();
      //      }
 
        }
    }
}
