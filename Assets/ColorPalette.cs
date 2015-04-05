using UnityEngine;
using Audionauts.Enums;

public class ColorPalette : MonoBehaviour {
	
	public Color first;
	public Color second;
	public Color both;
	public Color any;
	public Color none;
	
	public Color GetColor(NoteKind t) {
		
		switch (t) {
		case NoteKind.Any: 
			return any; 
		case NoteKind.First: 
			return first;
		case NoteKind.Second: 
			return second;
		case NoteKind.Both: 
			return both;
		case NoteKind.None: 
			return none;
		default: 
			throw new UnityException("Could not resolve a color for the given beat type.");
		}
	}
	
}
