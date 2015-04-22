using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MenuOption : MonoBehaviour {

    public float Size;

    public MenuAction[] Actions;

    public void ExecuteActions() {

        foreach (var action in Actions) {
            action.Execute();
        }

    }

#if UNITY_EDITOR
        void OnDrawGizmosSelected() {

            var stdColor = Handles.color;

            Handles.color = Color.Lerp(Color.grey, Color.clear, 0.5f);

            Handles.DrawSolidDisc(transform.position, transform.forward, Size);

            Handles.color = stdColor;

        }
#endif
}
