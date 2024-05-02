using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Material mat;
    // Start is called before the first frame update
   

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (mat == null)
        {
            Graphics.Blit(source, destination);
        }
        Graphics.Blit(source, destination, mat);
    }
}
