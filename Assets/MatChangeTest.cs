using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MatChangeTest : MonoBehaviour
{
    public List<Material> material;

    public int index;

    public SpriteRenderer spriteRenderer;
    
    
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        choseMaterial(index);
    }

    private void choseMaterial(int index)
    {
        spriteRenderer.material = material[index];
    }
}
