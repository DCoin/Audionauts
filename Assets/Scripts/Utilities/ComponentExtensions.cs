using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts.Utilities
{
    public static class ComponentExtensions {
        public static IEnumerable<T> GetComponentsInImmediateChildren<T>(this Component parent) where T : Component {
            return 
                from component 
                    in parent.GetComponentsInChildren<T>() 
                where component.transform.parent == parent.transform 
                select component;
        }
    }
}
