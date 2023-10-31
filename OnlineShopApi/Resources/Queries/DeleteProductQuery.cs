using MediatR;

namespace OnlineShopApi.Resources.Queries
{
    public class DeleteProductQuery: IRequest<bool>
    {
        public string ProductId { get; set; }
    }
}
