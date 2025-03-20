using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent {

    public Transform GetKitchenObjectFollowTransform();
    public KitchenObject KitchenObject { get; set; }
    public void ClearKitcheObject();
    public bool HasKitchenObject();
}
