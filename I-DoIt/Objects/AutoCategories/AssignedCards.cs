﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATG__ASSIGNED_CARDS")]
	[CategoryDisplayName("Assigned Cards")]
	class AssignedCards : Category
	{

		public enum C__CATG__ASSIGNED_CARDS__OBJ : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int connected_obj;
	}
}
