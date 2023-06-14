using Xunit;
using Moq;
using FluentAssertions;
using CadastroCliente.Model;
using CadastroCliente.Infra.IRepository;
using CadastroCliente.Services.Services;
using CadastroCliente.Services.Validators;
using System.Threading.Tasks;
using CadastroCliente.Test;
using FluentValidation.Results;
using UserService = CadastroCliente.Test.UserService;

namespace CadastroCliente.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task UpdateUserAsync_ShouldReturnUpdatedUser_WhenUserExists()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var mockValidator = new Mock<IUserValidator>(); // Mock UserValidator
            var user = new User { Id = 1, Name = "Test" };
            mockRepo.Setup(repo => repo.UpdateUserAsync(user))
                .ReturnsAsync(new User
                {
                    Id = 1,
                    Name = "Heitor Oliveira Gonçalves",
                    Address = "Avenida General Marciano Magalhães",
                    Email = "heitorhog@gmail.com",
                    PhoneNumber = "24992196805",
                    PostalCode = "25630405"
                });

            // Assuming a valid result
            var validationResult = new ValidationResult();
            mockValidator.Setup(v => v.ValidateAsync(user)).ReturnsAsync(validationResult);

            var service = new UserService(mockRepo.Object, mockValidator.Object); // Pass the mock validator to UserService constructor

            // Act
            var result = await service.UpdateUserAsync(user);

            // Assert
            result.Should().BeEquivalentTo(new User
            {
                Id = 1,
                Name = "Heitor Oliveira Gonçalves",
                Address = "Avenida General Marciano Magalhães",
                Email = "heitorhog@gmail.com",
                PhoneNumber = "24992196805",
                PostalCode = "25630405"
            });
            mockRepo.Verify(repo => repo.UpdateUserAsync(user), Times.Once());
        }
    }

}

