using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyBillboard : MonoBehaviour
{
    //Definitely not performant but it gets the job done!
    Transform _cam;
    Transform _billboardTransform;

    private void Awake()
    {
        _billboardTransform = this.transform;

        //Find main camera (definitely bad practice!)
        _cam = Camera.main.transform;

    }
    private void FixedUpdate()
    {
        if (_cam == null) return;
        _billboardTransform.rotation = Quaternion.LookRotation(_billboardTransform.position - _cam.position);
    }
}
