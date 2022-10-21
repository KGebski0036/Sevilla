using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumsAndStructs : MonoBehaviour
{
    [System.Serializable]
    public enum ResourceType
    {
        Wood,
        Stone
    };

    [System.Serializable]
    public struct Resource
	{
		public ResourceType name;
		public int amount;
	}

    [System.Serializable]
    public enum BuildingsType
    {
        Castle,
        Mine,
        Sawmill,
        Hause
    };
}
