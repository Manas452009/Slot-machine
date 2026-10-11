# 🎰 Unity Slot Machine Game

A 2D slot machine game developed using **Unity and C#**. The game recreates the basic slot machine experience with animated reels, a pull handle, betting mechanics, and symbol-based win detection.

## 🎮 Game Overview

The player places a bet and spins three slot reels. Each reel displays three symbols: top, middle, and bottom. After the reels stop, the game checks the **middle row** to determine whether the player has won.

The game currently uses four symbols:

* **Seven**
* **Cherry**
* **Bell**
* **Bar**

The project focuses on implementing slot machine mechanics, UI interactions, reel animation, and game logic.

## ✨ Features

* **Three-Reel Slot Machine:** Three reels, each displaying three symbol positions.
* **Four Slot Symbols:** Seven, Cherry, Bell, and Bar.
* **Animated Reels:** Reels cycle through symbols and stop at selected results.
* **Spin Handle Animation:** The handle moves down and returns to its original position.
* **Middle-Row Win Detection:** Winning combinations are evaluated using the middle symbol of each reel.
* **Betting System:** Players can select or adjust their bet.
* **Balance Management:** The player's balance is updated according to the game's betting and payout rules.
* **Win/Loss Feedback:** The UI displays the result of each spin.

## 🕹️ How to Play

1. Launch the game.
2. Set your bet using the available controls.
3. Pull the handle to start the reels.
4. Wait for all three reels to stop.
5. Check the middle row for a winning combination.
6. Your balance and the result message will update according to the outcome.

## 🌐 Running the WebGL Build

### Play in a Browser

The WebGL build is available in the `Build/WebGL/` folder.

Unity WebGL builds generally need to be served through a local or hosted web server rather than opened directly using `index.html`.

To run the game locally:

1. Download or clone this repository.

2. Open a terminal in the repository's root directory.

3. Start a local HTTP server using Python:

   `python -m http.server 8000`
   

4. Open the following address in your browser:

   `http://localhost:8000/Build/WebGL/`

**Note:** The WebGL build must be generated in Unity before these instructions will work. If the build files are stored elsewhere, update the path above accordingly.

### Play Online

You can host the WebGL build using GitHub Pages or another static hosting service configured to serve Unity WebGL files correctly.

## 🛠️ Technologies Used

* **Game Engine:** Unity
* **Programming Language:** C#
* **Platform:** WebGL
* **UI System:** Unity UI
* **Version Control:** Git and GitHub

## 📁 Project Structure

The project is organized to keep scripts, UI elements, animations, and build files separate.

UnitySlotMachine/
├── Assets/
│   ├── Scripts/
│   │   ├── GameManager.cs
│   │   ├── Reel.cs
│   │   ├── RNGManager.cs
│   │   ├── UIManager.cs
│   │   ├── SlotHandle.cs
│   │   └── SymbolData.cs
│   ├── Prefabs/
│   ├── Animations/
│   └── ...
├── Build/
│   └── WebGL/
├── Data/
├── Packages/
├── ProjectSettings/
├── README.md
└── .gitignore

## 🧠 Thought Process and Approach

The project was developed incrementally, starting with the core slot machine functionality and then improving the user experience.

### 1. Slot Machine Logic

The game uses three reels, with three visible symbol positions per reel. A fixed symbol sequence is used to create an ordered reel appearance rather than displaying completely unrelated symbols.

### 2. Random Outcome Selection

The random number generator selects the target symbol for each reel before the spin finishes. This separates outcome selection from the reel's visual animation.

### 3. Reel Animation

The reel script manages symbol display and spin timing. The animation is designed to create a scrolling effect and give each reel a clear stopping point.

### 4. Win Detection

The game evaluates only the three middle symbols. This keeps the winning logic separate from the top and bottom display positions.

### 5. User Interface and Feedback

The UI provides controls for spinning, adjusting the bet, displaying the balance, and communicating whether the player has won or lost. The animated handle adds a more interactive slot machine experience.

### 6. Iterative Development and Debugging

The game was built and tested in stages. Issues involving symbol selection, reel state, image alignment, and win detection were addressed as individual features were implemented.

## 🚀 Future Improvements

Potential improvements include:

* Sound effects for spinning, stopping, winning, and losing.
* More polished reel transitions and stopping effects.
* Additional winning combinations and configurable payouts.
* Improved visual effects for jackpots.
* Responsive layouts for different browser sizes.
* Online hosting for direct browser access.

## 📦 Submission Contents

* Unity project source files.
* C# scripts and UI assets.
* WebGL build in `Build/WebGL/`.
* README documentation.
* Git history showing incremental development.

## 👨‍💻 Author

Manas Mishra

Manas452009
