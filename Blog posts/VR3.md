# Catarina and Lyubomir- Ending scene, problems faced and final touches

## The Ending scene
The third scene is the Ending scene, which is the is where the player discovers the end of the game- a mummy attack, as there was no possible 
escape in the first place. This ending was created with a twist to the original classic storyline of escape rooms as the intention was to catch 
the player off-guard and offer a surprising ending.

In the OutroSceneManager, there are three UI’s (one for the ending message, one for the mummy image and one for the cliff hanger), two audio
sources for the mummy jumpscare and three float variables to define timing regarding this three steps.

In the Start function, Only the professor UI is set to full visiblity while the other panels are set to be invisible. The PlaySequence 
coroutine is started.

In the PlaySequence() method, yield is used for waiting a couple of seconds before playing the footsteps audio, and then again before the 
jumpscare audio is played. There is a while loop that uses again the Mathf.Lerp function to fade the visibility of the mummy panel before 
yield is used again, as to introduce the fade in of the outro panel. Lastly there is a RestartScene() method for the functionality of the 
restart button.

The Ending scene was made very similar to the Introduction scene, as both of them are build with the same goal of showing transition of UI 
panels, with the exception this scene also includes a restart button. The functionality for this button was implemented with an 
OnCollisionEnter() method that triggers another method called RestartScene() in which the SceneManager loads the introduction scene. The 
script for this functionality, also like in the introduction scene, is attached to an empty GameObject in the unity editor.


## Problems faced
Throughout the development of this game, there were a couple of problems that presented themselves as a challenge. The first major problem 
were merging conflicts. The GitHub repository for this game was build on the following architecture: Each branch concerns a step of the game. 
Every pull request is merged to sandbox and then from sandbox to main. This had the goal of preventing the merge of faulty code. The problems
lied on the fact that several parts of the game were developed simultaneously and had commits to sandbox, and therefore there were conflicts
on the scenes themselves. This made merging everything together almost impossible, losing some progress when the merge was finally successful 
as not every piece of code could persist, in risk of clashing with some other change. For this merge, the following merging tool was used:

Broken assets was one of the problems faced. In the TMP packages, two of the assets were broken, which was portraited by Unity by making the 
assets hot pink. A lot of fixes were tried, including reinstalling the TMP packages and the extras. The winning solution was to just move
permanently the files somewhere else. Thankfully only one of the group members faced this issue.

The last big issue faced was understanding how to set up XR device simulator as duplicate hidden functionality in the scripts and created
GameObjects, e.g. locomotion, made it impossible for the simulator to run properly.


## Additions
Because it is a VR project, giving the player an immersive feeling is also a core part of the experience. To be help contribute to the 
realism of the environment some GameObjects were added to the scenes, such as torches.
