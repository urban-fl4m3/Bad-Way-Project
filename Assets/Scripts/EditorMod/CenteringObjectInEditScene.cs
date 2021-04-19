using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class CenteringObjectInEditScene : MonoBehaviour
{
    private Vector3 _positon => transform.position;
    public int magnitudeValue = 1;

    private void OnEnable()
    {
        if (Application.isEditor)
        {
            Destroy(this);
        }
    }

    public void Update()
   {
     var position = new Vector3((int) (_positon.x/magnitudeValue), (int) (_positon.y/magnitudeValue), (int) (_positon.z/magnitudeValue));
     transform.position = position*magnitudeValue;
   }
}
