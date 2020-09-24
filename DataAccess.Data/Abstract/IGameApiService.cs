using DataAccess.Model.SharedModels;
using System;
using System.Threading.Tasks;

namespace DataAccess.Data.Abstract
{
    public interface IGameApiService
    {
        public Task<GameStatusResponse> GetCurrentStatus();
        public Task<JoinResponse> JoinCurrentRound(string teamId);
    }
}