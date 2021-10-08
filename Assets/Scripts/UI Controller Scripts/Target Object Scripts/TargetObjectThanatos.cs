using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectThanatos : MonoBehaviour
{
    private void Awake()
    {
        UIControllerThanatos ui = GetComponentInParent<UIControllerThanatos>();
        if (ui == null)
        {
            ui = GameObject.Find("World").GetComponent<UIControllerThanatos>();
        }

        if (ui == null) Debug.LogError("No UIControllerThanatos component found");

        ui.AddTargetIndicator(this.gameObject);
    }
}