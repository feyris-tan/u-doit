﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATS__DATABASE_OBJECTS")]
	[CategoryDisplayName("Database Objects")]
	class DatabaseObjects : Category
	{
		public string title;

		public enum C__CMDB__CATS__DATABASE_OBJECTS__TYPE : int
		{
			C__DATABASE_OBJECTS__TABLE = 1,
			C__DATABASE_OBJECTS__VIEW = 2,
			C__DATABASE_OBJECTS__SEQUENCE = 3,
			C__DATABASE_OBJECTS__SYNONYM = 4,
			C__DATABASE_OBJECTS__INDEX = 5,
			C__DATABASE_OBJECTS__CLUSTER = 6,
			C__DATABASE_OBJECTS__SNAPSHOT = 7,
			C__DATABASE_OBJECTS__PROCEDURE = 8,
			C__DATABASE_OBJECTS__FUNCTION = 9,
			C__DATABASE_OBJECTS__PACKAGE = 10,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public C__CMDB__CATS__DATABASE_OBJECTS__TYPE database_object;
		public string description;
	}
}
