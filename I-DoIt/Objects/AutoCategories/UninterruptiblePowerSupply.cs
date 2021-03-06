﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATS__UPS")]
	[CategoryDisplayName("Uninterruptible Power Supply")]
	class UninterruptiblePowerSupply : Category
	{

		public enum C__CMDB__CATS__UPS__TYPE : int
		{
			Online = 1,
			Offline = 2,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int type;

		public enum C__CMDB__CATS__UPS__BATTERY_TYPE : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int battery_type;
		public int amount;
		public int charge_time;

		public enum C__CMDB__CATS__UPS__CHARGE_TIME_UNIT_OF_TIME : int
		{
			C__CMDB__UNIT_OF_TIME__SECOND = 1,
			C__CMDB__UNIT_OF_TIME__MINUTE = 2,
			C__CMDB__UNIT_OF_TIME__HOUR = 3,
			C__CMDB__UNIT_OF_TIME__DAY = 4,
			C__CMDB__UNIT_OF_TIME__MONTH = 5,
			C__CMDB__UNIT_OF_TIME__YEAR = 6,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public C__CMDB__CATS__UPS__CHARGE_TIME_UNIT_OF_TIME charge_time_unit;
		public int autonomy_time;

		public enum C__CMDB__CATS__UPS__AUTONOMY_TIME_UNIT_OF_TIME : int
		{
			C__CMDB__UNIT_OF_TIME__SECOND = 1,
			C__CMDB__UNIT_OF_TIME__MINUTE = 2,
			C__CMDB__UNIT_OF_TIME__HOUR = 3,
			C__CMDB__UNIT_OF_TIME__DAY = 4,
			C__CMDB__UNIT_OF_TIME__MONTH = 5,
			C__CMDB__UNIT_OF_TIME__YEAR = 6,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public C__CMDB__CATS__UPS__AUTONOMY_TIME_UNIT_OF_TIME autonomy_time_unit;
		public string description;
	}
}
