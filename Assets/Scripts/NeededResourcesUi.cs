using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NeededResourcesUi : MonoBehaviour
{
    [SerializeField] PlecableObject obj;
    private TextMeshProUGUI t;

    void Start()
    {
        t = GetComponent<TextMeshProUGUI>();
        foreach(var i in obj.neededResources)
        {
            if(i.name == EnumsAndStructs.ResourceType.Wood)
            {
                t.text += "Wood: " + i.amount + " ";
            }
            if (i.name == EnumsAndStructs.ResourceType.Stone)
            {
                t.text += "Stone: " + i.amount + " ";
            }
        }
    }
}
