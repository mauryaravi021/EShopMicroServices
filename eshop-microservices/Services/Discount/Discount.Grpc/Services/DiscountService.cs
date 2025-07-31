using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CoupanModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
           var coupan = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
           coupan ??= new Coupon { Id = 0, ProductName = "Dummy Coupan", Amount = 100, Description = "Dummy Coupan" };
           var coupanModel = coupan.Adapt<CoupanModel>();
           return coupanModel;
        }

        public override async Task<CoupanModel> CreateDiscount(CreteDiscountRequest request, ServerCallContext context)
        {
            var coupan = request.Coupan.Adapt<Coupon>();
            if (coupan == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request Object" + request));
            dbContext.Coupons.Add(coupan);
            await dbContext.SaveChangesAsync();
            var coupanModel = coupan.Adapt<CoupanModel>();
            return coupanModel;

        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupan = await dbContext.Coupons.FirstOrDefaultAsync(x=>x.ProductName == request.ProductName);
            if(coupan == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Invalid Request Object" + request));
            dbContext.Remove(coupan);
            await dbContext.SaveChangesAsync();
            return new DeleteDiscountResponse { Success = true };
        }

        public override async Task<CoupanModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupan = request.Coupan.Adapt<Coupon>();
            if (coupan == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request Object" + request));
            dbContext.Coupons.Update(coupan);
            await dbContext.SaveChangesAsync();
            var coupanModel = coupan.Adapt<CoupanModel>();
            return coupanModel;
        }
    }
}
