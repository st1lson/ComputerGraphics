namespace RenderEngine.Tests.Shapes;

public class DiskTests
{
    [Fact]
    public void Disk_Intersection_Correct()
    {
        // Arrange
        var disk = new Disk(new Vector3(4, 4, 0), 2, new Vector3(0, 0, 1));
        var ray = new Ray(
            new Vector3(4, 4, 1),
            new Vector3(0, 0, -5)
        );

        // Act
        var result = disk.Intersects(ray);

        // Assert
        Assert.Equal(new Vector3(4, 4, 0), result);
    }

    [Fact]
    public void Disk_Intersection_On_Border_Correct()
    {
        // Arrange
        var disk = new Disk(new Vector3(4, 4, 0), 2, new Vector3(0, 0, 1));
        var ray = new Ray(
            new Vector3(2, 4, 1),
            new Vector3(0, 0, -5)
        );

        // Act
        var result = disk.Intersects(ray);

        // Assert
        Assert.Equal(new Vector3(2, 4, 0), result);
    }

    [Fact]
    public void Disk_NotIntersectsRay()
    {
        // Arrange
        var disk = new Disk(new Vector3(4, 4, 0), 2, new Vector3(0, 0, 1));
        var ray = new Ray(
            new Vector3(4, 4, 1),
            new Vector3(0, 0, 5)
        );

        // Act
        var result = disk.Intersects(ray);

        // Assert
        Assert.Null(result);
    }
}