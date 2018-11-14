using System.Collections;
using UnityEngine;

public class SeedController : MonoBehaviour {

    private MeshRenderer meshRenderer;
    private int ticks = 15;
    private readonly float blinkDuration = 0.3f;

    private float aux = 0.06f;
    private float intervalBetweenBlinks = 1f;

    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(updateSeed());
    }

    public void collectSeed() {
        destroySeed();
        // TODO play animation, increase the qtd of seeds of the player
    }

    private IEnumerator updateSeed() { // make seed blink until is collected or vanish

        while (ticks >= 0) {
            StartCoroutine(blink(blinkDuration));
            ticks--;
            aux = aux * 1.1f;
            intervalBetweenBlinks = intervalBetweenBlinks - aux;
            yield return new WaitForSeconds(intervalBetweenBlinks + blinkDuration);
        }

        destroySeed();
    }

    private void destroySeed() {
        Destroy(gameObject);
    }

    private IEnumerator blink(float duration) {
        meshRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        meshRenderer.enabled = true;
    }

}