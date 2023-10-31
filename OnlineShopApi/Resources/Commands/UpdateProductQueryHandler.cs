using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class UpdateProductQueryHandler : IRequestHandler<UpdateProductQuery, bool>
    {
        private readonly IMongoCollection<Product> _productCollection;

        public UpdateProductQueryHandler(IMongoDatabase database)
        {
            _productCollection = database.GetCollection<Product>("Products");
        }

        public async Task<bool> Handle(UpdateProductQuery request, CancellationToken cancellationToken)
        {
            var productId = request.ProductId;
            var updatedProduct = request.UpdatedProduct;

            var existingProduct = await _productCollection
                .Find(p => p.Id == productId)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingProduct == null)
            {
                return false; 
            }

            if (updatedProduct.Title != null)
            {
                existingProduct.Title = updatedProduct.Title;
            }

            if (updatedProduct.Description != null)
            {
                existingProduct.Description = updatedProduct.Description;
            }

            if (updatedProduct.Image != null)
            {
                existingProduct.Image = updatedProduct.Image;
            }

            if (updatedProduct.Rating != 0)
            {
                existingProduct.Rating = updatedProduct.Rating;
            }

            if (updatedProduct.Price != 0)
            {
                existingProduct.Price = updatedProduct.Price;
            }

            if (updatedProduct.CategoryId != null)
            {
                existingProduct.CategoryId = updatedProduct.CategoryId;
            }

            var updateResult = await _productCollection.ReplaceOneAsync(
                p => p.Id == productId,
                existingProduct,
                cancellationToken: cancellationToken
            );

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

    }
}
