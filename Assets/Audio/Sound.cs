using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Header("General")]
    [Range(0.0f, 1.0f)] public float volumeDefault = 1.0f;
    [Range(0.5f, 3.0f)] public float pitchDefault = 1.0f;
    public bool loopDefault = false;

    [HideInInspector] public AudioSource source;

    [Header("Audible Area Limit")]
    public bool hasAudibleArea = false;
    public float minDistance = 1f, maxDistance = 10f;
    public Transform reference;
}