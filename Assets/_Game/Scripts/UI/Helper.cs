using UnityEngine;

public class Helper : MonoBehaviour {

	public GameManager gameManager;
	
	public void closeHelper() {
		gameObject.SetActive(false);
		gameManager.resumeGame();
		
	}
	
	public void displayHelper() {
		gameObject.SetActive(true);
		gameManager.pauseGame();
	} 
		
}
