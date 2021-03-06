﻿// <copyright file="Metric.cs" company="SpectralCoding.com">
//     Copyright (c) 2015 SpectralCoding
// </copyright>
// <license>
// This file is part of HSCSReader.
// 
// HSCSReader is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// HSCSReader is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with HSCSReader.  If not, see <http://www.gnu.org/licenses/>.
// </license>
// <author>Caesar Kabalan</author>

using System;
using System.Collections.Generic;
using HSCSReader.Support.Enumerations;

namespace HSCSReader.Replay {
	public class Metric {
		public readonly MetricType MetricType;
		public readonly String Name;
		public readonly List<Int32> Values = new List<Int32>();

		/// <summary>
		/// Initializes an instance of the Metric class.
		/// </summary>
		/// <param name="name">The name of the metrics.</param>
		/// <param name="metricType">The type of metric.</param>
		/// <param name="value">The value of the metric.</param>
		public Metric(String name, MetricType metricType, Int32 value) {
			Name = name;
			MetricType = metricType;
			Values.Add(value);
		}
	}
}
