using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class RotateWithCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _camera;

    void Update()
    {
        transform.rotation = Quaternion.AngleAxis( _camera.rotation.eulerAngles.y,Vector3.up);

    }
}
