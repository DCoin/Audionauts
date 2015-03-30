using UnityEngine;

public class Sequence : MonoBehaviour {

	public Slice[] slices;

	private Color[] colors;

	private float[] times;

	private int next;

	public int barCount;

	public float barLength;

	public int repeats;

	private bool replay;

	public Color color;

	void Start () {

		int l = slices.Length;

		times = new float[l];

		colors = new Color[l];

		for(int i = 0; i < l; ++i) {

			Slice s = slices[i];

			s.Hit += new Slice.HitEventHandler(OnHit);
			
			Renderer r = s.GetComponent<Renderer>();

			colors[i] = r.material.color;

		}
		next = 0; 
	
	}
	
	void Update () {

		for(int i = 0; i < slices.Length; ++i) {

			Slice s = slices[i];

			Renderer r = s.GetComponent<Renderer>();

			r.material.color = Color.Lerp(colors[i], color, Mathf.Sin(Time.time * Mathf.PI * Mathf.PI));

		}

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
