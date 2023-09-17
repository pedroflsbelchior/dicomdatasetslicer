using Dicom.Imaging;
using Dicom.Imaging.Render;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LoadDicom : MonoBehaviour
{
    [MenuItem("DicomVolume/Create")]
    static void CreateTexture3D()
    {

        int from = 61;
        int to = 520;
        int num = to - from + 1;

        var tmp = new DicomImage("Assets/Resources/1-061.dcm");
        int width = tmp.Width;
        int height = tmp.Height;
        int offset = width * height;
        int depth = num;

        int maxVal = -10000;
        int minVal = 10000;

        float[] values = new float[width * height * depth];

        TextureFormat format = TextureFormat.RFloat;
        TextureWrapMode wrapMode = TextureWrapMode.Clamp;
        
        Texture3D texture = new Texture3D(width, height, num, format, false);
        texture.wrapMode = wrapMode;
        texture.anisoLevel = 1;
        texture.filterMode = FilterMode.Trilinear;

        for (int i = 0; i < num; i++)
        {
            string s = (i < (100 - from)) ? "0" : string.Empty;
            var image = new DicomImage("Assets/Resources/1-" + s + (i + from) + ".dcm");

            var data = image.PixelData;
            var pixelData = PixelDataFactory.Create(data, 0);
            
            for (int w = 0; w < pixelData.Width; w++)
            {
                for (int h = 0; h < pixelData.Height; h++)
                {
                    var pixel = pixelData.GetPixel(w, h);
                    values[offset * i + w * pixelData.Height + h] = (float)pixel;
                    //if (pixel < minVal) minVal = (int)pixel;
                    //if (pixel > maxVal) maxVal = (int)pixel;
                }
            }
        }

        texture.SetPixelData<float>(values, 0);
        texture.Apply();

        //Debug.Log("Min: " + minVal);
        //Debug.Log("Max: " + maxVal);

        AssetDatabase.CreateAsset(texture, @"Assets\3dDicom.asset");
    }
}
