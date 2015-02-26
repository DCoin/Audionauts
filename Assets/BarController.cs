using UnityEngine;
using System.Collections;

[RequireComponent (typeof(RectTransform))]
public class BarController : MonoBehaviour {

    private RectTransform rectTransform;

    public int maxValue;
    
    private int _currentValue;

    public int CurrentValue
    {
        get { return _currentValue;  }
        set { 
                _currentValue = 
                    value > maxValue ? maxValue 
                        : 
                    value < 0 ? 0 
                        :
                    value;  
        
        }
    }

	// Use this for initialization
	void Start () {

        CurrentValue = maxValue / 2;

        rectTransform = GetComponent<RectTransform>();
	
	}
	
	// Update is called once per frame
	void Update () {

        float m = (float)maxValue;
        float c = (float)CurrentValue;

        float pct = c / m;

        Vector3 v = rectTransform.localScale;
        v.y = pct;
        rectTransform.localScale = v;

	}
}
