using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] TextMeshProUGUI PearlReadout = null;
    [SerializeField] GameObject Achievement = null;

    int pearlsCollected = 0;
    public int totalPearls;
    private void Start()
    {
        totalPearls = FindObjectsOfType<ItemObject>().Length;
        PearlReadout.text = "Pearls Collected: " + pearlsCollected.ToString() + "/" + totalPearls.ToString();
    }
    public void AddPearl()
    {
        pearlsCollected++;
        PearlReadout.text = "Pearls Collected: " + pearlsCollected.ToString() + "/" + totalPearls.ToString();
    }
    private void Update()
    {
        if (pearlsCollected == totalPearls && !Achievement.activeSelf)
        {
            Achievement.SetActive(true);
        }
    }
}
