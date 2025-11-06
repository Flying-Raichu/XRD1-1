# Lyubo - Orbit System Improvements

Today, Lyubo focused on refining and expanding the code that handles the spawning and orbital movement of the planets around the Sun in our AR solar system scene.

Previously, the project featured a single orbiting planet (Mercury), but it had several technical issues - Mercury would “fly off” into the air instead of orbiting the Sun properly. The goal of this session was to identify what caused that behavior, fix it, and scale the system so that all seven planets can orbit the Sun smoothly once placed in the AR environment.

## Process
1. Debugging the Orbit Problem

The initial version of the MercuryOrbit script attempted to make Mercury orbit the Sun using Unity’s RotateAround() method. However, because the Sun object wasn’t present in the scene until the user tapped a plane, the orbitCenter reference pointed to the prefab asset in the project rather than the spawned instance in the AR scene. This caused Mercury’s orbit calculations to use an invalid position, resulting in the “flinging” behavior.

Additionally, Mercury still had a non-kinematic Rigidbody, which conflicted with transform-based rotation. The physics engine and the manual transform manipulation fought each other, further exaggerating the unwanted movement.

To fix this, the orbit center is now dynamically assigned to the instantiated Sun object at runtime. The script also automatically sets Mercury’s Rigidbody to isKinematic, preventing physics interference.

2. Updating TapToPlaceSingle

The TapToPlaceSingle script was rewritten to preserve its original logic - tapping once spawns the solar system, and subsequent taps move it. The improved version ensures that:

The Sun and Mercury are spawned together only after a valid AR plane tap.

Both objects share a common anchor parent, so AR plane updates move them consistently.

Mercury spawns at a set offset distance from the Sun instead of overlapping it.

The orbit center is automatically connected in code using SetOrbitCenter().

A SetDistance() helper method was retained to adjust the spacing between the Sun and Mercury, keeping the naming and structure similar to the original for continuity.

3. Expanding to All Planets

Once the basic orbit system was stable, the code was scaled up to support all seven planets.
A new generalized orbit script, PlanetOrbit, replaced MercuryOrbit, using the same OrbitingAroundSun() structure but allowing each planet to have:

Its own orbit radius and speed,

Optional tilt and self-rotation, and

Individual starting phase and orbit axis.

On the placement side, the new TapToPlaceSystem script uses an inspector-driven list, making it easy to assign prefabs and parameters for each planet. When the user taps a surface, the Sun and all planets are spawned and wired automatically.

## Results

By the end of the session:

Mercury now correctly orbits the Sun without physics glitches.

The TapToPlaceSingle system spawns both Sun and Mercury dynamically and keeps them aligned to the AR plane.

The system was successfully generalized for all planets, supporting adjustable speeds and orbit distances.

This approach not only fixed the instability but also made the project more modular and scalable - allowing future additions like moons or rings without rewriting core logic.
