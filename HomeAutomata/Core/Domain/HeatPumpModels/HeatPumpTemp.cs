﻿using System;

namespace HomeAutomata.Core.Domain.HeatPumpModels
{
    public class HeatPumpTemp : BaseEntity
    {
        public DateTime Date { get; set; }
        public double Kwh { get; set; }
    }
}