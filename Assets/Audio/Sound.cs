using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0.0f, 1.0f)] public float volumeDefualt = 1.0f;
    [Range(0.5f, 3.0f)] public float pitchDefault = 1.0f;
    public bool loopDefault = false;

    [HideInInspector]
    public AudioSource source;
}