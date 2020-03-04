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

#### BoardGame\_Module
The BoardGame\_Module code is located in Assets -> Application -> BoardGame\_Module Folder.

- /Player\_Movement

  - BoardManager.cs class does most of the work.
    The class has a Move, validity and many other functions 
    - Trymove() validates the drag functionality from initial drag to final drag position.
    - UpdateMousePosition() returns camera hit point in space with x, y and z value within the board.
    - checkforAvailableMove() lists all the tiger gameobject that has restrict to move. checks in every position within the matrix value of 14 * 14 of frame per unit of time.
    - checkWinCondition() triggers gameover canvas for a valid wincondition.
  - Pieces.cs class has defined all the rule set for tiger and goat piece moveement.
  - clickevent.cs is attached to buttons for onhover and onclick sound effect
  - Rail.cs class is attached to rail gameobject present in mainmenu scene. This class gives a rail like effect with the help of 7 objects present across the board when game starts.
  - Mover.cs class attached to main camera in mainmenu for camera to move across those 7 objects from Rail class.
  - tigercount.cs class attached to tiger, counts total number of tiger gameobject present in the game by the name of the MonoBehaviour type.
  - Count.cs attached to goat, counts total number of goat gameobject present in the game by the name of the MonoBehaviour type.
  
- /Sounds
  
  - volumecontroller.cs
  - backgroundmusic.cs

- /Animation
  
  - destroyblood.cs
  - killtrigger.cs
  - FlashingText.cs
  - running.cs
  - runninggoat.cs
  
- /Others

  - PauseMenu.cs
  - GameOver.cs
  - MainMenu.cs
  
#### Learning\_Module
The Learning\_Module code is located in Assets -> Application -> Learning\_Module Folder.

  - ButtonForAnswer.cs
  - DataForGame.cs
  - DataForQuestion.cs
  - DataMonitor.cs
  - GameMonitor.cs
  - LevelData.cs
  - PlayerDevelopment.cs
  - SceneController.cs
  - SimpleObjectPool.cs
  - DataForAnswer.cs
  
  ### Running the Game
- Open the application with a supported Unity3D Game Engine.
- Open the "MainMenu" scene from the scenes folder at /Assets/scene.
- Make sure that all of the scenes for the application are there in the build settings:

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.
