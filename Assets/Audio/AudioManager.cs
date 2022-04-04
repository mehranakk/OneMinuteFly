using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public List<Sound> sounds;

    private List<Sound> playingSongs;

    private bool ready;

    public static AudioManager GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);

        foreach (Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volumeDefault;
            s.source.pitch = s.pitchDefault;
            s.source.loop = s.loopDefault;

            if (s.hasAudibleArea)
            {
                s.source.transform.position = s.reference.position;
            }
        }

        ready = true;
    }

    private void Start()
    {
    }

    private void Update()
    {
        foreach (Sound s in sounds)
        {

            if (s.hasAudibleArea)
            {
                try // TODO: when player dies and respawns `distance` crazy
                {
                    Vector3 distance = GameManager.GetInstance().GetPlayer().transform.position - s.source.transform.position;
                    float playerDistance = distance.magnitude;
                    float audiblePercentage = Mathf.InverseLerp(s.maxDistance, s.minDistance, playerDistance);
                    float streo = Mathf.InverseLerp(-10, 10, distance.x) * 1.4f - 0.7f;

                    s.source.volume = s.volumeDefault * audiblePercentage;
                    s.source.panStereo = streo;
                }
                finally { }
            }

            else
                s.source.volume = s.volumeDefault;
        }
    }

    private void OnValidate()
    {
        if (ready)
            foreach (Sound s in sounds)
            {
                s.source.volume = s.volumeDefault;
                s.source.pitch = s.pitchDefault;
            }
    }

    public void PlayByName(string name, Vector2 location)
    {
        try
        {
            Sound s = sounds.Find(sound => sound.name == name);
            s.source.Play();
            Debug.Log(string.Format("Playing audio clip '{0}'", name));
        }
        catch (Exception exp)
        {
            Debug.LogWarning(string.Format("Can not find audio clip '{0}' to play", name));
        }
    }

    public void ChangeVolumeByName(string name, float volume)
    {
        try
        {
            Sound s = sounds.Find(sound => sound.name == name);
            s.volumeDefault = volume;
        }
        catch (Exception exp)
        {
            Debug.LogWarning(string.Format("Can not find audio clip '{0}' to modify", name));
        }
    }

    public void SetPositionByName(string name, Vector3 pos)
    {
        try
        {
            Sound s = sounds.Find(sound => sound.name == name);
            s.source.transform.position = pos;
        }
        catch (Exception exp)
        {
            Debug.LogWarning(string.Format("Can not find audio clip '{0}' to set pos", name));
        }
    }

    public void StopByName(string name)
    {
        try
        {
            Sound s = sounds.Find(sound => sound.name == name);
            s.source.Stop();
            Debug.Log(string.Format("Stoping audio clip '{0}'", name));
        }
        catch (Exception exp)
        {
            Debug.LogWarning(string.Format("Can not find audio clip '{0}' to stop", name));
        }
    }

}
