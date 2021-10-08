using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectAllyName : MonoBehaviour
{
    private void Awake()
    {
        UIControllerAllyName ui = GetComponentInParent<UIControllerAllyName>();
        if (ui == null)
        {
            ui = GameObject.Find("World").GetComponent<UIControllerAllyName>();
        }

        if (ui == null) Debug.LogError("No UIControllerAllyName component found");

        ui.AddTargetIndicator(this.gameObject);
    }
}