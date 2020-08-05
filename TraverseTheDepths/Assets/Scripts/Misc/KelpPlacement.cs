using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpPlacement : MonoBehaviour
{
    [SerializeField] LayerMask CaveLayer;
    [SerializeField] float HeightOffset = .5f;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform kelp in transform)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(kelp.position, Vector3.down, out hit, 30.0f, CaveLayer))
            {
                kelp.position = hit.point + (Vector3.up *HeightOffset);
            } else
            {
                Destroy(kelp.gameObject, 2.0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
