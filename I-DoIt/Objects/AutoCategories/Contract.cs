﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATS__CONTRACT")]
	[CategoryDisplayName("Contract")]
	class Contract : Category
	{

		public enum C__CATS__CONTRACT__TYPE : int
		{
			Agreement_with_GuaranteeorWarranty = 1,
			Maintenance_Agreement = 2,
			Leasing = 3,
			Leasing_with_maintenance = 4,
			License_contract = 5,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int type;
		public string contract_no;
		public string customer_no;
		public string internal_no;
		public double costs;
		public string product;

		public enum C__CATS__CONTRACT__REACTION_RATE : int
		{
			_8x5x4 = 1,
			_24x7x4 = 2,
			Other = 3,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int reaction_rate;

		public enum C__CATS__CONTRACT__CONTRACT_STATUS : int
		{
			Active = 1,
			Terminated = 2,
			Finished = 3,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int contract_status;
		public DateTime start_date;
		public DateTime end_date;
		public int run_time;

		public enum C__CATS__CONTRACT__RUNTIME_PERIOD_UNIT : int
		{
			C__GUARANTEE_PERIOD_UNIT_MONTH = 1,
			C__GUARANTEE_PERIOD_UNIT_DAYS = 2,
			C__GUARANTEE_PERIOD_UNIT_WEEKS = 3,
			C__GUARANTEE_PERIOD_UNIT_YEARS = 4,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public C__CATS__CONTRACT__RUNTIME_PERIOD_UNIT run_time_unit;
		public string next_contract_end_date;

		public enum C__CATS__CONTRACT__END_TYPE : int
		{
			Notice_of_termination = 1,
			end_of_period = 2,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int end_type;
		public string next_notice_end_date;
		public DateTime notice_date;
		public int notice_period;

		public enum C__CATS__CONTRACT__NOTICE_UNIT : int
		{
			C__GUARANTEE_PERIOD_UNIT_MONTH = 1,
			C__GUARANTEE_PERIOD_UNIT_DAYS = 2,
			C__GUARANTEE_PERIOD_UNIT_WEEKS = 3,
			C__GUARANTEE_PERIOD_UNIT_YEARS = 4,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int notice_period_unit;

		public enum C__CATS__CONTRACT__NOTICE_PERIOD_TYPE : int
		{
			C__CONTRACT__FROM_NOTICE_DATE = 1,
			C__CONTRACT__ON_CONTRACT_END = 2,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public C__CATS__CONTRACT__NOTICE_PERIOD_TYPE notice_type;
		public int maintenance_period;

		public enum C__CATS__CONTRACT__MAINTENANCE_PERIOD_UNIT : int
		{
			C__GUARANTEE_PERIOD_UNIT_MONTH = 1,
			C__GUARANTEE_PERIOD_UNIT_DAYS = 2,
			C__GUARANTEE_PERIOD_UNIT_WEEKS = 3,
			C__GUARANTEE_PERIOD_UNIT_YEARS = 4,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int maintenance_period_unit;

		public enum C__CATS__CONTRACT__PAYMENT_PERIOD : int
		{
			C__CONTRACT__PAYMENT_PERIOD__MONTHLY = 1,
			C__CONTRACT__PAYMENT_PERIOD__QUARTERLY = 2,
			C__CONTRACT__PAYMENT_PERIOD__HALF_YEARLY = 3,
			C__CONTRACT__PAYMENT_PERIOD__YEARLY = 4,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int payment_period;
		public string description;
	}
}