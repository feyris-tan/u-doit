﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATG__EMERGENCY_PLAN")]
	[CategoryDisplayName("Emergency Plan Assignment")]
	class EmergencyPlanAssignment : Category
	{
		public string title;

		public enum C__CATG__EMERGENCY_PLAN_OBJ_EMERGENCY_PLAN : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int emergency_plan;
		public string time_needed;
		public string practice_date;
		public string description;
	}
}
