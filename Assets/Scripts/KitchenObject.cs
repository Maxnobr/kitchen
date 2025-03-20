using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] 
    private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent _kitchenObjectParent;

    public IKitchenObjectParent KitchenObjectParent { 
        get { return _kitchenObjectParent; } 
        set {
            _kitchenObjectParent?.ClearKitcheObject();
            _kitchenObjectParent = value;

            if (_kitchenObjectParent.HasKitchenObject()) {
                Debug.LogError("IKitchenObjectParent already has a KitchenObject!");
            }

            _kitchenObjectParent.KitchenObject = this;
            transform.parent = _kitchenObjectParent.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        } 
    }

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void DestroySelf() {
        KitchenObjectParent.ClearKitcheObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.KitchenObjectParent = kitchenObjectParent;
        return kitchenObject;
    }
}
