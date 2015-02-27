using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
	
	public Material GreenMat;
	public Material RedMat;

	public bool removable = true;

	private bool wasHit = false;

	void Start () {
	
	}
	
	void FixedUpdate () {
		
	}

	public void Hit () {
		wasHit = true;
		renderer.material = GreenMat;
	}

	public void Missed ()
	{
		renderer.material = RedMat;
	}

	public void Reset () {
		wasHit = false;
	}

	public bool WasHit () {
		return wasHit;
	}

	public void remove() {
		if (removable) Destroy(gameObject);
	}
}
