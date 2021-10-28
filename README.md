![Unity Animation Manager](https://github.com/MathewHDYT/Unity-Animation-Manager-UAM/blob/main/logo.png/)

[![MIT license](https://img.shields.io/badge/License-MIT-yellow.svg?style=flat-square)](https://lbesson.mit-license.org/)
[![Unity](https://img.shields.io/badge/Unity-5.2%2B-green.svg?style=flat-square)](https://docs.unity3d.com/520/)
[![GitHub release](https://img.shields.io/github/release/MathewHDYT/Unity-Animation-Manager-UAM/all.svg?style=flat-square)](https://github.com/MathewHDYT/Unity-Animation-Manager-UAM/releases/)
[![GitHub downloads](https://img.shields.io/github/downloads/MathewHDYT/Unity-Animation-Manager-UAM/all.svg?style=flat-square)](https://github.com/MathewHDYT/Unity-Animation-Manager-UAM/releases/)

# Unity Animation Manager (UAM)
Used to simply play animations without having to create transitions, while giving the opportunity to create them via. code instead.

## Contents
- [Unity Animation Manager (UAM)](#unity-animation-manager-uam)
  - [Contents](#contents)
  - [Introduction](#introduction)
  - [Installation](#installation)
- [Documentation](#documentation)
  - [Reference to Animation Manager Script](#reference-to-animation-manager-script)
  - [Possible Errors](#possible-errors)
  - [Inspiration](#inspiration)
  - [Public accesible methods](#public-accesible-methods)
  	- [Play Animation method](#play-animation-method)
	- [Get Current Animation Length method](#get-current-animation-length-method)

## Introduction
A lot of games use the animation tree to create transitions between animations this small and easily integrated Animation Manager can help you play animations without having to create transitions that will result in animation trees like this.

![Image of Animation Tree](https://www.gamasutra.com/db_area/images/blog/183567/base_state.jpg)

**Unity Animation Manager implements the following methods consisting of a way to:**
- Play an Animation with additional error message depeding if starting to play succeded or failed (see [Play Animation method](#play-animation-method))

For each method there is a description on how to call it and how to use it correctly for your game in the given section.

## Installation
**Required Software:**
- [Unity](https://unity3d.com/get-unity/download) Ver. 2020.3.17f1

The Animation Manager itself is version independent, as long as the Animator object already exists. Additionally the example project can be opened with Unity itself or the newest release can be downloaded and exectued to test the functionality.

If you prefer the first method, you can simply install the shown Unity version and after installing it you can download the project and open it in Unity (see [Opening a Project in Unity](https://docs.unity3d.com/2021.2/Documentation/Manual/GettingStartedOpeningProjects.html)). Then you can start the game with the play button to test the Animation Managers functionality.

To simply use the Animation Manager in your own project without downloading the Unity project get the file in the **Example Project/Assets/Scritps/** called ```AnimationManager.CS``` or alternatively get the file from the newest release (may not include the newest changes) and save them in your own project. Then create a new empty ```gameObject``` and attach the ```AnimationManager.CS``` script to it.

# Documentation
This documentation strives to explain how to start using the Animation Manager in your project and explains how to call and how to use its publicly accesible methods correctly.

## Reference to Animation Manager Script
To use the Animation Manager to start playing sounds outside of itself you need to reference it. As the Animation Manager is a [Singelton](https://stackoverflow.com/questions/2155688/what-is-a-singleton-in-c) this can be done easily when we get the instance and save it as a private variable in the script that uses the Animation Manager.

```csharp
private AnimationManager am;
private Animator animator;

const string PLAYER_ATTACK = "Player_attack";

private void Start() {
    am = AnimationManager.instance;
    animator = gameObject.GetComponent<Animator>();

    // Calling Function in AnimationManager
    am.PlayAnimation(animator, PLAYER_ATTACK);
}
```

Alternatively you can directly call the methods this is not advised tough, if it is executed multiple times or you're going to need the instance multiple times in the same file.

```csharp
private Animator animator;

const string PLAYER_ATTACK = "Player_attack";

private void Start() {
    animator = gameObject.GetComponent<Animator>();
    // Calling Function in AnimationManager
    am.PlayAnimation(animator, PLAYER_ATTACK);
}
```

## Possible Errors

| **ID** | **CONSTANT**                  | **MEANING**                                                                                    |
| -------| ------------------------------| -----------------------------------------------------------------------------------------------|
| 0      | SUCCESS                       | Method succesfully executed                                                                    |
| 1      | ALREADY_PLAYING               | The given animation is already playing currently                                               |
| 2      | DOES_NOT_EXIST                | Given animation does not exist on the given animator or layer                                  |

## Inspiration
The creation of this Animation Manager has been inspired by the [Escaping Unity Animator HELL](https://youtu.be/nBkiSJ5z-hE) video.

## Public accesible methods
This section explains all public accesible methods, especially what they do, how to call them and when using them might be advantageous instead of other methods. We always assume AnimationManager instance has been already referenced in the script. If you haven't done that already see [Reference to Animation Manager Script](#reference-to-animation-manager-script).

### Play Animation method
**What it does:**
Starts playing the choosen animation if possible and returns an AnimationError (see [Possible Errors](#possible-errors)), showing wheter and how playing the animation failed.

**How to call it:**
- ```Animator``` is the animator the given animation is contained in
- ```NewAnimation``` is the name of the animation we want to play
- ```Layer``` is the layer on the animator that the animation resides on
- ```StartTime``` is the time in the animation we want to start playing at


```csharp
Animator animator = gameObject.GetComponent<Animator>();
string newAnimation = "Player_attack";
int layer = 0;
float startTime = 0.0f;
AnimationManager.AninmationError err = am.PlayAnimation(animator, newAnimation, layer, startTime);
if (err != AnimationManager.AninmationError.OK) {
    Debug.Log("Playing animation failed with error id: ", err);
}
else {
    Debug.Log("Playing animation successful");
}
```

Alternatively you can call the methods with less paramters as some of them have default arguments.

```csharp
Animator animator = gameObject.GetComponent<Animator>();
string newAnimation = "Player_attack";
AnimationManager.AninmationError err = am.PlayAnimation(animator, newAnimation);
if (err != AnimationManager.AninmationError.OK) {
    Debug.Log("Playing animation failed with error id: ", err);
}
else {
    Debug.Log("Playing animation successful");
}
```

**When to use it:**
When you want to play an animation directly without any transitions.

See [```Animator.Play```](https://docs.unity3d.com/2021.2/Documentation/ScriptReference/Animator.Play.html) for more details on what Play Animation does.

### Get Current Animation Length method
**What it does:**
Returns the ```length``` of the currently playing animation clip.

**How to call it:**
- ```Animator``` is the animator the given animation is contained in
- ```Layer``` is the layer on the animator that the animation resides on


```csharp
Animator animator = gameObject.GetComponent<Animator>();
int layer = 0;
float length = am.GetCurrentAnimationLength(animator, layer);
Debug.Log("Current animation has a length of: " + length + " seconds");
```

Alternatively you can call the methods with less paramters as some of them have default arguments.

```csharp
Animator animator = gameObject.GetComponent<Animator>();
float length = am.GetCurrentAnimationLength(animator);
Debug.Log("Current animation has a length of: " + length + " seconds");
```

**When to use it:**
When you want to know the length of the current animation.
