using NUnit.Framework;

namespace homeworld;

public class LiquidSystemTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TransferTestWellToBucket()
    {
        // Arrange
        XY here = new XY(1,1);
        Entity well = Create.Well(here);
        Entity bucket = Create.Bucket(here);
        string liquid = "water";
        var bucket_capacity = LiquidSystem.GetCapacity(bucket);

        // Act
        LiquidSystem.Transfer(well, bucket_capacity, liquid, bucket);
        var well_contents = LiquidSystem.GetContents(well);
        var bucket_contents = LiquidSystem.GetContents(bucket);
        
        // Assert
        Assert.AreEqual(900, well_contents);
        Assert.AreEqual(100, bucket_contents);
    }
    [Test]
    public void TransferTestBucketToKettle()
    {
        // Arrange
        XY here = new XY(1,1);
        Entity bucket = Create.Bucket(here);
        Entity kettle = Create.Kettle(here);
        string liquid = "water";
        LiquidSystem.Fill(bucket, liquid);
        var kettle_capacity = LiquidSystem.GetCapacity(kettle);

        // Act
        LiquidSystem.Transfer(bucket, kettle_capacity, liquid, kettle);
        var kettle_contents = LiquidSystem.GetContents(kettle);
        var bucket_contents = LiquidSystem.GetContents(bucket);
        
        // Assert
        Assert.AreEqual(20, kettle_contents);
        Assert.AreEqual(80, bucket_contents);
    }
    [Test]
    public void TransferTestKettleToBucket()
    {
        // Arrange
        XY here = new XY(1,1);
        Entity kettle = Create.Kettle(here);
        Entity bucket = Create.Bucket(here);
        string liquid = "water";
        LiquidSystem.Fill(kettle, liquid);
        var kettle_capacity = LiquidSystem.GetCapacity(kettle);

        // Act
        LiquidSystem.Transfer(kettle, kettle_capacity, liquid, bucket);
        var kettle_contents = LiquidSystem.GetContents(kettle);
        var bucket_contents = LiquidSystem.GetContents(bucket);
        
        // Assert
        Assert.AreEqual(0, kettle_contents);
        Assert.AreEqual(20, bucket_contents);
    }
}