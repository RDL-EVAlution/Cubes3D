using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct TwoStateButtonReference
{
    public Sprite onImage;
    public Sprite offImage;
    public Image imageComponent;

    public TwoStateButtonReference(Sprite onImage, Sprite offImage, Image imageComponent)
    {
        this.onImage = onImage;
        this.offImage = offImage;
        this.imageComponent = imageComponent;
    }
}
