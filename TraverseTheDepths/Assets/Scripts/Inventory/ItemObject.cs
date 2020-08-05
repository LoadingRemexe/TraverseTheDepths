using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] GameObject PearlObject = null;
    [SerializeField] Animator anim = null;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && PearlObject)
        {
            InventoryManager.Instance.AddPearl();
            anim.SetTrigger("Close");
            Destroy(PearlObject);
        }
    }
}
