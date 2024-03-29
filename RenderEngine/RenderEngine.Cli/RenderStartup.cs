﻿using RenderEngine.Basic;
using RenderEngine.Cli.CommandLineCommands;
using RenderEngine.Cli.Configurations;
using RenderEngine.Cli.IO.Readers;
using RenderEngine.Cli.IO.Writers;
using RenderEngine.Core;
using RenderEngine.Core.Scenes;
using RenderEngine.DependencyInjection;
using RenderEngine.Interfaces;
using RenderEngine.Lightings;
using RenderEngine.Transformer;
using RenderEngine.Optimizers;
using System.Diagnostics;

namespace RenderEngine.Cli;

internal sealed class RenderStartup : IService
{
    private readonly RenderCommand _command;

    [Service]
    private readonly ImageWriter _imageWriter = null!;

    [Service]
    private readonly SceneFactory _sceneFactory = null!;

    [Service]
    private readonly ObjReader _objReader = null!;

    public RenderStartup(RenderCommand command)
    {
        _command = command;
    }

    public void Run()
    {
        (Camera camera, Scene scene) = _sceneFactory.CreateScene(_command, _objReader);

        List<IOptimizer> optimizers = CreateOptimizers(_command.Mode);
        foreach (var optimizer in optimizers)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            optimizer.Build(scene.Shapes);

            Renderer _renderer = new Renderer(camera, scene, optimizer);

            var image = _renderer.Render();

            _imageWriter.Write(image, _command);

            stopwatch.Stop();

            Console.WriteLine($"{optimizer.GetType().Name} - {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    private List<IOptimizer> CreateOptimizers(string mode)
    {
        return mode switch
        {
            "default" => new List<IOptimizer>() { new DefaultOptimizer() },
            "octree" => new List<IOptimizer>() { new OctTree(new BoundingBox(new Vector3(-30, -30, -30), new Vector3(30, 30, 30))) },
            "octree-compare" => CreateOptimizers("default").Concat(CreateOptimizers("octree")).ToList(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
