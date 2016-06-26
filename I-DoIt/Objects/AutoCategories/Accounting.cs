﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATG__ACCOUNTING")]
	[CategoryDisplayName("Accounting")]
	class Accounting : Category
	{
		public string inventory_no;

		public enum C__CATG__ACCOUNTING__ACCOUNT : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int account;
		public DateTime acquirementdate;

		public enum C__CATG__PURCHASE_CONTACT : int
		{
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int contact;
		public double price;
		public double operation_expense;

		public enum C__CATG__ACCOUNTING__OPERATION_EXPENSE_INTERVAL : int
		{
			C__INTERVAL__PER_DAY = 1,
			C__INTERVAL__PER_WEEK = 2,
			C__INTERVAL__PER_MONTH = 3,
			C__INTERVAL__PER_YEAR = 4,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public C__CATG__ACCOUNTING__OPERATION_EXPENSE_INTERVAL operation_expense_interval;
		public string invoice_no;
		public string order_no;
		public int guarantee_period;

		public enum C__CATG__ACCOUNTING_GUARANTEE_PERIOD_UNIT : int
		{
			C__GUARANTEE_PERIOD_UNIT_MONTH = 1,
			C__GUARANTEE_PERIOD_UNIT_DAYS = 2,
			C__GUARANTEE_PERIOD_UNIT_WEEKS = 3,
			C__GUARANTEE_PERIOD_UNIT_YEARS = 4,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public C__CATG__ACCOUNTING_GUARANTEE_PERIOD_UNIT guarantee_period_unit;
		public string guarantee_period_status;
		public string description;
	}
}
