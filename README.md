# Tank Game

In order to get an idea of your programming skills, we will ask you to develop a little tank game using Unity3D.
Below, you will find some general instructions about the tools to use as well as to what we will pay attention to.

As mentioned in the email you have received, the result of the test will be based on the number and the quality of each feature you have implemented, and on the quality of design and implementation.

So don't worry if the game isn't fully completed.

## Requirements

The game manages five tanks :

* one is driven by the player using the keyboard :
  * cursor up/down : move tank forward/backward at 50 meters per second along 		the Z axis
  * cursor left/right : turn tank left/right at 30 degrees per second around the Y axis
  * space: shoot a shell
* the others are driven by the computer :
  * they constantly move forward at 20 meters per second
  * they turn randomly, choosing every second a new turning speed from -50 to +50 degrees per second
  * they automatically shoot a shell when the player is in front of them

The tanks are dropped randomly inside the fighting zone, which is one kilometer long by one kilometer wide, with a vertex every 50 meters at a random height from –5 to +5 meters. When a tank exits the fighting zone, it is immediately dropped again randomly inside the fighting zone.

The tank height and orientation follows the ground relief. The tank height is computed using the ground height at the X/Z coordinates of the tank center, and the tank orientation follows the ground normal at this point.

All tanks can shoot at 5 shells per second with a maximum of 5 simultaneous shells per tank.

The shells are shot at 30 meters per second and a fixed angle of 15 degrees, using the following ballistic curve where "(x0,y0,z0)" is the initial position, "(vx0,vy0,vz0)" the initial velocity vector, "g" the gravity acceleration (9.81), "5" the time scaling factor and "t" the time elapsed since the shot :

* x(t) 	= x0 + 5 \* vx0 \* t
* y(t) 	= y0 + 5 \* vy0 \* t – ( 5 \* t ) \* ( 5 \* t ) \* g / 2
* z(t) 	= z0 + 5 \* vz0 \* t

The shell immediately disappears when it touches the ground.

If it hits a tank body before touching the ground, a hit sound is launched. Once a tank has been hit three times, an explosion sound is launched and the tank disappears. If it's the player's tank, the game plays a game over sound and then restarts. But if the player has finished destroying the four computer tanks, the game plays a victory sound and then restarts.

The game is rendered using a third-person camera, which always stays 80 meters behind the player's tank and 16 meters higher, so that he sees well where his tank will shoot.

The game uses the following sounds :

* "engine" sound, played continuously
* "fire" sound, played asynchronously when a shell is shot (3d sound at shooting tank position)
* "hit" sound, played asynchronously when a tank is hit (3d sound at hit tank position)
* "destroyed" sound, played asynchronously when a tank is destroyed (3d sound at destroyed tank position)
* "victory" sound, played synchronously when the player shoots the last tank
* "game_over" sound, played synchronously when the player tank is destroyed

## Instructions

### Tools

If possible, please use the latest Long Term Support / LTS **Unity3d** version or a previous one.
At the time of writing, it is 2017.4.10f1.

Please use **git** during your development and try to keep separate features in separate commits (or branches).
This will allow us to better understand how the project was developed and prioritized.
Do not worry if you find a bug and need to commit a patch. This is normal programmer's life ;)

### Plugins / Open-source code

You are allowed to use free plugins if you want to but we will only consider the code you wrote yourself for the evaluation.
If you copy/paste some code from elsewhere, please identify it clearly (e.g. with a comment pointing at the source URL).

### What we'll pay attention to

* the number and quality / conformance of implemented features
* respect good coding practices but do not complexify unnecessarily, for example
  * aim for clearly defined code responsibilities
  * avoid code duplication (https://en.wikipedia.org/wiki/Don%27t_repeat_yourself)
  * cache 'heavy' computations / queries
  * use prefabs when they make sense
  * avoid hard coded values in code (prefer tweakable parameters)
  * ...
* use a consistent coding standard

**GOOD LUCK**