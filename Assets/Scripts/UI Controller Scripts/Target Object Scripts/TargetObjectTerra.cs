using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectTerra : MonoBehaviour
{
    private void Awake()
    {
        UIControllerTerra ui = GetComponentInParent<UIControllerTerra>();
        //UIControllerAllyName ui = GetComponentInParent<UIControllerAllyName>();
        if (ui == null)
        {
            ui = GameObject.Find("World").GetComponent<UIControllerTerra>();
        }

        if (ui == null) Debug.LogError("No UIControllerEmberTerra component found");

        ui.AddTargetIndicator(this.gameObject);
    }
}