﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATS__LAYER2_NET_ASSIGNED_PORTS")]
	[CategoryDisplayName("Assigned Ports")]
	class AssignedPorts : Category
	{
		public int isys_obj__id;

		public enum C__CMDB__CATS__LAYER2_NET_ASSIGNED_PORTS__ISYS_CATG_PORT_LIST__ID : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int isys_catg_port_list__id;
	}
}
