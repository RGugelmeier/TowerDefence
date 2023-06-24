using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGScroller : MonoBehaviour
{
    [SerializeField]
    private float bgSpeed;
    [SerializeField]
    private Renderer bgRenderer;   

    //Move the offset of the image every frame by the bgSpeed
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(bgSpeed * Time.deltaTime, -(bgSpeed / 2) * Time.deltaTime);
    }
}
