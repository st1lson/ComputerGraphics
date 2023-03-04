namespace RenderEngine.Tests.Basic
{
    public class RayTests
    {
        [Fact]
        public void Ray_GetPoint_Correct()
        {
            // Arrange
            var ray = new Ray(new Vector3(3), new Vector3(2));

            // Act
            var result = ray.GetPoint(1.5f);

            // Assert
            Assert.Equal(new Vector3(6), result);
        }
    }
}
