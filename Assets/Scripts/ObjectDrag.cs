using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    Vector3 velocity;
    public float smoothTime = 0.01f;
    public float speed = 5000;
    private void Update()
    {
        Vector3 pos = BuildingSystem.GetMouseWorldPosition();
        transform.position = Vector3.SmoothDamp(transform.position, BuildingSystem.instance.SnapCoordinateToGrid(pos), ref velocity, smoothTime, speed * Time.deltaTime);
    }
}
