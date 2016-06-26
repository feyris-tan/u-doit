﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATS__WAN")]
	[CategoryDisplayName("Wan")]
	class Wan : Category
	{

		public enum C__CATS__WAN__ROLE : int
		{
			C__WAN_ROLE__PRIMARY = 1,
			C__WAN_ROLE__BACKUP = 2,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int role;

		public enum C__CATS__WAN__TYPE : int
		{
			C__WAN_TYPE__NOT_KNOWN = 1,
			C__WAN_TYPE__FRAME_RELAY = 2,
			C__WAN_TYPE__ATM = 3,
			C__WAN_TYPE__ISDN = 4,
			C__WAN_TYPE__XDSL = 5,
			C__WAN_TYPE__X21 = 6,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int type;
		public double capacity;

		public enum C__CATS__WAN__UNIT : int
		{
			C__WAN_CAPACITY_UNIT__MBITS = 1,
			C__WAN_CAPACITY_UNIT__KBITS = 2,
			C__WAN_CAPACITY_UNIT__GBITS = 3,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int capacity_unit;
		public string description;
	}
}
