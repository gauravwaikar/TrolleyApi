using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;
using TrolleyApi.Exercise2.Domain;

namespace TrolleyApi.Exercise2.Services
{
    public interface IShoppingHistoryRepository
    {
        [Get("/shopperHistory?token={token}")]
        public Task<List<ShopperHistoryResponse>> Get(string token);
    }
}
