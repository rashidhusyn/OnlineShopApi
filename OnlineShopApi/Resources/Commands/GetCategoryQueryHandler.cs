using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Categorie>
    {
        private readonly IMongoCollection<Categorie> _categoryCollection;

        public GetCategoryQueryHandler(IMongoDatabase database)
        {
            _categoryCollection = database.GetCollection<Categorie>("Categories");
        }

        public async Task<Categorie> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var categoryId = request.CategoryId;
            var category = await _categoryCollection
                .Find(c => c.Id == categoryId)
                .FirstOrDefaultAsync(cancellationToken);

            return category;
        }

    }
}
