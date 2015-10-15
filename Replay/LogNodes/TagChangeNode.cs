﻿// <copyright file="TagChange.cs" company="SpectralCoding.com">
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
using HSCSReader.Replay.EntityStates;
using HSCSReader.Support;
using HSCSReader.Support.Enumerations;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Replay.LogNodes {
	internal class TagChangeNode : LogNode {
		private Game _game;
		public Int32 Entity;
		public GameTag Tag;
		public Int32 Value;
		public String Ts;

		public TagChangeNode(XmlNode xmlNode, Game game) {
			// entity % entity; #REQUIRED
			// tag % gameTag; #REQUIRED
			// value NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			_game = game;
			Int32.TryParse(xmlNode.Attributes?["entity"]?.Value, out Entity);
			if (xmlNode.Attributes?["tag"]?.Value == null) { throw new NullReferenceException(); }
			Tag = (GameTag)Enum.Parse(typeof(GameTag), xmlNode.Attributes?["tag"]?.Value);
			Int32.TryParse(xmlNode.Attributes?["value"]?.Value, out Value);
			Ts = xmlNode.Attributes?["ts"]?.Value;
		}

		public override void Process() {
			if (Entity == 0) {
				// What?! Invalid Replay?
				// Hopefully we can just ignore this tag change?
				// Maybe we should discard the whole game?
				return;
			}
			if (!_game.ActorStates[Entity].Tags.ContainsKey(Tag)) {
				// I'm not sure if this is correct. It might come back to bite me later.
				// We're saying any unset tag is actually set to -1.
				_game.ActorStates[Entity].Tags.Add(Tag, -1);
			}
			Int32 oldValue = _game.ActorStates[Entity].Tags[Tag];
			_game.ActorStates[Entity].Tags[Tag] = Value;
			List<Metric> newMetrics = new List<Metric>();
			switch (Tag) {
				case GameTag.NUM_TURNS_IN_PLAY:
					newMetrics.Add(new Metric($"VALUE.LATEST.NUM_TURNS_IN_PLAY", MetricType.Overwrite, Value));
					newMetrics.Add(new Metric($"MAX_NUM_TURNS_IN_PLAY", MetricType.OverwriteMax, Value));
					break;
				case GameTag.ATK:
					newMetrics.Add(new Metric($"COUNT_ATK." + Value, MetricType.AddToValue, 1));
					newMetrics.Add(new Metric($"MAX_ATK", MetricType.OverwriteMax, Value));
					break;
				case GameTag.ZONE_POSITION:
					newMetrics.Add(new Metric($"COUNT_ZONE_POSITION." + Value, MetricType.AddToValue, 1));
					break;
				case GameTag.ZONE:
					if ((Enum.IsDefined(typeof(Zone), oldValue)) && (Enum.IsDefined(typeof(Zone), Value))) {
                        newMetrics.Add(new Metric($"COUNT_ZONE_" + ((Zone)oldValue) + "_TO_" + ((Zone)Value) + "." + _game.ActorStates[1].Tags[GameTag.TURN], MetricType.AddToValue, 1));
					} else if (oldValue == -1) {
						newMetrics.Add(new Metric($"COUNT_SEEN", MetricType.AddToValue, 1));
					}
					break;
				case GameTag.DAMAGE:
					newMetrics.Add(new Metric($"COUNT_DAMAGE." + Value, MetricType.AddToValue, 1));
					newMetrics.Add(new Metric($"MAX_DAMAGE", MetricType.OverwriteMax, Value));
					break;
				case GameTag.ARMOR:
					break;
			}
			Helpers.IntegrateMetrics(newMetrics, _game.ActorStates[Entity].Metrics);
		}
	}
}