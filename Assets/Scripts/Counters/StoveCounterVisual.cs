using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {
    [SerializeField]
    private StoveCounter stoveCounter;
    [SerializeField]
    private GameObject stoveOnGameObject;
    [SerializeField]
    private GameObject particlesGameObject;


    private void Awake() {
    }

    private void Start() {
        stoveCounter.onStageChanged += StoveCounter_onStageChanged;
    }

    private void StoveCounter_onStageChanged(object sender, StoveCounter.OnStageChangedEventArgs e) {
        bool showVisual = e.state == StoveCounter.State.Frying;
        stoveOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}
