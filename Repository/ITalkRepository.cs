using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interview.Repository
{
    public interface ITalkRepository
    {
        Task<List<TalkEntity>> GetTalks(int conventionId);
        Task<TalkEntity> GetTalk(int talkId);
        Task<TalkEntity> RegisterTalk(int conventionId, Talk talk);
        Task<bool> ReserveSeat(int talkId, int userId);
    }
}
