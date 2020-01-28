﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibraryApi.Domain
{
    public class Reservation
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ReservationStatus {  Pending, Approved, Cancelled }
        public int Id { get; set; }
        public string For { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime ReservationCreated { get; set; }
        public string Books { get; set; }

    }
}
