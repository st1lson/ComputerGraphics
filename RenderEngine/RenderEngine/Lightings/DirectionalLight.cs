﻿using RenderEngine.Basic;
using RenderEngine.Core;
using RenderEngine.Interfaces;
using RenderEngine.Models;
using RenderEngine.Transformer;

namespace RenderEngine.Lightings;

public class DirectionalLight : ILighting
{
    public Vector3 LightDir { get; set; }

    public Pixel Color { get; set; } = new Pixel(255);

    private const float Threshold = 0.00001f;

    public DirectionalLight(Vector3 rayLight)
    {
        LightDir = -rayLight.Normalize();
    }

    public DirectionalLight(Vector3 rayLight, Pixel color)
    {
        LightDir = -rayLight.Normalize();
        Color = color;
    }

    public Pixel GetLight(IShape shape, IReadOnlyList<IShape> shapes, Vector3 intersectionPoint, Vector3 cameraPos)
    {
        Vector3 normal = shape.GetNormal(intersectionPoint).Normalize();
        float cosA = Vector3.Dot(normal, LightDir);
        float cosB = Vector3.Dot(normal, cameraPos - intersectionPoint);

        if ((cosA > Threshold && cosB < -Threshold) || (cosA < -Threshold && cosB < -Threshold)) 
        {
            normal = -normal;
        }

        Ray rayLight = new Ray(intersectionPoint, LightDir);
        bool isShadowed = false;
        foreach (IShape otherShapes in shapes)
        {
            if (otherShapes == shape) 
            {
                continue;
            }

            var intersection = otherShapes.Intersects(rayLight);
            if (intersection != null) 
            {
                isShadowed = true;
                break;
            }
        }

        var coef = isShadowed ? 0 : Math.Max(Vector3.Dot(normal, LightDir), 0);

        return Color * new Vector3(coef);
    }

    public void Transform(Transform transform)
    {
        LightDir = LightDir.TransformAsDirection(transform);
    }
}