using UnityEngine;

using Assets.Scripts.Managers;

public class ControlLoader : MonoBehaviour {

    public ControllerManager controllerManagerPrefab;

    void Awake() {

        if(ControllerManager.Controllers != null)
            return;

        Instantiate(controllerManagerPrefab);

    }

}
