using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSize : MonoBehaviour
{
    private ParticleSystem particles;
    
    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        var ItemSprite = particles.textureSheetAnimation.GetSprite(0);
        var main = particles.main;
        main.startSize = new ParticleSystem.MinMaxCurve(ItemSprite.rect.width/ItemSprite.pixelsPerUnit);
    }
}
