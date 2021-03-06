﻿// <copyright file="OptionNode.cs" company="SpectralCoding.com">
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
	internal class OptionNode : LogNode {
		private readonly Game _game;
		public readonly Int32 Entity;
		public readonly Int32 Index;
		public readonly String Ts;
		public readonly OptionType Type;

		/// <summary>
		/// Initializes an instance of the OptionNode class.
		/// </summary>
		/// <param name="xmlNode">The XML Node describing the Node.</param>
		/// <param name="game">The game object related to the Node.</param>
		public OptionNode(XmlNode xmlNode, Game game) {
			// entity % entity; #IMPLIED
			// index NMTOKEN #REQUIRED
			// type NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			_game = game;
			Int32.TryParse(xmlNode.Attributes?["entity"]?.Value, out Entity);
			Int32.TryParse(xmlNode.Attributes?["index"]?.Value, out Index);
			if (xmlNode.Attributes?["type"]?.Value == null) { throw new NullReferenceException(); }
			Type = (OptionType)Enum.Parse(typeof(OptionType), xmlNode.Attributes?["type"]?.Value);
			Ts = xmlNode.Attributes?["ts"]?.Value;
		}

		/// <summary>
		/// Processes this node, deriving whatever information it can.
		/// </summary>
		public override void Process() { }
	}
}
