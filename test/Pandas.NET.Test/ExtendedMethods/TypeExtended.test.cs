using System;
using Xunit;

public class TypeExtendedTests
{
    [Fact]
    public void TestIsNumericType()
    {
        // Arrange
        Type intType = typeof(int);
        Type nullableIntType = typeof(int?);
        Type stringType = typeof(string);
        Type doubleType = typeof(double);
        Type floatType = typeof(float);

        // Act
        bool isIntNumeric = intType.IsNumericType();
        bool isNullableIntNumeric = nullableIntType.IsNumericType();
        bool isStringNumeric = stringType.IsNumericType();
        bool isDoubleNumeric = doubleType.IsNumericType();
        bool isFloatNumeric = floatType.IsNumericType();

        // Assert
        Assert.True(isIntNumeric);
        Assert.True(isNullableIntNumeric);
        Assert.False(isStringNumeric);
        Assert.True(isDoubleNumeric);
        Assert.True(isFloatNumeric);
    }
}