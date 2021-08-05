The prototype should run either in the unity editor, or as a build for PC.

Controls:
A and D to move, Space to jump.  Space while in the air to double jump.



Design Overview:
Camera - Follow cam on Y axis to help increase feeling of speed; camera zooms out as speed increases to improve seeing where you're going at high speeds.

Death - Freeze frame helps players identify what killed them.  The inverted colors, enemies slowly drifting apart, and quick jagged return movement make it feel "wrong" to die, while still being cool.

Tutorials - Freeze frame on tutorials and the unusual button to close it forces players to read the quick tutorials.  The contextual locations of them helps make sure it's always relevant information.

Enemy Bounce - As the enemies explode; it felt right that the player felt some of that force; hence the bounce.

Backtracking - The level isn't huge, but it isn't tiny either.  Some of the enemies are positioned to take advantage of their one-directional deaths.  This opens up backtracking paths for the player if they die later.  This is instead of respawn points which always lag behind where the player wants them to be in a more open level like this.

Wall Jump/Climb - Helps with vertical movement.  Once I realized I wanted the enemies to be able to bounce you; I wanted to make sure there was always a way to get above them.  Wall climb was the solution I used.

Coin 1 - Tutorial coin; easy to get to, makes the player use wall jumps.

Coin 2 (top right) - Teaches the player that enemies don't respawn (and is a big hint to look for shortcut paths like the one right above the door)

Coin 3 (bot left) - Simple jumping challange; potential introduction to spikes.

Coin 4 (top left) - Traversal and timing.  Requires use of all skills learned in the rest of the level.

Several of the enemies are also in very specific places (either to block/grant passage, encourage multi-bounces, or guide the player in that direction).

Elements that would be next up:

SFX and Audio

VFX on contact with walls/floors





Technical Overview:

Some shortcuts needed to be taken in the name of time; but the below is a rough description of the scripts used for the project.


CameraController:

  -This is fairy self-explanatory; it controls the camera.  It reads from elsewhere, but nothing external has access to it.


StationaryCollider

  -CoinController
  
  -EndDoorController
  
  -SpikesController
  
  -TutorialController
  
  -All 4 of these share signifigant common elements, and use the framework provided by StationaryCollider to reduce code duplication.  
  
  -Tutorial controller is also the class used to run all of the popup tutorials in the game.
  
  
  
FireworksController

  -Used to control all aspects of starting the Fireworks from the victory moment.
  

Firework

  -A class build to handle the firework animation for a single firework.
  
  
HUDController

  -Used to set all simple data on the HUD (things that don't need thier own setup, such as single text boxes).
  
  -In this case, it's fairly small, but it's good for extensibility.
  
  
InversionQuad

  -Simple class to trigger inverting the colors on the screen.  In this case only used when the screen is frozen, but could have other uses in a larger project.
  
  
EnemyController

  -2 major functions:
  
    1) Handle enemy AI and functionality.
    
      -There are 4 types of enemies (Stationary, Rotate, Move, MoveRotate), and the logic for controlling that is in here.
      
      -In a larger project, this could be split into subclasses, but at this size that would be overengineered.
      
    2) Deal with the enemy explosion visuals.
    
      -This can (and would) be moved to a different script as it expanded in complexity.  For now it works; but it creates double duties for EnemyController.
      
  
PlayerController

  -A singleton with far too much power.
  
  -Is responsible for player movement, storing player data (like coin count and respawn location), and also for animations on things like death.
  
  -Making this powerful singleton was the biggest technical shortcut taken for time.
  
  
InversionShader

  -Used on the quad to invert the colors during a freeze frame.
  
