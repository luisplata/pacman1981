using UnityEngine;

public class MapSpritesFactory
{
    private readonly MapSpritesConfiguration mapSpritesConfiguration;

    public MapSpritesFactory(MapSpritesConfiguration mapSpritesConfiguration)
    {
        this.mapSpritesConfiguration = mapSpritesConfiguration;
    }

    public MapSprite Create(string id)
    {
        var prefab = mapSpritesConfiguration.GetMapSpritePrefabById(id);

        return Object.Instantiate(prefab);
    }
}