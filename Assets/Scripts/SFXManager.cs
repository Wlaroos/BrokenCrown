using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] coinSFX;
    [SerializeField] private AudioClip[] enemyDownSFX;
    [SerializeField] private AudioClip[] enemyHitSFX;
    [SerializeField] private AudioClip[] gameOverSFX;
    [SerializeField] private AudioClip[] playerHurtSFX;
    [SerializeField] private AudioClip[] punchWhooshSFX;
    [SerializeField] private AudioClip[] healSFX;
    [SerializeField] private AudioClip[] destructableSFX;
    [SerializeField] private AudioClip[] upgradeSFX;
    [SerializeField] private AudioClip[] rerollSFX;
    [SerializeField] private AudioClip[] textboxHoverSFX;
    [SerializeField] private AudioClip[] noMoneySFX;
    
    public static SFXManager Instance { get; private set; }
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    public void PlayCoinSFX()
    {
        AudioHelper.PlayClip2D(coinSFX[Random.Range(0, coinSFX.Length)], 0.2f);
    }
    
    public void PlayEnemyDownSFX()
    {
        AudioHelper.PlayClip2D(enemyDownSFX[Random.Range(0, enemyDownSFX.Length)], .5f);
    }
    
    public void PlayEnemyHitSFX()
    {
        AudioHelper.PlayClip2D(enemyHitSFX[Random.Range(0, enemyHitSFX.Length)], 1f);
    }
    
    public void PlayGameOverSFX()
    {
        AudioHelper.PlayClip2D(gameOverSFX[Random.Range(0, gameOverSFX.Length)], 1f);
    }
    
    public void PlayPlayerHurtSFX()
    {
        AudioHelper.PlayClip2D(playerHurtSFX[Random.Range(0, playerHurtSFX.Length)], 1f);
    }
    
    public void PlayPunchWhooshSFX()
    {
        AudioHelper.PlayClip2D(punchWhooshSFX[Random.Range(0, punchWhooshSFX.Length)], .5f);
    }
    
    public void PlayHealSFX()
    {
        AudioHelper.PlayClip2D(healSFX[Random.Range(0, healSFX.Length)], 1f);
    }
    
    public void PlayDestructableSFX()
    {
        AudioHelper.PlayClip2D(destructableSFX[Random.Range(0, destructableSFX.Length)], .33f);
    }
    
    public void PlayUpgradeSFX()
    {
        AudioHelper.PlayClip2D(upgradeSFX[Random.Range(0, upgradeSFX.Length)], 0.8f);
    }
    
    public void PlayRerollSFX()
    {
        AudioHelper.PlayClip2D(rerollSFX[Random.Range(0, rerollSFX.Length)], 0.8f);
    }
    
    public void PlayTextboxHoverSFX()
    {
        AudioHelper.PlayClip2D(textboxHoverSFX[Random.Range(0, textboxHoverSFX.Length)], 0.66f);
    }
    
    public void PlayNoMoneySFX()
    {
        AudioHelper.PlayClip2D(noMoneySFX[Random.Range(0, noMoneySFX.Length)], .66f);
    }
}
