using DataAccess.Model;
using DataAccess.Model.SharedModels;
using System;
using System.Threading.Tasks;

namespace DataAccess.Data.Abstract
{
    public interface IGameApiService
    {
        public Task<GameStatusResponse> GetCurrentStatus();
        public Task<JoinResponse> JoinCurrentRound(string teamId);
        Task<bool> ValidRequest(string path, string username);
    }
}