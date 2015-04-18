using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleRenderer : MonoBehaviour
{

    public float Radius = 5.0f;
    public float Width;
    public Color Color;

    [Range(4,360)]
    public int Detail = 4;

    void OnValidate()
    {
        Refresh();
    }

	// Update is called once per frame
	public void Refresh ()
	{

        const float tau = Mathf.PI * 2.0f;

        var lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(Detail+1);
        lineRenderer.SetWidth(Width, Width);
        lineRenderer.SetColors(Color, Color);

        for (var i = 0; i <= Detail; ++i)
        {

            var fs = (float) Detail;
            var fi = (float) i;

            var theta = fi * tau / fs;

            var x = Radius * Mathf.Cos(theta);
            var y = Radius * Mathf.Sin(theta);

            var pos = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, pos);
        }

        

	}

}
