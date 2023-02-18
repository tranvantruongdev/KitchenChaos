using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {
    [SerializeField] private GameObject stoveOn;
    [SerializeField] private GameObject fryingParticle;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start() {
        stoveCounter.OnFryStateChanged += StoveCounter_OnFryStateChanged;
    }

    private void StoveCounter_OnFryStateChanged(object sender, StoveCounter.FryStateChangedArgs e) {
        var isShowFryingEffect = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Cooked;
        stoveOn.SetActive(isShowFryingEffect);
        fryingParticle.SetActive(isShowFryingEffect);
    }
}
