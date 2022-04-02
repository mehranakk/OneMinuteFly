using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float dampTime = 0.15f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    private Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (target)
        {
            Vector3 targetPosition = target.position + offset;
            Vector3 point = camera.WorldToViewportPoint(targetPosition);
            Vector3 delta = targetPosition - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}
