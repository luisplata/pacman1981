using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/MapSprite configuration")]
public class MapSpritesConfiguration : ScriptableObject
{
    [SerializeField] private MapSprite[] mapSprite;
    private Dictionary<string, MapSprite> idToMapSprite;

    private void Awake()
    {
        idToMapSprite = new Dictionary<string, MapSprite>(mapSprite.Length);
        foreach (var mapSprite in mapSprite)
        {
            idToMapSprite.Add(mapSprite.Id, mapSprite);
        }
    }

    public MapSprite GetMapSpritePrefabById(string id)
    {
        if (!idToMapSprite.TryGetValue(id, out var mapSprite))
        {
            throw new Exception($"PowerUp with id {id} does not exit");
        }
        return mapSprite;
    }
}