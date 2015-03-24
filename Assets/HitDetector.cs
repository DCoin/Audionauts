using UnityEngine;
using System.Collections.Generic;

public class HitDetector : MonoBehaviour {

	private float distance;

	private HitManager HM {

		get {
			return HitManager.Instance;
		}
	}
	
	// Use this for initialization
	void Start () {
	
		distance = GetDistance();
	}

	private float GetDistance() {

		Vector3 a = this.transform.position;

		if(HM == null) {
			Debug.LogWarning("FUCK");
		}

		Vector3 b = HM.players.transform.position;
		Vector3 d = a - b;

		return d.z;
	}
	
	// Update is called once per frame
	void Update () {
	
		float newDistance = GetDistance ();

		if (distance > 0f && newDistance < 0f) {
			RegisterHits();

		}

		distance = newDistance;

	}

	private IEnumerable<Transform> GetImmediateChildren() {

		foreach (Transform child in GetComponentInChildren<Transform>()) {

			if(child.parent == this.transform)
				yield return child;

		}

		yield break;

	}

	private Transform GetHit(Transform player) {

		Vector2 p = player.transform.position;
		
		Transform hit = null;
		
		foreach(Radius r in GetComponentsInChildren<Radius>()) {
			
			Vector2 c = r.transform.position;
			
			float dsqr = (p-c).sqrMagnitude;
			
			if(dsqr < r.value * r.value) {
				hit = r.transform;
				break;
			}
			
		}

		return hit;
	}

	private void RegisterHits() {

		Transform hit1 = GetHit (HM.player1);
		Transform hit2 = GetHit (HM.player2);

		if (hit1 == hit2) {
			RegisterBoth(hit1);
		} else {
			RegisterFirst(hit1);
			RegisterSecond(hit2);
		}

	}

	private void RegisterBoth(Transform t) {

		if (t == null)
			return;

		Slice s = t.GetComponent<Slice>();

		if (s == null)
			return;

		s.OnHit (Slice.HitType.Both);

		switch (s.sliceType) {
		case Slice.SliceType.Any: {
			// Both players hit an "Any" slice.
			// Set their colors to white.
			SetPlayerColor(HM.player1, Color.white);
			SetPlayerColor(HM.player2, Color.white);
		} break;
		case Slice.SliceType.None: {
			// Both players hit a "None" slice.
			// Set their colors to red and do a screen shake.
			SetPlayerColor(HM.player1, Color.red);
			SetPlayerColor(HM.player2, Color.red);
			DoScreenShake();
		} break;
		case Slice.SliceType.First: {
			// Both players hit a "First" slice.

			// Not entirely sure what should happen exactly.
			// For now, the first player gets a rainbow color.
			EnablePlayerRainbow(HM.player1);
			// While the second becomes white.
			SetPlayerColor(HM.player2, Color.white);
		} break;
		case Slice.SliceType.Second: {
			// Both players hit a "Second" slice.
			
			// Not entirely sure what should happen exactly.
			// For now, the second player gets a rainbow color.
			EnablePlayerRainbow(HM.player2);
			// While the first becomes white.
			SetPlayerColor(HM.player1, Color.white);

		} break;
		case Slice.SliceType.Both: {
			// Not entirely sure what should happen exactly.
			// Both players gets a rainbow color.
			EnablePlayerRainbow(HM.player1);
			EnablePlayerRainbow(HM.player2);
		} break;
		default: { Debug.Log ("Supposedly unreachable code has been reached."); } break; 
		}

	}

	private void SetPlayerColor(Transform player, Color c) {

		if (player == null)
			return;

		ColorController cc = player.GetComponent<ColorController> ();
		Animation an = player.GetComponent<Animation> ();


		if(cc == null || an == null) 
			return;

		an.enabled = false;

		cc.color = c;

	}

	private void EnablePlayerRainbow(Transform player) {

		if (player == null)
			return;
		
		ColorController cc = player.GetComponent<ColorController> ();
		Animation an = player.GetComponent<Animation> ();
		
		
		if(cc == null || an == null) 
			return;
		
		an.enabled = true;

	}

	private void RegisterFirst(Transform t) {

		if (t == null) 
			return;

		Slice s = t.GetComponent<Slice> ();

		if (s == null) 
			return;
			
		s.OnHit (Slice.HitType.First);

		switch (s.sliceType) {
		case Slice.SliceType.Any: {
			// First player hit an "Any" slice.
			// Set its color to white.
			SetPlayerColor(HM.player1, Color.white);
		} break;
		case Slice.SliceType.None: {
			// First player hit a "None" slice.
			// Set its color to red and do a screen shake.
			SetPlayerColor(HM.player1, Color.red);
			DoScreenShake();
		} break;
		case Slice.SliceType.First: {
			// First player hit a "First" slice.
			
			// First player gets a rainbow color.
			EnablePlayerRainbow(HM.player1);
		} break;
		case Slice.SliceType.Second: {
			// First player hit a "Second" slice.
			
			// Not entirely sure what should happen exactly.
			// For now, the first player becomes white.
			SetPlayerColor(HM.player1, Color.white);
			
		} break;
		case Slice.SliceType.Both: {
			// First player hit a "Both" slice, (without the second player).

			// Not entirely sure what should happen exactly.
			// For now, the first player becomes white.
			SetPlayerColor(HM.player1, Color.white);
		} break;
		default: { Debug.Log ("Supposedly unreachable code has been reached."); } break; 
		}


	}

	private void RegisterSecond(Transform t) {
		
		if (t == null) 
			return;
		
		Slice s = t.GetComponent<Slice> ();
		
		if (s == null) 
			return;
		
		s.OnHit (Slice.HitType.Second);

		
		switch (s.sliceType) {
		case Slice.SliceType.Any: {
			// Second player hit an "Any" slice.
			// Set its color to white.
			SetPlayerColor(HM.player2, Color.white);
		} break;
		case Slice.SliceType.None: {
			// Second player hit a "None" slice.
			// Set its color to red and do a screen shake.
			SetPlayerColor(HM.player2, Color.red);
			DoScreenShake();
		} break;
		case Slice.SliceType.First: {
			// Second player hit a "First" slice.

			// Not entirely sure what should happen exactly.
			// For now, the second player becomes white.
			SetPlayerColor(HM.player2, Color.white);

		} break;
		case Slice.SliceType.Second: {
			// Second player hit a "Second" slice.
			
			// Second player gets a rainbow color.
			EnablePlayerRainbow(HM.player2);			
		} break;
		case Slice.SliceType.Both: {
			// Second player hit a "Both" slice, (without the first player).
			
			// Not entirely sure what should happen exactly.
			// For now, the second player becomes white.
			SetPlayerColor(HM.player2, Color.white);
		} break;
		default: { Debug.Log ("Supposedly unreachable code has been reached."); } break; 
		}

	}

	private void DoScreenShake() {

		if (HM.shaker == null)
			return;

		ScreenShake ss = Instantiate(HM.shaker) as ScreenShake;
		ss.target = HM.shakeTarget;
		ss.Begin();
	}
}
