﻿// <copyright file="ChoicesNode.cs" company="SpectralCoding.com">
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
using System.Xml;
using HSCSReader.Support;
using HSCSReader.Support.Enumerations;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Replay.LogNodes {
	internal class ChoicesNode : LogNode {
		private readonly Game _game;
		public readonly List<LogNode> Children = new List<LogNode>();
		public readonly Int32 Entity;
		public readonly Int32 Max;
		public readonly Int32 Min;
		public readonly Int32 PlayerId;
		public readonly Int32 Source;
		public readonly String Ts;
		public readonly ChoiceType Type;

		/// <summary>
		/// Initializes an instance of the ChoicesNode class.
		/// </summary>
		/// <param name="xmlNode">The XML Node describing the Node.</param>
		/// <param name="game">The game object related to the Node.</param>
		public ChoicesNode(XmlNode xmlNode, Game game) {
			// entity % entity; #REQUIRED
			// playerID NMTOKEN #REQUIRED
			// source NMTOKEN #REQUIRED
			// type NMTOKEN #REQUIRED
			// min NMTOKEN #IMPLIED
			// max NMTOKEN #IMPLIED
			// ts NMTOKEN #IMPLIED
			_game = game;
			Int32.TryParse(xmlNode.Attributes?["entity"]?.Value, out Entity);
			Int32.TryParse(xmlNode.Attributes?["playerID"]?.Value, out PlayerId);
			Int32.TryParse(xmlNode.Attributes?["source"]?.Value, out Source);
			if (xmlNode.Attributes?["type"]?.Value == null) { throw new NullReferenceException(); }
			Type = (ChoiceType)Enum.Parse(typeof(ChoiceType), xmlNode.Attributes?["type"]?.Value);
			Int32.TryParse(xmlNode.Attributes?["min"]?.Value, out Min);
			Int32.TryParse(xmlNode.Attributes?["max"]?.Value, out Max);
			Ts = xmlNode.Attributes?["ts"]?.Value;
			foreach (XmlNode childNode in xmlNode.ChildNodes) {
				Children.Add(NodeImporter.Import(childNode, game));
			}
		}

		/// <summary>
		/// Processes this node, deriving whatever information it can.
		/// </summary>
		public override void Process() {
			if (Type == ChoiceType.MULLIGAN) {
				foreach (LogNode curLogNode in Children) {
					if (curLogNode.GetType() == typeof(ChoiceNode)) {
						ChoiceNode curChoiceNode = (ChoiceNode)curLogNode;
						Helpers.IntegrateMetrics(
												 new List<Metric> {new Metric("COUNT_MULLIGAN_OPPORTUNITY", MetricType.AddToValue, 1)},
												_game.ActorStates[curChoiceNode.Entity].Metrics);
					}
				}
			}
		}
	}
}
