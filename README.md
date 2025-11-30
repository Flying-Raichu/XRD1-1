![PIA06890-56a8ccd83df78cf772a0c5df](https://github.com/user-attachments/assets/74eed05a-b434-43b5-bf93-775678d0921c)

# Solar system AR project
This is the repository for the first group project in XRD1 - A mini, educational solar system animation.

# Core premise
The general idea behind the project as an educational piece is to provide a robust learning addition to would-be beginners in the field of astronomy. The application would allow for exploration of a scale version of our Solar System, providing information on each celestial body situated within it. How this takes advantage of AR is that it provides a clearer (if not 100% scientifically-accurate) model of the distances involved, how the travel of light and photons affects brightness of celestial bodies, as well as general orbital dynamics.

# Features
1. A scale model - The planets are all scaled for educational purposes. One can see the relative size difference between the rocky planets and the further gas giants, while still remaining visible enough to distinguish;
2. Orbital periods - The planets orbit around the Sun, as well as rotate around their axes with realistic axial tilts corresponding to their real world counterparts. This allows for a visual explanation of the planets' tilts;
3. Realistic lighting - Thanks to Unity's lighting system, the relative strength of the Sun's lighting could be scaled to showcase a realistic intensity of sunlight striking different planets - from the bright, unforgiving surface of Mercury to the dimly-lit Neptune
4. Information - Each planet comes in with a small description and some fun facts regarding the planet itself.

# Known issues
- Uranus' rings do not behave realistically, its tilted axis isn't modelled correctly
- The AR plane detection sometimes detects multiple planes
- The Sun itself isn't modelled as a planet. Therefore, it is stationary, does not rotate around its axis and does not have a description

# Links to blog posts
[Introduction](<Blog posts/01 Introduction.md>)

[Milestone 1](<Blog posts/Milestone 1.md>)

[Milestone 2](<Blog posts/Milestone 2.md>)

[Milestone 3](<Blog posts/Milestone 3.md>)

[Milestone 4](<Blog posts/Milestone 4.md>)

[Video showcase](<Blog posts/Video showcase/Solar System app recording.mp4>)

Link to Youtube video: (<https://youtube.com/shorts/5Lq7TW6wbOY?feature=share>)

## References
[1] Unity AR Foundation documentation  
https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@3.0/manual/index.html

[2] Unity tutorial – Placing an object on a plane  
https://learn.unity.com/tutorial/placing-an-object-on-a-plane-in-ar

[3] AR Foundation beginner guide  
https://technerdus.com/unity-ar-foundation-tutorial-for-beginners/

[4] Unity Technologies. “AR Foundation Manual.” *Unity Documentation*.  
https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@5.0/manual/index.html  

[5] Unity Technologies. “ARRaycastManager.” *Unity Scripting API*.  
https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@5.0/api/UnityEngine.XR.ARFoundation.ARRaycastManager.html  

[6] Unity Technologies. “Transform.RotateAround.” *Unity Scripting API*.  
https://docs.unity3d.com/ScriptReference/Transform.RotateAround.html  

[7] Unity Technologies. “LineRenderer Component.” *Unity Manual*.  
https://docs.unity3d.com/Manual/class-LineRenderer.html  

[8] Boards to bits games. "Making a Custom Planet Ring in Unity".
https://www.youtube.com/watch?v=Rze4GEFrYYs&t=1s

[9] Unity Technologies. “World-Space UI.” *Unity Manual*.  
https://docs.unity3d.com/Manual/HOWTO-UIWorldSpace.html  

[10] Unity Technologies. “Camera.WorldToScreenPoint.” *Unity Scripting API*.  
https://docs.unity3d.com/ScriptReference/Camera.WorldToScreenPoint.html  

[11] Unity Technologies. “Material Emission Properties.” *Unity Manual*.  
https://docs.unity3d.com/Manual/StandardShaderMaterialParameterEmission.html  

[12] Unity Technologies. “Event System & Input in Unity UI.” *Unity Manual*.  
https://docs.unity3d.com/Manual/EventSystem.html  

[13] Unity Technologies. “TextMeshPro Essentials.” *Unity Documentation*.  
https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest  

[14] Unity Technologies. “EventSystem and UI Input.” *Unity Manual*.  
https://docs.unity3d.com/Manual/EventSystem.html  

[15] Unity Technologies. “IPointerClickHandler Interface.” *Unity Scripting API*.  
https://docs.unity3d.com/ScriptReference/EventSystems.IPointerClickHandler.html  

[16] Unity Technologies. “Canvas and UI Rendering.” *Unity Manual*.  
https://docs.unity3d.com/Manual/class-Canvas.html  
