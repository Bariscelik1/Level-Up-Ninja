using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    Vector3 final_offset;
    public GameObject camera_final;
    Vector3 offset;
    public bool endArea;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;
        final_offset = transform.position - camera_final.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!endArea)
        transform.position = Vector3.Lerp(transform.position, target.transform.position+offset,.125f  );
        else
            transform.position = Vector3.Lerp(transform.position, camera_final.transform.position, .05f);

    }

   
}
