For all problems contact:
Zhiyuan Zhang
zzhang615@gatech.edu

Scenes: 
DemoLevel (main scene)
MainMenu (entry)
LevelSelect
Credits

alphaLevel (still in progress but worth looking at)

How to Start:
Either hit Play on the DemoLevel scene in editor, or start in any other scenes and use the menu buttons to navigate to the DemoLevel.

Controls:
[W] for forward, and braking while car is reversing
[S] for braking while going forward, or reversing
[A] [D] for steering
[Space] for handbrake (rear wheel only) or resuming a tutorial pause menu
[Esc] for pausing menu toggle
It is possible to control the game via a gamepad, however this has not been tested.

General Logic:
Player is expected to accomplish two tasks: 
	1. collect as many Notes as possible
	2. Escape from drunk UGA drivers identified by black pickup trucks
	3. collect Buzz and T for repair and shield

Checklist:
1.) Your game must be implemented in Unity. 
	Yep

2.) Your game must consist of a 3D world.
	Yep

3.) Your game must utilize a character/vehicle controlled by the player...
	Player vehicle is controlled in real time by the player. Actions are actuated by forces on Unity's wheel colliders to simulate a realistic vehicle with tire slip and suspension travel.

4.) Your game must implement a real-time steering, path planning, and statemachine based AI (or similar reactive agent). 
	The "Drunk UGA drivers" are controlled by a real-time steering algorithm called pure pursuit. It calculates derised steering angle to reach player's location in order to crash into it and deal damage. This is one of the major challenges of the game

6.) Your game must be a Game Feel game. 
	With the sore exception of tutorials and user-induced pauses, user can interact with the vehicle in a realtime manner.

7.) Your game must attempt to provide interesting choices for the player to
make during gameplay
	One of our shortcomings currently. We plan to develop alternative routes in other levels.

8.) Your game must include engaging and polished starting/resolving actions,
pause in gameplay, configuration, credits, licenses, etc., via GUI menu
	Menus, pauses, tutorials, and credits are implemented

Additional Comments:
	2/6 of members left shortly before submission
	We planned to add Professors (rugdolls) that walk across the track, if player hits them they get an F immediately, but this was not finished
Shortcomings:
	Handling of vehicle is not very natural
	

Manifesto (who did what, also external assets used)

Nick:--------
Player: (independent)
	Pickup Truck ( Unity Asset Store)
	Wheel Colliders
	Input System 
	CarController.cs ( input -> wheel forces)
	PlayerLogicHandler.cs (handles collectible, buffs, player health, collision etc)
	SmokeHandler.cs (car emits white smoke when healthy, black smoke when low health)
AiOpponents:(independent)
	PurePursuitAi.cs (calculate steering with pure pursuit algorithm)
	AiCarController.cs (actuate control commands to wheel colliders, determine speed, handles explosion on collision)
Tutorials: (independent)
	Logic and

BackgroundMusic:(independent)
	AudioPlayer.cs

GameLogic: (independent)
	CameraFollow.cs (adapted from class project)
	GameLogicHandler.cs (handles tutorial and other game logic)
UI:
	PauseMenu
	Game End Ui
	HUD
WorldMap: (collaboration with Liam)
	Arrangement of elemnts, invisible colliders for boundary

Misc:
HealthHandler.cs (unused)
HudHandler.cs (head up display)
AudioPlayer.cs
SpawnAi.cs

Sophia:-------
Collectibles (independent)
	Meshes/Prefabs for collectibles (external assets)
	BobbingPickup.cs
	RotatingPickup.cs
UI: 
	Main menu, credit scene, level selector, background picture
	CreditScenes.cs
	MainMenu.cs
	LevelSelector.cs
	LevelSelectReturn.cs
Liam: -------
All maps and level, assets. (roads are created with unity objects, other assets are external)

Dhyey: ----
Initially assigned to do AI (uga drivers) and rugdolls. Nick took over AI. Rugdolls are not finished.
