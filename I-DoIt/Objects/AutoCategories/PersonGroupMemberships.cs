﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATS__PERSON_ASSIGNED_GROUPS")]
	[CategoryDisplayName("Person Group Memberships")]
	class PersonGroupMemberships : Category
	{
		[JsonConverter(typeof(EnumDeserializer))]
		public int connected_object;
		public int contact;
	}
}