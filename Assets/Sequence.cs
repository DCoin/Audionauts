using UnityEngine;

public class Sequence : MonoBehaviour {

	public Slice[] slices;

	private float[] times;

	private int next;

	public int barCount;

	public float barLength;

	public int repeats;

	private bool replay;

	void Start () {

		times = new float[slices.Length];


		foreach(Slice slice in slices) {

			slice.Hit += new Slice.HitEventHandler(OnHit);

		}

		next = 0; 
	
	}
	
	void Update () {

		if(!replay) return;

		for(int i = 0; i < times.Length; ++i) {
			if(Time.time >= times[i]) {

				slices[i].PlaySound();

				times[i] += ((float) barCount) * barLength;

				if(i+1 == times.Length) {

					Debug.Log ("repeat executed");

					repeats--;

					if(repeats == 0) {

						Debug.Log("finished repeating");

						replay = false;
					}
				}

			}
		}
	
	}

	private void OnHit(object sender) {

		if(sender == slices[next]) {
			times[next] = Time.time + ((float) barCount) * barLength;
			next++;
		}

		if(next == slices.Length) {
			replay = true;
			Debug.Log ("sequence complete");
		}

	}
}
