# dicomdatasetslicer

Main scene is in "Assets/Slicer" folder, called "Slicer.scene".

First, use the option "DicomVolume > Create Volume Texture". This will open a window where you can drag your dicom files and choose a filename for the resulting volume texture. Click the create button to create the texture (if no dicom images are added or the filename is empty/whitespace, no texture will be created). The file will be created in the Assets folder.

Then, apply the generated file to the 3DSampler material found in the "Assets/Slicer/Material" folder, as a texture.

The project is now ready to run. Press play and then move and rotate the GameObject "Volume > Slice" (in the scene). This GameObject has a "Dataset Viewer" component which allows you to flip the dataset along each Axis individually. You can also move the "Volume > Min" and "Volume > Max" GameObjects to change the dataset's volume.