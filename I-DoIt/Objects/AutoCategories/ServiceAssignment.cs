﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATG__NAGIOS_APPLICATION_REFS_NAGIOS_SERVICE")]
	[CategoryDisplayName("Service Assignment")]
	class ServiceAssignment : Category
	{

		public enum C__CATG__NAGIOS_ASSIGNED_SERVICES : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int assigned_objects;
	}
}
