# Rock Noise
What factors should influence how much noise a rock hitting the ground should make?
- Speed at impact
- Surface collided with

It is important to note that a detector's perception of the noise will also come in to play. The rock's conditions will determine how big the notify radius is, the detector's sensitivity and distance from the noise will determine how they react.

Speed at impact is easy to get. When the collision event fires, I can query the velocity for that frame.  
Getting the surface will actually be harder. I don't know anything about static actors when they collide..

## Algorithm
1. Use rock speed and surface type to determine how large the noise radius is
2. Perform an overlap circle check for sound detectors centered on the rock with a radius defined in step one
3. Use the detector's sensitivity and distance to calculate how loud the sound is to the detector
4. Let the detector react

How to actually calculate the values will be the hard part...