using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField]
    private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;
    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            if (HasKitchenObject() && cuttingProgress == 0) {
                KitchenObject.KitchenObjectParent = player;
            }
        }
        else if (!HasKitchenObject()) {
            player.KitchenObject.KitchenObjectParent = this;
        }
    }

    public override void InteractAlternative(Player player) {
        if (HasKitchenObject()) {
            CuttingRecipeSO cutKitchenObjectSO = GetOutputForInput(KitchenObject.GetKitchenObjectSO());
            if (cutKitchenObjectSO != null) {
                cuttingProgress++;
                if (cuttingProgress >= cutKitchenObjectSO.cuttingProgressMax) {
                    KitchenObject.DestroySelf();
                    KitchenObject.SpawnKitchenObject(cutKitchenObjectSO.output, this);
                    cuttingProgress = 0;
                }
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = (float)cuttingProgress / cutKitchenObjectSO.cuttingProgressMax
                });
            }
        }
    }

    private CuttingRecipeSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
                return cuttingRecipeSO;
        return null;
    }
}
