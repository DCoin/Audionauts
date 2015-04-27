using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using System.Linq;

public class MenuHandler : MonoBehaviour {

    private Transform player1;
    private Transform player2;

    public Transform ActiveMenu;
    public Transform NextMenu = null;

    public float TransitionSpeed;
    public float TransitionDistance;

    private IEnumerable<Transform> Children
    {
        get { return transform.GetComponentsInChildren<Transform>().Where(c => c.parent == transform); }
    }

    // Use this for initialization
	void Start () {

	    player1 = StageManager.Instance.Player1.transform;
	    player2 = StageManager.Instance.Player2.transform;

	}


    private bool hits(Transform player, MenuOption option) {

        Vector2 v1 = player.position;
        Vector2 v2 = option.transform.position;

        var l = (v1 - v2).SqrMagnitude();

        var r = option.Size;

        return l < r*r;

    }


    private bool transitioning;
	
	// Update is called once per frame
	void Update () {

	    if (transitioning) {

	        var pos = ActiveMenu.localPosition;
	        pos.z -= Time.deltaTime*TransitionSpeed;
	        ActiveMenu.localPosition = pos;


	        if (Mathf.Abs(pos.z) > TransitionDistance) {
	            pos.z = TransitionDistance;
                ActiveMenu.localPosition = pos;
	            ActiveMenu.gameObject.SetActive(false);
	            transitioning = false;
	            ActiveMenu = NextMenu;
	        }

            

	    } else {

	        if (ActiveMenu == null)
	            return;

            var options = ActiveMenu.GetComponentsInChildren<MenuOption>();

            foreach(var option in options) {

                if(hits(player1, option) || hits(player2, option)) {

                    transitioning = true;

                    option.ExecuteActions();

                }

            }


	    }


	    

	}
}
