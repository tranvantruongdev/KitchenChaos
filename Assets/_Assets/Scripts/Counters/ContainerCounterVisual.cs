using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour {
    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private Animator animator;
    [SerializeField] private ContainerCounter containerCounter;

    // Start is called before the first frame update
    private void Start() {
        containerCounter.OnPlayerGrabbedKitchenObject += ContainerCounter_OnPlayerGrabbedKitchenObject;
    }

    private void ContainerCounter_OnPlayerGrabbedKitchenObject(object sender, System.EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
