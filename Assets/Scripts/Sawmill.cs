using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static EnumsAndStructs;

public class Sawmill : ResouceMaker
{
    public float woodPerSec = 0;
    private PlecableObject obj;
    [SerializeField] TextMeshProUGUI efficency;

    bool init = true;
    void Start()
    {
        obj = GetComponent<PlecableObject>();
    }

    private void Update()
    {
        if (obj.Placed && init)
        {
            efficency.text = (woodPerSec * obj.nearResources.FirstOrDefault(x => x.name == ResourceType.Wood).amount).ToString();

            make.Add(ResourceType.Wood, woodPerSec * obj.nearResources.FirstOrDefault(x => x.name == ResourceType.Wood).amount);
            init = false;
        }
    }
}
