﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATG__NETWORK")]
	[CategoryDisplayName("Network")]
	class Network : Category
	{
		public string title;

		public enum C__CATG__INTERFACE_P_MANUFACTURER : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int manufacturer;

		public enum C__CATG__INTERFACE_P_MODEL : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int model;
		public string serial;
		public string slot;
		public string description;
	}
}
