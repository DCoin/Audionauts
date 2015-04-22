using UnityEngine;
using Assets.Scripts.Managers;

public class MenuHandler : MonoBehaviour {

    private Transform player1;
    private Transform player2;

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

	
	// Update is called once per frame
	void Update () {

	    var options = GetComponentsInChildren<MenuOption>();

	    foreach (var option in options) {

	        if (hits(player1, option) || hits(player2, option)) {

                option.ExecuteActions();

	        }

	    }

	}
}
