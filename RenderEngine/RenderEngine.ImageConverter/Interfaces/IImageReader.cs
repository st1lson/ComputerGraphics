﻿using RenderEngine.Models;

namespace RenderEngine.ImageConverter.Interfaces;

public interface IImageReader : IPlugin
{
    Bitmap Read(Stream source);

    bool Validate(Stream stream);
}