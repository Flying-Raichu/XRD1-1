On the computer – Catarina
Writing the post – Lyubomir

For our second lab session, our main goal was to fix the object placement system that we built during Week 1 so that it would actually respond to touch input on Android devices. In the previous session, we managed to detect planes and place a sphere (representing the Sun) in AR using Unity’s AR Foundation tutorial code, but our script relied on the old Unity input system, which wasn’t compatible with our current Unity project setup.

After confirming that our plane detection worked but the sphere never spawned when tapping, we investigated and found that our Unity project was configured to use the New Input System (the default in newer Unity and AR Foundation versions). Because of that, functions like Input.GetTouch() were no longer being called, which meant the app never recognized our taps.

What we changed

We replaced the tutorial’s outdated script with a new one that supports Unity’s New Input System while keeping the same AR Foundation structure.
Here’s what we introduced:

Enhanced Touch Support: enabled through EnhancedTouchSupport.Enable() so Unity can track touch events properly under the new input system.

Touchscreen API: instead of Input.GetTouch(0), we used:

var ts = Touchscreen.current;
var t = ts.primaryTouch;
if (t.press.wasPressedThisFrame)

This detects taps reliably on Android AR builds.

Simplified placement logic: the script now checks for a single touch, raycasts to the detected plane, and spawns or moves one object (the Sun prefab) at that position.

Cleaner architecture: we removed the old “reference point list” and “RemoveAllAnchors()” logic from the Week 1 code. The new version instead uses a single anchor or directly positions the object, which is more in line with current AR Foundation best practices.

Testing and debugging

To make sure touch detection worked, we built the project again and used Android Logcat inside Unity to view debug messages (Debug.Log). After installing the Android Logcat package through the Package Manager, we could see our own custom log entries like:

[TapToSet] Touch @ (x, y)
[TapToSet] Plane hit id=...
[TapToSet] Spawn WITH anchor.

Results

By the end of the session, we were able to:

Detect planes on the floor or a table through the phone’s camera

Tap on a plane and have our Sun object appear exactly where we touched

Tap again to move the Sun to a new position (instead of creating duplicates)

The placement now works smoothly on Android devices, and our AR scene correctly tracks the object in space. This marks the completion of the first working version of our AR solar system foundation, setting us up for next week when we’ll start adding orbiting planets around the Sun.

References

Unity Tutorial – Placing an Object on a Plane in AR
https://learn.unity.com/tutorial/placing-an-object-on-a-plane-in-ar

Unity Documentation – AR Foundation
https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@6.0/manual/index.html

Unity Manual – New Input System
https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/index.html
