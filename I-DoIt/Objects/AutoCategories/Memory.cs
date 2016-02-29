﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATG__MEMORY")]
	[CategoryDisplayName("Memory")]
	class Memory : Category
	{
		public object quantity;

        [JsonConverter(typeof(EnumDeserializer))]
	    public int title;

		[JsonConverter(typeof(EnumDeserializer))]
		public int manufacturer;

		[JsonConverter(typeof(EnumDeserializer))]
		public int type;
		[JsonConverter(typeof(FloatDeserializer))]
		public float capacity;

		public enum C__CATG__MEMORY_UNIT : int
		{
			C__MEMORY_UNIT__KB = 1,
			C__MEMORY_UNIT__MB = 2,
			C__MEMORY_UNIT__GB = 3,
			C__MEMORY_UNIT__TB = 4,
			C__MEMORY_UNIT__B = 1000,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public C__CATG__MEMORY_UNIT unit;
		public string description;

	    public override bool Equals(object obj)
	    {
	        Memory other = (obj as Memory);
	        if (other == null) return false;
	        return other.title == this.title;
	    }
	}

}
