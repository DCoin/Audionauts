using System;

namespace Assets.Scripts.Enums {
	
	public enum NoteKind { 
		None, 
		Any, 
		First, 
		Second, 
		Both 
	};
	
	public static class NoteKindExtensions {

		public static NoteKind Next(this NoteKind n) {
			
			NoteKind[] ns = (NoteKind[]) Enum.GetValues (typeof(NoteKind));
			int l = ns.Length;
			
			for (int i = 0; i < l; ++i) {
				
				if(n == ns[i]) {
					
					return ns[(i+1) % l];
					
				}
				
			}
			
			return NoteKind.None;
			
		}
		
	}
	
	
}