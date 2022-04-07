using System;
using System.Collections.Generic;
using System.ComponentModel;
using FrodoAPI.Domain;

namespace FrodoAPI.JourneyRepository
{
    public interface IJourneyRepository
    {
        public Guid AddJourney(Journey journey);

        public Journey GetJourney(Guid id);
    }

    class JourneyRepository : IJourneyRepository
    {
        // add item to dictionary
        private Dictionary<Guid, Journey> _repo;
        public Guid AddJourney(Journey journey)
        {
            var id = new Guid();
            _repo[id] = journey;
            return id;
        }

        public Journey GetJourney (Guid id)
        {
            return _repo[id];
        }
    }
}