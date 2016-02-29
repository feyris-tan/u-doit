﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATG__AUDIT")]
	[CategoryDisplayName("Audit")]
	class Audit : Category
	{
		public string title;

		public enum C__CMDB__CATG__AUDIT__TYPE : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int type;

		public enum C__CMDB__CATG__AUDIT__COMMISSION : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int commission;

		public enum C__CMDB__CATG__AUDIT__RESPONSIBLE : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int responsible;

		public enum C__CMDB__CATG__AUDIT__INVOLVED : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int involved;
		public DateTime period_manufacturer;
		public DateTime period_operator;
		public DateTime apply;
		public string result;
		public string fault;
		public string incident;
		public string description;
	}
}
