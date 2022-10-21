﻿using UnityEngine;

public class SelectableCharacter : MonoBehaviour {

    public SpriteRenderer selectImage;

    private void Awake() {
        selectImage.enabled = false;
    }

    //Turns off the sprite renderer
    public void TurnOffSelector()
    {
        selectImage.enabled = false;
        GetComponentInParent<UnitControler>().selected = false;
    }

    //Turns on the sprite renderer
    public void TurnOnSelector()
    {
        selectImage.enabled = true;
        GetComponentInParent<UnitControler>().selected = true;

    }

}
