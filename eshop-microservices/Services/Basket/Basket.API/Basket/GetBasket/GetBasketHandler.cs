using Basket.API.Models;
using BuildingBlocks.CQRS;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketRresult>;
    public record GetBasketRresult(ShoppingCart Cart);
    public class GetBasketHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketRresult>
    {
        public async Task<GetBasketRresult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            return new GetBasketRresult(await basketRepository.GetBasket(query.UserName, cancellationToken));
        }
    }
}
