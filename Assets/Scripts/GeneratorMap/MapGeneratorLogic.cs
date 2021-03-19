using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapGeneratorLogic
{
    private IMapGeneratorView mapGeneratorView;

    private int height, weidht;
    public Cell[,] map;
    public MapGeneratorLogic(IMapGeneratorView mapGeneratorView, float separacion)
    {
        this.mapGeneratorView = mapGeneratorView;
        var file = ReadFile(@"Assets/Resources/map.csv");
        map = new Cell[file[0].Length, file.Count];
        height = 0;
        foreach (string[] s in file)
        {
            weidht = 0;
            foreach (string ss in s)
            {
                map[height, weidht] = new Cell(ss, height, weidht, separacion);
                map[height, weidht].Name += "_" + height + "_" + weidht;
                weidht++;
            }
            height++;
        }
        DeterminWhatSpriteIsRender(map);
        mapGeneratorView.CreateSpritesInGame(map);
        ServiceLocator.Instance.GetService<IMapGenerator>().SaveMap(map);
    }

    private void DeterminWhatSpriteIsRender(Cell[,] map)
    {
        foreach(Cell cell in map)
        {
            if (cell.Type.Equals("+"))
            {
                
            }
            int Y = (int)cell.PositionInList.Y;
            int X = (int)cell.PositionInList.X;
            try
            {
                cell.Render = DeterminateRender(map[X, Y], ".", "Point");
            }
            catch (TypeOfRenderException) { }
            try
            {
                cell.Render = DeterminateRender(map[X, Y], "^", "Background");
            }
            catch (TypeOfRenderException) { }
            try
            {
                cell.Render = DeterminateRender(map[X, Y], "v", "Pacman");
            }
            catch (TypeOfRenderException){ }
            try
            {
                cell.Render = DeterminateRender(map[X, Y], "p", "PowerUp");
            }
            catch (TypeOfRenderException) { }
            try
            {
                cell.Render = DeterminateRender(map[X, Y], "+", "Esquina");
                if(X == 0)
                {
                    cell.Render += "Izq";
                }
                else
                {
                    cell.Render += "Der";
                }
                if(Y == 0)
                {
                    cell.Render += "Sup";
                }
                else
                {
                    cell.Render += "Inf";
                }
            }
            catch (TypeOfRenderException) { }
            try
            {
                cell.Render = DeterminateRender(map[X, Y], "l", "Pared");
                if (Y == 0)
                {
                    cell.Render += "Sup";
                }
                else
                {
                    cell.Render += "Inf";
                }
            }
            catch (TypeOfRenderException) { }
            try
            {
                cell.Render = DeterminateRender(map[X, Y], "=", "Pared");
                if (X == 0)
                {
                    cell.Render += "Izq";
                }
                else
                {
                    cell.Render += "Der";
                }
            }
            catch (TypeOfRenderException) { }

            try
            {
                cell.Render = DeterminateRender(map[X, Y], "-", "ParedDelgadaV");
                if (CheckInPositionIsCaracter(map, X, Y, -1, 0, "."))
                {
                    cell.Render += "Izq";
                }
                if (CheckInPositionIsCaracter(map, X, Y, 1, 0, "."))
                {
                    cell.Render += "Der";
                }
            }
            catch (TypeOfRenderException) { }
            try
            {
                cell.Render = DeterminateRender(map[X, Y], "i", "ParedDelgadaH");
                if (CheckInPositionIsCaracter(map, X, Y, 0,-1, "."))
                {
                    cell.Render += "Sup";
                }
                if (CheckInPositionIsCaracter(map, X, Y,0, 1, "."))
                {
                    cell.Render += "Inf";
                }
            }
            catch (TypeOfRenderException) { }
            try
            {
                cell.Render = DeterminateRender(map[X, Y], "*", "EsquinaDelgada");
                if (CheckInPositionIsCaracter(map, X, Y, 0, 1, "."))
                {
                    cell.Render += "Inf";
                    if(CheckInPositionIsCaracter(map, X, Y, -1, 0, "."))
                    {
                        cell.Render += "Izq";
                    }
                    if (CheckInPositionIsCaracter(map, X, Y, +1, 0, "."))
                    {
                        cell.Render += "Der";
                    }
                }
                if (CheckInPositionIsCaracter(map, X, Y, 0, -1, "."))
                {
                    cell.Render += "Sup";
                    if (CheckInPositionIsCaracter(map, X, Y, -1, 0, "."))
                    {
                        cell.Render += "Izq";
                    }
                    if (CheckInPositionIsCaracter(map, X, Y, +1, 0, "."))
                    {
                        cell.Render += "Der";
                    }
                }
            }
            catch (TypeOfRenderException) { }
        }
    }
    private bool CheckInPositionIsCaracter(Cell[,] map, int x, int y, int incrementX, int incrementY, string caracter)
    {
        return map[x + incrementX, y + incrementY].Type.Equals(caracter);
    }
    private string DeterminateRender(Cell inMap, string character, string render)
    {
        var isEcualToSame = inMap.Type.Equals(character);
        var isEcualToBarr = inMap.Type.Equals(character);
        if (isEcualToSame && isEcualToBarr)
        {
            return render;
        }
        throw new TypeOfRenderException("The render is not exist");
    }

    public List<string[]> ReadFile(string path)
    {
        string line;
        List<string[]> result = new List<string[]>();
        StreamReader file = new StreamReader(path);
        while ((line = file.ReadLine()) != null)
        { 
            result.Add(line.Split(' '));
        }
        file.Close();
        return result;
    }
}