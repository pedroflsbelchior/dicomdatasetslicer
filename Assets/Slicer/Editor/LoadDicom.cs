using Dicom.Imaging;
using Dicom.Imaging.Render;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LoadDicom
{
    static float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    public static void CreateDicomVolumeTexture(List<DefaultAsset> files, string filename)
    {
        if (files == null || files.Count == 0 || string.IsNullOrWhiteSpace(filename))
        {
            return;
        }

        var tmp = new DicomImage(AssetDatabase.GetAssetPath(files[0]));
        int width = tmp.Width;
        int height = tmp.Height;
        int offset = width * height;
        int depth = files.Count;

        int maxVal = -10000;
        int minVal = 10000;

        float[] values = new float[width * height * depth];

        TextureFormat format = TextureFormat.RFloat;
        TextureWrapMode wrapMode = TextureWrapMode.Clamp;

        Texture3D texture = new Texture3D(width, height, files.Count, format, false);
        texture.wrapMode = wrapMode;
        texture.anisoLevel = 1;
        texture.filterMode = FilterMode.Trilinear;

        for (int i = 0; i < files.Count; i++)
        {
            var image = new DicomImage(AssetDatabase.GetAssetPath(files[i]));

            var data = image.PixelData;
            var pixelData = PixelDataFactory.Create(data, 0);

            for (int w = 0; w < pixelData.Width; w++)
            {
                for (int h = 0; h < pixelData.Height; h++)
                {
                    var pixel = pixelData.GetPixel(w, h);
                    values[offset * i + w * pixelData.Height + h] = (float)pixel;
                    if (pixel < minVal) minVal = (int)pixel;
                    if (pixel > maxVal) maxVal = (int)pixel;
                }
            }
        }

        for (int i = 0; i < values.Length; i++)
            values[i] = map(values[i], minVal, maxVal, 0f, 1f);

        texture.SetPixelData<float>(values, 0);
        texture.Apply();

        //Debug.Log("Min: " + minVal);
        //Debug.Log("Max: " + maxVal);

        AssetDatabase.CreateAsset(texture, @"Assets\" + filename + ".asset");
    }
}
