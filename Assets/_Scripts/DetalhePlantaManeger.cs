using UnityEngine;

public class DetalhePlantaManeger : MonoBehaviour {
    void OnDisable() { //seta detalhe panel para inicio do scroll
            var t = gameObject.transform.GetChild(0).gameObject;
            t.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, 0.0f);
            t.GetComponent<RectTransform>().offsetMin = new Vector2(-0.5f, -330f);
    }
}
