using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class DeleteCategoryQueryHandler : IRequestHandler<DeleteCategoryQuery, bool>
    {
        private readonly IMongoCollection<Categorie> _categoryCollection;

        public DeleteCategoryQueryHandler(IMongoDatabase database)
        {
            _categoryCollection = database.GetCollection<Categorie>("Categories");
        }

        public async Task<bool> Handle(DeleteCategoryQuery request, CancellationToken cancellationToken)
        {
            var categoryId = request.CategoryId;

            var result = await _categoryCollection.DeleteOneAsync(c => c.Id == categoryId, cancellationToken);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

    }
}
