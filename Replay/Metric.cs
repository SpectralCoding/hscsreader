﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSCSReader.Support.Enumerations;

namespace HSCSReader.Replay {
	public class Metric {
		public String Name;
		public MetricType MetricType;
		public List<Int32> Values = new List<Int32>();

		public Metric(String name, MetricType metricType, Int32 value) {
			Name = name;
			MetricType = metricType;
			Values.Add(value);
		}
	}
}
