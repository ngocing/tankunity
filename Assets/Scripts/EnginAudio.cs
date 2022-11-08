using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public float minVolume = .05f, maxVolume = .1f;
    public float volumeIncease = .01f;
    [SerializeField]
    private float currentVolume;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        currentVolume = minVolume;
    }
    private void Start() {
        audioSource.volume = currentVolume;
    }
    public void ControlEngineVolume(float speed) {
        if(speed > 0){
            if (currentVolume < maxVolume)
                currentVolume =+ volumeIncease * Time.deltaTime;
        }else{
            if (currentVolume > minVolume)
                currentVolume -= volumeIncease * Time.deltaTime;
        }
        currentVolume = Mathf.Clamp(currentVolume, minVolume, maxVolume);
        audioSource.volume = currentVolume;
    }
}
