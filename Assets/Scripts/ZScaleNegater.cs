using System;
using UnityEngine;

namespace Assets.Scripts
{
    [ExecuteInEditMode]
    public class ZScaleNegater : MonoBehaviour {
	
        private void Update () {

            var scale = transform.localScale;

            var z = transform.parent.localScale.z;

            if (Math.Abs(z) < float.Epsilon)
            {
                scale.z = 1f;
            }
            else
            {
                scale.z = 1f/z;
            }

            transform.localScale = scale;
	
        }
    }
}
