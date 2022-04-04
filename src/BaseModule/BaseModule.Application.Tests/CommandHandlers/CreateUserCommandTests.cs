using AutoFixture;
using AutoFixture.AutoMoq;
using BaseModule.Common;
using CoreModule.Domain.Users;
using MockQueryable.Moq;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BaseModule.CommandHandlers;

public class CreateUserCommandTests : TestBase
{
    [Fact]
    public async Task CorrectData_CreateUser_Success()
    {
        var fixture = new Fixture()
            .Customize(new AutoMoqCustomization());

        var command = fixture.Create<CreateUserCommand>();

        var users = fixture.CreateMany<User>(5);

        var mockUserDbSet = users.AsQueryable().BuildMockDbSet();

        mockUserDbSet.Setup(x => x.Add(It.IsAny<User>()));

        Mock<IBaseModuleDbContext> mockDbContext = new Mock<IBaseModuleDbContext>();

        mockDbContext.Setup(x => x.Users).Returns(mockUserDbSet.Object);

        var handler = new CreateUserCommand.Handler(mockDbContext.Object, Mapper);

        var result = await handler.Handle(command);

        Assert.True(result.Succeeded);
    }
}
