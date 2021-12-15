using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCollectible : Collectible
{
    private void Update()
    {
        transform.Rotate(Vector3.up, 45.0f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp(this.gameObject);
        }
    }

    protected override void PickUp(GameObject obj)
    {
        ServiceLocator.Instance.GetLevelManager().FinishLevel();
        Destroy(obj);
    }
}
