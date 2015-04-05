using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Audionauts.Enums;

public class Bar : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Beat[] Beats {
		
		get {
			IEnumerable<Beat> ibeats = 
				from note in transform.GetComponentsInChildren<Beat>()
					where note.transform.parent == this.transform
					select note;
			
			return ibeats.ToArray();
		}
		
	}

	public void AddBeat() {

		Section section = GetComponentInParent<Section>();

		Beat beat = (Beat) Instantiate(section.beatPrefab, Vector3.zero, Quaternion.identity);
		
		beat.transform.parent = this.transform;

		beat.transform.SetAsLastSibling();

		RefreshChildren();
		
	}

	public void RemoveBeat() {

		if(transform.childCount <= 1) {

			return;

		}

		Transform t = this.transform.GetChild(this.transform.childCount-1);
		
		DestroyImmediate(t.gameObject);
		
		RefreshChildren();
		
	}

	private void RefreshChildren() {

		float b = Beats.Length;

		foreach(Beat beat in Beats) {
			
			int idx = beat.transform.GetSiblingIndex();
			
			beat.name = "Beat" + idx;

			float a = (float) idx;

			beat.transform.localPosition = new Vector3(0f, 0f, a/b);
			
		}
		
	}
}
