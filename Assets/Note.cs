using UnityEngine;
using Audionauts.Enums;

[RequireComponent(typeof(SpriteRenderer))]
public class Note : MonoBehaviour {

	[SerializeField]
	private NoteKind _kind;

	public NoteKind Kind {

		get {

			return _kind;

		}

		set {

			_kind = value;

			RefreshColor();

		}

	}

	public void RefreshColor() {

		SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
		Section section = this.GetComponentInParent<Section>();
		renderer.color = section.palettePrefab.GetColor(Kind);
		
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
