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
        }

        ready = true;
    }

    private void Update()
    {
        foreach (Sound s in sounds)
        {
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

    public void PlayByName(string name, Vector2 location, float delay = 0)
    {
        try
        {
            Sound s = sounds.Find(sound => sound.name == name);
            s.source.PlayDelayed(delay);
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

    public Sound CopyByName(string name)
    {
        try
        {
            Sound s = sounds.Find(sound => sound.name == name);
            Sound newSound = new Sound();
            newSound.clip = s.clip;
            newSound.volumeDefault = s.volumeDefault;
            newSound.pitchDefault = s.pitchDefault;
            newSound.loopDefault = s.loopDefault;

            return newSound;
        }
        catch (Exception exp)
        {
            Debug.LogWarning(string.Format("Can not find audio clip '{0}' to stop", name));
            return null;
        }
    }

    public void StopEverything()
    {
        foreach (Sound s in sounds)
            s.source.Stop();

        foreach (FrogController f in GameObject.FindObjectsOfType<FrogController>())
            f.StopIdleAudio();
    }

}
