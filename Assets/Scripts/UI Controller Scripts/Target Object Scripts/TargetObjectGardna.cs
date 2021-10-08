using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectGardna : MonoBehaviour
{
    private void Awake()
    {
        UIControllerGardna ui = GetComponentInParent<UIControllerGardna>();
        if (ui == null)
        {
            ui = GameObject.Find("World").GetComponent<UIControllerGardna>();
        }

        if (ui == null) Debug.LogError("No UIControllerGardna component found");

        ui.AddTargetIndicator(this.gameObject);
    }
}