using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnumsAndStructs;

public class ConditionMenager : MonoBehaviour
{
    public Dictionary<BuildingsType, int> buildings = new Dictionary<BuildingsType, int>();

    [SerializeField] Button castle;
    [SerializeField] Button mine;
    [SerializeField] Button sawmill;
    [SerializeField] Button house;

    private void Start()
    {
        mine.enabled = false;
        sawmill.enabled = false;
        house.enabled = false;
    }

    public void AddBuilding(string name)
    {

        if(name == "Castle")
        {
            buildings.Add(BuildingsType.Castle, 1);
            castle.enabled = false;
            sawmill.enabled = true;
        }
        else if(name == "Sawmill")
        {
            if (buildings.ContainsKey(BuildingsType.Sawmill))
                buildings[BuildingsType.Sawmill]++;
            else
                buildings.Add(BuildingsType.Sawmill, 1);
            mine.enabled = true;
        }
        else if (name == "Mine")
        {
            if (buildings.ContainsKey(BuildingsType.Mine))
                buildings[BuildingsType.Mine]++;
            else
                buildings.Add(BuildingsType.Mine, 1);
            house.enabled = true;
        }
        else if (name == "House")
        {
            if (buildings.ContainsKey(BuildingsType.Hause))
                buildings[BuildingsType.Hause]++;
            else
                buildings.Add(BuildingsType.Hause, 1);
        }
    }
}
