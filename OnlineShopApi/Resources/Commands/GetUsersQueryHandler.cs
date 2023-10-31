using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Models;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        private readonly IMongoCollection<User> _userCollection;

        public GetUsersQueryHandler(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<User>("Users");
        }

        public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userCollection
                .Find(_ => true) 
                .ToListAsync();

            return users.Select(user => new User
            {
                
                Id = user.Id,
                Name = user.Name,
                UserName = user.Name,
                Email = user.Email,
                Password = user.Password,
               
            });
        }

    }
}
