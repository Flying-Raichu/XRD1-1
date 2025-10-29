# Lyubo – Texture Work
Today, Lyubo focused on creating and refining textures for the planets and the Sun in our solar system scene.

Because our project aims for a stylized but readable look — and we’re working with limited time — we decided to use pixelated textures instead of high-resolution photorealistic ones. This approach not only fits the visual style we’re going for, but also makes the creation process much faster and easier to iterate on.

## Process
Each planet was given a 64×64 texture map, created with simple color patterns and procedural noise to suggest surface detail (like craters on Mercury, clouds on Venus, and gas bands on Jupiter). The smaller texture size keeps the assets lightweight and gives them a clean, consistent aesthetic.

After generating each texture, we imported them into Unity and applied them to basic sphere meshes. We set the Filter Mode to Point (no filter) so the pixels remain sharp rather than blurred, keeping the deliberate pixel-art appearance. The Wrap Mode was adjusted as needed — either Repeat for seamless textures (like Uranus or Venus), or Clamp for planets like Neptune where repeating would create unwanted seams.

We also created normal maps for each texture to give the planets subtle lighting detail. This adds a small amount of depth to the surfaces without increasing model complexity — for example, craters or cloud bands catch light realistically when the scene’s light source moves.

## Results
By the end of the session, all major celestial bodies have functional textures with consistent styling and proper material setup in Unity. The combination of pixel textures and normal maps gives each planet its own character while maintaining a cohesive low-resolution look across the solar system.

Next steps will involve refining lighting and testing the textures under different environmental settings to ensure they read clearly in the final scene. 

# Catarina - Coding work
To work on making planets orbit the sun, the branch “Orbits” was created. The goal was to understand how to make a GameObject orbit another one, in this case the sun. Therefore the first planet was created- Mercury- with a temporary material and a script was attached to it. It is necessary for the planets to have a script attached to it to define specifics on what GameObject is to the central point, how do they orbit the central point and how fast do they orbit it. In the future we will attempt to optimize the code and make an orbit scripts, as to decrease the amount of scripts that need to be created. That script was named MercuryOrbit and it declares 3 variables: two GameObjects as serialized fields and a float variable to define speed. There are two methods:
OrbitingAroundTheSun(), that takes a GameObject and where we define the orbit using the RotateAround method from the transform class; and the Update() method, where we call OrbitingAroundTheSun upon mercury.

However, changes needed to be made to the TapToPlaceSingle class, as it was needed to define when and how do planets spawn in the scene. To achieve this, a GameObject mercury and a GameObject mercuryPrefab were added to the script, just like the central point (sun). Then in order to only spawn mercury if the sun was spawn in the first place, an if statement was created and placed right after the central point object was defined as the parent for anchor transform and spawn. Similar to the sun, the if statements makes it so that the object spawns at a different position than the sun (which was set previously as adjectedPosition2) and that it spawns at a set distance from the sun. A debug log was created with the message “Log” so that if there are any issues, we can be sure that they did not come specifically from the newly added piece of code.

Lastly, in the Inspector, the GameObject prefabs were attached to their respective variables on both MercuryOrbit and TapToPlaceSingle scripts.
