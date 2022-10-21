using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildMenager : MonoBehaviour
{
    public Canvas buildMenu;
    [SerializeField] AudioSource maudio;
    [SerializeField] AudioClip sound;

    private ConditionMenager conditionMenager;

    private void Awake()
    {
        conditionMenager = GetComponent<ConditionMenager>();
    }

    public void InitializeBuilding(GameObject prefab)
    {
         if (BuildingSystem.instance.objectToPlace != null)
            Destroy(BuildingSystem.instance.objectToPlace.gameObject);

        BuildingSystem.instance.InitializeWithObject(prefab);
        buildMenu.enabled = false;
        conditionMenager.AddBuilding(prefab.name);

        maudio.PlayOneShot(sound);
    }
}
