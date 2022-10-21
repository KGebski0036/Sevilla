using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using static EnumsAndStructs;

public class Mine : ResouceMaker
{
    [SerializeField] float stonePerSec = 0;
    [SerializeField] TextMeshProUGUI efficency;

    private PlecableObject obj;
    bool init = true;
    void Start()
    {
        obj = GetComponent<PlecableObject>();
    }

    private void Update()
    {
        if (obj.Placed && init)
        {
            efficency.text = (stonePerSec * obj.nearResources.FirstOrDefault(x => x.name == ResourceType.Stone).amount).ToString();
            make.Add(ResourceType.Stone, stonePerSec * obj.nearResources.FirstOrDefault(x => x.name == ResourceType.Stone).amount);
            init = false;
        }
    }

}
