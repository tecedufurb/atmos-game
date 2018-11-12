using UnityEngine;

public class SeedController : MonoBehaviour {

	public void collectSeed() {
		print("seed destroyed");
		Destroy(gameObject);
		// play animation, destroy this seed and increase the qtd of seeds of the player
	}
}
