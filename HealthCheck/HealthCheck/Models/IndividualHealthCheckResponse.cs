﻿using System;

namespace HealthCheck.Models
{
    public class IndividualHealthCheckResponse
    {
        public string Status { get; set; }
        public string Components { get; set; }
        public string Description { get; set; }
    }
}