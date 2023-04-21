namespace RenderEngine.Tests.Basic;

public class Vector3Tests
{
    [Fact]
    public void Vector3_PlusOperation_Correct()
    {
        // Arrange
        var firstVector = new Vector3(2);
        var secondVector = new Vector3(3);

        // Act
        var result = firstVector + secondVector;

        // Assert
        Assert.Equal(new Vector3(5), result);
    }

    [Fact]
    public void Vector3_MinusOperation_Correct()
    {
        // Arrange
        var firstVector = new Vector3(5);
        var secondVector = new Vector3(3);

        // Act
        var result = firstVector - secondVector;

        // Assert
        Assert.Equal(new Vector3(2), result);
    }

    [Fact]
    public void Vector3_UnaryMinusOperation_Correct()
    {
        // Arrange
        var vector = new Vector3(5, 9, -1);

        // Act
        var result = -vector;

        // Assert
        Assert.Equal(new Vector3(-5, -9, 1), result);
    }

    [Fact]
    public void Vector3_MultiplyOperation_Correct()
    {
        // Arrange
        var vector = new Vector3(5, 2, 3);

        // Act
        var result = vector * 3;

        // Assert
        Assert.Equal(new Vector3(15, 6, 9), result);
    }

    [Fact]
    public void Vector3_DivideOperation_Correct()
    {
        // Arrange
        var vector = new Vector3(15, 6, 9);

        // Act
        var result = vector / 3;

        // Assert
        Assert.Equal(new Vector3(5, 2, 3), result);
    }

    [Fact]
    public void Vector3_AbsOperation_Correct()
    {
        // Arrange
        var vector = new Vector3(5, 2, 3);

        // Act
        var result = vector.Abs();

        // Assert
        Assert.Equal(6.16441f, result, 1e-5);
    }

    [Fact]
    public void Vector3_NormalizeOperation_Correct()
    {
        // Arrange
        var vector = new Vector3(5, 2, 3);
        const float absoluteValue = 6.16441f;

        // Act
        var result = vector.Normalize();

        // Assert
        Assert.Equal(new Vector3(5 / absoluteValue, 2 / absoluteValue, 3 / absoluteValue), result);
    }

    [Fact]
    public void Vector3_DotOperation_Correct()
    {
        // Arrange
        var firstVector = new Vector3(5, 2, 3);
        var secondVector = new Vector3(2, 9, 1);

        // Act
        var result = Vector3.Dot(firstVector, secondVector);

        // Assert
        Assert.Equal(31, result);
    }

    [Fact]
    public void Vector3_CrossOperation_Correct()
    {
        // Arrange
        var firstVector = new Vector3(5, 2, 3);
        var secondVector = new Vector3(2, 9, 1);

        // Act
        var result = Vector3.Cross(firstVector, secondVector);

        // Assert
        Assert.Equal(new Vector3(-25, 1, 41), result);
    }

    [Fact]
    public void Vector3_EqualsOperation_Correct()
    {
        // Arrange
        var firstVector = new Vector3(5, 2, 3);
        var secondVector = new Vector3(5, 2, 3);

        // Act
        var result = firstVector == secondVector;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Vector3_NotEqualsOperation_Correct()
    {
        // Arrange
        var firstVector = new Vector3(5, 2, 3);
        var secondVector = new Vector3(3, 2, 5);

        // Act
        var result = firstVector != secondVector;

        // Assert
        Assert.False(result);
    }
}