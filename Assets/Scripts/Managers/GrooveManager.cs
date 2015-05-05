using UnityEngine;
using System.Collections;
using InControl;

namespace Assets.Scripts.Managers
{
	public class GrooveManager : MonoBehaviour {
		
		public static GrooveManager Instance { get; private set; }

		public float Decay = 0.001f;
		public float MissScale = .5f;
		public float Smoothing = .1f;
		public float MaxGroove = 40;
		public AnimationCurve Progression;

		private float groove = 0;
		private float smoothGroove = 0;

		public float PercentGroove {
			get { return Progression.Evaluate(smoothGroove / MaxGroove); }
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
			groove -= groove * Decay * Time.deltaTime * 60;
			smoothGroove += (groove - smoothGroove) * Smoothing * 60 * Time.deltaTime;
		}

		public void Hit(bool hit) {
			groove = Mathf.Max(0, groove + (hit ? 1 : -MissScale));
		}

#if UNITY_EDITOR
		void OnGUI() {
			
			GUI.Label(new Rect(32, 50, 200, 128), "Groove: " + smoothGroove);
			GUI.Label(new Rect(32, 62, 200, 128), "Groove%: " + PercentGroove);
			
		}
#endif

	}
}
