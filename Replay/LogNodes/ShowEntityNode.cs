﻿// <copyright file="ShowEntityNode.cs" company="SpectralCoding.com">
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

namespace HSCSReader.Replay.LogNodes {
	internal class ShowEntityNode : LogNode {
		private readonly Game _game;
		public readonly String CardId;
		public readonly List<LogNode> Children = new List<LogNode>();
		public readonly Int32 Entity;
		public readonly String Ts;

		/// <summary>
		/// Initializes an instance of the ShowEntityNode class.
		/// </summary>
		/// <param name="xmlNode">The XML Node describing the Node.</param>
		/// <param name="game">The game object related to the Node.</param>
		public ShowEntityNode(XmlNode xmlNode, Game game) {
			// cardID NMTOKEN #IMPLIED
			// entity % entity; #REQUIRED
			// ts NMTOKEN #IMPLIED
			_game = game;
			CardId = xmlNode.Attributes?["cardID"]?.Value;
			Int32.TryParse(xmlNode.Attributes?["entity"]?.Value, out Entity);
			Ts = xmlNode.Attributes?["ts"]?.Value;
			foreach (XmlNode childNode in xmlNode.ChildNodes) {
				Children.Add(NodeImporter.Import(childNode, game));
			}
		}

		/// <summary>
		/// Processes this node, deriving whatever information it can.
		/// </summary>
		public override void Process() {
			if (_game.ActorStates.ContainsKey(Entity)) {
				if (_game.ActorStates[Entity].GetType() == typeof(FullEntityState)) {
					FullEntityState curState = (FullEntityState)_game.ActorStates[Entity];
					if ((curState.Ts == null) && (Ts != null)) { curState.Ts = Ts; }
					if ((curState.CardId == null) && (CardId != null)) { curState.CardId = CardId; }
				}
			} else {
				throw new NotSupportedException();
			}
			foreach (LogNode curLogNode in Children) {
				if (curLogNode.GetType() == typeof(TagNode)) {
					((TagNode)curLogNode).Process(Entity);
				} else {
					throw new NotSupportedException();
				}
			}
		}
	}
}
