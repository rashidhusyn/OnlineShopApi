using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class UpdateCategoryQueryHandler : IRequestHandler<UpdateCategoryQuery, bool>
    {
        private readonly IMongoCollection<Categorie> _categoryCollection;

        public UpdateCategoryQueryHandler(IMongoDatabase database)
        {
            _categoryCollection = database.GetCollection<Categorie>("Categories");
        }

        public async Task<bool> Handle(UpdateCategoryQuery request, CancellationToken cancellationToken)
        {
            var categoryId = request.CategoryId;
            var updatedCategory = request.UpdatedCategory;
            var existingCategory = await _categoryCollection
               .Find(p => p.Id == categoryId)
               .FirstOrDefaultAsync(cancellationToken);
            if (existingCategory == null)
            {
                return false;
            }
            if (updatedCategory.Title != null)
            {
                existingCategory.Title = updatedCategory.Title;
            }
            if (updatedCategory.Descrtiption != null)
            {
                existingCategory.Descrtiption = updatedCategory.Descrtiption;
            }
            var updateResult = await _categoryCollection.ReplaceOneAsync(
                p => p.Id == categoryId,
                existingCategory,
                cancellationToken: cancellationToken
            );

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;



        }

    }
}
