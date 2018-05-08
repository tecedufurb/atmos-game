using UnityEngine;

/// <summary>
/// Responsible for the game audio effects.
/// </summary>
/// Originally attached to the _SoundEffectsManager object.
public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip PlantAudio;
    [SerializeField] private AudioClip ButtonClicked;

    /// <summary>
    /// Plays a sound when a plant is planted.
    /// </summary>
    public void PlayPlantAudio() {
        AudioSource.clip = PlantAudio;
        AudioSource.Play();
    }

    /// <summary>
    /// Plays a sound when a button is clicked.
    /// </summary>
    public void PlayButtonClickedAudio() {
        AudioSource.clip = ButtonClicked;
        AudioSource.Play();
    }
}
