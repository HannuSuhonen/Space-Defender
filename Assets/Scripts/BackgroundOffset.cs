using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundOffset : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.3f;
    Material myMaterial;
    Vector2 textureOffset; 


    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        textureOffset = new Vector2(0, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += textureOffset * Time.deltaTime;
    }
}
