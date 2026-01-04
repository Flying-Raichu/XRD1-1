# Catarina -Storyline, Setup of the project and introduction
For this project, we focused on an Indiana Jones inspired escape room. Firstly, the player is presented with a short introduction to 
give the context. The introduction also serves as a transition moment for the player to adapt to the transition into the VR game. In the 
beginning, the player spawns in the middle of a temple and is prompted to solve enigmas throughout the experience to try to escape.

The project was created with the VR core template. To create the three existent scenes, the sample scene was duplicated and then adapted the
GameObjects present to each one of the scene’s needs.

For the construction of the environment on all the scenes, it is composed of a plane, four walls and 4 columns, all prefabs of the correspondent
GameObjects. For all of them, base assets to build materials were imported (sources bellow). For each type of structure, there was a material 
created to be able to insert the assets imported into the actual game. The purplish color tones were put in the Normal color slot, the beiger 
tones in the Base color slot and the actual texture on the “Metallic map”. All the GameObjects that are part of the core structure of the 
temple have a box collider so that the player does not go through them.

All the scenes have the same existent component XR Origin (VR), which is the component responsible for the main setup of the player and the 
way the player can interact with the environment around. To configure some of the embodiments of the player inside the game, the character 
controller component in the XR Origin was adapted to set the player to a reasonable height and radius to set.


## Introduction scene
Regarding the introduction scene, the main focus is on the following: the UI, with contains two canvas (one for the black background and the 
other for the text) and the IntroSceneManager class, to manage the UI transitions and the transition into the main scene.

In the IntroSceneManagerController.cs there are two private variables: the introduction UI panel from the type CanvasGroup and a fadeDuration 
variable of type float. Both of these variables are private as it is good practice, but both are Serialized fields as to be reached from the 
Unity Editor. Lastly there is also an Image object, which is there to hold the black background for the transition in the introduction.

In the Start() method, the coroutine is started with the StartIntro() method and the introPanel UI set to false.

In this script, the highlight is on the StartIntro() method, which is from type IEnumerator, as coroutines were used to make the introduction
possible. IEnumerator methods are the ones mostly used when dealing with coroutines as they are iterators that go through the set path of 
events. In this method, we take the color of the fadeImage variable and set it to value 1 alpha, which means it will be completely visible 
and then it sets it as the new background color. In order to trigger steps only after a certain amount of time or a specific event, yield is 
the keyword used. In the StartIntro() method, there is a moment of wait before starting another coroutine, Fade(), which is used to smoothen 
the transition into the introduction text and then into the next scene. Finally in the StartIntro() function, the UI panel’s alpha is set to 
true, yield is used again to hold the sequence for a couple of seconds and the next scene is loaded.

Regarding the Fade() method, it is a method as mentioned above to smoothen the introduction’s transitions. This was a method important to 
include due to the players possible bad reaction to fast transitions and color environment changes when wearing the VR glasses, as those 
factors may cause for example discomfort and dizziness. In this method, In order to control the fade between the sequence’s steps, firstly 
we get the current transparency and set a time variable to zero. Following there is a while loop so that while the time spent is lower than 
the time set for the duration of the fade, the transparency values can transition smoothly from 1 to 0 using the Mathf.Lerp . Finally the 
transparency is set to the maximum when the time spent equals the time set for the transition.
