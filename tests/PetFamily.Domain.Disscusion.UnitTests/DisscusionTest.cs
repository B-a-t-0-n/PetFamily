using PetFamily.Disscusions.Domain.Entity;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.IDs;

namespace PetFamily.Domain.Disscusion.UnitTests;

public class DisscusionTest
{
    private Disscusions.Domain.Entity.Disscusion CreateDisscusion()
    {
        return Disscusions.Domain.Entity.Disscusion.Create(
            DisscusionId.Create(Guid.NewGuid()),
            Guid.NewGuid(),
            new List<UserId> { UserId.NewUserId(), UserId.NewUserId() }).Value;
    }

    [Fact]
    public void CreateDisscusion_ShouldCreateDisscusion()
    {
        // Arrange & Act
        var disscusion = CreateDisscusion();

        // Assert
        Assert.NotNull(disscusion);
        Assert.Equal(2, disscusion.Users.Count);
    }

    [Fact]
    public void AddMessage_ShouldAddMessageToDisscusion()
    {
        // Arrange
        var disscusion = CreateDisscusion();
        var message = Message.Create(MessageId.NewMessageId(), Text.Create("Hello").Value, DateTime.UtcNow, disscusion.Users.First()).Value;

        // Act
        var result = disscusion.AddMessage(message);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(disscusion.Messages);
    }

    [Fact]
    public void CloseDisscusion_ShouldCloseDisscusion()
    {
        // Arrange
        var disscusion = CreateDisscusion();

        // Act
        var result = disscusion.Close();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(disscusion.IsClosed);
    }

    [Fact]
    public void EditMessage_ShouldEditMessage()
    {
        // Arrange
        var disscusion = CreateDisscusion();
        var message = Message.Create(MessageId.NewMessageId(), Text.Create("Hello").Value, DateTime.UtcNow, disscusion.Users.First()).Value;
        disscusion.AddMessage(message);
        var newText = Text.Create("Hello, World!").Value;

        // Act
        var result = disscusion.EditMessage(message.Id, newText, message.UserId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Hello, World!", disscusion.Messages.First().Text.Value);
    }

    [Fact]
    public void DeleteMessage_ShouldRemoveMessageFromDisscusion()
    {
        // Arrange
        var disscusion = CreateDisscusion();
        var message = Message.Create(MessageId.NewMessageId(), Text.Create("Hello").Value, DateTime.UtcNow, disscusion.Users.First()).Value;
        disscusion.AddMessage(message);

        // Act
        var result = disscusion.DeleteMessage(message.Id, message.UserId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(disscusion.Messages);
    }
}
