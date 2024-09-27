<h1>Spline Editor</h1>
<p>by <b>Antoine Mordant</b></p>

<h2> Introduction </h2>

<p>This project is made with <i>Unity 2022.3.10f1</i> in C# in ISART DIGITAL.

The goal was to create a spline editor tool and an environment to test it.</p>

<img src = "README_Images/Scenes.png">

<h2>Editor</h2>

The spline editor is in the "Spline Editor" Scene.

The tool is managed in editor mode. You don't have to launch the scene to make it work.

<h3>Spline Editor</h3>

<img src = "README_Images/SplineEditor.png">

- <b>Variables</b>

The File Name is used in the saving and loading of the spline :
- For the save, it is the name of the file in which it will be saved
- For the load, it is the name of the file that will be loaded

You don't have to add the .txt at the end, it is done automatically.

The Curve Type is the type of curve (Hermitian, Bezier etc) of the current curve, defined by the last 4 points added

The Curve Precision is the number of point by curve (4 points) the line renderer draws.

- <b>Buttons</b>

Add Curve adds 4 new points that define a new curve.

Refresh Spline redraw the spline if there are some problems with it.

Save and Load spline are explicit.

Clear Spline reset completely the spline.

    There are some problems with the clear. Some points don't delete themself. Be careful with that : the points that are used are the one in the List Spline Points.

<h3>Spline Point</h3>

<img src = "README_Images/SplinePoint.png">

Moving it automatically redraw the spline.

<h2>Testing</h2>

You can test your splines in the SplineTest scene.
This scene doesn't work in editor mode, you have to start the scene to test it.

<h3>Spline Follower</h3>

<img src = "README_Images/SplineFollower.png">

This is a object that will follow a given spline.

The File Name is the file of the Spline that will be loaded.
You can change it in runtime and press R to load a new one.

<h3>Spline Renderer</h3>

<img src = "README_Images/SplineRenderer.png">

This will render the spline of the linked Follower.

The Curve Precision is the amount of point by curve drawn by the line renderer.

<h3>Camera</h3>

To move it, you have to keep the left mouse button pressed and then, with ZQSD or WASD, you can move around.