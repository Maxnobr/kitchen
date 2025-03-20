using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            if (HasKitchenObject())
                KitchenObject.KitchenObjectParent = player;
        }
        else if (!HasKitchenObject())
            player.KitchenObject.KitchenObjectParent = this;
    }
}
