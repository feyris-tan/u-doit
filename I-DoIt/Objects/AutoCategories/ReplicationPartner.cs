﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATS__REPLICATION_PARTNER")]
	[CategoryDisplayName("Replication Partner")]
	class ReplicationPartner : Category
	{

		public enum C__CATS__REPLICATION_PARTNER__TYPE : int
		{
			Master = 1,
			Slave = 2,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int type;

		public enum C__CATS__REPLICATION_PARTNER__OBJ : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int replication_partner;
		public string description;
	}
}
