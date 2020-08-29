using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServices.Db;
using DataServices.Model;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace DataServices.Services
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
    public class MemberServices : IMember
    {
        private readonly IRepository _repository;
        private readonly INewRedisRepository _newRedisRepository;

        private static readonly string prefix = "Members";

        public MemberServices(IRepository repository, INewRedisRepository newRedisRepository)
        {
            _repository = repository;
            _newRedisRepository = newRedisRepository;
        }
        Task IMember.CreateMember(Member member)
        {
            _repository.Add(member);
            return _repository.SaveChangesAsync();
        }
        Task IMember.CreateMember(Guid id, string location)
        {
            _repository.Members.AddAsync(new Member { IdentityId = id, Location = location });
            return _repository.SaveChangesAsync();
        }

        Task IMember.DeleteMember(int id)
        {
            var member = _repository.Members.FirstOrDefault(m => m.Id == id);
            _repository.Delete(member);
            return _repository.SaveChangesAsync();
        }

        Task<Member> IMember.GetMember(int id)
        {
            return _repository.Members.FirstOrDefaultAsync(m => m.Id == id);
        }

        Task<Member[]> IMember.GetMembers()
        {
            return _repository.Members.AsNoTracking().ToArrayAsync();
        }

        Task IMember.UpdateMember(Member member, HashSet<Game> games)
        {
            //foreach (var game in member.Games)
            //{
            //    _repository.Entry(game).State = EntityState.Deleted;

            //}
            //foreach (var game in games)
            //{
            //    member.Games.Add(game);
            //}

            _repository.Entry(member).State = EntityState.Modified;
            return _repository.SaveChangesAsync();
        }

        Task IMember.GetGames(int id)
        {
            return _repository.Members.AsNoTracking().ToArrayAsync();
        }

        Task<RedisValue> IMember.GetMemberCache(int id)
        {
            return _newRedisRepository.GetAsync<Member>(id.ToString());
        }

        Task IMember.UpdateMemberCache(Member member)
        {
            return _newRedisRepository.UpdateAsync(prefix, member.Id.ToString(), member);
        }

        Task IMember.DeleteMemberCache(int id)
        {
            return _newRedisRepository.DeleteHashAsync(prefix, id.ToString());
        }

        Task IMember.CreateMemberCache(Member member)
        {
            return _newRedisRepository.AddHashAsync(prefix, member.Id.ToString(), member);
        }

        Task<RedisValue> IMember.GetMemberHashCache(int id)
        {
            _newRedisRepository.ListRightPushAsync("history", id.ToString());
            return _newRedisRepository.GetHashAsync<Member>(prefix, id.ToString());
        }
    }
}
