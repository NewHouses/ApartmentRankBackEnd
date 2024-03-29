﻿using System.Text.Json;

namespace ApartmentRank.Domain.Entities
{
    public class ApartmentRankResponse
    {
        public Apartment[] apartments { get; set; }
        public int total { get; set; }

        public ApartmentRankResponse(Apartment[] apartments, int total) 
        {
            this.apartments = apartments;
            this.total = total;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
