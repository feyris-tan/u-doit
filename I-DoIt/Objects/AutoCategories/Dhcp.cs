﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATS__NET_DHCP")]
	[CategoryDisplayName("Dhcp")]
	class Dhcp : Category
	{

		public enum C__CMDB__CATS__NET_DHCP__TYPE : int
		{
			C__NET__DHCP_DYNAMIC = 1,
			C__NET__DHCP_RESERVED = 2,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public C__CMDB__CATS__NET_DHCP__TYPE type;

		public enum C__CMDB__CATS__NET_DHCP__TYPEV6 : int
		{
			C__NET__DHCPV6__DHCPV6 = 1,
			C__NET__DHCPV6__SLAAC_AND_DHCPV6 = 2,
			C__NET__DHCPV6__DHCPV6_RESERVED = 3,
			C__NET__DHCPV6__SLAAC_AND_DHCPV6_RESERVED = 4,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public C__CMDB__CATS__NET_DHCP__TYPEV6 typev6;
		public string range_from;
		public string range_to;
		public string description;
	}
}
