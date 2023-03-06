﻿using ApartmentRank.Domain.Entities.Idealista;
using ApartmentRank.Domain.ValueObjects;
using System.Text.Json;

namespace ApartmentRank.Domain.Entities
{
    public class ApartmentRankRequest
    {
        public IEnumerable<Preference> preferences { get; set; }

        public static ApartmentRankRequest FromJson(string request)
        {
            return JsonSerializer.Deserialize<ApartmentRankRequest>(request);
        }
    }
}