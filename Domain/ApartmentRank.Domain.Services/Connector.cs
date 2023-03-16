using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Interfaces;
using ApartmentRank.Domain.Services.Interfaces;

namespace ApartmentRank.Domain.Services
{
    public class Connector : IConnector
    {
        private readonly IAdapterFactory adapterFactory;
        public Connector(IAdapterFactory adapterFactory)
        {
            this.adapterFactory = adapterFactory;
        }

        public string TransformRequest(ApartmentRankRequest apartmentRankRequest)
        {
            var requestAdapter = GetRequestAdapter(apartmentRankRequest);
            return TransformToApiRequest(requestAdapter);
        }

        public ApartmentRankResponse TransformResponse(string[] apiResponse)
        {
            var responseAdapter = GetResponseAdapter(apiResponse);
            return TransformToApartmentRankResponse(responseAdapter);
        }

        private IRequestAdapter GetRequestAdapter(ApartmentRankRequest apartmentRankRequest)
        {
            return adapterFactory.CreateRequestAdapter(apartmentRankRequest);
        }

        private static string TransformToApiRequest(IRequestAdapter requestAdapter)
        {
            return requestAdapter.ToJson();
        }

        private IResponseAdapter GetResponseAdapter(string[] apiResponse)
        {
            return adapterFactory.CreateResponseAdapter(apiResponse);
        }

        private static ApartmentRankResponse TransformToApartmentRankResponse(IResponseAdapter responseAdapter)
        {
            return responseAdapter.Convert();
        }
    }
}
