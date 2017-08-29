using UnityEngine;

/// <summary>
/// Increase and decrease an object scale.
/// </summary>
public class ResizeObject : MonoBehaviour {

    public bool m_Active = false;

    [SerializeField] private float MaxSize;
    [SerializeField] private float Speed;

    private bool mMaxSize;
    private float x;

    void Start() {
        x = transform.localScale.x;
    }

	void Update () {
        if (m_Active) {
            if (!mMaxSize && (transform.localScale.x <= MaxSize))
                transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * Speed, transform.localScale.y + Time.deltaTime * Speed, transform.localScale.z);
            else
                mMaxSize = true;

            if (mMaxSize && (transform.localScale.x > x))
                transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * Speed, transform.localScale.y - Time.deltaTime * Speed, transform.localScale.z);
            else
                mMaxSize = false;
        }
    }
}
