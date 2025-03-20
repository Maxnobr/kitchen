using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPLeayerGrabbedObject;

    public override void Interact(Player player) {
        if (!player.HasKitchenObject())
            if (!HasKitchenObject()) {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
                OnPLeayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
            else
                KitchenObject.KitchenObjectParent = player;
        else if (!HasKitchenObject()) {
            player.KitchenObject.KitchenObjectParent = this;
        }
    }
}
