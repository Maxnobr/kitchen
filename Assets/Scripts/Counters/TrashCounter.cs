using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(Player player) {
        player.KitchenObject?.DestroySelf();
    }
}
