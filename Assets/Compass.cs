using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Compass : MonoBehaviour
{
    [SerializeField] private Transform source;
    private Transform target;
    [SerializeField] private RectTransform needle;
    [SerializeField] private CanvasGroup compassCanvasGroup;

    [SerializeField] private List<Transform> _potentialTargets;

    private void Start()
    {
        
    }

    public void UpdateTargets(List<Transform> potentialTargets)
    {
        _potentialTargets = potentialTargets;
    }

    // Update is called once per frame
    void Update()
    {
        //var angle = Vector3.SignedAngle(new Vector3(0, 0, 0), target.position- source.position, new Vector3(0,0,1));
        ////needle.Rotate(new Vector3(0, 0, 1), angle);
        //needle.eulerAngles = new Vector3(0,0,angle);

        // get nearest target
        target = _potentialTargets.OrderBy(t => Vector3.Distance(t.position, source.position)).FirstOrDefault();

        compassCanvasGroup.alpha = target == null ? 0 : 1;
        if (target == null) return;

        Vector3 relative = source.InverseTransformPoint(target.position);
        float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        needle.transform.rotation = Quaternion.Euler(0, 0, -angle);
    }
}
