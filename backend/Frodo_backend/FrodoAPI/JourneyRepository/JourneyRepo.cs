using System;
using System.Collections.Generic;
using System.ComponentModel;
using FrodoAPI.Domain;

namespace FrodoAPI.JourneyRepository
{
    public interface IJourneyRepository
    {
        Guid AddJourney(Journey journey);

        Journey GetJourney(Guid id);

    }

    class JourneyRepository : IJourneyRepository
    {
        // add item to dictionary
        private Dictionary<Guid, Journey> _repo = new Dictionary<Guid, Journey>();
        public Guid AddJourney(Journey journey)
        {
            var id = Guid.NewGuid();
            _repo[id] = journey;
            return id;
        }

        public Journey GetJourney (Guid id)
        {
            return _repo.ContainsKey(id) ? _repo[id] : null;
        }


    }
}