using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    #region Singelton
    public static AnimationManager instance;

    private void Awake() {
        // Check if instance is already defined and if this gameObject is not the current instance.
        if (instance != null) {
            Debug.LogWarning("Multiple Instances of AnimationManager found. Current instance was destroyed.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    /// <summary>
    /// Enumeration of all possible AnimationErrors.
    /// </summary>
    public enum AnimationError {
        AlreadyPlaying = 0,
        DoesNotExist = 1,
        Success = 2,
    }

    /// <summary>
    /// Plays the Animation with the given name on the given layer and with the given startTime,
    /// if it isn't already playing and if it actually exist on the given Layer and Animator.
    /// </summary>
    /// <param name="animator">Animator we wan't to call the Animations on.</param>
    /// <param name="newAnimation">Name of the Animation we wan't to play.</param>
    /// <param name="delayTime">Time until we want to start the Animation.</param>
    /// <param name="startTime">Time we want to start the Animation from.</param>
    /// <param name="layer">Layer we find the given Animation Name on.</param>
    /// <returns>AnimationError with all possible Error States of the function.</returns>
    private AnimationError PlayAnimation(Animator animator, string newAnimation,  float delayTime = 0.0f, float startTime = 0.0f, int layer = 0) {
        AnimationError message = AnimationError.Success;

        // Fetch the current Animation clips information for the base layer.
        List<AnimatorClipInfo> currentClipInfoList = new List<AnimatorClipInfo>();
        animator.GetCurrentAnimatorClipInfo(layer, currentClipInfoList);

        string currentAnimaton = string.Empty;

        if (currentClipInfoList.Any()) {
            // Access the current Animation clip name if we are currently playing one.
            AnimatorClipInfo currentClipInfo = currentClipInfoList.FirstOrDefault();

            if (currentClipInfo.clip != null) {
                currentAnimaton = currentClipInfo.clip.name;
            }
        }

        // Check if we are already playing the newAnimation.
        if (string.Equals(currentAnimaton, newAnimation)) {
            message = AnimationError.AlreadyPlaying;
        }
        // Check if the given Animator actually has the newAnimation at all.
        else if (!AnimationExists(animator, newAnimation)) {
            message = AnimationError.DoesNotExist;
        }
        // Play the newAnimation at the given startTime.
        else {
            StartCoroutine(Play(animator, newAnimation, delayTime, startTime, layer));
        }

        return message;
    }

    /// <summary>
    /// Check if the given Animator acutally contains the given AnimationClip.
    /// </summary>
    /// <param name="animator">Animator component to get all animationclips from.</param>
    /// <param name="newAnimation">Animation Name we want to check for.</param>
    /// <returns>Boolean that is true if any clips in the given Animator have the given name.</returns>
    private bool AnimationExists(Animator animator, string newAnimation) {
        // Fetch all Animations from the given Animator.
        AnimationClip[] allclips = animator.runtimeAnimatorController.animationClips;

        // Check if any Animations in the Animator have the given clip name. 
        return allclips.Any(animation => string.Equals(animation.name, newAnimation));
    }

    /// <summary>
    /// Get the Length of the Animation that is playing currently on the given Animator in the given Layer.
    /// </summary>
    /// <param name="animator">Animator component to get all animationclips from.</param>
    /// <param name="layer">Layer we find the currently playing Animation on.</param>
    /// <returns>Float that is equal to the length of the clip.</returns>
    public float GetCurrentAnimationLength(Animator animator, int layer = 0) {
        // Fetch the current Animation clips information for the base layer.
        AnimatorClipInfo[] currentClipInfo = animator.GetCurrentAnimatorClipInfo(layer);

        // Access the current Animation clip length.
        return currentClipInfo[0].clip.length;
    }

    private IEnumerator Play(Animator animator, string newAnimation, float delayTime, float startTime, int layer) {
        yield return new WaitForSeconds(delayTime);
        animator.Play(newAnimation, layer, startTime);
    }
}
