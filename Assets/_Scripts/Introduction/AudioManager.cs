using UnityEngine;

/// <summary>
/// Responsible for the game audio effects.
/// </summary>
/// Originally attached to the _SoundEffectsManager object.
public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip PlantAudio;

    /// <summary>
    /// Plays a sound when the plant was planted.
    /// </summary>
    public void PlayPlantAudio() {
        AudioSource.clip = PlantAudio;
        AudioSource.Play();
    }
}
