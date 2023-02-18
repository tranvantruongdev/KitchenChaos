using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {
    private const string CUT = "Cut";

    [SerializeField] private Animator animator;
    [SerializeField] private CuttingCounter cuttingCounter;

    // Start is called before the first frame update
    private void Start() {
        cuttingCounter.OnProgressChanged += ContainerCounter_OnCuttingProgressChanged;
    }

    private void ContainerCounter_OnCuttingProgressChanged(object sender, System.EventArgs e) {
        animator.SetTrigger(CUT);
    }
}
