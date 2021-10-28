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
        AninmationManager.AninmationError error = am.PlayAnimation(animator, inputField.text);
        output.text = GetStringFromError(error);
    }

    public void GetCurrentAnimationLengthClicked() {
        float length = am.GetCurrentAnimationLength(animator);
        output.text = "Length of the currently playing animation in seconds: " + length;
    }

    private string GetStringFromError(AninmationManager.AnimationError error) {
        switch (error) {
            case AnimationError.Id.AlreadyPlaying:
                // No Message
                return;
            case AnimationError.Id.DoesNotExist:
                warning = "Given Animation Name does not exist  on the given Animator.";
                break;
            case AnimationError.Id.Success:
                // No Message
                return;
            default:
                warning = "Given AnimatonError State not defined.";
                break;
        }
    }
}
