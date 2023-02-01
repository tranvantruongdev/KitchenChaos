using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    private const string IS_WALKING = "IsWalking";

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Player player;

    // Update is called once per frame
    void Update() {
        animator.SetBool(IS_WALKING, player.IsWalking);
    }
}
