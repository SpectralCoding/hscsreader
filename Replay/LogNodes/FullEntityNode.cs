﻿// <copyright file="FullEntityNode.cs" company="SpectralCoding.com">
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

namespace HSCSReader.Replay.LogNodes {
	internal class FullEntityNode : LogNode {
		private readonly Game _game;
		public readonly String CardId;
		public readonly List<LogNode> Children = new List<LogNode>();
		public readonly Int32 Id;
		public readonly String Ts;

		/// <summary>
		/// Initializes an instance of the FullEntityNode class.
		/// </summary>
		/// <param name="xmlNode">The XML Node describing the Node.</param>
		/// <param name="game">The game object related to the Node.</param>
		public FullEntityNode(XmlNode xmlNode, Game game) {
			// cardID NMTOKEN #IMPLIED
			// id % gameTag; #REQUIRED
			// ts NMTOKEN #IMPLIED
			_game = game;
			CardId = xmlNode.Attributes?["cardID"]?.Value;
			Int32.TryParse(xmlNode.Attributes?["id"]?.Value, out Id);
			Ts = xmlNode.Attributes?["ts"]?.Value;
			foreach (XmlNode childNode in xmlNode.ChildNodes) {
				Children.Add(NodeImporter.Import(childNode, game));
			}
		}

		/// <summary>
		/// Processes this node, deriving whatever information it can.
		/// </summary>
		public override void Process() {
			FullEntityState tempState = new FullEntityState {
																Id = Id,
																CardId = CardId,
																Ts = Ts
															};
			if (_game.ActorStates.ContainsKey(Id)) {
				if (_game.ActorStates[Id].GetType() == typeof(FullEntityState)) {
					FullEntityState curState = (FullEntityState)_game.ActorStates[Id];
					if ((curState.Ts == null) && (Ts != null)) { curState.Ts = Ts; }
					if ((curState.CardId == null) && (CardId != null)) { curState.CardId = CardId; }
				}
			} else {
				_game.ActorStates.Add(Id, tempState);
			}
			List<Metric> newMetrics = new List<Metric>() {
															new Metric("COUNT_SEEN", MetricType.AddToValue, 1)
														};
			Helpers.IntegrateMetrics(newMetrics, _game.ActorStates[Id].Metrics);
			foreach (LogNode curLogNode in Children) {
				if (curLogNode.GetType() == typeof(TagNode)) {
					((TagNode)curLogNode).Process(Id);
				} else {
					throw new NotSupportedException();
				}
			}
		}
	}
}
