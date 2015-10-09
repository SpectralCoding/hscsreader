﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HSCSReader.Replay {
	static class NodeProcessor {

		public static object Process(XmlNode xmlNode, Game game) {
			switch (xmlNode.Name) {
				case "Action": return Action(xmlNode, game);
				case "Choices": return Choices(xmlNode, game);
				case "FullEntity": return FullEntity(xmlNode, game);
				case "GameEntity": return GameEntity(xmlNode, game);
				case "HideEntity": return HideEntity(xmlNode, game);
				case "Info": return Info(xmlNode, game);
				case "MetaData": return MetaData(xmlNode, game);
				case "Options": return Options(xmlNode, game);
				case "Player": return Player(xmlNode, game);
				case "SendChoices": return SendChoices(xmlNode, game);
				case "SendOption": return SendOption(xmlNode, game);
				case "ShowEntity": return ShowEntity(xmlNode, game);
				case "TagChange": return TagChange(xmlNode, game);
				case "Tag": return Tag(xmlNode, game);
				case "Target": return Target(xmlNode, game);
				default: throw new NotImplementedException();
			}
		}

		private static Action Action(XmlNode xmlNode, Game game) {
			// entity % entity; #REQUIRED
			// index NMTOKEN #IMPLIED
			// target NMTOKEN #IMPLIED
			// type NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new Action(xmlNode, game);
		}

		private static Choices Choices(XmlNode xmlNode, Game game) {
			// entity % entity; #REQUIRED
			// playerID NMTOKEN #REQUIRED
			// source NMTOKEN #REQUIRED
			// type NMTOKEN #REQUIRED
			// min NMTOKEN #IMPLIED
			// max NMTOKEN #IMPLIED
			// ts NMTOKEN #IMPLIED
			return new Choices(xmlNode, game);
		}

		private static FullEntity FullEntity(XmlNode xmlNode, Game game) {
			// cardID NMTOKEN #IMPLIED
			// id % gameTag; #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new FullEntity(xmlNode, game);
		}

		private static GameEntity GameEntity(XmlNode xmlNode, Game game) {
			// id %entity; #REQUIRED
			return new GameEntity(xmlNode, game);
		}

		private static HideEntity HideEntity(XmlNode xmlNode, Game game) {
			// entity % entity; #REQUIRED
			// tag % gameTag; #REQUIRED
			// value NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new HideEntity(xmlNode, game);
		}

		private static MetaData MetaData(XmlNode xmlNode, Game game) {
			// meta NMTOKEN #REQUIRED
			// data % entity; #IMPLIED
			// info NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new MetaData(xmlNode, game);
		}

		private static Info Info(XmlNode xmlNode, Game game) {
			// index NMTOKEN #REQUIRED
			// id % entity; #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new Info(xmlNode, game);
		}

		private static Options Options(XmlNode xmlNode, Game game) {
			// id NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new Options(xmlNode, game);
		}

		private static Player Player(XmlNode xmlNode, Game game) {
			// id NMTOKEN #REQUIRED
			// playerID NMTOKEN #REQUIRED
			// name CDATA #IMPLIED
			// accountHi NMTOKEN #IMPLIED
			// accountLo NMTOKEN #IMPLIED
			// ts NMTOKEN #IMPLIED
			return new Player(xmlNode, game);
		}

		private static SendChoices SendChoices(XmlNode xmlNode, Game game) {
			// entity % entity; #REQUIRED
			// type NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new SendChoices(xmlNode, game);
		}
		private static SendOption SendOption(XmlNode xmlNode, Game game) {
			// option NMTOKEN #REQUIRED
			// subOption NMTOKEN #IMPLIED
			// position NMTOKEN #IMPLIED
			// target NMTOKEN #IMPLIED
			// ts NMTOKEN #IMPLIED
			return new SendOption(xmlNode, game);
		}
		private static ShowEntity ShowEntity(XmlNode xmlNode, Game game) {
			// cardID NMTOKEN #IMPLIED
			// entity % entity; #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new ShowEntity(xmlNode, game);
		}

		private static Tag Tag(XmlNode xmlNode, Game game) {
			// 	tag %gameTag; #REQUIRED
			//	value NMTOKEN #REQUIRED
			//	ts NMTOKEN #IMPLIED
			return new Tag(xmlNode, game);
		}

		private static TagChange TagChange(XmlNode xmlNode, Game game) {
			// entity % entity; #REQUIRED
			// tag % gameTag; #REQUIRED
			// value NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			//if (xmlNode.Attributes?["entity"].Value != "[UNKNOWN HUMAN PLAYER]") {
			//	Int32 entityId = Convert.ToInt32(xmlNode.Attributes?["entity"].Value);
			//	GameTag entityTag = (GameTag)Enum.Parse(typeof(GameTag), xmlNode.Attributes?["tag"].Value);
			//	Int32 newValue = Convert.ToInt32(xmlNode.Attributes?["value"].Value);
			//	Entities[entityId].ChangeOrAddTag(this, entityTag, newValue);
			//}
			return new TagChange(xmlNode, game);
		}

		private static Target Target(XmlNode xmlNode, Game game) {
			// entity %entity; #REQUIRED
			// index NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new Target(xmlNode, game);
		}

	}
}