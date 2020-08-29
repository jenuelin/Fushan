using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataServices.Model;
using StackExchange.Redis;

namespace DataServices.Interface
{
    public interface IMember
    {
        Task<Member> GetMember(int id);
        Task<Member[]> GetMembers();

        Task CreateMember(Member member);
        Task CreateMember(Guid id, string location);

        Task UpdateMember(Member request, HashSet<Game> games);

        Task DeleteMember(int id);

        Task GetGames(int id);

        Task<RedisValue> GetMemberCache(int id);
        Task<RedisValue> GetMemberHashCache(int id);
        Task UpdateMemberCache(Member member);
        Task DeleteMemberCache(int id);

        Task CreateMemberCache(Member member);
    }
}
