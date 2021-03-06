﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATG__TELEPHONE_FAX")]
	[CategoryDisplayName("Telephone/fax")]
	class Telephonefax : Category
	{

		public enum C__CATG__TELEPHONE_FAX__TYPE : int
		{
			C__CMDB__TELEPHONE_FAX__TYPE_ANALOG = 1,
			C__CMDB__TELEPHONE_FAX__TYPE_VOIP = 2,
			C__CMDB__TELEPHONE_FAX__TYPE_ISDN = 3,
		}

		[JsonConverter(typeof(EnumDeserializer))]
		public int type;
		public string telephone_number;
		public string fax_number;
		public string extension;
		public string pincode;
		public string imei;
		public string description;
	}
}
