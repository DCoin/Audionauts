using Assets.Scripts.Managers;
using UnityEngine;

public class CreditsRoller : MonoBehaviour {

    [ContextMenuItem("Load", "LoadText")]
    public TextAsset CreditsText;

    public GameObject CreditTextPrefab;

    public Camera Camera;


    public TextMesh[] Texts;

    private int active = 0;

    void LoadText() {

        if (Texts != null) {
            foreach (var t in Texts) {
                DestroyImmediate(t);
            }
        }

        var credits = CreditsText.text.ToUpper().Split('#');

        Texts = new TextMesh[credits.Length];

        for(int i = 0; i < credits.Length; ++i) {

            var obj = Instantiate(CreditTextPrefab);
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.forward * (i + 1) * 2;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;

            var txt = obj.GetComponentInChildren<TextMesh>();
            txt.text = credits[i].Trim();

            Texts[i] = txt;

        }


    }

    void Start() {

        for (int i = 0; i < Texts.Length; ++i) {

            Texts[i].gameObject.SetActive(true);

        }
        
    }

    void Update() {

        if (active >= Texts.Length)
            return;

        var current = Texts[active];

        var a = current.transform.position.z;
        var b = Camera.transform.position.z;

        if (a < b) {
            current.gameObject.SetActive(false);
            active++;
            if (active >= Texts.Length) {


                FadeManager.Instance.EndScene(ReturnToMenu);
                return;

            }

            Texts[active].gameObject.SetActive(true);
        }
    }

    private void ReturnToMenu() {
        
        Application.LoadLevel("Menu");

    }

}
