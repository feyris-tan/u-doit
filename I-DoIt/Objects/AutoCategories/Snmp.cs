﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATG__SNMP")]
	[CategoryDisplayName("Snmp")]
	class Snmp : Category
	{

		public enum C__CATG__SNMP_COMMUNITY : int
		{
			C__SNMP_COMMUNITY__PUBLIC = 1,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public C__CATG__SNMP_COMMUNITY title;
		public string description;
	}
}