using UnityEngine;

public class LootAtCamera : MonoBehaviour {
    [SerializeField] private Mode mode;

    private enum Mode {
        LookAt,
        LookAtInverted,
        LookForward,
        LookForwardInverted,
    }

    private void LateUpdate() {
        switch (mode) {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                var dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.LookForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.LookForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
            default:
                break;
        }
    }
}
