﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATG__MANUAL")]
	[CategoryDisplayName("Manual Assignment")]
	class ManualAssignment : Category
	{
		public string title;

		public enum C__CATG__MANUAL_OBJ_FILE : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int manual;
		public string description;
	}
}
