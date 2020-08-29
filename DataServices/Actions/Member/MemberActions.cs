using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServices.Interface;
using DataServices.Model;
using Messages;
using Messages.Const;
using Newtonsoft.Json;

namespace DataServices.Services
{
    public interface IMemberActions
    {
        public Task<MessageResponse> Execute(string action, string request);
        public void AddAction(string key, Func<string, IMember, Task<MessageResponse>> func);
    }
    public class MemberActions : IMemberActions
    {
        private readonly IMember _member;

        public MemberActions(IMember member)
        {
            _member = member;
        }
        private readonly Dictionary<string, Func<string, IMember, Task<MessageResponse>>>
            Action = new Dictionary<string, Func<string, IMember, Task<MessageResponse>>>
        {
            { HttpAction.Get, Get },
            { HttpAction.GetAll, GetAll },
            { HttpAction.Create, Create },
            { HttpAction.Update, Update },
            { HttpAction.Delete, Delete }

        };

        private static readonly Func<string, IMember, Task<MessageResponse>>
            Get = async (request, memberRes) =>
        {
            var r = JsonConvert.DeserializeObject<GetMemberRequest>(request);
            var result = await memberRes.GetMemberHashCache(r.Data.Id);
            var member = new Member();
            if (result.IsNullOrEmpty)
            {
                member = await memberRes.GetMember(r.Data.Id);
                if (member == null)
                {
                    return new GetMemberResponse();
                }
                await memberRes.CreateMemberCache(member);
            }
            else
            {
                member = JsonConvert.DeserializeObject<Member>(result);
            }


            return new GetMemberResponse
            {
                Valid = true,
                Member = new MemberModel
                {
                    Id = member.Id,
                    Username = member.Identity.FirstName,
                }
            };
        };

        private static readonly Func<string, IMember, Task<MessageResponse>>
            GetAll = async (request, memberRes) =>
        {
            var r = JsonConvert.DeserializeObject<GetMembersRequest>(request);
            var member = await memberRes.GetMembers();

            return new GetMembersResponse
            {
                Valid = true,
                Members = member.Select(mm => new MemberModel
                {
                    Id = mm.Id,
                    Username = mm.Identity.FirstName,
                }).ToArray()
            };
        };

        private static readonly Func<string, IMember, Task<MessageResponse>>
            Update = async (request, memberRes) =>
        {
            var r = JsonConvert.DeserializeObject<CreateUpdateMemberRequest>(request);
            if (r.Data.Id == null)
            {
                return new GetMemberResponse();
            }

            var member = await memberRes.GetMember(r.Data.Id.Value);
            if (member == null)
            {
                return new GetMemberResponse();
            }

            member.Identity.FirstName = r.Data.Username;
            var games = r.Data.Games.Select(g => new Game
            {
                Id = Guid.NewGuid(),
                Code = g.Code,
                Name = g.Name
            }).ToHashSet();

            await memberRes.UpdateMember(member, games);
            await memberRes.UpdateMemberCache(member);

            return new MessageResponse
            {
                Valid = true
            };
        };

        private static readonly Func<string, IMember, Task<MessageResponse>>
            Delete = async (request, memberRes) =>
        {
            var r = JsonConvert.DeserializeObject<DeleteMemberRequest>(request);
            var member = await memberRes.GetMember(r.Data.Id);
            if (member == null)
            {
                return new MessageResponse();
            }
            await memberRes.DeleteMember(r.Data.Id);
            await memberRes.DeleteMemberCache(r.Data.Id);

            return new MessageResponse
            {
                Valid = true
            };
        };

        private static readonly Func<string, IMember, Task<MessageResponse>>
            Create = async (request, memberRes) =>
        {
            var r = JsonConvert.DeserializeObject<CreateUpdateMemberRequest>(request);
            var member = new Member
            {
                Identity = new AppUser
                {
                    FirstName = r.Data.Username
                },
            };
            await memberRes.CreateMember(member);
            await memberRes.CreateMemberCache(member);

            return new MessageResponse
            {
                Valid = true
            };
        };

        void IMemberActions.AddAction(string key, Func<string, IMember, Task<MessageResponse>> func) =>
            Action.Add(key, func);

        Task<MessageResponse> IMemberActions.Execute(string action, string request)
        {
            return Action[action].Invoke(request, _member);
        }
    }
}
