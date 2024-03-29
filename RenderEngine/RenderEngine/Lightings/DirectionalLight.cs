﻿using RenderEngine.Basic;
using RenderEngine.Interfaces;
using RenderEngine.Models;
using RenderEngine.Transformer;

namespace RenderEngine.Lightings;

public class DirectionalLight : ILighting
{
    public Vector3 LightDir { get; set; }

    public Pixel Color { get; set; } = new Pixel(255);

    public float Strength { get; set; } = 1;

    private const float Threshold = 0.00001f;

    public DirectionalLight(Vector3 rayLight)
    {
        LightDir = -rayLight.Normalize();
    }

    public DirectionalLight(Vector3 rayLight, Pixel color, float stength)
    {
        LightDir = -rayLight.Normalize();
        Color = color;
        Strength = stength;
    }

    public Pixel GetLight(IShape shape, IReadOnlyList<IShape> shapes, Vector3 intersectionPoint, Vector3 cameraPos, IOptimizer optimizer)
    {
        Vector3 normal = shape.GetInterpolatedNormal(intersectionPoint);
        float cosA = Vector3.Dot(normal, LightDir);
        float cosB = Vector3.Dot(normal, cameraPos - intersectionPoint);

        if ((cosA > Threshold && cosB < -Threshold) || (cosA < -Threshold && cosB < -Threshold)) 
        {
            normal = -normal;
        }

        Ray rayLight = new Ray(intersectionPoint, LightDir);

        (Vector3? shadowedPoint, _) = optimizer.GetIntersection(rayLight, intersectionPoint);

        var coefficient = shadowedPoint != null ? 0 : Math.Max(Vector3.Dot(normal, LightDir), 0);

        return Color * (new Vector3(coefficient) * Strength);
    }

    public void Transform(Transform transform)
    {
        LightDir = LightDir.TransformAsDirection(transform);
    }
}