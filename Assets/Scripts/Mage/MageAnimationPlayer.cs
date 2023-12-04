using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MageAnimationPlayer : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private string currentState;

    private void Start()
    {
        animator ??= GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}