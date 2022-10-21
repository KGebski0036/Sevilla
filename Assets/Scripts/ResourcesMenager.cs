using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static EnumsAndStructs;

public class ResourcesMenager : MonoBehaviour
{
    private Dictionary<ResourceType, float> resources = new Dictionary<ResourceType, float>();
    private Dictionary<ResourceType, float> resourceGrowth = new Dictionary<ResourceType, float>();
    private Dictionary<ResourceType, int> maxresources = new Dictionary<ResourceType, int>();

    private MagazineBuilding[] lastMagazines;
    private ResouceMaker[] lastResouceMaker;

    public TextMeshProUGUI maxWood;
    public TextMeshProUGUI maxStone;

    public TextMeshProUGUI currentWood;
    public TextMeshProUGUI currentStone;

    static public ResourcesMenager instance;

    private void Awake()
    {
        instance = this;
        maxresources.Add(ResourceType.Wood, 150);
        maxresources.Add(ResourceType.Stone, 150);
        resources.Add(ResourceType.Wood, 150);
        resources.Add(ResourceType.Stone, 150);
    }

    void Update()
    {
        var magazines = FindObjectsOfType<MagazineBuilding>();
        var resourceMakers = FindObjectsOfType<ResouceMaker>();

        if (lastMagazines != magazines)
        {
            lastMagazines = magazines;
            UpdateMaxResources();
        }

        if (lastResouceMaker != resourceMakers)
        {
            lastResouceMaker = resourceMakers;
            UpdateResourcesMakers();
        }

        UpdateResources();
    }

    private void UpdateResources()
    {
        foreach (var i in resourceGrowth)
        {
            if (resources.ContainsKey(i.Key) && maxresources.ContainsKey(i.Key))
            {
                resources[i.Key] += i.Value * Time.deltaTime;
                resources[i.Key] = Math.Min(resources[i.Key], maxresources[i.Key]);
            }
            else
            {
                if (maxresources.ContainsKey(i.Key) && maxresources[i.Key] >= 1)
                    resources.Add(i.Key, 1);
            }
        }

        currentWood.text = resources.ContainsKey(ResourceType.Wood) ? Mathf.FloorToInt(resources[ResourceType.Wood]).ToString() : "0";
        currentStone.text = resources.ContainsKey(ResourceType.Stone) ? Mathf.FloorToInt(resources[ResourceType.Stone]).ToString() : "0";
    }

    private void UpdateMaxResources()
    {
        maxresources = new Dictionary<ResourceType, int>();

        foreach (var m in lastMagazines)
        {
            if (m.GetComponentInParent<PlecableObject>().Placed)
            {
                foreach (var n in m.amount)
                {
                    if (maxresources.ContainsKey(n.Key))
                    {
                        maxresources[n.Key] += n.Value;
                    }
                    else
                    {
                        maxresources.Add(n.Key, n.Value);
                    }
                }
            }
        }

        maxWood.text = maxresources.ContainsKey(ResourceType.Wood) ? maxresources[ResourceType.Wood].ToString() : "150";
        maxStone.text = maxresources.ContainsKey(ResourceType.Stone) ? maxresources[ResourceType.Stone].ToString() : "150";
    }
    private void UpdateResourcesMakers()
    {
        resourceGrowth = new Dictionary<ResourceType, float>();
        foreach (var m in lastResouceMaker)
        {
            if (m.GetComponentInParent<PlecableObject>().Placed)
            {
                foreach (var n in m.make)
                {
                    if (resourceGrowth.ContainsKey(n.Key))
                    {
                        resourceGrowth[n.Key] += n.Value;
                    }
                    else
                    {
                        resourceGrowth.Add(n.Key, n.Value);
                    }
                }
            }
        }
    }

    public bool usedResources(Resource[] usedResources)
    {
        foreach (var i in usedResources)
        {
            if (!resources.ContainsKey(i.name) || resources[i.name] - i.amount < 0)
            {
                return false;
                
            }
        }
        foreach (var i in usedResources)
        {
            resources[i.name] -= i.amount;
        }
        return true;
    } 
}
