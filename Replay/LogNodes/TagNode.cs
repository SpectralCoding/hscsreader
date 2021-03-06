﻿// <copyright file="TagNode.cs" company="SpectralCoding.com">
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
using System.Xml;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Replay.LogNodes {
	internal class TagNode : LogNode {
		private readonly Game _game;
		public readonly GameTag Name;
		public readonly String Ts;
		public readonly Int32 Value;

		/// <summary>
		/// Initializes an instance of the TagNode class.
		/// </summary>
		/// <param name="xmlNode">The XML Node describing the Node.</param>
		/// <param name="game">The game object related to the Node.</param>
		public TagNode(XmlNode xmlNode, Game game) {
			// tag % gameTag; #REQUIRED
			// value NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			_game = game;
			if (xmlNode.Attributes?["tag"]?.Value == null) { throw new NullReferenceException(); }
			Name = (GameTag)Enum.Parse(typeof(GameTag), xmlNode.Attributes?["tag"]?.Value);
			Int32.TryParse(xmlNode.Attributes?["value"]?.Value, out Value);
			Ts = xmlNode.Attributes?["ts"]?.Value;
		}

		/// <summary>
		/// Processes this node, deriving whatever information it can.
		/// </summary>
		public override void Process() { }

		/// <summary>
		/// Processes this node, deriving whatever information it can.
		/// </summary>
		public void Process(Int32 id) {
			Process();
			if (_game.ActorStates[id].Tags.ContainsKey(Name)) {
				_game.ActorStates[id].Tags[Name] = Value;
			} else {
				_game.ActorStates[id].Tags.Add(Name, Value);
			}
		}
	}
}
