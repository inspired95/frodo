using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using FrodoAPI.Domain;

namespace FrodoAPI.JourneyRepository
{
    public interface IJourneyRepository
    {
        Guid AddJourney(Journey journey);

        Journey GetJourney(Guid id);
        Guid Add(object journey);
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
            return _repo[id];
        }

        public Guid Add(object journey)
        {
            throw new NotImplementedException();
        }
    }
}