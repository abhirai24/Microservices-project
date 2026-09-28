using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Google.Rpc;
using Grpc.Core;
using GoogleStatus = Google.Rpc.Status;
using GrpcStatus = Grpc.Core.Status;

namespace Discount.Extensions
{
    public static class GrpcErrorHelper
    {
        public static RpcException CreateValidationException(Dictionary<string, string> fieldErrors)
        {
            var fieldViolatiions = new List<BadRequest.Types.FieldViolation>();

            foreach(var error in fieldErrors)
            {
                fieldViolatiions.Add(new BadRequest.Types.FieldViolation
                {
                    Field = error.Key,
                    Description = error.Value
                });
            }

            // now add bbad request
            var badRequest = new BadRequest();
            badRequest.FieldViolations.AddRange(fieldViolatiions);
            var status = new GoogleStatus
            {
                Code = (int)StatusCode.InvalidArgument,
                Message = "Validation Failed",
                Details = { Any.Pack(badRequest) }
            };

            var trailers = new Metadata
            {
                {"grpc-status-details-bin", status.ToByteArray() }
            };

            return new RpcException(new GrpcStatus(StatusCode.InvalidArgument, "Validation Errors"), trailers);
        }
    }
}
