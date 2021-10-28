using UnityEngine;
using UnityEngine.UI;

public class MethodCalls : MonoBehaviour {

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Text output;
    [SerializeField]
    private InputField inputField;

    private AnimationManager am;

    private void Start() {
        am = AnimationManager.instance;
    }

    public void PlayAnimationClicked() {
        AnimationManager.AninmationError error = am.PlayAnimation(animator, inputField.text);
        output.text = GetStringFromError(error);
    }

    public void GetCurrentAnimationLengthClicked() {
        float length = am.GetCurrentAnimationLength(animator);
        output.text = "Length of the currently playing animation in seconds: " + length;
    }

    private string GetStringFromError(AninmationManager.AnimationError error) {
        switch (error) {
            case AnimationManager.AnimationError.OK:
                // No Message
                return;
            case AnimationManager.AnimationError.ALREADY_PLAYING:
                // No Message
                return;
            case AnimationManager.AnimationError.DOES_NOT_EXIST:
                warning = "Given Animation Name does not exist  on the given Animator.";
                break;
            default:
                warning = "Given AnimatonError State not defined.";
                break;
        }
    }
}
