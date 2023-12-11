using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PointCheckBar : MonoBehaviour
{
    public int newCoinValue;

    private void OnDrawGizmos()
    {
        //Handles.color = Color.blue;
        //Handles.ScaleHandle(new Vector3(2,2,2), Vector3.zero);
        //Handles.Label(transform.position, newCoinValue.ToString());
    }
}
