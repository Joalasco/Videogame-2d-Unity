using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform Mc;

    void Update()
    {
        if (Mc != null)
        {
            Vector3 position = transform.position;
            position.x = Mc.position.x;
            transform.position = position;
        }
    }
}
