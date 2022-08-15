using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Compass : MonoBehaviour
{
    [SerializeField] private Transform source;
    [SerializeField] private Transform target;
    [SerializeField] private RectTransform needle;

    // Update is called once per frame
    void Update()
    {
        //var angle = Vector3.SignedAngle(new Vector3(0, 0, 0), target.position- source.position, new Vector3(0,0,1));
        ////needle.Rotate(new Vector3(0, 0, 1), angle);
        //needle.eulerAngles = new Vector3(0,0,angle);

        Vector3 relative = source.InverseTransformPoint(target.position);
        float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        needle.transform.rotation = Quaternion.Euler(0, 0, -angle);
    }
}
