# dicomdatasetslicer

Main scene is in "Assets/Slicer" folder, called "Slicer.scene".

First, use the option "DicomVolume > Create". This will create a file called 3dDicom.asset in the Assets folder.

Then, apply this file to the 3DSampler material found in the "Assets/Slicer/Material" folder.

The project is now ready to run. Press play and then move and rotate the GameObject "Volume > Slice" (in the scene). This GameObject has a "Dataset Viewer" component which allows you to flip the dataset along each Axis individually. You can also move the "Volume > Min" and "Volume > Max" GameObjects to change the dataset's volume.

The project is currently stuck to being Axis-Aligned.