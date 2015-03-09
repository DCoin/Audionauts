using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float maxHealth;

	public float currentHealth;

	public float CurrentHealthPct {

		get {
			return currentHealth / maxHealth;
		}
	}

	void OnValidate() {

		currentHealth = Mathf.Clamp (currentHealth, 0f, maxHealth);

	}
}
