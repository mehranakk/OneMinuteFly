using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public List<Sound> sounds;

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

        foreach (Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volumeDefualt;
            s.source.pitch = s.pitchDefault;
            s.source.loop = s.loopDefault;
        }
    }

    private void Start()
    {
        Vector2 playAt = GameObject.Find("Main Camera").transform.position;
        PlayByName("theme", playAt);
    }

    private void Update()
    {

    }

    public void PlayByName(string name, Vector2 location)
    {
        try
        {
            Sound s = sounds.Find(sound => sound.name == name);
            s.source.Play();
            Debug.Log(string.Format("Playing audio clip '{0}'", name));
        }
        catch (NullReferenceException exp)
        {
            Debug.LogWarning(string.Format("Can not find audio clip '{0}' to play", name));
        }
        catch (ArgumentNullException exp)
        {
            Debug.LogWarning(string.Format("Can not find audio clip '{0}' to play", name));
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
        catch (NullReferenceException exp)
        {
            Debug.LogWarning(string.Format("Can not find audio clip '{0}' to stop", name));
        }
        catch (ArgumentNullException exp)
        {
            Debug.LogWarning(string.Format("Can not find audio clip '{0}' to stop", name));
        }
    }

}
