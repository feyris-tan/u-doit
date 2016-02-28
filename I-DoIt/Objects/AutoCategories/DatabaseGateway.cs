﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATS__DATABASE_GATEWAY")]
	[CategoryDisplayName("Database Gateway")]
	class DatabaseGateway : Category
	{
		public string type;
		public string host;
		public string port;
		public string user;

		public enum C__CATS__DATABASE_GATEWAY__TARGET_SCHEMA : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int target_schema;
		public string description;
	}
}