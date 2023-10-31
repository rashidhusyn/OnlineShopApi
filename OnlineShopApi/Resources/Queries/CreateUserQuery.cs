using MediatR;

namespace OnlineShopApi.Resources.Queries
{
    public class CreateUserQuery : IRequest<string>
    {
        public string Name { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
