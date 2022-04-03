using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{

    public List<RaySensor> sensors;

    public void CastAll()
    {
        foreach (RaySensor sensor in sensors)
            sensor.Draw();
    }

    public List<RaySensor> GetByTag(string tag)
    {
        List<RaySensor> sensorsByTag = new List<RaySensor>();

        foreach (RaySensor sensor in sensors)
            if (sensor.label == tag)
                sensorsByTag.Add(sensor);

        return sensorsByTag;
    }

    public void SetDirection(string label, Vector2 direction)
    {
        foreach(RaySensor sensor in GetByTag(label))
            sensor.direction = direction;
    }
}

