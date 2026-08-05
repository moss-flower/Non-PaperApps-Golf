# Non-Paper Apps Golf

Non-Paper Apps Golf is a 2d, tile based, dice powered golf game based on the notepad game Paper Apps Golf.

## Installation
If you just want to give the game a try, download for your Operating System via the Releases tab on the side (coming soon). 

If you want to build it yourself (not recommended) via the unity engine, please follow the following steps:

1. Clone the repository or download the zip as per usual.
2. Launch in Safe Mode. The game makes use of an external library called PrimeTween. Due to how the library is installed, you'll have to delete the entire Library Packages folder and the package-lock.json from the Packages directory. This is becasue it will try to locate the package from via a hardcoded path, and the package will not exist yet. You have to edit the manifest.json, find PrimeTween, and set it to 4.11.0 rather than whatever hardcoded string sits there. 
3. Once the above is fixed, you'll need to download the UGUI samples from the Shader Graph unity package so that the shaders can build properly.
4. Close the project and reopen it for the necessary changes to take effect.

## System Design and Future Builds
For an outline of how the systems in the project were structured, and what I would do if I had more time to expand the game, see the following link: (Insert link here).


## Credits

This is an implementation of the Paper Apps — Golf notebook game. I built this project as a way of re-learning the Unity Engine over the summer of 2026. 

In the process of developing this application I used the following references and resources. If you suspect I've used your work and didn't leave credit, let me know. I did my best to keep track of the different videos, articles, books or forum posts that I used for reference, but some will have inevitably slipped through the cracks.

(Prime tween credit here).


