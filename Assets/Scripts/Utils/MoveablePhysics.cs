using System.Collections;
using UnityEngine;

public static class MoveablePhysics
{
    public static float FallY(Vector2 velocity, float gravity, float gravityDownwardModifier, float maxFallSpeed)
    {
        float y = velocity.y; 
        float acc = gravity * Time.deltaTime;
        if (velocity.y < 0)
        {
            acc *= gravityDownwardModifier;
        }
        y += acc;
        y = Mathf.Max(y, maxFallSpeed);
        return y;
    }

    public static float FallY(Vector2 velocity, float gravity, float maxFallSpeed)
    {
        float y = velocity.y; 
        float acc = gravity * Time.deltaTime;
        y += acc;
        y = Mathf.Max(y, maxFallSpeed);
        return y;
    }

    public static bool GroundCheck(RayCaster raycaster, Vector2 velocity)
    {
        if (velocity.y > Mathf.Epsilon)
            return false;
        foreach (RaySensor sensor in raycaster.GetByTag("DOWN"))
        {
            if (sensor.isContacted)
            {
                return true;
            }
        }
        return false;
    }
    public static Vector2 AdjustObject(RayCaster raycaster)
    {
        Vector2 adjustment = new Vector2(0, 0);
        float adjustmentDistance = 0.02f;
        if (CheckAdjustBySensors(raycaster, "DOWN"))
            adjustment.y += adjustmentDistance;
        if (CheckAdjustBySensors(raycaster, "UP"))
            adjustment.y -= adjustmentDistance;
        if (CheckAdjustBySensors(raycaster, "RIGHT"))
            adjustment.x -= adjustmentDistance;
        if (CheckAdjustBySensors(raycaster, "LEFT"))
            adjustment.x += adjustmentDistance;

        return adjustment;
    }

    private static bool CheckAdjustBySensors(RayCaster raycaster, string sensorsTag)
    {
        foreach (RaySensor rs in raycaster.GetByTag(sensorsTag))
        {
            if (rs.isAdjusted)
                return true;
        }
        return false;
    }
}