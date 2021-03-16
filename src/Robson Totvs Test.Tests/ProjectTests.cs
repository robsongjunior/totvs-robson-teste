using System;
using Xunit;
using Moq;
using Robson_Totvs_Test.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace Robson_Totvs_Test.Tests
{
    public class ProjectTests
    {
        [Fact]
        public async void CreateTokenOK()
        {
            var fakeTokenBuilder = new Mock<ITotvsTokenService>();
            fakeTokenBuilder.Setup(x => x.GenerateTokenAsync(It.IsAny<string>()))
                .Returns(Task.FromResult("my_new_token"));

            var fakeService = fakeTokenBuilder.Object;

            var myToken = await fakeService.GenerateTokenAsync(It.IsAny<string>());

            Assert.Equal("my_new_token", myToken);
        }

        [Fact]
        public async void CreateTokenNullExceptionError()
        {
            var fakeTokenBuilder = new Mock<ITotvsTokenService>();
            fakeTokenBuilder.Setup(x => x.GenerateTokenAsync(It.IsAny<string>()))
                .Throws(new NullReferenceException("Username can not be null"));

            var fakeService = fakeTokenBuilder.Object;

            try
            {
                await fakeService.GenerateTokenAsync(It.IsAny<string>());
            }
            catch(Exception ex)
            {
                Assert.Equal("Username can not be null", ex.Message);
            }
        }
    }
}
