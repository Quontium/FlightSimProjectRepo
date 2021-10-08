using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectEnemy : MonoBehaviour
{
    private void Awake()
    {
        UIControllerEnemy ui = GetComponentInParent<UIControllerEnemy>();
        if (ui == null)
        {
            ui = GameObject.Find("World").GetComponent<UIControllerEnemy>();
        }

        if (ui == null) Debug.LogError("No UIControllerEnemy component found");

        ui.AddTargetIndicator(this.gameObject);
    }
}