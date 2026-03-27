using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Brick Destroy")]
    public AudioClip clipDestroy;
    [Header("BallHit - Brick Types")]
    public AudioClip clipType1;
    public AudioClip clipType2;
    public AudioClip clipType3;
    [Header("BallHit - Other")]
    public AudioClip clipWall;
    public AudioClip clipBall;
    public AudioClip clipPaddle;
    public AudioClip clipBullet;
    public AudioClip clipBallDeath;
    [Header("Bullet")]
    public AudioClip clipBulletFire;
    public AudioClip clipBulletHit;
    [Header("Other")]
    public AudioClip clipLose;
    public AudioClip clipVictory;
    public AudioClip clipStatic;
    public AudioClip clipButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayDestroy()
    {
        string type = "Brick";
        AudioManager.Instance.PlaySFX(clipDestroy, type);
    }
    public void PlayType1()
    {
        string type = "Brick";
        AudioManager.Instance.PlaySFX(clipType1, type);
    }
    public void PlayType2()
    {
        string type = "Brick";
        AudioManager.Instance.PlaySFX(clipType2, type);
    }
    public void PlayType3()
    {
        string type = "Brick";
        AudioManager.Instance.PlaySFX(clipType3, type);
    }
    public void PlayWall()
    {
        string type = "Ball";
        AudioManager.Instance.PlaySFX(clipWall, type);
    }
    public void PlayBall()
    {
        string type = "Ball";
        AudioManager.Instance.PlaySFX(clipBall, type);
    }
    public void PlayBallDeath()
    {
        string type = "Ball";
        AudioManager.Instance.PlaySFX(clipBallDeath, type);
    }
    public void PlayPaddle()
    {
        string type = "Ball";
        AudioManager.Instance.PlaySFX(clipPaddle, type);
    }
    public void PlayBullet()
    {
        string type = "Projectile";
        AudioManager.Instance.PlaySFX(clipBullet, type);
    }
    public void PlayBulletFire()
    {
        string type = "Projectile";
        AudioManager.Instance.PlaySFX(clipBulletFire, type);
    }
    public void PlayBulletHit()
    {
        string type = "Projectile";
        AudioManager.Instance.PlaySFX(clipBulletHit, type);
    }
    public void PlayLose()
    {
        string type = "Lose";
        AudioManager.Instance.PlaySFX(clipLose, type);
    }
    public void PlayVictory()
    {
        string type = "Victory";
        AudioManager.Instance.PlaySFX(clipVictory, type);
    }
    public void PlayButton()
    {
        string type = "Button";
        AudioManager.Instance.PlaySFX(clipButton, type);
    }
    public void PlayStatic()
    {
        string type = "Static";
        AudioManager.Instance.PlaySFX(clipStatic, type);
    }
}