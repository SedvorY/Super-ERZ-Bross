using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(-0.3f, 3f, -10);

    public float dampTime = 0.3f;

    public Vector3 velocity = Vector3.zero;


    private void Awake()
    {
        Application.targetFrameRate = 60;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 point = GetComponent<Camera>().ScreenToWorldPoint(target.position);

        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, offset.z));

        Vector3 destination = point + delta;

        destination = new Vector3(destination.x, offset.y, offset.z);

        this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);

    }

    public void ResetCameraPosition()
    {
        Vector3 point = GetComponent<Camera>().ScreenToWorldPoint(target.position);

        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, offset.z));

        Vector3 destination = point + delta;

        destination = new Vector3(destination.x, offset.y, offset.z);

        this.transform.position = destination;
    }
}
