﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATG__UNIVERSAL_INTERFACE")]
	[CategoryDisplayName("Interface")]
	class Interface : Category
	{
		public string title;

		public enum C__CATG__UI_CONNECTION_TYPE : int
		{
			C__UI_CON_TYPE__MONITOR = 1,
			C__UI_CON_TYPE__MOUSE = 2,
			C__UI_CON_TYPE__KEYBOARD = 3,
			C__UI_CON_TYPE__KVM = 4,
			C__UI_CON_TYPE__PRINTER = 5,
			C__UI_CON_TYPE__MEMORY_DISK = 6,
			C__UI_CON_TYPE__CONSOLE = 7,
			C__UI_CON_TYPE__PHONE_DIGITAL = 8,
			C__UI_CON_TYPE__PHONE_ANALOG = 9,
			C__UI_CON_TYPE__PHONE_S0 = 10,
			C__UI_CON_TYPE__MULTIMEDIA = 11,
			C__UI_CON_TYPE__OTHER = 12,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int type;

		public enum C__CATG__UI_PLUG_TYPE : int
		{
			C__UI_PLUGTYPE__OTHER = 1,
			C__UI_PLUGTYPE__SERIAL = 2,
			USB = 3,
			FireWire = 4,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int plug;

		public enum C__CATG__UI__ASSIGNED_UI : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int assigned_connector;
		public int connector_sibling;
		public string description;
		public int relation_direction;
	}
}
