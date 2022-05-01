using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookInCamera : MonoBehaviour
{
    private Transform _cam;

    private void Start()
    {
        _cam = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        transform.LookAt(_cam);
    }
}
