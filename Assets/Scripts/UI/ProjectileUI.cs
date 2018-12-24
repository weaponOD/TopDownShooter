using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileUI : MonoBehaviour
{
    public bool Active { get { return spriteImage && spriteImage.sprite == projectileAvailableSprite; } }

    private Image spriteImage;
    private Sprite projectileAvailableSprite;
    private Sprite projectileUsedSprite;

    public void Init(Sprite projectileAvailableSprite, Sprite projectileUsedSprite, bool available = true)
    {
        if (spriteImage == null)
            spriteImage = GetComponent<Image>();

        this.projectileAvailableSprite = projectileAvailableSprite;
        this.projectileUsedSprite = projectileUsedSprite;

        if (available)
            spriteImage.sprite = projectileAvailableSprite;
        else
            spriteImage.sprite = projectileUsedSprite;
    }

    public void ProjectileUsed()
    {
        spriteImage.sprite = projectileUsedSprite;

        if (projectileUsedSprite == null)
            spriteImage.color = Color.clear;
        else
            spriteImage.color = Color.white;
    }

    public void Reloaded()
    {
        spriteImage.sprite = projectileAvailableSprite;

        if (projectileAvailableSprite == null)
            spriteImage.color = Color.clear;
        else
            spriteImage.color = Color.white;
    }
}
