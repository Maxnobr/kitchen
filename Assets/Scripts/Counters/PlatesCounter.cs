using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    //public event EventHandler OnPLeayerGrabbedObject;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;

    public override void Interact(Player player) {
        if (!player.HasKitchenObject())
            if (HasKitchenObject())
                KitchenObject.KitchenObjectParent = player;
        else if (!HasKitchenObject()) {
            player.KitchenObject.KitchenObjectParent = this;
        }
    }

    public void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= spawnPlateTimerMax) {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
        }
    }
}
