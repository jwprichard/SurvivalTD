using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class VariableManager
{
    private static int _width = 0;
    public static int Width { get => _width; set => _width = value; }

    private static int _height = 0;
    public static int Height { get => _height; set => _height = value; }

    public static readonly float SpawnPointMultiplier = 1.2f;
    public static readonly float SpawnPointIncrementConstant = 2f;
}