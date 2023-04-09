﻿using RenderEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.Interfaces
{
    public interface IMeshReader
    {
        List<IMesh> Read(string path);
    }
}