using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.Tests.Shapes
{
    public class TriangleTests
    {
        [Fact]
        public void Triangle_Intersaction_Correct()
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
        public void Triangle_IntersactionInsideSphere_Correct()
        {
            // Arrange
            var sphere = new Sphere(new Vector3(0, 5, 0), 2);
            var ray = new Ray(
                    new Vector3(0, 5, 0),
                    new Vector3(0, 1, 0)
                );

            // Act
            var result = sphere.Intersects(ray);

            // Assert
            Assert.Equal(new Vector3(0, 7, 0), result);
        }

        [Fact]
        public void Triangle_NotIntersactsRay()
        {
            // Arrange
            var sphere = new Sphere(new Vector3(0, 5, 0), 2);
            var ray = new Ray(
                    Vector3.Zero,
                    new Vector3(0, 0.5f, 15f)
                );

            // Act
            var result = sphere.Intersects(ray);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Triangle_IntersactsBehindRay()
        {
            // Arrange
            var sphere = new Sphere(new Vector3(0, 5, 0), 2);
            var ray = new Ray(
                    new Vector3(0, 8, 0),
                    new Vector3(0, 1, 0)
                );

            // Act
            var result = sphere.Intersects(ray);

            // Assert
            Assert.Null(result);
        }
    }
}
