using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Audionauts.Enums;

public class Beat : MonoBehaviour {

	public float radius;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Note[] Notes {
		
		get {

			IEnumerable<Note> inotes = 
					from note in transform.GetComponentsInChildren<Note>()
					where note.transform.parent == this.transform
					select note;

			return inotes.ToArray();

		}
		
	}

	public void SetNoteCount(int count) {

		int diff = Notes.Length - count;

		if(diff > 0) {

			RemoveNotes(diff);

		}

		if(diff < 0) {

			AddNotes(-diff);

		}

	}

	
	public void AddNotes(int count) {

		Section section = GetComponentInParent<Section>();

		for(int i = 0; i < count; ++i) {

			Note note = (Note) Instantiate(section.notePrefab, Vector3.zero, Quaternion.identity);
			
			note.transform.parent = this.transform;
			
			note.transform.SetAsLastSibling();

		}

		RefreshChildren();

	}
	
	public void RemoveNotes(int count) {

		for(int i = 0; i < count; ++i) {

			DestroyImmediate(this.transform.GetChild(this.transform.childCount-1).gameObject);

		}
		
		RefreshChildren();
		
	}

	private void RefreshChildren() {

		Section section = GetComponentInParent<Section>();

		float b = Notes.Length;
		
		foreach(Note note in Notes) {

			note.RefreshColor();

			int idx = note.transform.GetSiblingIndex();

			note.name = section.sources[idx].name;

			float a = (float) idx;
			
			note.transform.localPosition = Quaternion.Euler(0f, 0f, a * 360f / b) * (new Vector3(0f, radius, 0f));

		}

	}

}
