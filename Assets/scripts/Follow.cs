using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > 8f)
        {//move if distance from target is greater than 1
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 25 * Time.deltaTime);
        }
    }
}
