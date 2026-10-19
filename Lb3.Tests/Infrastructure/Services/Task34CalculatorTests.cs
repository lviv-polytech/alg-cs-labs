using Lpnu.Lb3.Core.Models;
using Lpnu.Lb3.Core.Services;
using Lpnu.Lb3.Infrastructure;
using Xunit;

namespace Lpnu.Lb3.Tests.Infrastructure.Services;

/// <summary>
/// Unit tests for Lab 3 Task 3.4: Target region hit detection for point (x, y) with radius R.
/// </summary>
public class Task34CalculatorTests
{
    private readonly ITask34Calculator _calculator = Lb3ServiceFactory.CreateTask34Calculator();

    [Theory]
    [InlineData(-1.0, 0.5, 2.0)]
    [InlineData(-1.5, 0.2, 2.0)]
    [InlineData(-0.5, 0.8, 3.0)]
    public void Calculate_PointInsideRegion1_ReturnsHitAndRegion1Zone(double x, double y, double r)
    {
        var result = _calculator.Calculate(x, y, r);

        Assert.True(result.IsHit);
        Assert.Equal(HitZone.Region1UpperLeft, result.Zone);
        Assert.Contains("Quadrant II", result.Message);
        Assert.True(_calculator.IsInRegion1(x, y, r));
        Assert.False(_calculator.IsInRegion2(x, y, r));
    }

    [Theory]
    [InlineData(1.0, -0.5, 2.0)]
    [InlineData(1.5, -0.2, 2.0)]
    [InlineData(0.5, -0.8, 3.0)]
    public void Calculate_PointInsideRegion2_ReturnsHitAndRegion2Zone(double x, double y, double r)
    {
        var result = _calculator.Calculate(x, y, r);

        Assert.True(result.IsHit);
        Assert.Equal(HitZone.Region2LowerRight, result.Zone);
        Assert.Contains("Quadrant IV", result.Message);
        Assert.False(_calculator.IsInRegion1(x, y, r));
        Assert.True(_calculator.IsInRegion2(x, y, r));
    }

    [Fact]
    public void Calculate_Origin_ReturnsHitAndOriginZone()
    {
        var result = _calculator.Calculate(0.0, 0.0, 2.0);

        Assert.True(result.IsHit);
        Assert.Equal(HitZone.Origin, result.Zone);
        Assert.Contains("origin (0, 0)", result.Message);
        Assert.True(_calculator.IsInRegion1(0.0, 0.0, 2.0));
        Assert.True(_calculator.IsInRegion2(0.0, 0.0, 2.0));
    }

    [Theory]
    [InlineData(2.0)]
    [InlineData(5.0)]
    [InlineData(10.0)]
    public void Calculate_Region1KeyBoundaryPoints_ReturnsHit(double r)
    {
        // Boundary: Sector center on X axis (-R, 0)
        var leftVertex = _calculator.Calculate(-r, 0.0, r);
        Assert.True(leftVertex.IsHit);
        Assert.Equal(HitZone.Region1UpperLeft, leftVertex.Zone);

        // Boundary: Top-left vertical segment corner (-R, R)
        var topLeftCorner = _calculator.Calculate(-r, r, r);
        Assert.True(topLeftCorner.IsHit);
        Assert.Equal(HitZone.Region1UpperLeft, topLeftCorner.Zone);

        // Boundary: Point on circular arc at 45 degrees
        double arcX = -r + r / Math.Sqrt(2.0);
        double arcY = r / Math.Sqrt(2.0);
        var arcPoint = _calculator.Calculate(arcX, arcY, r);
        Assert.True(arcPoint.IsHit);
        Assert.Equal(HitZone.Region1UpperLeft, arcPoint.Zone);
    }

    [Theory]
    [InlineData(2.0)]
    [InlineData(5.0)]
    [InlineData(10.0)]
    public void Calculate_Region2KeyBoundaryPoints_ReturnsHit(double r)
    {
        // Boundary: Sector center on X axis (R, 0)
        var rightVertex = _calculator.Calculate(r, 0.0, r);
        Assert.True(rightVertex.IsHit);
        Assert.Equal(HitZone.Region2LowerRight, rightVertex.Zone);

        // Boundary: Bottom-right vertical segment corner (R, -R)
        var bottomRightCorner = _calculator.Calculate(r, -r, r);
        Assert.True(bottomRightCorner.IsHit);
        Assert.Equal(HitZone.Region2LowerRight, bottomRightCorner.Zone);

        // Boundary: Point on circular arc at 225 degrees from center (R, 0)
        double arcX = r - r / Math.Sqrt(2.0);
        double arcY = -r / Math.Sqrt(2.0);
        var arcPoint = _calculator.Calculate(arcX, arcY, r);
        Assert.True(arcPoint.IsHit);
        Assert.Equal(HitZone.Region2LowerRight, arcPoint.Zone);
    }

    [Theory]
    [InlineData(1.0, 1.0, 2.0)]      // Quadrant I (always outside)
    [InlineData(3.0, 4.0, 2.0)]      // Quadrant I far away
    [InlineData(-1.0, -1.0, 2.0)]    // Quadrant III (always outside)
    [InlineData(-3.0, -2.0, 2.0)]    // Quadrant III far away
    [InlineData(0.0, 2.0, 2.0)]      // (0, R) on positive Y axis is outside the circle: (0+R)^2 + R^2 = 2R^2 > R^2
    [InlineData(0.0, -2.0, 2.0)]     // (0, -R) on negative Y axis is outside the circle: (0-R)^2 + R^2 = 2R^2 > R^2
    [InlineData(-2.01, 1.0, 2.0)]    // Beyond left vertical boundary x < -R
    [InlineData(2.01, -1.0, 2.0)]    // Beyond right vertical boundary x > R
    [InlineData(-1.0, 2.01, 2.0)]    // Outside upper arc
    [InlineData(1.0, -2.01, 2.0)]    // Outside lower arc
    public void Calculate_PointsOutsideTarget_ReturnsNoHitAndOutsideZone(double x, double y, double r)
    {
        var result = _calculator.Calculate(x, y, r);

        Assert.False(result.IsHit);
        Assert.Equal(HitZone.Outside, result.Zone);
        Assert.Contains("does NOT fall", result.Message);
        Assert.False(_calculator.IsInRegion1(x, y, r));
        Assert.False(_calculator.IsInRegion2(x, y, r));
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    [InlineData(-100.0)]
    public void Calculate_NonPositiveRadius_ThrowsArgumentOutOfRangeException(double nonPositiveR)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(0.0, 0.0, nonPositiveR));
    }

    [Theory]
    [InlineData(double.NaN, 0.0, 2.0)]
    [InlineData(0.0, double.NaN, 2.0)]
    [InlineData(0.0, 0.0, double.NaN)]
    [InlineData(double.PositiveInfinity, 0.0, 2.0)]
    [InlineData(0.0, double.NegativeInfinity, 2.0)]
    [InlineData(0.0, 0.0, double.PositiveInfinity)]
    public void Calculate_NonFiniteValues_ThrowsArgumentOutOfRangeException(double x, double y, double r)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(x, y, r));
    }
}
