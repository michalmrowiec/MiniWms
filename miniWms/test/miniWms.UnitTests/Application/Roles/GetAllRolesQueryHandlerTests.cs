using miniWms.Application.Contracts;
using miniWms.Application.Functions.Roles.Queries.GetAllRoles;
using miniWms.Domain.Entities;

namespace miniWms.UnitTests.Application.Roles
{
    public class GetAllRolesQueryHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new List<Role> ()
                {
                    new()
                    {
                        RoleId = "ope",
                        RoleName = "Operator",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2022, 11, 13),
                        ModifiedAt = new DateTime(2023, 12, 23)
                    },
                    new()
                    {
                        RoleId = "mng",
                        RoleName = "Manager",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2022, 11, 13),
                        ModifiedAt = new DateTime(2022, 12, 23)
                    },
                    new()
                    {
                        RoleId = "adm",
                        RoleName = "Admin",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2022, 11, 13),
                        ModifiedAt = new DateTime(2022, 12, 23)
                    }
                }
            },
            new object[]
            {
                new List<Role> ()
            },
            new object[]
            {
                new List<Role> ()
                {
                    new()
                    {
                        RoleId = "adm",
                        RoleName = "Admin",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2022, 11, 13),
                        ModifiedAt = new DateTime(2023, 12, 23)
                    }
                }
            }

        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task GetAllRolesQueryHandler_ForValidData_ReturnsSuccedWithListOfRoles(List<Role> roles)
        {
            var repo = new Mock<IRoleRepository>();
            repo.Setup(m => m.GetAllRolesAsync())
                .ReturnsAsync(roles);

            GetAllRolesQueryHandler handler = new(repo.Object);

            var response = await handler.Handle(new GetAllRolesQuery(), new CancellationToken());

            response.Should().NotBeNull().And.BeEquivalentTo(roles);
        }
    }
}
