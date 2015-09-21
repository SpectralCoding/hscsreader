﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSCSReader.Support.Enumerations;

namespace HSCSReader.Replay {
	struct TagChange {
		public GameTag Tag;
		public Int32 OldValue;
		public Int32 NewValue;
		public String Timestamp;
		public Boolean IsNew;
	}
}
