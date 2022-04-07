using System;
using System.Collections.Generic;
using System.Linq;

namespace FrodoAPI.UserRepository
{
    public interface IUserRepository
    {
        Guid AddUser(string firstName, string lastName);
        void AddJourney(Guid userId, Guid journey);

        IEnumerable<Guid> GetJourneys(Guid userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly Dictionary<Guid, Tuple<string, string>> _users = new Dictionary<Guid, Tuple<string, string>>();
        private readonly Dictionary<Guid, List<Guid>> _journeys = new Dictionary<Guid, List<Guid>>();

        public Guid AddUser(string firstName, string lastName)
        {
            var id = new Guid();
            _users.Add(id, Tuple.Create(firstName, lastName));

            return id;
        }

        public void AddJourney(Guid userId, Guid journey)
        {
            if (!_journeys.ContainsKey(userId))
                _journeys.Add(userId, new List<Guid>());

            _journeys[userId].Add(journey);
        }

        public IEnumerable<Guid> GetJourneys(Guid userId)
        {
            if (_journeys.ContainsKey(userId))
                return _journeys[userId];

            return Enumerable.Empty<Guid>();
        }
    }
}