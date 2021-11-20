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
        AnimationManager.AnimationError err = am.PlayAnimation(animator, inputField.text);
        output.text = GetStringFromError(err);
    }

    public void GetCurrentAnimationLengthClicked() {
        float length = am.GetCurrentAnimationLength(animator);
        output.text = "Length of the currently playing animation in seconds: " + length;
    }

    private string GetStringFromError(AnimationManager.AnimationError err) {
        string warning = "";

        switch (err) {
            case AnimationManager.AnimationError.OK:
                warning = "Succesfully executed playing animation.";
                break;
            case AnimationManager.AnimationError.ALREADY_PLAYING:
                warning = "Method is already playing.";
                break;
            case AnimationManager.AnimationError.DOES_NOT_EXIST:
                warning = "Given Animation Name does not exist  on the given Animator.";
                break;
            default:
                // Invalid AnimationManager.AnimationError argument.
                break;
        }

        return warning;
    }
}
