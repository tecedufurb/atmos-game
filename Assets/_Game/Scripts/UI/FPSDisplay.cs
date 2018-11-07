using UnityEngine;

public class FPSDisplay : MonoBehaviour {

    float deltaTime = 0.0f;

    void Update() {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI() {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(w/4, h-50, 200, 10);
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = h * 2 / 40;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float fps = 1.0f / deltaTime;
        string text = string.Format("({0:0.0} Fps)", fps);
        GUI.Label(rect, text, style);
    }

}