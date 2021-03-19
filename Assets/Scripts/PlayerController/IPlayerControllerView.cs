using UnityEngine;

public interface IPlayerControllerView
{
    float GetDeltaTime();
    void Configure();
    Position GetPositionPacmanInTheMap();
}