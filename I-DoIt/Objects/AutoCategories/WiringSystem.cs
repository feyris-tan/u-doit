﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATS__WS")]
	[CategoryDisplayName("Wiring System")]
	class WiringSystem : Category
	{

		public enum C__CATS__WS_NET_TYPE_TITLE_ID : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int title;
		public string description;
	}
}
