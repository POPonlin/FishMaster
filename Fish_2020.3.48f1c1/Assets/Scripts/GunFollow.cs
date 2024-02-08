using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFollow : MonoBehaviour
{
    public RectTransform uguiRect;
    public Camera mainCamera;

    void Start()
    {
        
    }

    void Update()
    {
        float z;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(uguiRect, new Vector2(Input.mousePosition.x, Input.mousePosition.y), mainCamera, out Vector3 mousePos);
        if (mousePos.x > transform.position.x)
        {
            z = -Vector3.Angle(Vector3.up, mousePos - transform.position);
        }
        else
        {
           z = Vector3.Angle(Vector3.up, mousePos - transform.position);
        }
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, z));
    }
}
