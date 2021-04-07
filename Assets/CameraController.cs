using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float SmoothX;
    [SerializeField] private float SmoothY;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform SelectedAcrot;
    
    private void FixedUpdate()
    {
        transform.position = SelectedAcrot.position + offset;
    }

    public void SelectNextActor(Transform actor)
    {
        SelectedAcrot = actor;
    }
}
