# Catarina- The main scene

Right after being spawned into the scene, the player is presented with the first clue. The player should then find a key, situated on the 
altar with all the drawings and use the key to open a music box that is on top of a piece of furniture. Only after collecting the key the 
music box opens and reveals a short video with a small melody, which is the second clue.

To perform all these steps, there is the following logic behind:

## Key
For the key functionality, we have a KeyHandler class where we have a KeyCollectedUI CanvasGroup and a bool static variable to check if 
the player has the key, which is privately set in this class, but can be accessed by other classes.

In the Start () method the KeyCollectedUI is set to not be active after checking if it exists.

Next there is a method called TryCollectKey() where it checks if the player has collected the key already. If not then it sets the variable 
KeyCollected as true and starts the HandleKeyCollectionSequence coroutine.

The HandleKeyCollectionSequence() method is responsible for activating the KeyCollectedUI, waiting for a couple of seconds and deactivating 
it, so that the player can be notified of the collection of the key, which is destroyed at the end of this sequence, to avoid further 
confused for the player.

Regarding the Unity Editor, the key was set on the scene with a quad as it is easier to manage its position than another plane, and the image
of the key itself was converted into a material by inserting the raw image in the Base map slot and the surfice type made transparent, just 
like the materials for the floor and walls. The material was applied to the quad and the tiling was adjusted to make the proportions appear 
more realistic. The KeyHandler script was attached to the key object and the pertinent GameObject variables of the script assigned.


## Music Box
In order to open the music box, in the KeyHandler class the variable bool hasKey was set to public static so that in the MusicHandler class 
we could use that information to reveal or not the next clue.

In this class we have two different UI’s, one to display a message if the music box is opened and another to display a message if the box is 
locked. There is also a variable for the music box video and the hasKey variable from type KeyHandler.

In the Start() function, all the variables but hasKey are set to false or deactivated, and the audio for the music video is fetched and 
stopped.

Then there is a function called when the user attempts to open the music box where all coroutines are stopped and the OpenMusicBox coroutine 
is started.

In this OpenMusicBox method, it checks if the player has the key and if so activates the UI correspondent and plays the audio and video. 
If the player does not have a key, then it activates the musicBoxLockedUI.


## Music Cubes
Using the melody from the music box, the player should then reproduce the sequence with the music cubes also in the altar. After the sequence 
is performed, another clue hints at the final tragedy and the user is redirected to the ending scene.

To make sure that the functionality of the music cubes worked, there are two classes, one to control the music cubes, and another one to
manage them.

In the MusicCubeController class, there are four variables: a variable note of string type to hold the id of the note played, a variable to
hold the audio source, a variable to hold the actual music clip and a manager of type MusicCubeManager.

The first function is an Awake() function, where we initialize the manager and fetch the audio source component that is linked through the
unity editor. Following that we have a function dedicated to the functionality regarding the cube actually being selected called 
OnCubeSelected() where the private functions PlayNote() and SendToManager() are called. These last two are set private as there only needs 
to be one public exposition (the OnCubeSelected method) since the functions mentioned are how the cubes function internally. Still regading 
the last two functions, PlayNote() checks if the audio clip and audio source exist, attaches the clip to the audio source and plays it. For 
the SendNoteToManager class, it checks if the manager is null and then uses the manager variable to access a method called AddNoteToMelody 
to register which note was played.

Regarding the MusicCubeManager class, we have 2 lists both type string (one to hold the correct melody and the other one to hold the played 
melody), two Canvas to hold the right sequence and wrong sequence UI and a private float variable to hold the display time for the wrong 
sequence UI.

In the Start(), the correct melody is initialized to a specific sequence, the playedMelody list is also instantiated and both the UI 
gameobjects are set to false.

Following there are three methods that make up the manager’s functionality: AddNoteToMelody method, where the notes are added to the played
melody list and CheckMelody method is invoked, CheckMelody method, that waits for all the notes to be played, checks if the played melody
matches with the correct melody and if so it sets the right sequence UI to active and the wrong sequence UI to not be active and invokes the 
NextScene method- if not then its sets activates the wrong sequence UI and deactivates the other UI and first stops the HideWrongSequence 
coroutine, just to start the same coroutine the next line . Finally, we have HideWrongSequence method, which waits for the time set for the 
wrongSequenceDisplayTime variable, and deactivates the wrongSequence gameObject.

The last method in this class is the NextScene method that uses the scene manager to load the ending scene.
