using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using System.Linq;
using Assets.Scripts;
using JetBrains.Annotations;
using UnityEngine.SocialPlatforms.GameCenter;

public class MenuHandler : MonoBehaviour {

    private Transform player1;
    private Transform player2;
    private Vector3 player1Center;
    private Vector3 player2Center;
    private Vector3 player1From;
    private Vector3 player2From;

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
	    player1Center = player1.localPosition;
	    player2Center = player2.localPosition;

	}


    private bool hits(Transform player, MenuOption option) {

        Vector2 v1 = player.position;
        Vector2 v2 = option.transform.position;

        var l = (v1 - v2).SqrMagnitude();

        var r = option.Size;

        return l < r*r;

    }


    private bool transitioning;

    private bool centering;

    public float centeringDuration;

    private float centeringProgress;

    private bool Center(Transform player, Vector3 from, Vector3 to) {

        centeringProgress += Time.deltaTime;

        if (centeringProgress < centeringDuration) {
            player.localPosition = Vector3.Lerp(from, to, centeringProgress/centeringDuration);
            return false;
        } else {
            player.localPosition = to;
            return true;
        }

    }



	// Update is called once per frame
	void Update () {

	    if (centering) {
	        var b1 = Center(player1, player1From, player1Center);
            var b2 = Center(player2, player2From, player2Center);

	        if (b1 && b2) {
	            centering = false;
                player1.GetComponent<MovementController>().enabled = true;
                player2.GetComponent<MovementController>().enabled = true;
	        }
	    }

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
	            NextMenu = null;
	        }

            

	    } else {

	        if (ActiveMenu == null)
	            return;

	        if (NextMenu != null) {

                transitioning = true;

	        }

            var options = ActiveMenu.GetComponentsInChildren<MenuOption>();

            foreach(var option in options) {

                if(hits(player1, option) || hits(player2, option)) {

                    centering = true;

                    player1.GetComponent<MovementController>().enabled = false;
                    player2.GetComponent<MovementController>().enabled = false;

                    player1From = player1.localPosition;
                    player2From = player2.localPosition;
                    centeringProgress = 0f;

                    option.ExecuteActions();

                }

            }


	    }

        


	    

	}
}
