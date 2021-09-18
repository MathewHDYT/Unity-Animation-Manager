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
        am.PlayAnimation(animator, inputField.text);
    }

    public void GetCurrentAnimationLengthClicked() {
        float length = am.GetCurrentAnimationLength(animator);
        output.text = "Length of the currently playing animation in seconds: " + length;
    }
}
