# BaghLearn Mobile Based Learning Application
## What is BaghLearn? ##
A Mobile game based learning application which develops and upskills programming and algorithmic knowledge with the addition of learning module within a board game that incorporates a key features like:
* Interactive, application has reduced passive activities and has tried to involve learner as much as positivity.
* Simplified gaming Environment that are more relevant and recognizable to the learner.
* Experimentation, where learners' work through failures.

## Getting Started

### Prerequisites

* Requires latest version of Unity that can be downloaded from [here](https://unity3d.com/get-unity/download). This game was developed and tested using Unity 2018.2.16f1.
* Latest version of Visual Studio its use with Unity Engine to provide Integrated Development Environment(IDE) for C# that can be downloaded from [here](https://visualstudio.microsoft.com/downloads/). Visual studio version 2017 was used for this application.
* Java SE Development Kit (version 1.8.0_191 or above) for Java compilation and application build. Jdk Can be downloaded from [here](https://www.oracle.com/java/technologies/javase/javase-jdk8-downloads.html).
* The Android SDK (software development kit) (version 24.4.1 or above).
* Linux or Windows operating system.

### Installing
**Unity on Ubuntu 18.04.**

 1. Open a terminal (Ctrl+Alt+T) and use the following command:
    ```
     sudo apt install ubuntu-unity-desktop
     ```
  2. At the installation time, you’ll be asked whether you want to [switch to LightDM](https://itsfoss.com/switch-gdm-and-lightdm-in-ubuntu-14-04/).

   3. A Display manager is what you see on the login screen.

      * If you want Unity like login screen: select [lightdm](https://wiki.ubuntu.com/LightDM).
      * If you want to keep the default login screen in Ubuntu 18.04: select gdm3.

   4. Once the installation is complete, restart your system. At the login screen, click on the Ubuntu symbol in LightDM or gear symbol in GDM.
   
**Unity on Windows 10.**
 
 1. Go to Unity’s [Download Page](https://unity3d.com/get-unity/download) and click “Download Installer for Windows”. A UnityDownloadAssistant-x.x.exe file should be downloaded to your “Downloads” folder (where x.x is the current Unity version).
 
 2. Open the downloaded installer. Accept the license and terms and click Next.
 
 3. Select the components you would like to be installed with Unity and click “Next”. Note: If you ever want to change the components, you can re-run the installer.
 
 4. You can change where you want Unity installed, or leave the default option and click “Next”.
 5. Depending on the components you selected, you may see additional prompts before installing. Follow the prompts and click “Install”. Installing Unity may take some time. After the installation is finished, Unity will be installed on your computer.
  
### Setup

There are two sets of files under Behaviors folder.
![.github/images/module.png](https://github.com/alokyadavnp/BaghLearn/images/module.png)

#### BoardGame\_Module
The BoardGame\_Module code is located in Assets -> Application -> BoardGame\_Module Folder.

- /Player\_Movement

  - BoardManager.cs class does most of the work.
    The class has a Move, validity and many other functions. 
    - Trymove() validates the drag functionality from initial drag to final drag position.
    - UpdateMousePosition() returns camera hit point in space with x, y and z value within the board.
    - checkforAvailableMove() lists all the tiger gameobject that has restrict to move. checks in every position within the matrix value of 14 * 14 of frame per unit of time.
    - checkWinCondition() triggers gameover canvas for a valid wincondition.
  - Pieces.cs class has defined all the rule set for tiger and goat piece moveement.
  - clickevent.cs is attached to buttons for onhover and onclick sound effect
  - Rail.cs class is attached to rail gameobject present in mainmenu scene. This class gives a rail like effect with the help of 7 objects present across the board when game starts.
  - Mover.cs class attached to main camera in mainmenu for camera to move across those 7 objects from Rail class.
  - tigercount.cs class attached to tiger, counts total number of tiger gameobject present in the game by the name of the MonoBehaviour type.
  - Count.cs class is attached to goat, counts total number of goat gameobject present in the game by the name of the MonoBehaviour type.
  
- /Sounds
  
  - volumecontroller.cs class for setting volume of the mixer with slider that is referenced with audiomixer. Added to slider button in option present at mainmenu scene with a size of 10, means value will decrease to 1/10 with each slide.
  - backgroundmusic.cs class is attached to Background_Music gameobject in mainmenu scene for uninterrupted music transation between the scenes. It also checks if instance(backgroundmusic) is not null and current instance is not the first scene, in that case it removes the background_Music gameobject to avoid duplication.

- /Animation
  
  - destroyblood.cs class attached to bloodparticlesystem and is active when a goat player is killed by the tiger
  - killtrigger.cs class attached to goat gameobject that sets the rule for goat gameobject for triggering death animation along with bool particle system.
  - FlashingText.cs class attached to play button in mainmenu scene, creates flash efect for play button.
  - running.cs class attached to tiger gameobject for its different animations like idle, roar, walk, run etc. Gameobject must have a box collider and rigid body component attached to it for animation to come to effect.
  - runninggoat.cs class attached to goat gameobject for its different animations like idle, run, kill etc. Gameobject must have a box collider and rigid body component attached to it for animation to come to effect
  
- /Others

  - PauseMenu.cs class is attached to PauseCanvas of all the scenes passing global variable available for pause options. 
  - GameOver.cs class is attached to GameOverCanvas in game1scene scene and is called when gameover condition is meet by any player side.
  - MainMenu.cs class is attached to canvas in mainmenu scene for different buttons to work which is also a Homescreen. 
  
#### Learning\_Module
The Learning\_Module code is located in Assets -> Application -> Learning\_Module Folder.

  - ButtonForAnswer.cs class added to AnswerButton gameobject in Game scene. It takes answerdata of type data as argument and pass to ButtonForAnswer to display all the options for answer.
    - It has public reference for answer button text.
  - DataForGame.cs is a data class with no variables. Here data is abstracted from an external file called JSon file and for that serialization of an object is done and later when needed, that data is deserialized and used to populate the field of game data object.
  - DataForQuestion.cs is a data class with no variables. It is used to hold a number of solutions associated with it.
  - DataMonitor.cs class is attached to Persistant scene and will load the data through out the scenes since this scene is responsible to load data needed for execution.
  - GameMonitor.cs class is attached to Game Scene is resonsible for execution of learning module of the application. It has number of functions to look at:
    - DisplayQuestion() reaches out to pool of questions, get the questions and from there to the string of question.
    - CorrectAnswerSelected() for mouse click event on answers, increase the point if correct answer and even check if there are additional question in the round.
  - LevelData.cs is a pure c# class representing the number of question in round.
  - PlayerDevelopment.cs is a data class for storing the highest scores for tiger and goat player at the end of round.
  - SceneController.cs class is attached to MenuScreenController gameobject in MenuScreen scene and will load the topic selection when a start button is pressed in learning module.
  - SimpleObjectPool.cs class is attached to Game scene and used to reuse object instead of instantiating and destroying. This allows to recycle objects and avoid allocations which will lead to garbage collection.
  - DataForAnswer.cs is a data class to hold the correct answer. this data class presists through out the scenes.
  
  ### Running the Game
- Open the application with a supported Unity3D Game Engine.
- Open the "MainMenu" scene from the scenes folder at /Assets/scene.
- Make sure that all of the scenes for the application are there in the build settings:

### Order of Execution
1. MainMenu scene is called when game starts. It has number of buttons to choose from Play, Option, About, Exit and Learning Button.
2. When player presses play button, it is redirected to game1scene where actual boardgame runs and start with an empty board.
3. After a couple of seconds staying in game1scene, Persistant scene loads where data related to learning module is loaded and will persist throughout the other scene as well.
4. Once player presses start button, topic selection canvas is loaded followed by learning in nutshell which is further folowed by respective quiz questions.
With the score count, Player is loaded and loaded back to game1scene again.
5. Process 3. and 4. repeats again in same manner for goat player generation.
6. while in game1scene BoardManager.cs and Pieces.cs class executes. At the end, GameOver canvas in triggered in either of the case where tiger or goat player wins.

## Contributing

Please read [CONTRIBUTING.md](https://github.com/alokyadavnp/BaghLearn/CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.


## Authors

* **Alok Yadav** - *Initial work* - [BaghLearn](https://github.com/alokyadavnp)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
