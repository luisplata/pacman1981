using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class MapSprite : MonoBehaviour
{
    [SerializeField] protected string id;

    [SerializeField] private Sprite sprite;
    [SerializeField] private bool isTrigger; 

    public void Config()
    {
        GetComponent<SpriteRenderer>().sprite = Sprite;
        GetComponent<BoxCollider2D>().isTrigger = isTrigger;
    }

    public string Id => id;

    public Sprite Sprite => sprite;

    public Cell Cell { get; set; }

    public bool ColliderIsTrigger()
    {
        return isTrigger;
    }
}