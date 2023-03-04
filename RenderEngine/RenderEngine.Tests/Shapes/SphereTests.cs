using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine.Tests.Shapes
{
    public class SphereTests
    {
        [Fact]
        public void Sphere_Intersaction_Correct()
        {
            // Arrange
            var sphere = new Sphere(new Vector3(0, 5, 0), 2);
            var ray = new Ray(
                    Vector3.Zero,
                    new Vector3(0, 1.5f, 0.5f)
                );

            // Act
            var result = sphere.Intersects(ray);

            // Assert
            Assert.Equal(new Vector3(0, 3.338105f, 1.1127017f), result);
        }

        [Fact]
        public void Sphere_IntersactionInsideSphere_Correct()
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
        public void Sphere_NotIntersactsRay()
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
        public void Sphere_IntersactsBehindRay()
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
