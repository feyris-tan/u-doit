﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATS__SERVICE")]
	[CategoryDisplayName("Services")]
	class Services : Category
	{
		public string title;
		public string specification;

		public enum C__CATS__SERVICE_MANUFACTURER_ID : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int service_manufacturer;
		public string release;
		public string description;
	}
}
