using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage : MonoBehaviour {

	public string barPrefix;
	public string beatPrefix;

	public IEnumerable<Transform> GetBars() {
		
		Transform[] children = this.GetComponentsInChildren<Transform>();
		
		foreach (Transform child in children) {
			
			if(child.parent != this.transform)
				continue;
			
			if(!child.gameObject.name.StartsWith(this.barPrefix))
				continue;
			
			yield return child;
			
		}
		
		yield break;
		
	}

}
