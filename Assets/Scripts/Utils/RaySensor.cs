using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySensor : MonoBehaviour
{
    public string label;

    public Vector2 direction;
    public float rayDistance = 2f;
    public float adjustmentDistance = 0.01f;
    public float contactDistance = 0.05f;
    public float closeDistance = 0.5f;
    public float nearDistance = 2f;
    public float awareDistance = 5f;

    public LayerMask layerMask = 1;

    public bool isAdjusted { get; private set; }
    public bool isContacted {get; private set;}
    public bool isClose {get; private set;}
    public bool isNear {get; private set;}
    public bool isAware {get; private set;}

    public LayersEnum firstHitLayer {get; private set;}

    private RaycastHit2D hits;

    private float disableTimer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (disableTimer > 0)
            disableTimer -= Time.deltaTime;
    }

    public void Draw()
    {
        if (disableTimer > 0)
        {
            isAdjusted = isContacted = isClose = isNear = false;
            return;
        }
        SetEnvironmentCollideParameters();

        DrawGizmos();
    }

    private void SetEnvironmentCollideParameters()
    {
        Collider2D insideHit = CheckInsideCollider(layerMask);
        if (insideHit)
        {
            //Debug.Log(string.Format("Raycast {0} is inside {1}", label, insideHit.transform.name));
            isAdjusted = isContacted = isClose = isNear = isAware = true;
        }
        else
        {

            hits = Physics2D.Raycast(transform.position, direction, rayDistance, layerMask);

            if (hits)
            {
                Debug.Log(string.Format("Raycast {0} hits {1} by distance {2}", label, hits.collider.transform.name, hits.distance));
                firstHitLayer = LayersUtils.LayerNumberToLayerEnum(hits.collider.gameObject.layer);
            }

            isAdjusted = hits && hits.distance < adjustmentDistance;
            isContacted = hits && hits.distance < contactDistance;
            isClose = hits && hits.distance < closeDistance;
            isNear = hits && hits.distance < nearDistance;
            isAware = hits && hits.distance < awareDistance;
        }
    }

    private Collider2D CheckInsideCollider(int layerMask)
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.001f, layerMask);
        return hit;
    }

    public void AddLayer(LayersEnum layerEnum, float timer = 0)
    {
        string layerName = LayersUtils.LayerEnumToLayerName(layerEnum);
        int layerNumber = LayerMask.NameToLayer(layerName);
        int layerValue = LayerMask.GetMask(layerName);
        // Check if layer is not already in layermask. If already exist don't do anything.
        if ((layerMask & 1 << layerNumber) != 0)
            return;

        if (timer > 0)
        {
            StartCoroutine(AddLayerAfterTime(timer, layerEnum));
        }
        layerMask += layerValue;
    }

    public void RemoveLayer(LayersEnum layerEnum, float timer = 0)
    {
        string layerName = LayersUtils.LayerEnumToLayerName(layerEnum);
        int layerNumber = LayerMask.NameToLayer(layerName);
        int layerValue = LayerMask.GetMask(layerName);

        // Check if layer is already in layermask. If not don't do anything.
        if ((layerMask & 1 << layerNumber) == 0)
            return;

        if (timer > 0)
        {
            StartCoroutine(RemoveLayerAfterTime(timer, layerEnum));
        }
        layerMask -= layerValue;
    }

    IEnumerator AddLayerAfterTime(float timer, LayersEnum layerEnum)
    {
        yield return new WaitForSeconds(timer);
        AddLayer(layerEnum);
    }

    IEnumerator RemoveLayerAfterTime(float timer, LayersEnum layerEnum)
    {
        yield return new WaitForSeconds(timer);
        RemoveLayer(layerEnum);
    }

    public void Disable(float timer)
    {
        disableTimer = timer;
    }

    private void OnDrawGizmosSelected()
    {
        // Warning: the gizmo radius is greater than the actual radius
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }

    private void DrawGizmos()
    {
        Vector2 startPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir;
        Color color;

        // Adjustment Distance
        color = isAdjusted ? Color.red : Color.green;
        dir = adjustmentDistance * direction;
        Debug.DrawRay(startPosition, dir, color);
        startPosition = startPosition + dir;

        // Contact Distance
        color = isContacted ? Color.red : Color.green;
        dir = contactDistance * direction;
        Debug.DrawRay(startPosition, dir, color);
        startPosition = startPosition + dir;

        // CloseContact Distance
        color = isClose ? Color.red : Color.green;
        dir = (closeDistance - contactDistance) * direction;
        Debug.DrawRay(startPosition, dir, color);
        startPosition = startPosition + dir;

        // Near Distance 
        color = isNear ? Color.red : Color.green;
        dir = (nearDistance - closeDistance) * direction;
        Debug.DrawRay(startPosition, dir, color);
        startPosition = startPosition + dir;

        // Aware Distance
        color = isAware ? Color.red : Color.green;
        dir = (awareDistance - nearDistance) * direction;
        Debug.DrawRay(startPosition, dir, color);
        startPosition = startPosition + dir;
    }

}
