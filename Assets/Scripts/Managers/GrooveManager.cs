using UnityEngine;
using System.Collections;
using InControl;

namespace Assets.Scripts.Managers
{
	public class GrooveManager : MonoBehaviour {
		
		public static GrooveManager Instance { get; private set; }

		public float Decay = 0.999f;
		public float MissScale = .5f;
		public float Smoothing = .1f;
		public float MaxGroove = 40;

		private float groove = 0;
		private float smoothGroove = 0;

		public float PercentGroove {
			get { return smoothGroove / MaxGroove; }
		}

		void Start () {
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Debug.LogError("A scene should only have one GrooveManager");
			}
		}

		void Update () {
			groove *= Decay;
			smoothGroove += (groove - smoothGroove) * Smoothing;
		}

		public void Hit(bool hit) {
			groove = Mathf.Max(0, groove + (hit ? 1 : -MissScale));
		}

#if UNITY_EDITOR
		void OnGUI() {
			
			GUI.Label(new Rect(32, 50, 128, 128), groove.ToString());
			GUI.Label(new Rect(32, 62, 128, 128), smoothGroove.ToString());
			
		}
#endif

	}
}
