﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATG__SOUND")]
	[CategoryDisplayName("Sound Card")]
	class SoundCard : Category
	{
		[JsonConverter(typeof(EnumDeserializer))]
		public int manufacturer;
		public string title;
		public string description;
	}
}
