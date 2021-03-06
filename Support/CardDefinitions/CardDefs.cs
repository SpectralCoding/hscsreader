﻿// <copyright file="CardDefs.cs" company="SpectralCoding.com">
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

namespace HSCSReader.Support.CardDefinitions {
	public static class CardDefs {
		public static readonly Dictionary<String, Card> Cards = new Dictionary<String, Card>();

		/// <summary>
		/// Loads Card Definitions from a CardDefs.xml file.
		/// </summary>
		/// <param name="filePath">Path to CardDefs.xml</param>
		public static void Load(String filePath) {
			XmlDocument cardsDoc = new XmlDocument();
			cardsDoc.Load(filePath);
			//XmlNode rootNode = cardsDoc.SelectSingleNode("/CardDefs");
			XmlNodeList entityNodeList = cardsDoc.SelectNodes("/CardDefs/Entity");
			if (entityNodeList != null) {
				foreach (XmlNode curEntityNode in entityNodeList) {
					String value = curEntityNode.Attributes?["CardID"].Value;
					if (value != null) {
						Cards.Add(value, new Card(curEntityNode));
					} else {
						throw new NotSupportedException();
					}
				}
			}
			//foreach (KeyValuePair<String, Card> curCard in Cards) {
			//	Console.WriteLine(curCard.Value.ShortDescription);
			//}
		}
	}
}
