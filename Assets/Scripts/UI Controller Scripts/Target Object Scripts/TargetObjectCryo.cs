using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectCryo : MonoBehaviour
{
    private void Awake()
    {
        UIControllerCryo ui = GetComponentInParent<UIControllerCryo>();
        //UIControllerAllyName ui = GetComponentInParent<UIControllerAllyName>();
        if (ui == null)
        {
            ui = GameObject.Find("World").GetComponent<UIControllerCryo>();
        }

        if (ui == null) Debug.LogError("No UIControllerEmberCryo component found");

        ui.AddTargetIndicator(this.gameObject);
    }
}