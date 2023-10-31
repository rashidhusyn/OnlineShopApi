using MediatR;

namespace OnlineShopApi.Resources.Queries
{
    public class UpdateCartItemCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
