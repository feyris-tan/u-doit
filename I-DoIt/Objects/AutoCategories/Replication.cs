﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATS__REPLICATION")]
	[CategoryDisplayName("Replication")]
	class Replication : Category
	{

		public enum C__CATS__REPLICATION__MECHANISM : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int replication_mechanism;
		public string description;
	}
}
