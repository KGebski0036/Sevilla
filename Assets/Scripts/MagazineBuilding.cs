using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumsAndStructs;

public class MagazineBuilding : MonoBehaviour
{
    private void Awake()
    {
        amount.Add(ResourceType.Wood, 1000);
        amount.Add(ResourceType.Stone, 1000);
    }
    public Dictionary<ResourceType, int> amount = new Dictionary<ResourceType, int>();
}
