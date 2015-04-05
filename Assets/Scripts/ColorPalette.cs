using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets
{
    public class ColorPalette : MonoBehaviour {
	
        public Color First;
        public Color Second;
        public Color Both;
        public Color Any;
        public Color None;
	
        public Color GetColor(NoteKind t) {
		
            switch (t) {
                case NoteKind.Any: 
                    return Any; 
                case NoteKind.First: 
                    return First;
                case NoteKind.Second: 
                    return Second;
                case NoteKind.Both: 
                    return Both;
                case NoteKind.None: 
                    return None;
                default: 
                    throw new UnityException("Could not resolve a color for the given beat type.");
            }
        }
	
    }
}
