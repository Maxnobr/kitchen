using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStageChangedEventArgs> onStageChanged;
    public class OnStageChangedEventArgs : EventArgs {
        public State state;
    }

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;


    [SerializeField]
    private FryingRecipeSO[] fryingRecipeSOArray;

    private float fryingTimer;
    private FryingRecipeSO currentRecipe;

    public enum State {
        Idle,
        Frying,
        Burned,
    }

    private State state;


    private void Start() {
        state = State.Idle;
    }
    private void FixedUpdate() {
        if (HasKitchenObject() && state == State.Frying){
            fryingTimer += Time.deltaTime;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = fryingTimer / currentRecipe.fryingTimerMax
            });

            if (fryingTimer >= currentRecipe.fryingTimerMax) {
                    KitchenObject.DestroySelf();
                    KitchenObject.SpawnKitchenObject(currentRecipe.output, this);
                    currentRecipe = GetSOForInput(currentRecipe.output);
                    fryingTimer = 0f;

                if (!currentRecipe) {
                    state = State.Burned;
                    onStageChanged?.Invoke(this, new OnStageChangedEventArgs {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = 0f
                    });;
                }
            }
        }


        //if (HasKitchenObject() && currentRecipe != null) {
            
        //}
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            if (HasKitchenObject()) {
                KitchenObject.KitchenObjectParent = player;
                state = State.Idle;
                onStageChanged?.Invoke(this, new OnStageChangedEventArgs {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = 0f
                });
            }
        }
        else if (!HasKitchenObject()) {
            FryingRecipeSO fryingRecipeSO = GetSOForInput(player.KitchenObject.GetKitchenObjectSO());
            if (fryingRecipeSO) {
                player.KitchenObject.KitchenObjectParent = this;
                currentRecipe = fryingRecipeSO;
                state = State.Frying;
                fryingTimer = 0f;
                onStageChanged?.Invoke(this, new OnStageChangedEventArgs {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = 0f
                });
            }
        }
    }

    private FryingRecipeSO GetSOForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
            if (fryingRecipeSO.input == inputKitchenObjectSO)
                return fryingRecipeSO;
        return null;
    }
}
