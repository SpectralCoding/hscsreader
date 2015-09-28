﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSCSReader.Support.HSEnumerations {
	public enum GameType {
		GT_UNKNOWN = 0,
		GT_VS_AI = 1,
		GT_VS_FRIEND = 2,
		GT_TUTORIAL = 4,
		GT_ARENA = 5,
		GT_TEST = 6,
		GT_RANKED = 7,
		GT_UNRANKED = 8,
		GT_TAVERNBRAWL = 16,
		GT_LAST = 17
	}
}
