using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class VariableManager : Singleton<VariableManager>
{
    private int _width = 0;
    private int _height = 0;

    public int Width { get => _width; set => _width = value; }
    public int Height { get => _height; set => _height = value; }
}

