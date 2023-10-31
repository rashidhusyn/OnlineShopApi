using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryQuery, string>
    {
        private readonly IMongoCollection<Categorie> _categoryCollection;

        public CreateCategoryCommandHandler(IMongoDatabase database)
        {
            _categoryCollection = database.GetCollection<Categorie>("Categories");
        }

        public async Task<string> Handle(CreateCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = new Categorie
            {
                Title = request.Title,
                 Descrtiption= request.Description
            };

            await _categoryCollection.InsertOneAsync(category);

            return category.Id;
        }

    }
}
