using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

    [SerializeField]
    private Transform counterTopPoint;
    public KitchenObject KitchenObject { get; set; }

    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlternative(Player player) {
        //Debug.LogError("BaseCounter.InteractAlternative();");
        //DO NOTHING
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void ClearKitcheObject() {
        KitchenObject = null;
    }

    public bool HasKitchenObject() {
        return KitchenObject != null;
    }
}
