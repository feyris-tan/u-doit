﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATG__DATABASE_ASSIGNMENT")]
	[CategoryDisplayName("Database Assignment")]
	class DatabaseAssignment : Category
	{

		public enum C__CATG__DATABASE_ASSIGNMENT__TARGET_SCHEMA : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int database_assignment;

		public enum C__CATG__DATABASE_ASSIGNMENT__RELATION_OBJECT : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int runs_on;
	}
}
