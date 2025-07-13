
using Catalog.API.Products.UpdateProduct;
using JasperFx.Events.Daemon;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductComamnd(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductComamndValidator : AbstractValidator<DeleteProductComamnd>
    {
        public DeleteProductComamndValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }

    public class DeleteProductHandler(IDocumentSession session) : ICommandHandler<DeleteProductComamnd, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductComamnd command, CancellationToken cancellationToken)
        {
            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
