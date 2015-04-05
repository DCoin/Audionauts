using UnityEngine;
using System.Collections.Generic;
using Audionauts.Enums;
using System.Linq;

public class Section : MonoBehaviour {

	public ColorPalette palettePrefab;
	public AudioSource[] sources;


	public Bar barPrefab;
	public Beat beatPrefab;
	public Note notePrefab;

	public string[] SourceNames {

		get {

			IEnumerable<string> inames = from source in sources select source.name;

			return inames.ToArray();

		}
	}

	public Bar[] Bars {
		
		get {
			IEnumerable<Bar> ibars = 
				from note in transform.GetComponentsInChildren<Bar>()
					where note.transform.parent == this.transform
					select note;
			
			return ibars.ToArray();
		}
		
	}

	public void AddBar(int index) {

		Bar bar = (Bar) Instantiate(barPrefab, Vector3.zero, Quaternion.identity);

		bar.transform.parent = this.transform;

		bar.transform.SetSiblingIndex(index);

		bar.AddBeat();

		RefreshChildren();

	}

	public void RemoveBar(int index) {

		Transform t = this.transform.GetChild(index);

		DestroyImmediate(t.gameObject);

		RefreshChildren();

	}

	public void MoveBar(int oldIndex, int newIndex) {

		Transform t = this.transform.GetChild(oldIndex);

		t.SetSiblingIndex(newIndex);

		RefreshChildren();

	}

	private void RefreshChildren() {

		foreach(Bar b in Bars) {

			int idx = b.transform.GetSiblingIndex();

			b.name = "Bar" + idx;

			b.transform.localPosition = new Vector3(0f, 0f, (float) idx);

			Vector3 s = b.transform.localScale;
			s.z = 1f;
			b.transform.localScale = s;

		}

	}

}
