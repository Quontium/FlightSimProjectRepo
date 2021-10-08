using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectWraith : MonoBehaviour
{
    private void Awake()
    {
        UIControllerWraith ui = GetComponentInParent<UIControllerWraith>();
        if (ui == null)
        {
            ui = GameObject.Find("World").GetComponent<UIControllerWraith>();
        }

        if (ui == null) Debug.LogError("No UIControllerWraith component found");

        ui.AddTargetIndicator(this.gameObject);
    }
}