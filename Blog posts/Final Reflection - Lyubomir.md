# Individual Reflection — Lyubomir

### **Main Contributions**



\- Planet interaction and selection system



\- Planet selection



\- Visual highlighting using material emission and label toggling



\- World-space labels with billboarding for AR readability



\- Planet information system and UI



\- Integrated AR interaction with Unity’s EventSystem to avoid conflicts between placement and UI input



\- Primary responsibility for user interaction, feedback, and educational UI design of the AR project


\- Minor bugfixes on the parallel VR project (secondary contribution)

### **Reflection on the Project and Underlying Theory**

Working on this AR solar system project reinforced how XR theory directly influences practical design decisions. Unlike desktop applications, AR does not provide a stable, absolute coordinate system. All content exists relative to tracked planes and anchors that are continuously updated through sensor fusion (camera and IMU). This has major implications for interaction, UI, and feedback design, as any assumption of precision or fixed positioning quickly fails in AR.



From an XR theory perspective, this project strongly emphasized 6DoF tracking and spatial interaction. Users physically move around the system, meaning interaction design must account for changing viewpoints, occlusion, distance, and tracking noise. Several interaction techniques that worked well in GMD failed here due to the requirement for a project that is capable of supporting a 360 degree perspective shift.



### **Technical Decisions and Their Rationale**

**Screen-Space Selection Instead of Physics Ray casting**



Instead of using physics ray casting for planet selection, I implemented a screen-space distance-based selection system. Ray casting against small, moving planets proved unreliable on mobile devices, especially for distant objects. By converting world positions to screen space and selecting the closest planet within a radius, interaction better matched how users naturally tap screens. This prioritised usability over technical purity, a key XR design principle.



This decision also demonstrates an understanding of coordinate transformations - touch input exists in screen space, while AR content exists in world space, and effective interaction requires bridging that gap.



**Visual Feedback and Spatial UI**



Clear visual feedback is essential in AR due to the complexity of the real-world background. Highlighting planets using material emission ensured visibility under varying lighting conditions and provided immediate selection feedback. World-space labels anchored information directly to objects, reflecting XR principles of spatial UI rather than detached screen-based interfaces.



To maintain readability from any viewpoint, labels were billboarded, always facing towards the camera. This directly supports user comfort and accessibility, reinforcing that technically correct solutions are insufficient if they are uncomfortable or confusing to use.



**Integrating AR Interaction with UI**



Managing input conflicts between AR placement, object selection, and UI interaction was a key challenge. The same touch gesture can place content, select an object, or interact with UI. By checking whether touches were consumed by Unity’s EventSystem, I ensured UI interaction did not accidentally reposition the solar system. This highlighted the importance of clearly separated interaction layers in XR applications.



### **Technical Issues**

**The Sun is not an interactable object**



The interaction system was planet-centric - selection iterated over planets only, and labels depended on planet components. Since the Sun is a separate anchored object, it could not be selected.



Future improvement - introduce a shared Selectable component used by both planets and the Sun.



**Uranus' rings rotate with the planet, resulting in unrealistic behavior**



The ring inherits Uranus’ rotation, causing it to visually track the Sun rather than behave as a stable ring plane.



Future improvement - separate Uranus into orbit, body rotation, and ring transforms, locking the ring’s orientation to axial tilt.



**Venus’ retrograde rotation is missing.**



All planets share the same rotation logic, so Venus’ retrograde spin is not represented.

Future improvement: separate orbital motion from axial rotation and allow negative spin values.



### **Lessons Learned**

I feel strong ownership over the interaction and UI systems in the AR solar system project. Many issues only became apparent through on-device testing, reinforcing that XR development cannot be done purely in the editor. The most important takeaway is that XR development is fundamentally user-centred: usability, clarity, and comfort often matter more than technical correctness. This project helped solidify my understanding of AR as a distinct medium with its own design constraints and opportunities.

