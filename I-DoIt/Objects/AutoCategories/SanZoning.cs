﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATS__SAN_ZONING")]
	[CategoryDisplayName("San Zoning")]
	class SanZoning : Category
	{
		public string title;

		public enum C__CATS__SAN_ZONING__MEMBERS : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int members;
		public string description;
	}
}