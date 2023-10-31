using MediatR;

namespace OnlineShopApi.Resources.Queries
{
    public class AddToCartCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
