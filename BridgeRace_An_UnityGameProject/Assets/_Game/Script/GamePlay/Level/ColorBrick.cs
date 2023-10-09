using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ColorBrick : MonoBehaviour
{
    [SerializeField] ColorData colorData;
    [SerializeField] Renderer meshRenderer;

    public ColorType color;
    public void ChangeColor(ColorType colorType)
    {
        color = colorType;
        meshRenderer.material = colorData.GetMat(colorType);
    }
}
