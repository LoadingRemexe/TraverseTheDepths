using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(0.0f, 0.0f, -10.0f);
    [SerializeField] GameObject Target = null;

    void FixedUpdate()
    {
            transform.position = Target.transform.position + offset;
    }
}
