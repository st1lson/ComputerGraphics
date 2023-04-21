namespace RenderEngine.Tests.Shapes;

public class TriangleTests
{
    [Fact]
    public void Triangle_Intersection_On_Border_Correct()
    {
        // Arrange
        var triangle = new Triangle(new Vector3(2, 4, 0), new Vector3(4, 2, 0), new Vector3(4, 4, 0));
        var ray = new Ray(
            new Vector3(3, 3, 1),
            new Vector3(0, 0, -5)
        );

        // Act
        var result = triangle.Intersects(ray);

        // Assert
        Assert.Equal(new Vector3(3, 3, 0), result);
    }

    [Fact]
    public void Triangle_Intersection_Correct()
    {
        // Arrange
        var triangle = new Triangle(new Vector3(2, 3, 0), new Vector3(4, 2, 0), new Vector3(4, 4, 0));
        var ray = new Ray(
            new Vector3(3, 3, 1),
            new Vector3(0, 0, -5)
        );

        // Act
        var result = triangle.Intersects(ray);

        // Assert
        Assert.Equal(new Vector3(3, 3, 0), result);
    }

    [Fact]
    public void Triangle_NotIntersectsRay()
    {
        // Arrange
        var triangle = new Triangle(new Vector3(2, 4, 0), new Vector3(4, 2, 0), new Vector3(4, 4, 0));
        var ray = new Ray(
            new Vector3(3, 3, 1),
            new Vector3(0, 0, 5)
        );

        // Act
        var result = triangle.Intersects(ray);

        // Assert
        Assert.Null(result);
    }
}