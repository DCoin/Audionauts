using UnityEngine;
using System.Collections;

public class EventDestroy : MonoBehaviour {

    public void EventDestroyObject()
    {
        Destroy(this.gameObject);
    }
}
